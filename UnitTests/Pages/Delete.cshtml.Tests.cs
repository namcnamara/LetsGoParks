using Microsoft.Extensions.Logging;

using NUnit.Framework;

using Moq;

using LetsGoPark.WebSite.Pages;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace UnitTests.Pages.Delete
{
    public class DeleteModelTests
    {
        [Test]
        public void ModelIsRendered()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteModel>>();

            // Act
            var model = new DeleteModel(loggerMock.Object);

            // Assert
            Assert.NotNull(model);
           
        }

        
    }
}
