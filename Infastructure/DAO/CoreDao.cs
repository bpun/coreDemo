using System;
using System.Data;
using System.Data.SqlClient;

namespace Infastructure.DAO
{
    public class CoreDao
    {
        public static string Connectionconfig;
        private string _connectionString = "";
        public CoreDao()
        {
            _connectionString = Connectionconfig;
        }
        private string connectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        private int GetCommandTimeOut()
        {
            int cto = 0;
            try
            {
                int.TryParse("30", out cto);
                if (cto == 0)
                    cto = 30;
            }
            catch (Exception ex)
            {
                cto = 30;
            }
            return cto;
        }

        public DataSet ExecuteDataset(string sql)
        {
            var x = connectionString;
            var ds = new DataSet();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = GetCommandTimeOut();
                SqlDataAdapter da;
                try
                {
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    da = null;
                    cmd.Dispose();
                }
                return ds;
            }
        }

        public DataTable ExecuteDataTable(string sql)
        {
            using (var ds = ExecuteDataset(sql))
            {
                if (ds == null || ds.Tables.Count == 0)
                    return null;

                return ds.Tables[0];
            }
        }

        public DataRow ExecuteDataRow(string sql)
        {
            using (var ds = ExecuteDataset(sql))
            {
                if (ds == null || ds.Tables.Count == 0)
                    return null;

                if (ds.Tables[0].Rows.Count == 0)
                    return null;

                return ds.Tables[0].Rows[0];
            }
        }

        public String FilterString(string strVal)
        {
            var str = FilterQuote(strVal);

            if (str.ToLower() != "null")
                str = "'" + str + "'";

            return str;
        }

        public String FilterQuote(string strVal)
        {
            if (string.IsNullOrEmpty(strVal))
            {
                strVal = "";
            }
            var str = strVal.Trim();

            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace(";", "");
                //str = str.Replace(",", "");
                str = str.Replace("--", "");
                str = str.Replace("'", "");

                str = str.Replace("/*", "");
                str = str.Replace("*/", "");

                str = str.Replace(" select ", "");
                str = str.Replace(" insert ", "");
                str = str.Replace(" update ", "");
                str = str.Replace(" delete ", "");

                str = str.Replace(" drop ", "");
                str = str.Replace(" truncate ", "");
                str = str.Replace(" create ", "");

                str = str.Replace(" begin ", "");
                str = str.Replace(" end ", "");
                str = str.Replace(" char(", "");

                str = str.Replace(" exec ", "");
                str = str.Replace(" xp_cmd ", "");


                str = str.Replace("<script", "");

            }
            else
            {
                str = "null";
            }
            return str;
        }
    }
}
