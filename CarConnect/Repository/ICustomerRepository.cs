using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Repository
{
    internal interface ICustomerRepository
    {
        bool RegisterCustomer(Customer customer);
        Customer GetCustomerById(int customerId);
        Customer GetCustomerByUsername(string name);
        List<Customer> GetAllCustomers();
        bool UpdateCustomerDetailsByID(Customer customerData,int id);
        bool DeleteCustomerDetailsByID(int id);
        bool AuthenticateCustomer(string username, string password);

        
    }
}
