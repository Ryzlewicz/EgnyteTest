using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.Events;
using NUnit.Framework;

namespace EgnyteTest
{
    public class EgnyteTests
    {        
        IWebDriver driver;

        public string downloadPath = @"C:\EgnyteTest\EgnyteTest\EgnyteTest\DownloadData";
        public string dataPath = @"C:\EgnyteTest\EgnyteTest\EgnyteTest\Data";
        public string originalExtractFolder = @"C:\EgnyteTest\EgnyteTemp\Original";
        public string newExtractFolder = @"C\EgnyteTest\EgnyteTemp\New";
        public string fileName = "Aleksander Ryzlewski";
        public string url = "https://qarecruitment.egnyte.com/fl/rCey0sCxMN";
                
        public bool CheckFile(string name)
        {
            string currentFile = downloadPath + "\\" + name + ".zip";
            if (File.Exists(currentFile))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ExtractFiles(string nameFile)
        {
            string currentFile = downloadPath + "\\" + fileName + ".zip";
            string originalFile = dataPath + "\\" + nameFile + ".zip";
            ZipFile.ExtractToDirectory(currentFile, newExtractFolder);
            ZipFile.ExtractToDirectory(originalFile, originalExtractFolder);
            VerifyFiles(newExtractFolder, originalExtractFolder);
        }

        public void VerifyFiles(string newExtractFolder, string originalExtractFolder)
        {
            string[] newFileEntries = Directory.GetFiles(newExtractFolder);
            List<string> listItemsName = new List<string>();
            for (int i = 0; i < newFileEntries.Length; i++)
            {
                string[] split = newFileEntries[i].Split('\\');
                listItemsName.Add(split.Last());
            }

            string[] originalFileEntries = Directory.GetFiles(originalExtractFolder);
            List<string> originalItemList = new List<string>();
            for (int i = 0; i < originalFileEntries.Length; i++)
            {
                string[] split = originalFileEntries[i].Split('\\');
                originalItemList.Add(split.Last());
            }

            var result = (originalItemList.Count == listItemsName.Count) && originalItemList.All(listItemsName.Contains);

            if (result)
            {
                File.Delete(downloadPath + "\\" + fileName + ".zip");
                Assert.Pass();
            }
            else
            {
                File.Delete(downloadPath + "\\" + fileName + ".zip");
                Assert.Fail("Downloaded file is not valid.");
            }
        }

        public void DeleteFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }   
        }

        [SetUp]
        public void startChrome()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            var downloadPath = "C:\\EgnyteTest\\EgnyteTest\\EgnyteTest\\DownloadData";
            chromeOptions.AddUserProfilePreference("download.default_directory", downloadPath);
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", true);

            driver = new ChromeDriver("C:\\EgnyteTest\\EgnyteTest\\EgnyteTest\\Chrome", chromeOptions);
            driver.Url = url;
            driver.Manage().Window.Maximize();
        }

        [Test(Description= "Check system behavior when valid password is entered")]
        public void validPassword()
        {
            driver.FindElement(By.Id("password")).SendKeys("2bZ8Hzoe");
            driver.FindElement(By.XPath("//*[@id=\"password_controls\"]/form/a")).Click();                       
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[@id=\"content\"]/div/header/div[2]/div/div/div[1]/button[2]"));
        }

        [Test(Description = "Check system behavior when invalid password is entered")]
        public void invalidPassword()
        {
            driver.FindElement(By.Id("password")).SendKeys("wRoNgPaSsWoRd");            
            driver.FindElement(By.XPath("//*[@id=\"password_controls\"]/form/a")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[@id=\"password_controls\"]/div")).Text.Equals("Incorrect password. Try again.");            
        }

        [Test(Description = "Check system behavior when empty password is entered")]
        public void emptyPassword()
        {            
            driver.FindElement(By.XPath("//*[@id=\"password_controls\"]/form/a")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[@id=\"password_controls\"]/form/a"));            
        }

        [Test(Description = "Switch to gallery view in folder containing images")]
        public void showImages()
        {
            driver.FindElement(By.Id("password")).SendKeys("2bZ8Hzoe");
            driver.FindElement(By.XPath("//*[@id=\"password_controls\"]/form/a")).Click();
            System.Threading.Thread.Sleep(2000);            
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/ul/li[1]/div/div/div[4]/span/span[2]")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/ul/li[1]/div/div/div[4]/span/span[2]")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/div[1]/ul/li[2]/button[2]/span")).Click();
            System.Threading.Thread.Sleep(4000);
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/div[1]/div/div[1]/div[2]/img"));
        }

        [Test(Description = "Switch to gallery view in folder with no images")]
        public void dontShowImages()
        {
            driver.Url = url;
            driver.Manage().Window.Maximize();
            driver.FindElement(By.Id("password")).SendKeys("2bZ8Hzoe");
            driver.FindElement(By.XPath("//*[@id=\"password_controls\"]/form/a")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/ul/li[1]/div/div/div[4]/span/span[2]")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/ul/li[2]/div/div/div[4]/span/span[2]")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/div[1]/ul/li[2]/button[2]/span")).Click();
            System.Threading.Thread.Sleep(2000);                       
            driver.FindElement(By.ClassName("gallery-empty-label"));
        }

        [Test(Description = "Sort descending by name in folder containing files and folders")]
        public void sortByNameDesc()
        {
            driver.FindElement(By.Id("password")).SendKeys("2bZ8Hzoe");
            driver.FindElement(By.XPath("//*[@id=\"password_controls\"]/form/a")).Click(); //click Continue
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/ul/li[1]/div/div/div[4]/span/span[2]")).Click(); //click Data Folder
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/ul/li[3]/div/div/div[4]/span/span[2]")).Click(); //click Data3
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/div[2]/div/ul/li[4]/a")).Click(); //click 2
            System.Threading.Thread.Sleep(2000);
            IWebElement element_1 = driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/ul/li[1]/div/div/div[4]/span/span[2]"));
            element_1.Text.Equals("48.txt");
            IWebElement element_2 = driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/ul/li[32]/div/div/div[4]/span/span[2]"));
            element_2.Text.Equals("79.txt");
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.ClassName("dropdown-toggle")).Click(); //dropdown
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/div[1]/ul/li[1]/div/div/div/ul/li[6]")).Click(); //click DESCENDING            
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.ClassName("next")).Click(); //click Next
            System.Threading.Thread.Sleep(2000);
            IWebElement element_3 = driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/ul/li[1]/div/div/div[4]/span/span[2]"));
            element_3.Text.Equals("31.txt");
            IWebElement element_4 = driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/ul/li[32]/div/div/div[4]/span/span[2]"));
            element_4.Text.Equals("10.txt");
        }

        [Test(Description = "Use Download Folder button in root folder")]
        public void downloadRootFolder()
        {
            driver.FindElement(By.Id("password")).SendKeys("2bZ8Hzoe");
            driver.FindElement(By.XPath("//*[@id=\"password_controls\"]/form/a")).Click(); //click Continue
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/header/div[2]/div/div/div[1]/button[4]")).Click(); //click Download Folder button
            System.Threading.Thread.Sleep(10000);

            if (Directory.Exists(downloadPath))
            {
                bool result = CheckFile(fileName);
                if (result)
                {
                    ExtractFiles(fileName);
                }
                else
                {
                    Assert.Fail("Download directory is empty.");
                }
            }
            else
            {
                Assert.Fail("Directory or folder does not exist.");
            }            
            System.Threading.Thread.Sleep(5000);
        }

        [Test(Description = "Use Download Selected button with one folder selected")]
        public void downloadOneSelectedFolder()
        {
            driver.FindElement(By.Id("password")).SendKeys("2bZ8Hzoe");
            driver.FindElement(By.XPath("//*[@id=\"password_controls\"]/form/a")).Click(); //click Continue
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/section/div/div/ul/li[1]/div/div/div[1]")).Click(); //check Data Folder checkbox
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/header/div[2]/div/div/div[1]/button[3]")).Click(); //click Download Selected button
            System.Threading.Thread.Sleep(10000);

            if (Directory.Exists(downloadPath))
            {
                bool result = CheckFile(fileName);
                if (result)
                {
                    ExtractFiles(fileName + "_2");
                }
                else
                {
                    Assert.Fail("Download directory is empty.");
                }
            }
            else
            {
                Assert.Fail("Directory or folder does not exist.");
            }
            System.Threading.Thread.Sleep(5000);

        }

        [Test(Description = "Use Download Selected button with selected folders and files")]
        public void downloadSelectedFilesAndFolder()
        {
            driver.FindElement(By.Id("password")).SendKeys("2bZ8Hzoe");
            driver.FindElement(By.XPath("//*[@id=\"password_controls\"]/form/a")).Click(); //click Continue
            System.Threading.Thread.Sleep(2000);
            IWebElement checkAll = driver.FindElement(By.XPath("/html/body/div/div[1]/div/div[1]/div[1]/div/div"));
            checkAll.Click();
            driver.FindElement(By.XPath("/html/body/div/div[1]/div/header/div[2]/div/div/div[1]/button[3]")).Click(); //click Download Selected button
            System.Threading.Thread.Sleep(10000);

            if (Directory.Exists(downloadPath))
            {
                bool result = CheckFile(fileName);
                if (result)
                {
                    ExtractFiles(fileName);
                }
                else
                {
                    Assert.Fail("Download directory is empty.");
                }
            }
            else
            {
                Assert.Fail("Directory or folder does not exist.");
            }
            //driver.FindElement(By.XPath("/html/body/div/div[1]/div/header/div[2]/div/div/div[1]/button[4]")).Click(); //click Download Folder button
            System.Threading.Thread.Sleep(10000);
        }

        [TearDown]
        public void closeChrome()
        {
            //DeleteFolder(downloadPath);
            DeleteFolder("C:\\EgnyteTest\\EgnyteTemp");
            DeleteFolder("D:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\IDE\\C\\EgnyteTest");
            driver.Close();
        }
                
    }
}
