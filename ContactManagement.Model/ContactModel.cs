using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Model
{
    public class ContactModel
    {
        public Int32 ContactId { get; set; }
        public Int32 CategoryId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }

        public ContactModels ContactModels { get; set; }
        public List<ContactModels> ContactList { get; set; }
    }
}
