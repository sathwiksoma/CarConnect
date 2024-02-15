using CarConnect.Exceptions;
using CarConnect.Model;
using CarConnect.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Service
{
    public class ReservationService : IReservationService
    {
        readonly IReservationRepository reservationRepository;
        readonly IVehicleRepository vehicleRepository;
        public ReservationService()
        {
            reservationRepository = new ReservationRepository();
            vehicleRepository = new VehicleRepository();
        }

        public void CalculateTotalCost()
        {
            Console.WriteLine("Enter the reservation id");
            int id = int.Parse(Console.ReadLine());
            Reservation reservation = reservationRepository.GetReservationById(id);
            decimal totalCost = reservationRepository.CalculateTotalCost(reservation.StartDate, reservation.EndDate, reservation.VehicleID);
            Console.WriteLine($"The Total Cost of Reservation id ({reservation.ReservationID} : {totalCost})"); 
        }

        public void CancelReservation()
        {
            Console.WriteLine("Enter Id to delete");
            int id = int.Parse(Console.ReadLine());

            try
            {
                if (reservationRepository.CancelReservation(id))
                {
                    Console.WriteLine($"The reservation {id} is succesfully cancelled");
                }
                else { Console.WriteLine("The Resrvation cancellation is failed"); }
            }catch(Exception e) { Console.WriteLine(e.Message); }
        }

        public void CreateReservation(int id)
        {
            try
            {
                Console.WriteLine("To Create reservation");
                Console.WriteLine("Enter the ReservationID: ");
                int rid = int.Parse(Console.ReadLine()) ;
                Console.WriteLine("Enter the VehicleID: ");
                int vid = int.Parse(Console.ReadLine());
                Vehicle vehicle = new Vehicle();
                vehicle = vehicleRepository.GetVehicleById(vid);

                if (vehicle != null && !vehicle.Availability)
                {
                    throw new Exception("Vehicle is not available for reservation.");
                }

                Console.WriteLine("Enter the StartDate: ");
                DateTime sd = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter the EndDate: ");
                DateTime ed = DateTime.Parse(Console.ReadLine());
                decimal tc = reservationRepository.CalculateTotalCost(sd, ed, vid);
                string sts = "confirmed";
                Reservation reservation = new Reservation
                {
                    ReservationID = rid,
                    CustomerID = id,
                    VehicleID = vid,
                    StartDate = sd,
                    EndDate = ed,
                    TotalCost = tc,
                    Status = sts
                };
                reservationRepository.CreateReservation(reservation);
                Console.WriteLine("The ride is confirmed.");
                Console.WriteLine($"The total cost of the ride : {tc}Rs!!");
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GetAllReservations()
        {
            try
            {
                List<Reservation> reservations = reservationRepository.GetAllReservations();
                if (reservations != null)
                {
                    foreach (Reservation reservation in reservations)
                    {
                        Console.WriteLine(reservation);
                        Console.WriteLine("\n");
                    }
                }
                else
                {
                    throw new ReservationException("No reservations found");
                }
                
            }catch(ReservationException re)
            {
                Console.WriteLine(re.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void GetReservationById(int reservationId)
        {
            try
            {
                Reservation reservation = reservationRepository.GetReservationById(reservationId);
                if (reservation != null)
                {
                    Console.Clear();
                    Console.WriteLine($"\t\t\t\tDetails of Reservation ID : {reservationId}\n");
                    Console.WriteLine($"  CustomerId        = {reservation.CustomerID}");
                    Console.WriteLine($"  VehicleId         = {reservation.VehicleID}");
                    Console.WriteLine($"  StartDate         = {reservation.StartDate}");
                    Console.WriteLine($"  EndDate           = {reservation.EndDate}");
                    Console.WriteLine($"  TotalCost         = {reservation.TotalCost}");
                    Console.WriteLine($"  Status            = {reservation.Status}");
                }
                else
                {
                    throw new ReservationException($"No Reservation is present with ID: {reservationId}.");
                }
            }
            catch (ReservationException ex)
            {
                // Handle the exception
                Console.WriteLine(ex.Message);
            }
        }

        public void GetReservationsByCustomerId(int customerId)
        {
            try
            {
                Reservation reservation = reservationRepository.GetReservationsByCustomerId(customerId);
                if (reservation != null)
                {
                    Console.Clear();
                    Console.WriteLine($"Details of CustomerId : {customerId}\n");
                    Console.WriteLine($"  Reservation ID      = {reservation.ReservationID}");
                    Console.WriteLine($"  VehicleId           = {reservation.VehicleID}");
                    Console.WriteLine($"  StartDate           = {reservation.StartDate}");
                    Console.WriteLine($"  EndDate             = {reservation.EndDate}");
                    Console.WriteLine($"  TotalCost           = {reservation.TotalCost}");
                    Console.WriteLine($"  Status              = {reservation.Status}");
                }
                else
                {
                    throw new CustomerNotFoundException($"No Customer is present with CustomerId: {customerId}.");
                }
            }
            catch (CustomerNotFoundException ex)
            {
                // Handle the exception
                Console.WriteLine(ex.Message);
            }
        }


        public void UpdateReservation()
        {
            Console.WriteLine("Enter Id Of Reservation To Update the status");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Status to be Updated : ");
            string status = Console.ReadLine();
            try
            {
                 if (reservationRepository.UpdateReservation(id, status))
                 {
                      Console.WriteLine("Updated successful");
                 }
                 else
                 {
                      Console.WriteLine("Updation failed");
                 }
            } 
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
    }
}
