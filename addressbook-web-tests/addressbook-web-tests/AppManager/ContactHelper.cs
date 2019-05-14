﻿using System;
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
    public class ContactHelper : HelperBase
    {
        private string firstnameField = "firstname";
        private string lastnameField = "lastname";

        
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Remove(int index)
        {
            manager.Navigator.OpenHomePage();
            SelectContact(index);
            RemoveContact();
            CloseAlert();
            //manager.Navigator.OpenHomePage();
            return this;
        }

        public ContactHelper Modify(int index, ContactData newData)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(index);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            Submit();
            ReturnToHomePage();
            return this;
        }

        /// <summary>
        /// TODO: продумать, как создавать недостающие записи в цикле. Этот вариант работает, но мне не нравится, локатор в условии цикла
        /// </summary>
        /// <param name="index"></param>
        /*public void CheckRecordsExistAndCreate(int index)
        {
            if (!IsElementPresent(By.XPath("(//img[@alt='Edit'])[" + index + "]")))
            {
                while (driver.FindElements(By.XPath("//img[@alt='Edit']")).Count < index)
                {
                    ContactData contact = new ContactData("aaa", "bbb");
                    Create(contact);
                }
            }
        }*/

        public void CheckRecordExistAndCreate(int index, ContactData contact)
        {
            if (!CheckRecord(index))
            {
                Create(contact);
            }
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name(firstnameField), contact.Firstname);
            Type(By.Name(lastnameField), contact.Lastname);
            return this;
        }

        public ContactHelper Submit()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + index + "]")).Click();
          
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            return this;
        }

        public ContactHelper CloseAlert()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public void AssertModifiedRecord(int index, ContactData contact)
        {
            InitContactModification(index);
            Assert.IsTrue(AssertContact(contact));
        }

        public bool CheckRecord(int index)
        {
            return IsElementPresent(By.XPath("(//img[@alt='Edit'])[" + index + "]"));
        }

        public bool AssertContact(ContactData contact)
        {
            return driver.FindElement(By.Name(firstnameField)).GetAttribute("value") == contact.Firstname
                && driver.FindElement(By.Name(lastnameField)).GetAttribute("value") == contact.Lastname;
        }


    }
}
