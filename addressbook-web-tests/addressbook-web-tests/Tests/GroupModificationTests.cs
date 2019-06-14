using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            //preparation
            GroupData data = new GroupData("ааа");
            data.Header = "ббб";
            data.Footer = "ввв";
            app.Navigator.GoToGroupsPage();
            app.Groups.CheckRecordsExistAndCreate(0, data);
            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData oldData = oldGroups[0];

            //action
            GroupData newData = new GroupData("111");
            newData.Header = "222";
            newData.Footer = "333";
            app.Groups.ModifyById(oldData, newData);

            //verification
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

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
