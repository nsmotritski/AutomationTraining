using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Task70.Locators
{
    public class TutByLoginPopup
    {
        private IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = "input[type=\"text\"][name=\"login\"]")]
        public IWebElement UsernameInput;

        [FindsBy(How = How.CssSelector, Using = "input[type=\"password\"][name=\"password\"]")]
        public IWebElement PasswordInput;

        [FindsBy(How = How.CssSelector, Using = "input.button.auth__enter[type=\"submit\"]")]
        private IWebElement LoginButton;

        public TutByLoginPopup(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void clickLoginButton()
        {
            LoginButton.Click();
        }

        private TutByLoginPopup setUsername(string username)
        {
            UsernameInput.SendKeys(username);
            return this;
        }

        private TutByLoginPopup setPassword(string password)
        {
            PasswordInput.SendKeys(password);
            return this;
        }

        public TutByMemberPage PerformLogin(string username, string password)
        {
            setUsername(username);
            setPassword(password);
            clickLoginButton();
            return new TutByMemberPage(_driver);
        }
    }
}
