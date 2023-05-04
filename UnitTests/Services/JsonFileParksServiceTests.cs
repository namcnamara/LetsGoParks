using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using LetsGoPark.WebSite.Models;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace UnitTests.Pages.Park.AddRating
{
    public class JsonFileParksServiceTests
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        #region AddRating
        [Test]
        public void AddRating_InValid_Boundary_Below_0_Valid_ID_Should_Return_False()
        {
            // Arrange
            var data = TestHelper.ParkService.GetParks().First();

            // Act

            var result = TestHelper.ParkService.AddRating(data.Id, -1);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AddRating_InValid_Boundary_Above_5_Valid_ID_Should_Return_False()
        {
            // Arrange
            var data = TestHelper.ParkService.GetParks().First();

            // Act
            var result = TestHelper.ParkService.AddRating(data.Id, 6);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AddRating_InValid_Park_Null_Valid_Rating_Should_Return_False()
        {
            // Arrange

            // Act

            var result = TestHelper.ParkService.AddRating(null, 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AddRating_InValid_Park_ID_Valid_Rating_Should_Return_False()
        {
            // Arrange

            // Act

            var result = TestHelper.ParkService.AddRating("BadId", 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AddRating_Empty_ParkArray_Rating_Valid_Should_Return_True()
        {
            // Arrange
            

            // Act
            //Id with empty ratings
            var result = TestHelper.ParkService.AddRating("2nd Ave South Dock", 1);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void AddRating_Valid_Park_Valid_Rating_Valid_Should_Return_True()
        {
            // Arrange

            // Get the First data item
            var data = TestHelper.ParkService.GetParks().First();
            var countOriginal = data.Ratings.Length;

            // Act
            var result = TestHelper.ParkService.AddRating(data.Id, 5);
            var dataNewList = TestHelper.ParkService.GetParks().First();

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(countOriginal + 1, dataNewList.Ratings.Length);
            Assert.AreEqual(5, dataNewList.Ratings.Last());
        }
        #endregion AddRating

        #region GetHighestRatedPark
        [Test]
        public void GetHighestRatedPark_Ensure_Correct_Park_Returned()
        {
            //Arrange

            //Act
            var park = TestHelper.ParkService.GetHighestRatedPark();

            //Assert
            Assert.AreEqual(park.Id, "LAKE SAMMAMISH STATE PARK");

        }
        #endregion GetHighestRatedPark

        #region GetHighestRatedParks
        [Test]
        public void GetHighestRatedParks_Ensure_Correct_Park_Returned()
        {
            //Arrange
            

            //Act
            var parks = TestHelper.ParkService.GetHighestRatedParks();

            //Assert
                //City
            Assert.AreEqual(parks[0].Id, "Juanita Beach Park");
                //State
            Assert.AreEqual(parks[1].Id, "LAKE SAMMAMISH STATE PARK");
                //National
            Assert.AreEqual(parks[2].Id, "San Juan Island National Historical Park");

        }
        #endregion GetHighestRatedParks

        #region CompareParks
        [Test]
        public void CompareParks_TopPark_Null_park_Not_Null()
        {
            //Arrange
            var toppark = TestHelper.ParkService.GetParks().First(x => x.Id == "FLAMING GEYSER STATE PARK");
            var newpark = TestHelper.ParkService.GetParks().First(x=> x.Id == "Juanita Beach Park");

            //Act
            var newTopPark = TestHelper.ParkService.CompareParks(toppark, newpark);

            //Assert
            Assert.AreEqual(newpark, newTopPark);
        }
        #endregion CompareParks

        #region AddComment
        [Test]
        public void AddComment_Comment_Added_To_Empty_Array()
        {
            //Arrange
            string[] comment = new string[] {"TestName", "2023-04-20 10:09:34", "This is a Test", "100" };
            var park = TestHelper.ParkService.GetParks().First();

            //Act
            TestHelper.ParkService.AddComment(park.Id, comment);
            string[] addedComment = TestHelper.ParkService.GetParks().First().Comments.First();

            //Assert
            Assert.AreEqual(comment[0], addedComment[0]);
            Assert.AreEqual(comment[1], addedComment[1]);
            Assert.AreEqual(comment[2], addedComment[2]);
            Assert.AreEqual(comment[3], addedComment[3]);

        }

        [Test]
        public void AddComment_Comment_Added_To_Populated_Array()
        {
            //Arrange
            string[] comment = new string[] { "TestName", "2023-04-20 10:09:34", "This is a Test", "100" };
            var park = TestHelper.ParkService.GetParks().FirstOrDefault(Park => Park.Park_system == "National Parks");

            //Act
            TestHelper.ParkService.AddComment(park.Id, comment);
            park = TestHelper.ParkService.GetParks().FirstOrDefault(Park => Park.Park_system == "National Parks");
            string[] addedComment = park.Comments[park.Comments.Length - 1];

            //Assert
            Assert.AreEqual(comment[0], addedComment[0]);
            Assert.AreEqual(comment[1], addedComment[1]);
            Assert.AreEqual(comment[2], addedComment[2]);
            Assert.AreEqual(comment[3], addedComment[3]);

        }

        #endregion AddComment

        #region UpdateComment

        [Test]
        public void UpdateComment_With_Null_Comment_Returns_False()
        {
            // Arrange
            string[] comment = null;
            int oldCommentIndex = 0;

            // Act
            bool result = TestHelper.ParkService.UpdateComment("San Juan Island National Historical Park", comment, oldCommentIndex);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void UpdateComment_With_Empty_Comment_Returns_False()
        {
            // Arrange
            string[] comment = new string[0];
            int oldCommentIndex = 0;

            // Act
            bool result = TestHelper.ParkService.UpdateComment("San Juan Island National Historical Park", comment, oldCommentIndex);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void UpdateComment_With_Null_SelectedParkId_Returns_False()
        {
            // Arrange
            string[] comment = { "New comment" };
            int oldCommentIndex = 0;

            // Act
            bool result = TestHelper.ParkService.UpdateComment(null, comment, oldCommentIndex);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void UpdateComment_With_Nonexistent_SelectedParkId_Returns_False()
        {
            // Arrange
            string[] comment = { "New comment" };
            int oldCommentIndex = 0;

            // Act
            bool result = TestHelper.ParkService.UpdateComment("Nonexistent park", comment, oldCommentIndex);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void UpdateComment_With_Invalid_OldCommentIndex_Returns_False()
        {
            // Arrange
            string[] comment = { "New comment" };
            int oldCommentIndex = 10;

            // Act
            bool result = TestHelper.ParkService.UpdateComment("San Juan Island National Historical Park", comment, oldCommentIndex);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void UpdateComment_With_Valid_Parameters_Updates_Comment_And_Returns_True()
        {
            // Arrange
            string[] comment = { "New comment" };
            int oldCommentIndex = 0;

            // Act
            bool result = TestHelper.ParkService.UpdateComment("San Juan Island National Historical Park", comment, oldCommentIndex);

            // Assert
            Assert.IsTrue(result);

            var updatedComment = TestHelper.ParkService.GetParks().First(x => x.Id == "San Juan Island National Historical Park").Comments[oldCommentIndex];
            Assert.AreEqual(comment, updatedComment);
        }

        [Test]
        public void UpdateComment_With_Valid_OldCommentIndex_UpdatesComment()
        {
            // Arrange
            var selectedParkId = "San Juan Island National Historical Park";
            var comment = new string[] { "New comment" };
            var oldCommentIndex = 0;

            // Act
            var result = TestHelper.ParkService.UpdateComment(selectedParkId, comment, oldCommentIndex);

            // Assert
            Assert.IsTrue(result);
            var updatedComment = TestHelper.ParkService.GetParks()
                .First(x => x.Id == selectedParkId)
                .Comments[oldCommentIndex];
            Assert.AreEqual(comment, updatedComment);
        }
        #endregion UpdateComment

        #region DeleteComment
        [Test]
        public void DeleteComment_Comments_Deleted_Successfully_At_Least_1_Comment_In_Array_Left()
        {
            //Arrange
                //Get Initial comment count
            var Comments = TestHelper.ParkService.GetParks().First
                    (x => x.Id == "San Juan Island National Historical Park").Comments;

            //Add comment so intial length of Comments array is 1
            string[] comment = { "3", "4", "5", "6" };
            TestHelper.ParkService.AddComment(TestHelper.ParkService.GetParks().First(x => x.Id == "San Juan Island National Historical Park").Id, comment);
            int countInitial= TestHelper.ParkService.GetParks().First
                   (x => x.Id == "San Juan Island National Historical Park").Comments.Length;

            //Add a comment so length is 2
            TestHelper.ParkService.AddComment(TestHelper.ParkService.GetParks().First(x => x.Id == "San Juan Island National Historical Park").Id, comment);
            int countMid = TestHelper.ParkService.GetParks().First
                    (x => x.Id == "San Juan Island National Historical Park").Comments.Length;

            //Act
            //Delete comment so length is again 1
            TestHelper.ParkService.DeleteComment("San Juan Island National Historical Park", 0);
            int countAfter = TestHelper.ParkService.GetParks().First
                    (x => x.Id == "San Juan Island National Historical Park").Comments.Length;
            //Assert
            Assert.AreNotEqual(countAfter, countMid);
            Assert.AreEqual(countInitial, countAfter);
        }
        #endregion DeleteComment

    }
}
