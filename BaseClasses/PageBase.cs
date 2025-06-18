using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using DataHub_Automation.ComponentHelper;

namespace DataHub_Automation.BaseClasses
{
    public class PageBase
    {

        public PageBase(IWebDriver _driver)
        {
            PageFactory.InitElements(_driver, this);
        }


        public void NavigateToHomePage()
        {
            //HomeLink.Click();
        }
      
    }
}

