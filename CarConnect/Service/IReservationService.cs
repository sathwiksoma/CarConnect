using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Service
{
    public interface IReservationService
    {
        void GetReservationById(int reservationid);
        void GetReservationsByCustomerId(int customerId);
        void GetAllReservations();
        void CreateReservation(int id);
        void UpdateReservation();
        void CancelReservation();
        void CalculateTotalCost();
    }
}
