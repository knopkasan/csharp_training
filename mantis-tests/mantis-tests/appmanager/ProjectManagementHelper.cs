using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager)
        {
            this.manager = manager;
        }

        public void Create(ProjectData project)
        {
            manager.Navigator.GoToControlPage();
            manager.Navigator.GoToManageProject();
            InitCreation();
            FillProjectForm(project);
            SubmitCreation();
            manager.Navigator.GoToManageProject();
            projectCache = null;
        }

        public void Remove(int index)
        {
            manager.Navigator.GoToControlPage();
            manager.Navigator.GoToManageProject();
            SelectProject(index);
            InitRemove();
            SubmitRemove();
            manager.Navigator.GoToManageProject();
            projectCache = null;
        }

        public void SubmitCreation()
        {
            driver.FindElement(By.CssSelector("input[value='Добавить проект']")).Click();
        }

        public void FillProjectForm(ProjectData project)
        {
            driver.FindElement(By.Name("name")).SendKeys(project.Name);
        }

        public void InitCreation()
        {
            driver.FindElement(By.XPath("//button[contains(text(),'Создать новый проект')]")).Click();
        }

        private void SubmitRemove()
        {
            driver.FindElement(By.XPath("//input[@class='btn btn-primary btn-white btn-round']")).Click();
        }

        private void InitRemove()
        {
            driver.FindElement(By.XPath("//input[@class='btn btn-primary btn-sm btn-white btn-round']")).Click();
        }

        private void SelectProject(int index)
        {
            //.col-md-12 > .widget-color-blue2 table tbody tr
            driver.FindElement(By.XPath("//div[@class='widget-box widget-color-blue2']//tbody/tr[" + (index + 1) + "]")).FindElement(By.TagName("a")).Click();
        }

        private List<ProjectData> projectCache = null;

        
        public List<ProjectData> GetProjectList()
        {
            if (projectCache == null)
            {
                // .widget-box:first-child  table tr
                // [??parent].widget-box:last-child
                projectCache = new List<ProjectData>();
                manager.Navigator.GoToControlPage();
                manager.Navigator.GoToManageProject();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector(".col-md-12 > .widget-color-blue2 table tbody tr"));
                foreach (IWebElement element in elements)
                {
                    var Name = element.FindElement(By.XPath("./td[1]"));
                    projectCache.Add(new ProjectData(Name.Text){});
                }
            }
            return new List<ProjectData>(projectCache);
        }

        public int GetProjectCount()
        {
            return driver.FindElements(By.CssSelector(".col-md-12 > .widget-color-blue2 table tbody tr")).Count;
        }
    }
}
