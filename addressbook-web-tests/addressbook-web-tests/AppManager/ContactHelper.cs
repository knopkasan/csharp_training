using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
        private string addressField = "address";
        private string homePhoneField = "home";
        private string mobilePhoneField = "mobile";
        private string workPhoneField = "work";
        private string emailField = "email";
        private string email2Field = "email2";
        private string email3Field = "email3";

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void CheckExcistContactWithoutGroup(ContactData contact, ContactData contactData)
        {
            if (contact == null)
            {
                Create(contactData);
            }
        }

        public void CheckExcistContactInGroup(ContactData contactToBeRemoved, List<ContactData> oldList, GroupData group)
        {
            if (contactToBeRemoved == null)
            {
                ContactData newContact = ContactData.GetAll().Except(oldList).First();
                AddContactToGroup(newContact, group);
            }
        }

        public ContactHelper Remove(int index)
        {
            manager.Navigator.OpenHomePage();
            SelectContact(index);
            RemoveContact();
            CloseAlert();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public ContactHelper Remove(ContactData contact)
        {
            manager.Navigator.OpenHomePage();
            SelectContactById(contact.Id);
            RemoveContact();
            CloseAlert();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public ContactHelper SelectContactById(string id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + id + "'])")).Click();
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


        internal ContactHelper ModifyById(ContactData oldData, ContactData newData)
        {
            manager.Navigator.OpenHomePage();
            SelectContactById(oldData.Id);
            InitContactModificationById(oldData.Id);
            FillContactForm(newData);
            SubmitContactModification();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public void InitContactModificationById(string id)
        {
            driver.Navigate().GoToUrl("http://localhost:8080/addressbook/edit.php?id=" + id + "");
        }

        public ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
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

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper ShowDetails(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            contactCache = null;
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
            contactCache = null;
            return this;
        }

        public ContactHelper Search(string text)
        {
            manager.Navigator.OpenHomePage();
            Type(By.Name("searchstring"), text);
            return this;
        }

        public void AssertModifiedRecord(int index, ContactData contact)
        {
            InitContactModification(index);
            Assert.IsTrue(AssertContact(contact));
        }

        public bool CheckRecord(int index)
        {
            return IsElementPresent(By.XPath("(//img[@alt='Edit'])[" + (index+1) + "]"));
        }

        public bool AssertContact(ContactData contact)
        {
            return driver.FindElement(By.Name(firstnameField)).GetAttribute("value") == contact.Firstname
                && driver.FindElement(By.Name(lastnameField)).GetAttribute("value") == contact.Lastname;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.OpenHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name='entry']"));
                foreach(IWebElement element in elements)
                {
                    var lastname = element.FindElement(By.XPath("./td[2]"));
                    var firstname = element.FindElement(By.XPath("./td[3]"));
                    contactCache.Add(new ContactData(firstname.Text, lastname.Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("tr[name='entry']")).Count;
        }


        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.OpenHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastname = cells[1].Text;
            string firstname = cells[2].Text;
            string address = cells[3].Text;
            string allMails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstname, lastname)
            {
                Address = address,
                AllPhones = allPhones,
                AllMails = allMails
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(0);
            string firstname = driver.FindElement(By.Name(firstnameField)).GetAttribute("value");
            string lastname = driver.FindElement(By.Name(lastnameField)).GetAttribute("value");
            string address = driver.FindElement(By.Name(addressField)).GetAttribute("value");
            string email = driver.FindElement(By.Name(emailField)).GetAttribute("value");
            string email2 = driver.FindElement(By.Name(email2Field)).GetAttribute("value");
            string email3 = driver.FindElement(By.Name(email3Field)).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name(homePhoneField)).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name(mobilePhoneField)).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name(workPhoneField)).GetAttribute("value");

            return new ContactData(firstname, lastname)
            {
                Address = address,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };
        }

        public ContactData GetContactInformationFromDetails(int index)
        {
            manager.Navigator.OpenHomePage();
            ShowDetails(index);
            string content = driver.FindElement(By.Id("content")).Text;

            return new ContactData()
            {
                DetailInformation = content
            };
        }

        public int GetNumberOfSearchResult()
        {
            manager.Navigator.OpenHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }


        public int GetDisplayedElements()
        {
            int result = 0;
            IList<IWebElement> displayedrows = driver.FindElements(By.Name("entry"));
            foreach(IWebElement row in displayedrows)
            {
                if (row.Displayed)
                {
                    result++;
                }
            }

            return result;
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }


        public void RemoveContactFromGroup(ContactData contactToBeRemoved, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            SelectGroupInFilter(group.Name);
            SelectContact(contactToBeRemoved.Id);
            CommitRemoveContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void SelectGroupInFilter(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        private void CommitRemoveContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        private void SelectContact(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        private void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        private void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }
    }
}
