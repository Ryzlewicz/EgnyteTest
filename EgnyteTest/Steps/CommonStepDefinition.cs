using EgnyteTest.WrapperFactory;
using EgnyteTest.PageObjects;
using System.Configuration;
using TechTalk.SpecFlow;

namespace EgnyteTest.Steps
{
    [Binding]
    public class CommonStepDefinition
    {
        [Given(@"Setup driver")]
        public void GivenSetupDriver()
        {
            string downloadPath = ConfigurationManager.AppSettings["DownloadFolder"];
            string dataPath = ConfigurationManager.AppSettings["DataFolder"];
        }

        [Given(@"I open 'Chrome|IE|Firefox' browser and login'")]
        public void GivenOpenBrowser(string browser)
        {
            string url = ConfigurationManager.AppSettings["URL"];

            BrowserFactory.InitBrowser(browser);
            BrowserFactory.LoadApplication(url);
            Page.Login.LogInToApp("2bZ8Hzoe");
        }

        [Given(@"I open 'Chrome|IE|Firefox' browser and I enter '(.*)'")]
        public void GivenOpenBrowser(string browser, string password)
        {
            string url = ConfigurationManager.AppSettings["URL"];

            BrowserFactory.InitBrowser(browser);
            BrowserFactory.LoadApplication(url);
            Page.Login.LogInToApp(password);
        }
    }
}
