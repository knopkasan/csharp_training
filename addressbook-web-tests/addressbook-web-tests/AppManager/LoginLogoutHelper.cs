using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
   public class LoginLogoutHelper : HelperBase
    {
        private string loginField = "user";
        private string passwordField = "pass";

        public LoginLogoutHelper(ApplicationManager manager) : base(manager)
        {
        }
        public void Login(AccountData account)
        {
            driver.FindElement(By.Name(loginField)).Click();
            driver.FindElement(By.Name(loginField)).Clear();
            driver.FindElement(By.Name(loginField)).SendKeys(account.Username);
            driver.FindElement(By.Name(passwordField)).Click();
            driver.FindElement(By.Name(passwordField)).Clear();
            driver.FindElement(By.Name(passwordField)).SendKeys(account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }

        public void Logout()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }
    }
}
