using Microsoft.Extensions.Logging;

using NUnit.Framework;

using Moq;

using LetsGoPark.WebSite.Pages;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace UnitTests.Pages.Privacy
{
    public class PrivacyModelTests
    {
        [Test]
        public void ModelIsRendered()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<PrivacyModel>>();

            // Act
            var model = new PrivacyModel(loggerMock.Object);

            // Assert
            Assert.NotNull(model);
           
        }


        [Test]
        public void Logger_IsCalled_OnGet()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<PrivacyModel>>();
            var model = new PrivacyModel(loggerMock.Object);

            // Act
            model.OnGet();

            // Assert
            loggerMock.Verify(
                x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                ),
                Times.Once
            );
        }
    }
}
