using NUnit.Framework;
using OpenQA.Selenium;
using System;
using Allure.Commons;
using Allure.NUnit.Attributes;
using AutomationTraining;
using NUnit.Framework.Interfaces;
using Task70.Locators;

namespace Task70
{
    [AllureSuite("Suite without base class")]
    [AllureEpic("Epic story")]
    public class TutByTests : AllureReport
    {
        private IWebDriver _driver;

        [SetUp]
        public void StartBrowser()
        {
            _driver = WebDriverHelper.WebDriverHelper.DeployWebDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [AllureLink("1")]
        [AllureOwner("Nikolai Smotritski")]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Minor)]
        [AllureTest("Test login to tut.by using PageObject")]
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

            //take screenshot before the final check
            _driver.TakeScreenshot("TutByLoginTest.png");

            //validate logged in user
            Assert.AreEqual(expectedUser, loggedUser, "User 'Selenium Test' is not logged in!");
        }

        [AllureLink("2")]
        [AllureOwner("Nikolai Smotritski")]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Minor)]
        [AllureTest("Test logout to tut.by using PageObject")]
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

            //take screenshot before the final check
            _driver.TakeScreenshot("TutByLogoutTest.png");

            //validate user was logged out
            Assert.AreEqual(homePageButtonText, buttonText, "User was not logged out!");
        }

        [AllureLink("3")]
        [AllureOwner("Nikolai Smotritski")]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Minor)]
        [AllureTest("Test login to tut.by using PageFactory")]
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

            //take screenshot before the final check
            _driver.TakeScreenshot("PageFactoryTutByLoginTest.png");

            //validate logged in user
            Assert.AreEqual(expectedUser, loggedUser, "User 'Selenium Test' is not logged in!");
        }

        [AllureLink("4")]
        [AllureOwner("Nikolai Smotritski")]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Minor)]
        [AllureTest("Test logout to tut.by using PageFactory")]
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

            //take screenshot before the final check
            _driver.TakeScreenshot("PageFactoryTutByLogoutTest.png");

            //validate user was logged out
            Assert.AreEqual(homePageButtonText, buttonText, "User was not logged out!");
        }

        [TearDown]
        public void CloseBrowser()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                _driver.TakeScreenshot(TestContext.CurrentContext.Test.Properties["AllureTest"].ToString());
            }
            _driver.Close();
        }
    }
}
