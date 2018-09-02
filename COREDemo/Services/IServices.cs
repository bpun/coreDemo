using Infastructure.Models;
using System.Collections.Generic;

namespace COREDemo.Services
{
    public interface IServices
    {
        List<Customer> GetCustomerList();
    }
}
