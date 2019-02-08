using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;

namespace AutomationTraining
{
    public class FrameTest
    {
        private IWebDriver _driver;
        private const string RegularText = "Hello";
        private const string BoldText = "world!";

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
        public void FrameTextInputTest()
        {
            //Open the start page of the test
            _driver.Url = "https://the-internet.herokuapp.com/iframe";
            _driver.Manage().Window.Maximize();

            //switch to iframe
            var iframe = _driver.FindElement(By.CssSelector(FrameSelectors.IFrameSelector));
            _driver.SwitchTo().Frame(iframe);

            //input regular text to the frame
            var frameInput = _driver.FindElement(By.CssSelector(FrameSelectors.FrameTextSelector));
            frameInput.Clear();
            _driver.TypeTextToElement(frameInput, RegularText + " ");

            //Switch to main content, then click bold button
            _driver.SwitchTo().DefaultContent();
            var boldButton = _driver.FindElement(By.CssSelector(FrameSelectors.BoldButtonSelector));
            boldButton.Click();

            //Switch back to the iframe and input bold part of the text
            _driver.SwitchTo().Frame(iframe);
            _driver.TypeTextToElement(frameInput, BoldText);

            //Get the text entered to the frame
            var actualText = frameInput.Text;

            Assert.IsTrue(actualText.Contains(RegularText) && actualText.Contains(BoldText), "Not all the text was entered!");
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }
    }
}
