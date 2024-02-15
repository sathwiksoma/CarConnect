using CarConnect.Exceptions;
using CarConnect.Model;
using CarConnect.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        public string connectionString;
        SqlCommand cmd;
        public ReservationRepository()
        {
            connectionString = DBConnectionUtility.GetConnectedString();
            cmd = new SqlCommand();
        }

        public bool CancelReservation(int reservationId)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "delete from Reservation where ReservationID=@id";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id",reservationId);
                    cmd.Connection = conn;
                    conn.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    return rowsEffected > 0;
                }
            }
            catch
            {
                throw new DatabaseConnectionException("Unable to establish a connection to the database");
            }
        }

        public bool CreateReservation(Reservation reservation)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                
                cmd.CommandText = "insert into Reservation(ReservationID, CustomerID,VehicleID,StartDate,EndDate,TotalCost,Status) values(@rid,@cid,@vid,@sd,@ed,@tc,@st)";
                cmd.Parameters.Clear();
                
                cmd.Parameters.AddWithValue("@rid", reservation.ReservationID);
                cmd.Parameters.AddWithValue("@cid", reservation.CustomerID);
                cmd.Parameters.AddWithValue("@vid", reservation.VehicleID);
                cmd.Parameters.AddWithValue("@sd", reservation.StartDate);
                cmd.Parameters.AddWithValue("@ed", reservation.EndDate);
                cmd.Parameters.AddWithValue("@tc", reservation.TotalCost);
                cmd.Parameters.AddWithValue("@st", reservation.Status);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int createReservationStatus;
                try
                {
                    createReservationStatus = cmd.ExecuteNonQuery();
                    return createReservationStatus > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
                return false;
            }
        }

        public List<Reservation> GetAllReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            try { 
            
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "select * from Reservation";
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Reservation reservation = new Reservation();
                            reservation.ReservationID = (int)reader["ReservationID"];
                            reservation.CustomerID = (int)reader["CustomerID"];
                            reservation.VehicleID = (int)reader["VehicleID"];
                            reservation.StartDate = (DateTime)reader["StartDate"];
                            reservation.EndDate = (DateTime)reader["EndDate"];
                            reservation.TotalCost = (decimal)reader["TotalCost"];
                            reservation.Status = (string)reader["Status"];
                            reservations.Add(reservation);
                        }
                    }
                    else
                    {
                        throw new ReservationException("No reservations found");
                    }

                }
            }
            catch (ReservationException ide)
            {
                Console.WriteLine(ide.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return reservations;
        }

        
        public Reservation GetReservationById(int reservationid)
        {

            Reservation reservation = new Reservation();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                
                cmd.CommandText = "select * from Reservation where ReservationID=@R_iid";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@R_iid", reservationid);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reservation.ReservationID = (int)reader["ReservationID"];
                        reservation.CustomerID = (int)reader["CustomerID"];
                        reservation.VehicleID = (int)reader["VehicleID"];
                        reservation.StartDate = (DateTime)reader["StartDate"];
                        reservation.EndDate = (DateTime)reader["EndDate"];
                        reservation.TotalCost = (decimal)reader["TotalCost"];
                        reservation.Status = (string)reader["Status"];

                    }
                    return reservation;
                }
                else
                {
                    return null;
                }
            }
        }

        public Reservation GetReservationsByCustomerId(int customerId)
        {

            Reservation reservation = new Reservation();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from Reservation where CustomerID=@CustomerId";
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reservation.ReservationID = (int)reader["ReservationID"];
                        reservation.CustomerID = (int)reader["CustomerID"];
                        reservation.VehicleID = (int)reader["VehicleID"];
                        reservation.StartDate = (DateTime)reader["StartDate"];
                        reservation.EndDate = (DateTime)reader["EndDate"];
                        reservation.TotalCost = (decimal)reader["TotalCost"];
                        reservation.Status = (string)reader["Status"];

                    }
                    return reservation;
                }
                else
                {
                    return null; 
                }
            }
        }

        public bool UpdateReservation(int reservationId, string status)
        {
            cmd.Parameters.Clear();
            try
            {
                var exists = GetReservationById(reservationId);
                if (exists != null)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        cmd.CommandText = "UPDATE Reservation set Status=@status where ReservationID=@Id";
                        cmd.Parameters.AddWithValue("@Id", reservationId);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();
                        int updateReservationStatus = cmd.ExecuteNonQuery();
                        exists.Status = status;
                        return true;
                    }
                }
                else
                {
                    throw new ReservationException($"ReservationID:{reservationId} not found");
                }
            }
            catch (ReservationException re)
            {
                Console.WriteLine($"Error:{re.Message}");
            }
            return false;
        }

        public decimal CalculateTotalCost(DateTime startDate, DateTime endDate, int vehicleId)
        {
            decimal totalCost = 0;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT DailyRate FROM Vehicle WHERE VehicleID = @VehicleID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            decimal dailyRate = (decimal)reader["DailyRate"];
                            var numberOfDays = (endDate - startDate).Days;
                            totalCost = Math.Max(0, dailyRate * numberOfDays);
                        }
                    }
                }
                return totalCost;
            }
            catch (SqlException se)
            {
                Console.WriteLine("An error occurred: " + se.Message);
            }
            catch (DatabaseConnectionException dce)
            {
                Console.WriteLine(dce.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return totalCost;
        }

    }
}

