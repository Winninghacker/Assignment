using OpenQA.Selenium;

namespace selenium_tests.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void SearchForProduct(string productName)
        {
            var searchBox = _driver.FindElement(By.XPath("//input[@placeholder='Search for Products, Brands and More']"));
            searchBox.SendKeys(productName + Keys.Enter);
        }

        public void SelectSecondItem()
        {
            var secondItem = _driver.FindElement(By.XPath("(//div[@class='KzDlHZ'])[2]"));
            secondItem.Click();
        }

        public void ReturnToHomePage()
        {
            var homeButton = _driver.FindElement(By.XPath("//img[@title='Flipkart']"));
            homeButton.Click();
        }

        public void NavigateToCart()
        {
            var cartButton = _driver.FindElement(By.XPath("//a[@class='_3SkBxJ']")); // Update XPath as needed
            cartButton.Click();
        }

        public void SelectSecondItemFromGrid()
        {
            var secondItem = _driver.FindElement(By.XPath("(//a[@class='wjcEIp'])[2]"));
            secondItem.Click();
        }
    }
}
