using DataHub_Automation.ComponentHelper;
using DataHub_Automation.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace DataHub_Automation.Test_Cases.Navigation.Step_Definitions
{
    [Binding]
    public class MegaMenuSteps
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // XPath and selector constants
        private static class Locators
        {
            // Menu items
            public static By AboutMenu = By.XPath("//*[@id='headerAndMain']/header/div/nav/ul/li[1]");
            public static By PolicyAreasMenu = By.XPath("//*[@id='headerAndMain']/header/div/nav/ul/li[2]/a");
            public static By DashboardInventoryMeny = By.XPath("//*[@id='headerAndMain']/header/div/nav/ul/li[1]/a");
            public static By DataInventoryMeny = By.XPath("//*[@id='headerAndMain']/header/div/nav/ul/li[1]/a");

            // Dropdown menu
            public static readonly By AssetAssetTransportationManagement = By.XPath("//*[@id='basic-mega-nav-section-two']/div/div[2]/h3/a");
            public static readonly By OfficeGovernmentPolicy = By.XPath("//*[@id='basic-mega-nav-section-two']/div/div[1]/h3[1]/a");
            public static readonly By AcquisitionPolicy = By.XPath("//*[@id='basic-mega-nav-section-two']/div/div[1]/h3[2]/a");
            public static readonly By OfficeGOfficeofAcquisition = By.XPath("//*[@id='policy-area']/div[1]/div/div/span/p");


            // Dropdown options
            public static By DropdownOption => By.XPath("");

            // Page identifiers
            public static By PageIdentifier => By.XPath("//div[contains(@class");
            public static By AboutPageData => By.XPath("//*[@id='main-content']/div/div/section/div[2]/div/div/h2");

        }

        public MegaMenuSteps()
        {
            _driver = ObjectRepository.Driver;
            _wait = GenericHelper.GetWebdriverWait(TimeSpan.FromSeconds(10));
        }

        [When(@"the user clicks on the About menu item")]
        public void WhenTheUserClicksOnTheAboutMenuItem()
        {
            // Find and click the menu item
            ButtonHelper.ClickButton(Locators.AboutMenu);

            // Add a delay to allow page transition
            Thread.Sleep(2000);
        }

        [Then(@"the About Data Hub page should load properly")]
        public void ThenTheAboutDataHubPageShouldLoadProperly()
        {
            // Allow page to load
            Thread.Sleep(2000);

            // Verify page identifier is present
            Assert.IsTrue(GenericHelper.IsElementPresent(Locators.AboutPageData),
                $"The About page did not load properly");
        }



        [Then(@"the page title should contain ""(.*)""")]
        public void ThenThePageTitleShouldContain(string titleText)
        {
            string AboutDataHub = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div/section/div[2]/div/div/h2")).Text;
            Assert.AreEqual("Streamlining the process for your most essential data resources.", AboutDataHub);
        }

        [When(@"the user hovers over the ""(.*)"" menu item")]
        public void WhenTheUserHoversOverTheMenuItem(string menuItem)
        {
            // Find the menu element
            IWebElement menuElement = GenericHelper.GetElement(Locators.PolicyAreasMenu);

            // Create and perform hover action
            Actions action = new Actions(_driver);
            action.MoveToElement(menuElement).Perform();

            // Small delay to ensure hover state is recognized
            Thread.Sleep(1000);
        }

        [Then(@"the dropdown menu should appear")]
        public void ThenTheDropdownMenuShouldAppear()
        {
            // Allow dropdown to appear
            Thread.Sleep(1000);

            // Verify dropdown is displayed
            Assert.IsTrue(GenericHelper.IsElementPresent(Locators.AssetAssetTransportationManagement),
                "Dropdown menu did not appear");
        }



        [When(@"the user hovers over the Policy Areas Menu")]
        public void WhenTheUserHoversOverThePolicyAreasMenu()
        {
            try
            {
                // Get the element you want to hover over
                IWebElement element = ObjectRepository.Driver.FindElement(Locators.PolicyAreasMenu);

                // Create an instance of Actions class
                Actions action = new Actions(ObjectRepository.Driver);

                // Move to the element (hover)
                action.MoveToElement(element).Perform();

                // Add a small delay to ensure the hover effect is registered
                Thread.Sleep(500);

                // You can log the successful hover
                Console.WriteLine($"Successfully hovered over element: {Locators.PolicyAreasMenu}");
            }
            catch (Exception ex)
            {
                // Log and handle any exceptions
                Console.WriteLine($"Failed to hover over element: {Locators.PolicyAreasMenu}. Error: {ex.Message}");
                throw;
            }
        }


        [Then(@"the dropdown should contain  ""([^""]*)"" options")]
        public void ThenTheDropdownShouldContainOptions(string p0)
        {
            string AssetAssetTransportationManagement = GenericHelper.GetElement(Locators.AssetAssetTransportationManagement).Text;
            Assert.AreEqual("Asset & Transportation Management", AssetAssetTransportationManagement);
        }

        [Then(@"the dropdown countains ""([^""]*)"" option")]
        public void ThenTheDropdownCountainsOption(string p0)
        {
            string OfficeGovernmentPolicy = GenericHelper.GetElement(Locators.OfficeGovernmentPolicy).Text;
            Assert.AreEqual("Office of Government-wide Policy", OfficeGovernmentPolicy);
        }

        [When(@"the user clicks on ""([^""]*)"" option")]
        public void WhenTheUserClicksOnOption(string p0)
        {
            ButtonHelper.ClickButton(Locators.AcquisitionPolicy);
        }

        [Then(@"the ""([^""]*)"" page should load properly")]
        public void ThenThePageShouldLoadProperly(string p0)
        {
            string OfficeGOfficeofAcquisition = GenericHelper.GetElement(Locators.OfficeGOfficeofAcquisition).Text;
            Assert.AreEqual("Office of Acquisition Policy", OfficeGOfficeofAcquisition);

        }


    }
}