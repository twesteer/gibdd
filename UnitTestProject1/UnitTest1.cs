using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using gibdd;
using gibdd.Models;
using gibdd.Windows;


namespace UnitTestProject1
{
    [TestClass]
    public class DriverTests
    {
        private Nachalnik nachalnik;

        [TestInitialize]
        public void Setup()
        {
            // Подготовка контекста
            nachalnik = new Nachalnik();
        }

        [TestMethod]
        public void AddDriver_ValidDriver_ShouldReturnTrue()
        {
            // Arrange
            var driver = new Drivers
            {
                id = 250,
                Name = "Иван",
                Middlename = "Иванович",
                PassportSerial = 1234,
                PassportNumber = 567890,
                Postcode = 123456,
                Phone = "+79111234567",
                Email = "ivan@example.com",
                Photo = "photo.jpg",
                Description = "Test description",
                id_Workplace = 1,
                id_Adress = 1,
                id_Licence = 1
            };

            // Act
            bool result = nachalnik.AddDriver(driver);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddDriver_InvalidDriver_ShouldReturnFalse()
        {
            // Arrange
            var driver = new Drivers
            {
                id = 251,
                Name = "",
                Middlename = "Иванович",
                PassportSerial = -1,
                PassportNumber = 0,
                id_Workplace = 0,
                id_Adress = 0,
                id_Licence = 0
            };

            // Act
            bool result = nachalnik.AddDriver(driver);

            // Assert
            Assert.IsFalse(result);
        }
    }
    [TestClass]
    public class AuthenticationTests
    {
        private Login login;

        [TestInitialize]
        public void Setup()
        {
            login = new Login();
        }

        [TestMethod]
        public void AuthenticateUser_ValidCredentials_ShouldReturnTrueAndRole()
        {
            // Arrange
            string role;

            // Act
            bool result = login.AuthenticateUser("nachalnik", "nachalnik", out role);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("nachalnik", role);
        }

        [TestMethod]
        public void AuthenticateUser_InvalidCredentials_ShouldReturnFalse()
        {
            // Arrange
            string role;

            // Act
            bool result = login.AuthenticateUser("123", "123", out role);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(role);
        }
    }



}
