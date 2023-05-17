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

namespace UnitTests.Pages.DeletePark
{
    /// <summary>
    /// Unit tests for DeletePark
    /// </summary>
    public class DeleteParkTests
    {
        #region TestSetup
        /// <summary>
        /// Test Setup
        /// </summary>
        public static DeleteParkModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            pageModel = new DeleteParkModel(TestHelper.ParkService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet
        [Test]
        /// <summary>
        /// Use an existed Id
        /// Retrieve specified park with existing id
        /// Tests if page created, and the Id should match initial parameter passed in as Id
        /// </summary>
        public void OnGet_Park_Id_Correctly_Retrieved_Park_Should_Be_Populated()
        {
            //Arrange
            //Id does exist
            string Id = "LAKE SAMMAMISH STATE PARK";

            //Act
            //retrieve specified park with existing id
            pageModel.OnGet(Id);

            //Assert
            //Should create page, and the Id should match initial parameter passed in as Id
            Assert.AreEqual(pageModel.Park.Id, "LAKE SAMMAMISH STATE PARK");
        }

        [Test]
        /// <summary>
        /// Use an not existed Id
        /// Retrieve specified park with the id
        /// Tests if pageModel.Park returns null
        /// </summary>
        public void OnGet_Park_Id_Not_Retrieved_Should_Return_False()
        {
            //Arrange
            //Id doesn't exist
            string badId = "Not a real park";

            //Act
            //retrieve specified park
            pageModel.OnGet(badId);

            //Assert
            //Should return null
            Assert.IsNull(pageModel.Park);
        }
        #endregion OnGet

        #region OnPost
        [Test]
        /// <summary>
        /// Create new park to database
        /// Gets original number of parks in database
        /// Adds park to database without URL field, will be invalid field
        /// Try to delete park
        /// Tests if park counts before and after are equal
        /// </summary>
        public void OnPost_Delete_Should_Reduce_Park_Count()
        {
            // Arrange
            //Create new park to database
            var park = new ParksModel()
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
            TestHelper.ParkService.CreateData(park);
            //Gets original number of parks in database
            var oldCount = TestHelper.ParkService.GetParks().Count();

            // Act
            //Adds park to database without URL field, will be invalid field
            //Try to delete park
            pageModel.Park = park;
            var result = pageModel.OnPost();

            // Assert
            //Values should be equal
            Assert.AreEqual(oldCount-1, TestHelper.ParkService.GetParks().Count());
        }

        

        #endregion OnPost
    }
}