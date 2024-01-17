USE MDK_02_02_GRUZDEV
GO
--dependency level 3
IF OBJECT_ID (N'dbo.StudentCourses', N'U') IS NOT NULL
	DROP TABLE dbo.StudentCourses
GO
--dependency level 2
IF OBJECT_ID (N'dbo.Courses', N'U') IS NOT NULL
	DROP TABLE dbo.Courses
GO
--dependency level 1
IF OBJECT_ID (N'dbo.Students', N'U') IS NOT NULL
	DROP TABLE dbo.Students
GO

IF OBJECT_ID (N'dbo.Teachers', N'U') IS NOT NULL
	DROP TABLE dbo.Teachers
GO
--dependency level 0
IF OBJECT_ID (N'dbo.Firstnames', N'U') IS NOT NULL
	DROP TABLE dbo.Firstnames
GO

IF OBJECT_ID (N'dbo.Surnames', N'U') IS NOT NULL
	DROP TABLE dbo.Surnames
GO
--dependency level 0
CREATE TABLE Firstnames
(
	Id INT IDENTITY(1,1)
,	Firstname NVARCHAR(200) NOT NULL
,	CONSTRAINT PK_Firstnames_Id PRIMARY KEY (Id)
,	CONSTRAINT UQ_Firstnames_Firstname UNIQUE (Firstname)
)
GO

CREATE TABLE Surnames
(
	id INT IDENTITY(1,1)
,	Surname NVARCHAR(200) NOT NULL
,	CONSTRAINT PK_Surnames_id PRIMARY KEY (id)
,	CONSTRAINT UQ_Surnames_Surname UNIQUE (Surname)
)
GO
--dependency level 1
CREATE TABLE Students
(
	Id INT IDENTITY(1,1)
,	Firstname INT 
,	Surname INT 
,	CONSTRAINT PK_Students_Id PRIMARY KEY (Id)
,	CONSTRAINT FK_Students_to_Firstnames FOREIGN KEY (Firstname) REFERENCES Firstnames(Id)  
,	CONSTRAINT FK_Students_to_Surnames FOREIGN KEY (Surname) REFERENCES Surnames(id)  
)
GO

CREATE TABLE Teachers
(
	Id INT IDENTITY(1,1)
,	Firstname INT 
,	Surname INT 
,	CONSTRAINT PK_Teachers_Id PRIMARY KEY (Id)
,	CONSTRAINT FK_Teachers_to_Firstnames FOREIGN KEY (Firstname) REFERENCES Firstnames(Id)  
,	CONSTRAINT FK_Teachers_to_Surnames FOREIGN KEY (Surname) REFERENCES Surnames(id)  
)
GO
--dependency level 2
CREATE TABLE Courses
(
	Course_Id INT IDENTITY(1,1)
,	Name NVARCHAR(200) NOT NULL
,	Teacher INT 
,	CONSTRAINT PK_Courses_Course_Id PRIMARY KEY (Course_Id)
,	CONSTRAINT FK_Courses_to_Teachers FOREIGN KEY (Teacher) REFERENCES Teachers(Id)  
,	CONSTRAINT UQ_Courses_Name UNIQUE (Name)
)
GO
--dependency level 3
CREATE TABLE StudentCourses
(
	Id INT IDENTITY(1,1)
,	Stud_Id INT NOT NULL
,	Course_Id INT NOT NULL
,	CONSTRAINT PK_StudentCourses_Id PRIMARY KEY (Id)
,	CONSTRAINT FK_StudentCourses_to_Students FOREIGN KEY (Stud_Id) REFERENCES Students(Id)  
,	CONSTRAINT FK_StudentCourses_to_Courses FOREIGN KEY (Course_Id) REFERENCES Courses(Course_Id)  
)
GO
