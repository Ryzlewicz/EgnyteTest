using EgnyteTest.PageObjects;
using EgnyteTest.WrapperFactory;
using TechTalk.SpecFlow;

namespace EgnyteTest
{
    [Binding]
    public sealed class LoginSteps
    {        
        [Then(@"Page URL is correct")]
        public void CheckPage()
        {
            string expectedUrl = "https://qarecruitment.egnyte.com/fl/rCey0sCxMN#folder-link/";
            string.Equals(BrowserFactory.GetUrl(), expectedUrl);
        }
        
        [Then(@"Error '(.*)' message is displayed")]
        public void CheckPage(string message)
        {
            string.Equals(Page.Login.ErrorMessageIsDisplayed(), message);
        }
    }
}
