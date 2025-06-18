using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using DataHub_Automation.ComponentHelper;
using System;
using System.IO;
using TechTalk.SpecFlow;

namespace DataHub_Automation.Hooks
{
    [Binding]
    class Hooks
    {
        private static ScenarioContext _scenarioContext;
        private static FeatureContext _featureContext;
        private static ExtentReports _extentReports;
        private static ExtentSparkReporter _extentSparkReporter;
        private static ExtentTest _feature;
        private static ExtentTest _scenario;
        private static string LatestResultsReportFolder { get; set; }
      
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            // Create the directory for reports first
            CreateReportDirectory();

            // Use the created directory for the HTML report
            string reportPath = Path.Combine(LatestResultsReportFolder, "DataHub.html");
            _extentSparkReporter = new ExtentSparkReporter(reportPath);
            _extentSparkReporter.Config.ReportName = "DataHub Automated Testing";
            _extentSparkReporter.Config.Theme = Theme.Dark;
            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(_extentSparkReporter);
        }

        [BeforeFeature]
        public static void BeforeFeatureStart(FeatureContext featureContext)
        {
            if (featureContext != null)
            {
                _feature = _extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Description);
            }
        }
        [BeforeScenario]
        public static void BeforeScenarioStart(ScenarioContext scenarioContext)
        {
            if (scenarioContext != null)
            {
                _scenarioContext = scenarioContext;
                _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title, scenarioContext.ScenarioInfo.Description);
            }
        }
        [AfterStep]
        public void AfterEachStep()
        {
            ScenarioBlock scenarioBlock = _scenarioContext.CurrentScenarioBlock;
            switch (scenarioBlock)
            {
                case ScenarioBlock.Given:
                    CreateNode<Given>();
                    break;
                case ScenarioBlock.When:
                    CreateNode<When>();
                    break;
                case ScenarioBlock.Then:
                    CreateNode<Then>();
                    break;
                default:
                    CreateNode<And>();
                    break;
            }
        }
        public void CreateNode<T>() where T : IGherkinFormatterModel
        {
            if (_scenarioContext.TestError != null)
            {
                string name = GetScreenshotFileName();
                GenericHelper.TakeScreenShot(name);
                _scenario.CreateNode<T>(_scenarioContext.StepContext.StepInfo.Text)
                    .Fail($"{_scenarioContext.TestError.Message}\n{_scenarioContext.TestError.StackTrace}")
                    .AddScreenCaptureFromPath(name);
            }
            else
            {
                _scenario.CreateNode<T>(_scenarioContext.StepContext.StepInfo.Text).Pass("");
            }
        }
        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extentReports.Flush();
        }
        private static void CreateReportDirectory()
        {
            // Change from absolute path to relative path
            var filePath = Path.GetFullPath("Reporting");
            LatestResultsReportFolder = Path.Combine(filePath, DateTime.Now.ToString("MMdd_HHmm"));
            Directory.CreateDirectory(LatestResultsReportFolder);
        }

        private static string GetScreenshotFileName()
        {
            return Path.Combine(LatestResultsReportFolder, $"{_scenarioContext.ScenarioInfo.Title.Replace(" ", "")}.jpeg");
        }
    }
}

