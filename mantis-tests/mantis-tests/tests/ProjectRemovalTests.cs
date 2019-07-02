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
            List<ProjectData> oldProjects = app.Project.GetProjectList();
            if(oldProjects == null)
            {
                app.Project.Create(project);
                oldProjects = app.Project.GetProjectList();
            }

            //action
            app.Project.Remove(0);

            //verification
            Assert.AreEqual(oldProjects.Count - 1, app.Project.GetProjectCount());
            List<ProjectData> newProjects = app.Project.GetProjectList();

            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);

        }
    }
}