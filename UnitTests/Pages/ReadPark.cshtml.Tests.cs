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
    public class ReadParkTests
    {
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
        #endregion OnGet
    }
}
    