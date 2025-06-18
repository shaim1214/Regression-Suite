using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace DataHub_Automation.ComponentHelper
{
    public class TextBoxHelper
    {
        private static IWebElement element;
        public static void TypeInTextBox(By locator, string text)
        {
            element = GenericHelper.GetElement(locator);
            element.SendKeys(text);
        }

        public static void ClearTextBox(By locator)
        {
            IWebElement element = GenericHelper.GetElement(locator);
            // Check if the element is not null before interacting with it
            if (element != null)
            {
                // Click to ensure the text box is in focus
                element.Click();
                // Clear the text by sending Ctrl + A (select all) and then Backspace
                element.SendKeys(Keys.Control + "a");
                element.SendKeys(Keys.Backspace);
            }
            else
            {
                Console.WriteLine($"Element with locator '{locator}' not found.");
                // You might consider throwing an exception here if appropriate for your use case
            }
        }
    }
}
