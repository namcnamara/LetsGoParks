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

namespace UnitTests.Pages.ReadPark
{
    /// <summary>
    /// Unit tests for ReadPark
    /// </summary>
    public class ReadParkTests
    {
        /// <summary>
        /// Test Setup
        /// </summary>
        #region TestSetup
        public static ReadParkModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            pageModel = new ReadParkModel(TestHelper.ParkService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet
        [Test]
        /// <summary>
        /// Invoke OnGet and grab data from a park id
        /// Tests if the model is created from OnGet and correct page has been loaded
        /// </summary>
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange

            // Act
            //Grab data from Park.Id = Lake Sammamish State park
            pageModel.OnGet("LAKE SAMMAMISH STATE PARK");

            // Assert
            //Ensure model is created from OnGet
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            //Ensure correct page has been loaded
            Assert.AreEqual("LAKE SAMMAMISH STATE PARK", pageModel.Park.Id);
        }

        [Test]
        /// <summary>
        /// Invoke OnGet and grab data from an invalid park id
        /// Should return to the Explore page
        /// </summary>
        public void OnGet_Invalid_Should_Redirect_To_Explore()
        {
            // Arrange
            var data = "asdf";

            // Act
            //Grab data from the invalid id
            pageModel.OnGet(data);

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(null, pageModel.Park);
        }
        #endregion OnGet
    }
}
    