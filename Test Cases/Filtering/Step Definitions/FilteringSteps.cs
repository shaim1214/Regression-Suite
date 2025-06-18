using DataHub_Automation.ComponentHelper;
using DataHub_Automation.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using TechTalk.SpecFlow;

namespace DataHub_Automation.Test_Cases.Filtering.Step_Definitions
{
    [Binding]
    public class FilteringSteps
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private int _initialRowCount;

        // XPath and selector constants
        private static class Locators
        {
            // Dashboard Inventory link
            public static readonly By DashboardInventoryLink = By.Id("dashboard-inventory-link");
            public static readonly By ExploreDashboardInventoryButton = By.XPath("//*[@id='main-content']/div[3]/div/div/div[1]/div/div[4]/button/a");

            // Data Inventory link
            public static readonly By DataInventoryLink = By.XPath("//*[@id='headerAndMain']/header/div/nav/ul/li[4]/a");

            // Filters
            public static readonly By DashboardFilter = By.Id("dashboard-combobox");
            public static readonly By DashboardTypeInput = By.Id("dashboard-combobox");
            public static readonly By OfficeFilter = By.Id("office-combobox");
            public static readonly By DateFilter = By.Id("date-filter-dropdown");

            // Filter options
            public static By DashboardOption(string option) => By.XPath($"//ul[contains(@class, 'dropdown-options')]/li[contains(text(), '{option}')]");
            public static By ITDashboardOption1 = By.Id("dashboard-combobox--list--option-0");
            public static By ITDashboardOption3 = By.Id("dashboard-combobox--list--option-3");
            public static readonly By AutocompleteDropdown = By.XPath("//*[@id='dashboard-combobox--list--option-0']");
            public static readonly By AutocompleteSuggestions = By.XPath("//div[contains(@class, 'autocomplete-dropdown')]/div");

            // Radio buttons
            public static readonly By PrivateOnlyRadioButton = By.XPath("//*[@id='filter-accordian']/div/div/div[1]/div[3]/div/div/div/div[2]/label");
            public static readonly By PublicOnlyRadioButton = By.XPath("//*[@id='filter-accordian']/div/div/div[1]/div[3]/div/div/div/div[3]/label");

            // Buttons
            public static readonly By ApplyFiltersButton = By.XPath("//*[@id='filter-accordian']/div/div/div[2]/div[1]/button");
            public static readonly By ClearFiltersButton = By.XPath("//*[@id='filter-accordian']/div/div/div[2]/div[2]/button");

            
            // Filter indicators
            public static By PublicIndicator = By.XPath("//span[contains(text(), 'Public')]");
            public static By FeaturedIndicator = By.XPath("//span[contains(text(), 'Featured')]");

            // Clear filter buttons
            public static By FilterClearButton(string filterName) => By.XPath("//*[@id='filter-accordian']/div/div/div[1]/div[1]/div/div/span[1]/button");

            // Show Data Checkboxes
            public static readonly By RawFilter = By.XPath("//*[@id='filter-accordian']/div/div/div[2]/div[4]/div/div/div[2]/div[1]/div/label");
            public static readonly By AggregatedFilter = By.XPath("//*[@id='filter-accordian']/div/div/div[2]/div[4]/div/div/div[2]/div[2]/div/label");


        }

        public FilteringSteps()
        {
            _driver = ObjectRepository.Driver;
            _wait = GenericHelper.GetWebdriverWait(TimeSpan.FromSeconds(10));
        }

        [Given(@"user navigates to Dashboard Inventory page")]
        public void GivenUserNavigatesToDashboardInventoryPage()
        {
            ButtonHelper.ClickButton(Locators.ExploreDashboardInventoryButton);
            Thread.Sleep(3000);

        }

        [Given(@"user navigates to Data Inventory page")]
        public void GivenUserNavigatesToDataInventoryPage()
        {
            ButtonHelper.ClickButton(Locators.DataInventoryLink);
        }


        [When(@"the user types ""(.*)"" in the dashboard type filter")]
        public void WhenTheUserTypesInTheDashboardTypeFilter(string filterText)
        {
            // Click on the dashboard filter first
            ButtonHelper.ClickButton(Locators.DashboardFilter);
            Thread.Sleep(1000);

            // Type the text
            TextBoxHelper.TypeInTextBox(Locators.DashboardTypeInput, filterText);
            Thread.Sleep(1500);
        }

        [Then(@"the autocomplete suggestions should appear")]
        public void ThenTheAutocompleteSuggestionsShouldAppear()
        {
            // Allow time for suggestions to appear
            Thread.Sleep(1500);

            // Verify suggestions are present
            Assert.IsTrue(GenericHelper.IsElementPresent(Locators.AutocompleteDropdown),
                "Autocomplete suggestions did not appear");
        }

        [When(@"the user selects ""(.*)"" from suggestions")]
        public void WhenTheUserSelectsFromSuggestions(string suggestionText)
        {
            if (suggestionText == "IT Dashboard")
            {
                // Select IT Dashboard option
                ButtonHelper.ClickButton(Locators.ITDashboardOption1);
            }
            else
            {
                // For other options, find by text
                ButtonHelper.ClickButton(By.XPath($"//div[contains(@class, 'autocomplete-dropdown')]/div[contains(text(), '{suggestionText}')]"));
            }

            Thread.Sleep(1000);
        }

        [When(@"the user selects ""(.*)"" checkbox filter")]
        public void WhenTheUserSelectsCheckboxFilter(string checkboxLabel)
        {
            if (checkboxLabel == "Public")
            {
                ButtonHelper.ClickButton(Locators.PublicOnlyRadioButton);
            }
            else if (checkboxLabel == "Private")
            {
                ButtonHelper.ClickButton(Locators.PrivateOnlyRadioButton);
            }
            else if (checkboxLabel == "Featured")
            {
                // Assuming there's a featured checkbox - add the locator if it exists
                ButtonHelper.ClickButton(By.XPath($"//label[contains(text(), '{checkboxLabel}')]"));
            }

            Thread.Sleep(1000);
        }

        [When(@"the user unchecks ""([^""]*)"" checkbox filter")]
        public void WhenTheUserUnchecksCheckboxFilter(string raw)
        {
            Thread.Sleep(1000);
            ButtonHelper.ClickButton(Locators.RawFilter);
        }

        [Then(@"unchecks ""([^""]*)"" checkbox filter")]
        public void ThenUnchecksCheckboxFilter(string aggregated)
        {
            ButtonHelper.ClickButton(Locators.AggregatedFilter);
        }

        [Then(@"verifies data displays correctly")]
        public void ThenVerifiesDataDisplaysCorrectly()
        {
            //Verify the returned data 
            string CityPair = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr[1]/td[1]/a")).Text;
            Assert.AreEqual("CityPair", CityPair);
            string FederalAutomotive = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr[2]/td[1]/a")).Text;
            Assert.AreEqual("Federal Automotive Statistical Tool (FAST) Fleet Data/ Federal Fleet Report (FFR)", FederalAutomotive);
            string Fedrooms = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr[3]/td[1]/a")).Text;
            Assert.AreEqual("Fedrooms", Fedrooms);
        }

        [Then(@"verifies no data is returned")]
        public void ThenVerifiesNoDataIsReturned()
        {
            string Empty = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/div")).Text;
            Assert.AreEqual("", Empty);
        }


        [When(@"the user selects ""([^""]*)"" from ""([^""]*)"" filter")]
        public void WhenTheUserSelectsFromFilter(string external, string p1)
        {
            // Select dashboard dropdown
            ButtonHelper.ClickButton(Locators.OfficeFilter);
            ButtonHelper.ClickButton(By.XPath("//*[@id='office-combobox--list--option-5']"));

        }


        [Then(@"only public items are displayed")]
        public void ThenOnlyPublicItemsAreDisplayed()
        {
            // Allow table to update
            Thread.Sleep(2000);

            // Check for the public indicator on the first row
            Assert.IsTrue(GenericHelper.IsElementPresent(Locators.PublicIndicator),
                "Public indicator not found for the first row");
        }

        [Then(@"only public and featured items are displayed")]
        public void ThenOnlyPublicAndFeaturedItemsAreDisplayed()
        {
            //Verify the returned data 
            string PersonalPropertyDashboard = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr[1]/td[1]/a")).Text;
            Assert.AreEqual("Personal Property Dashboard", PersonalPropertyDashboard);
            string BusinessTravel = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr[2]/td[1]/a")).Text;
            Assert.AreEqual("Business Travel and Relocation Dashboard", BusinessTravel);
            string FederalFleet = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr[3]/td[1]/a")).Text;
            Assert.AreEqual("Federal Fleet Open Data Visualization", FederalFleet);
        }

        [When(@"the user clicks on ""(.*)"" button")]
        public void WhenTheUserClicksOnButton(string buttonText)
        {
            if (buttonText == "Clear Filters")
            {
                ButtonHelper.ClickButton(Locators.ClearFiltersButton);
            }
            else if (buttonText == "Apply Filters")
            {
                ButtonHelper.ClickButton(Locators.ApplyFiltersButton);
            }
            else
            {
                // Generic button handling
                ButtonHelper.ClickButton(By.XPath($"//button[contains(text(), '{buttonText}')]"));
            }

            Thread.Sleep(2000);
        }

        [Then(@"all filters are reset")]
        public void ThenAllFiltersAreReset()
        {
            // Check dashboard filter
            Assert.AreEqual(string.Empty, GenericHelper.GetElement(Locators.DashboardTypeInput).GetAttribute("value"),
                "Dashboard filter was not reset");

            // Allow time for UI to update
            Thread.Sleep(1000);
        }

        [Then(@"the original unfiltered data is displayed")]
        public void ThenTheOriginalUnfilteredDataIsDisplayed()
        {
            //Verify the returned data 
            string TurnstileIncident = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr[1]/td[1]/a")).Text;
            Assert.AreEqual("Turnstile Incident Report", TurnstileIncident);
            string DailyOccupancyTool = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr[2]/td[1]/a")).Text;
            Assert.AreEqual("Daily Occupancy Tool", DailyOccupancyTool);
            string TenantSatisfaction = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr[3]/td[1]/a")).Text;
            Assert.AreEqual("Tenant Satisfaction Survey (TSS)", TenantSatisfaction);
        }

        [When(@"the user clicks on the clear button for ""(.*)"" filter")]
        public void WhenTheUserClicksOnTheClearButtonForFilter(string filterName)
        {
            ButtonHelper.ClickButton(Locators.FilterClearButton(filterName));
            Thread.Sleep(1500);
        }

        [Then(@"the ""(.*)"" filter is reset")]
        public void ThenTheFilterIsReset(string filterName)
        {
            if (filterName == "Dashboard Type")
            {
                Assert.AreEqual(string.Empty, GenericHelper.GetElement(Locators.DashboardTypeInput).GetAttribute("value"),
                    "Dashboard Type filter was not reset");
            }

            Thread.Sleep(1000);
        }

        [Then(@"the data is filtered only by ""(.*)"" checkbox")]
        public void ThenTheDataIsFilteredOnlyByCheckbox(string checkboxLabel)
        {
            // Allow table to update
            Thread.Sleep(2000);

            if (checkboxLabel == "Public")
            {
                // Check for public indicator
                Assert.IsTrue(GenericHelper.IsElementPresent(Locators.PublicIndicator),
                    "Public indicator not found");
            }
        }

        [When(@"the user selects ""(.*)"" from dashboard type filter")]
        public void WhenTheUserSelectsFromDashboardTypeFilter(string option)
        {
            // Click dashboard filter
            ButtonHelper.ClickButton(Locators.DashboardFilter);
            Thread.Sleep(1000);

            if (option == "IT Dashboard")
            {
                ButtonHelper.ClickButton(Locators.ITDashboardOption3);
            }
            else
            {
                // For other options
                ButtonHelper.ClickButton(Locators.DashboardOption(option));
            }

            Thread.Sleep(1000);
        }

        [Then(@"only items matching all filter conditions are displayed")]
        public void ThenOnlyItemsMatchingAllFilterConditionsAreDisplayed()
        {
            // Allow table to update
            Thread.Sleep(2000);

            string GSAPerformanceBased = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr[1]/td[1]/a")).Text;
            Assert.AreEqual("GSA Performance Based Acquisition Utilization Tool", GSAPerformanceBased);
            string BusinEmployeeEngagement = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr[2]/td[1]/a")).Text;
            Assert.AreEqual("Employee Engagement Index (EEI) Scores", BusinEmployeeEngagement);
            string ProcurementAdmin = GenericHelper.GetElement(By.XPath("//*[@id='main-content']/div/div[2]/main/div[2]/div[2]/table/tbody/tr[3]/td[1]/a")).Text;
            Assert.AreEqual("Procurement Administrative Lead Time (PALT, a measure of timeliness)", ProcurementAdmin);

        }
    }
}