using System;
using System.Collections.Generic;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace EgnyteTest.WrapperFactory
{
    class BrowserFactory
    {
        public string path = ConfigurationManager.AppSettings["DataFolder"];
        private static readonly Dictionary<string, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();
        private static IWebDriver driver;

        public static IWebDriver Driver
        {
            get
            {
                if(driver == null)                
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");                                    
                return driver;
            }
            private set
            {
                driver = value;
            }
        }

        public static void InitBrowser(string browserName)
        {
            if (driver == null)
            {
                switch (browserName)
                {
                    case "Firefox":
                        Driver = new FirefoxDriver(ConfigurationManager.AppSettings["DataFolder"]);
                        Drivers.Add("Firefox", Driver);
                        break;

                    case "IE":
                        Driver = new InternetExplorerDriver(ConfigurationManager.AppSettings["DataFolder"]);
                        Drivers.Add("IE", Driver);
                        break;

                    case "Chrome":
                        {
                            ChromeOptions chromeOptions = new ChromeOptions();
                            chromeOptions.AddUserProfilePreference("download.default_directory", ConfigurationManager.AppSettings["DownloadFolder"]);
                            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
                            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                            
                            Driver = new ChromeDriver(ConfigurationManager.AppSettings["DataFolder"], chromeOptions);
                            Drivers.Add("Chrome", Driver);
                            break;
                        }                        
                }
            }
        }

        public static void LoadApplication(string url)
        {
            Driver.Url = url;
            Driver.Manage().Window.Maximize();
            System.Threading.Thread.Sleep(2000);
        }

        public static string GetUrl()
        {
            return Driver.Url;
        }

        public static void CloseAllDrivers()
        {
            foreach (var key in Drivers.Keys)
            {
                Drivers[key].Close();
                Drivers[key].Quit();
            }
        }
    }
}
