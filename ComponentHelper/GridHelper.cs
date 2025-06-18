using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using DataHub_Automation.Settings;

namespace DataHub_Automation.ComponentHelper
{
    public class GridHelper
    {
        internal static string GetTableXpath(string locator, int row, int col)
        {
            return $"{locator}/div[2]/div[2]/div/div[3]/div[{row}]/div/div[{col}]";
        }

        private static IWebElement GetGridElement(string locator, int row, int col)
        {
            var xpath = GetTableXpath(locator, row, col);

            if (GenericHelper.IsElementPresent(By.XPath(xpath + "//a")))
            {
                return ObjectRepository.Driver.FindElement(By.XPath(xpath + "//a"));
            }
            else if (GenericHelper.IsElementPresent(By.XPath(xpath + "//input")))
            {
                return ObjectRepository.Driver.FindElement(By.XPath(xpath + "//input"));
            }
            else
            {
                return ObjectRepository.Driver.FindElement(By.XPath(xpath));
            }
        }

        public static string GetColumnValue(string @locator, int @row, int @col)
        {
            return GetGridElement(locator, row, col).Text;
        }

        public static IList<string> GetAllValues(string @locator)
        {
            List<string> list = new List<string>();

            var row = 1;
            var col = 1;

            while (GenericHelper.IsElementPresent(By.XPath(GetTableXpath(locator, row, col))))
            {
                while (GenericHelper.IsElementPresent(By.XPath(GetTableXpath(locator, row, col))))
                {
                    list.Add(ObjectRepository.Driver.FindElement(By.XPath(GetTableXpath(locator, row, col))).Text);
                    col++;
                }
                row++;
                col = 1;
            }
            return list;

        }

        public static void ClickButtonInGrid(string @locator, int @row, int @col)
        {
            GetGridElement(locator, row, col).Click();
        }

    }
}
