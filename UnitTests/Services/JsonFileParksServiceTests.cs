using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using LetsGoPark.WebSite.Models;
using System.Linq;
using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;

namespace UnitTests.Pages.Park.AddRating
{
    /// <summary>
    /// Unit tests for JsonFileParksService
    /// </summary>
    public class JsonFileParksServiceTests
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

        #region AddRating
        /// <summary>
        /// Gather park to test
        /// Pass in bad value to function
        /// Tests if the function fails, returns false
        /// </summary>
        [Test]
        public void AddRating_InValid_Boundary_Below_0_Valid_ID_Should_Return_False()
        {
            // Arrange
            //Gather park to test
            var data = TestHelper.ParkService.GetParks().First();

            // Act
            //Pass in bad value to function
            var result = TestHelper.ParkService.AddRating(data.Id, -1);

            // Assert
            //Ensure function fails, returns false
            Assert.AreEqual(false, result);
        }

        [Test]
        /// <summary>
        /// Gather park to test
        /// Pass in bad value to function
        /// Tests if the function fails, returns false
        /// </summary>
        public void AddRating_InValid_Boundary_Above_5_Valid_ID_Should_Return_False()
        {
            // Arrange
            //Gather park to test
            var data = TestHelper.ParkService.GetParks().First();

            // Act
            //Pass in bad value to function
            var result = TestHelper.ParkService.AddRating(data.Id, 6);

            // Assert
            //Ensure function fails, return false
            Assert.AreEqual(false, result);
        }

        [Test]
        /// <summary>
        /// Gather park to test
        /// Try to add a rating to park that doesn't exist
        /// Tests if the function fails, returns false
        /// </summary>
        public void AddRating_InValid_Park_Null_Valid_Rating_Should_Return_False()
        {
            // Arrange
            // Act
            //Try to add a rating to park that doesn't exist
            var result = TestHelper.ParkService.AddRating(null, 1);

            // Assert
            //Ensure function fails, return false
            Assert.AreEqual(false, result);
        }

        [Test]
        /// <summary>
        /// Gather park to test
        /// Pass in bad value to function
        /// Tests if the function fails, returns false
        /// </summary>
        public void AddRating_InValid_Park_ID_Valid_Rating_Should_Return_False()
        {
            // Arrange

            // Act
            //Pass in bad Id for park
            var result = TestHelper.ParkService.AddRating("BadId", 1);

            // Assert
            //Ensure function fails, return false
            Assert.AreEqual(false, result);
        }

        [Test]
        /// <summary>
        /// Gather park to test
        /// Pass in Id with empty ratings
        /// Tests if the function fails, returns false
        /// </summary>
        public void AddRating_Empty_ParkArray_Rating_Valid_Should_Return_True()
        {
            // Arrange
            

            // Act
            //Id with empty ratings
            var result = TestHelper.ParkService.AddRating("2nd Ave South Dock", 1);

            // Assert
            //Ensure rating array created, and value was added.
            Assert.AreEqual(true, result);
        }

        [Test]
        /// <summary>
        /// Gather park to test
        /// Pass in bad value to function
        /// Tests if the function fails, returns false
        /// </summary>
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
            //Ensure function was successful
            Assert.AreEqual(true, result);
            //Ensure count was incremented by one
            Assert.AreEqual(countOriginal + 1, dataNewList.Ratings.Length);
            //Ensure the rating is the most recent input
            Assert.AreEqual(5, dataNewList.Ratings.Last());
        }
        #endregion AddRating

        #region GetHighestRatedPark
        [Test]
        /// <summary>
        /// Gather highest rated park
        /// Tests if the park Id is as desired
        /// </summary>
        public void GetHighestRatedPark_Ensure_Correct_Park_Returned_Should_Be_Lake_Sammamish()
        {
            //Arrange

            //Act
            //Gather highest rated park
            var park = TestHelper.ParkService.GetHighestRatedPark();

            //Assert
            //Ensure highest rated park LAKE SAMMAMISH STATE PARK
            Assert.AreEqual(park.Id, "LAKE SAMMAMISH STATE PARK");

        }
        #endregion GetHighestRatedPark

        #region GetHighestRatedParks
        [Test]
        /// <summary>
        /// Gather highest rated park
        /// Tests if the park Id is as desired
        /// </summary>
        public void GetHighestRatedParks_Ensure_Correct_Park_Returned_Should_Return_Array_Size_3()
        {
            //Act
            //Get top three parks
            var parks = TestHelper.ParkService.GetHighestRatedParks();

            //Assert
                //Ensure highest City pakr is Doris Cooper
            Assert.AreEqual(parks[0].Id, "Doris Cooper Houghton Beach Park");
                //Ensure State park is Lake sammamish
            Assert.AreEqual(parks[1].Id, "LAKE SAMMAMISH STATE PARK");
                //Ensure National is Mount Rainier
            Assert.AreEqual(parks[2].Id, "North Cascades National Park");

        }
        #endregion GetHighestRatedParks

        #region CompareParks
        [Test]
        /// <summary>
        /// Grab park with null rating, assign as top park
        /// Compare parks
        /// Tests if the parks were switched
        /// </summary>
        public void CompareParks_TopPark_Null_park_Not_Null_Should_Update_Top_Park()
        {
            //Arrange
            //Grab park with null rating, assign as top park
            var toppark = TestHelper.ParkService.GetParks().First(x => x.Id == "FLAMING GEYSER STATE PARK");
            //Grab park with a rating
            var newpark = TestHelper.ParkService.GetParks().First(x=> x.Id == "Juanita Beach Park");

            //Act
            //Compare parks
            var newTopPark = TestHelper.ParkService.CompareParks(toppark, newpark);

            //Assert
            //Ensure parks were switched
            Assert.AreEqual(newpark.Id, newTopPark.Id);
        }

        [Test]
        /// <summary>
        /// Create two parks with same rating, toppark has less votes
        /// Swap parks
        /// Tests if the newpark is newtoppark
        /// </summary>
        public void CompareParks_TopPark_Less_Votes_Same_Rating_Should_Switch_To_Newpark()
        {
            //Arrange
            //Create two parks with same rating, toppark has less votes
            var toppark = TestHelper.ParkService.GetParks().First(x => x.Id == "BRIDLE TRAILS STATE PARK");
            var newpark = TestHelper.ParkService.GetParks().First(x => x.Id == "LAKE SAMMAMISH STATE PARK");

            //Act
            //should swap parks
            var newTopPark = TestHelper.ParkService.CompareParks(toppark, newpark);

            //Assert
            //ensure newpark is newtoppark
            Assert.AreEqual(newpark.Id, newTopPark.Id);
        }
        #endregion CompareParks

        #region AddComment
        [Test]
        /// <summary>
        /// Create new comment
        /// Get park and add new comment to selected park
        /// Tests if the 4 values in the comment array are equal to the passed in values
        /// </summary>
        public void AddComment_Comment_Added_To_Empty_Array_Should_Create_Comment()
        {
            //Arrange
            //Create new comment
            string[] comment = new string[] {"TestName", "2023-04-20 10:09:34", "This is a Test", "100" };
            //Get park
            var park = TestHelper.ParkService.GetParks().First(x => x.Id == "Marsh Park");

            //Act
            //Add new comment to selected park
            TestHelper.ParkService.AddComment(park.Id, comment);
            string[] addedComment = TestHelper.ParkService.GetParks().First(x => x.Id == "Marsh Park").Comments.First();

            //Assert
            //Ensure the 4 values in the comment array are equal to the passed in values
            Assert.AreEqual(comment[0], addedComment[0]);
            Assert.AreEqual(comment[1], addedComment[1]);
            Assert.AreEqual(comment[2], addedComment[2]);
            Assert.AreEqual(comment[3], addedComment[3]);
        }

        [Test]
        /// <summary>
        /// Create new comment array and gather first park in national Parks
        /// Attempt to add comment
        /// Regrab park with updated comment
        /// Tests if the comment grabbed is the same one just created in the park
        /// </summary>
        public void AddComment_Comment_Added_To_Populated_Array_Should_Create_New_Comment()
        {
            //Arrange
            //Create new comment array and gather first park in national Parks
            string[] comment = new string[] { "TestName", "2023-04-20 10:09:34", "This is a Test", "100" };
            var park = TestHelper.ParkService.GetParks().FirstOrDefault(Park => Park.Park_system == ParkSystemEnum.National);

            //Act
            //Attempt to add comment
            TestHelper.ParkService.AddComment(park.Id, comment);
            //Regrab park with updated comment
            park = TestHelper.ParkService.GetParks().FirstOrDefault(Park => Park.Park_system == ParkSystemEnum.National);
            //Grab the last comment
            string[] addedComment = park.Comments[park.Comments.Length - 1];

            //Assert
            //Ensure the comment grabbed is the same one just created in the park
            Assert.AreEqual(comment[0], addedComment[0]);
            Assert.AreEqual(comment[1], addedComment[1]);
            Assert.AreEqual(comment[2], addedComment[2]);
            Assert.AreEqual(comment[3], addedComment[3]);

        }

        #endregion AddComment

        #region UpdateComment


        [Test]
        /// <summary>
        /// Create new comment and update
        /// Tests if the comment update was successful
        /// </summary>
        public void UpdateComment_With_Valid_Comment_Should_Return_True()
        {
            // Arrange
            //Create noew comment
            string[] comment = { "3", "4", "5", "6" };
            //Choose which park to add comment to
            string parkId = "San Juan Island National Historical Park";
            //Invoke add comment function
            TestHelper.ParkService.AddComment(parkId, comment);

            // Act
            //Create updated comment to pass
            bool result = TestHelper.ParkService.UpdateComment(parkId, new[] { "I'm here", "4", "5", "6" }, 0);

            // Assert
            //Ensure comment update was successful
            Assert.IsTrue(result);
        }

        [Test]
        /// <summary>
        /// Create new comment and update with null value entries
        /// Tests if the comment update was failed
        /// </summary>
        public void UpdateComment_With_Null_Comment_Should_Return_False()
        {
            // Arrange
            //Create comment and grab park to send it to
            string[] comment = { "3", "4", "5", "6" };
            string parkId = "San Juan Island National Historical Park";
            //Add comment to selected park
            TestHelper.ParkService.AddComment(parkId, comment);

            // Act
            //Invoke Update comment
            bool result = TestHelper.ParkService.UpdateComment(parkId, null, 0);

            // Assert
            //Ensure update failed
            Assert.IsFalse(result);
        }

        [Test]
        /// <summary>
        /// Create new comment and update with Non-existent park ID
        /// Tests if the comment update was failed
        /// </summary>
        public void UpdateComment_With_Invalid_ParkId_Should_Return_False()
        {
            // Arrange
            // Create new comment
            string[] comment = { "3", "4", "5", "6" };
            //Get fake park id
            string parkId = "Non-existent park ID";

            // Act
            //To to pass comment to a fake id
            bool result = TestHelper.ParkService.UpdateComment(parkId, new[] { "I'm here", "4", "5", "6" }, 0);

            // Assert
            //Ensure update failed
            Assert.IsFalse(result);
        }

        [Test]
        /// <summary>
        /// Create new comment and update with a comment index that doesn't exist
        /// Tests if the comment update was failed
        /// </summary>
        public void UpdateComment_With_Invalid_CommentIndex_Should_Return_False()
        {
            // Arrange
            //Comment creation
            string[] comment = { "3", "4", "5", "6" };
            string parkId = "San Juan Island National Historical Park";
            //Add comment to selected park
            TestHelper.ParkService.AddComment(parkId, comment);

            // Act
            //Invoke Update Comment with a comment index that doesn't exist
            bool result = TestHelper.ParkService.UpdateComment(parkId, new[] { "I'm here", "4", "5", "6" }, 5);

            // Assert
            //Ensure update fails
            Assert.IsFalse(result);
        }

        [Test]
        /// <summary>
        /// Create new comment and update and update with a new comment
        /// Tests if the comment was updated
        /// </summary>
        public void UpdateComment_Updates_Comment_Successfully_Should_Display_Expected_String()
        {
            // Arrange
            //Create comment and choose which park to test
            string[] comment = { "3", "4", "5", "6" };
            string parkId = "San Juan Island National Historical Park";
            //Add comment to park
            TestHelper.ParkService.AddComment(parkId, comment);

            // Act
            //Update park with new commment
            TestHelper.ParkService.UpdateComment(parkId, new[] { "I'm here", "4", "5", "6" }, 0);
            //Grab updated comment from the test
            var updatedComment = TestHelper.ParkService.GetParks().First(p => p.Id == parkId).Comments[0];

            // Assert
            //Ensure comment was updated
            Assert.AreEqual(updatedComment[0], "I'm here");
        }


        [Test]
        /// <summary>
        /// Create new comment and update and update with index less than comment count
        /// Tests if the comment was updated
        /// </summary>
        public void UpdateComment_With_Index_Less_Than_Comment_Count_Should_Update_Successfully()
        {
            // Arrange
            //Select park and create comment
            var selectedParkId = "San Juan Island National Historical Park";
            string[] comment = { "New comment", "New comment", "New comment", "New comment" };
            //Track comment index
            var oldCommentIndex = 0;

            // Act
            //Try to update comment
            var result = TestHelper.ParkService.UpdateComment(selectedParkId, comment, oldCommentIndex);

            // Assert
            //ensure updatw was successful
            Assert.IsTrue(result);
            var updatedComment = TestHelper.ParkService.GetParks()
                .First(x => x.Id == selectedParkId)
                .Comments[oldCommentIndex];
            //Esnure the comment returned matches the updated comment we used a  a parameter
            Assert.AreEqual(comment, updatedComment);
        }

        [Test]
        /// <summary>
        /// Create new comment and update and update with index greater than comment count
        /// Tests if the comment was not updated
        /// </summary>
        public void UpdateComment_With_Index_Greater_Than_Comment_Count_Should_Return_False()
        {
            // Arrange
            var selectedParkId = "San Juan Island National Historical Park";
            string[] comment = { "New comment", "New comment", "New comment", "New comment" };
            var oldCommentIndex = 10;

            // Act
            var result = TestHelper.ParkService.UpdateComment(selectedParkId, comment, oldCommentIndex);

            // Assert
            Assert.IsFalse(result);
        }
        [Test]
        /// <summary>
        /// Create new comment and update and update with null values
        /// Tests if the comment was not updated
        /// </summary>
        public void UpdateComment_Pass_Null_Value_Should_Return_False()
        {
            // Arrange
            string[] comment = { "New comment", "New comment", "New comment", "New comment" };
            var oldCommentIndex = 0;

            // Act
            //Invoke Update comment with a null value for Id
            var result = TestHelper.ParkService.UpdateComment(null, comment, oldCommentIndex);

            // Assert
            //Ensure update failed
            Assert.AreEqual(result, false);
        }
        #endregion UpdateComment

        #region DeleteComment
        [Test]
        /// <summary>
        /// Delete a comment with null value
        /// Tests if the delete is failed
        /// </summary>
        public void DeleteComment_Pass_Null_Value_Should_Return_False()
        {
            // Arrange
            //
            var oldCommentIndex = 0;

            // Act
            //Pass a null value for Id
            var result = TestHelper.ParkService.DeleteComment(null, oldCommentIndex);

            // Assert
            //Ensure delete failed
            Assert.AreEqual(result, false);
        }
        [Test]
        /// <summary>
        /// Delete a comment when at least one comment in the array left
        /// Tests if the delete is successful
        /// </summary>
        public void DeleteComment_Comments_Deleted_Successfully_At_Least_1_Comment_In_Array_Left_Count_Should_Be_Equal()
        {
            //Arrange
            //Get Initial comment count
            //Add comment so intial length of Comments array is 1
            string[] comment = { "3", "4", "5", "6" };
            TestHelper.ParkService.AddComment(TestHelper.ParkService.GetParks().First(x => x.Id == "San Juan Island National Historical Park").Id, comment);
            int countInitial= TestHelper.ParkService.GetParks().First
                   (x => x.Id == "San Juan Island National Historical Park").Comments.Length;

            //Add a comment so length is 2
            TestHelper.ParkService.AddComment(TestHelper.ParkService.GetParks().First(x => x.Id == "San Juan Island National Historical Park").Id, comment);


            //Act
            //Delete comment so length is again 1
            TestHelper.ParkService.DeleteComment("San Juan Island National Historical Park", 0);
            int countAfter = TestHelper.ParkService.GetParks().First
                    (x => x.Id == "San Juan Island National Historical Park").Comments.Length;
            //Assert
            Assert.AreEqual(countInitial, countAfter);
        }

        [Test]
        /// <summary>
        /// Delete a comment 
        /// Tests if the comment was deleted
        /// </summary>
        public void DeleteComment_Remove_Only_Comment_Comments_Attribute_Should_Be_Null()
        {
            // Arrange
            //Grab desired park with only 1 comment
            int countInitial = TestHelper.ParkService.GetParks().First
                   (x => x.Id == "San Juan Island National Historical Park").Comments.Length;
            var selectedParkId = "San Juan Island National Historical Park";
            var commentIndex = 0;

            // Act
            TestHelper.ParkService.DeleteComment(selectedParkId, commentIndex);
            var Comments = TestHelper.ParkService.GetParks().First(x => x.Id == "San Juan Island National Historical Park").Comments;
            
            // Assert
            //Comments should have gone from an array to null
            Assert.AreEqual(null, Comments);
        }

       

        [Test]
        /// <summary>
        /// Delete a comment that doesn't exsit
        /// Tests if the delete failed
        /// </summary>
        public void DeleteComment_Requested_Park_ID_Does_Not_Exist_Should_Return_False()
        {
            // Arrange
            var selectedParkId = "San Juanito extremely hugely big Island National Historical Park";
            var commentIndex = 0;

            // Act
            //Deletes an invalid comment from a park with no comments
            bool retVal = TestHelper.ParkService.DeleteComment(selectedParkId, commentIndex);

            //Assert
            Assert.IsFalse(retVal);
        }

        [Test]
        /// <summary>
        /// Delete a comment that with negative index
        /// Tests if the delete failed
        /// </summary>
        public void DeleteComment_Passing_Negative_Index_Should_Return_False()
        {

            // Arrange
            string selectedParkId = "San Juan Island National Historical Park";
            //Index value that isn't valid for the specified park
            int commentIndex = -1;
            bool retVal = TestHelper.ParkService.DeleteComment(selectedParkId, commentIndex);

            // Act and Assert
            //Ensure the function failed due to poor index passed.
            Assert.IsFalse(retVal);
        }

        #endregion DeleteComment

        #region CreateData
        [Test]
        /// <summary>
        /// Create data with valid input
        /// Tests if the park entry is created 
        /// </summary>
        public void CreateData_With_Valid_Input_Should_Create_New_Park()
        {
            //Arrange
            var newPark = new ParksModel
            {
                Id = "Test Park",
                Description = "This is a test park",
                Url = "http://testpark.com",
                Image = "http://testpark.com",
                Address = "123 Test Street",
                Park_system = ParkSystemEnum.National,
                Activities =  "Hiking" ,
                Map_brochure = "http://testpark.com",
                Permits = "No fees"
            };

            //Act
            //Creates a new park with valid data
            TestHelper.ParkService.CreateData(newPark); 
            var testPark = TestHelper.ParkService.GetParks().FirstOrDefault(m => m.Id.Equals(newPark.Id));

            //Assert
            //Ensure pulled park is the one we just created
            Assert.AreEqual(newPark.Id, testPark.Id);


        }
        [Test]
        /// <summary>
        /// Create data with invalid input
        /// Tests if the park entry is created 
        /// </summary>
        public void CreateData_With_Invalid_Input_Should_Return_Null()
        {
            //Arrange
            var newPark = new ParksModel
            {
                Id = "",
                Description = "This is a test park",
                Url = "http://testpark.com",
                Image = "http://testpark.com",
                Address = "123 Test Street",
                Park_system = ParkSystemEnum.National,
                Activities = "Hiking",
                Map_brochure = "http://testpark.com",
                Permits = "No fees"
            };

            //Act
            var result = TestHelper.ParkService.CreateData(newPark);
            

            //Assert
            Assert.IsNull(result);


        }
        [Test]
        /// <summary>
        /// Create data with default input
        /// Tests if the park entry is created 
        /// </summary>
        public void CreateData_With_Default_Input_Should_Return_Not_Null()
        {
            //Arrange
            var data = new ParksModel()
            {
                Id = "Enter Park Id",
                Url = "Enter URL",
                Image = "Enter Image URL",
                Description = "Enter Description",
                Ratings = null,
                Address = "Enter Park Address",
                Phone = "Enter Park Agency Phone Number",
                Park_system = ParkSystemEnum.National,
                Activities = "Enter activites separated by a comma, or NA",
                Map_brochure = "Enter Map brochure URL or NA",
                Permits = "Enter any fees associated with park",
                Comments = null,
            };

            //Act
            var createResult = TestHelper.ParkService.CreateData(data);
            
            //Assert
            Assert.IsNotNull(createResult);
        }

        #endregion CreateData

        #region UpdateData
        [Test]
        /// <summary>
        /// Update data with valid input
        /// Tests if the update is successful
        /// </summary>
        public void UpdateData_Passing_In_Valid_Data_Should_Return_Updated_Park()
        {
            //Arrange
            //Create new park
            var data = new ParksModel()
            {
                Id = "Enter Park Id",
                Url = "Enter URL",
                Image = "Enter Image URL",
                Description = "Enter Description",
                Ratings = null,
                Address = "Enter Park Address",
                Phone = "Enter Park Agency Phone Number",
                Park_system = ParkSystemEnum.National,
                Activities = "Enter activites separated by a comma, or NA",
                Map_brochure = "Enter Map brochure URL or NA",
                Permits = "Enter any fees associated with park",
                Comments = null,
            };
            //Add to database
            TestHelper.ParkService.CreateData(data );

            //Act
            string newURL = "updated URL"; 
            //Change URL from initial creation
            data.Url = newURL;
            //Invoke UpdateData with the created parkmodel instance
            var result = TestHelper.ParkService.UpdateData(data);

            //Assert
            //ensure URL field has been updated
            Assert.AreEqual(data.Url, newURL);

        }

        [Test]
        /// <summary>
        /// Update data with null values included in the input
        /// Tests if the update is successful
        /// </summary>
        public void UpdateData_Passing_In_InValid_Data_Should_Return_Null()
        {
            //Arrange
            ///Create new park w/o adding to database
            var data = new ParksModel()
            {
                Id = "Enter Park Id",
                Url = "Enter URL",
                Image = "Enter Image URL",
                Description = "Enter Description",
                Ratings = null,
                Address = "Enter Park Address",
                Phone = "Enter Park Agency Phone Number",
                Park_system = ParkSystemEnum.National,
                Activities = "Enter activites separated by a comma, or NA",
                Map_brochure = "Enter Map brochure URL or NA",
                Permits = "Enter any fees associated with park",
                Comments = null,
            };

            //Act
            //Invoke UpdateData with the created parkmodel instance
            var result = TestHelper.ParkService.UpdateData(data);

            //Assert
            //ensure URL field has been updated
            Assert.IsNull(result);

        }
        #endregion UpdateData

        #region DeleteData
        [Test]
        /// <summary>
        /// Delete data with valid Id
        /// Tests if the delete is successful
        /// </summary>
        public void DeleteData_Passing_In_Valid_Id_Should_Return_New_Park()
        {
            //Arrange
            //Creating new park to delete
            var data = new ParksModel()
            {
                Id = "Enter Park Id",
                Url = "Enter URL",
                Image = "Enter Image URL",
                Description = "Enter Description",
                Ratings = null,
                Address = "Enter Park Address",
                Phone = "Enter Park Agency Phone Number",
                Park_system = ParkSystemEnum.National,
                Activities = "Enter activites separated by a comma, or NA",
                Map_brochure = "Enter Map brochure URL or NA",
                Permits = "Enter any fees associated with park",
                Comments = null,
            };
            //Act
            //Add the new park to the database
            TestHelper.ParkService.CreateData(data) ;
            //Delete park
            var result = TestHelper.ParkService.DeleteData(data.Id) ;


            //Assert
            //Ensure park deletion
            Assert.AreEqual(data.Id, result.Id) ;


        }

        [Test]
        /// <summary>
        /// Delete data with invalid Id
        /// Tests if the delete is successful
        /// </summary>
        public void DeleteData_Passing_In_Valid_Id_Should_Return_Null()
        {
            //Arrange
            //No arrange needed

            //Delete park
            var result = TestHelper.ParkService.DeleteData("I dont exist");


            //Assert
            //Ensure park deletion
            Assert.IsNull(result);

        }
        #endregion DeleteData
    }
}
