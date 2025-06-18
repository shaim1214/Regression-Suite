using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using DataHub_Automation.Configuration;
using DataHub_Automation.CustomException;
using DataHub_Automation.Settings;
using DataHub_Automation.ComponentHelper;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace DataHub_Automation.BaseClasses
{
    [Binding]
    public class BaseClass
    {
        private static FirefoxOptions GetFirefoxOptions() => new FirefoxOptions();
        private static ChromeOptions GetChromeOptions()
        {
            var option = new ChromeOptions();
            option.AddArgument("start-maximized");
            //option.AddArgument("enable-download-warning-improvements");
            //option.AddArgument("--disable-features=InsecureDownloadWarnings");
            //option.AddArgument("--headless");
            //option.AddArguments("--window-size=1920,1080");
            return option;
        }
        private static EdgeOptions GetEdgeOptions()
        {
            var option = new EdgeOptions();
            option.AddArgument("start-maximized");
            //option.AddArgument("--headless");
            //option.AddArguments("--window-size=1920,1080");
            return option;
        }
        private static FirefoxDriver GetFirefoxDriver() => new FirefoxDriver(GetFirefoxOptions());
        private static EdgeDriver GetEdgeDriver() => new EdgeDriver(GetEdgeOptions());
        private static ChromeDriver GetChromeDriver() => new ChromeDriver(GetChromeOptions());
        [BeforeScenario]
        [OneTimeSetUp]
        public static void InitWebdriver()
        {
            ObjectRepository.Config = new AppConfigReader();
            switch (ObjectRepository.Config.GetBrowser())
            {
                case BrowserType.Firefox:
                    ObjectRepository.Driver = GetFirefoxDriver();
                    break;
                case BrowserType.Chrome:
                    ObjectRepository.Driver = GetChromeDriver();
                    break;
                case BrowserType.Edge:
                    ObjectRepository.Driver = GetEdgeDriver();
                    break;
                default:
                    throw new NoSutiableDriverFound("Driver Not Found : " + ObjectRepository.Config.GetBrowser().ToString());
            }
            ObjectRepository.Driver.Manage().Timeouts().PageLoad = System.TimeSpan.FromSeconds(ObjectRepository.Config.GetPageLoadTimeOut());
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(ObjectRepository.Config.GetElementLoadTimeOut());
            BrowserHelper.BrowserMaximize();
        }
        [AfterScenario]
        public static void TearDown()
        {
            if (ObjectRepository.Driver != null)
            {
                ObjectRepository.Driver.Close();
                ObjectRepository.Driver.Quit();
            }
        }
    }
}