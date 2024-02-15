using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Model
{
    public class Vehicle
    {
        private int _vehicleID;
        private string _model;
        private string _make;
        private int _year;
        private string _color;
        private string _registrationNumber;
        private bool _availability;
        private decimal _dailyRate;

        public int VehicleID
        {
            get { return _vehicleID; }
            set { _vehicleID = value; }
        }

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public string Make
        {
            get { return _make; }
            set { _make = value; }
        }

        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public string RegistrationNumber
        {
            get { return _registrationNumber; }
            set { _registrationNumber = value; }
        }

        public bool Availability
        {
            get { return _availability; }
            set { _availability = value; }
        }

        public decimal DailyRate
        {
            get { return _dailyRate; }
            set { _dailyRate = value; }
        }

        public Vehicle() { }

        public Vehicle(string model, string make, int year, string color, string registrationNumber, bool availability, decimal dailyRate, [Optional] int vehicleID)
        {
            _model = model;
            _make = make;
            _year = year;
            _color = color;
            _registrationNumber = registrationNumber;
            _availability = availability;
            _dailyRate = dailyRate;
            _vehicleID = vehicleID;
        }

        public override string ToString()
        {
            return $"  VehicleID          :  {_vehicleID}\n" +
                   $"  Model              :  {_model}\n" +
                   $"  Make               :  {_make}\n" +
                   $"  Year               :  {_year}\n" +
                   $"  Color              :  {_color}\n" +
                   $"  RegistrationNumber :  {_registrationNumber}\n" +
                   $"  Availability       :  {_availability}\n" +
                   $"  DailyRate          :  {_dailyRate}";
        }
    }

}
