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
    public class UpdateParkTests
    {
        #region TestSetup
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
        public void OnGet_Valid_Should_Return_Parks()
        {
            // Arrange

            // Act
            pageModel.OnGet("LAKE SAMMAMISH STATE PARK");

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("LAKE SAMMAMISH STATE PARK", pageModel.Park.Id);
        }
        #endregion OnGet

        #region OnPost
        [Test]
        public void OnPost_Valid_Should_Return_Parks()
        {
            // Arrange
            pageModel.Park = new ParksModel
            {
                Id = "LAKE SAMMAMISH STATE PARK 2",
                Image = "a",
                Url = "a",
                Description = "a",
                Ratings = null,
                Address = "a",
                Phone = "1234567890",
                Park_system = "City",
                Activities = "Hiking, Camping",
                Map_brochure = "a",
                Permits = "a",
                Comments = null
            };

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        [Test]
        public void OnPost_InValid_Model_NotValid_Return_Page()
        {
            // Arrange

            // Force an invalid error state
            pageModel.ModelState.AddModelError("bogus", "bogus error");

            // Act
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }
        #endregion OnPost
    }
}