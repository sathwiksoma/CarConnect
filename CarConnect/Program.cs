using CarConnect.Exceptions;
using CarConnect.Model;
using CarConnect.Repository;
using CarConnect.Service;
using System;
using System.Text;
using System.Threading.Channels;

namespace CarConnect
{
    internal class Program
    {        
        static void Main()
        {

            Console.WriteLine();
            Console.WriteLine("\t\t\t\t\t                                   Car Connect                     ");
            Console.WriteLine("\t\t\t\t\t                              A Car Rental Platform                      ");
            Console.WriteLine();
            Console.WriteLine(" Please select your role : ");
            Console.WriteLine("1. Customer");
            Console.WriteLine("2. Admin");
            Console.WriteLine("3. Guest");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice : ");

            int roleChoice;
            while (!int.TryParse(Console.ReadLine(), out roleChoice) || roleChoice < 1 || roleChoice > 4)
            {
                Console.WriteLine("Invalid choice. Please enter a valid number.");
            }

            Console.Clear();
            switch (roleChoice)
            {
                case 1:
                    CustomerLogin();
                    break;
                case 2:
                    AdminLogin();
                    break;
                case 3:
                    GuestLogin();
                    break;
                case 4:
                    Console.WriteLine("Exiting the application.!");
                    break;
            }
        }

        static void CustomerLogin()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Customer Login");
                Console.WriteLine();
                Console.WriteLine("         1. Login              ");
                Console.WriteLine("         2. Register           ");
                Console.WriteLine("         3. Exit               ");
                Console.WriteLine();
                Console.Write("Enter your choice: ");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number.");
                    Console.Write("Enter your choice: ");
                }

                Console.Clear();
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("\nCustomer Login");
                        Console.Write("Enter your username: ");
                        string username = Console.ReadLine();
                        Console.Write("Enter your password: ");
                        string password = Console.ReadLine();
                        int id = customerService.AuthenticateCustomer(username, password);
                        if (id != -1)
                        {
                            Console.WriteLine("\nLogin successful!");
                            CustomerMenu(username, id);
                        }
                        else
                        {
                            Console.WriteLine("\nLogin failed. Invalid credentials.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Customer Registration");
                        customerService.RegisterCustomer();
                        Thread.Sleep(2000);
                        break;
                    case 3:
                        return;
                }
            }
        }
       

        static void AdminLogin()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("        Admin Login      ");
            Console.WriteLine();

            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            if (adminService.Authenticate(username, password))
            {
                Console.WriteLine("\nLogin successful!");
                AdminMenu();
            }
            else
            {
                Console.WriteLine("\nLogin failed. Invalid credentials.");
            }
        }

        static void GuestLogin()
        {
            Console.Clear();
            Console.WriteLine("You have logged in as a guest\n");
            GuestMenu();
        }

        static void CustomerMenu(string username, int id)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("        Customer Menu       ");
                Console.WriteLine();
                Console.WriteLine("            1. Get All Vehicles                       ");
                Console.WriteLine("            2. Get Available Vehicles                 ");
                Console.WriteLine("            3. Create a Reservation                   ");
                Console.WriteLine("            4. Get Reservation by Reservation Id      ");
                Console.WriteLine("            5. Get Reservation by Customer Id         ");
                Console.WriteLine("            6. Get Customer by Id                     ");
                Console.WriteLine("            7. Get Customer by Username               ");
                Console.WriteLine("            8. Logout                                 ");
                Console.WriteLine();
                Console.Write("Enter your choice: ");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 8)
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number.");
                }

                Console.Clear();
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        vehicleService.GetAllVehicles();
                        break;
                    case 2:
                        Console.Clear();
                        vehicleService.GetAvailableVehicles();
                        break;
                    case 3:
                        reservationService.CreateReservation(id);
                        break;
                    case 4:
                        Console.WriteLine("Enter Reservation Id");
                        int Id = int.Parse(Console.ReadLine());
                        reservationService.GetReservationById(Id);                       
                        break;
                    case 5:
                        Console.WriteLine("Enter Customer Id for Reservation Details");
                        int ID = int.Parse(Console.ReadLine());
                        reservationService.GetReservationsByCustomerId(ID);
                        break;
                    case 6:
                        Console.WriteLine("Enter Id for Customer Details");
                        int cid = int.Parse(Console.ReadLine());
                        Customer customer = customerService.GetCustomerById(cid);
                        Console.WriteLine(customer);
                        break;
                    case 7:
                        Console.WriteLine("Enter Customer Username for Details");
                        string u_name = Console.ReadLine();
                        Customer customerr = customerService.GetCustomerByUsername(u_name);
                        Console.WriteLine(customerr);
                        break;
                    case 8:
                        Console.WriteLine("Logging out");
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("          Admin Menu         ");
                Console.WriteLine();
                Console.WriteLine(" 1. Get Admin by id          ");
                Console.WriteLine(" 2. Get Admin by Username    ");
                Console.WriteLine(" 3. Register Admin           ");
                Console.WriteLine(" 4. Update an admin          ");
                Console.WriteLine(" 5. Delete an existing admin ");
                Console.WriteLine(" 6. Get all customers        ");
                Console.WriteLine(" 7. Update Customer          ");
                Console.WriteLine(" 8. Delete Customer          ");
                Console.WriteLine(" 9. Get Vehicle by id        ");
                Console.WriteLine(" 10. Add Vehicle             ");
                Console.WriteLine(" 11. Update Vehicle          ");
                Console.WriteLine(" 12. Remove Vehicle          ");
                Console.WriteLine(" 13. Update Reservation      ");
                Console.WriteLine(" 14. Delete Reservation      ");
                Console.WriteLine(" 15. Report Generation       ");
                Console.WriteLine(" 16. Logout                  ");
                Console.WriteLine();
                Console.Write("Enter your choice: ");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 16)
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number.");
                }

                Console.Clear();
                switch (choice)
                {
                    case 1:                        
                        Console.WriteLine("Get Admin by id");
                        Console.WriteLine("Enter Id");
                        int id = int.Parse(Console.ReadLine());
                        adminService.GetAdminById(id);
                        break;

                    case 2:
                        Console.WriteLine("Get Admin by Username");
                        Console.WriteLine("Enter Username");
                        string name = Console.ReadLine();
                        adminService.GetAdminByUsername(name);
                        break;

                    case 3:                        
                        Console.WriteLine("Register a new admin");
                        adminService.RegisterAdmin();
                        break;

                    case 4:                        
                        Console.WriteLine("Update an admin");
                        adminService.UpdateAdmin();
                        break;

                    case 5:                        
                        Console.WriteLine("Delete an existing admin");
                        adminService.DeleteAdmin();
                        break;

                    case 6:                        
                        Console.WriteLine("Get all customers");
                        customerService.GetAllCustomers();
                        break;

                    case 7:                        
                        Console.WriteLine("Update a customer by id");
                        customerService.UpdateCustomer();
                        break;

                    case 8:                        
                        Console.WriteLine("Delete a customer by id");
                        customerService.deleteCustomer();
                        break;

                    case 9:
                        Console.WriteLine("Get Vehicle by id");
                        Console.WriteLine("Enter Id");
                        int vid = int.Parse(Console.ReadLine());
                        vehicleService.GetVehicleById(vid);
                        break;

                    case 10:
                        Console.WriteLine("Add Vehicle");
                        vehicleService.AddVehicle();
                        break;

                    case 11:
                        Console.WriteLine("Update Vehicle");
                        vehicleService.UpdateVehicle();
                        break;

                    case 12:                       
                        Console.WriteLine("Remove Vehicle");
                        vehicleService.RemoveVehicle();
                        break;
                    case 13:
                        Console.WriteLine("Update Reservation");
                        reservationService.UpdateReservation();
                        break;

                    case 14:
                        Console.WriteLine("Delete Reservation");
                        reservationService.CancelReservation();
                        break;

                    case 15:
                        Console.WriteLine("Reports");
                        ReportMenu();
                        break;

                    case 16:
                        Console.WriteLine("Logging out");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid number.");
                        break;
                }

            }
        }
        

        static void GuestMenu()
        {
            while (true)
            {
                
                Console.WriteLine("\n Guest Menu ");
                Console.WriteLine("1. View Available vehicles");
                Console.WriteLine("2. Register as a customer");
                Console.WriteLine("3. Logout");
                Console.WriteLine("Enter Your choice: ");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number.");
                }

                Console.Clear();
                switch (choice)
                {
                    
                    case 1:
                        Console.WriteLine("Available Vehicles data");
                        vehicleService.GetAvailableVehicles();
                        break;
                    case 2:
                        CustomerLogin();
                        break;
                    case 3:
                        Console.WriteLine("Exiting ");
                        return;
                }
            }
        }
        static void ReportMenu()
        {
            while (true)
            {

                Console.WriteLine("\nReport Menu");
                Console.WriteLine("1. Get Reservation History");
                Console.WriteLine("2. Get Vehicle Utilization Info");
                Console.WriteLine("3. Get Revenue");
                Console.WriteLine("4. Logout");
                Console.WriteLine("Enter Your choice: ");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number.");
                }

                Console.Clear();
                switch (choice)
                {

                    case 1:
                        reportService.GetReservationHistory();
                        break;
                    case 2:
                        reportService.GetVehicleUtilizationData();
                        break;
                    case 3:
                        reportService.GetRevenueData();
                        break;
                    case 4:
                        Console.WriteLine("Exiting ");
                        return;
                }
            }
        }
        static IAdminService adminService = new AdminService();
        static ICustomerService customerService = new CustomerService();
        static IReservationService reservationService = new ReservationService();
        static IVehicleService vehicleService = new VehicleService();
        static IReportService reportService = new ReportService();
    }
}
            
        

