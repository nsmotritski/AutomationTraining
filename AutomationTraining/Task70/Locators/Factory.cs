using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Task70.Locators
{
    public class Factory
    {
        private IWebDriver _driver;
           
        public Factory(IWebDriver driver)
        {
            _driver = driver;
        }
       
        public TutByHomePage homePage()
        {
            return new TutByHomePage(_driver);
        }
       
        public TutByLoginPopup loginPopup()
        {
            return new TutByLoginPopup(_driver);
        }

        public TutByMemberPage memberPage()
        {
            return new TutByMemberPage(_driver);
        }
    }
}
