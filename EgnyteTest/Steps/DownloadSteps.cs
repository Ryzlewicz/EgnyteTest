using EgnyteTest.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using TechTalk.SpecFlow;

namespace EgnyteTest.Steps
{
    [Binding]
    public class DownloadSteps
    {
        [Then(@"Downloaded file is correct")]
        public void ThenDownloadedFileIsCorrect()
        {
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
                    throw new BindingException("Download directory is empty.");
                }
            }
            else
            {
                throw new BindingException("Directory or folder does not exist.");
            }
        }

        [Then(@"Downloaded file2 is correct")]
        public void ThenDownloadedFile2IsCorrect()
        {
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
                    throw new BindingException("Download directory is empty.");
                }
            }
            else
            {
                throw new BindingException("Directory or folder does not exist.");
            }
        }

        [When(@"I click Download 'Folder|Selected' button")]
        public void ButtonClick(string name)
        {
            if( string.Equals("Folder", name))
                Page.Home.DownloadFolder();
            else if (string.Equals("Selected", name))
                Page.Home.DownloadSelected();
        }

        [When(@"I check (.*) checkbox")]
        public void CheckboxCheck(int numer)
        {
            Page.Home.CheckboxUse(1);
        }

        [When(@"Go to folder (.*)")]
        public void GoToFolder(int numer)
        {
            Page.Home.FolderUse(numer);
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
            }
            else
            {
                File.Delete(downloadPath + "\\" + userName + ".zip");
                throw new BindingException("Downloaded file is not valid.");
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

        public string downloadPath = ConfigurationManager.AppSettings["DownloadFolder"];
        public string dataPath = ConfigurationManager.AppSettings["DataFolder"];
        public string originalExtractFolder = @"C:\EgnyteTest\EgnyteTemp\Original";
        public string newExtractFolder = @"C:\EgnyteTest\EgnyteTemp\New";
        public string userName = "Aleksander Ryzlewski";
    }

}
