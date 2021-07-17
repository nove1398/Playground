using LinqToLdap.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace LDAP
{
    [DirectorySchema(NamingContext, ObjectCategory = "Person", ObjectClass = "User")]
    public class ADUser
    {
        public const string NamingContext = "DC=mtw,DC=gov,DC=jm";

        [DistinguishedName]
        public string DistinguishedName { get; set; }

        [DirectoryAttribute("cn", ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public string CommonName { get; set; }

        [DirectoryAttribute("objectguid", ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public Guid Guid { get; set; }

        [DirectoryAttribute("objectsid", ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public SecurityIdentifier Sid { get; set; }

        [DirectoryAttribute("title", ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public string Title { get; set; }

        [DirectoryAttribute("description", ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public string TitleDescription { get; set; }

        [DirectoryAttribute("givenname")]
        public string FirstName { get; set; }

        [DirectoryAttribute("sn")]
        public string LastName { get; set; }

        [DirectoryAttribute(ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public DateTime WhenCreated { get; set; }

        [DirectoryAttribute(ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public DateTime WhenChanged { get; set; }

        [DirectoryAttribute("physicaldeliveryofficename", ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public string Office { get; set; }

        [DirectoryAttribute("mail", ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public string Email { get; set; }

        [DirectoryAttribute("telephonenumber", ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public string TelephoneNumber { get; set; }

        [DirectoryAttribute("mobile", ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public string MobileNumber { get; set; }

        [DirectoryAttribute("ipphone", ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public string Extension { get; set; }

        [DirectoryAttribute("employeenumber", ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public string EmployeeNumber { get; set; }

        [DirectoryAttribute("employeeid", ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public string EmployeeId { get; set; }

        [DirectoryAttribute("department", ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public string Department { get; set; }

        [DirectoryAttribute("manager", ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public string Supervisor { get; set; }

        [DirectoryAttribute("info", ReadOnlyOnAdd = true, ReadOnlyOnSet = true)]
        public string Notes { get; set; }

        public void SetDistinguishedName()
        {
            DistinguishedName = string.Format("CN={0},{1}", CommonName, NamingContext);
        }
    }
}