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
    /// Unit tests for IndexModel
    /// </summary>
    public class IndexModelTests
    {
        [Test]
        /// <summary>
        /// Create mock variables to instantiate park service
        /// Create index model object
        /// Tests if model was created.
        /// </summary>
        public void ModelIsRendered()
        {
            // Arrange
            // Create mock variables to instantiate park service
            var loggerMock = new Mock<ILogger<IndexModel>>();
            var envMock = new Mock<IWebHostEnvironment>();
            var parkService = new JsonFileParksService(envMock.Object);

            // Act
            // Create index model object
            var model = new IndexModel(loggerMock.Object, parkService);

            // Assert
            //ensure model was created.
            Assert.NotNull(model);
        }

        [Test]
        /// <summary>
        /// Create mock variables
        /// Create new model with mock variables
        /// Tests if Parkservice variable is created
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
        /// Create variables to mock logger and set up environment
        /// Incoke parkservice using mock environment with access to root path
        /// Call model and onGet function
        /// Tests if enumerable has parks filled
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
