using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Task70.Locators
{
    public class TutByHomePage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = "a[data-target-popup=\"authorize-form\"]")]
        private IWebElement EnterButton;

        public TutByHomePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public TutByLoginPopup ClickEnterButton()
        {
            EnterButton.Click();
            return new TutByLoginPopup(_driver);
        }

    }
}
