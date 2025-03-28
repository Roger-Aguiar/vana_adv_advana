USE advocacy_database;

SELECT * FROM signatures;
DELETE FROM signatures 
WHERE IdSignature = 4;

SELECT * FROM customers;

DELETE FROM customers
WHERE IdCustomer = 3;

SELECT * FROM lawyers
WHERE IdSignature = 1;
SELECT * FROM bankaccount;

DESCRIBE lawyers;

ALTER TABLE lawyers DROP FOREIGN KEY fk_Lawyers_Address1;
ALTER TABLE lawyers 
DROP COLUMN Nationality,
DROP COLUMN CivilStatus,
DROP COLUMN Phone,
DROP COLUMN Email,
DROP COLUMN IdAddress,
DROP COLUMN AppPassword;

