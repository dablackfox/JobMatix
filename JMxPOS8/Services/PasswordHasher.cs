using System;
using System.Security.Cryptography;

namespace JMxPOS8.Services;

// ROADMAP.md Phase 1, still-open security item: staff.password was stored plaintext.
// Investigated 2026-09-02: the field is genuinely vestigial today - the real identity
// mechanism in this app is barcode/staff-number entry (a communal-till design, see
// MainWindowViewModel's own comment on IsStaffAdminUnlocked), and all 45 real staff rows
// have password = ''. Hashed anyway, defensively, so the column is safe the day it does
// become load-bearing (a PIN/password-based admin override is noted as "deferred", not
// dropped) rather than waiting for a real incident to force the fix.
//
// PBKDF2 via the BCL (Rfc2898DeriveBytes), not a new NuGet dependency - matches this
// project's established preference for avoiding dependencies where a stdlib primitive
// does the job (see JobDocumentPdfService's PDFsharp-over-QuestPDF licensing note for the
// same pattern). Encoded as "iterations:base64(salt):base64(hash)" so the format is
// self-describing and the iteration count can be raised later without breaking old hashes.
public static class PasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100_000;

    public static string Hash(string plainPassword)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(plainPassword, salt, Iterations, HashAlgorithmName.SHA256, HashSize);
        return $"{Iterations}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
    }

    public static bool Verify(string plainPassword, string storedHash)
    {
        var parts = storedHash.Split(':');
        if (parts.Length != 3 || !int.TryParse(parts[0], out var iterations))
            return false;

        var salt = Convert.FromBase64String(parts[1]);
        var expectedHash = Convert.FromBase64String(parts[2]);
        var actualHash = Rfc2898DeriveBytes.Pbkdf2(plainPassword, salt, iterations, HashAlgorithmName.SHA256, expectedHash.Length);
        return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
    }
}
