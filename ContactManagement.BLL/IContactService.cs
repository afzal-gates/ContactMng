using ContactManagement.Model;
using ContactManagement.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.BLL
{
    public interface IContactService
    {
        List<ContactModels> GetContacts(string searchText, int pn, int ps, out int total);
        ContactModels GetContactById(int id);
        ContactModels SaveContact(int id, ContactModels model);
        bool DeleteContact(int id);
        List<SelectModel> GetContactSelectModels(string compCode);
    }
}
