using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager) { }

        public void Register(AccountData account)
        {
            OpenHomePage();
            OpenRegistrationForm();
            FillRegistrationForm(account);
            SubmitRegistration();
            string url = GetConfirmationUrl(account);
            FillPassworForm(url);
            SubmitPasswordForm();
        }

        public string GetConfirmationUrl(AccountData account)
        {
            string message = manager.Mail.GetLastMail(account);
            Match match = Regex.Match(message, @"http://\S*");
            return match.Value;
        }

        public void FillPassworForm(string url)
        {
            throw new NotImplementedException();
        }

        public void SubmitPasswordForm()
        {
            throw new NotImplementedException();
        }

        private void OpenRegistrationForm()
        {
            driver.FindElement(By.ClassName("back-to-login-link")).Click();
        }

        public void OpenHomePage()
        {
            manager.Driver.Url = "http://localhost:8080/mantisbt-2.21.1/login_page.php";
        }

        public void FillRegistrationForm(AccountData account)
        {
            driver.FindElement(By.Id("username")).SendKeys(account.Username);
            driver.FindElement(By.Id("email-field")).SendKeys(account.Email);
            driver.FindElement(By.Id("username")).SendKeys(account.Username);
        }

        public void SubmitRegistration()
        {
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
        }
    }
}
