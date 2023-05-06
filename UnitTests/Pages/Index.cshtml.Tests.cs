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
    public class IndexModelTests
    {
        [Test]
        public void ModelIsRendered()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<IndexModel>>();
            var envMock = new Mock<IWebHostEnvironment>();
            var parkService = new JsonFileParksService(envMock.Object);

            // Act
            var model = new IndexModel(loggerMock.Object, parkService);

            // Assert
            Assert.NotNull(model);
        }

        [Test]
        public void ParkService_Is_Loaded()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<IndexModel>>();
            var envMock = new Mock<IWebHostEnvironment>();
            var parkService = new JsonFileParksService(envMock.Object);

            // Act
            var model = new IndexModel(loggerMock.Object, parkService);

            // Assert
            Assert.NotNull(model.ParkService);
        }

        [Test]
        public void Parks_Are_Loaded()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<IndexModel>>();
            string wwwRootPath = Path.Combine(AppContext.BaseDirectory, "wwwroot");
            var envMock = new Mock<IWebHostEnvironment>();
            envMock.Setup(x => x.WebRootPath).Returns(wwwRootPath);
            var parkService = new JsonFileParksService(envMock.Object);

            // Act
            var model = new IndexModel(loggerMock.Object, parkService);
            model.OnGet();

            // Assert
            Assert.NotNull(model.Parks);
        }
    }
}
