using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
   public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModoficationTest()
        {
            ContactData newData = new ContactData("ccc", "vvv");

            app.Contacts.Modify(1, newData);
        }
    }
}
