using Bunit;
using NUnit.Framework;

using LetsGoPark.WebSite.Components;
using Microsoft.Extensions.DependencyInjection;
using LetsGoPark.WebSite.Services;
using System.Linq;

namespace UnitTests.Components
{
    /// <summary>
    /// Unit tests for Homepage
    /// </summary>
    public class HomepageTests : BunitTestContext
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
        /// Render the Homepage page
        /// Get the Cards retrned
        /// Ensure a valid Id is present
        /// </summary>
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

        #region SubmitRating

        [Test]
        /// <summary>
        /// This test tests that the SubmitRating will change the vote as well as the Star checked
        /// Because the star check is a calculation of the ratings, using a record that has no stars and checking one makes it clear what was changed
        /// The test needs to open the page
        /// Then open the popup on the card
        /// Then record the state of the count and star check status
        /// Then check a star
        /// Then check again the state of the cound and star check status
        /// </summary>
        public void SubmitRating_Valid_ID_Click_Stared_Should_Increment_Count_And_Leave_Star_Check_Remaining()
        {
            // Arrange
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            var id = "Doris Cooper Houghton Beach Park";

            var page = RenderComponent<Homepage>();

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
            // Get the Vote Count, the List should have 9 elements, element 5 is the string for the count
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

            // Get the Vote Count, the List should have 9 elements, element 5 is the string for the count
            var postVoteCountSpan = starButtonList[4];
            var postVoteCountString = postVoteCountSpan.OuterHtml;

            // Get the Last stared item from the list
            starButton = starButtonList.Last(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            // Save the html for it to compare after the click
            var postStarChange = starButton.OuterHtml;

            // Assert

            // Confirm that the record had no votes to start, and 1 vote after
            Assert.AreEqual(true, preVoteCountString.Contains("2"));
            Assert.AreEqual(true, postVoteCountString.Contains("3"));
            Assert.AreEqual(false, preVoteCountString.Equals(postVoteCountString));
        }
        #endregion SubmitRating

        #region SubmitComment

        [Test]
        /// <summary>
        ///This test tests that the GetCurrentCommentCount will return zero if no comments exsited
        ///The test needs to open the page
        ///Then open the popup on the card
        ///Then check the count of the comment
        /// </summary>
        public void GetCurrentCommentCount_Valid_ID_No_Comments_Should_Return_Zero()
        {
            // Arrange
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            var id = "Mount Rainier National Park";

            var page = RenderComponent<Homepage>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Get the spans
            var spans = page.FindAll("span");

            // Get the comment Count
            var commentCountSpan = spans[4];
            var commentCountSpanString = commentCountSpan.OuterHtml;

            // Assert

            // Confirm that the comment count is zero
            Assert.AreEqual(true, commentCountSpanString.Contains("Be the first to comment!"));
        }

        [Test]
        /// <summary>
        /// This test tests that the SubmitComment function with valid comment
        /// The test needs to open the page
        /// Then open the popup on the card
        /// Submit a comment
        /// Then check the count of the comment
        /// </summary>
        public void SubmitComment_Valid_Comment_CommentCount_Should_Return_Not_Zero()
        {
            // Arrange
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            var id = "Mount Rainier National Park";

            var page = RenderComponent<Homepage>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Get the spans
            var spans = page.FindAll("span");

            // submit a comment
            page.Find("#name-input").Input("TestName");
            page.Find("#comment-input").Input("This is a Test");
            page.Find("#token-input").Input("100");

            var submitButton = page.Find("button[type='submit']");
            submitButton.Click();

            // Get the comment Count
            var commentCountSpan = spans[4];
            var commentCountSpanString = commentCountSpan.OuterHtml;
            // Assert

            // Confirm that the comment count is not zero
            Assert.AreEqual(true, commentCountSpanString.Contains("comment"));
        }

        [Test]
        /// <summary>
        /// This test tests that the SubmitComment will fail if an invalid comment was provided
        /// The test needs to open the page
        /// Then open the popup on the card
        /// Submit an invalid comment
        /// Then check the count of the comment
        /// </summary>
        public void SubmitComment_InValid_Comment_CommentCount_Should_Return_Zero()
        {
            // Arrange
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            var id = "Mount Rainier National Park";

            var page = RenderComponent<Homepage>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Get the spans
            var spans = page.FindAll("span");

            // submit an invalid comment with no name field
            page.Find("#comment-input").Input("This is a Test");
            page.Find("#token-input").Input("100");

            var submitButton = page.Find("button[type='submit']");
            submitButton.Click();

            // Get the comment Count, the List should have 10 elements, element 4 is the string for the count
            var commentCountSpan = spans[4];
            var commentCountSpanString = commentCountSpan.OuterHtml;
            // Assert

            // Confirm that the comment count is zero
            Assert.AreEqual(true, commentCountSpanString.Contains("Be the first to comment!"));
        }
        #endregion SubmitComment
    }
}
