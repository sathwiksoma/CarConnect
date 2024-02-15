using CarConnect.Exceptions;
using CarConnect.Model;
using CarConnect.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public string connectionString;
        SqlCommand cmd;
        public CustomerRepository() {
            connectionString = DBConnectionUtility.GetConnectedString();
            cmd = new SqlCommand();
        }

        public bool AuthenticateCustomer(string username, string password)
        {
            try
            {
                var userExists = GetCustomerByUsername(username);
                if (userExists != null)
                {
                    if (userExists.Password == password)
                    {
                        Console.WriteLine("Logged in successfully");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid password");
                        return false;
                    }
                }
                else
                {
                    throw new CustomerNotFoundException($"Invalid Username");
                }
            }
            catch (CustomerNotFoundException e)
            {
                Console.WriteLine($"Error : {e.Message}");
                return false;
            }
        }

        public bool DeleteCustomerDetailsByID(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Delete from customer where CustomerID=@id";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = conn;
                    conn.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    return rowsEffected > 0;
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> ret = new List<Customer>();
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    cmd.CommandText = "select * from customer";
                    cmd.Connection = conn;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.CustomerID = (int)reader["CustomerID"];
                        customer.FirstName = (string)reader["FirstName"];
                        customer.LastName = (string)reader["LastName"];
                        customer.Email = (string)reader["Email"];
                        customer.PhoneNumber = (string)reader["PhoneNumber"];
                        customer.Address = (string)reader["Address"];
                        customer.UserName = (string)reader["Username"];
                        customer.Password = (string)reader["Password"];
                        customer.RegistrationDate = (DateTime)reader["RegistrationDate"];
                        ret.Add(customer);
                    }
                }
                
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            return ret;

        }

        public Customer GetCustomerById(int customerId)
        {
            try
            {
                Customer customer = new Customer();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select * from Customer where CustomerID=@cid";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@cid", customerId);
                    cmd.Connection= conn;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            customer.CustomerID = (int)reader["CustomerID"];
                            customer.FirstName = (string)reader["FirstName"];
                            customer.LastName = (string)reader["LastName"];
                            customer.Email = (string)reader["Email"];
                            customer.PhoneNumber = (string)reader["PhoneNumber"];
                            customer.Address = (string)reader["Address"];
                            customer.UserName = (string)reader["Username"];
                            customer.Password = (string)reader["Password"];
                            customer.RegistrationDate = (DateTime)reader["RegistrationDate"];
                        }
                        return customer;
                    }
                    
                    
                }
            }
            catch
            {
                throw new DatabaseConnectionException("Problem while connecting database");
            }
            return null;

        }

        public Customer GetCustomerByUsername(string name)
        {
            Customer customer = new Customer();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from Customer where Username=@u_name";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@u_name", name);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        customer.CustomerID = (int)reader["CustomerID"];
                        customer.FirstName = (string)reader["FirstName"];
                        customer.LastName = (string)reader["LastName"];
                        customer.Email = (string)reader["Email"];
                        customer.PhoneNumber = (string)reader["PhoneNumber"];
                        customer.Address = (string)reader["Address"];
                        customer.UserName = (string)reader["Username"];
                        customer.Password = (string)reader["Password"];
                        customer.RegistrationDate = (DateTime)reader["RegistrationDate"];

                    }
                    return customer;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool RegisterCustomer(Customer customer)
        {

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "insert into Customer(CustomerID,FirstName,LastName,Email,PhoneNumber,Username,Password,RegistrationDate,address) values(@id,@fName,@lName,@email,@phone,@user,@pw,@date,@address)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id",customer.CustomerID);
                    cmd.Parameters.AddWithValue("@fName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@lName", customer.LastName);
                    cmd.Parameters.AddWithValue("@email", customer.Email);
                    cmd.Parameters.AddWithValue("@phone", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("@user", customer.UserName);
                    cmd.Parameters.AddWithValue("@pw", customer.Password);
                    cmd.Parameters.AddWithValue("@date", customer.RegistrationDate);
                    cmd.Parameters.AddWithValue("@address", customer.Address);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    return rowsEffected > 0;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("duplicate key value"))
                {
                    Console.WriteLine($"Username:{customer.UserName} already exists");
                }
            }
            return false;
        }

        public bool UpdateCustomerDetailsByID(Customer customerData, int id)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "UPDATE Customer SET PhoneNumber=@phone, FirstName=@fname, LastName=@lname, Email=@eml WHERE CustomerID=@iid";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@phone", customerData.PhoneNumber);
                    cmd.Parameters.AddWithValue("@iid", id);
                    cmd.Parameters.AddWithValue("@fname", customerData.FirstName);
                    cmd.Parameters.AddWithValue("@lname", customerData.LastName);
                    cmd.Parameters.AddWithValue("@eml", customerData.Email);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    int updatedRows = cmd.ExecuteNonQuery();
                    Console.WriteLine(updatedRows);
                    return updatedRows > 0;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
