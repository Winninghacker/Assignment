using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TrelloApiTests.Base
{
    public class TestListeners : BaseTest
    {
        // Instance of ExtentTest to log the test steps and results
        private ExtentTest _test;

        // Instance of ExtentReports to generate the report
        private static readonly ExtentReports Extent = ExtentReportNG.GetReportObject();

        /// <summary>
        /// This method is invoked when a test case starts. It creates a new test entry
        /// in the ExtentReports.
        /// </summary>
        [SetUp]
        public void OnTestStart()
        {
            // Create a new test entry with the name of the test method
            var testName = TestContext.CurrentContext.Test.MethodName;
            _test = Extent.CreateTest(testName);
        }

        /// <summary>
        /// This method is invoked when a test case fails. It logs the failure and the
        /// exception stack trace in the ExtentReports.
        /// </summary>
        [TearDown]
        public void OnTestEnd()
        {
            // Check if the test has failed
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                // Log the failure and the exception message
                _test.Fail(TestContext.CurrentContext.Result.Message);
            }

            // After each test, write all the logged information to the report file
            Extent.Flush();
        }
    }
}
