using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class SearchTests : AuthTestBase
    {
        [Test]
        public void SearchTest()
        {
            app.Contacts.Search("firstname");

            //verification
            Assert.AreEqual(app.Contacts.GetNumberOfSearchResult(), app.Contacts.GetDisplayedElements());
        }
    }
}
