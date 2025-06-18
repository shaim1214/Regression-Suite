using DataHub_Automation.ComponentHelper;
using DataHub_Automation.Settings;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DataHub_Automation.ComplexUIHelper
{
    /// <summary>
    /// Extended UI Helper for complex UI interactions
    /// </summary>
    public static class ExtendedUIHelper
    {
        /// <summary>
        /// Performs a hover action on an element
        /// </summary>
        /// <param name="locator">Locator of the element to hover over</param>
        public static void HoverOverElement(By locator)
        {
            IWebElement element = GenericHelper.GetElement(locator);
            Actions action = new Actions(ObjectRepository.Driver);
            action.MoveToElement(element).Perform();

            // Allow time for hover state
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Selects an option from an autocomplete dropdown after typing
        /// </summary>
        /// <param name="inputLocator">Locator of the input field</param>
        /// <param name="searchText">Text to type in the field</param>
        /// <param name="optionLocator">Locator of the option to select</param>
        public static void SelectFromAutocomplete(By inputLocator, string searchText, By optionLocator)
        {
            // Click input field
            ButtonHelper.ClickButton(inputLocator);

            // Type search text
            TextBoxHelper.TypeInTextBox(inputLocator, searchText);
            Thread.Sleep(1500);

            // Select option
            ButtonHelper.ClickButton(optionLocator);
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Verifies if a specific text exists in a table column
        /// </summary>
        /// <param name="rowLocator">Locator for table rows</param>
        /// <param name="columnXPath">XPath to find the column within each row</param>
        /// <param name="expectedText">Text to look for</param>
        /// <returns>True if text is found in any row's column</returns>
        public static bool VerifyTextInTableColumn(By rowLocator, string columnXPath, string expectedText)
        {
            IReadOnlyCollection<IWebElement> rows = ObjectRepository.Driver.FindElements(rowLocator);

            foreach (var row in rows)
            {
                try
                {
                    string cellText = row.FindElement(By.XPath(columnXPath)).Text;
                    if (cellText.Contains(expectedText))
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    // Skip rows that don't have this column
                    continue;
                }
            }

            return false;
        }

        /// <summary>
        /// Waits for filters to be applied and table to update
        /// </summary>
        /// <param name="previousRowCount">Previous row count for comparison</param>
        /// <param name="tableRowsLocator">Locator for table rows</param>
        /// <param name="maxWaitTimeInSeconds">Maximum wait time in seconds</param>
        /// <returns>True if the table was updated</returns>
        public static bool WaitForFilterResults(int previousRowCount, By tableRowsLocator, int maxWaitTimeInSeconds = 10)
        {
            DateTime endTime = DateTime.Now.AddSeconds(maxWaitTimeInSeconds);

            while (DateTime.Now < endTime)
            {
                Thread.Sleep(500);
                int currentRowCount = ObjectRepository.Driver.FindElements(tableRowsLocator).Count;

                // If row count changed or a loading indicator disappeared, consider the update complete
                if (currentRowCount != previousRowCount)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Clears all filters using the clear filters button and verifies reset
        /// </summary>
        /// <param name="clearFiltersButtonLocator">Locator for clear filters button</param>
        /// <param name="filterInputLocators">List of filter input locators to verify</param>
        /// <returns>True if all filters were cleared successfully</returns>
        public static bool ClearAllFilters(By clearFiltersButtonLocator, List<By> filterInputLocators)
        {
            // Click clear filters button
            ButtonHelper.ClickButton(clearFiltersButtonLocator);
            Thread.Sleep(1500);

            // Verify all filters are cleared
            foreach (var locator in filterInputLocators)
            {
                try
                {
                    string value = GenericHelper.GetElement(locator).GetAttribute("value");
                    if (!string.IsNullOrEmpty(value))
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    // If element isn't found or accessible, continue checking others
                    continue;
                }
            }

            return true;
        }

        /// <summary>
        /// Handles complex dropdown selection where the dropdown requires clicking, typing, and selecting
        /// </summary>
        /// <param name="dropdownLocator">Locator for the dropdown trigger</param>
        /// <param name="searchText">Text to type for filtering options</param>
        /// <param name="optionText">Option text to select</param>
        /// <param name="optionLocator">Optional explicit locator for the option</param>
        public static void HandleComplexDropdown(By dropdownLocator, string searchText, string optionText, By optionLocator = null)
        {
            // Click on dropdown
            ButtonHelper.ClickButton(dropdownLocator);
            Thread.Sleep(1000);

            // Type search text if provided
            if (!string.IsNullOrEmpty(searchText))
            {
                TextBoxHelper.TypeInTextBox(dropdownLocator, searchText);
                Thread.Sleep(1500);
            }

            // Select option
            if (optionLocator != null)
            {
                ButtonHelper.ClickButton(optionLocator);
            }
            else
            {
                ButtonHelper.ClickButton(By.XPath($"//li[contains(text(), '{optionText}')] | //div[contains(text(), '{optionText}')]"));
            }

            Thread.Sleep(1000);
        }

        /// <summary>
        /// Validates that filtered results meet multiple criteria
        /// </summary>
        /// <param name="tableRowsLocator">Locator for table rows</param>
        /// <param name="criteria">Dictionary of XPath expressions and expected values</param>
        /// <returns>True if all rows meet all criteria</returns>
        public static bool ValidateFilteredResults(By tableRowsLocator, Dictionary<string, string> criteria)
        {
            IReadOnlyCollection<IWebElement> rows = ObjectRepository.Driver.FindElements(tableRowsLocator);
            if (rows.Count == 0)
            {
                return false;
            }

            foreach (var row in rows)
            {
                foreach (var criterion in criteria)
                {
                    try
                    {
                        string actualValue = row.FindElement(By.XPath(criterion.Key)).Text;
                        if (!actualValue.Contains(criterion.Value))
                        {
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        // If we can't find the element, the criterion isn't met
                        return false;
                    }
                }
            }

            return true;
        }
    }
}