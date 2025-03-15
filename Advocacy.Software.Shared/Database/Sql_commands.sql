USE advocacy_database;

SELECT * FROM signatures;
DELETE FROM signatures 
WHERE IdSignature = 4;

SELECT * FROM customers;

DELETE FROM customers
WHERE IdCustomer = 3;