using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Service
{
    public interface ICustomerService
    {
        int AuthenticateCustomer(string username, string password);
        void GetAllCustomers();
        Customer GetCustomerById(int customerId);

        Customer GetCustomerByUsername(string name);

        void RegisterCustomer();

        void UpdateCustomer();

        void deleteCustomer();


    }

}
