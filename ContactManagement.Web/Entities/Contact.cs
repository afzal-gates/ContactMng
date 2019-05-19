using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManagement.Web.Entities
{
    public class Contact
    {
        public Int32 ContactId { get; set; }
        public Int32 CategoryId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
    }

    public class Category
    {
        public Int32 CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}