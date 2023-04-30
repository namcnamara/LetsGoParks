using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using LetsGoPark.WebSite.Models;
using System.Linq;
namespace UnitTests.Pages.Park.AddRating
{
    public class JsonFileParksServiceTests
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        #region AddRating
        [Test]
        public void AddRating_InValid_Boundary_Below_0_Valid_ID_Should_Return_False()
        {
            // Arrange
            var data = TestHelper.ParkService.GetParks().First();

            // Act

            var result = TestHelper.ParkService.AddRating(data.Id, -1);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AddRating_InValid_Boundary_Above_5_Valid_ID_Should_Return_False()
        {
            // Arrange
            var data = TestHelper.ParkService.GetParks().First();

            // Act
            var result = TestHelper.ParkService.AddRating(data.Id, 6);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AddRating_InValid_Park_Null_Valid_Rating_Should_Return_False()
        {
            // Arrange

            // Act

            var result = TestHelper.ParkService.AddRating(null, 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AddRating_InValid_Park_ID_Valid_Rating_Should_Return_False()
        {
            // Arrange

            // Act

            var result = TestHelper.ParkService.AddRating("BadId", 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AddRating_Empty_ParkArray_Rating_Valid_Should_Return_True()
        {
            // Arrange
            

            // Act
            //Id with empty ratings
            var result = TestHelper.ParkService.AddRating("jenlooper-light", 1);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void AddRating_Valid_Park_Valid_Rating_Valid_Should_Return_True()
        {
            // Arrange

            // Get the First data item
            var data = TestHelper.ParkService.GetParks().First();
            var countOriginal = data.Ratings.Length;

            // Act
            var result = TestHelper.ParkService.AddRating(data.Id, 5);
            var dataNewList = TestHelper.ParkService.GetParks().First();

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(countOriginal + 1, dataNewList.Ratings.Length);
            Assert.AreEqual(5, dataNewList.Ratings.Last());
        }
        #endregion AddRating

    }
}
