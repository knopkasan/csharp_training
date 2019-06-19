using System;
using NUnit.Framework;
using System.Collections.Generic;
namespace addressbook_tests_autoit
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void TestGroupRemoval()
        {
            //preparation
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            GroupData newGroup = new GroupData()
            {
                Name = "тест"
            };
            app.Groups.CreateGroupIfNeeded(oldGroups, newGroup);
            if (oldGroups == null)
            {
                oldGroups = app.Groups.GetGroupList();
            }

            //action
            app.Groups.Remove(1);

            //verification
            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Remove(newGroup);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
