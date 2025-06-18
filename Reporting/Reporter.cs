using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;


namespace DataHub_Automation.Reporting
{
    class Reporter
    {
        private static ExtentReports ReportManager { get; set; }
        private static string ApplicationDebuggingFolder => @" ";

        private static string HtmlReportFullPath { get; set; }

        public static string LatestResultsReportFolder { get; set; }

        private static TestContext MyTestContext { get; set; }

        private static ExtentTest CurrentTestCase { get; set; }

        private static bool IsExtentReportStarted = false;

        //Attaching htmlreporter object to extent report and start the reporter engine
        public static void StartReporter()
        {
            CreateReportDirectory();
            var htmlReporter = new ExtentSparkReporter(HtmlReportFullPath);
            ExtentSparkReporter spark = new ExtentSparkReporter("DataHub.html");
            ReportManager = new ExtentReports();
            ReportManager.AttachReporter(spark);
        }

        private static void CreateReportDirectory()
        {
            var filePath = Path.GetFullPath(ApplicationDebuggingFolder);
            LatestResultsReportFolder = Path.Combine(filePath, DateTime.Now.ToString("MMdd_HHmm"));
            Directory.CreateDirectory(LatestResultsReportFolder);

            HtmlReportFullPath = $"{LatestResultsReportFolder}\\DataHub.html";
        }

        public static void AddTestCaseMetadataToHtmlReport(TestContext testContext)
        {
            MyTestContext = testContext;
            CurrentTestCase = ReportManager.CreateTest(MyTestContext.TestName);
        }

        //Marks whether test cases passed/failed..screenshot attachment not supported with current version of ExtentReports
        public static void ReportTestOutcome(string screenshotPath)
        {
            var status = MyTestContext.CurrentTestOutcome;

            switch (status)
            {
                case UnitTestOutcome.Failed:
                    CurrentTestCase.AddScreenCaptureFromPath(screenshotPath);
                    CurrentTestCase.Fail("Fail");
                    break;
                case UnitTestOutcome.Inconclusive:
                    CurrentTestCase.AddScreenCaptureFromPath(screenshotPath);
                    CurrentTestCase.Warning("Inconclusive");
                    break;
                case UnitTestOutcome.Unknown:
                    CurrentTestCase.Skip("Test skipped");
                    break;
                default:
                    CurrentTestCase.Pass("Pass");
                    break;
            }

            //Flush
            ReportManager.Flush();
        }

        public static void GetReportManager()
        {
            if (!IsExtentReportStarted)
            {
                IsExtentReportStarted = true;
                StartReporter();
            }
        }
    }
}
