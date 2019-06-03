using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
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

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Middlename { get; set; }

        public string Nickname { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        public string Homepage { get; set; }

        public string Address2 { get; set; }

        public string Phone2 { get; set; }

        public string Notes { get; set; }

        public string Id { get; set; }

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
    }
}
