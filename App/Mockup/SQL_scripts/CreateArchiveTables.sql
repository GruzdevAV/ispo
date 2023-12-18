-- level 3
IF OBJECT_ID(N'dbo.ArchiveReturnedSales', N'U') IS NOT NULL  
	DROP TABLE [dbo].ArchiveReturnedSales;  
GO
-- level 2
IF OBJECT_ID(N'dbo.ArchiveEnterHistory', N'U') IS NOT NULL  
	DROP TABLE [dbo].ArchiveEnterHistory;  
GO
IF OBJECT_ID(N'dbo.ArchiveSales', N'U') IS NOT NULL  
	DROP TABLE [dbo].ArchiveSales;  
GO
IF OBJECT_ID(N'dbo.ArchiveShipmentsToAndWriteOffsFromWarehouses', N'U') IS NOT NULL  
	DROP TABLE [dbo].ArchiveShipmentsToAndWriteOffsFromWarehouses;  
GO
-- level 1
IF OBJECT_ID(N'dbo.ArchiveWorkers', N'U') IS NOT NULL  
	DROP TABLE [dbo].ArchiveWorkers;  
GO
IF OBJECT_ID(N'dbo.ArchiveProducts', N'U') IS NOT NULL  
	DROP TABLE [dbo].ArchiveProducts;  
GO
-- level 0
IF OBJECT_ID(N'dbo.ArchiveTypesOfStorageMovements', N'U') IS NOT NULL  
	DROP TABLE [dbo].ArchiveTypesOfStorageMovements;  
GO
IF OBJECT_ID(N'dbo.ArchiveDepartments', N'U') IS NOT NULL  
	DROP TABLE [dbo].ArchiveDepartments;  
GO
IF OBJECT_ID(N'dbo.ArchiveRoles', N'U') IS NOT NULL  
	DROP TABLE [dbo].ArchiveRoles;  
GO
IF OBJECT_ID(N'dbo.ArchiveFirstnames', N'U') IS NOT NULL  
	DROP TABLE [dbo].ArchiveFirstnames;  
GO
IF OBJECT_ID(N'dbo.ArchiveSurnames', N'U') IS NOT NULL  
	DROP TABLE [dbo].ArchiveSurnames;  
GO
IF OBJECT_ID(N'dbo.ArchiveMeasurementUnits', N'U') IS NOT NULL  
	DROP TABLE [dbo].ArchiveMeasurementUnits;  
GO
IF OBJECT_ID(N'dbo.ArchiveImages', N'U') IS NOT NULL  
	DROP TABLE [dbo].ArchiveImages;  
GO

-- level 0
CREATE TABLE ArchiveTypesOfStorageMovements (
	ArchiveId INT IDENTITY(1,1) PRIMARY KEY
,	DeletedTime DATETIME2
,	TypeId INT
,	Name NVARCHAR(20)
)
GO
ALTER TABLE ArchiveTypesOfStorageMovements
	ADD CONSTRAINT DefaultDateTypesOfStorageMovements
	DEFAULT GETDATE() FOR DeletedTime;
GO
CREATE TABLE ArchiveDepartments (
	ArchiveId INT IDENTITY(1,1) PRIMARY KEY
,	DeletedTime DATETIME2
,	DepartmentCode INT
,	DepartmentName NVARCHAR(50)
,	PhoneNumber VARCHAR(50)
,	PlannedSoldQuantityPerDay DECIMAL(18,4)
)
GO
ALTER TABLE ArchiveDepartments
	ADD CONSTRAINT DefaultDateDepartments
	DEFAULT GETDATE() FOR DeletedTime;
GO
CREATE TABLE ArchiveRoles (
	ArchiveId INT IDENTITY(1,1) PRIMARY KEY
,	DeletedTime DATETIME2
,	RoleId INT
,	Name NVARCHAR(50)
)
GO
ALTER TABLE ArchiveRoles
	ADD CONSTRAINT DefaultDateRoles
	DEFAULT GETDATE() FOR DeletedTime;
GO
CREATE TABLE ArchiveFirstnames (
	ArchiveId INT IDENTITY(1,1) PRIMARY KEY
,	DeletedTime DATETIME2
,	FirstnameId INT
,	Firstname NVARCHAR(50)
)
GO
ALTER TABLE ArchiveFirstnames
	ADD CONSTRAINT DefaultDateFirstnames
	DEFAULT GETDATE() FOR DeletedTime;
GO
CREATE TABLE ArchiveSurnames (
	ArchiveId INT IDENTITY(1,1) PRIMARY KEY
,	DeletedTime DATETIME2
,	SurnameId INT
,	Surname NVARCHAR(50)
)
GO
ALTER TABLE ArchiveSurnames
	ADD CONSTRAINT DefaultDateSurnames
	DEFAULT GETDATE() FOR DeletedTime;
GO
CREATE TABLE ArchiveMeasurementUnits (
	ArchiveId INT IDENTITY(1,1) PRIMARY KEY
,	DeletedTime DATETIME2
,	Id INT
,	ShortName NVARCHAR(10)
,	Name NVARCHAR(50)
,	MinimalQuantity DECIMAL(9,4)
)
GO
ALTER TABLE ArchiveMeasurementUnits
	ADD CONSTRAINT DefaultDateMeasurementUnits
	DEFAULT GETDATE() FOR DeletedTime;
GO
CREATE TABLE ArchiveImages (
	ArchiveId INT IDENTITY(1,1) PRIMARY KEY
,	DeletedTime DATETIME2
,	ImgId INT
,	PathToImage NVARCHAR(500)
)
GO
ALTER TABLE ArchiveImages
	ADD CONSTRAINT DefaultDateImages
	DEFAULT GETDATE() FOR DeletedTime;
GO
-- level 1
CREATE TABLE ArchiveWorkers (
	ArchiveId INT IDENTITY(1,1) PRIMARY KEY
,	DeletedTime DATETIME2
,	WorkerId INT
,	SurnameId INT
,	FirstnameId INT
,	DepartmentCode INT
,	RoleId INT
,	Login NVARCHAR(50)
,	Password NVARCHAR(50)
,	ImgId INT
)
GO
ALTER TABLE ArchiveWorkers
	ADD CONSTRAINT DefaultDateWorkers
	DEFAULT GETDATE() FOR DeletedTime;
GO
CREATE TABLE ArchiveProducts (
	ArchiveId INT IDENTITY(1,1) PRIMARY KEY
,	DeletedTime DATETIME2
,	ProductArticleNumber NVARCHAR(20)
,	MeasurementUnitId INT
,	ProductName NVARCHAR(50)
,	Price DECIMAL(18,4)
)
GO
ALTER TABLE ArchiveProducts
	ADD CONSTRAINT DefaultDateProducts
	DEFAULT GETDATE() FOR DeletedTime;
GO
-- level 2
CREATE TABLE ArchiveEnterHistory (
	ArchiveId INT IDENTITY(1,1) PRIMARY KEY
,	DeletedTime DATETIME2
,	EntryId INT
,	WorkerId INT
,	Datetime DATETIME2
,	IsSuccessful BIT
)
GO
ALTER TABLE ArchiveEnterHistory
	ADD CONSTRAINT DefaultDateEnterHistory
	DEFAULT GETDATE() FOR DeletedTime;
GO
CREATE TABLE ArchiveSales (
	ArchiveId INT IDENTITY(1,1) PRIMARY KEY
,	DeletedTime DATETIME2
,	SaleId INT
,	DepartmentCode INT
,	ProductArticle NVARCHAR(20)
,	WorkerId INT
,	Datetime DATETIME2
,	SoldQuantity DECIMAL(18,4)
,	SalePrice DECIMAL(18,4)
)
GO
ALTER TABLE ArchiveSales
	ADD CONSTRAINT DefaultDateSales
	DEFAULT GETDATE() FOR DeletedTime;
GO
CREATE TABLE ArchiveShipmentsToAndWriteOffsFromWarehouses (
	ArchiveId INT IDENTITY(1,1) PRIMARY KEY
,	DeletedTime DATETIME2
,	EntryId INT
,	DepartmentCode INT
,	ProductId NVARCHAR(20)
,	StorageWorkerId INT
,	TypeId INT
,	Datetime Datetime2
,	Quantity DECIMAL(18,4)
)
GO
ALTER TABLE ArchiveShipmentsToAndWriteOffsFromWarehouses
	ADD CONSTRAINT DefaultDateShipmentsToAndWriteOffsFromWarehouses
	DEFAULT GETDATE() FOR DeletedTime;
GO
-- level 3
CREATE TABLE ArchiveReturnedSales (
	ArchiveId INT IDENTITY(1,1) PRIMARY KEY
,	DeletedTime DATETIME2
,	SaleId INT
,	Reason NVARCHAR(MAX)
,	Datetime Datetime2
)
GO
ALTER TABLE ArchiveReturnedSales
	ADD CONSTRAINT DefaultDateReturnedSales
	DEFAULT GETDATE() FOR DeletedTime;
GO

-- level 0
SELECT * FROM ArchiveTypesOfStorageMovements
GO
SELECT * FROM ArchiveDepartments
GO
SELECT * FROM ArchiveRoles
GO
SELECT * FROM ArchiveFirstnames
GO
SELECT * FROM ArchiveSurnames
GO
SELECT * FROM ArchiveMeasurementUnits
GO
-- level 1
SELECT * FROM ArchiveWorkers
GO
SELECT * FROM ArchiveProducts
GO
-- level 2
SELECT * FROM ArchiveEnterHistory
GO
SELECT * FROM ArchiveSales
GO
SELECT * FROM ArchiveShipmentsToAndWriteOffsFromWarehouses
GO
-- level 3
SELECT * FROM ArchiveReturnedSales
GO

