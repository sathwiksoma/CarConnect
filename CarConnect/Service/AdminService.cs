using CarConnect.Exceptions;
using CarConnect.Model;
using CarConnect.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Service
{
    public class AdminService : IAdminService
    {
        readonly IAdminRepository _adminRepository;
        public AdminService()
        {
            _adminRepository = new AdminRepository();
        }
        public bool Authenticate(string username, string password)
        {
            try
            {
                Admin admin = _adminRepository.Authenticate(username, password);
                if (admin != null)
                {
                    if(password == admin.Password)
                    {
                        Console.WriteLine("Admin Authentication successful! LOGGED IN");
                        Thread.Sleep(2000);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    throw new AuthenticationException("Admin Not found");
                }
            }catch (AuthenticationException anfe) {
                Console.WriteLine(anfe.Message);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public void GetAdminById(int id)
        {
            try
            {
                Admin admin = _adminRepository.GetAdminById(id);
                if (admin != null)
                {
                    Console.Clear();
                    Console.WriteLine($"\t\t\t\tDetails of Admin ID : {id}\n");
                    Console.WriteLine($"  FirstName         = {admin.FirstName}");
                    Console.WriteLine($"  LastName          = {admin.LastName}");
                    Console.WriteLine($"  Email             = {admin.Email}");
                    Console.WriteLine($"  PhoneNumber       = {admin.PhoneNumber}");
                    Console.WriteLine($"  UserName          = {admin.UserName}");
                    Console.WriteLine($"  Password          = {admin.Password}");
                    Console.WriteLine($"  Role              = {admin.Role}");
                    Console.WriteLine($"  JoinDate          = {admin.JoinDate.ToShortDateString()}");

                }
                else
                {
                    throw new AdminNotFoundException($"No Admin is present with ID: {id}.");
                }
            }
            catch(AdminNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void GetAdminByUsername(string username)
        {
            try
            {
                Admin admin = _adminRepository.GetAdminByUsername(username);
                if (admin != null)
                {
                    Console.Clear();
                    Console.WriteLine($"\t\t\t\tDetails of Username : {username}\n");
                    Console.WriteLine($"  FirstName         = {admin.FirstName}");
                    Console.WriteLine($"  LastName          = {admin.LastName}");
                    Console.WriteLine($"  Email             = {admin.Email}");
                    Console.WriteLine($"  PhoneNumber       = {admin.PhoneNumber}");
                    Console.WriteLine($"  UserName          = {admin.UserName}");
                    Console.WriteLine($"  Password          = {admin.Password}");
                    Console.WriteLine($"  Role              = {admin.Role}");
                    Console.WriteLine($"  JoinDate          = {admin.JoinDate.ToShortDateString()}");

                }
                else
                {
                    throw new AdminNotFoundException($"No Admin is present with username: {username}.");
                }
            }catch(AdminNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateAdmin(string username, Admin admin)
        {
            try
            {
                if (_adminRepository.UpdateAdmin(admin, username))
                {
                    Console.WriteLine("Updated successfully");
                }
                else
                {
                    throw new  AdminNotFoundException("Updation failed");
                }
            }catch (AdminNotFoundException afe) {
                Console.WriteLine(afe.Message);
            }
        }
    

        public void RegisterAdmin()
        {
            Admin admin = new Admin();
            Console.WriteLine("Enter Admin ID: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input for Customer ID. Please enter a valid integer.");
                return;
            }
            admin.AdminID = id;
            Console.WriteLine("Enter First Name: ");
            admin.FirstName = Console.ReadLine();

            Console.WriteLine("Enter Last Name: ");
            admin.LastName = Console.ReadLine();

            Console.WriteLine("Enter Email: ");
            string email = Console.ReadLine();
            if (!email.Contains("@"))
            {
                Console.WriteLine("Invalid email format. Please enter a valid email address.");
                return;
            }
            admin.Email = email;

            Console.WriteLine("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();
            if (!(phoneNumber.All(char.IsDigit)))
            {
                Console.WriteLine("Invalid phone number format. Please enter a valid phone number.");
                return;
            }
            admin.PhoneNumber = phoneNumber;
            Console.WriteLine("Enter Username: ");
            admin.UserName = Console.ReadLine();

            Console.WriteLine("Enter Password: ");
            admin.Password = Console.ReadLine();

            admin.Role = "admin";

            admin.JoinDate = DateTime.Now;

            try
            {
                if (_adminRepository.RegisterAdmin(admin))
                {
                    Console.WriteLine("Admin Registered Successfully, Login with your credentials");
                    return;
                }
                else
                {
                    Console.WriteLine("Admin not Registered, Try again");
                    return;
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteAdmin()
        {
            try
            {
                Console.WriteLine("Enter Id to delete");
                int id = int.Parse(Console.ReadLine());
                if (_adminRepository.DeleteAdminByID(id))
                {
                    Console.WriteLine("Successfully Deleted");
                }
                else
                {
                    throw new AdminNotFoundException("Id is not correct");
                }
            }
            catch (InvalidInputException)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for the ID.");
            }
            catch (AdminNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }


        public void UpdateAdmin()
        {
            Console.WriteLine("Enter ID to Update: ");
            int id = int.Parse(Console.ReadLine());
            Admin admin = _adminRepository.GetAdminById(id);
            if (admin == null)
            {
                throw new AdminNotFoundException("Invalid id");
            }
            Console.WriteLine("Enter the details below to update:");

            Console.WriteLine("Phone number (leave blank to skip): ");
            string phoneNumber = Console.ReadLine();

            Console.WriteLine("Role (leave blank to skip): ");
            string role = Console.ReadLine();

            Console.WriteLine("First name (leave blank to skip): ");
            string firstName = Console.ReadLine();

            Console.WriteLine("Last name (leave blank to skip): ");
            string lastName = Console.ReadLine();

            Console.WriteLine("Email (leave blank to skip): ");
            string email = Console.ReadLine();
            admin.PhoneNumber = (string.IsNullOrEmpty(phoneNumber) ? "Not updated" : phoneNumber);
            admin.Role = (string.IsNullOrEmpty(role) ? "Not updated" : role);
            admin.FirstName = (string.IsNullOrEmpty(firstName) ? "Not updated" : firstName);
            admin.LastName = (string.IsNullOrEmpty(lastName) ? "Not updated" : lastName);
            admin.Email = (string.IsNullOrEmpty(email) ? "Not updated" : email);
            try
            {
                if (_adminRepository.UpdateAdmin(admin, admin.UserName))
                {
                    Console.WriteLine("Updated Successfully");
                }
                else
                {
                    throw new AdminNotFoundException($"No admin with the id {id}");
                }
            }catch(AdminNotFoundException anfe)
            {
                Console.WriteLine(anfe.Message);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
