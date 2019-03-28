using NUnit.Framework;
using OpenQA.Selenium;
using System;
using Allure.Commons;
using Allure.NUnit.Attributes;
using AutomationTraining;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Task70.Locators;

namespace Task70
{
    [AllureSuite("Suite without base class")]
    [AllureEpic("Epic story")]
    [TestFixture]
    [Parallelizable]
    public class TutByTests : AllureReport
    {
        private IWebDriver _driver;
        private readonly string _sauceUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
        private readonly string _sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);

        [SetUp]
        public void StartBrowser()
        {
            //DesiredCapabilities caps = new DesiredCapabilities();
            //caps.SetCapability("browserName", "Safari");
            //caps.SetCapability("platform", "macOS 10.13");
            //caps.SetCapability("version", "11.1");
            //caps.SetCapability("username", _sauceUserName);
            //caps.SetCapability("accessKey", _sauceAccessKey);
            //caps.SetCapability("name", TestContext.CurrentContext.Test.Name);

            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--start-maximized");

            //var localNodeURL = "http://localhost:5566/wd/hub";
            var jenkinsGridURL = "http://localhost:4444/wd/hub";

            //const string sauceLabsURL = "http://ondemand.eu-central-1.saucelabs.com/wd/hub";

            //_driver = WebDriverHelper.WebDriverHelper.DeployWebDriver();
            //_driver = new RemoteWebDriver(new Uri(localNodeURL), options);
            //_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            //_driver = new RemoteWebDriver(new Uri(localNodeURL), options);
            _driver = new RemoteWebDriver(new Uri(jenkinsGridURL), options);

            //_driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));
            //_driver = new RemoteWebDriver(new Uri("http://ondemand.eu-central-1.saucelabs.com/wd/hub"), caps, TimeSpan.FromSeconds(600));

            //_driver = new RemoteWebDriver(new Uri($"http://\" + {_sauceUserName} + \":\" + {_sauceAccessKey} + \"@ondemand.saucelabs.com:80/wd/hub\""), caps, TimeSpan.FromSeconds(600));

            //_javascriptExecutor = ((IJavaScriptExecutor)_driver);
        }

        [AllureLink("1")]
        [AllureOwner("Nikolai Smotritski")]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Minor)]
        [AllureTest("Test login to tut.by using PageObject")]
        [TestCase("seleniumtests@tut.by", "123456789zxcvbn", "Selenium Test")]
        [Parallelizable]
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
        [Parallelizable]
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
        [Parallelizable]
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
        [Parallelizable]
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
            _driver.Quit();
        }
    }
}
