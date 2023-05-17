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
    /// Unit tests for ExploreModel
    /// </summary>
    public class ExploreModelTests
    {
        [Test]
        /// <summary>
        /// Create mock variables, and invoke JsonFileParksService.
        /// Uses mock logger and webhost to create model.
        /// Tests if the model was correclty rendered.
        /// </summary>
        public void Model_Is_Rendered_Model_Should_Not_Be_Null()
        {
            // Arrange
            //Create mock variables, and invoke JsonFileParksService.
            var loggerMock = new Mock<ILogger<ExploreModel>>();
            var envMock = new Mock<IWebHostEnvironment>();
            var parkService = new JsonFileParksService(envMock.Object);

            // Act
            //Uses mock logger and webhost to create model.
            var model = new ExploreModel(loggerMock.Object, parkService);

            // Assert
            //Ensure the model was correclty rendered.
            Assert.NotNull(model);
        }

        [Test]
        /// <summary>
        /// Create mock variables
        /// Create new model with mock variables
        /// Ensure Parkservice variable is created
        /// </summary>
        public void ParkService_Is_Loaded_Model_Should_Not_Be_Null()
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
        /// Create variables to mock logger and environment
        /// Create root path for database 
        /// Incoke parkservice using mock environment with access to root path
        /// Call model and onGet function
        /// Ensure enumerable has parks filled
        /// </summary>
        public void Parks_Are_Loaded_ParkList_Should_Not_Be_Null()
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
