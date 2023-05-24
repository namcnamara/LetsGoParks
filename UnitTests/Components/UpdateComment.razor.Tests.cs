using Bunit;
using NUnit.Framework;

using LetsGoPark.WebSite.Components;
using Microsoft.Extensions.DependencyInjection;
using LetsGoPark.WebSite.Services;
using System.Linq;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using AngleSharp.Css.Values;

namespace UnitTests.Components
{
    /// <summary>
    /// Unit tests for Homepage
    /// </summary>
    public class UpdateCommentTests : BunitTestContext
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

        #region ModelRendered
        /// <summary>
        /// This test uses valid parameters and should return true
        /// It Creates a parkService from a mocked webhost,
        /// then passes in values to the renderComponent function
        /// </summary>
        [Test]
        public void UpdateComment_Page_Passed_Parameters_Should_Return_Page()
        {
            // Arrange
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            //Populate bunit component parameter dictionary: 
            var commentIndex = ComponentParameter.CreateParameter(nameof(DeleteComment.CommentIndex), 0);
            var parkId = ComponentParameter.CreateParameter(nameof(DeleteComment.SelectedParkId), "LAKE SAMMAMISH STATE PARK");
            var name = ComponentParameter.CreateParameter(nameof(DeleteComment.Name), "Nick");
            var datetime = ComponentParameter.CreateParameter(nameof(DeleteComment.Datetime), "2022-10-01");
            var desc = ComponentParameter.CreateParameter(nameof(DeleteComment.Description), "Test description");
            var token = ComponentParameter.CreateParameter(nameof(DeleteComment.Token), "0");
            //Create parameter variable to hold component parameters
            var parameters = new[]
            {
                commentIndex, parkId, name, datetime, desc, token
            };

            // Act
            //Render webpage passing in parameters list
            var page = RenderComponent<UpdateComment>(parameters);

            // Assert
            //Check to see if page is loaded in a valid state
            Assert.NotNull(page);
        }
        #endregion ModelRendered
    
    #region SubmitUpdatedComment
    /// <summary>
    /// This function checks to see if a comment was updated on button click
    /// It creates four string values for comment creation
    /// It uses those strings to create BUnit parameters
    /// It Creates a new comment for the specified park using the comment created previously
    /// It gathers all comments for the specified park
    /// It renders the webpage using BUnit parameters
    /// It gathers the button, then preforms a Click() action
    /// A new list of comments is gathered to ensure the second comment list is different from the first
    /// </summary>
    [Test]
    public void SubmitUpdatedComment_Click_Delete_Button_Should_Remove_Comment_From_List()
    {
        // Arrange
        Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
        string Name = "Nick";
        string DateTime = "2022-10-01";
        string Desc = "test descript";
        string Token = "0";
        string ParkId = "LAKE SAMMAMISH STATE PARK";
        //Create comment array
        var comment = new[] { Name, DateTime, Desc, Token };
        //Create new comment in specified park:
        TestHelper.ParkService.AddComment(ParkId, comment);
        //Gather all existing comments for the park 
        var comments = TestHelper.ParkService.GetParks().First(m => m.Id == ParkId).Comments;

        //Populate bunit component parameter dictionary: 
        var commentIndex = ComponentParameter.CreateParameter(nameof(DeleteComment.CommentIndex), 0);
        var parkId = ComponentParameter.CreateParameter(nameof(DeleteComment.SelectedParkId), ParkId);
        var name = ComponentParameter.CreateParameter(nameof(DeleteComment.Name), "John");
        var datetime = ComponentParameter.CreateParameter(nameof(DeleteComment.Datetime), DateTime);
        var desc = ComponentParameter.CreateParameter(nameof(DeleteComment.Description), Desc);
        var token = ComponentParameter.CreateParameter(nameof(DeleteComment.Token), Token);

        //Create parameter variable to hold component parameters
        var parameters = new[]
        {
                commentIndex, parkId, name, datetime, desc, token
            };

        // Act
        //Render webpage passing in parameters list
        var page = RenderComponent<UpdateComment>(parameters);
        // Get the Cards retrned
        var result = page.Markup;

        // Find the Buttons (more info)
        var buttonList = page.FindAll("Button");
        var button = buttonList.FirstOrDefault();
        //Call the deleteComment function in the page
        button.Click();
        //Gather the comments in the current park again
        var comments2 = TestHelper.ParkService.GetParks().First(m => m.Id == ParkId).Comments;

            // Assert
            //Ensure comments2 had an updated name 
            Assert.AreNotEqual(comments[0], comments2[0]);

    }
        /// <summary>
        /// This function checks to see if a comment was updated on button click with new information from an input field
        /// It creates four string values for comment creation
        /// It uses those strings to create BUnit parameters
        /// It Creates a new comment for the specified park using the comment created previously
        /// It gathers all comments for the specified park
        /// It renders the webpage using BUnit parameters
        /// It modifies an input, allowing for comment update
        /// It gathers the button, then preforms a Click() action
        /// A new list of comments is gathered to ensure the second comment list is different from the first
        /// </summary>
        [Test]
        public void SubmitUpdatedComment_Update_Token_Field_Should_Show_Updated_Token()
        {
            // Arrange
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            string Name = "Nick";
            string DateTime = "2022-10-01";
            string Desc = "test descript";
            string Token = "0";
            string ParkId = "LAKE SAMMAMISH STATE PARK";
            //Create comment array
            var comment = new[] { Name, DateTime, Desc, Token };
            //Create new comment in specified park:
            TestHelper.ParkService.AddComment(ParkId, comment);
            //Gather all existing comments for the park 

            //Populate bunit component parameter dictionary: 
            var commentIndex = ComponentParameter.CreateParameter(nameof(DeleteComment.CommentIndex), 0);
            var parkId = ComponentParameter.CreateParameter(nameof(DeleteComment.SelectedParkId), ParkId);
            var name = ComponentParameter.CreateParameter(nameof(DeleteComment.Name), "John");
            var datetime = ComponentParameter.CreateParameter(nameof(DeleteComment.Datetime), DateTime);
            var desc = ComponentParameter.CreateParameter(nameof(DeleteComment.Description), Desc);
            var token = ComponentParameter.CreateParameter(nameof(DeleteComment.Token), Token);

            //Create parameter variable to hold component parameters
            var parameters = new[]
            {
                commentIndex, parkId, name, datetime, desc, token
            };

            // Act
            //Render webpage passing in parameters list
            var page = RenderComponent<UpdateComment>(parameters);
            // Find the name input element
            var nameInput = page.Find("#token-input");
            // Simulate user input
            nameInput.Input("New Token");

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var button = buttonList.FirstOrDefault();
            button.Click();
            //Gather the comments in the current park again
            var comments = TestHelper.ParkService.GetParks().First(m => m.Id == ParkId).Comments;

            // Assert
            //Ensure comments2 had an updated name 
            Assert.AreEqual("New Token", comments[0][3]);

        }

        // <summary>
        /// This function checks to see if a comment was updated on button click with new information from an input field
        /// It creates four string values for comment creation
        /// It uses those strings to create BUnit parameters
        /// It Creates a new comment for the specified park using the comment created previously
        /// It gathers all comments for the specified park
        /// It renders the webpage using BUnit parameters
        /// It modifies an input, allowing for comment update
        /// It gathers the button, then preforms a Click() action
        /// A new list of comments is gathered to ensure the second comment list is different from the first
        /// </summary>
        [Test]
        public void SubmitUpdatedComment_Update_Description_Field_Should_Show_Updated_Description()
        {
            // Arrange
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            string Name = "Nick";
            string DateTime = "2022-10-01";
            string Desc = "test descript";
            string Token = "0";
            string ParkId = "LAKE SAMMAMISH STATE PARK";
            //Create comment array
            var comment = new[] { Name, DateTime, Desc, Token };
            //Create new comment in specified park:
            TestHelper.ParkService.AddComment(ParkId, comment);
            //Gather all existing comments for the park 

            //Populate bunit component parameter dictionary: 
            var commentIndex = ComponentParameter.CreateParameter(nameof(DeleteComment.CommentIndex), 0);
            var parkId = ComponentParameter.CreateParameter(nameof(DeleteComment.SelectedParkId), ParkId);
            var name = ComponentParameter.CreateParameter(nameof(DeleteComment.Name), "John");
            var datetime = ComponentParameter.CreateParameter(nameof(DeleteComment.Datetime), DateTime);
            var desc = ComponentParameter.CreateParameter(nameof(DeleteComment.Description), Desc);
            var token = ComponentParameter.CreateParameter(nameof(DeleteComment.Token), Token);

            //Create parameter variable to hold component parameters
            var parameters = new[]
            {
                commentIndex, parkId, name, datetime, desc, token
            };

            // Act
            //Render webpage passing in parameters list
            var page = RenderComponent<UpdateComment>(parameters);
            // Find the name input element
            var nameInput = page.Find("#description-input");
            // Simulate user input
            nameInput.Input("New Desc");

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var button = buttonList.FirstOrDefault();
            button.Click();
            //Gather the comments in the current park again
            var comments = TestHelper.ParkService.GetParks().First(m => m.Id == ParkId).Comments;

            // Assert
            //Ensure comments2 had an updated name 
            Assert.AreEqual("New Desc", comments[0][2]);

        }

        /// <summary>
        /// This function checks to see if a comment was updated on button click with new information from an input field
        /// It creates four string values for comment creation
        /// It uses those strings to create BUnit parameters
        /// It Creates a new comment for the specified park using the comment created previously
        /// It gathers all comments for the specified park
        /// It renders the webpage using BUnit parameters
        /// It modifies an input, allowing for comment update
        /// It gathers the button, then preforms a Click() action
        /// A new list of comments is gathered to ensure the second comment list is different from the first
        /// </summary>
        [Test]
        public void SubmitUpdatedComment_Update_Name_Field_Should_Show_Updated_Name()
        {
            // Arrange
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            string Name = "Nick";
            string DateTime = "2022-10-01";
            string Desc = "test descript";
            string Token = "0";
            string ParkId = "LAKE SAMMAMISH STATE PARK";
            //Create comment array
            var comment = new[] { Name, DateTime, Desc, Token };
            //Create new comment in specified park:
            TestHelper.ParkService.AddComment(ParkId, comment);
            //Gather all existing comments for the park 

            //Populate bunit component parameter dictionary: 
            var commentIndex = ComponentParameter.CreateParameter(nameof(DeleteComment.CommentIndex), 0);
            var parkId = ComponentParameter.CreateParameter(nameof(DeleteComment.SelectedParkId), ParkId);
            var name = ComponentParameter.CreateParameter(nameof(DeleteComment.Name), "John");
            var datetime = ComponentParameter.CreateParameter(nameof(DeleteComment.Datetime), DateTime);
            var desc = ComponentParameter.CreateParameter(nameof(DeleteComment.Description), Desc);
            var token = ComponentParameter.CreateParameter(nameof(DeleteComment.Token), Token);

            //Create parameter variable to hold component parameters
            var parameters = new[]
            {
                commentIndex, parkId, name, datetime, desc, token
            };

            // Act
            //Render webpage passing in parameters list
            var page = RenderComponent<UpdateComment>(parameters);
            // Find the name input element
            var nameInput = page.Find("#name-input");
            // Simulate user input
            nameInput.Input("New Name");

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");
            var button = buttonList.FirstOrDefault();
            button.Click();
            //Gather the comments in the current park again
            var comments = TestHelper.ParkService.GetParks().First(m => m.Id == ParkId).Comments;

            // Assert
            //Ensure comments2 had an updated name 
            Assert.AreEqual("New Name", comments[0][0]);

        }
        #endregion SubmitUpdatedComment
    }
}
