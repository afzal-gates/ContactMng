using ContactManagement.DAL;
using ContactManagement.Model;
using ContactManagement.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ContactManagement.Data
{
    public static class ContactRepository
    {

        public static bool Delete(int id)
        {
            SqlDatabase db = new SqlDatabase();
            string sql = string.Format(@"delete from tblContacts where ContactId='{0}'", id);
            return db.ExecNoneQuery(sql) > 0;
        }

        public static List<ContactModels> GetAll(string UserName)
        {
            try
            {
                SqlDatabase db = new SqlDatabase();
                var obList = new List<ContactModels>();
                string sql = String.Format(@"Select * from tblContacts c Join tblCategory cc ON c.CategoryId = cc.CategoryId where c.UserName='{0}' order by Name", UserName);
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ContactModels ob = new ContactModels();
                    ob.ContactId = (dr["ContactId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ContactId"]);
                    ob.CategoryId = (dr["CategoryId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["CategoryId"]);
                    ob.Name = (dr["Name"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Name"]);
                    ob.Email = (dr["Email"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Email"]);
                    ob.MobileNo = (dr["MobileNo"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MobileNo"]);
                    ob.Address = (dr["Address"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Address"]);
                    ob.ProfilePicture = (dr["ProfilePicture"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ProfilePicture"]);
                    ob.Title = (dr["Title"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Title"]);
                    ob.UserName = (dr["UserName"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["UserName"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ContactModels GetById(int id)
        {
            string sql = String.Format(@"select * from tblContacts where UserName='{0}' order by Name", id);
            try
            {
                SqlDatabase db = new SqlDatabase();
                var ob = new ContactModels();
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.ContactId = (dr["ContactId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ContactId"]);
                    ob.CategoryId = (dr["CategoryId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["CategoryId"]);
                    ob.Name = (dr["Name"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Name"]);
                    ob.Email = (dr["Email"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Email"]);
                    ob.MobileNo = (dr["MobileNo"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MobileNo"]);
                    ob.Address = (dr["Address"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Address"]);
                    ob.ProfilePicture = (dr["ProfilePicture"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ProfilePicture"]);
                    ob.UserName = (dr["UserName"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["UserName"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static bool Save(int id, ContactModels model)
        {
            string sql = "";
            if (id > 0)
            {
                sql = String.Format(@"update tblContacts Set Name='{0}',
                                    Email= '{1}',MobileNo='{2}',Address= '{3}',ProfilePicture='{4}',CategoryId={5} where ContactId={6}",
                                    model.Name, model.Email, model.MobileNo, model.Address, model.ProfilePicture, model.CategoryId, model.ContactId);
            }
            else
            {
                sql = String.Format(@"INSERT INTO tblContacts(CategoryId,Name,Email,MobileNo,Address,ProfilePicture,UserName)
                                        Values ({0},'{1}','{2}','{3}','{4}','{5}','{6}')",
                    model.CategoryId, model.Name, model.Email, model.MobileNo, model.Address, model.ProfilePicture, model.UserName==null? HttpContext.Current.User.Identity.ToString(): model.UserName);
            }
            SqlDatabase db = new SqlDatabase();
            return db.ExecNoneQuery(sql) > 0;
        }

        public static List<SelectModel> GetContactSelectModels(string comp_code)
        {

            try
            {
                SqlDatabase db = new SqlDatabase();
                var obList = new List<SelectModel>();
                string sql = String.Format("select ContactId,Name from ContactModels where UserName='{0}'", comp_code);
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SelectModel ob = new SelectModel();
                    ob.Value = (dr["ContactId"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ContactId"]);
                    ob.Text = (dr["Name"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Name"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

