using NUnit.Framework;
using RestSharp;
using System;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions; // Add FluentAssertions namespace
using Newtonsoft.Json;
using NLog;

namespace Tests
{
    [TestFixture]
    public class TrelloAPIs
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private static string listID; // Variable to store the ID of the created list
        private static string cardID; // Variable to store the ID of the created card
        private static string boardID; // Variable to store the ID of the created board

        private RestClient client;
        private RestRequest request;

        [SetUp]
        public void SetUp()
        {
            client = new RestClient("https://api.trello.com/");
            request = new RestRequest();
            // Add any additional setup here (e.g., setting up authentication)
        }

        [Test, Order(0)]
        public async Task CreateBoard()
        {
            Logger.Info("Creating board");

            request.Resource = "1/boards";
            request.AddQueryParameter("name", "Test new board");
            request.AddQueryParameter("prefs_background", "orange");
            request.AddQueryParameter("prefs_background_url", "https://images.unsplash.com/photo-1676694090990-b9e29828fdd3?ixid=Mnw3MDY2fDB8MXxjb2xsZWN0aW9ufDN8MzE3MDk5fHx8fHwyfHwxNjc2OTc4MzA4&ixlib=rb-4.0.3&w=2560&h=2048&q=90");

            var response = await client.PostAsync(request);
            var board = JsonConvert.DeserializeObject<GetBoards>(response.Content);
            boardID = board.Id; // Store the board ID
            Console.WriteLine(boardID);
            Logger.Info("Board created successfully");
        }

        [Test, Order(1)]
        public async Task GetCreatedBoard()
        {
            Logger.Info("Retrieving board data");

            request.Resource = $"1/boards/{boardID}";
            var response = await client.GetAsync(request);
            var board = JsonConvert.DeserializeObject<GetBoards>(response.Content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            board.Id.Should().Be(boardID);
            board.Name.Should().Be("Test new board");

            Logger.Info("Board data retrieved successfully");
        }

        [Test, Order(2)]
        public async Task GetMembershipsOfABoard()
        {
            Logger.Info("Retrieving board memberships");

            request.Resource = $"1/boards/{boardID}/memberships";
            var response = await client.GetAsync(request);
            var memberships = JsonConvert.DeserializeObject<dynamic[]>(response.Content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            ((string)memberships[0].memberType).Should().Be("admin");

            Logger.Info("Board memberships retrieved successfully");
        }

        [Test, Order(3)]
        [TestCase("name")]
        [TestCase("desc")]
        public async Task GetAFieldOnABoard(string field)
        {
            Logger.Info($"Retrieving field '{field}' on board");

            request.Resource = $"1/boards/{boardID}/{field}";
            var response = await client.GetAsync(request);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            Logger.Info($"Field '{field}' retrieved successfully");
        }

        [Test, Order(4)]
        public async Task UpdateABoard()
        {
            Logger.Info("Updating board");

            request.Resource = $"1/boards/{boardID}";
            request.AddQueryParameter("prefs/background", "green");
            var response = await client.PutAsync(request);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            Logger.Info("Board updated successfully");
        }

        [Test, Order(5)]
        public async Task CreateList()
        {
            Logger.Info("Creating list");

            request.Resource = $"1/boards/{boardID}/lists";
            request.AddQueryParameter("name", "list1");
            var response = await client.PostAsync(request);
            var list = JsonConvert.DeserializeObject<dynamic>(response.Content);
            listID = list.Id; // Store the list ID

            Logger.Info("List created successfully");
        }

        [Test, Order(6)]
        public async Task CreateCard()
        {
            Logger.Info("Creating card");

            request.Resource = "1/cards";
            request.AddQueryParameter("name", "card");
            request.AddQueryParameter("idList", listID);
            var response = await client.PostAsync(request);
            var card = JsonConvert.DeserializeObject<dynamic>(response.Content);
            cardID = card.Id; // Store the card ID

            Logger.Info("Card created successfully");
        }

        
        [Test, Order(8)]
        public async Task GetCreatedBoardFail()
        {
            Logger.Info("Intentionally failing test");

            request.Resource = $"1/boards/{boardID}";
            var response = await client.GetAsync(request);
            var board = JsonConvert.DeserializeObject<GetBoards>(response.Content);

            response.StatusCode.Should().Be((System.Net.HttpStatusCode)201); // Intentionally incorrect status code for failure
            board.Id.Should().Be(boardID);
            board.Name.Should().Be("Test Case 1"); // Intentionally incorrect name for failure

            Logger.Info("Intentionally failed test completed");
        }
    }
}
