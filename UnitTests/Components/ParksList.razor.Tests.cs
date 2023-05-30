using Bunit;
using NUnit.Framework;

using LetsGoPark.WebSite.Components;
using LetsGoPark.WebSite.Models;
using Microsoft.Extensions.DependencyInjection;
using LetsGoPark.WebSite.Services;
using System.Linq;
using Moq;
using NUnit.Framework.Internal;
using System.Threading.Channels;

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

            var page = RenderComponent<ParksList>();

            // Find the filter dropdown
            var dropdown = page.Find("#filter-dropdown");

            // Act
            // Find the one that matches the filterDataString looking for and click it
            dropdown.Change("filterDataString");

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            //ensure the "National Parks" is within the markup
            Assert.AreEqual(true, pageMarkup.Contains("National"));
        }

        #endregion FilterPark

        #region UpdateSelectedFilterOption
        [Test]
        ///
        /// This test selects All from the dropdown menu in ParksList razor and ensures the correct
        /// fields are populated. 
        ///
        public void UpdateSelectedFilterOption_Select_All_Should_Set_Flag_To_False()
        {
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);

            var page = RenderComponent<ParksList>();

            // Find the filter dropdown
            var dropdown = page.Find("#filter-dropdown");

            // Act
            // Find the one that matches the filterDataString looking for and click it
            dropdown.Change("All");

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            //ensure the set flag is false within the markup
            Assert.AreEqual(false, page.Instance.dropDownFilterActivated);
        }

        [Test]
        ///
        /// This test selects City from the dropdown menu in ParksList razor and ensures the correct
        /// fields are populated. 
        ///
        public void UpdateSelectedFilterOption_Select_City_Should_Select_City_Parks()
        {
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);

            var page = RenderComponent<ParksList>();

            // Find the filter dropdown
            var dropdown = page.Find("#filter-dropdown");

            // Act
            // Select a value in the dropdown and click it
            dropdown.Change("City");

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            //ensure the dropDownFilter" flag is true in the instance markup
            Assert.AreEqual(true, page.Instance.dropDownFilterActivated);
            Assert.AreEqual(ParkSystemEnum.City, page.Instance.SelectedFilterOption);
        }

        [Test]
        ///
        /// This test selects State from the dropdown menu in ParksList razor and ensures the correct
        /// fields are populated. 
        ///
        public void UpdateSelectedFilterOption_Select_State_Should_Select_State_Parks()
        {
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);

            var page = RenderComponent<ParksList>();

            // Find the filter dropdown
            var dropdown = page.Find("#filter-dropdown");

            // Act
            // Select a value in the dropdown and click it
            dropdown.Change("State");

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            //ensure the dropDownFilter" flag is true in the instance markup
            Assert.AreEqual(true, page.Instance.dropDownFilterActivated);
            Assert.AreEqual(ParkSystemEnum.State, page.Instance.SelectedFilterOption);
        }

        [Test]
        ///
        /// This test selects National from the dropdown menu in ParksList razor and ensures the correct
        /// fields are populated. 
        ///
        public void UpdateSelectedFilterOption_Select_National_Should_Select_National_Parks()
        {
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);

            var page = RenderComponent<ParksList>();

            // Find the filter dropdown
            var dropdown = page.Find("#filter-dropdown");

            // Act
            // Select a value in the dropdown and click it
            dropdown.Change("National");

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            //ensure the dropDownFilter" flag is true in the instance markup
            Assert.AreEqual(true, page.Instance.dropDownFilterActivated);
            Assert.AreEqual(ParkSystemEnum.National, page.Instance.SelectedFilterOption);
        }

        #endregion UpdateSelectedFilterOption

        #region GetImageURL
        [Test]
        /// <summary>
        /// This test creates a highly rated park, and ensures it has no image so the default is used.
        /// </summary>
        public void RenderPage_Highly_Rated_Blank_Image_Should_Use_Default()
        {
            // Arrange
            //Create singleton of parkService
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            //Alter park's image to a bad value
            var park = TestHelper.ParkService.GetParks().FirstOrDefault(x => x.Id == "Mount Rainier National Park");
            string imgURL = park.Image;
            park.Image = "badURL";
            //Id for button to click after page renders
            var id = "MoreInfoButton_Mount Rainier National Park";
            //Update poor url
            TestHelper.ParkService.UpdateData(park);

            //Act
            //Renders the homepage with 3 highest rated parks
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
            Assert.AreEqual(true, pageMarkup.Contains("defaultPark.jpg"));
            //Reset image value
            park.Image = imgURL;
            TestHelper.ParkService.UpdateData(park);

        }
        #endregion GetImageURL

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
        public void SubmitRating_Valid_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
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
            var id = "Olympic National Park";

            var page = RenderComponent<ParksList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Get the spans
            var spans = page.FindAll("span");

            // Get the comment Count, the List should have 10 elements, element 4 is the string for the count
            var commentCountSpan = spans[3];
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
            var id = "BRIDLE TRAILS STATE PARK";

            var page = RenderComponent<ParksList>();

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

            // Get the comment Count, the List should have 10 elements, element 4 is the string for the count
            var commentCountSpan = spans[3];
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
            var id = "FLAMING GEYSER STATE PARK";

            var page = RenderComponent<ParksList>();

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
            var commentCountSpan = spans[3];
            var commentCountSpanString = commentCountSpan.OuterHtml;
            // Assert

            // Confirm that the comment count is not zero
            Assert.AreEqual(true, commentCountSpanString.Contains("Be the first to comment!"));
        }
        #endregion SubmitComment

        #region Search

        [Test]
        /// <summary>
        ///This test tests that the search will return content
        ///Then open the popup on the card
        ///Then search with the valid id
        ///Ensure the search value is within the page markup
        /// </summary>
        public void Search_Parks_Valid_ID_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            var data = "Park";

            var page = RenderComponent<ParksList>();

            // Find the Buttons (search) and performs searching with the data
            var button = page.FindAll("Button").First(m => m.ClassName.Contains("btn btn-success"));
            page.FindAll("Input").First().Change(data);
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            //ensure the "Parks" is within the markup
            Assert.AreEqual(true, pageMarkup.Contains(data));
        }

        [Test]
        /// <summary>
        /// This test tests that the clear button will return all content
        /// Then open the popup on the card
        /// Then search with an invalid id
        /// Then click on the clear button
        /// Ensure all the park value is within the page markup
        /// </summary>
        public void Clear_Search_Parks_Should_Return_All_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            var data = "adsfasd";

            var page = RenderComponent<ParksList>();

            // Find the Buttons (search) and performs searching with the data
            var search_button = page.FindAll("Button").First(m => m.ClassName.Contains("btn btn-success"));
            page.FindAll("Input").First().Change(data);
            search_button.Click();

            // Find the Buttons (clear) and click
            var clear_button = page.FindAll("Button").First(m => m.ClassName.Contains("btn btn-danger"));
            clear_button.Click();

            // Get the markup to use for the assert
            var afterPageMarkup = page.Markup;


            // Assert
            //ensure the "Parks" is within the after markup
            Assert.AreEqual(true, afterPageMarkup.Contains("Park"));
        }
        #endregion Search

        #region SortPark
        [Test]
        /// <summary>
        /// Creates a singleton of the file service
        /// Render the ParksList page
        /// Click on the sort ascending order button
        /// Get the Cards retrned
        /// Ensure a valid Id is present
        /// </summary>
        public void ParkList_Sort_Ascending_Order_Should_Return_Content()
        {
            // Arrange
            //Creates a singleton of the file service
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);

            // Act
            //Render the ParksList page
            var page = RenderComponent<ParksList>();

            // Find the Button (Sort A-Z)
            var button = page.FindAll("Button").First(m => m.ClassName.Contains("btn btn-sort"));
            button.Click();

            // Get the Cards retrned
            var result = page.Markup;

            // Assert
            //Ensure a valid Id is present
            Assert.AreEqual(true, result.Contains("LAKE SAMMAMISH STATE PARK"));
        }

        [Test]
        /// <summary>
        /// Creates a singleton of the file service
        /// Render the ParksList page
        /// Click on the sort descending order button
        /// Get the Cards retrned
        /// Ensure a valid Id is present
        /// </summary>
        public void ParkList_Sort_Descending_Order_Should_Return_Content()
        {
            // Arrange
            //Creates a singleton of the file service
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);

            // Act
            //Render the ParksList page
            var page = RenderComponent<ParksList>();

            // Find the Button (Sort Z-A)
            var button = page.FindAll("Button").Last(m => m.ClassName.Contains("btn btn-sort"));
            button.Click();

            // Get the Cards retrned
            var result = page.Markup;

            // Assert
            //Ensure a valid Id is present
            Assert.AreEqual(true, result.Contains("LAKE SAMMAMISH STATE PARK"));
        }
        #endregion SortPark
    }
}
