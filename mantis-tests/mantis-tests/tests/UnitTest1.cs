using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;

namespace mantis_tests
{
    [TestFixture]
    public class Tests : TestBase
    {
        /*public static IEnumerable<ProjectData> RandomContactDataProvider()
        {
            List<ProjectData> projects = new List<ProjectData>();
            for (int i = 0; i < 5; i++)
            {
                projects.Add(new ProjectData(GenerateRandomString(10)){});
            }
            return projects;
        }
        */

        [Test /*, TestCaseSource("RandomContactDataProvider")*/]
        public void Testn()
        {
            IWebDriver driver = new SimpleBrowserDriver();
            driver.Url = "http://localhost:8080/mantisbt-2.21.1/login_page.php";
            driver.FindElement(By.Name("username")).SendKeys("administrator");
            driver.FindElement(By.CssSelector("input.btn")).Click();
            driver.FindElement(By.Name("password")).SendKeys("root");
            driver.FindElement(By.CssSelector("input.btn")).Click();
        }
    }
}