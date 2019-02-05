using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace AutomationTraining
{
    public static class WebDriverExtensions
    {
        public static bool WaitForElement(this IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
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
    }
}
