using Bunit;
using NUnit.Framework;

using LetsGoPark.WebSite.Components;
using Microsoft.Extensions.DependencyInjection;
using LetsGoPark.WebSite.Services;
using System.Linq;

namespace UnitTests.Components
{
    public class HomepageTests : BunitTestContext
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        [Test]
        public void ParkList_Default_Should_Return_Content()
        {
            // Arrange
            //Creates a singleton of the file service
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);

            // Act
            //Render the Homepage page
            var page = RenderComponent<Homepage>();

            // Get the Cards retrned
            var result = page.Markup;

            // Assert
            //Ensure a valid Id is present
            Assert.AreEqual(true, result.Contains("LAKE SAMMAMISH STATE PARK"));
        }

        #region SelectPark
        [Test]
        public void SelectPark_Valid_ID_Lake_Sammamish_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            var id = "MoreInfoButton_LAKE SAMMAMISH STATE PARK";

            var page = RenderComponent<Homepage>();

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
