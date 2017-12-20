
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TatAutomationFramework.Common;

namespace NewSeleniumProject
{
    public class Class1
    {
        IWebDriver driver;
        ReportingTasks _reportingTasks;

        [SetUp]
        public void Initialize()
        {
            ExtentReports extentReports = ReportingManager.Instance;
            extentReports.LoadConfig(Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.FullName + "\\extent-config.xml");
            //Note we have hardcoded the browser, we will deal with this later
            extentReports.AddSystemInfo("Browser", "Firefox");

            _reportingTasks = new ReportingTasks(extentReports);

            _reportingTasks.InitializeTest();
            
        }

        [Test]
        public void SearchinFirefox()
        {
            driver = new FirefoxDriver();
            driver.Url = "http://www.google.com";
            //Wait and then check until the control with id=Name is available
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return d.FindElement(By.Id("lst-ib")); });

            driver.FindElement(By.Id("lst-ib")).SendKeys("selenium webdriver");//types selenium webdriver into the search field
            driver.FindElement(By.Id("lst-ib")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            Assert.AreEqual("selenium webdriver - Buscar con Google", driver.Title);//Nunit assertion to check title is correct
            
        }

        [Test]
        public void SearchinChrome()
        {
            driver = new ChromeDriver();
            driver.Url = "http://www.google.com";
            //driver.FindElement(By.Id("lst-ib")).Clear(); //clears the search box
            driver.FindElement(By.Id("lst-ib")).SendKeys("selenium webdriver");//types selenium webdriver into the search field
            driver.FindElement(By.Id("lst-ib")).SendKeys(Keys.Enter);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2000);

            Assert.AreEqual("selenium webdriver - Buscar con Google", driver.Title);//Nunit assertion to check title is correct

        }

        [TearDown]
        public void EndTest()
        {
           driver.Close();
            _reportingTasks.FinalizeTest();
            //_reportingTasks.CleanUpReporting();
            //driver.Manage().Cookies.DeleteAllCookies();
        }

    }
}
