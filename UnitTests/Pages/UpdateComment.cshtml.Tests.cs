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
            var loggerMock = new Mock<ILogger<UpdateCommentModel>>();

            // Act
            var model = new UpdateCommentModel(loggerMock.Object);

            // Assert
            Assert.NotNull(model);
           
        }


        
    }
}
