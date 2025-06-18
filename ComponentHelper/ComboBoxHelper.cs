using System;
using System.Collections.Generic;
using System.Linq;
using DataHub_Automation.Settings;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using OpenQA.Selenium.Support.UI;



namespace DataHub_Automation.ComponentHelper
{
    public class ComboBoxHelper
    {
        private static SelectElement select;


        private static WebDriverWait GetWebDriverWait(IWebDriver driver, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout)
            {
                PollingInterval = TimeSpan.FromMilliseconds(250)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait;
        }


        public static void SelectElement(By locator, int index)
        {
            select = new SelectElement(GenericHelper.GetElement(locator));
            select.SelectByIndex(index);
        }

        public static void SelectElement(By locator, string visibletext)
        {
            select = new SelectElement(GenericHelper.GetElement(locator));
            select.SelectByText(visibletext);
        }

        public static void SelectElementByValue(By locator, string valueTexts)
        {
            select = new SelectElement(GenericHelper.GetElement(locator));
            select.SelectByValue(valueTexts);
        }

        public static IList<string> GetAllItem(By locator)
        {
            select = new SelectElement(GenericHelper.GetElement(locator));
            return select.Options.Select((x) => x.Text).ToList();
        }


        public static void SelectElement(IWebElement element, string visibletext)
        {
            select = new SelectElement(element);
            select.SelectByValue(visibletext);
        }

        // Select by visible text
        public void SelectByText(IWebElement comboBoxElement, string text)
        {
            var selectElement = new SelectElement(comboBoxElement);
            selectElement.SelectByText(text);
        }
        // Select by value
        public void SelectByValue(IWebElement comboBoxElement, string value)
        {
            var selectElement = new SelectElement(comboBoxElement);
            selectElement.SelectByValue(value);
        }
        // Select by index
        public void SelectByIndex(IWebElement comboBoxElement, int index)
        {
            var selectElement = new SelectElement(comboBoxElement);
            selectElement.SelectByIndex(index);
        }
        // Get selected option text
        public string GetSelectedOptionText(IWebElement comboBoxElement)
        {
            var selectElement = new SelectElement(comboBoxElement);
            return selectElement.SelectedOption.Text;
        }
        // Get all available options
        public List<string> GetAllOptions(IWebElement comboBoxElement)
        {
            var selectElement = new SelectElement(comboBoxElement);
            return selectElement.Options.Select(option => option.Text).ToList();
        }
    }

}
