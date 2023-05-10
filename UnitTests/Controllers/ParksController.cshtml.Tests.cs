using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using LetsGoPark.WebSite.Pages;
using LetsGoPark.WebSite.Controllers;
using LetsGoPark.WebSite.Services;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;

namespace UnitTests.Controllers
{
    /// <summary>
    /// Unit tests for ParksController class
    /// </summary>
    public class ParksControllerTests
    {
        #region TestSetup
        /// <summary>
        /// Test Setup
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }
        #endregion


        #region
        /// <summary>
        /// Creates a default datapoint of ParkService
        /// Creates a new datapoint of the a ParksController with datapoint
        /// Gets the whole datapoint
        /// Tests if Equal
        /// </summary>
        [Test]
        public void get_AllData_Present_Should_Return_True()
        {
            //arrange
            //Create new default ParkService datapoint

            //Act
            //store datapoint as a ParkController datapoint
            var newData = new ParksController(TestHelper.ParkService).Get().First();

            var response = TestHelper.ParkService.GetParks().First();

            //Assert
            Assert.AreEqual(newData.Id, response.Id);
        }
        #endregion

        #region
        /// <summary>
        /// Creates a default datapoint of ParkService
        /// Creates a new datapoint of the a ParksController with datapoint
        /// Gets the whole datapoint
        /// Tests if Added dataPoint equals the created one
        /// </summary>
        [Test]
        public void Patch_AddValid_Rating_Should_Return_True()
        {
            //arrange
            //Create new default ParkService datapoint

            //Act
            //store datapoint as a ParkController datapoint
            var newData = new ParksController(TestHelper.ParkService);
            //Create a newRating datapoint to "Patch to theDataController"
            var newRating = new ParksController.RatingRequest();
            {
                newRating.ParkId = newData.ParkService.GetParks().Last().Id;
                newRating.Rating = 5;
            }

            //Act
            newData.Patch(newRating);

            //Assert
            Assert.AreEqual(newData.ParkService.GetParks().Last().Id, newRating.ParkId);
        }
        #endregion
    }
}
