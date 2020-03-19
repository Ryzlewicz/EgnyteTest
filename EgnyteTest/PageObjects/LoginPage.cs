using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace EgnyteTest.PageObjects
{
    public class LoginPage
    {
        [FindsBy(How = How.XPath, Using = "//*[@id=\"password\"]")][CacheLookup]
        private IWebElement Password { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/div[3]/div/form/a")][CacheLookup]
        private IWebElement ContinueBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"password_controls\"]/div")][CacheLookup]
        private IWebElement ErrorMessage { get; set; }

        public string ErrorMessageIsDisplayed()
        {
            return ErrorMessage.Text.ToString();
        }

        public void LogInToApp(string pass)
        {            
            Password.SendKeys(pass);
            ContinueBtn.Click();
            System.Threading.Thread.Sleep(2000);
        }

    }
}
