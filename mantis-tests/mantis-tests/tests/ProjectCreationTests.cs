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
            List<ProjectData> oldProjects = app.Project.GetProjectList();
            
            //action
            app.Project.Create(project);

            //verification
            Assert.AreEqual(oldProjects.Count + 1, app.Project.GetProjectCount());
            List<ProjectData> newProjects = app.Project.GetProjectList();

            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);

        }
    }
}
