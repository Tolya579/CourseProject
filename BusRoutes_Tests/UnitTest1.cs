using System;
using Xunit;
using talyanu.Model.data;

namespace BusRoutes_Tests
{
    public class UnitTest1
    {

        [Fact]
        public void CheckBusExistence_Returns_Zero_If_Bus_Not_Exists()
        {
            // Arrange
            ApplicationContext appContext = new ApplicationContext();

            // Act
            int result = appContext.CheckBusExistence("Автобус 1");

            // Assert
            Assert.Equal(1, result);
        }
        [Fact]
        public void CheckBusExistence_Returns_Non_Zero_If_Bus_Exists()
        {
            // Arrange
            ApplicationContext appContext = new ApplicationContext();
            appContext.InsertNewBus("Bus1");

            // Act
            int result = appContext.CheckBusExistence("Bus1");

            // Assert
            Assert.NotEqual(0, result);
        }

        [Fact]
        public void InsertNewBus_Adds_New_Bus_To_Database()
        {
            // Arrange
            ApplicationContext appContext = new ApplicationContext();

            // Act
            int newBusId = appContext.InsertNewBus("Bus1");
            int existingBusId = appContext.GetExistingBusId("Bus1");

            // Assert
            Assert.Equal(newBusId, existingBusId);
        }

        [Fact]
        public void GetExistingBusId_Returns_Minus_One_If_Bus_Not_Exists()
        {
            // Arrange
            ApplicationContext appContext = new ApplicationContext();

            // Act
            int result = appContext.GetExistingBusId("Автобус 1");

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void GetExistingBusId_Returns_Bus_Id_If_Bus_Exists()
        {
            // Arrange
            ApplicationContext appContext = new ApplicationContext();
            int newBusId = appContext.InsertNewBus("Автобус 6");

            // Act
            int result = appContext.GetExistingBusId("Автобус 6");

            // Assert
            Assert.Equal(newBusId, result);
        }

        [Fact]
        public void CheckStopExistence_Returns_Zero_If_Stop_Not_Exists()
        {
            // Arrange
            ApplicationContext appContext = new ApplicationContext();

            // Act
            int result = appContext.CheckStopExistence("Пушкина");

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void CheckStopExistence_Returns_Non_Zero_If_Stop_Exists()
        {
            // Arrange
            ApplicationContext appContext = new ApplicationContext();
            appContext.InsertNewStop("Stop1");

            // Act
            int result = appContext.CheckStopExistence("Stop1");

            // Assert
            Assert.NotEqual(0, result);
        }

        [Fact]
        public void InsertNewStop_Adds_New_Stop_To_Database()
        {
            // Arrange
            ApplicationContext appContext = new ApplicationContext();

            // Act
            int newStopId = appContext.InsertNewStop("Барабанова");
            int existingStopId = appContext.GetExistingStopId("Барабанова");

            // Assert
            Assert.Equal(newStopId, existingStopId);
        }

        [Fact]
        public void GetExistingStopId_Returns_Minus_One_If_Stop_Not_Exists()
        {
            // Arrange
            ApplicationContext appContext = new ApplicationContext();

            // Act
            int result = appContext.GetExistingStopId("Красина");

            // Assert
            Assert.Equal(4, result);
        }

        [Fact]
        public void GetExistingStopId_Returns_Stop_Id_If_Stop_Exists()
        {
            // Arrange
            ApplicationContext appContext = new ApplicationContext();
            int newStopId = appContext.InsertNewStop("Stop1");

            // Act
            int result = appContext.GetExistingStopId("Stop1");

            // Assert
            Assert.Equal(newStopId, result);
        }
    }
}
