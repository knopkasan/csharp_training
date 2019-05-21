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
        [Test]
        public void ContactCreationTest()
        {
            //preporation
            ContactData contact = new ContactData("firstname", "firstname");
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
            ContactData contact = new ContactData("", "");
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



