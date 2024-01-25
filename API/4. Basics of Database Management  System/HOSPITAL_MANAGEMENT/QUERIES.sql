-- JOIN, LIMIT, ORDER BY
EXPLAIN ANALYZE SELECT 
	D01.D01F01 AS RECORD_ID,
    N01.N01F02 AS PATIENT_NAME,
    F01.F01F02 AS DOCTOR_NAME,
    S01.S01F02 AS DIEASES_NAME,
    F02.F02F02 AS HELPER_NAME,
    D01.D01F07 AS ADMIT_DATE,
    D01.D01F08 AS DISCHARGE_DATE,
    G01.G01F04 AS CHARGE
FROM
	RCD01 D01 JOIN PTN01 N01
ON 
	D01.D01F02 = N01.N01F01
JOIN 
	STF01 F01 
ON
	D01.D01F03 = F01.F01F01
JOIN
	STF02 F02
ON
	D01.D01F04 = F02.F02F01
JOIN
	DIS01 S01 
ON 
	D01.D01F05 = S01F01
JOIN
	CHRG01 G01 
ON
	D01.D01F06 = G01.G01F01;
-- ORDER BY 
-- 	CHARGE
-- LIMIT 2,5;
    
-- CREATE VIEW
CREATE OR REPLACE VIEW 
	`VWS_RCD01` 
AS
SELECT 
	D01.D01F01 AS RECORD_ID,
    N01.N01F02 AS PATIENT_NAME,
    F01.F01F02 AS DOCTOR_NAME,
    S01.S01F02 AS DIEASES_NAME,
    F02.F02F02 AS HELPER_NAME,
    G01.G01F04 AS CHARGE
FROM
	RCD01 D01,
    PTN01 N01,
    STF01 F01,
    STF02 F02,
    DIS01 S01,
    CHRG01 G01 
WHERE 
	D01.D01F02 = N01.N01F01 
AND 
	D01.D01F03 = F01.F01F01 
AND 
	D01.D01F04 = F02.F02F01 
AND 
	D01.D01F05 = S01.S01F01 
AND 
	D01.D01F06 = G01.G01F01;

-- SELECT FROM VIEW IN DESCENDING ORDER
SELECT 
	RECORD_ID,
    PATIENT_NAME,
    DOCTOR_NAME,
    DIEASES_NAME,
    HELPER_NAME,
    CHARGE
FROM 
	`VWS_RCD01`;
-- ORDER BY 
-- 	PATIENT_NAME 
-- DESC;

-- EXPLAIN
EXPLAIN ANALYZE SELECT 
	RECORD_ID,
    PATIENT_NAME,
    DOCTOR_NAME,
    DIEASES_NAME,
    HELPER_NAME,
    CHARGE
FROM 
	`VWS_RCD01`
ORDER BY 
	PATIENT_NAME 
DESC;

-- COUNT, SUB-QUERY
SELECT 
	COUNT(D01F01)
FROM 
	RCD01 
WHERE 
	D01F02 
IN 
	(SELECT N01F01 FROM PTN01 WHERE N01F02 LIKE "_I%");

-- UNION WITH DIFFERENT COLUMNS
SELECT 
	D01F01 AS RECORD_ID,
    D01F07 AS ADMITING_DATE,
    D01F08 AS DISCHARGE_DATE
FROM
	RCD01
UNION
SELECT
	N01F02
FROM
	PTN01
WHERE 
	N01F01
IN
	(SELECT D01F02 FROM RCD01);
  
-- UNION WITH SAME COLUMN
SELECT 
	D01F02
FROM 
	RCD01
UNION
SELECT 
	N01F01
FROM
	PTN01;
    
CREATE UNIQUE INDEX
	IND01
ON 
	RCD01(D01F07);
    
SHOW INDEX FROM
	RCD01;
    
SELECT IFNULL(D01F03,D01F08),D01F01 from rcd01;
SELECT ISNULL(D01F03),D01F01 from rcd01 order by d01f01;

