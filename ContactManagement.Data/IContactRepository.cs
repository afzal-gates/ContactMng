using ContactManagement.Model;
using ContactManagement.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Data
{
    public interface IContactRepository
    {
        List<ContactModels> GetAll(string compCode);
        ContactModels GetById(int id);
        bool Save(int id, ContactModels model);
        bool Delete(int id);
        string GetNewId(string comp_code);
        List<SelectModel> GetContactSelectModels(string comp_code);
    }
}
