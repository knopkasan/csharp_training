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
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contactToBeRemoved = oldList.FirstOrDefault();
            if (contactToBeRemoved == null)
            {
                ContactData contact = ContactData.GetAll().Except(oldList).First();
                app.Contacts.AddContactToGroup(contact, group);
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
