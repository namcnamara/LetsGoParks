using Microsoft.Extensions.Logging;

using NUnit.Framework;

using Moq;

using LetsGoPark.WebSite.Pages;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace UnitTests.Pages.AboutUs
{
    /// <summary>
    /// Unit tests for AboutUsModel
    /// </summary>
    public class AboutUsModelTests
    {
        /// <summary>
        /// Creates a loggerMock
        /// Invoke model
        /// Tests if the model is null
        /// </summary>
        [Test]
        public void Model_Is_Rendered_Model_Should_Not_Be_Null()
        {
            // Arrange
            //Create mock logger
            var loggerMock = new Mock<ILogger<AboutUsModel>>();

            // Act
            //Invoke model
            var model = new AboutUsModel(loggerMock.Object);

            // Assert
            //Verify logger operation correctly
            Assert.NotNull(model);
           
        }


    }
}
