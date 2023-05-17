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

        #region DeleteCurrentComment
        /// <summary>
        /// This test uses valid parameters and should return true
        /// It Creates a parkService from a mocked webhost,
        /// then passes in values to the renderComponent function
        /// </summary>
        [Test]
        public void DeleteCurrentComment_Passed_Valid_Parameters_Should_Return_True()
        {
            // Arrange
            //Create mock variables
            var loggerMock = new Mock<ILogger<ExploreModel>>();
            var envMock = new Mock<IWebHostEnvironment>();
            var parkService = new JsonFileParksService(envMock.Object);
            //Create a test context

            //Populate component paraments for OnInitialize



            // Act
            var result = true;

            // Assert
            Assert.True(result);
        }

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
