using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace AutomationTraining
{
    /*
     * From Task 20:
     * Complete 2 task:
     * 1.	Create test 1, which goes to https://www.tut.by/, 
     * login with correct credentials (Username – seleniumtests@tut.by, 
     * Password – 123456789zxcvbn; create new project for that in Visual Studio; 
     * choose testing framework NUnit3*). Do not forget to add assertion in your test.
     * 2.	Create By variables, which over all possible types of location in Selenium WebDriver (in separate class).

     *  From Task 50:
     *  Complete tasks:
     * 1.	Add implicit waiter for WebDriver.
     * 2.	Add Thread.sleep for login test, which was created on previous training. What type of waiter is it (add your answer as comment near sleep)?
     * 3.	Add explicit waiter* for login test, which will wait until name appears (after login). Add polling frequency, which is differ from default value 
     * 4.	Create DDT test, which will login with different credentials
       
     */
    public class Task20
    {
        private IWebDriver _driver;

        [SetUp]
        public void StartBrowser()
        {
            _driver = WebDriverHelper.WebDriverHelper.DeployWebDriver();
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
            _driver.WaitForElement(By.CssSelector(TutBySelectors.EnterButtonSelector));
            var enterButton = _driver.FindElement(By.CssSelector(TutBySelectors.EnterButtonSelector));
            enterButton.Click();
            Thread.Sleep(1000); //Thread.Sleep() is a waiter without a condition to wait for. Bad example of waiter.

            //provide credentials and login
            _driver.WaitForElement(By.CssSelector(TutBySelectors.UsernameInputSelector));
            var usernameInput = _driver.FindElement(By.CssSelector(TutBySelectors.UsernameInputSelector));
            var passwordInput = _driver.FindElement(By.CssSelector(TutBySelectors.PasswordInputSelector));
            _driver.TypeTextToElement(usernameInput, username);
            _driver.TypeTextToElement(passwordInput, password);
            var loginButton = _driver.FindElement(By.CssSelector(TutBySelectors.LoginButton));
            loginButton.Click();

            //validate that test user is logged in
            _driver.WaitForElement(By.CssSelector(TutBySelectors.UsernameSpanSelector));
            var usernameSpan = _driver.FindElement(By.CssSelector(TutBySelectors.UsernameSpanSelector));

            Assert.AreEqual(expectedUser, usernameSpan.Text, "User 'Selenium Test' is not logged in!");
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }
    }
}