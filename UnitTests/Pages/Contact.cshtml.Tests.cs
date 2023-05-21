using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using LetsGoPark.WebSite.Pages;

namespace UnitTests.Pages.Contact
{
    /// <summary>
    /// Unit tests for ContactModel
    /// </summary>
    public class ContactModelTests
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
            var loggerMock = new Mock<ILogger<ContactModel>>();

            // Act
            //Invoke model
            var model = new ContactModel(loggerMock.Object);

            // Assert
            //Verify logger operation correctly
            Assert.NotNull(model);

        }


    }
}
