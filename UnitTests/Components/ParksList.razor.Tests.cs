using Bunit;
using NUnit.Framework;

using LetsGoPark.WebSite.Components;
using Microsoft.Extensions.DependencyInjection;
using LetsGoPark.WebSite.Services;
using System.Linq;

namespace UnitTests.Components
{
    /// <summary>
    /// Unit tests for ParksList
    /// </summary>
    public class ParksListTests : BunitTestContext
    {
        #region TestSetup
        /// <summary>
        /// Test Setup
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        [Test]
        /// <summary>
        /// Creates a singleton of the file service
        /// Render the ParksList page
        /// Get the Cards retrned
        /// Ensure a valid Id is present
        /// </summary>
        public void ParkList_Default_Should_Return_Content()
        {
            // Arrange
            //Creates a singleton of the file service
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);

            // Act
            //Render the ParksList page
            var page = RenderComponent<ParksList>();

            // Get the Cards retrned
            var result = page.Markup;

            // Assert
            //Ensure a valid Id is present
            Assert.AreEqual(true, result.Contains("LAKE SAMMAMISH STATE PARK"));
        }

        #region SelectPark
        [Test]
        /// <summary>
        /// Find the Buttons (more info)
        /// Find the one that matches the ID looking for and click it
        /// Get the markup to use for the assert
        /// Ensure the acerage of "LAKE SAMMAMISH STATE PARK" is within the markup
        /// </summary>
        public void SelectPark_Valid_ID_Lake_Sammamish_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            var id = "MoreInfoButton_LAKE SAMMAMISH STATE PARK";

            var page = RenderComponent<ParksList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            //ensure the acerage of "LAKE SAMMAMISH STATE PARK" is within the markup
            Assert.AreEqual(true, pageMarkup.Contains("531"));
        }
        #endregion SelectPark
    }
}
