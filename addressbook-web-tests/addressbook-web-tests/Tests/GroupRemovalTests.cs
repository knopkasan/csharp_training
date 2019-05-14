using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            //preparation
            GroupData group = new GroupData("111");
            group.Header = "ббб";
            group.Footer = "ввв";
            app.Navigator.GoToGroupsPage();
            app.Groups.CheckRecordsExistAndCreate(1, group);

            //action
            app.Groups.Remove(1);

            //verification
            Assert.IsFalse(app.Groups.CheckRecord(1));
        }
    }
}
