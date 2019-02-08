using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AutomationTraining
{
    public class AlertsTests
    {
        private IWebDriver _driver;
        private const string ExpectedAlertText = "You successfuly clicked an alert";
        private const string ExpectedConfirmText = "You clicked: Ok";
        private const string ExpectedPromptText = "You entered: ";
        private static readonly Random Random = new Random();

        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        [SetUp]
        public void StartBrowser()
        {
            var pathToDriver = Path.Combine(AssemblyDirectory, "drivers");
            _driver = new ChromeDriver(pathToDriver);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void JSAlertTest()
        {
            //Open the start page of the test
            _driver.Url = "https://the-internet.herokuapp.com/javascript_alerts";
            _driver.Manage().Window.Maximize();

            //click JS alert button
            var alertButton = _driver.FindElement(By.XPath(AlertsSelectors.JSAlertButton));
            alertButton.Click();

            //get alert text
            _driver.SwitchTo().Alert().Accept();

            //check that the text of the alert is the one expected
            var resultElement = _driver.FindElement(By.CssSelector(AlertsSelectors.ResultElement));

            Assert.AreEqual(ExpectedAlertText, resultElement.Text, "The result text is not the one expected!");
        }

        [Test]
        public void JSConfirmTest()
        {
            //Open the start page of the test
            _driver.Url = "https://the-internet.herokuapp.com/javascript_alerts";
            _driver.Manage().Window.Maximize();

            //click JS confirm button
            var confirmButton = _driver.FindElement(By.XPath(AlertsSelectors.JSConfirmButton));
            confirmButton.Click();

            //get confirm text
            _driver.SwitchTo().Alert().Accept();

            //check that the text of the confirm is the one expected
            var resultElement = _driver.FindElement(By.CssSelector(AlertsSelectors.ResultElement));

            Assert.AreEqual(ExpectedConfirmText, resultElement.Text, "The result text is not the one expected!");
        }

        [Test]
        public void JSPromptTest()
        {
            var stringToInput = RandomString(10);

            //Open the start page of the test
            _driver.Url = "https://the-internet.herokuapp.com/javascript_alerts";
            _driver.Manage().Window.Maximize();

            //click JS prompt button
            var promptButton = _driver.FindElement(By.XPath(AlertsSelectors.JSPromptButton));
            promptButton.Click();

            //input random string in the opened JS prompt
            _driver.SwitchTo().Alert().SendKeys(stringToInput);
            _driver.SwitchTo().Alert().Accept();

            //check that the text of the prompt is the one expected
            var resultElement = _driver.FindElement(By.CssSelector(AlertsSelectors.ResultElement));

            Assert.AreEqual(ExpectedPromptText + stringToInput, resultElement.Text, "The result text is not the one expected!");
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }
    }
}
