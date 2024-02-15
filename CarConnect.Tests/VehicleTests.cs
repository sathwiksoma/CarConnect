using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Model;
using CarConnect.Repository;

namespace carconnect.test
{
    public class VehicleTests
    {
        VehicleRepository _vehicleRepository;
        [SetUp]

        public void Setup()
        {
            _vehicleRepository = new VehicleRepository();
        }

        [Test]
        public void GetAllVehiclesTestWhenNotNull()
        {
            Assert.IsNotNull(_vehicleRepository.GetAllVehicles());
        }

        [Test]
        public void GetAvailableVehiclesTestWhenNotNull()
        {
            Assert.IsNotNull(_vehicleRepository.GetAvailableVehicles());
        }

        [Test]
        public void UpdateVehiclesTestWhenNotNull()
        {
            Vehicle vehicle = new Vehicle() { Availability = true, DailyRate = 500, RegistrationNumber = "TS36l5555" };

            Assert.IsNotNull(_vehicleRepository.UpdateVehicle(vehicle));
        }

        [Test]
        public void AddVehiclesTestWhenNotNull()
        {
            Vehicle vehicle = new Vehicle() { Model = "Spender", Make = "Hero", Year = 2022, Color = "Blue", Availability = true, DailyRate = 1000, RegistrationNumber = "JK56L0023" };

            Assert.IsNotNull(_vehicleRepository.AddVehicle(vehicle));
        }
    }
}