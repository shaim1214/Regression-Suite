using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using DataHub_Automation.Interfaces;
//using DataHub_Automation.PageObject;
using DataHub_Automation.Configuration;

namespace DataHub_Automation.Settings
{
    public class ObjectRepository
    {
        public static IConfig Config { get; set; }

        public static IWebDriver Driver { get; set; }

        public static List<string> CostNames { get; set; } = new List<string>();
        //Still working on pageObjects
        //public static HomePage hPage;
        
        
    }
}
