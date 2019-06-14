using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class GroupTestBase : AuthTestBase
    {
        [TearDown]
        public void CompareGroupsUi_Db()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                List<GroupData> FromUi = app.Groups.GetGroupList();
                List<GroupData> FromDb = GroupData.GetAll();
                FromUi.Sort();
                FromDb.Sort();
                Assert.AreEqual(FromUi, FromDb);
            }
        }
    }
}
