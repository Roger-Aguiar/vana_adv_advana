USE `bjthjgepvhxsr8sdyoxt`;
SELECT * FROM signatures;
SHOW TABLES;
DESCRIBE states;
INSERT INTO states (IdState, State) VALUES
(1, 'AC'),
(2, 'AL'),
(3, 'AP'),
(4, 'AM'),
(5, 'BA'),
(6, 'CE'),
(7, 'DF'),
(8, 'ES'),
(9, 'GO'),
(10, 'MA'),
(11, 'MT'),
(12, 'MS'),
(13, 'MG'),
(14, 'PA'),
(15, 'PB'),
(16, 'PR'),
(17, 'PE'),
(18, 'PI'),
(19, 'RJ'),
(20, 'RN'),
(21, 'RS'),
(22, 'RO'),
(23, 'RR'),
(24, 'SC'),
(25, 'SP'),
(26, 'SE'),
(27, 'TO');

SELECT * FROM states;
SELECT * FROM cities;
SELECT * FROM bankaccount;

SELECT * FROM customers
WHERE IdSignature = 7;

UPDATE customers
SET Phone = "11959237389"
WHERE IdCustomer = 5;

DESCRIBE lawyers;

ALTER TABLE lawyers DROP FOREIGN KEY fk_Lawyers_Address1;
ALTER TABLE lawyers 
DROP COLUMN Nationality,
DROP COLUMN CivilStatus,
DROP COLUMN Phone,
DROP COLUMN Email,
DROP COLUMN IdAddress,
DROP COLUMN AppPassword;
