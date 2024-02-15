using CarConnect.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CarConnect.Repository
{
    public class ReportRepository : IReportRepository
    {
        public string connectionString;
        SqlCommand cmd = null;
        public ReportRepository()
        {
            connectionString = DBConnectionUtility.GetConnectedString();
            cmd = new SqlCommand();
        }

        public void GenerateAdminReport()
        {
            DataTable reservationData = GetReservationHistory();
            DataTable vehicleUtilizationData = GetVehicleUtilizationData();
            DataTable revenueData = GetRevenueData();

            Console.WriteLine("Reservation History:");
            DisplayDataTable(reservationData);
            Console.WriteLine();

            Console.WriteLine("Vehicle Utilization:");
            DisplayDataTable(vehicleUtilizationData);
            Console.WriteLine();

            Console.WriteLine("Revenue:");
            DisplayDataTable(revenueData);
            
        }

        public DataTable GetReservationHistory()
        {
            DataTable reservationData = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.CommandText = @"SELECT r.ReservationID, c.FirstName + ' ' + c.LastName AS CustomerName, v.Model, v.Make, v.Year, v.Color, 
                                    r.StartDate, r.EndDate, r.TotalCost, r.Status
                                FROM Reservation r
                                INNER JOIN Customer c ON r.CustomerID = c.CustomerID
                                INNER JOIN Vehicle v ON r.VehicleID = v.VehicleID";
                cmd.Connection = connection;
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reservationData.Load(reader);
                
            }
            return reservationData;
        }

        public DataTable GetVehicleUtilizationData()
        {
            DataTable vehicleUtilizationData = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.CommandText = @"SELECT v.VehicleID, v.Model, COUNT(r.ReservationID) AS ReservationCount
                                FROM Vehicle v
                                LEFT JOIN Reservation r ON v.VehicleID = r.VehicleID
                                GROUP BY v.VehicleID, v.Model";
                cmd.Connection = connection;
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                vehicleUtilizationData.Load(reader);
            }
            return vehicleUtilizationData;
        }

        public DataTable GetRevenueData()
        {
            DataTable revenueData = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.CommandText = @"SELECT v.VehicleID, v.Model, SUM(r.TotalCost) AS TotalRevenue
                                FROM Vehicle v
                                INNER JOIN Reservation r ON v.VehicleID = r.VehicleID
                                GROUP BY v.VehicleID, v.Model";
                cmd.Connection = connection;
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                revenueData.Load(reader);
            }
            return revenueData;
        }

        public void DisplayDataTable(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn column in dataTable.Columns)
                {
                    Console.Write($"{column.ColumnName}: {row[column]} | ");
                }
                Console.WriteLine();
            }
        }
    }
}
