using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using selenium_tests.Pages;
using selenium_tests.TestData;
using selenium_tests.Utils;
using TechTalk.SpecFlow;
using System.Collections.Generic;

namespace selenium_tests.StepDefinitions
{
    [Binding]
    public class FlipkartStepDefinitions
    {
        private IWebDriver? _driver;
        private DataProvide? _dataProvide;
        private HomePage? _homePage;
        private ProductPage? _productPage;
        private CartPage? _cartPage;

        [BeforeScenario]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();

            // Load test data from JSON file
            _dataProvide = JsonDataHelper.LoadTestData<DataProvide>("TestData/DataProvide.json");

            _homePage = new HomePage(_driver);
            _productPage = new ProductPage(_driver);
            _cartPage = new CartPage(_driver);
        }

        [AfterScenario]
        public void TearDown()
        {
            if (_driver != null)
            {
                _driver.Quit();
            }
        }

        [Given(@"I am on the Flipkart home page")]
        public void GivenIAmOnTheFlipkartHomePage()
        {
            if (_driver != null)
            {
                _driver.Navigate().GoToUrl("https://www.flipkart.com");
            }
            else
            {
                Assert.Fail("Driver is not initialized.");
            }
        }
        [When(@"I search for '(.*)'")]
        public void WhenISearchFor(string searchTerm)
        {
            _homePage?.SearchForProduct(searchTerm);
        }

        [When(@"I select the second item from the search results")]
        public void WhenISelectTheSecondItemFromTheSearchResults()
        {
            _homePage?.SelectSecondItem();
        }

        [When(@"I enter the pin code '(.*)' and check availability")]
        public void WhenIEnterThePinCodeAndCheckAvailability(string pinCode)
        {
            _productPage?.EnterPinCode(pinCode);
            _productPage?.CheckAvailability();
        }

        [When(@"I add the item to the cart")]
        public void WhenIAddTheItemToTheCart()
        {
            _productPage?.AddToCart();
        }

        [When(@"I return to the home page")]
        public void WhenIReturnToTheHomePage()
        {
            _homePage?.ReturnToHomePage();
        }

        [When(@"I search for '(.*)'")]
        public void WhenISearchForItem(string searchTerm)
        {
            _homePage?.SearchForProduct(searchTerm);
        }

        [When(@"I select the second item from the grid")]
        public void WhenISelectTheSecondItemFromTheGrid()
        {
            _homePage?.SelectSecondItemFromGrid();
        }

        [When(@"I enter the pin code '(.*)' and check availability")]
        public void WhenIEnterThePinCodeAndCheckAvailabilityForSecondItem(string pinCode)
        {
            _productPage?.EnterPinCode(pinCode);
            _productPage?.CheckAvailability();
        }

        [When(@"I add the second item to the cart")]
        public void WhenIAddTheSecondItemToTheCart()
        {
            _productPage?.AddToCart();
        }

        [When(@"I navigate to the cart")]
        public void WhenINavigateToTheCart()
        {
            _homePage?.NavigateToCart();
        }

        [Then(@"I should see both items in the cart")]
        public void ThenIShouldSeeBothItemsInTheCart()
        {
            var itemNames = new List<string> { _dataProvide?.Item1 ?? "", _dataProvide?.Item2 ?? "" };
            Assert.IsTrue(_cartPage?.AreItemsPresent(itemNames) ?? false);
        }

        [When(@"I remove the (.*) from the cart")]
        public void WhenIRemoveItemFromTheCart(string itemName)
        {
            _cartPage?.RemoveItem(itemName);
        }
    }
}
