using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace selenium_tests.Pages
{
    public class CartPage
    {
        private readonly IWebDriver _driver;

        public CartPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public bool AreItemsPresent(List<string> itemNames)
        {
            var cartItems = _driver.FindElements(By.XPath("//div[@class='_3NFOwM']")).Select(e => e.Text).ToList();
            return itemNames.All(name => cartItems.Contains(name));
        }

        public void RemoveItem(string itemName)
        {
            var removeButtonXPath = $"//div[contains(text(),'{itemName}')]/following-sibling::div[contains(@class,'sBxzFz')]";
            var removeButton = _driver.FindElement(By.XPath(removeButtonXPath));
            removeButton.Click();

            var removePopupButton = _driver.FindElement(By.XPath("//div[contains(@class,'sBxzFz fF30ZI')][1]"));
            removePopupButton.Click();
        }
    }
}
