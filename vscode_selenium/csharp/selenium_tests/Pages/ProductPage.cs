using OpenQA.Selenium;

namespace selenium_tests.Pages
{
    public class ProductPage
    {
        private readonly IWebDriver _driver;

        public ProductPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterPinCode(string pinCode)
        {
            var pinCodeInput = _driver.FindElement(By.XPath("//input[@placeholder='Enter Delivery Pincode']"));
            pinCodeInput.SendKeys(pinCode);
        }

        public void CheckAvailability()
        {
            var checkButton = _driver.FindElement(By.XPath("//span[contains(.,'Check✕')]"));
            checkButton.Click();
        }

        public void AddToCart()
        {
            var addToCartButton = _driver.FindElement(By.XPath("//button[normalize-space(text())='Add to cart']"));
            addToCartButton.Click();
        }
    }
}
