using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace AutomationTraining
{
    public static class WebDriverExtensions
    {
        public static bool WaitForElement(this IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15))
            {
                PollingInterval = TimeSpan.FromMilliseconds(600)
            };
            var element = wait.Until(condition =>
            {
                try
                {
                    var elementToBeDisplayed = driver.FindElement(by);
                    return elementToBeDisplayed.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
            return false;
        }

        public static void TypeTextToElement(this IWebDriver driver, IWebElement element, string text)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Click();
            actions.SendKeys(text);
            actions.Build().Perform();
        }

        public static void TakeScreenshot(this IWebDriver driver, string testTitle)
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            string Runname = testTitle + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss");
            string screenshotfilename = Path.Combine(WebDriverHelper.WebDriverHelper.AssemblyDirectory, Runname + ".jpg");
            ss.SaveAsFile(screenshotfilename, ScreenshotImageFormat.Png);
        }
    }
}
