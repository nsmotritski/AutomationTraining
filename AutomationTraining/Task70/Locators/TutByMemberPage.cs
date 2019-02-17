using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Task70.Locators
{
    public class TutByMemberPage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = "span.uname")]
        public IWebElement UsernameSpan;

        [FindsBy(How = How.XPath, Using = "//a[contains(@href,\'logout\')]")]
        public IWebElement LogoutLink;

        public TutByMemberPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public string GetLoggedInUser()
        {
            return UsernameSpan.Text;
        }

        public TutByHomePage Logout()
        {
            UsernameSpan.Click();
            LogoutLink.Click();
            return new TutByHomePage(_driver);
        }
    }
}
