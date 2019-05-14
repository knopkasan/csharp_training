using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
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


        public GroupHelper Modify(int index, GroupData newData)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(index);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupsPage();
            return this;
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

        /// <summary>
        /// TODO: продумать, как создавать недостающие записи в цикле. Этот вариант работает, но мне не нравится, локатор в условии цикла
        /// </summary>
        /// <param name="index"></param>
        /*public void CheckRecordsExistAndCreate(int index)
        {
            if (!CheckRecord(index))
            {
               while (driver.FindElements(By.XPath("//input[@name='selected[]']")).Count < index)
               {
                    GroupData group = new GroupData("aaa");
                    group.Header = "bbb";
                    group.Footer = "ccc";
                    Create(group);
               }
            }
            
        }*/

        public void CheckRecordsExistAndCreate(int index, GroupData group)
        {
            if (!CheckRecord(index))
            {
                Create(group);
            }
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
            Type(By.Name(groupNameField), group.Name);
            Type(By.Name(groupHeaderField), group.Header);
            Type(By.Name(groupFooterField), group.Footer);
            return this;
        }


        public GroupHelper Submit()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }
        public bool CheckRecord(int index)
        {
            return IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + index + "]"));
        }

        public bool AssertFields(GroupData group)
        {
            return driver.FindElement(By.Name(groupNameField)).GetAttribute("value")
                   == group.Name
                && driver.FindElement(By.Name(groupHeaderField)).GetAttribute("value")
                   == group.Header
                && driver.FindElement(By.Name(groupFooterField)).GetAttribute("value")
                   == group.Footer;
        }
        public void AssertModifiedGroup(int index, GroupData group)
        {
            SelectGroup(index);
            InitGroupModification();
            Assert.IsTrue(AssertFields(group));
        }

    }
}
