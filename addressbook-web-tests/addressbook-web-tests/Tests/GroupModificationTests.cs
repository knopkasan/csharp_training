using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            //preporation
            GroupData data = new GroupData("ааа");
            data.Header = "ббб";
            data.Footer = "ввв";
            app.Navigator.GoToGroupsPage();
            app.Groups.CheckRecordsExistAndCreate(1, data);

            //action
            GroupData newData = new GroupData("111");
            newData.Header = "222";
            newData.Footer = "333";
            app.Groups.Modify(1, newData);

            //verification
            app.Groups.AssertModifiedGroup(1, newData);
        }
    }
}
