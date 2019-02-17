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

        [TestCase("seleniumtests@tut.by", "123456789zxcvbn", "Войти")]
        public void TutByLogoutTest(string username, string password, string homePageButtonText)
        {
            //Open tut.by hompage
            _driver.Url = "https://tut.by";
            _driver.Manage().Window.Maximize();

            //perform login and logout (with methods chaining)
            TutByHomePage homePage = new TutByHomePage(_driver);
            var buttonText = homePage.ClickEnterButton()
                .PerformLogin(username, password)
                .Logout()
                .EnterButton.Text;

            //validate user was logged out
            Assert.AreEqual(homePageButtonText, buttonText, "User was not logged out!");
        }

        [TestCase("seleniumtests@tut.by", "123456789zxcvbn", "Selenium Test")]
        public void PageFactoryTutByLoginTest(string username, string password, string expectedUser)
        {
            //Open tut.by hompage
            _driver.Url = "https://tut.by";
            _driver.Manage().Window.Maximize();

            //perform login (with methods chaining)
            var pages = new Factory(_driver);
            var loggedUser = pages.homePage().ClickEnterButton()
                .PerformLogin(username, password)
                .GetLoggedInUser();

            //validate logged in user
            Assert.AreEqual(expectedUser, loggedUser, "User 'Selenium Test' is not logged in!");
        }

        [TestCase("seleniumtests@tut.by", "123456789zxcvbn", "Войти")]
        public void PageFactoryTutByLogoutTest(string username, string password, string homePageButtonText)
        {
            //Open tut.by hompage
            _driver.Url = "https://tut.by";
            _driver.Manage().Window.Maximize();

            //perform login and logout (with methods chaining)
            var pages = new Factory(_driver);
            var buttonText = pages.homePage().ClickEnterButton()
                .PerformLogin(username, password)
                .Logout()
                .EnterButton.Text;

            //validate user was logged out
            Assert.AreEqual(homePageButtonText, buttonText, "User was not logged out!");
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }
    }
}
