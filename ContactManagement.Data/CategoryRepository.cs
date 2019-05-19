using ContactManagement.DAL;
using ContactManagement.Model;
using ContactManagement.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Data
{
    public static class CategoryRepository
    {

        public static List<CategoryModels> GetAll()
        {
            try
            {
                SqlDatabase db = new SqlDatabase();
                var obList = new List<CategoryModels>();
                string sql = String.Format(@"Select * from tblCategory order by Title");
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CategoryModels ob = new CategoryModels();
                    ob.CategoryId = (dr["CategoryId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["CategoryId"]);
                    ob.Title = (dr["Title"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Title"]);
                    ob.Description = (dr["Description"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["Description"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public static bool Save(int id, CategoryModels model)
        {
            string sql = "";
            if (id > 0)
            {
                sql = String.Format(@"update tblCategory Set Title='{0}',Description= '{1}' where CategoryId={2}", model.Title, model.Description, model.CategoryId);
            }
            else
            {
                sql = String.Format(@"INSERT INTO tblCategory(Title,Description) Values ('{0}','{1}')", model.Title, model.Description);
            }
            SqlDatabase db = new SqlDatabase();
            return db.ExecNoneQuery(sql) > 0;
        }
        
        public static bool Delete(int id)
        {
            SqlDatabase db = new SqlDatabase();
            string sql = string.Format(@"delete from tblCategory where CategoryId='{0}'", id);
            return db.ExecNoneQuery(sql) > 0;
        }
    }
}
