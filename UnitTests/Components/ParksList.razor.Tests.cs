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

        #region FilterPark
        [Test]
        /// <summary>
        /// Find the filter dropdown
        /// Find the one that matches the filterDataString looking for and click it
        /// Get the markup to use for the assert
        /// Ensure the "National Parks" is within the markup
        /// </summary>
        public void Filter_Park_By_Park_System_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            var filterDataString = "National Parks";

            var page = RenderComponent<ParksList>();

            // Find the filter dropdown
            var optionList = page.FindAll("Option");

            // Act
            // Find the one that matches the filterDataString looking for and click it
            var option = optionList.First(m => m.OuterHtml.Contains(filterDataString));

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            //ensure the "National Parks" is within the markup
            Assert.AreEqual(true, pageMarkup.Contains("Mount Rainier National Park"));
        }

        #endregion FilterPark

        #region SubmitRating

        [Test]
        public void SubmitRating_Valid_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
            /*
             This test tests that the SubmitRating will change the vote as well as the Star checked
             Because the star check is a calculation of the ratings, using a record that has no stars and checking one makes it clear what was changed

            The test needs to open the page
            Then open the popup on the card
            Then record the state of the count and star check status
            Then check a star
            Then check again the state of the cound and star check status

            */

            // Arrange
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            var id = "Olympic National Park";

            var page = RenderComponent<ParksList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Get the markup of the page post the Click action
            var buttonMarkup = page.Markup;

            // Get the Star Buttons
            var starButtonList = page.FindAll("span");

            // Get the Vote Count
            // Get the Vote Count, the List should have 10 elements, element 5 is the string for the count
            var preVoteCountSpan = starButtonList[4];
            var preVoteCountString = preVoteCountSpan.OuterHtml;

            // Get the First star item from the list, it should not be checked
            var starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));

            // Save the html for it to compare after the click
            var preStarChange = starButton.OuterHtml;

            // Act

            // Click the star button
            starButton.Click();

            // Get the markup to use for the assert
            buttonMarkup = page.Markup;

            // Get the Star Buttons
            starButtonList = page.FindAll("span");

            // Get the Vote Count, the List should have 10 elements, element 5 is the string for the count
            var postVoteCountSpan = starButtonList[4];
            var postVoteCountString = postVoteCountSpan.OuterHtml;

            // Get the Last stared item from the list
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            // Save the html for it to compare after the click
            var postStarChange = starButton.OuterHtml;

            // Assert

            // Confirm that the record had no votes to start, and 1 vote after
            Assert.AreEqual(true, preVoteCountString.Contains("Be the first to vote!"));
            Assert.AreEqual(true, postVoteCountString.Contains("1"));
            Assert.AreEqual(false, preVoteCountString.Equals(postVoteCountString));
        }

        [Test]
        public void SubmitRating_Valid_ID_Click_Stared_Should_Increment_Count_And_Leave_Star_Check_Remaining()
        {
            /*
             This test tests that the SubmitRating will change the vote as well as the Star checked
             Because the star check is a calculation of the ratings, using a record that has no stars and checking one makes it clear what was changed

            The test needs to open the page
            Then open the popup on the card
            Then record the state of the count and star check status
            Then check a star
            Then check again the state of the cound and star check status

            */

            // Arrange
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            var id = "North Cascades National Park";

            var page = RenderComponent<ParksList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Get the markup of the page post the Click action
            var buttonMarkup = page.Markup;

            // Get the Star Buttons
            var starButtonList = page.FindAll("span");

            // Get the Vote Count
            // Get the Vote Count, the List should have 10 elements, element 5 is the string for the count
            var preVoteCountSpan = starButtonList[4];
            var preVoteCountString = preVoteCountSpan.OuterHtml;

            // Get the Last star item from the list, it should one that is checked
            var starButton = starButtonList.Last(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            // Save the html for it to compare after the click
            var preStarChange = starButton.OuterHtml;

            // Act

            // Click the star button
            starButton.Click();

            // Get the markup to use for the assert
            buttonMarkup = page.Markup;

            // Get the Star Buttons
            starButtonList = page.FindAll("span");

            // Get the Vote Count, the List should have 10 elements, element 5 is the string for the count
            var postVoteCountSpan = starButtonList[4];
            var postVoteCountString = postVoteCountSpan.OuterHtml;

            // Get the Last stared item from the list
            starButton = starButtonList.Last(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            // Save the html for it to compare after the click
            var postStarChange = starButton.OuterHtml;

            // Assert

            // Confirm that the record had no votes to start, and 1 vote after
            Assert.AreEqual(true, preVoteCountString.Contains("1"));
            Assert.AreEqual(true, postVoteCountString.Contains("2"));
            Assert.AreEqual(false, preVoteCountString.Equals(postVoteCountString));
        }
        #endregion SubmitRating
    }
}
