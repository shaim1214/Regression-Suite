﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace DataHub_Automation.ComponentHelper
{
    public class LinkHelper
    {
        private static IWebElement element;

        public static void ClickLink(By Locator)
        {
            element = GenericHelper.GetElement(Locator);
            element.Click();
        }
    }
}
