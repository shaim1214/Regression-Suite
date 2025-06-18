Regression Test Suite Setup Guide 

This document provides instructions for setting up and running the automated regression test suite locally on your development machine. 
The suite uses Selenium WebDriver with C#, SpecFlow for BDD, and NUnit as the test framework. 

Prerequisites 
For All Operating Systems 
.NET SDK (version 6.0 or later) 
Chrome and/or Edge, Firefox browsers installed 
Git for accessing the repository 


Windows-Specific 
Visual Studio 2019 or 2022 (Community, Professional, or Enterprise)  
Ensure ".NET desktop development" workload is installed 
Add "SpecFlow for Visual Studio" extension from Extensions menu 
 

macOS-Specific 
Visual Studio for Mac or VS Code 
If using VS Code:  
C# extension 
.NET Extension Pack 
SpecFlow for VS Code extension 

Hard-coded Configuration (Required for macOS)

For local development or cross-platform execution, you may need to modify the browser setup
Locate the browser initialization code in BaseClass.cs
NavigationHelper.NavigateToUrl("http://localhost:8090");

Known Cross-Platform Issues
macOS Compatibility
Project File Changes:

Change the target framework from net7.0-windows to net7.0 in the .csproj file
xml<TargetFramework>net7.0</TargetFramework>

Package Updates:
Remove Allure.NUnit and Allure.SpecFlow packages
Add Allure.Reqnroll package


Setup Instructions 
1. Clone the Repository 
bash 
git clone [repository-url] cd [repository-name] 

 
2. Install Dependencies 
From the project directory, restore NuGet packages: 
bash 
dotnet restore 

 
3. Install WebDriver Executables 
The test suite uses WebDriver to control browsers: 
WebDriverManager  
This is already configured in the code. The test runner will automatically download and manage the correct drivers. 
Running Tests 
Using Visual Studio (Windows or Mac) 
Open the solution file (.sln) in Visual Studio 
Build the solution (Build → Build Solution) 
Open Test Explorer (Test → Test Explorer) 
Click "Run All Tests" or select specific tests to run 


Using VS Code 
Open the project folder in VS Code 
Open a terminal in VS Code 
Run tests using the dotnet CLI: 
bash 
dotnet test 
Using Command Line (Any OS) 
Navigate to the project directory and run: 
bash
dotnet test 

To run specific tagged tests: 
bash 
dotnet test --filter "Category=Regression" 


Configuration 
Browser Selection 
By default, the tests run in Chrome. To specify a different browser, modify the testhost.dll.config file: 

<?xml version="1.0" encoding="utf-8" ?> 
<configuration> 
<appSettings> 
<add key="Browser" value ="Chrome"/> 
<!--<add key="Browser" value ="Edge"/>--> 
<!--<add key="Username" value =""/>--> 
<add key="Website" value="example.com"/> 
<add key="PageLoadTimeout" value="30" /> 
<add key="ElementLoadTimeout" value="4" /> 
</appSettings> 
</configuration> 
 

Adding New Tests 
Create new feature files in the Features directory 
Implement step definitions in the StepDefinitions directory 
Add page objects locators 

