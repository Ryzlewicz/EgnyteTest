using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using EgnyteTest.WrapperFactory;
using EgnyteTest.PageObjects;
using System.Configuration;

namespace EgnyteTest.TestCases
{
    class OtherTests
    {
        IWebDriver driver;

        public string downloadPath = ConfigurationManager.AppSettings["DownloadFolder"];
        public string dataPath = ConfigurationManager.AppSettings["DataFolder"];
        public string originalExtractFolder = @"C:\EgnyteTest\EgnyteTemp\Original";
        public string newExtractFolder = @"C:\EgnyteTest\EgnyteTemp\New";
        public string userName = "Aleksander Ryzlewski";
        public string url = ConfigurationManager.AppSettings["URL"];

        [SetUp]
        public void startBrowser()
        {
            BrowserFactory.InitBrowser("Chrome");
            BrowserFactory.LoadApplication(url);
        }

        [Test(Description = "Switch to gallery view in folder containing images")]
        public void showImages()
        {
            Page.Login.LogInToApp("2bZ8Hzoe");
            Page.Home.FolderUse(1);
            Page.Home.FolderUse(1);
            WaitAndClick("/html/body/div/div[1]/div/div[1]/ul/li[2]/button[2]/span");
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists(
                By.XPath("/html/body/div/div[1]/div/section/div/div/div[1]/div/div[1]/div[2]/img")));
        }

        [Test(Description = "Switch to gallery view in folder with no images")]
        public void dontShowImages()
        {
            Page.Login.LogInToApp("2bZ8Hzoe");
            Page.Home.FolderUse(1);
            Page.Home.FolderUse(2);
            WaitAndClick("/html/body/div/div[1]/div/div[1]/ul/li[2]/button[2]/span");
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists(
                By.ClassName("gallery-empty-label")));
        }

        [Test(Description = "Sort descending by name in folder containing files and folders")]
        public void sortByNameDesc()
        {
            Page.Login.LogInToApp("2bZ8Hzoe");
            Page.Home.FolderUse(1);
            Page.Home.FolderUse(3);
            WaitAndClick("/html/body/div/div[1]/div/section/div/div/div[2]/div/ul/li[4]/a"); //click 2
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/ul/li[1]/div/div/div[4]/span/span[2]")).Text.Equals("48.txt");
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/ul/li[1]/div/div/div[4]/span/span[2]")).Text.Equals("79.txt");
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.ClassName("dropdown-toggle")).Click(); //dropdown
            WaitAndClick("/html/body/div/div[1]/div/div[1]/ul/li[1]/div/div/div/ul/li[6]"); //click DESCENDIN
            driver.FindElement(By.ClassName("next")).Click(); //click Next
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/ul/li[1]/div/div/div[4]/span/span[2]")).Text.Equals("31.txt");
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/ul/li[32]/div/div/div[4]/span/span[2]")).Text.Equals("10.txt");
            System.Threading.Thread.Sleep(5000);
        }

        [TearDown]
        public void closeChrome()
        {
            driver.Quit();
        }

        public IWebDriver WaitAndClick(string xPath)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists(By.XPath(xPath)));
            driver.FindElement(By.XPath(xPath)).Click();
            System.Threading.Thread.Sleep(2000);
            return driver;
        }
    }
}
