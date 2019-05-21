using System;
using System.Collections.Generic;
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
            GroupData newGroup = new GroupData("111");
            newGroup.Header = "ббб";
            newGroup.Footer = "ввв";
            app.Navigator.GoToGroupsPage();
            app.Groups.CheckRecordsExistAndCreate(0, newGroup);
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            //action
            app.Groups.Remove(0);

            //verification
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            GroupData toBeRemoved = oldGroups[0];
            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);

            foreach(GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
