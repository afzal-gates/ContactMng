using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.DAL
{
    public class SqlDatabase
    {
        public DataSet ExecuteStoredProcedure(List<CommandParameter> commandParameters, String spName)
        {
            string constr = ConfigurationManager.ConnectionStrings["AuthContext"].ConnectionString;
            var cn = new SqlConnection(constr);

            try
            {
                var cm = new SqlCommand(spName, cn)
                {
                    //BindByName = true,
                    CommandType = CommandType.StoredProcedure
                };

                cn.Open();
                SqlCommandBuilder.DeriveParameters(cm);
                cn.Close();

                foreach (CommandParameter commandParameter in commandParameters)
                {
                    if (cm.Parameters.Contains(commandParameter.ParameterName))
                    {
                        cm.Parameters[commandParameter.ParameterName].Value = commandParameter.Value;
                    }
                    else
                    {
                        throw new Exception("Store Procedure does not contain parameter : " + commandParameter.ParameterName);
                    }
                }

                var ds = new DataSet();
                var adap = new SqlDataAdapter(cm);
                adap.Fill(ds);

                var dt = new DataTable("OUTPARAM");
                dt.Columns.Add("KEY");
                dt.Columns.Add("VALUE");

                foreach (SqlParameter op in cm.Parameters)
                {
                    if (op.Direction == ParameterDirection.InputOutput || op.Direction == ParameterDirection.Output)
                    {
                        DataRow dr = dt.NewRow();
                        dr["KEY"] = op.ParameterName;
                        dr["VALUE"] = op.Value;
                        dt.Rows.Add(dr);
                    }
                }

                ds.Tables.Add(dt);
                cm.Dispose();
                adap.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                cn.Close();
            }
        }

        public DataSet ExecuteSQLStatement(String SQL)
        {
            string constr = ConfigurationManager.ConnectionStrings["AuthContext"].ConnectionString;
            var cn = new SqlConnection(constr);

            try
            {
                var cm = new SqlCommand(SQL, cn);

                cn.Open();
                cm.ExecuteReader();
                cn.Close();

                var ds = new DataSet();
                var adap = new SqlDataAdapter(cm);
                adap.Fill(ds);
                cm.Dispose();
                adap.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                cn.Close();
            }
        }
        public DataTable ExecWithSqlQuery(string query)
        {
            string constr = ConfigurationManager.ConnectionStrings["AuthContext"].ConnectionString;
            var cn = new SqlConnection(constr);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);
                DataTable dt = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if
                    (cn.State == ConnectionState.Open)
                {

                    cn.Close();
                }
            }
        }

        public int ExecNoneQuery(string query)
        {


            string constr = ConfigurationManager.ConnectionStrings["AuthContext"].ConnectionString;
            var cn = new SqlConnection(constr);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if
                    (cn.State == ConnectionState.Open)
                {

                    cn.Close();
                }
            }
        }
        public bool Exist(string query)
        {


            string constr = ConfigurationManager.ConnectionStrings["AuthContext"].ConnectionString;
            var cn = new SqlConnection(constr);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);
                object val = cmd.ExecuteScalar();
                int flg = Convert.ToInt32(val);

                return flg > 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if
                    (cn.State == ConnectionState.Open)
                {

                    cn.Close();
                }
            }
        }

    }


    //****Helper Class**********************************
    public class CommandParameter
    {
        public String ParameterName { get; set; }
        public Object Value { get; set; }
        public ParameterDirection Direction { get; set; }

        public CommandParameter()
        {
            Direction = ParameterDirection.Input;
        }
    }
}
