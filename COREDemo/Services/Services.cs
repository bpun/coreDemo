using Infastructure.Models;
using Repository;
using System.Collections.Generic;

namespace COREDemo.Services
{
    public class AppServices : IServices
    {
        private static IRepository _repo;
        public AppServices(IRepository repo)
        {
            _repo = repo;
        }
        public List<Customer> GetCustomerList()
        {
            return _repo.GetCustomerList();
        }
    }
}
