using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovalTests : AuthTestBase
    {

        [Test]
        public void TestProjectRemoval()
        {
            //preparation
            ProjectData project = new ProjectData(GenerateRandomString(10));
            AccountData account = new AccountData("administrator", "root1") { };
            List<ProjectData> oldProjects = app.API.GetProjectListThroughAPI(account);
            if(oldProjects == null)
            {
                app.API.CreateNewProject(account, project);
            }

            //action
            app.Project.Remove(0);

            //verification
            Assert.AreEqual(oldProjects.Count - 1, app.Project.GetProjectCount());

            List<ProjectData> newProjects = app.API.GetProjectListThroughAPI(account);

            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }

        [Test]
        public void AddNewProject()
        {
            AccountData account = new AccountData()
            {
                Username = "administrator",
                Password = "root1"
            };

            ProjectData project = new ProjectData()
            {
                Name = "The first of API tests"
            };

            app.API.CreateNewProject(account, project);

        }
    }
}