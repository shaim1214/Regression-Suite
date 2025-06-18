using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using DataHub_Automation.Settings;
using System.Threading;
using System.IO;


namespace DataHub_Automation.ComponentHelper
{
    public class GenericHelper
    {
        private static Func<IWebDriver, bool> WaitForWebElementFunc(By locator)
        {
            return (x) =>
            {
                if (x.FindElements(locator).Count == 1)
                    return true;
                return false;
            };
        }



        private static Func<IWebDriver, IWebElement> WaitForWebElementInPageFunc(By locator)
        {
            return (x) =>
            {
                if (x.FindElements(locator).Count == 1)
                    return x.FindElement(locator);
                return null;
            };
        }
        public static WebDriverWait GetWebdriverWait(TimeSpan timeout)
        {
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            WebDriverWait wait = new WebDriverWait(ObjectRepository.Driver, timeout)
            {
                PollingInterval = TimeSpan.FromMilliseconds(500),
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait;
        }
        public static bool IsElementPresent(By locator)
        {
            try
            {
                return ObjectRepository.Driver.FindElements(locator).Count == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool IsElementNotPresent(By locator)
        {
            try
            {
                // Use FindElements to check if the element exists
                return ObjectRepository.Driver.FindElements(locator).Count == 0;
            }
            catch (Exception ex)
            {
                // Optionally log the exception for debugging purposes
                Console.WriteLine($"Exception in IsElementNotPresent: {ex.Message}");
                return true; // If an exception occurs, assume the element is not present
            }
        }

        public static IWebElement GetElement(By locator)
        {
            if (IsElementPresent(locator))
                return ObjectRepository.Driver.FindElement(locator);
            else
                throw new NoSuchElementException("Element Not Found : " + locator.ToString());
        }


        public static void TakeScreenShot(string filename = "Screen")
        {
            var screen = ((ITakesScreenshot)ObjectRepository.Driver).GetScreenshot();
            if (filename.Equals("Screen"))
            {
                filename = "Screen_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png";
            }
            using (var stream = new MemoryStream(screen.AsByteArray))
            using (var fileStream = new FileStream(filename, FileMode.Create))
            {
                stream.CopyTo(fileStream);
            }
        }

        public static bool WaitForWebElement(By locator, TimeSpan timeout)
        {
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            var wait = GetWebdriverWait(timeout);
            var flag = wait.Until(WaitForWebElementFunc(locator));
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(ObjectRepository.Config.GetElementLoadTimeOut());
            return flag;
        }

        public static IWebElement WaitForWebElementInPage(By locator, TimeSpan timeout)
        {
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));
            var wait = GetWebdriverWait(timeout);
            var flag = wait.Until(WaitForWebElementInPageFunc(locator));
            ObjectRepository.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(ObjectRepository.Config.GetElementLoadTimeOut());
            return flag;
        }

        //Method to verify elements are present on a page given an string array of XPaths
        public static void Verification(String[] InputArray)
        {
            String[] XPath_Array = InputArray;

            //Generic Loop used to verify elements on a page given an array of XPaths
            for (int i = 0; i < XPath_Array.Length; i++)
            {
                GenericHelper.IsElementPresent(By.XPath(XPath_Array[i]));
                Thread.Sleep(1000);
            }
        }

        //Method used to locate objects on any DataHub Listing Grids
        public static void Locate(String XPath, String Text, String Listing_Grid_Element)
        {
            var DataHub_XPath = XPath;
            var DataHub_Object = Text;
            var Listing_Grid_Index = Listing_Grid_Element;

            //Types the desired object name into the listing grid
            TextBoxHelper.TypeInTextBox(By.XPath(DataHub_XPath), DataHub_Object);
            Thread.Sleep(9000);

            //Clicks on the first element in the listing grid
            ButtonHelper.ClickButton(By.XPath(Listing_Grid_Index));
            Thread.Sleep(2000);
        }


        public static int RandomGenerate()
         {
            Random random = new Random();
            var n = random.Next(1000, 2000);
            return n;
        }
        

       
    }
}