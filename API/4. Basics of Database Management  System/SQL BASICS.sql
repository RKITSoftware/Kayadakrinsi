-- CREATION OF DATABASE
CREATE DATABASE inventory;

-- CHANGE DATABASE
USE inventory;

-- CREATION OF TABLE
CREATE TABLE PRO01(
	O01F01 INT NOT NULL PRIMARY KEY COMMENT 'PRODUCT ID',
    O01F02 VARCHAR(20) COMMENT 'PRODUCT NAME',
    O01F03 VARCHAR(20) COMMENT 'PRODUCT CATEGORY'
);

-- ALTERING TABLE TO ADD NEW COLUMN
ALTER TABLE 
	PRO01 
ADD 
	(O01F05 VARCHAR(20) COMMENT 'MANUFACTURER OF PRODUCT');

-- ALTERING TABLE TO RENAME COLUMN
ALTER TABLE 
	PRO01 
RENAME COLUMN 
	O01F05 TO O01F04;

-- INSERTING DATA INTO TABLE
INSERT INTO 
	PRO01 
VALUES 
	(1,'BOOK','STATIONERY','STA-MANUFACTURING'),
    (2,'CHOCOLATE','FOOD','CADEBURY'),
    (3,'BOOK','STATIONERY','BOOK-MANUFACTURING'),
    (4,'CHAIR','FURNITURE','YOUR-FURNITURE');
    
-- UPDATING DATA INTO TABLE
UPDATE 
	PRO01 
SET 
	PRO04='CADBURY' 
WHERE O01F01=2;
  
-- RETRIVING DATA FROM TABLE
SELECT 
	O01F01,O01F02,O01F03,O01F04 
FROM 
	PRO01
ORDER BY
	O01F01;
    
-- RETRIVING DATA IN SORTING MANNER
SELECT 
	O01F01,O01F02,O01F03,O01F04 
FROM 
	PRO01
ORDER BY
	O01F01 DESC;

-- WHERE CLUASE AND CONDITION FOR RETRIVING DATA
SELECT 
	O01F01,O01F02,O01F03,O01F04 
FROM 
	PRO01
WHERE 
	O01F02='BOOK';
    
-- PRIMARY KEY
CREATE TABLE PRO02(
	O02F01 INT NOT NULL PRIMARY KEY COMMENT 'PRODUCT ID',
    O02F02 DECIMAL(10,2) COMMENT 'PRODUCT PRICE'
);

-- DELETE TABLE INCLUDING SCHEMA
DROP TABLE PRO02;

-- FOREIGN KEY
CREATE TABLE PRO02(
	O02F01 INT NOT NULL PRIMARY KEY COMMENT 'PRODUCT ID',
    O02F02 DECIMAL(10,2) COMMENT 'PRODUCT PRICE',
    FOREIGN KEY (O02F01) REFERENCES PRO01(O01F01)
);

-- DELETING DATA FROM TABLE
DELETE 
FROM 
	PRO01
WHERE
	O01F01=4;

