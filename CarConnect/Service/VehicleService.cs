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
    public class VehicleService : IVehicleService
    {
        readonly IVehicleRepository _vehicleRepository;
        public VehicleService()
        {
            _vehicleRepository = new VehicleRepository();
        }

        public void GetVehicleById(int vehicleId)
        {
            try
            {
                Vehicle vehicle = _vehicleRepository.GetVehicleById(vehicleId);
                if (vehicle != null)
                {
                    Console.Clear();
                    Console.WriteLine(vehicle);
                }
                else
                {
                    throw new VehicleNotFoundException($"No Vehicle is present with ID: {vehicleId}.");
                }
            }
            catch (VehicleNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetAllVehicles()
        {
            List<Vehicle> vehicles = _vehicleRepository.GetAllVehicles();
            foreach (Vehicle vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }
        }

        public void AddVehicle()
        {
            try
            {
                Vehicle newVehicle = new Vehicle();

                Console.WriteLine("Id: ");
                newVehicle.VehicleID = int.Parse(Console.ReadLine());

                Console.WriteLine("Model: ");
                newVehicle.Model = Console.ReadLine();

                Console.WriteLine("Make: ");
                newVehicle.Make = Console.ReadLine();

                Console.WriteLine("Year: ");
                newVehicle.Year = int.Parse(Console.ReadLine());

                Console.WriteLine("Color: ");
                newVehicle.Color = Console.ReadLine();

                Console.WriteLine("Registration Number: ");
                newVehicle.RegistrationNumber = Console.ReadLine();

                Console.WriteLine("Availability (true/false): ");
                try
                {
                    if (bool.TryParse(Console.ReadLine(), out bool availability))
                    {
                        newVehicle.Availability = availability;
                    }
                    else
                    {
                        throw new InvalidDataException("Invalid input for Availability. Please enter true or false.");

                    }
                }catch (InvalidDataException ide)
                {
                    Console.WriteLine(ide.Message);
                }
                Console.Write("Daily Rate: ");
                try
                {
                    if (decimal.TryParse(Console.ReadLine(), out decimal dailyRate))
                    {
                        newVehicle.DailyRate = dailyRate;
                    }
                    else
                    {
                        throw new InvalidDataException("Invalid input for Daily Rate. Please enter a valid decimal number.");
                    }
                }catch(InvalidDataException ide)
                {
                    Console.WriteLine(ide.Message);
                }

                if (_vehicleRepository.AddVehicle(newVehicle))
                {
                    Console.WriteLine("Vehicle added successfully");
                }
                else
                {
                    Console.WriteLine("Vehicle not added");
                }
            }catch(InvalidDataException ide)
            {
                Console.WriteLine(ide.Message);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GetAvailableVehicles()
        {
            try
            {
                List<Vehicle> vehicles = _vehicleRepository.GetAvailableVehicles();
                foreach (Vehicle vehicle in vehicles)
                {
                    Console.WriteLine(vehicle);
                    Console.WriteLine("\n");
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        

        public void RemoveVehicle()
        {
            try
            {
                Console.Write("Enter the Vehicle ID: ");
                int vehicleId = int.Parse(Console.ReadLine());

                if (_vehicleRepository.RemoveVehicle(vehicleId))
                {
                    Console.WriteLine($"Vehicle with ID {vehicleId} has been deleted.");
                }
                else
                {
                    throw new VehicleNotFoundException($"Vehicle with ID {vehicleId} is not found");
                }
            }catch (VehicleNotFoundException vnfe)
            {
                Console.WriteLine(vnfe.Message);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateVehicle()
        {
            Console.WriteLine("To Update Vehicle Details, Enter VehicleId : ");
            int vehicleIdToUpdate = int.Parse(Console.ReadLine());

            Vehicle existingVehicle = _vehicleRepository.GetVehicleById(vehicleIdToUpdate);

            if (existingVehicle != null)
            {
                Console.WriteLine("Enter New Model Name: ");
                string newModelName = Console.ReadLine();

                Console.WriteLine("Enter New Make : ");
                string newMake = Console.ReadLine();

                Console.WriteLine("Enter New Year : ");
                string newYearInput = Console.ReadLine();
                int? newYear = string.IsNullOrEmpty(newYearInput) ? (int?)null : int.Parse(newYearInput);

                Console.WriteLine("Enter New Color : ");
                string newColor = Console.ReadLine();

                Console.WriteLine("Enter New Registration Number : ");
                string newRegistrationNumber = Console.ReadLine();

                Console.WriteLine("Enter New Availability (true/false) : ");
                string newAvailabilityInput = Console.ReadLine();
                bool? newAvailability = string.IsNullOrEmpty(newAvailabilityInput) ? (bool?)null : bool.Parse(newAvailabilityInput);

                Console.WriteLine("Enter New Daily Rate : ");
                string newDailyRateInput = Console.ReadLine();
                float? newDailyRate = string.IsNullOrEmpty(newDailyRateInput) ? (float?)null : float.Parse(newDailyRateInput);

                if (!string.IsNullOrEmpty(newModelName))
                    existingVehicle.Model = newModelName;

                if (!string.IsNullOrEmpty(newMake))
                    existingVehicle.Make = newMake;

                if (newYear.HasValue)
                    existingVehicle.Year = newYear.Value;

                if (!string.IsNullOrEmpty(newColor))
                    existingVehicle.Color = newColor;

                if (!string.IsNullOrEmpty(newRegistrationNumber))
                    existingVehicle.RegistrationNumber = newRegistrationNumber;

                if (newAvailability.HasValue)
                    existingVehicle.Availability = newAvailability.Value;

                if (newDailyRate.HasValue)
                    existingVehicle.DailyRate = (decimal)newDailyRate.Value;

                _vehicleRepository.UpdateVehicle(existingVehicle);
            }
            else
            {
                Console.WriteLine($"No vehicle found with VehicleId: {vehicleIdToUpdate}");
            }
        }
    }
}
