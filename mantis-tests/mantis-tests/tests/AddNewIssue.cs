using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    [TestFixture]
    public class AddNewIssue : TestBase
    {
        [Test]
        public void AddNewIssueTest()
        {
             AccountData account = new AccountData("administrator", "root1"){};
             ProjectData project = new ProjectData()
             {
                 Id = "1"
             };

             IssueData issue = new IssueData()
             {
                 Summary = "some short text",
                 Description = "some long text",
                 Category = "General"
             };

             app.API.CreateNewIssue(account, project, issue);
        }
    }
}
