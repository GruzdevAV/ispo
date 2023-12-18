-- level 0
INSERT INTO TypesOfStorageMovements
	(Name)
	VALUES 
	(N'��������')
,	(N'��������')
GO
INSERT INTO Departments
	(DepartmentName, PhoneNumber, PlannedSoldQuantityPerDay)
	VALUES 
	(N'��������', '88005553531', 800000)
,	(N'�����', '88005553532', 850000)
--,	(N'��������', '88005553533', 750000)
--,	(N'���������', '88005553534', 810000)
GO
INSERT INTO Roles
	(Name)
	VALUES 
	(N'�������������')
,	(N'��������� ������')
,	(N'�������� ������')
,	(N'��������-������')
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
	(N'��', N'���������', 0.001)
,	(N'��', N'�����', 1)
,	(N'�',  N'�����', 1)
,	(N'�',  N'����', 0.001)
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
	, (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'�������������')
	, 'Admin', 'Admin_1'
	, (SELECT TOP 1 ImgId FROM Images ORDER BY Newid()) )
,	( (SELECT TOP 1 SurnameId FROM Surnames ORDER BY Newid())
	, (SELECT TOP 1 FirstnameId FROM Firstnames ORDER BY Newid())
	, (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'��������')
	, (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'��������-������')
	, 'Cassier.One', 'Pass_1'
	, (SELECT TOP 1 ImgId FROM Images ORDER BY Newid()) )
,	( (SELECT TOP 1 SurnameId FROM Surnames ORDER BY Newid())
	, (SELECT TOP 1 FirstnameId FROM Firstnames ORDER BY Newid())
	, (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'�����')
	, (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'��������-������')
	, 'Cassier.Two', 'Pass_1'
	, (SELECT TOP 1 ImgId FROM Images ORDER BY Newid()))
,	( (SELECT TOP 1 SurnameId FROM Surnames ORDER BY Newid())
	, (SELECT TOP 1 FirstnameId FROM Firstnames ORDER BY Newid())
	, (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'��������')
	, (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'�������� ������')
	, 'StorageWorker.One', 'Pass_1'
	, (SELECT TOP 1 ImgId FROM Images ORDER BY Newid()))
,	( (SELECT TOP 1 SurnameId FROM Surnames ORDER BY Newid())
	, (SELECT TOP 1 FirstnameId FROM Firstnames ORDER BY Newid())
	, (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'�����')
	, (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'�������� ������')
	, 'StorageWorker.Two', 'Pass_1'
	, (SELECT TOP 1 ImgId FROM Images ORDER BY Newid()))
,	( (SELECT TOP 1 SurnameId FROM Surnames ORDER BY Newid())
	, (SELECT TOP 1 FirstnameId FROM Firstnames ORDER BY Newid())
	, (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'��������')
	, (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'��������� ������')
	, 'DepHead.One', 'Pass_1'
	, (SELECT TOP 1 ImgId FROM Images ORDER BY Newid()))
,	( (SELECT TOP 1 SurnameId FROM Surnames ORDER BY Newid())
	, (SELECT TOP 1 FirstnameId FROM Firstnames ORDER BY Newid())
	, (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'�����')
	, (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'��������� ������')
	, 'DepHead.Two', 'Pass_1'
	, (SELECT TOP 1 ImgId FROM Images ORDER BY Newid()))
GO
INSERT INTO Products
--	('��', '���������', 0.001)
--,	('��', '�����', 1)
--,	('�',  '�����', 1)
--,	('�',  '����', 0.001)
	(ProductArticleNumber, MeasurementUnitId, ProductName, Price)
	VALUES 
	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'��'), N'�����', 89.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'��'), N'��������', 99.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'��'), N'������ ������ ����', 109.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'��'), N'�����', 359.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'��'), N'���������', 18.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'��'), N'�������', 39.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'��'), N'������', 39.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'��'), N'����', 69.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'��'), N'����� ���������', 139.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'��'), N'��������� 1', 139.99)
,	(SUBSTRING(CONVERT(NVARCHAR(255), NEWID()),0,8), (SELECT TOP 1 Id FROM MeasurementUnits WHERE ShortName=N'��'), N'��������� 2', 39.99)
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
	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'��������')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'��������' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'��������')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'��������' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'��������')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'��������' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'��������')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'��������' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'��������')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'��������' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'��������')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'��������' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'�����')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'�����' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'�����')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'�����' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'�����')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'�����' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'�����')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'�����' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'�����')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'�����' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'�����')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'�����' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'�����')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'�����' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
,	( (SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'�����')
	, (SELECT TOP 1 ProductArticleNumber FROM Products ORDER BY Newid())
	, (SELECT TOP 1 WorkerId FROM Workers INNER JOIN Departments ON Workers.DepartmentCode = Departments.DepartmentCode WHERE DepartmentName=N'�����' ORDER BY NEWID())
	, GETDATE(), FLOOR(RAND()*100+1))
GO
INSERT INTO ShipmentsToAndWriteOffsFromWarehouses
	(DepartmentCode, ProductId, StorageWorkerId, TypeId, Datetime, Quantity) 
--	((SELECT TOP 1 DepartmentCode FROM Departments WHERE DepartmentName=N'�����')
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
	AND Workers.RoleId = (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'�������� ������')
	AND TypesOfStorageMovements.TypeId = (SELECT TOP 1 TypeId FROM TypesOfStorageMovements WHERE Name=N'��������')
	UNION ALL
	SELECT Departments.DepartmentCode, ProductArticleNumber, WorkerId, TypeId, GETDATE(), 100
		FROM Departments, Products, Workers, TypesOfStorageMovements
	WHERE Workers.DepartmentCode = Departments.DepartmentCode
	AND Workers.RoleId = (SELECT TOP 1 RoleId FROM Roles WHERE Name=N'�������� ������')
	AND TypesOfStorageMovements.TypeId = (SELECT TOP 1 TypeId FROM TypesOfStorageMovements WHERE Name=N'��������')

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

