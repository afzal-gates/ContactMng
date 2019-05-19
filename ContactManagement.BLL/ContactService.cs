using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactManagement.Model;
using ContactManagement.Shared;
using ContactManagement.Data;

namespace ContactManagement.BLL
{
    public static class ContactService
    {
        public static List<ContactModels> GetContacts(string userName)
        {
            List<ContactModels> Contacts = ContactRepository.GetAll(userName);
            return Contacts.ToList();
        }

        public static ContactModels GetContactById(int id)
        {
            ContactModels model = new ContactModels();
            model = ContactRepository.GetById(id);
            return model;
        }

        public static ContactModels SaveContact(int id, ContactModels model)
        {
            ContactRepository.Save(id, model);
            return model;
        }
        public static bool DeleteContact(int id)
        {
            return ContactRepository.Delete(id);
        }

        public static List<SelectModel> GetContactSelectModels(string comp_code)
        {
            List<SelectModel> list = ContactRepository.GetContactSelectModels(comp_code);
            return list;
        }
    }
}

