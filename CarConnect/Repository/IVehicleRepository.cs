using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Repository
{
    internal interface IVehicleRepository
    {
        List<Vehicle> GetAllVehicles();
        Vehicle GetVehicleById(int vehicleId);
        List<Vehicle> GetAvailableVehicles();
        bool AddVehicle(Vehicle vehicleData);
        bool UpdateVehicle(Vehicle vehicle);
        bool RemoveVehicle(int vehicleId);
    }
}
