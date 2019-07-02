using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBrowser.WebDriver;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace mantis_tests
{
    public class AdminHelper : HelperBase
    {
        private string baseUrl;
        public AdminHelper(ApplicationManager manager, string baseUrl) : base(manager)
        {
            //this.manager = manager;
            this.baseUrl = baseUrl;
        }

        public List<AccountData> GetAllAccounts()
        {
            List<AccountData> accounts = new List<AccountData>();

            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "manage_user_page.php";
            IList<IWebElement> rows = driver.FindElements(By.CssSelector("tbody tr"));
            foreach (IWebElement row in rows)
            {
                IWebElement link = row.FindElement(By.TagName("a"));
                string name = link.Text;
                string href = link.GetAttribute("href");
                Match m = Regex.Match(href, @"\d+$");
                string id = m.Value;

                accounts.Add(new AccountData() {
                    Username = name,
                    Id = id
                });
            }
            return accounts;
        }

        public List<ProjectData> GetAllProjects()
        {
            List<ProjectData> projects = new List<ProjectData>();

            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "manage_proj_page.php";
            ICollection<IWebElement> rows = driver.FindElements(By.CssSelector(".col-md-12 > .widget-color-blue2 table tbody tr"));
            foreach (IWebElement row in rows)
            {
                IWebElement link = row.FindElement(By.TagName("a"));
                string name = link.Text;
                string href = link.GetAttribute("href");
                Match m = Regex.Match(href, @"\d+$");
                string id = m.Value;

                projects.Add(new ProjectData(name)
                {
                    Id = id
                });
            }
            return projects;
        }

        public void DeleteAccount(AccountData account)
        {
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElement(By.CssSelector("input[value='Удалить учётную запись']")).Click(); //удаление
            driver.FindElement(By.CssSelector("input[value='Удалить учётную запись']")).Click(); //подтверждение
        }

        public void DeleteProject(ProjectData project)
        {
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "manage_proj_edit_page.php?project_id=" + project.Id; 
            driver.FindElement(By.CssSelector("input[value='Удалить проект']")).Click(); //удаление
            driver.FindElement(By.CssSelector("input[value='Удалить проект']")).Click(); //подтверждение
        }

        private IWebDriver OpenAppAndLogin()
        {
            IWebDriver driver = new SimpleBrowserDriver();
            driver.Url = baseUrl + "login_page.php";
            //Thread.Sleep(1000);
            var input1 = driver.FindElement(By.Name("username"));
            input1.SendKeys("administrator");
            driver.FindElement(By.CssSelector("input.btn")).Click();
            //Thread.Sleep(1000);
            var input2 = driver.FindElement(By.Name("password"));
            input2.SendKeys("root");
            driver.FindElement(By.CssSelector("input.btn")).Click();
            return driver;
        }
    }
}
