using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        /*public static IEnumerable<ProjectData> RandomContactDataProvider()
        {
            List<ProjectData> projects = new List<ProjectData>();
            for (int i = 0; i < 5; i++)
            {
                projects.Add(new ProjectData(GenerateRandomString(10)){});
            }
            return projects;
        }
        */

        [Test /*, TestCaseSource("RandomContactDataProvider")*/]
        public void TestProjectCreation()
        {
            //preparation
            ProjectData project = new ProjectData(GenerateRandomString(10));
            AccountData account = new AccountData("administrator", "root1") { };
            Mantis.ProjectData [] oldProjects = app.API.GetProjectListThroughAPI(account);
            
            //action
            app.Project.Create(project);

            //verification
            Assert.AreEqual(oldProjects.Length + 1, app.Project.GetProjectCount());

            Mantis.ProjectData[] newProjects = app.API.GetProjectListThroughAPI(account);
            Assert.AreEqual(oldProjects.Length + 1, newProjects.Length);

            /*Array.Resize(ref oldProjects, oldProjects.Length + 1);
            oldProjects[oldProjects.Length - 1] = (Mantis.ProjectData)project;*/

            /*oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);*/

        }
    }
}
