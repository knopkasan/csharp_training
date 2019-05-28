using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(10))
                {
                    Firstname = GenerateRandomString(10),
                    Lastname = GenerateRandomString(10)
                });
            }
            return contacts;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {
            //preporation
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            //action
            app.Contacts.Create(contact);

            //verivication
            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts.Count, newContacts.Count);
        }

        [Test]
        public void EmrtyContactCreationTest()
        {
            //preporation
            ContactData contact = new ContactData("", null);
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            //action
            app.Contacts.Create(contact);

            //verivication
            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts.Count, newContacts.Count);
        }

    }
}



