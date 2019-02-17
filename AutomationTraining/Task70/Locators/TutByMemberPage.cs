using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Task70.Locators
{
    public class TutByMemberPage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = "span.uname")]
        public IWebElement UsernameSpan;

        public TutByMemberPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public string GetLoggedInUser()
        {
            return UsernameSpan.Text;
        }
    }
}
