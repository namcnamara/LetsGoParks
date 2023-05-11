using Microsoft.Extensions.Logging;

using NUnit.Framework;

using Moq;

using LetsGoPark.WebSite.Pages;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace UnitTests.Pages.Update
{
    public class UpdateCommentModelTests
    {
        [Test]
        public void ModelIsRendered()
        {
            // Arrange
            //Create mock logger
            var loggerMock = new Mock<ILogger<UpdateCommentModel>>();

            // Act
            //Load page model using mock logger
            var model = new UpdateCommentModel(loggerMock.Object);

            // Assert
            //Ensure page model created
            Assert.NotNull(model);
           
        }

        [Test]
        public void Logger_IsCalled_OnGet()
        {
            // Arrange
            //use mock logger to create page model
            var loggerMock = new Mock<ILogger<UpdateCommentModel>>();
            var model = new UpdateCommentModel(loggerMock.Object);

            // Act
            //Invoke OnGet function of page model
            model.OnGet();

            // Assert
            //Ensure logger is working correctly
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
