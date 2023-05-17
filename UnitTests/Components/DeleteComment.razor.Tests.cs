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
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using LetsGoPark.WebSite.Pages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using System;
using Microsoft.AspNetCore.Components;

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
        /// 
        /// </summary>
        [Test]
        public void DeleteCurrentComment_Passed_Invalid_Parameters_Should_Return_False()
        {

        }
        #endregion DeleteCurrentComment
    }
}
