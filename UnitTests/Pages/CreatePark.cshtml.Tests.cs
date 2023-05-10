using System.Linq;

using NUnit.Framework;

using LetsGoPark.WebSite.Pages;
using LetsGoPark.WebSite.Models;
using LetsGoPark.WebSite.Services;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Pages.CreatePark
{
    public class CreateParkTests
    {
        #region TestSetup
        public static CreateParkModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            pageModel = new CreateParkModel(TestHelper.ParkService)
            {
            };
        }

        #endregion TestSetup

        #region OnPost
        [Test]
        public void OnPost_Invalid_Model_Should_Not_Increase_Count()
        {
            // Arrange
            //Gets original number of parks in database
            var oldCount = TestHelper.ParkService.GetParks().Count();

            // Act
            //Adds park to database without URL field, will be invalid field
            pageModel.Park = new ParksModel()
            {
                Id = "",
                Image = "",
                Url = "",
                Description = "",
                Ratings = null,
                Address = "",
                Phone = "1234567890",
                Park_system = "City",
                Activities = "Hiking, Camping",
                Map_brochure = "",
                Permits = "",
                Comments = null
            };
            //Try to create park, should fail
            var result = pageModel.OnPost();

            // Assert
            //Values should be equal
            Assert.AreEqual(oldCount, TestHelper.ParkService.GetParks().Count());
        }

        [Test]
        public void OnPost_Force_Bad_State()
        {
            // Force an invalid error state
            pageModel.ModelState.AddModelError("bogus", "bogus error");

            // Act
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }

        [Test]
        public void OnPost_Valid_Should_Return_Incremented_Parks_Count()
        {
            // Arrange
            //Gets original number of parks in database
            var oldCount = TestHelper.ParkService.GetParks().Count();

            // Act
            //Adds park to database
            pageModel.Park = new ParksModel()
            {
                Id = "Enter Park Id",
                Url = "Enter URL",
                Image = "Enter Image URL",
                Description = "Enter Description",
                Ratings = null,
                Address = "Enter Park Address",
                Phone = "Enter Park Agency Phone Number",
                Park_system = "Enter \"National\", \"City\", Or \"WA State\"",
                Activities = "Enter activites separated by a comma, or NA",
                Map_brochure = "Enter Map brochure URL or NA",
                Permits = "Enter any fees associated with park",
                Comments = null,
            };
            pageModel.OnPost();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(oldCount + 1, TestHelper.ParkService.GetParks().Count());
        }

        #endregion OnPost
    }
}