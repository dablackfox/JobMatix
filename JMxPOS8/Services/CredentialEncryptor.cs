using System;
using System.Security.Cryptography;
using System.Text;

namespace JMxPOS8.Services;

// ROADMAP.md Phase 1: jobs.username/jobs.userpassword store customers' own PC login
// credentials (collected at intake so a tech can log into the machine being repaired) -
// found stored plaintext. Unlike staff.password (PasswordHasher.cs), this value has to
// come back out as real plaintext - it's printed on the job docket
// (JobDocumentPdfService.DrawUserLogonSection) so a tech has it on paper while working -
// so this is reversible AES-256-GCM encryption, not a one-way hash.
//
// AES-256-GCM via the BCL (System.Security.Cryptography.AesGcm), no new NuGet dependency -
// matches this project's established preference for stdlib over adding a package. Key
// comes from JOBMATIX_JOBS_CREDENTIAL_KEY (see .env's own comment for why it must be
// backed up separately from the database itself).
public static class CredentialEncryptor
{
    private const string Prefix = "ENC1:"; // distinguishes ciphertext from not-yet-migrated plaintext on read
    private const int NonceSize = 12;
    private const int TagSize = 16;

    private static byte[]? _key;

    private static byte[] GetKey()
    {
        if (_key != null)
            return _key;

        var base64Key = Environment.GetEnvironmentVariable("JOBMATIX_JOBS_CREDENTIAL_KEY");
        if (string.IsNullOrWhiteSpace(base64Key))
            throw new InvalidOperationException(
                "JOBMATIX_JOBS_CREDENTIAL_KEY is not set (.env) - required to encrypt/decrypt jobs.username/userpassword.");

        _key = Convert.FromBase64String(base64Key);
        if (_key.Length != 32)
            throw new InvalidOperationException("JOBMATIX_JOBS_CREDENTIAL_KEY must decode to exactly 32 bytes (AES-256).");

        return _key;
    }

    // Empty values stay empty (no point encrypting "no credential on file", and it keeps
    // GetOpenJobsAsync-style "username <> ''" filters meaningful without decrypting first).
    public static string Encrypt(string plaintext)
    {
        if (string.IsNullOrEmpty(plaintext))
            return plaintext;

        var nonce = RandomNumberGenerator.GetBytes(NonceSize);
        var plainBytes = Encoding.UTF8.GetBytes(plaintext);
        var cipherBytes = new byte[plainBytes.Length];
        var tag = new byte[TagSize];

        using var aes = new AesGcm(GetKey(), TagSize);
        aes.Encrypt(nonce, plainBytes, cipherBytes, tag);

        var combined = new byte[NonceSize + cipherBytes.Length + TagSize];
        Buffer.BlockCopy(nonce, 0, combined, 0, NonceSize);
        Buffer.BlockCopy(cipherBytes, 0, combined, NonceSize, cipherBytes.Length);
        Buffer.BlockCopy(tag, 0, combined, NonceSize + cipherBytes.Length, TagSize);

        return Prefix + Convert.ToBase64String(combined);
    }

    // Values without the ENC1: prefix are returned unchanged - covers empty strings and,
    // defensively, any row that somehow wasn't migrated rather than throwing on it.
    public static string Decrypt(string storedValue)
    {
        if (string.IsNullOrEmpty(storedValue) || !storedValue.StartsWith(Prefix, StringComparison.Ordinal))
            return storedValue;

        var combined = Convert.FromBase64String(storedValue[Prefix.Length..]);
        var nonce = combined[..NonceSize];
        var tag = combined[^TagSize..];
        var cipherBytes = combined[NonceSize..^TagSize];
        var plainBytes = new byte[cipherBytes.Length];

        using var aes = new AesGcm(GetKey(), TagSize);
        aes.Decrypt(nonce, cipherBytes, tag, plainBytes);

        return Encoding.UTF8.GetString(plainBytes);
    }

    public static bool IsEncrypted(string storedValue) =>
        !string.IsNullOrEmpty(storedValue) && storedValue.StartsWith(Prefix, StringComparison.Ordinal);
}
