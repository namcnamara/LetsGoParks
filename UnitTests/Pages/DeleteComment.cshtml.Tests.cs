using Microsoft.Extensions.Logging;

using NUnit.Framework;

using Moq;

using LetsGoPark.WebSite.Pages;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace UnitTests.Pages.Delete
{
    /// <summary>
    /// Unit tests for DeleteCommentModel class
    /// </summary>
    public class DeleteCommentModelTests
    {
        [Test]
        /// <summary>
        /// Creates a loggerMock
        /// Create page model using mock logger
        /// Tests if the model is null
        /// </summary>
        public void Model_Is_Rendered_Model_Should_Not_Be_Null()
        {
            // Arrange
            //create mock logger
            var loggerMock = new Mock<ILogger<DeleteCommentModel>>();

            // Act
            //Create page model using mock logger
            var model = new DeleteCommentModel(loggerMock.Object);

            // Assert
            //Ensure page created correctly
            Assert.NotNull(model);
           
        }

        [Test]
        /// <summary>
        /// Creates a loggerMock
        /// Create page model using mock logger
        /// Invoke onGet method
        /// Tests if the logger is working correctly
        /// </summary>
        public void Logger_IsCalled_OnGet_Logger_Should_Contain_Message()
        {
            // Arrange
            //Create logger
            var loggerMock = new Mock<ILogger<DeleteCommentModel>>();
            var model = new DeleteCommentModel(loggerMock.Object);

            // Act
            //Invoke onGet method
            model.OnGet();

            // Assert
            //assert logger is working correctly
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
