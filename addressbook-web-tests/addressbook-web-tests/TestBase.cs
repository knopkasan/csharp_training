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
    public class TestBase
    {
        protected IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private string loginField = "user";
        private string passwordField = "pass";
        private string groupNameField = "group_name";
        private string groupHeaderField = "group_header";
        private string groupFooterField = "group_footer";
        private string firstnameField = "firstname";
        private string lastnameField = "lastname";

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            baseURL = "http://localhost:8080/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        //общие методы для всех тестов
        protected void OpenHomePage()
        {
            driver.Navigate().GoToUrl(baseURL + "addressbook/");
        }

        protected void Login(AccountData account)
        {
            driver.FindElement(By.Name(loginField)).Click();
            driver.FindElement(By.Name(loginField)).Clear();
            driver.FindElement(By.Name(loginField)).SendKeys(account.Username);
            driver.FindElement(By.Name(passwordField)).Click();
            driver.FindElement(By.Name(passwordField)).Clear();
            driver.FindElement(By.Name(passwordField)).SendKeys(account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }

        protected void Logout()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }

        //методы для создания и удаления групп
        protected void GoToGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }

        protected void ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
        }

        protected void ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
        }

        //методы для удаления групп
        protected void SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
        }

        protected void DeleteGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
        }

        //методы для создания контакта
        protected void InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
        }

        protected void FillContactForm(ContactData contact)
        {
            driver.FindElement(By.Name(firstnameField)).Click();
            driver.FindElement(By.Name(firstnameField)).Clear();
            driver.FindElement(By.Name(firstnameField)).SendKeys(contact.Firstname);
            driver.FindElement(By.Name(lastnameField)).Click();
            driver.FindElement(By.Name(lastnameField)).Clear();
            driver.FindElement(By.Name(lastnameField)).SendKeys(contact.Lastname);
        }

        protected void Submit()
        {
            driver.FindElement(By.Name("submit")).Click();
        }

        //методы для создания группы
        protected void InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
        }

        protected void FillGroupForm(GroupData group)
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
        }
    }
}
