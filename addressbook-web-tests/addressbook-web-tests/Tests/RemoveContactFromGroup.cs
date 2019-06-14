using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class RemoveContactFromGroup : AuthTestBase
    {
        [Test]
        public void TestRemoveContactFromGroup()
        {
            //preparation
            ContactData contactData = new ContactData("Иван", "Иванов");
            app.Navigator.OpenHomePage();
            app.Contacts.CheckRecordExistAndCreate(0, contactData);

            GroupData groupData = new GroupData("ааа");
            app.Navigator.GoToGroupsPage();
            app.Groups.CheckRecordsExistAndCreate(0, groupData);

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contactToBeRemoved = oldList.FirstOrDefault();
            app.Contacts.CheckExcistContactInGroup(contactToBeRemoved, oldList, group);

            if (contactToBeRemoved == null)
            {
                contactToBeRemoved = ContactData.GetAll().Except(oldList).FirstOrDefault();
            }

            //actions
            app.Contacts.RemoveContactFromGroup(contactToBeRemoved, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contactToBeRemoved);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
