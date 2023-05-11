using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using LetsGoPark.WebSite.Pages;
using System.Collections.Generic;
using LetsGoPark.WebSite.Models;
using LetsGoPark.WebSite.Services;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

namespace UnitTests.Pages
{
    /// <summary>
    /// Unit tests for ParksIndexModel
    /// </summary>
    public class ParksIndexModelTests
    {
        [Test]
        /// <summary>
        /// Creates a logger mocker, an env mocker and set up parkService
        /// Invoke model
        /// Tests if the model is null
        /// </summary>
        public void ModelIsRendered()
        {
            // Arrange
            // Creates a logger mocker, an env mocker and set up parkService
            var loggerMock = new Mock<ILogger<ParksIndexModel>>();
            var envMock = new Mock<IWebHostEnvironment>();
            var parkService = new JsonFileParksService(envMock.Object);

            // Act
            // Invoke model
            var model = new ParksIndexModel(loggerMock.Object, parkService);

            // Assert
            // Tests if the model is null
            Assert.NotNull(model);
        }

        [Test]
        /// <summary>
        /// Creates a logger mocker, an env mocker and set up parkService
        /// Invoke model
        /// Tests if the model is null
        /// </summary>
        public void ParkService_Is_Loaded()
        {
            // Arrange
            //Create mock variables
            var loggerMock = new Mock<ILogger<ExploreModel>>();
            var envMock = new Mock<IWebHostEnvironment>();
            var parkService = new JsonFileParksService(envMock.Object);

            // Act
            //Create new model with mock variables
            var model = new ExploreModel(loggerMock.Object, parkService);

            // Assert
            //Ensure Parkservice variable is created
            Assert.NotNull(model.ParkService);
        }

        [Test]
        /// <summary>
        /// Creates a logger mocker, an env mocker and set up parkService
        /// Call OnGet
        /// Tests if the model is null
        /// </summary>
        public void Parks_Are_Loaded()
        {
            // Arrange
            //Create variables to mock logger and environment
            var loggerMock = new Mock<ILogger<ExploreModel>>();
            //Create root path for database 
            string wwwRootPath = Path.Combine(AppContext.BaseDirectory, "wwwroot");
            var envMock = new Mock<IWebHostEnvironment>();
            //allow mock environment access to database path
            envMock.Setup(x => x.WebRootPath).Returns(wwwRootPath);
            //Incoke parkservice using mock environment with access to root path
            var parkService = new JsonFileParksService(envMock.Object);

            // Act
            //Call model and onGet function
            var model = new ExploreModel(loggerMock.Object, parkService);
            model.OnGet();

            // Assert
            //Ensure enumerable has parks filled
            Assert.NotNull(model.Parks);
        }
    }
}
