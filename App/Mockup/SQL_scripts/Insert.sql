-- level 0
INSERT INTO TypesOfStorageMovements
	(Name)
	VALUES 
	(N'Погрузка')
,	(N'Списание')
GO
INSERT INTO Departments
	(DepartmentName, PhoneNumber, PlannedSoldQuantityPerDay)
	VALUES 
	(N'Северный', '88005553531', 800000)
,	(N'Южный', '88005553532', 850000)
--,	(N'Западный', '88005553533', 750000)
--,	(N'Восточный', '88005553534', 810000)
GO
INSERT INTO Roles
	(Name)
	VALUES 
	(N'Администратор')
,	(N'Начальник отдела')
,	(N'Работник склада')
,	(N'Продавец-кассир')
GO
INSERT INTO Firstnames
	(Firstname)
	VALUES 
	('Gail')
,	('Anita')
,	('Mary')
,	('Marjorie')
,	('Harold')
,	('Alfred')
,	('Shirley')
,	('Lawrence')
,	('Raymond')
,	('Annette')
,	('Kimberly')
,	('Jeffery')
,	('Derek')
,	('Sarah')
,	('Gina')
,	('Anthony')
,	('Carole')
,	('Robert')
,	('Grace')
,	('Matthew')
,	('Eleanor')
,	('Joseph')
,	('Melanie')
,	('Virginia')
,	('Norma')
,	('Ralph')
,	('Laura')
,	('Michael')
,	('Jonathan')
,	('Diana')
,	('Kenneth')
,	('Barbara')
,	('Timothy')
,	('Janet')
,	('Anna')
,	('Doris')
,	('Michele')
,	('Fred')
,	('Angela')
,	('Joshua')
,	('Harry')
,	('Christina')
,	('Walter')
,	('Alice')
,	('Oscar')
,	('Louis')
,	('Crystal')
GO
INSERT INTO Surnames
	(Surname)
	VALUES 
	('Yates')
,	('Hill')
,	('French')
,	('Fleming')
,	('Cox')
,	('Taylor')
,	('Jackson')
,	('Howard')
,	('Woods')
,	('Lucas')
,	('Parker')
,	('Kim')
,	('Williams')
,	('Martin')
,	('Figueroa')
,	('Peters')
,	('Moore')
,	('Wade')
,	('Wilson')
,	('Brown')
,	('Chavez')
,	('Smith')
,	('Rios')
,	('Walsh')
,	('Beck')
,	('Griffin')
,	('Marshall')
,	('Todd')
,	('White')
,	('Chandler')
,	('Weaver')
,	('Peterson')
,	('Thomas')
,	('Gilbert')
,	('Massey')
,	('Clark')
,	('Craig')
,	('Webb')
,	('Owens')
,	('Johnson')
,	('Hayes')
,	('Price')
GO
INSERT INTO MeasurementUnits
	(ShortName, Name, MinimalQuantity)
	VALUES 
	(N'кг', N'килограмм', 0.001)
,	(N'шт', N'штука', 1)
,	(N'г',  N'грамм', 1)
,	(N'л',  N'литр', 0.001)
GO
INSERT INTO Images
	(PathToImage)
	VALUES 
	('.\Images\unnamed.jpg')
,	('.\Images\dummyimage.jpg')
,	('.\Images\black.png')
,	('.\Images\t1.png')
,	('.\Images\t2.jpg')
,	('.\Images\t3.jpg')
GO
-- level 1
INSERT INTO Workers
	(SurnameId, FirstnameId, DepartmentCode, RoleId, Login, Password, ImgId)
	VALUES 
	( (SELECT TOP 1 SurnameId FROM Surnames ORDER BY Newid())
	, (SELECT TOP 1 FirstnameId FROM Firstnames ORDER BY Newid())
	, Null
	, (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'Администратор')
	, 'Admin', 'Admin_1'
	, (SELECT TOP 1 ImgId FROM Images ORDER BY Newid()) )
,	( (SELECT TOP 1 SurnameId FROM Surnames ORDER BY Newid())
	, (SELECT TOP 1 FirstnameId FROM Firstnames ORDER BY Newid())
	, (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Северный')
	, (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'Продавец-кассир')
	, 'Cassier.One', 'Pass_1'
	, (SELECT TOP 1 ImgId FROM Images ORDER BY Newid()) )
,	( (SELECT TOP 1 SurnameId FROM Surnames ORDER BY Newid())
	, (SELECT TOP 1 FirstnameId FROM Firstnames ORDER BY Newid())
	, (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Южный')
	, (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'Продавец-кассир')
	, 'Cassier.Two', 'Pass_1'
	, (SELECT TOP 1 ImgId FROM Images ORDER BY Newid()))
,	( (SELECT TOP 1 SurnameId FROM Surnames ORDER BY Newid())
	, (SELECT TOP 1 FirstnameId FROM Firstnames ORDER BY Newid())
	, (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Северный')
	, (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'Работник склада')
	, 'StorageWorker.One', 'Pass_1'
	, (SELECT TOP 1 ImgId FROM Images ORDER BY Newid()))
,	( (SELECT TOP 1 SurnameId FROM Surnames ORDER BY Newid())
	, (SELECT TOP 1 FirstnameId FROM Firstnames ORDER BY Newid())
	, (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Южный')
	, (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'Работник склада')
	, 'StorageWorker.Two', 'Pass_1'
	, (SELECT TOP 1 ImgId FROM Images ORDER BY Newid()))
,	( (SELECT TOP 1 SurnameId FROM Surnames ORDER BY Newid())
	, (SELECT TOP 1 FirstnameId FROM Firstnames ORDER BY Newid())
	, (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Северный')
	, (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'Начальник отдела')
	, 'DepHead.One', 'Pass_1'
	, (SELECT TOP 1 ImgId FROM Images ORDER BY Newid()))
,	( (SELECT TOP 1 SurnameId FROM Surnames ORDER BY Newid())
	, (SELECT TOP 1 FirstnameId FROM Firstnames ORDER BY Newid())
	, (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Южный')
	, (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'Начальник отдела')
	, 'DepHead.Two', 'Pass_1'
	, (SELECT TOP 1 ImgId FROM Images ORDER BY Newid()))
GO
INSERT INTO Products
--	('кг', 'килограмм', 0.001)
--,	('шт', 'штука', 1)
--,	('г',  'грамм', 1)
--,	('л',  'литр', 0.001)
	(ProductArticleNumber, MeasurementUnitId, ProductName, Price)
	VALUES 
	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'кг'), N'Банан', 89.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'кг'), N'Апельсин', 99.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'кг'), N'Яблоко Гренни Смит', 109.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'кг'), N'Манго', 359.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'кг'), N'Картофель', 18.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'кг'), N'Морковь', 39.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'кг'), N'Свекла', 39.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'шт'), N'Квас', 69.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'шт'), N'Масло сливочное', 139.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'шт'), N'Мороженое 1', 139.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'шт'), N'Мороженое 2', 39.99)
GO
-- level 2
--INSERT INTO EnterHistory
--	(WorkerId, Datetime, IsSuccessful)
--	VALUES 
--	(WorkerId, Datetime, IsSuccessful)
--,	(WorkerId, Datetime, IsSuccessful)
--,	(WorkerId, Datetime, IsSuccessful)
--,	(WorkerId, Datetime, IsSuccessful)
--,	(WorkerId, Datetime, IsSuccessful)
--GO
INSERT INTO Sales
	(DepartmentCode, ProductArticle, WorkerId, Datetime, SoldQuantity)
	VALUES 
	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Северный')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'Северный' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Северный')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'Северный' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Северный')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'Северный' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Северный')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'Северный' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Северный')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'Северный' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Северный')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'Северный' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Южный')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'Южный' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Южный')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'Южный' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Южный')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'Южный' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Южный')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'Южный' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Южный')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'Южный' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Южный')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'Южный' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Южный')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'Южный' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Южный')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'Южный' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
GO
INSERT INTO ShipmentsToAndWriteOffsFromWarehouses
	(DepartmentCode, ProductId, StorageWorkerId, TypeId, Datetime, Quantity) 
--	((SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'Южный')
--	, ProductId
--	, StorageWorkerId
--	, TypeId
--	, Datetime, Quantity)
--,	(DepartmentCode, ProductId, StorageWorkerId, TypeId, Datetime, Quantity)
--,	(DepartmentCode, ProductId, StorageWorkerId, TypeId, Datetime, Quantity)
--,	(DepartmentCode, ProductId, StorageWorkerId, TypeId, Datetime, Quantity)
--,	(DepartmentCode, ProductId, StorageWorkerId, TypeId, Datetime, Quantity)
	SELECT Departments.DepartmentCode, ProductArticleNumber, WorkerId, TypeId, GETDATE(), 2500
		FROM Departments, Products, Workers, TypesOfStorageMovements
	WHERE Workers.DepartmentCode = Departments.DepartmentCode
	AND Workers.RoleId = (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'Работник склада')
	AND TypesOfStorageMovements.TypeId = (SELECT TOP 1 TypeId FROM TypesOfStorageMovements WHERE Name=N'Погрузка')
	UNION ALL
	SELECT Departments.DepartmentCode, ProductArticleNumber, WorkerId, TypeId, GETDATE(), 100
		FROM Departments, Products, Workers, TypesOfStorageMovements
	WHERE Workers.DepartmentCode = Departments.DepartmentCode
	AND Workers.RoleId = (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'Работник склада')
	AND TypesOfStorageMovements.TypeId = (SELECT TOP 1 TypeId FROM TypesOfStorageMovements WHERE Name=N'Списание')

GO
-- level 3
--INSERT INTO ReturnedSales
--	(Reason, Datetime)
--	VALUES 
--	(Reason, Datetime)
--,	(Reason, Datetime)
--,	(Reason, Datetime)
--,	(Reason, Datetime)
--,	(Reason, Datetime)
--GO

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

