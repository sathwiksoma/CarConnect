using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Repository
{
    internal interface IReservationRepository
    {
        List<Reservation> GetAllReservations();
        Reservation GetReservationById(int reservationId);
        Reservation GetReservationsByCustomerId(int customerId);
        bool CreateReservation(Reservation reservationData);
        bool UpdateReservation(int id,string status);
        bool CancelReservation(int reservationId);

        decimal CalculateTotalCost(DateTime startDate, DateTime endDate, int vehicleId);

    }
}
