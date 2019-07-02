using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ManagementMenuHelper : HelperBase
    {
        private string baseURL;

        public ManagementMenuHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.manager = manager;
            this.baseURL = baseURL;
        }
        public void GoToHomePage()
        {
            if (driver.Url == baseURL)
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL);
        }
        public void GoToMyViewPage()
        {
            if (driver.Url == baseURL + "my_view_page.php"
               && IsElementPresent(By.ClassName("widget-menu")))
            {
                return;
            }
            driver.FindElement(By.XPath("//a[contains(text(),'Обзор')] ")).Click();
        }


        public void GoToTaskListPage()
        {
            if (driver.Url == baseURL + "view_all_bug_page.php"
               && IsElementPresent(By.XPath("//div[@class='widget-toolbar']")))
            {
                return;
            }
            driver.FindElement(By.XPath("//span[contains(text(),'Список задач')] ")).Click();
        }

        public void GoToJournalPage()
        {
            if (driver.Url == baseURL + "changelog_page.php"
               && IsElementPresent(By.XPath("//p[@class='lead']")))
            {
                return;
            }
            driver.FindElement(By.XPath("//span[contains(text(),'Журнал')]")).Click();
        }

        public void GoToPlanPage()
        {
            if (driver.Url == baseURL + "roadmap_page.php"
               && IsElementPresent(By.XPath("//p[@class='lead']")))
            {
                return;
            }
            driver.FindElement(By.XPath("//span[contains(text(),'План')]")).Click();
        }

        public void GoToStatPage()
        {
            if (driver.Url == baseURL + "summary_page.php"
               && IsElementPresent(By.XPath("//h4[@class='widget-title lighter']")))
            {
                return;
            }
            driver.FindElement(By.XPath("//span[contains(text(),'Статистика')] ")).Click();
        }

        public void GoToControlPage()
        {
            if (driver.Url == baseURL + "manage_overview_page.php"
               && IsElementPresent(By.XPath("//ul[@class='nav nav-tabs padding-18']//li[@class='active']//a ")))
            {
                return;
            }
            driver.FindElement(By.XPath("//span[contains(text(),'Управление')] ")).Click();
        }

        public void GoToManageProject()
        {
            if (driver.Url == baseURL + "manage_proj_page.php"
               && IsElementPresent(By.CssSelector("input[name='manage_proj_create_page_token']")))
            {
                return;
            }
            driver.FindElement(By.XPath("//a[contains(text(),'Управление проектами')]")).Click();
        }
    }
}

