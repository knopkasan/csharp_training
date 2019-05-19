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
            app.Groups.CheckRecordsExistAndCreate(0, data);
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            GroupData oldData = oldGroups[0];

            //action
            GroupData newData = new GroupData("111");
            newData.Header = "222";
            newData.Footer = "333";
            app.Groups.Modify(0, newData);

            //verification
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
            //app.Groups.AssertModifiedGroup(0, newData);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
}
