using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;
using Excel = Microsoft.Office.Interop.Excel;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(10))
                {
                    Firstname = GenerateRandomString(10),
                    Lastname = GenerateRandomString(10)
                });
            }
            return contacts;
        }

        /// <summary>
        /// Вытаскиваем данные для тестов из файла
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ContactData> ContactDataFromCsvFile()//погуглить
        {
            List<ContactData> contacts = new List<ContactData>();
            string[] lines = File.ReadAllLines(@"contacts.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                contacts.Add(new ContactData()
                {
                    Firstname = parts[0],
                    Lastname = parts[1]
                });
            }
            return contacts;
        }

        /// <summary>
        /// Читаем из xml файла
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>)new XmlSerializer(typeof(List<ContactData>))
                .Deserialize(new StreamReader(@"contacts.xml"));
        }

        /// <summary>
        /// Читаем из json файла
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"contacts.json"));
        }

        /// <summary>
        /// Читаем из excel файла
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ContactData> ContactDataFromExcelFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            Excel.Application app = new Excel.Application();
            //app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"groups.xlsx"));
            Excel.Worksheet sheet = wb.ActiveSheet;
            Excel.Range range = sheet.UsedRange;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                contacts.Add(new ContactData()
                {
                    Firstname = range.Cells[i, 1].Value,
                    Lastname = range.Cells[i, 2].Value
                });
            }
            wb.Close();
            //app.Visible = false;
            app.Quit();
            return contacts;
        }

        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void ContactCreationTest(ContactData contact)
        {
            //preporation
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            //action
            app.Contacts.Create(contact);

            //verivication
            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts.Count, newContacts.Count);
        }

        [Test]
        public void EmrtyContactCreationTest()
        {
            //preporation
            ContactData contact = new ContactData("", null);
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            //action
            app.Contacts.Create(contact);

            //verivication
            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts.Count, newContacts.Count);
        }

    }
}



