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

        
    }
}
