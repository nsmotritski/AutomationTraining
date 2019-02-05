using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutomationTraining;
using OpenQA.Selenium.Interactions;

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
            _driver.Url = "https://tut.by";
            _driver.Manage().Window.Maximize();
            var wait = new WebDriverWait(_driver, _timeOut);

            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(Selectors.EnterButtonSelector)));
            _driver.WaitForElement(By.CssSelector(Selectors.EnterButtonSelector));
            var enterButton = _driver.FindElement(By.CssSelector(Selectors.EnterButtonSelector));
            enterButton.Click();

            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(Selectors.UsernameInputSelector)));
            _driver.WaitForElement(By.CssSelector(Selectors.UsernameInputSelector));
            var usernameInput = wait.Until(x => x.FindElement(By.CssSelector(Selectors.UsernameInputSelector)));
            var passwordInput = wait.Until(x => x.FindElement(By.CssSelector(Selectors.PasswordInputSelector)));
            //var usernameInput = driver.FindElement(By.CssSelector("input[name=\"login\"]"));
            //var passwordInput = driver.FindElement(By.CssSelector("input[name=\"password\"]"));
            usernameInput.Click();
            _driver.TypeTextToElement(usernameInput, Username);
            passwordInput.Click();
            _driver.TypeTextToElement(passwordInput, Password);
            //usernameInput.SendKeys(Username);
            //passwordInput.SendKeys(Password);
            var loginButton = _driver.FindElement(By.CssSelector(Selectors.UsernameSpanSelector));
            loginButton.Click();
            var usernameSpan = _driver.FindElement(By.CssSelector(Selectors.AuthenticationForm));
        }



        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }
    }
}