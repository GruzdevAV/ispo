IF OBJECT_ID(N'dbo.ListSales', N'V') IS NOT NULL
	DROP VIEW [dbo].ListSales;  
GO
IF OBJECT_ID(N'dbo.ListHistory', N'V') IS NOT NULL
	DROP VIEW [dbo].ListHistory;  
GO
IF OBJECT_ID(N'dbo.ListShopments', N'V') IS NOT NULL
	DROP VIEW [dbo].ListShopments;  
GO
IF OBJECT_ID(N'dbo.ListProductsOfDepartment', N'V') IS NOT NULL
	DROP VIEW [dbo].ListProductsOfDepartment;  
GO
IF OBJECT_ID(N'dbo.ListProducts', N'V') IS NOT NULL
	DROP VIEW [dbo].ListProducts;  
GO
CREATE VIEW ListSales AS
-- VIEW SALES LIST
SELECT SaleId, Sales.Datetime, Firstname+' '+Surname AS Worker, ProductName, CONCAT(SoldQuantity,' '+MeasurementUnits.ShortName) AS Sold, SalePrice, DepartmentName, Sales.DepartmentCode, Sales.WorkerId, Sales.ProductArticle, Sales.SoldQuantity
	FROM SALES INNER JOIN Departments ON Sales.DepartmentCode = Departments.DepartmentCode
		INNER JOIN Workers ON Sales.WorkerId = Workers.WorkerId
		INNER JOIN Surnames ON Workers.SurnameId = Surnames.SurnameId
		INNER JOIN Firstnames ON Workers.FirstnameId = Firstnames.FirstnameId
		INNER JOIN Products ON Sales.ProductArticle = Products.ProductArticleNumber
		INNER JOIN MeasurementUnits ON MeasurementUnits.Id = MeasurementUnitId
		WHERE SaleId NOT IN(SELECT SaleId FROM ReturnedSales)
GO
CREATE VIEW ListShopments AS
SELECT EntryId, Datetime, Firstname+' '+Surname AS Worker, ProductName, CONCAT(Quantity,' '+MeasurementUnits.ShortName) AS Shipped, DepartmentName, Departments.DepartmentCode, ProductArticleNumber
	FROM ShipmentsToAndWriteOffsFromWarehouses 
		INNER JOIN Departments ON ShipmentsToAndWriteOffsFromWarehouses.DepartmentCode = Departments.DepartmentCode
		INNER JOIN Workers ON ShipmentsToAndWriteOffsFromWarehouses.StorageWorkerId = Workers.WorkerId
		INNER JOIN Surnames ON Workers.SurnameId = Surnames.SurnameId
		INNER JOIN Firstnames ON Workers.FirstnameId = Firstnames.FirstnameId
		INNER JOIN Products ON ShipmentsToAndWriteOffsFromWarehouses.ProductId = Products.ProductArticleNumber
		INNER JOIN MeasurementUnits ON MeasurementUnits.Id = MeasurementUnitId
GO
CREATE VIEW ListHistory AS
SELECT EntryId, EnterHistory.WorkerId, Login, Datetime, IsSuccessful FROM EnterHistory
	INNER JOIN Workers ON EnterHistory.WorkerId = Workers.WorkerId
GO
CREATE VIEW ListProductsOfDepartment AS
SELECT ProductId, ProductName, DepartmentCode, SUM(Quantity) AS SUM_Quantity, CONCAT(SUM(Quantity), ' '+ShortName) AS N1, Price FROM
(
	SELECT ProductId, DepartmentCode, Quantity from ShipmentsToAndWriteOffsFromWarehouses
	WHERE TypeId=(SELECT TypeId FROM TypesOfStorageMovements WHERE Name=N'Погрузка')
	UNION ALL
	SELECT ProductId, DepartmentCode, -Quantity from ShipmentsToAndWriteOffsFromWarehouses
	WHERE TypeId=(SELECT TypeId FROM TypesOfStorageMovements WHERE Name=N'Списание')
	UNION ALL
	SELECT ProductArticle AS ProductId, DepartmentCode, -SoldQuantity AS Quantity from Sales
	WHERE SaleId NOT IN (SELECT SaleId FROM ReturnedSales)
) AS T1
	INNER JOIN Products ON ProductId = ProductArticleNumber
	INNER JOIN MeasurementUnits ON MeasurementUnitId = MeasurementUnits.Id
GROUP BY ProductId, ProductName, DepartmentCode, Price, ShortName
GO
CREATE VIEW ListProducts AS
SELECT ProductArticleNumber, ShortName, ProductName, Price FROM Products
	INNER JOIN MeasurementUnits ON MeasurementUnitId=Id
GO
CREATE VIEW ListWorkers AS
SELECT Surname, Firstname, DepartmentName, Roles.Name, Login, Password, PathToImage FROM Workers
	INNER JOIN Surnames ON Workers.SurnameId = Surnames.SurnameId
	INNER JOIN Firstnames ON Workers.FirstnameId = Firstnames.FirstnameId
	INNER JOIN Roles ON Workers.RoleId = Roles.RoleId
	INNER JOIN Images ON Workers.ImgId = Images.ImgId
	LEFT JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode
GO