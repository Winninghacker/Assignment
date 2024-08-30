using NUnit.Framework;
using RestSharp;
using System;

namespace TrelloApiTests.Base
{
    public class BaseTest
    {
        // RequestSpecification to be used in all API requests
        protected static RestClient client;
        protected static RestRequest request;

        // Variable to store the ID of the Trello board
        protected static string boardID;

        /// <summary>
        /// Setup method to create RequestSpecifications before any tests run. This method is executed once before any tests in the class.
        /// </summary>
        [OneTimeSetUp]
        public void CreateRequestSpecifications()
        {
            // Initialize RestClient with base URL
            client = new RestClient("https://api.trello.com");

            // Initialize RestRequest with necessary headers and query parameters
            request = new RestRequest()
                .AddQueryParameter("key", "9f9358a4704adbd011634ab28eb21512") // API key
                .AddQueryParameter("token", "ATTA7e25286fabcb14baeb4bab3f6d28bd28a06fb940c0187d6ad141126a9f2f9ecdF7F5BC6D") // API token
                .AddHeader("Content-Type", "application/json"); // Content-Type header
        }

        /// <summary>
        /// Cleanup method to delete the Trello board after all tests have run. This method is executed once after all tests in the class.
        /// </summary>
        [OneTimeTearDown]
        public void DeleteBoard()
        {
            // Check if boardID is not null
            if (!string.IsNullOrEmpty(boardID))
            {
                var deleteRequest = new RestRequest("/1/boards/{id}", Method.Delete)
                    .AddUrlSegment("id", boardID); // Path parameter for board ID

                // Send DELETE request to delete the board
                var response = client.Execute(deleteRequest);

                // Verify successful deletion with status code 200
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Failed to delete the board.");
            }
            else
            {
                Console.WriteLine("boardID is null, cannot delete board"); // Log message if boardID is null
            }
        }

        /// <summary>
        /// Data provider method for providing board field data to test methods.
        /// </summary>
        /// <returns>A collection of board field data</returns>
        public static IEnumerable<TestCaseData> BoardField()
        {
            yield return new TestCaseData("idMemberCreator"); // Board field: idMemberCreator
            yield return new TestCaseData("idOrganization"); // Board field: idOrganization
            yield return new TestCaseData("desc"); // Board field: desc (description)
            yield return new TestCaseData("name"); // Board field: name
        }
    }
}
