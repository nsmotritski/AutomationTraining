using NUnit.Framework;
using OpenQA.Selenium;
using System;
using Task70.Locators;

namespace Task70
{
    public class TutByTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void StartBrowser()
        {
            _driver = WebDriverHelper.WebDriverHelper.DeployWebDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TestCase("seleniumtests@tut.by", "123456789zxcvbn", "Selenium Test")]
        public void TutByLoginTest(string username, string password, string expectedUser)
        {
            //Open tut.by hompage
            _driver.Url = "https://tut.by";
            _driver.Manage().Window.Maximize();

            //perform login (with methods chaining)
            TutByHomePage homePage = new TutByHomePage(_driver);
            var loggedUser = homePage.ClickEnterButton()
                                    .PerformLogin(username, password)
                                    .GetLoggedInUser();

            //validate logged in user
            Assert.AreEqual(expectedUser, loggedUser, "User 'Selenium Test' is not logged in!");
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }
    }
}
