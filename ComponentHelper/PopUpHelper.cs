using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using DataHub_Automation.Settings;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace DataHub_Automation.ComponentHelper
{
    public class PopUpHelper
    {


        public static bool IsPopupPresent()
        {
            try
            {
                ObjectRepository.Driver.SwitchTo().Alert();
                Thread.Sleep(2000);
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            } // catch

        }

        public static string GetPopUpText()
        {
            if (!IsPopupPresent())
            
            
            Thread.Sleep(7000);
            return ObjectRepository.Driver.SwitchTo().Alert().Text;
        }

        public static void ClickOkOnPopup()
        {
            if (!IsPopupPresent())
                return;
            ObjectRepository.Driver.SwitchTo().Alert().Accept();
        }

        public static void ClickCancelOnPopup()
        {
            if (!IsPopupPresent())
                return;
            ObjectRepository.Driver.SwitchTo().Alert().Dismiss();
        }
    }
}
