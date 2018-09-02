using System;
using System.Collections.Generic;
using System.Text;
using Infastructure.Models;
using Infastructure.DAO;
using System.Data;
using System.Linq;

namespace Repository
{
    public class AppRepository : IRepository
    {
        private CoreDao coreDao;
        public AppRepository()
        {
            coreDao = new CoreDao();
        }
        public List<Customer> GetCustomerList()
        {
            var sql = "SELECT TOP(10) Cust.First_Name + ' ' + ISNULL(Cust.Middle_Name, '') + ' ' + Cust.Last_Name FullName, Cust.MSISDN Mobile FROM SW_TBL_PROFILE_CUST(NOLOCK) Cust WHERE Cust.First_Name IS NOT NULL";

            var dt = coreDao.ExecuteDataTable(sql);

            var customerList = new List<Customer>();
            customerList = (from DataRow dr in dt.Rows
                               select new Customer
                               {
                                   FullName = dr["FullName"].ToString(),
                                   Mobile = dr["Mobile"].ToString()
                               }).ToList();


            return customerList;
        }
    }
}
