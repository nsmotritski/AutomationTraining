using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace AutomationTraining
{
    public class Task20
    {
        private IWebDriver _driver;

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

        [TestCase("seleniumtests@tut.by", "123456789zxcvbn", "Selenium Test")]
        [TestCase("seleniumtests2@tut.by", "123456789zxcvbn", "Selenium Test")]
        public void TutByLoginTest(string username, string password, string expectedUser)
        {
            //Open tut.by hompage
            _driver.Url = "https://tut.by";
            _driver.Manage().Window.Maximize();

            //wait for EnterButton displayed and open login popup
            _driver.WaitForElement(By.CssSelector(Selectors.EnterButtonSelector));
            var enterButton = _driver.FindElement(By.CssSelector(Selectors.EnterButtonSelector));
            enterButton.Click();
            Thread.Sleep(1000); //Thread.Sleep() is a waiter without a condition to wait for. Bad example of waiter.

            //provide credentials and login
            _driver.WaitForElement(By.CssSelector(Selectors.UsernameInputSelector));
            var usernameInput = _driver.FindElement(By.CssSelector(Selectors.UsernameInputSelector));
            var passwordInput = _driver.FindElement(By.CssSelector(Selectors.PasswordInputSelector));
            _driver.TypeTextToElement(usernameInput, username);
            _driver.TypeTextToElement(passwordInput, password);
            var loginButton = _driver.FindElement(By.CssSelector(Selectors.LoginButton));
            loginButton.Click();

            //validate that test user is logged in
            _driver.WaitForElement(By.CssSelector(Selectors.UsernameSpanSelector));
            var usernameSpan = _driver.FindElement(By.CssSelector(Selectors.UsernameSpanSelector));

            Assert.AreEqual(expectedUser, usernameSpan.Text, "User 'Selenium Test' is not logged in!");
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }
    }
}