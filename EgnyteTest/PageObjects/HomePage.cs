using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace EgnyteTest.PageObjects
{
    public class HomePage
    {
        private IWebDriver driver;      

        [FindsBy(How = How.XPath, Using = "/html/body/div/div[1]/div/header/div[2]/div/div/div[1]/button[4]")]
        [CacheLookup]
        public IWebElement DownloadFolderBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div[1]/div/header/div[2]/div/div/div[1]/button[3]")]
        [CacheLookup]
        public IWebElement DownloadSelectedBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div[1]/div/div[1]/div[1]/div/div")] [CacheLookup]
        public IWebElement SelectAllCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div[1]/div/section/div/div/div[2]/div/ul/li[1]/a")]
        [CacheLookup]
        public IWebElement NavigationPrev { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div[1]/div/section/div/div/div[2]/div/ul/li[4]/a")]
        [CacheLookup]
        public IWebElement NavigationNext { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div[1]/div/section/div/div/ul/li[1]/div/div/div[1]")]
        [CacheLookup]
        public IWebElement CheckBox_1 { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div[1]/div/section/div/div/ul/li[2]/div/div/div[1]")]
        [CacheLookup]
        public IWebElement CheckBox_2 { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div[1]/div/section/div/div/ul/li[3]/div/div/div[1]")]
        [CacheLookup]
        public IWebElement CheckBox_3 { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div[1]/div/div[1]/div[1]/div/div")]
        [CacheLookup]
        public IWebElement CheckBox_All { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div[1]/div/section/div/div/ul/li[1]/div/div/div[4]/span/span[2]")] [CacheLookup]
        public IWebElement Folder_1 { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div[1]/div/section/div/div/ul/li[2]/div/div/div[4]/span/span[2]")] [CacheLookup]
        public IWebElement Folder_2 { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div[1]/div/section/div/div/ul/li[3]/div/div/div[4]/span/span[2]")] [CacheLookup]
        public IWebElement Folder_3 { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div[1]/div/section/div/div/ul/li[48]/div/div/div[4]/span/span[2]")][CacheLookup]
        public IWebElement Folder_48 { get; set; }


        public void DownloadFolder()
        {
            DownloadFolderBtn.Click();
            System.Threading.Thread.Sleep(10000);
        }

        public void DownloadSelected()
        {
            DownloadSelectedBtn.Click();
            System.Threading.Thread.Sleep(10000);
        }

        public void CheckboxUse(int index)
        {
            switch(index)
            {
                case 0:
                    CheckBox_All.Click();
                    break;
                case 1:
                    CheckBox_1.Click();
                    break;
                case 2:
                    CheckBox_2.Click();
                    break;
                case 3:
                    CheckBox_3.Click();
                    break;
            }            
        }

        public void FolderUse(int index)
        {
            switch(index)
            {
                case 1:
                    Folder_1.Click();
                    break;
                case 2:
                    Folder_2.Click();
                    break;
                case 3:
                    Folder_3.Click();
                    break;
                case 48:
                    Folder_48.Click();
                    break;

            }
            System.Threading.Thread.Sleep(2000);
        }
    }
}
