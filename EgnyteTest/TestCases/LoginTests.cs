using OpenQA.Selenium;
using NUnit.Framework;
using EgnyteTest.WrapperFactory;
using EgnyteTest.PageObjects;
using System.Configuration;

namespace EgnyteTest.TestCases
{
    class LoginTests
    {
        IWebDriver driver;

        public string url = ConfigurationManager.AppSettings["URL"];

        [SetUp]
        public void startChrome()
        {
            BrowserFactory.InitBrowser("Chrome");
            BrowserFactory.LoadApplication(url);
        }

        [Test(Description = "Check system behavior when valid password is entered")]
        public void validPassword()
        {
            Page.Login.LogInToApp("2bZ8Hzoe");
            Assert.AreEqual(BrowserFactory.GetUrl(), "https://qarecruitment.egnyte.com/fl/rCey0sCxMN#folder-link/");
        }

        [Test(Description = "Check system behavior when invalid password is entered")]
        public void invalidPassword()
        {
            Page.Login.LogInToApp("wRoNgPaSsWoRd");
            Assert.AreEqual(Page.Login.ErrorMessageIsDisplayed(), "Incorrect password. Try again.");
        }

        [Test(Description = "Check system behavior when empty password is entered")]
        public void emptyPassword()
        {
            Page.Login.LogInToApp("");
            Assert.AreEqual(BrowserFactory.GetUrl(), url);
        }

        [TearDown]
        public void closeChrome()
        {            
            BrowserFactory.CloseAllDrivers();
        }        
    }
}
