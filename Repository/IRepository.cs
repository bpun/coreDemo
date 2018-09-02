using Infastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IRepository
    {
        List<Customer> GetCustomerList();
    }
}
