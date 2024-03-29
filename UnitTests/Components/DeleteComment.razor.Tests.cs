﻿using Bunit;
using NUnit.Framework;

using LetsGoPark.WebSite.Components;
using Microsoft.Extensions.DependencyInjection;
using LetsGoPark.WebSite.Services;
using LetsGoPark.WebSite.Models;
using System.Linq;


namespace UnitTests.Components
{
    /// <summary>
    /// Unit tests for DeleteComment.razor
    /// </summary>
    public class DeleteCommentTests : BunitTestContext
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
        public void DeleteComment_Page_Passed_Parameters_Should_Return_Page()
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
            var page = RenderComponent<DeleteComment>(parameters);

            // Assert
            //Check to see if page is loaded in a valid state
            Assert.NotNull(page);
        }
        #endregion ModelRendered

        #region DeleteCurrentComment
        /// <summary>
        /// This function checks to see if a comment was deleted on button click
        /// It creates four string values for comment creation
        /// It uses those strings to create BUnit parameters
        /// It Creates a new comment for the specified park using the comment created previously
        /// It gathers all comments for the specified park
        /// It renders the webpage using BUnit parameters
        /// It gathers the button, then preforms a Click() action
        /// A new list of comments is gathered to ensure the second comment list is different from the first
        /// </summary>
        [Test]
        public void DeleteCurrentComment_Click_Delete_Button_Should_Remove_Comment_From_List()
        {
            // Arrange
            Services.AddSingleton<JsonFileParksService>(TestHelper.ParkService);
            string Name = "Nick";
            string DateTime = "2022-10-01";
            string Desc = "test descript";
            string Token = "0";
            string ParkId = "LAKE SAMMAMISH STATE PARK";
            //Populate bunit component parameter dictionary: 
            var commentIndex = ComponentParameter.CreateParameter(nameof(DeleteComment.CommentIndex), 0);
            var parkId = ComponentParameter.CreateParameter(nameof(DeleteComment.SelectedParkId), ParkId);
            var name = ComponentParameter.CreateParameter(nameof(DeleteComment.Name), Name);
            var datetime = ComponentParameter.CreateParameter(nameof(DeleteComment.Datetime), DateTime);
            var desc = ComponentParameter.CreateParameter(nameof(DeleteComment.Description), Desc);
            var token = ComponentParameter.CreateParameter(nameof(DeleteComment.Token), Token);

            //Create parameter variable to hold component parameters
            var parameters = new[]
            {
                commentIndex, parkId, name, datetime, desc, token
            };
            //Create comment array
            var comment = new[] { Name, DateTime, Desc, Token};

            //Create new comment in specified park:
            TestHelper.ParkService.AddComment(ParkId, comment);
            //Gather all existing comments for the park 
            var comments = TestHelper.ParkService.GetParks().First(m => m.Id == ParkId).Comments;
            // Act
            //Render webpage passing in parameters list
            var page = RenderComponent<DeleteComment>(parameters);
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
            //Ensure The new comments aren't the same as the original
            Assert.AreNotEqual(comments, comments2);

        }
        #endregion DeleteCurrentComment
    }
}
