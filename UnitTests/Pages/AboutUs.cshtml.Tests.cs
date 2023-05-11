using Microsoft.Extensions.Logging;

using NUnit.Framework;

using Moq;

using LetsGoPark.WebSite.Pages;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace UnitTests.Pages.AboutUs
{
    public class AboutUsModelTests
    {
        [Test]
        public void ModelIsRendered()
        {
            // Arrange
            //Create mock logger
            var loggerMock = new Mock<ILogger<AboutUsModel>>();

            // Act
            //Invoke model
            var model = new AboutUsModel(loggerMock.Object);

            // Assert
            //Verify logger operation correctly
            Assert.NotNull(model);
           
        }


    }
}
