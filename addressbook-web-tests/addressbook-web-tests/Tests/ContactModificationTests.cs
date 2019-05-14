using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
   public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            //preporation
            ContactData data = new ContactData("Иван", "Иванов");
            ContactData newData = new ContactData("Петр", "Петров");
            app.Navigator.OpenHomePage();
            app.Contacts.CheckRecordExistAndCreate(1, data);
            
            //action
            app.Contacts.Modify(1, newData);

            //verification
            app.Contacts.AssertModifiedRecord(1, newData);
        }
    }
}
