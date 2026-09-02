-- Seeds the "ON-SITE JOB;" sentinel value into goodstypes so it's selectable from the
-- job-intake Goods Type dropdown (wired 2026-09-02). This exact literal (including the
-- trailing semicolon) is how the legacy app flagged an on-site job - the entire
-- goodsincare column is set to this value instead of a normal goods type, matched by
-- exact equality everywhere (UPPER(goodsincare) = 'ON-SITE JOB;'), never a substring
-- match. 2,080 real migrated jobs already carry this value; it was never itself present
-- as a row in goodstypes, so this just makes it pickable going forward too.
INSERT INTO goodstypes (goodstypedescription)
SELECT 'ON-SITE JOB;'
WHERE NOT EXISTS (
    SELECT 1 FROM goodstypes WHERE UPPER(goodstypedescription) = 'ON-SITE JOB;'
);
