using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            //preporation
            ContactData contact = new ContactData("Иван", "Иванов");
            app.Navigator.OpenHomePage();
            app.Contacts.CheckRecordExistAndCreate(1, contact);

            //action
            app.Contacts.Remove(1);

            //verification
            Assert.IsFalse(app.Contacts.CheckRecord(1));
        }
    }
}
