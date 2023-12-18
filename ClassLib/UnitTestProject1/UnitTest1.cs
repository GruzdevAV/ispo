using DatabaseController;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class TestDatabase
    {
        private string sqlstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\HW\UP_02.01\Задание 6\ClassLib\ClassLib\App_Data\DB1.mdf"";Integrated Security=True";
        string sqlCreate = "IF OBJECT_ID('DBO.STRINGS', N'U') is not NULL\r\n    DROP TABLE STRINGS;\r\nGO\r\nIF OBJECT_ID('DBO.USERS', N'U') is not NULL\r\n    DROP TABLE USERS;\r\nGO\r\n\r\nCREATE TABLE Users (\r\n    ID       INT PRIMARY KEY IDENTITY (1, 1),\r\n    Login    NVARCHAR (50)  unique not NULL,\r\n    Password NVARCHAR (100) not NULL\r\n)\r\nGO\r\n\r\nCREATE TABLE STRINGS (\r\n    ID INT PRIMARY KEY IDENTITY(1,1),\r\n    UserId INT FOREIGN KEY REFERENCES USERS(ID) NOT NULL,\r\n    STRING NVARCHAR(MAX)\r\n)\r\nGO";
        private Database database;
        public TestDatabase()
        {
            database = Database.GetInstance(sqlstr);
        }

        [TestMethod]
        public void TM_01TestExecuteSqlScript()
        {
            object result = database.GetScalar("SELECT 124");

            Assert.AreEqual(result.ToString(), "124");
        }
        [TestMethod]
        public void TM_02CreatingDatabase()
        {
            object result = database.Execute(sqlCreate);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void TM_03ExecuteCommandWithParameters()
        {
            string query = "INSERT INTO users (login, password) VALUES (@login, @password)";

            Parameter[] par =
            {
                new Parameter("@login", "user1"),
                new Parameter("@password", "12345")
            };

            int rows = database.Execute(query, par);

            Assert.AreEqual(1, rows);
        }
        [TestMethod]
        public void TM_04ImitateLogin()
        {
            string query = "SELECT * from users  where login=@login and password=@password";

            Parameter[] par =
            {
                new Parameter("@login", "user1"),
                new Parameter("@password", "12345")
            };

            object[][] table = database.GetRowsData(query, par);

            Assert.AreEqual(1, table.Length);
        }
        [TestMethod]
        public void TM_05ExecuteCommandWithNoParameters()
        {
            string query = "INSERT INTO STRINGS (UserId, STRING) VALUES (1, 'Text message')";

            int rows = database.Execute(query);

            Assert.AreEqual(1, rows);
        }
        [TestMethod]
        public void TM_06GetScalarFromExistingTable()
        {
            string query = "SELECT COUNT(*) FROM users";

            object result = database.GetScalar(query);

            Assert.IsNotNull(
                result,
                "Не удалось получить число записей."
            );
        }
        [TestMethod]
        public void TM_07GetRowsFromOneTable()
        {
            string query = "SELECT * FROM users";

            object[][] result = database.GetRowsData(query);

            CollectionAssert.AllItemsAreNotNull(
                result,
                "Не удалось получить записи из таблицы."
            );
        }
        [TestMethod]
        public void TM_08GetRowsFromTwoTables()
        {
            string query = "SELECT * FROM users INNER JOIN STRINGS ON USERS.ID = STRINGS.USERID";

            object[][] result = database.GetRowsData(query);

            CollectionAssert.AllItemsAreNotNull(
                result,
                "Не удалось получить записи из таблицы."
            );
        }
        [TestMethod]
        public void TM_09PutDataIntoTwoTables()
        {
            string query = "INSERT INTO users (login, password) VALUES (@login, @password)";

            Parameter[] par =
            {
                new Parameter("@login", "user2"),
                new Parameter("@password", "12345")
            };

            int rows = database.Execute(query, par);
            Assert.AreEqual(1, rows);

            query = "INSERT INTO STRINGS (UserId, STRING) VALUES " +
                "((SELECT TOP 1 ID FROM users ORDER BY NEWID()), 'Text message1')," +
                "((SELECT TOP 1 ID FROM users ORDER BY NEWID()), 'Text message2')," +
                "((SELECT TOP 1 ID FROM users ORDER BY NEWID()), 'Text message3')";

            rows = database.Execute(query);
            Assert.AreEqual(3, rows);
        }
        [TestMethod]
        public void TM_10AutoIncrimentWorksFine()
        {
            string query1 = "select id from users where login='user1'";
            string query2 = "select id from users where login='user2'";

            int rows1 =(int) database.GetScalar(query1);
            int rows2 = (int)database.GetScalar(query2);
            Assert.AreEqual(1, rows1);
            Assert.AreEqual(2, rows2);
        }
    }
}