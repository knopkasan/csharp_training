﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            //preparation
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).FirstOrDefault();

            ContactData contactData = new ContactData("Иван", "Иванов");
            app.Navigator.OpenHomePage();
            app.Contacts.CheckRecordExistAndCreate(0, contactData);

            GroupData groupData = new GroupData("ааа");
            app.Navigator.GoToGroupsPage();
            app.Groups.CheckRecordsExistAndCreate(0, groupData);

            app.Contacts.CheckExcistContactWithoutGroup(contact, contactData);

            //actions
            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }

    }
}
