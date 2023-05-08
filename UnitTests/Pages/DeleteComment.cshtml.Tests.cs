using Microsoft.Extensions.Logging;

using NUnit.Framework;

using Moq;

using LetsGoPark.WebSite.Pages;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace UnitTests.Pages.Delete
{
    public class DeleteCommentModelTests
    {
        [Test]
        public void ModelIsRendered()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteCommentModel>>();

            // Act
            var model = new DeleteCommentModel(loggerMock.Object);

            // Assert
            Assert.NotNull(model);
           
        }

        [Test]
        public void Logger_IsCalled_OnGet()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteCommentModel>>();
            var model = new DeleteCommentModel(loggerMock.Object);

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
