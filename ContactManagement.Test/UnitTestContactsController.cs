using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactManagement.Web.Controllers;
using ContactManagement.Model;
using System.Collections.Generic;

namespace ContactManagement.Test
{
    [TestClass]
    public class UnitTestContactsController
    {
        [TestMethod]
        public void getContacts()
        {
            var list = GetContacts();
            var controller = new ContactsController();
            var result = controller.GetContacts("AASDD") as List<ContactModels>;
            Assert.AreEqual(list.Count, result.Count);
        }

        public List<ContactModels> GetContacts()
        {
            var List = new List<ContactModels>();
            List.Add(new ContactModels { ContactId = 1, Name = "Afzal", Email = "", MobileNo = "", Address = "", CategoryId = 1, ProfilePicture = "", UserName = "AASDD" });
            List.Add(new ContactModels { ContactId = 2, Name = "Hossain", Email = "", MobileNo = "", Address = "", CategoryId = 2, ProfilePicture = "", UserName = "AASDD" });
            List.Add(new ContactModels { ContactId = 3, Name = "Noman", Email = "", MobileNo = "", Address = "", CategoryId = 3, ProfilePicture = "", UserName = "AASDD" });

            return List;
        }
    }
}
