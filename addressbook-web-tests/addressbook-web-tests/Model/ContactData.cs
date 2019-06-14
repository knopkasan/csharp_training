using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allMails;
        private string detailInformation;

        public ContactData()
        {
        }

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public ContactData(string firstname)
        {
            Firstname = firstname;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Firstname == other.Firstname
                && Lastname == other.Lastname;
        }

        public override int GetHashCode()
        {
            return Firstname.GetHashCode() ^ Lastname.GetHashCode();
        }

        public override string ToString()
        {
            return "firstname=" + Firstname + " " + "lastname=" + Lastname;
        }

        public int CompareTo(ContactData other)
        {
            var str1 = string.Concat(other.Firstname, other.Lastname);
            var str = string.Concat(Firstname, Lastname);
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return str.CompareTo(str1);
        }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        [Column(Name = "middlename")]
        public string Middlename { get; set; }

        [Column(Name = "nickname")]
        public string Nickname { get; set; }

        [Column(Name = "title")]
        public string Title { get; set; }

        [Column(Name = "company")]
        public string Company { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "fax")]
        public string Fax { get; set; }

        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "email2")]
        public string Email2 { get; set; }

        [Column(Name = "email3")]
        public string Email3 { get; set; }

        [Column(Name = "homepage")]
        public string Homepage { get; set; }

        [Column(Name = "ayear")]
        public string Ayear { get; set; }

        [Column(Name = "byear")]
        public string Byear { get; set; }

        [Column(Name = "address2")]
        public string Address2 { get; set; }

        [Column(Name = "phone2")]
        public string Phone2 { get; set; }

        [Column(Name = "notes")]
        public string Notes { get; set; }

        [Column(Name = "bday")]
        public string Bday { get; set; }

        [Column(Name = "aday")]
        public string Aday { get; set; }

        [Column(Name = "amonth")]
        public string Amonth { get; set; }

        [Column(Name = "bmonth")]
        public string Bmonth { get; set; }

        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public string AllPhones
        {
            get{
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set { allPhones = value; }
        }

        public string AllMails
        {
            get
            {
                if (allMails != null)
                {
                    return allMails;
                }
                else
                {
                    return (AddSpecSymbol(Email) + AddSpecSymbol(Email2) + AddSpecSymbol(Email3)).Trim();
                }
            }
            set { allMails = value; }
        }

        public string DetailInformation {
            get
            {
                if (detailInformation != null)
                {
                    return detailInformation;
                }
                else
                {
                    string resultText = "";

                    string fio = "";

                    if (!string.IsNullOrEmpty(Firstname))
                    {
                        fio += Firstname;
                    }
                    if (!string.IsNullOrEmpty(Lastname))
                    {
                        fio += Lastname;
                    }
                    if(!string.IsNullOrEmpty(fio))
                    {
                        resultText += fio + "\r\n";
                    }
                    if (!string.IsNullOrEmpty(Address))
                    {
                        resultText += Address + "\r\n";
                    }
                    resultText += "\r\n";
                    if (!string.IsNullOrEmpty(HomePhone))
                    {
                        resultText += "H: " + HomePhone + "\r\n";
                    }
                    if (!string.IsNullOrEmpty(MobilePhone))
                    {
                        resultText += "M: " + MobilePhone + "\r\n";
                    }
                    if (!string.IsNullOrEmpty(WorkPhone))
                    {
                        resultText += "W: " +  WorkPhone + "\r\n";
                    }
                    resultText += "\r\n";
                    if (!string.IsNullOrEmpty(Email))
                    {
                        resultText += Email + "\r\n";
                    }
                    if (!string.IsNullOrEmpty(Email2))
                    {
                        resultText += Email2 + "\r\n";
                    }
                    if (!string.IsNullOrEmpty(Email3))
                    {
                        resultText += Email3 + "\r\n";
                    }
                    resultText += "\r\n";

                    return resultText.Trim('\r', '\n');
                }
            }
            set { detailInformation = value; }
        }

        
        private string AddSpecSymbol(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            else
            {
                return str + "\r\n";
            }
        }

        private string CleanUp(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return "";
            }
            else
            {
                return Regex.Replace(phone, "[ -()]", "") + "\r\n";
            }
        }

        public static List<ContactData> GetAll()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }

    }
}
