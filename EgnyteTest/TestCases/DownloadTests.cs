using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using EgnyteTest.WrapperFactory;
using EgnyteTest.PageObjects;
using System.Configuration;

namespace EgnyteTest.TestCases
{
    class DownloadTests
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

        [Test(Description = "Use Download Folder button in root folder")]
        public void downloadRootFolder()
        {
            Page.Login.LogInToApp("2bZ8Hzoe");
            Page.Home.DownloadFolder();            
            
            if (Directory.Exists(downloadPath))
            {
                bool result = CheckFile(userName);
                if (result)
                {
                    string newPath = ExtractFiles(downloadPath, newExtractFolder, userName);
                    string comparePath = ExtractFiles(dataPath, originalExtractFolder, userName);
                    VerifyFiles(newPath, comparePath);
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
        }

        [Test(Description = "Use Download Selected button with one folder selected")]
        public void downloadOneSelectedFolder()
        {
            Page.Login.LogInToApp("2bZ8Hzoe");
            Page.Home.CheckboxUse(1);
            Page.Home.DownloadSelected();

            if (Directory.Exists(downloadPath))
            {
                bool result = CheckFile(userName);
                if (result)
                {
                    string newPath = ExtractFiles(downloadPath, newExtractFolder, userName);
                    string comparePath = ExtractFiles(dataPath, originalExtractFolder, (userName + "_2"));
                    VerifyFiles(newPath, comparePath);
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
        }

        [Test(Description = "Use Download Selected button with one subfolder selected")]
        public void downloadOneSelectedSubFolder()
        {
            Page.Login.LogInToApp("2bZ8Hzoe");
            Page.Home.FolderUse(1); //click Data Folder
            Page.Home.CheckboxUse(0); //check all
            Page.Home.DownloadSelected(); //click Download Selected button
           
            if (Directory.Exists(downloadPath))
            {
                bool result = CheckFile(userName);
                if (result)
                {
                    string newPath = ExtractFiles(downloadPath, newExtractFolder, userName);
                    string comparePath = ExtractFiles(dataPath, originalExtractFolder, (userName + "_2"));
                    VerifyFiles(newPath, comparePath);
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
        }

        [Test(Description = "Use Download Selected button with selected folders and files")]
        public void downloadSelectedFilesAndFolder()
        {
            Page.Login.LogInToApp("2bZ8Hzoe");
            Page.Home.CheckboxUse(0); 
            Page.Home.DownloadSelected();
            
            if (Directory.Exists(downloadPath))
            {
                bool result = CheckFile(userName);
                if (result)
                {
                    string newPath = ExtractFiles(downloadPath, newExtractFolder, userName);
                    string comparePath = ExtractFiles(dataPath, originalExtractFolder, userName);
                    VerifyFiles(newPath, comparePath);
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
        }

        [TearDown]
        public void closeChrome()
        {
            DeleteFolder("C:\\EgnyteTest\\EgnyteTemp");
            BrowserFactory.CloseAllDrivers();            
        }

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

        public string ExtractFiles(string dirPath, string destinationPath, string nameFile)
        {
            string fileToExtract = dirPath + "\\" + nameFile + ".zip";
            ZipFile.ExtractToDirectory(fileToExtract, destinationPath);
            return destinationPath;
        }

        public void VerifyFiles(string newExtractFolder, string originalExtractFolder)
        {
            var originalItemList = ItemList(originalExtractFolder);
            var newItemList = ItemList(newExtractFolder);

            var result = (originalItemList.Count == newItemList.Count) && originalItemList.All(newItemList.Contains);

            if (result)
            {
                File.Delete(downloadPath + "\\" + userName + ".zip");
                Assert.Pass();
            }
            else
            {
                File.Delete(downloadPath + "\\" + userName + ".zip");
                Assert.Fail("Downloaded file is not valid.");
            }
        }

        public List<string> ItemList(string extractFolder)
        {
            string[] fileEntries = Directory.GetFiles(extractFolder);
            List<string> itemList = new List<string>();
            for (int i = 0; i < fileEntries.Length; i++)
            {
                string[] split = fileEntries[i].Split('\\');
                itemList.Add(split.Last());
            }
            return itemList;
        }

        public void DeleteFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
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
