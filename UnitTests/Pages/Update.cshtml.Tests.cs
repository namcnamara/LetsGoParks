using Microsoft.Extensions.Logging;

using NUnit.Framework;

using Moq;

using LetsGoPark.WebSite.Pages;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace UnitTests.Pages.Update
{
    public class UpdateModelTests
    {
        [Test]
        public void ModelIsRendered()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UpdateModel>>();

            // Act
            var model = new UpdateModel(loggerMock.Object);

            // Assert
            Assert.NotNull(model);
           
        }


        
    }
}
