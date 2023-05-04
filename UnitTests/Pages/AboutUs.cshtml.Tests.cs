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
            var loggerMock = new Mock<ILogger<AboutUsModel>>();

            // Act
            var model = new AboutUsModel(loggerMock.Object);

            // Assert
            Assert.NotNull(model);
           
        }


    }
}
