using OpenQA.Selenium;
using System.IO;

namespace selenium_tests.Utils
{
    public static class ScreenshotHelper
    {
        public static void CaptureScreenshot(IWebDriver driver, string fileName)
        {
            string screenshotsDir = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");

            if (!Directory.Exists(screenshotsDir))
            {
                Directory.CreateDirectory(screenshotsDir);
            }

            var filePath = Path.Combine(screenshotsDir, fileName + ".png");
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
        }
    }
}
