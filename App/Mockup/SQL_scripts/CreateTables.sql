-- level 3
IF OBJECT_ID(N'dbo.ReturnedSales', N'U') IS NOT NULL
	DROP TABLE [dbo].ReturnedSales;  
GO
-- level 2
IF OBJECT_ID(N'dbo.EnterHistory', N'U') IS NOT NULL
	DROP TABLE [dbo].EnterHistory;  
GO
IF OBJECT_ID(N'dbo.Sales', N'U') IS NOT NULL
	DROP TABLE [dbo].Sales;  
GO
IF OBJECT_ID(N'dbo.ShipmentsToAndWriteOffsFromWarehouses', N'U') IS NOT NULL
	DROP TABLE [dbo].ShipmentsToAndWriteOffsFromWarehouses;  
GO
-- level 1
IF OBJECT_ID(N'dbo.Workers', N'U') IS NOT NULL
	DROP TABLE [dbo].Workers;  
GO
IF OBJECT_ID(N'dbo.Products', N'U') IS NOT NULL
	DROP TABLE [dbo].Products;  
GO
-- level 0
IF OBJECT_ID(N'dbo.TypesOfStorageMovements', N'U') IS NOT NULL
	DROP TABLE [dbo].TypesOfStorageMovements;  
GO
IF OBJECT_ID(N'dbo.Departments', N'U') IS NOT NULL
	DROP TABLE [dbo].Departments;  
GO
IF OBJECT_ID(N'dbo.Roles', N'U') IS NOT NULL
	DROP TABLE [dbo].Roles;  
GO
IF OBJECT_ID(N'dbo.Firstnames', N'U') IS NOT NULL
	DROP TABLE [dbo].Firstnames;  
GO
IF OBJECT_ID(N'dbo.Surnames', N'U') IS NOT NULL
	DROP TABLE [dbo].Surnames;  
GO
IF OBJECT_ID(N'dbo.MeasurementUnits', N'U') IS NOT NULL
	DROP TABLE [dbo].MeasurementUnits;  
GO
IF OBJECT_ID(N'dbo.Images', N'U') IS NOT NULL
	DROP TABLE [dbo].Images;  
GO

-- level 0
CREATE TABLE TypesOfStorageMovements (
	TypeId INT IDENTITY(1,1) PRIMARY KEY
,	Name NVARCHAR(20) UNIQUE NOT NULL
)
GO
CREATE TRIGGER ArchivistTypesOfStorageMovements ON TypesOfStorageMovements
AFTER UPDATE, DELETE AS
	INSERT INTO ArchiveTypesOfStorageMovements
	(TypeId, Name)
	SELECT TypeId, Name FROM deleted;

GO
CREATE TABLE Departments (
	DepartmentCode INT IDENTITY(1,1) PRIMARY KEY
,	DepartmentName NVARCHAR(50) UNIQUE NOT NULL
,	PhoneNumber VARCHAR(50)
,	PlannedSoldQuantityPerDay DECIMAL(18,4) NOT NULL
)
GO
CREATE TRIGGER ArchivistDepartments ON Departments
AFTER UPDATE, DELETE AS
	INSERT INTO ArchiveDepartments
	(DepartmentCode, DepartmentName, PhoneNumber, PlannedSoldQuantityPerDay)
	SELECT DepartmentCode, DepartmentName, PhoneNumber, PlannedSoldQuantityPerDay FROM deleted;

GO
CREATE TABLE Roles (
	RoleId INT IDENTITY(1,1) PRIMARY KEY
,	Name NVARCHAR(50) UNIQUE NOT NULL
)
GO
CREATE TRIGGER ArchivistRoles ON Roles
AFTER UPDATE, DELETE AS
	INSERT INTO ArchiveRoles
	(RoleId, Name)
	SELECT RoleId, Name FROM deleted;

GO
CREATE TABLE Firstnames (
	FirstnameId INT IDENTITY(1,1) PRIMARY KEY
,	Firstname NVARCHAR(50) UNIQUE NOT NULL
)
GO
CREATE TRIGGER ArchivistFirstnames ON Firstnames
AFTER UPDATE, DELETE AS
	INSERT INTO ArchiveFirstnames
	(FirstnameId, Firstname)
	SELECT FirstnameId, Firstname FROM deleted;

GO
CREATE TABLE Surnames (
	SurnameId INT IDENTITY(1,1) PRIMARY KEY
,	Surname NVARCHAR(50) UNIQUE NOT NULL
)
GO
CREATE TRIGGER ArchivistSurnames ON Surnames
AFTER UPDATE, DELETE AS
	INSERT INTO ArchiveSurnames
	(SurnameId, Surname)
	SELECT SurnameId, Surname FROM deleted;

GO
CREATE TABLE MeasurementUnits (
	Id INT IDENTITY(1,1) PRIMARY KEY
,	ShortName NVARCHAR(10) UNIQUE NOT NULL
,	Name NVARCHAR(50) UNIQUE NOT NULL
,	MinimalQuantity DECIMAL(9,4) NOT NULL
)
GO
CREATE TRIGGER ArchivistMeasurementUnits ON MeasurementUnits
AFTER UPDATE, DELETE AS
	INSERT INTO ArchiveMeasurementUnits
	(Id, ShortName, Name, MinimalQuantity)
	SELECT Id, ShortName, Name, MinimalQuantity FROM deleted;

GO
CREATE TABLE Images (
	ImgId INT IDENTITY(1,1) PRIMARY KEY
,	PathToImage NVARCHAR(500) UNIQUE NOT NULL
)
GO
CREATE TRIGGER ArchivistImages ON Images
AFTER UPDATE, DELETE AS
	INSERT INTO ArchiveImages
	(ImgId, PathToImage)
	SELECT ImgId, PathToImage FROM deleted;

GO
-- level 1
CREATE TABLE Workers (
	WorkerId INT IDENTITY(1,1) PRIMARY KEY
,	SurnameId INT FOREIGN KEY REFERENCES Surnames(SurnameId) NOT NULL
,	FirstnameId INT FOREIGN KEY REFERENCES Firstnames(FirstnameId) NOT NULL
,	DepartmentCode INT FOREIGN KEY REFERENCES Departments(DepartmentCode)
,	RoleId INT FOREIGN KEY REFERENCES Roles(RoleId) NOT NULL
,	Login NVARCHAR(50) UNIQUE NOT NULL
,	Password NVARCHAR(50) NOT NULL
,	ImgId INT FOREIGN KEY REFERENCES Images(ImgId)
)
GO
CREATE TRIGGER ArchivistWorkers ON Workers
AFTER UPDATE, DELETE AS
	INSERT INTO ArchiveWorkers
	(WorkerId, SurnameId, FirstnameId, DepartmentCode, RoleId, Login, Password, ImgId)
	SELECT WorkerId, SurnameId, FirstnameId, DepartmentCode, RoleId, Login, Password, ImgId FROM deleted;

GO
CREATE TABLE Products (
	ProductArticleNumber NVARCHAR(20) PRIMARY KEY NOT NULL
,	MeasurementUnitId INT FOREIGN KEY REFERENCES MeasurementUnits(Id) NOT NULL
,	ProductName NVARCHAR(50) UNIQUE NOT NULL
,	Price DECIMAL(18,4) NOT NULL
)
GO
CREATE TRIGGER ArchivistProducts ON Products
AFTER UPDATE, DELETE AS
	INSERT INTO ArchiveProducts
	(ProductArticleNumber, MeasurementUnitId, ProductName, Price)
	SELECT ProductArticleNumber, MeasurementUnitId, ProductName, Price FROM deleted;

GO
-- level 2
CREATE TABLE EnterHistory (
	EntryId INT IDENTITY(1,1) PRIMARY KEY
,	WorkerId INT FOREIGN KEY REFERENCES Workers(WorkerId) NOT NULL
,	Datetime DATETIME2 NOT NULL
,	IsSuccessful BIT NOT NULL
)
GO
ALTER TABLE EnterHistory
	ADD CONSTRAINT DefaultDatetime
	DEFAULT GETDATE() FOR Datetime;
GO
CREATE TRIGGER ArchivistEnterHistory ON EnterHistory
AFTER UPDATE, DELETE AS
	INSERT INTO ArchiveEnterHistory
	(EntryId, WorkerId, Datetime, IsSuccessful)
	SELECT EntryId, WorkerId, Datetime, IsSuccessful FROM deleted;

GO
CREATE TABLE Sales (
	SaleId INT IDENTITY(1,1) PRIMARY KEY
,	DepartmentCode INT FOREIGN KEY REFERENCES Departments(DepartmentCode) NOT NULL
,	ProductArticle NVARCHAR(20) FOREIGN KEY REFERENCES Products(ProductArticleNumber) NOT NULL
,	WorkerId INT FOREIGN KEY REFERENCES Workers(WorkerId) NOT NULL
,	Datetime DATETIME2 NOT NULL
,	SoldQuantity DECIMAL(18,4) NOT NULL
,	SalePrice DECIMAL(18,4) -- 
)
GO
CREATE TRIGGER ArchivistSales ON Sales
AFTER UPDATE, DELETE AS
	INSERT INTO ArchiveSales
	(SaleId, DepartmentCode, ProductArticle, WorkerId, Datetime, SoldQuantity,SalePrice)
	SELECT SaleId, DepartmentCode, ProductArticle, WorkerId, Datetime, SoldQuantity, SalePrice FROM deleted;

GO
CREATE TRIGGER DefaultSalePrice ON Sales
AFTER INSERT AS
UPDATE Sales
SET SalePrice = SoldQuantity  * (SELECT Price FROM Products WHERE ProductArticleNumber=ProductArticle)
WHERE SaleId IN (SELECT SaleId FROM inserted)
--END;
GO
CREATE TABLE ShipmentsToAndWriteOffsFromWarehouses (
	EntryId INT IDENTITY(1,1) PRIMARY KEY
,	DepartmentCode INT FOREIGN KEY REFERENCES Departments(DepartmentCode) NOT NULL
,	ProductId NVARCHAR(20) FOREIGN KEY REFERENCES Products(ProductArticleNumber) NOT NULL
,	StorageWorkerId INT FOREIGN KEY REFERENCES Workers(WorkerId) NOT NULL
,	TypeId INT FOREIGN KEY REFERENCES TypesOfStorageMovements(TypeId) NOT NULL
,	Datetime Datetime2 NOT NULL
,	Quantity DECIMAL(18,4) NOT NULL
)
GO
CREATE TRIGGER ArchivistShipmentsToAndWriteOffsFromWarehouses ON ShipmentsToAndWriteOffsFromWarehouses
AFTER UPDATE, DELETE AS
	INSERT INTO ArchiveShipmentsToAndWriteOffsFromWarehouses
	(EntryId, DepartmentCode, ProductId, StorageWorkerId, TypeId, Datetime, Quantity)
	SELECT EntryId, DepartmentCode, ProductId, StorageWorkerId, TypeId, Datetime, Quantity FROM deleted;

GO
-- level 3
CREATE TABLE ReturnedSales (
	SaleId INT PRIMARY KEY REFERENCES Sales(SaleId)
,	Reason NVARCHAR(MAX)
,	Datetime Datetime NOT NULL
)
GO
CREATE TRIGGER ArchivistReturnedSales ON ReturnedSales
AFTER UPDATE, DELETE AS
	INSERT INTO ArchiveReturnedSales
	(SaleId, Reason, Datetime)
	SELECT SaleId, Reason, Datetime FROM deleted;

GO

-- level 0
SELECT * FROM TypesOfStorageMovements
GO
SELECT * FROM Departments
GO
SELECT * FROM Roles
GO
SELECT * FROM Firstnames
GO
SELECT * FROM Surnames
GO
SELECT * FROM MeasurementUnits
GO
-- level 1
SELECT * FROM Workers
GO
SELECT * FROM Products
GO
-- level 2
SELECT * FROM EnterHistory
GO
SELECT * FROM Sales
GO
SELECT * FROM ShipmentsToAndWriteOffsFromWarehouses
GO
-- level 3
SELECT * FROM ReturnedSales
GO

