using System.Linq;

using NUnit.Framework;

using LetsGoPark.WebSite.Pages;
using LetsGoPark.WebSite.Models;
using LetsGoPark.WebSite.Services;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System.Numerics;
using System.Xml.Linq;
using System;

namespace UnitTests.Pages.UpdatePark
{
    /// <summary>
    /// Unit tests for UpdatePark
    /// </summary>
    public class UpdateParkTests
    {
        #region TestSetup
        /// <summary>
        /// Test Setup
        /// </summary>
        public static UpdateParkModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            pageModel = new UpdateParkModel(TestHelper.ParkService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet
        [Test]
        /// <summary>
        /// Invoke OnGet by a park id
        /// Tests if the Ids are equal
        /// </summary>
        public void OnGet_Valid_Should_Return_Parks()
        {
            // Arrange

            // Act
            //Pass in valid park Id to OnGet function
            pageModel.OnGet("LAKE SAMMAMISH STATE PARK");

            // Assert
            //Ensure model is valid, and the correct park was loaded per Id passed
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("LAKE SAMMAMISH STATE PARK", pageModel.Park.Id);
        }
        #endregion OnGet

        #region OnPost
        [Test]
        /// <summary>
        /// Create new park model
        /// Gather pageModel as return type for a successful OnPost call
        /// Tests if the model is valid, and the redirect goes to the correct index page
        /// </summary>
        public void OnPost_Valid_Should_Return_Parks()
        {
            // Arrange
            //Create new park model
            pageModel.Park = new ParksModel
            {
                Id = "LAKE SAMMAMISH STATE PARK 2",
                Image = "a",
                Url = "a",
                Description = "a",
                Ratings = null,
                Address = "a",
                Phone = "1234567890",
                Park_system = ParkSystemEnum.National,
                Activities = "Hiking, Camping",
                Map_brochure = "a",
                Permits = "a",
                Comments = null
            };

            // Act
            //Gather pageModel as return type for a successful OnPost call
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            // Ensure model is valid, and the redirect goes to the correct index page
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        [Test]
        /// <summary>
        /// Force an invalid error state
        /// Call OnPost method and get ActionResult
        /// Tests if the page model is in an invalid state
        /// </summary>
        public void OnPost_InValid_Model_NotValid_Return_Page()
        {
            // Arrange
            // Force an invalid error state
            pageModel.ModelState.AddModelError("bogus", "bogus error");

            // Act
            //Call OnPost method and get ActionResult
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            //Ensure page model is in an invalid state
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }
        #endregion OnPost
    }
}