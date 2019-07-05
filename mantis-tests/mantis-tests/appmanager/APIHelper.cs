using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager) { }

        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;

            client.mc_issue_add(account.Username, account.Password, issue);
        }

        public void CreateNewProject(AccountData account, ProjectData projectData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData project = new Mantis.ProjectData();
            project.name = projectData.Name;

            client.mc_project_add(account.Username, account.Password, project);
        }

        public List<ProjectData> GetProjectListThroughAPI(AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            //Mantis.ProjectData proj = new Mantis.ProjectData();
            Mantis.ProjectData[] projects = client.mc_projects_get_user_accessible(account.Username, account.Password);
            List<ProjectData> listOfProjects = new List<ProjectData>();
            ProjectData project = new ProjectData();

            foreach (Mantis.ProjectData item in projects)
            {
                project.Name = item.name;
                project.Id = item.id;

                listOfProjects.Add(new ProjectData(project.Name)
                {
                    Id = project.Id
                });
            }
            return listOfProjects;
        }
    }
}
