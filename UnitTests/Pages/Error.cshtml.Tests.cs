﻿using System.Diagnostics;

using Microsoft.Extensions.Logging;

using NUnit.Framework;

using Moq;

using LetsGoPark.WebSite.Pages;

namespace UnitTests.Pages.Error
{
    public class ErrorTests
    {
        /// <summary>
        /// Unit tests for pageModel
        /// </summary>
        #region TestSetup
        public static ErrorModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<ErrorModel>>();

            pageModel = new ErrorModel(MockLoggerDirect)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }

        #endregion TestSetup

        #region OnGet
        [Test]
        /// <summary>
        /// Creates a new activity
        /// Call OnGet method then reset
        /// Tests if the model is valid and Ids match and was correctly used
        /// </summary>
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange
            //Create new activity
            Activity activity = new Activity("activity");
            activity.Start();

            // Act
            //call OnGet method
            pageModel.OnGet();

            // Reset
            activity.Stop();

            // Assert
            //ensure model is valid
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            //ensure Ids match and was correctly used
            Assert.AreEqual(activity.Id, pageModel.RequestId);
        }

        [Test]
        /// <summary>
        /// Call OnGet method when activity is null
        /// Tests if the model is invalid and Id matches desired value 
        /// </summary>
        public void OnGet_InValid_Activity_Null_Should_Return_TraceIdentifier()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("trace", pageModel.RequestId);
            Assert.AreEqual(true, pageModel.ShowRequestId);
        }
        #endregion OnGet
    }
}