-- Name: Roger Silva Santos Aguiar
-- Database for the Advocacy Software

CREATE TABLE States
(
	IdState INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	State   VARCHAR(2)
);

--****************************************************************************************
CREATE TABLE Cities
(
	IdCity  INT           IDENTITY(1,1) NOT NULL PRIMARY KEY,
	City    VARCHAR(100),
	IdState INT NOT NULL,
	FOREIGN KEY (IdState) REFERENCES States(IdState)
);

--****************************************************************************************
CREATE TABLE Address
(
	IdAddress     INT          IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Street        VARCHAR(100),
	Number        VARCHAR(10),
	Neighbourhood VARCHAR(100),
	ZipCode       VARCHAR(8),
	IdCity        INT NOT NULL,
	FOREIGN KEY (IdCity) REFERENCES Cities(IdCity)
);

-- ****************************************************************************************
CREATE TABLE Customers
(
	IdCustomer       INT          NOT NULL identity(1, 1) primary key,
	Name             VARCHAR(150),
	Nationality      VARCHAR(100),
	CivilStatus      VARCHAR(50),
	Profession       VARCHAR(100),
	IdentityCustomer VARCHAR(50),
	Cpf              VARCHAR(11),
	Cnpj             VARCHAR(14),
	Phone            VARCHAR(30),
	Email            VARCHAR(100),
	RegisterDate     Date,
	LastUpdate       Date,
	Id               VARCHAR(36),
	IdAddress        INT NOT NULL,
	FOREIGN KEY (IdAddress) REFERENCES Address(IdAddress)
);

-- **************************************************************************************
CREATE TABLE Lawyers
(
	IdLawyer       INT          NOT NULL identity(1, 1) primary key,
	Name           VARCHAR(150),
	Nationality    VARCHAR(100),
	CivilStatus    VARCHAR(50),
	Profession     VARCHAR(100),
	IdentityLawyer VARCHAR(50),
	Cpf            VARCHAR(11),
	Cnpj           VARCHAR(14),
	Phone          VARCHAR(30),
	Email          VARCHAR(100),
	OabNumber      VARCHAR(50),
	RegisterDate   Date,
	LastUpdate     Date,
	Id             VARCHAR(36),
	IdAddress      INT NOT NULL,
	FOREIGN KEY (IdAddress) REFERENCES Address(IdAddress)
);

-- ****************************************************************************************
CREATE TABLE Attorney
(
	IdAttorney     INT         IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	Location       VARCHAR(50),
	SpecificPowers VARCHAR(200),
	RegisterDate   Date,
	Id             VARCHAR(36),
	IdCustomer     INT NOT NULL,
	FOREIGN KEY (IdCustomer) REFERENCES Customers(IdCustomer)
);

-- ****************************************************************************************
CREATE TABLE Attorney_has_lawyer
(
	IdAttorney INT NOT NULL,
	IdLawyer   INT NOT NULL,
	FOREIGN KEY (IdAttorney) REFERENCES Attorney(IdAttorney),
	FOREIGN KEY (IdLawyer) REFERENCES Lawyers(IdLawyer)
);

-- ********************************************************************************************************

CREATE TABLE BankAccount
(
	IdBankAccount INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	AccountType   VARCHAR(50)        NOT NULL,
	AgencyNumber  VARCHAR(50)        NOT NULL,
	BankName      VARCHAR(100)       NOT NULL,
	IdLawyer      INT                NOT NULL,
	FOREIGN KEY (IdLawyer) REFERENCES Lawyers(IdLawyer)
);
-- ********************************************************************************************************

CREATE TABLE Signatures
(
	IdSignature      INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	FullName         VARCHAR(100)       NOT NULL,
	Username         VARCHAR(100)       NOT NULL,
	Email            VARCHAR(100)       NOT NULL,
	Password         VARCHAR(100)       NOT NULL,
	GuidSignature    VARCHAR(36)        NOT NULL,
	RegisterDate     VARCHAR(10)        NOT NULL,
	SignaturePrice   DECIMAL            NOT NULL,
	SignatureType    VARCHAR(36)        NOT NULL,
	ImageProfilePath VARCHAR(200)       NOT NULL,
	Genre            VARCHAR(200)       NOT NULL,
);

SELECT * FROM Signatures;
-- ********************************************************************************************************
-- Data manipulation scripts

INSERT INTO States
VALUES ('AC'), ('AL'), ('AP'), ('AM'), ('BA'),
       ('CE'), ('DF'), ('ES'), ('GO'), ('MA'),
	   ('MT'), ('MS'), ('MG'), ('PA'), ('PB'),
	   ('PR'), ('PE'), ('PI'), ('RJ'), ('RN'),
	   ('RS'), ('RO'), ('RR'), ('SC'), ('SP'),
	   ('SO'), ('TO');

INSERT INTO States
VALUES ('SE')

SELECT * FROM States;

DECLARE @Id INTEGER = (SELECT IdState FROM States WHERE State = 'AC');

-- Use this script to fill the cities table
INSERT INTO Cities
VALUES ('Assis Brasil', (SELECT IdState FROM States WHERE State = 'AC'));

SELECT * FROM Cities;

SELECT City AS Cidades
FROM Cities;

SELECT City 
FROM Cities
WHERE IdState = '13';

SELECT IdState FROM States WHERE State = 'MG';

SELECT * FROM States;

SELECT * FROM Cities;
SELECT IdCity FROM Cities WHERE City = 'Acrelândia';

SELECT * FROM Address;

SELECT * FROM Cities WHERE City = 'Belo Horizonte';

SELECT * FROM Cities WHERE IdCity = 2895;

SELECT * FROM Customers
DELETE FROM Address WHERE IdAddress = 14;

INSERT INTO Customers (Name, Nationality, CivilStatus, Profession, IdentityCustomer, CpfOrCnpj, Phone, Email, RegisterDate, LastUpdate, Id, IdAddress)
VALUES ('Roger', 'Brasileiro', 'Casado', 'Analista de Sistemas', 'MG14766058', '07582635620', '31983453069', 'rogerdaviola@yahoo.com.br',
        '09/03/2023', '09/03/2023', '1233', '3');

-- DELETE FROM Customers WHERE IdCustomer = 10;

ALTER TABLE Customers
ALTER COLUMN RegisterDate VARCHAR(10);

SELECT * FROM Address WHERE IdAddress = 6;

ALTER TABLE Customers
ALTER COLUMN CpfOrCnpj VARCHAR(14);

SELECT * FROM States WHERE IdState = 13;

EXEC sp_rename 'Customers.Cpf', 'CpfOrCnpj';

ALTER TABLE Customers
DROP COLUMN Cnpj;

DELETE FROM Customers WHERE Id = 'bc07ce51-9b99-4238-ad84-751fc9d442ac';
SELECT * FROM Address;

SELECT * FROM Customers
DELETE FROM Address WHERE IdAddress = 9 AND Number = 430;

ALTER TABLE Address
ADD Id VARCHAR(36);

UPDATE Address
SET Id = 'bc07ce51-9b99-4238-ad84-751fc9d442ac'
WHERE IdAddress = 7;

SELECT IdAddress 
FROM Address 
WHERE Id = 'b825fb5f-6e98-44b4-b232-c8296cc32f64';

DELETE FROM Address
WHERE Id = 'b825fb5f-6e98-44b4-b232-c8296cc32f64';

UPDATE Customers
SET CpfOrCnpj = '19754752000176'
WHERE Id = '3a5fbb67-8b43-4db1-99b7-884899fd2e33';

SELECT * FROM Lawyers;

ALTER TABLE Lawyers
ADD CpfOrCnpj VARCHAR(14);

ALTER TABLE Lawyers
DROP COLUMN Cpf;

ALTER TABLE Lawyers
ALTER COLUMN CpfOrCnpj VARCHAR(14);

SELECT * FROM Address;

DELETE FROM Address
WHERE IdAddress = 29;

ALTER TABLE Lawyers
ALTER COLUMN LastUpdate VARCHAR(20);

ALTER TABLE Address
ADD Complement VARCHAR(200);

ALTER TABLE Lawyers
DROP COLUMN Complement;

SELECT * FROM Address
WHERE Id = 'd2940a49-dbf7-4f0a-94a0-59cda6a45a84';

UPDATE Address
SET Complement = 'Residencial'
WHERE IdCity = 1689;

SELECT * FROM Customers WHERE CpfOrCnpj = '07582635620';

SELECT Cities.City
FROM Cities, Lawyers, Address
WHERE Address.IdAddress = 30 AND Cities.IdCity = 2895 AND Lawyers.IdLawyer = 2;

SELECT *
FROM Cities, Lawyers, Address
WHERE Address.IdAddress = 30 AND Cities.IdCity = 2895 AND Lawyers.IdLawyer = 2;

SELECT * FROM Address;
SELECT * FROM Lawyers;
SELECT * FROM Cities;
SELECT * FROM Customers;
SELECT * FROM BankAccount ORDER BY BankName;

ALTER TABLE BankAccount
ADD Id VARCHAR(36) NOT NULL;

-- DELETE BankAccount WHERE IdBankAccount = 3;
SELECT * FROM Lawyers WHERE IdLawyer = 2

DELETE BankAccount WHERE IdBankAccount = 7;

UPDATE BankAccount 
SET BankName = '237 – Banco Bradesco S.A.', AccountType = 'Conta corrente', AgencyNumber = '1234', 
AccountNumber = '1003818-9', Pix = '31983453069'
WHERE Id = '2913a171-0573-466b-be91-577a3b98113c';

SELECT * FROM Signatures;

SELECT * FROM Signatures WHERE Password = '123';

INSERT INTO Signatures (FullName, Username, Email, Password, GuidSignature, RegisterDate, SignaturePrice, SignatureType, ImageProfilePath, Genre, UserType)
VALUES ('Roger Silva Santos Aguiar', 'Roger Aguiar', 'rogerdaviola@yahoo.com.br', '123', '132e89de-a13f-476c-841d-8b2da79f699e', '23-03-2023', '100', 'Mensal', 'C:/dev/Vana_Assis/images', 'Masculino', 'Administrador');

UPDATE Signatures
SET UserType = 'Usuário'
WHERE IdSignature = 4;

DELETE FROM Signatures;

SELECT * FROM Signatures2;

CREATE TABLE Signatures
(
	IdSignature      INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	FullName         VARCHAR(100)       NOT NULL,
	Username         VARCHAR(100)       NOT NULL,
	Email            VARCHAR(100)       NOT NULL,
	Password         VARCHAR(100)       NOT NULL,
	GuidSignature    VARCHAR(36)        NOT NULL,
	RegisterDate     VARCHAR(10)        NOT NULL,
	SignaturePrice   DECIMAL            NOT NULL,
	SignatureType    VARCHAR(36)        NOT NULL,
	ImageProfile     VARBINARY(MAX)     NULL,
	Genre            VARCHAR(200)       NOT NULL,
	UserType         VARCHAR(200)       NOT NULL,
);

SELECT * FROM Signatures;
DROP TABLE Signatures;

DELETE FROM Signatures
WHERE IdSignature = 3;


SELECT * FROM Customers;

ALTER TABLE BankAccount
ADD FOREIGN KEY (IdSignature) REFERENCES Signatures(IdSignature);

ALTER TABLE BankAccount
ADD IdSignature INT NOT NULL;

DELETE FROM Signatures WHERE IdSignature = 7;

SELECT * FROM Customers;
SELECT * FROM Lawyers;
SELECT * FROM Address;
SELECT * FROM Signatures;
SELECT * FROM BankAccount;
SELECT * FROM Cities;

SELECT * FROM Cities WHERE IdCity = 1491;
SELECT * FROM States WHERE State = 'CE';

DELETE FROM Signatures WHERE IdSignature = 16;
DELETE FROM Customers WHERE IdSignature = 6;
DELETE FROM Address WHERE IdSignature = 6;

ALTER TABLE Signatures
ADD ImageProfile VARBINARY(MAX) NULL

ALTER TABLE Signatures
ADD LogoHeader VARBINARY(MAX) NULL

ALTER TABLE Signatures
ADD LogoFooter VARBINARY(MAX) NULL

SELECT * FROM Address WHERE Id = '43c7a846-520a-48e1-8c6e-9ffa28987ae6';

-- DELETE FROM Address WHERE Id = '84b9ae21-2d95-427a-b8ed-c67988ccffbc';

SELECT * FROM Lawyers WHERE IdSignature = 19;

SELECT * FROM Address WHERE IdAddress = 44 and IdSignature = 19;

SELECT * FROM Cities WHERE IdCity = 3567;

-- DELETE FROM Lawyers
-- WHERE IdAddress = 49 AND IdSignature = 19

DELETE FROM Address
WHERE IdAddress = 60 AND Id = '7cb800df-0af1-4e09-a077-63f9c467815d';

-- DELETE FROM Lawyers
-- WHERE IdAddress = 50 AND CivilStatus = 'Viúvo';

DELETE FROM Signatures WHERE IdSignature = 13;
SELECT * FROM Customers;
DELETE FROM Customers WHERE IdSignature = 13 AND IdCustomer = 33;
SELECT * FROM Address;
SELECT * FROM Signatures;

SELECT * FROM Lawyers;
SELECT * FROM BankAccount WHERE IdSignature = 19;

DELETE FROM BankAccount WHERE IdBankAccount = 38 AND IdSignature = 19;

SELECT * FROM Cities;

SELECT * FROM Address;
SELECT * FROM Address WHERE Id = 'ca8e4a65-8223-4b35-b152-3a82edd02d82';

SELECT * FROM States WHERE IdState = 2895;

ALTER TABLE Lawyers
ADD AppPassword VARCHAR(100) NOT NULL;