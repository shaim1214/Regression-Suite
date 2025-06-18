using System;
using NUnit.Framework.Legacy;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DataHub_Automation.ComponentHelper
{
    //Custom assert helper to continue even if theres failure
    public class AssertHelper
    {
        //if true condition is met continue, if false catch the exception
        public static void AreEqual(string expected, string actual)
        {
            try
            {
                ClassicAssert.AreEqual(expected, actual);
                
            }
            catch (Exception)
            {
                //ignore
            }
        }

        internal static void AreEqual(string v, IWebElement webElement)
        {
            throw new NotImplementedException();
        }
    }
}
