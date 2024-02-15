using CarConnect.Exceptions;
using CarConnect.Model;
using CarConnect.Repository;
using CarConnect.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Service
{
    public class CustomerService : ICustomerService
    {
        readonly ICustomerRepository _customerRepository;

        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }

       

        public int AuthenticateCustomer(string user, string pass)
        {
            try
            {
                Customer userExists = _customerRepository.GetCustomerByUsername(user);
                Console.WriteLine(userExists);
                if (userExists != null)
                {
                    if (userExists.Password == pass)
                    {
                        return userExists.CustomerID;
                    }
                    else
                    {
                        return -1;

                    }
                }
                else
                {
                    throw new CustomerNotFoundException("Invalid username");
                }

            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return -1;
        }

        public void GetAllCustomers()
        {
            try
            {
                List<Customer> customers = _customerRepository.GetAllCustomers();
                if(customers.Count > 0 ) {
                    foreach (Customer customer in customers)
                    {
                        Console.WriteLine(customer);
                        Console.WriteLine("\n");
                    }
                }
                else
                {
                    throw new CustomerNotFoundException("No customers Found");
                }
                
            }catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Customer GetCustomerById(int customerId)
        {
            try
            {
                Customer customer = _customerRepository.GetCustomerById(customerId);
                if (customer == null)
                {
                    throw new CustomerNotFoundException("Invalid id");
                }
                return customer;
            }catch (CustomerNotFoundException cnfe)
            {
                Console.WriteLine(cnfe.Message);
            }
            return null;
        }


        public Customer GetCustomerByUsername(string name)
        {
            try
            {
                Customer customer = _customerRepository.GetCustomerByUsername(name);
                if (customer == null)
                {
                    throw new CustomerNotFoundException("Invalid username");
                }
                return customer;
            }catch(CustomerNotFoundException cnfe)
            {
                Console.WriteLine(cnfe.Message);
            }
            return null;
        }

        public void RegisterCustomer()
        {
            Customer customer = new Customer();
            Console.WriteLine("Enter Customer ID: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input for Customer ID. Please enter a valid integer.");
            }
            customer.CustomerID = id;
            Console.WriteLine("Enter First Name: ");
            customer.FirstName = Console.ReadLine();

            Console.WriteLine("Enter Last Name: ");
            customer.LastName = Console.ReadLine();

            Console.WriteLine("Enter Email: ");
            string email = Console.ReadLine();
            if (!email.Contains("@"))
            {
                Console.WriteLine("Invalid email format. Please enter a valid email address.");
            }
            customer.Email = email;

            Console.WriteLine("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();
            if (!(phoneNumber.All(char.IsDigit)))
            {
                Console.WriteLine("Invalid phone number format. Please enter a valid phone number.");
            }
            customer.PhoneNumber = phoneNumber;
            Console.WriteLine("Enter Username: ");
            customer.UserName = Console.ReadLine();

            Console.WriteLine("Enter Address: ");
            customer.Address = Console.ReadLine();

            Console.WriteLine("Enter Password: ");
            customer.Password = Console.ReadLine();

            customer.RegistrationDate = DateTime.Now;

            try
            {
                if (_customerRepository.RegisterCustomer(customer))
                {
                    Console.WriteLine("Customer Registered Successfully, Login with your credentials");
                }
                else
                {
                    Console.WriteLine("Customer not Registered, Try again");      
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateCustomer()
        {
            Console.WriteLine("Enter ID to Update: ");
            int id = int.Parse(Console.ReadLine());
            Customer customer = _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                throw new AdminNotFoundException("Invalid id");
            }
            Console.WriteLine("Enter the details below to update:");

            Console.WriteLine("Phone number (leave blank to skip): ");
            string phoneNumber = Console.ReadLine();

            Console.WriteLine("First name (leave blank to skip): ");
            string firstName = Console.ReadLine();

            Console.WriteLine("Last name (leave blank to skip): ");
            string lastName = Console.ReadLine();

            Console.WriteLine("Email (leave blank to skip): ");
            string email = Console.ReadLine();
            customer.PhoneNumber = (string.IsNullOrEmpty(phoneNumber) ? "Not updated" : phoneNumber);
            
            customer.FirstName = (string.IsNullOrEmpty(firstName) ? "Not updated" : firstName);
            customer.LastName = (string.IsNullOrEmpty(lastName) ? "Not updated" : lastName);
            customer.Email = (string.IsNullOrEmpty(email) ? "Not updated" : email);
            try
            {
                if (_customerRepository.UpdateCustomerDetailsByID(customer, id))
                {
                    Console.WriteLine("Updated successfully");
                }
                else
                {
                    Console.WriteLine("Updation failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void deleteCustomer()
        {
            try
            {
                Console.WriteLine("Enter Id to delete: ");
                int id = int.Parse(Console.ReadLine());
                if (_customerRepository.DeleteCustomerDetailsByID(id))
                {
                    Console.WriteLine("Successfully Deleted");
                }
                else
                {
                    throw new CustomerNotFoundException($"Customer with id: {id} not found");
                }
            }catch(CustomerNotFoundException cnfe)
            {
                Console.WriteLine(cnfe.Message);
            }catch(Exception ex) { Console.WriteLine(ex.Message); }
        }


    }
}
