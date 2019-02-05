using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;

namespace AutomationTraining
{
    public class Task20
    {
        private IWebDriver _driver;
        private const string Username = "seleniumtests@tut.by";
        private const string Password = "123456789zxcvbn";
        private readonly TimeSpan _timeOut = new TimeSpan(0, 0, 10);

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
            //driver.Manage().Timeouts().ImplicitWait(10, TimeUnit.SECONDS);
        }

        [Test]
        public void TutByLoginTest()
        {
            //Open tut.by hompage
            _driver.Url = "https://tut.by";
            _driver.Manage().Window.Maximize();

            //wait for EnterButton displayed and open login popup
            _driver.WaitForElement(By.CssSelector(Selectors.EnterButtonSelector));
            var enterButton = _driver.FindElement(By.CssSelector(Selectors.EnterButtonSelector));
            enterButton.Click();

            //provide credentials and login
            _driver.WaitForElement(By.CssSelector(Selectors.UsernameInputSelector));
            var usernameInput = _driver.FindElement(By.CssSelector(Selectors.UsernameInputSelector));
            var passwordInput = _driver.FindElement(By.CssSelector(Selectors.PasswordInputSelector));
            _driver.TypeTextToElement(usernameInput, Username);
            _driver.TypeTextToElement(passwordInput, Password);
            var loginButton = _driver.FindElement(By.CssSelector(Selectors.LoginButton));
            loginButton.Click();

            //validate that test user is logged in
            var usernameSpan = _driver.FindElement(By.CssSelector(Selectors.UsernameSpanSelector));

            Assert.AreEqual("Selenium Test", usernameSpan.Text, "User 'Selenium Test' is not logged in!");
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }
    }
}