using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Model
{
    public class Reservation
    {
        private int _reservationID;
        private int _customerID;
        private int _vehicleID;
        private DateTime _startDate;
        private DateTime _endDate;
        private decimal _totalCost;
        private string _status;

        public int ReservationID
        {
            get { return _reservationID; }
            set { _reservationID = value; }
        }

        public int CustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }

        public int VehicleID
        {
            get { return _vehicleID; }
            set { _vehicleID = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public decimal TotalCost
        {
            get { return _totalCost; }
            set { _totalCost = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public Reservation() { }

        public Reservation(int reservationID, int customerID, int vehicleID, DateTime startDate, DateTime endDate, decimal totalCost, string status)
        {
            _reservationID = reservationID;
            _customerID = customerID;
            _vehicleID = vehicleID;
            _startDate = startDate;
            _endDate = endDate;
            _totalCost = totalCost;
            _status = status;
        }

        public override string ToString()
        {
            return $"VehicleID      :  {_vehicleID}\n" +
                   $"ReservationID  :  {_reservationID}\n" +
                   $"CustomerID     :  {_customerID}\n" +
                   $"StartDate      :  {_startDate.ToShortDateString()}\n" +
                   $"EndDate        :  {_endDate.ToShortDateString()}\n" +
                   $"TotalCost      :  {_totalCost}\n" +
                   $"Status         :  {_status}";
        }
    }

}
