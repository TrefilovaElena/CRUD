CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

INSERT INTO "Bookings" 
    ("Id", "UserId", "PlaceId", "CreatedDate", "BeginDate", "EndDate", "IsDeleted", "Price", "CashPaymentId")
SELECT 
    (i * -1) AS "Id",
    CASE 
        WHEN i < 4 THEN '2304d8a2-258c-46ae-87b0-1f7d51c60c62'::uuid
        WHEN i > 9 THEN NULL
        ELSE (i::text || '304d8a2-258c-46ae-87b0-1f7d51c60c62')::uuid
    END AS "UserId",
    CASE 
        WHEN i > 20 THEN (floor(i / 2))::int * -1
        ELSE i * -1
    END AS "PlaceId",
    now() AT TIME ZONE 'utc' AS "CreatedDate",
    CASE 
        WHEN i IN (10, 12) THEN now() AT TIME ZONE 'utc'
        WHEN i < 14 THEN now() AT TIME ZONE 'utc' + (i * INTERVAL '-3 day')
        ELSE now() AT TIME ZONE 'utc' + (i * INTERVAL '0.4 day')
    END AS "BeginDate",
    CASE 
        WHEN i IN (10, 12) THEN now() AT TIME ZONE 'utc' + INTERVAL '8 hour'
        WHEN i < 14 THEN now() AT TIME ZONE 'utc' + (i * INTERVAL '-3 day') + INTERVAL '3 hour'
        ELSE now() AT TIME ZONE 'utc' + (i * INTERVAL '0.4 day') + INTERVAL '3 hour'
    END AS "EndDate",
    false AS "IsDeleted",
    0 AS "Price",
    NULL AS "CashPaymentId"
FROM generate_series(1, 28) AS s(i);

INSERT INTO "Bookings" 
    ("Id", "UserId", "PlaceId", "CreatedDate", "BeginDate", "EndDate", "IsDeleted", "Price", "CashPaymentId")
SELECT 
    (i * -1) AS "Id",
    CASE 
       WHEN mod(i, 2) = 0 THEN uuid_generate_v4()
       ELSE NULL
    END AS "UserId",
    CASE 
       WHEN mod(i, 2) = 0 THEN -4
       ELSE -6
    END AS "PlaceId",
    now() AT TIME ZONE 'utc' AS "CreatedDate",
    now() AT TIME ZONE 'utc' + (i * INTERVAL '1 hour') AS "BeginDate",
    now() AT TIME ZONE 'utc' + (i * INTERVAL '1 hour') + INTERVAL '1 hour' AS "EndDate",
    false AS "IsDeleted",
    0 AS "Price",
    NULL AS "CashPaymentId"
FROM generate_series(30, 1029) AS s(i);
