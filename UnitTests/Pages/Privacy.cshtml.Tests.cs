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
            //Create mock logger
            var loggerMock = new Mock<ILogger<PrivacyModel>>();

            // Act
            //Invoke privacy model
            var model = new PrivacyModel(loggerMock.Object);

            // Assert
            //Ensure model is created
            Assert.NotNull(model);
           
        }


        [Test]
        public void Logger_IsCalled_OnGet()
        {
            // Arrange
            //Create mock logger to create privacy page model
            var loggerMock = new Mock<ILogger<PrivacyModel>>();
            var model = new PrivacyModel(loggerMock.Object);

            // Act
            //Invoke OnGet
            model.OnGet();

            // Assert
            //verify correct usage of logger
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
