using DataHub_Automation.ComponentHelper;
using DataHub_Automation.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;

namespace DataHub_Automation.Test_Cases.Home_Page.Step_Definitions
{
    [Binding]
    public class HomePage_FindProgramsSteps
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // XPath and selector constants
        private static class Locators
        {
            public static readonly By HeaderText = By.XPath("//*[@id='main-content']/section/div/h1");
            public static readonly By FooterText = By.XPath("//*[@id='main-content']/div[2]/div[2]/div/div/h2");
            public static readonly By AgencyDropdown = By.Id("agency-select");
            public static readonly By AgencyFirstOption = By.Id("agency-select--list--option-0");
            public static readonly By DashboardInventoryTitle = By.XPath("//*[@id='main-content']/div[3]/div/div/div[1]/div/div[2]/h2");
            public static readonly By DataInventoryTitle = By.XPath("//*[@id='main-content']/div[3]/div/div/div[2]/div/div[2]/h2");
            public static readonly By ExploreDashboardInventoryButton = By.XPath("//*[@id='main-content']/div[3]/div/div/div[1]/div/div[4]/button/a");
            public static readonly By DashboardFilter = By.Id("dashboard-combobox");
            public static readonly By ITDashboardOption = By.Id("dashboard-combobox--list--option-35");
            public static readonly By OfficeFilter = By.Id("office-combobox");
            public static readonly By PrivateOnlyRadioButton = By.XPath("//*[@id='filter-accordian']/div/div/div[1]/div[3]/div/div/div/div[2]/label");
            public static readonly By PublicOnlyRadioButton = By.XPath("//*[@id='filter-accordian']/div/div/div[1]/div[3]/div/div/div/div[3]/label");
            public static readonly By ApplyFiltersButton = By.XPath("//*[@id='filter-accordian']/div/div/div[2]/div[1]/button");
            public static readonly By ApplyFiltersButton1 = By.XPath("//*[@id='filter-accordian']/div/div/div[3]/div[1]/button");
            public static readonly By ClearFiltersButton = By.XPath("//*[@id='filter-accordian']/div/div/div[2]/div[2]/button");
        }

        // Text constants for assertions
        private static class ExpectedText
        {
            public const string Header = "Office of Government-wide Policy\r\nData Hub";
            public const string Footer = "About The Office of Government-wide Policy (OGP)";
            public const string DashboardInventory = "Dashboard Inventory";
            public const string DataInventory = "Data Inventory";
            public const string ApplyFiltersButton = "Apply Filters";
            public const string ClearFiltersButton = "Clear Filters";
        }

        public HomePage_FindProgramsSteps()
        {
            _driver = ObjectRepository.Driver;
            _wait = GenericHelper.GetWebdriverWait(TimeSpan.FromSeconds(10));
        }

        [Given(@"the user is on the DataHub site")]
        public void GivenTheUserIsOnTheDataHubSite()
        {
            NavigationHelper.NavigateToUrl(ObjectRepository.Config.GetWebsite());
        }

        [When(@"the user verifies the header and footer are displayed")]
        public void WhenTheUserVerifiesTheHeaderAndFooterAreDisplayed()
        {
            // Verify header text
            string headerText = GenericHelper.GetElement(Locators.HeaderText).Text;
            Assert.AreEqual(ExpectedText.Header, headerText, "Header text does not match expected value");

            // Verify footer text
            string footerText = GenericHelper.GetElement(Locators.FooterText).Text;
            Assert.AreEqual(ExpectedText.Footer, footerText, "Footer text does not match expected value");
        }

        [When(@"the user filters data using Agency dropdown")]
        public void WhenTheUserFiltersDataUsingAgencyDropdown()
        {
            // Type into the agency dropdown
            TextBoxHelper.TypeInTextBox(Locators.AgencyDropdown, "Social Security Administration");

            // Select the first option from the dropdown
            ButtonHelper.ClickButton(Locators.AgencyFirstOption);
        }

        [When(@"Dashboard Inventory and Data Inventory are available")]
        public void WhenDashboardInventoryAndDataInventoryAreAvailable()
        {
            // Verify Dashboard Inventory title
            string dashboardInventory = GenericHelper.GetElement(Locators.DashboardInventoryTitle).Text;
            Assert.AreEqual(ExpectedText.DashboardInventory, dashboardInventory,
                "Dashboard Inventory title does not match expected value");

            // Verify Data Inventory title
            string dataInventory = GenericHelper.GetElement(Locators.DataInventoryTitle).Text;
            Assert.AreEqual(ExpectedText.DataInventory, dataInventory,
                "Data Inventory title does not match expected value");
        }

        [Then(@"user can click Explore Dashboard Inventory")]
        public void ThenUserCanClickExploreDashboardInventory()
        {
            ButtonHelper.ClickButton(Locators.ExploreDashboardInventoryButton);
        }

        [Then(@"verify the filters and buttons exist on the page")]
        public void ThenVerifyTheFiltersAndButtonsExistOnThePage()
        {
            // Verify filters exist
            Assert.IsTrue(GenericHelper.IsElementPresent(Locators.DashboardFilter),
                "Dashboard filter is not present");
            Assert.IsTrue(GenericHelper.IsElementPresent(Locators.OfficeFilter),
                "Office filter is not present");
            Assert.IsTrue(GenericHelper.IsElementPresent(Locators.PrivateOnlyRadioButton),
                "Private Only radio button is not present");
            Assert.IsTrue(GenericHelper.IsElementPresent(Locators.PublicOnlyRadioButton),
                "Public Only radio button is not present");

            // Verify button texts
            string applyFiltersText = GenericHelper.GetElement(Locators.ApplyFiltersButton).Text;
            Assert.AreEqual(ExpectedText.ApplyFiltersButton, applyFiltersText,
                "Apply Filters button text does not match expected value");

            string clearFiltersText = GenericHelper.GetElement(Locators.ClearFiltersButton).Text;
            Assert.AreEqual(ExpectedText.ClearFiltersButton, clearFiltersText,
                "Clear Filters button text does not match expected value");
        }

        [Then(@"the data reflects changes")]
        public void ThenTheDataReflectsChanges()
        {
            //Verify the returned data 
            string DashboardData = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr/td[1]/a")).Text;
            Assert.AreEqual("Acquisitions Workforce Chase Lists", DashboardData);
            string DescriptionData = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr/td[2]/div")).Text;
            Assert.AreEqual("Lists of individuals that need to achieve a metric that have yet complete.", DescriptionData);
            string OfficeData = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr/td[3]/div")).Text;
            Assert.AreEqual("MVA", OfficeData);
        }

        [When(@"clicks Apply Filters button")]
        public void WhenClicksApplyFiltersButton()
        {
            ButtonHelper.ClickButton(Locators.ApplyFiltersButton);
        }
        
        [When(@"clicks Apply Filters button (.*)")]
        public void WhenClicksApplyFiltersButton(int p0)
        {
            ButtonHelper.ClickButton(Locators.ApplyFiltersButton1);
        }
        [Then(@"clicks Apply Filters button (.*)")]
        public void ThenClicksApplyFiltersButton(int p0)
        {
            ButtonHelper.ClickButton(Locators.ApplyFiltersButton1);
        }


        [When(@"user selects ""([^""]*)"" and Private filters")]
        public void WhenUserSelectsAndPrivateFilters(string p0)
        {
            // Select dashboard dropdown
            ButtonHelper.ClickButton(Locators.DashboardFilter);
            ButtonHelper.ClickButton(By.XPath("//*[@id='dashboard-combobox--list--option-3']"));

            // Click Public radio button if needed
            ButtonHelper.ClickButton(Locators.PrivateOnlyRadioButton);
        }

        [Then(@"the ""([^""]*)"" info displays correctly")]
        public void ThenTheInfoDisplaysCorrectly(string p0)
        {
            //Verify the returned data 
            string ITDashboardData = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr/td[1]/a")).Text;
            Assert.AreEqual("IT Dashboard", ITDashboardData);
            string ITDashboardDescriptionData = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr/td[2]/div")).Text;
            Assert.AreEqual("Provides Federal agencies and the public with the ability to view details of Federal information technology (IT) investments online and to track their progress over time.", ITDashboardDescriptionData);
            string ITDashboardOfficeData = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr/td[3]/div")).Text;
            Assert.AreEqual("ME", ITDashboardOfficeData);
        }

        [Then(@"only the public data displays")]
        public void ThenOnlyThePublicDataDisplays()
        {
            //Verify the returned data 
            string PersonalPropertyDashboard = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr[1]/td[1]/a")).Text;
            Assert.AreEqual("Personal Property Dashboard", PersonalPropertyDashboard);
            string BusinessTravel = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr[2]/td[1]/a")).Text;
            Assert.AreEqual("Business Travel and Relocation Dashboard", BusinessTravel);
            string FederalFleet = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr[3]/td[1]/a")).Text;
            Assert.AreEqual("Federal Fleet Open Data Visualization", FederalFleet);
        }


    }

}