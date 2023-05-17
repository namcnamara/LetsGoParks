using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using LetsGoPark.WebSite.Pages;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace UnitTests.Pages.Privacy
{
    /// <summary>
    /// Unit tests for PrivacyModel
    /// </summary>
    public class PrivacyModelTests
    {
        [Test]
        /// <summary>
        /// Creates a loggerMock
        /// Invoke model
        /// Tests if the model is null
        /// </summary>
        public void ParkService_Is_Loaded_Model_Should_Not_Be_Null()
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
        /// <summary>
        /// Creates a loggerMock
        /// Invoke OnGet
        /// Verify correct usage of logger
        /// </summary>
        public void Parks_Are_Loaded_Parks_List_Should_Not_Be_Null()
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
