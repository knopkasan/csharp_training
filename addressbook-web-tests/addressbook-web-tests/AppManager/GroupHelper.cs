using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class GroupHelper : HelperBase
    {
        private string groupNameField = "group_name";
        private string groupHeaderField = "group_header";
        private string groupFooterField = "group_footer";

        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            InitGroupCreation();
            FillGroupForm(group);
            Submit();
            ReturnToGroupsPage();
            return this;
        }

        public GroupHelper Remove (int index)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(index);
            DeleteGroup();
            ReturnToGroupsPage();
            return this;
        }


        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }

        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public GroupHelper DeleteGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper FillGroupForm(GroupData group)
        {
            driver.FindElement(By.Name(groupNameField)).Click();
            driver.FindElement(By.Name(groupNameField)).Clear();
            driver.FindElement(By.Name(groupNameField)).SendKeys(group.Name);
            driver.FindElement(By.Name(groupHeaderField)).Click();
            driver.FindElement(By.Name(groupHeaderField)).Clear();
            driver.FindElement(By.Name(groupHeaderField)).SendKeys(group.Header);
            driver.FindElement(By.Name(groupFooterField)).Click();
            driver.FindElement(By.Name(groupFooterField)).Clear();
            driver.FindElement(By.Name(groupFooterField)).SendKeys(group.Footer);
            return this;
        }

        public GroupHelper Submit()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }
    }
}
