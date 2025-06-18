using OpenQA.Selenium;
using System.Collections.Generic;

public class HomePage
{
    private readonly IWebDriver driver;
    public HomePage(IWebDriver driver)
    {
        this.driver = driver;
    }
    // Example locators for text elements on the homepage
    private IWebElement HeaderText => driver.FindElement(By.XPath("/html/body/header"));
    private IWebElement WelcomeText => driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div/div/div[1]"));
    private IWebElement FooterText => driver.FindElement(By.XPath("/html/body/section/div/div[2]/div[1]/p[1]"));

    // Method to get text from specific elements
    public string GetElementText(By locator)
    {
        try
        {
            return driver.FindElement(locator).Text;

        }
        catch (NoSuchElementException)
        {
            return null;
        }    // Define locators for the texts you want to validate
    }

    public Dictionary<string, By> TextLocators => new Dictionary<string, By>
    {
        { "header", By.XPath("/html/body/header") },
        { "welcome message", By.XPath("/html/body/div[2]/div/div[1]/div/div/div[1]") },
        { "footer", By.XPath("/html/body/section/div/div[2]/div[1]/p[1]") }
    };
}