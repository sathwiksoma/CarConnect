using CarConnect.Exceptions;
using CarConnect.Model;
using CarConnect.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        public string connectionString;
        SqlCommand cmd = null;
        public VehicleRepository()
        {
            connectionString = DBConnectionUtility.GetConnectedString();
            cmd = new SqlCommand();
        }
        public bool AddVehicle(Vehicle vehicleData)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.CommandText = "insert into Vehicle(VehicleID, Model, Make,Year ,Color,RegistrationNumber,Availability,DailyRate) values(@id,@mdl,@mke,@year,@color,@reg_no,@availability,@daily_rate)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", vehicleData.VehicleID);
                cmd.Parameters.AddWithValue("@mdl", vehicleData.Model);
                cmd.Parameters.AddWithValue("@mke", vehicleData.Make);
                cmd.Parameters.AddWithValue("@year", vehicleData.Year);
                cmd.Parameters.AddWithValue("@color", vehicleData.Color);
                cmd.Parameters.AddWithValue("@reg_no", vehicleData.RegistrationNumber);
                cmd.Parameters.AddWithValue("@availability", vehicleData.Availability);
                cmd.Parameters.AddWithValue("@daily_rate", vehicleData.DailyRate);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int addVehicleStatus;
                try
                {
                    addVehicleStatus = cmd.ExecuteNonQuery();
                    return addVehicleStatus > 0;
                }
                catch (SqlException se)
                {
                    Console.WriteLine(se.Message);

                }catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return false;
            }
        }

        public List<Vehicle> GetAvailableVehicles()
        {
                List<Vehicle> vehicles = new List<Vehicle>();
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        cmd.CommandText = "select * from Vehicle WHERE Availability=1";
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Vehicle vehicle = new Vehicle();
                            vehicle.VehicleID = (int)reader["VehicleID"];
                            vehicle.Model = (string)reader["Model"];
                            vehicle.Make = (string)reader["Make"];
                            vehicle.Year = (int)reader["Year"];
                            vehicle.Color = (string)reader["Color"];
                            vehicle.RegistrationNumber = (string)reader["RegistrationNumber"];
                            vehicle.Availability = Convert.ToBoolean(reader["Availability"]);

                            vehicle.DailyRate = (decimal)reader["DailyRate"];
                            vehicles.Add(vehicle);
                        }
                    }
                }
                catch 
                {
                    throw new VehicleNotFoundException("Vehicle data not found");
                }

                return vehicles;
            }
        

        public Vehicle GetVehicleById(int id)
        {
            
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                Vehicle vehicle = new Vehicle();
                cmd.CommandText = "select * from Vehicle where VehicleID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vehicle.VehicleID = (int)reader["VehicleID"];
                        vehicle.Model = reader["Model"] != DBNull.Value ? (string)reader["Model"] : null;
                        vehicle.Make = (string)reader["Make"];
                        vehicle.Year = (int)reader["Year"];
                        vehicle.Color = (string)reader["Color"];
                        vehicle.RegistrationNumber = (string)reader["RegistrationNumber"];
                        vehicle.Availability = Convert.ToBoolean(reader["Availability"]);
                        vehicle.DailyRate = (decimal)reader["DailyRate"];

                    }
                return vehicle;
            }
            else
            {
                return null;
            }
        }
    }

        public bool RemoveVehicle(int vehicleId)
        {
            try
            {
                cmd.Parameters.Clear();
                var vehicleExists = GetVehicleById(vehicleId);
                if (vehicleExists != null)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();
                        cmd.CommandText = "delete from Vehicle where VehicleID=@VV_id";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@VV_id", vehicleId);
                        int deleteVehiclStatus = cmd.ExecuteNonQuery();
                        return deleteVehiclStatus > 0;
                    }

                }
                else
                {
                    throw new VehicleNotFoundException($"Vehicle ID:{vehicleId} doesn't exist");
                }
            }
            catch (VehicleNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return false;
        }

        public bool UpdateVehicle(Vehicle vehicle)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.CommandText = " UPDATE Vehicle SET Model = @modell, Make = @makee, Year = @yearr, Color = @colorr, RegistrationNumber = @reg_noo, Availability = @availabilityy, DailyRate = @daily_ratee WHERE VehicleId = @idd ";
                cmd.Parameters.AddWithValue("@modell", vehicle.Model);
                cmd.Parameters.AddWithValue("@makee", vehicle.Make);
                cmd.Parameters.AddWithValue("@yearr", vehicle.Year);
                cmd.Parameters.AddWithValue("@colorr", vehicle.Color);
                cmd.Parameters.AddWithValue("@reg_noo", vehicle.RegistrationNumber);
                cmd.Parameters.AddWithValue("@availabilityy", vehicle.Availability);
                cmd.Parameters.AddWithValue("@daily_ratee", vehicle.DailyRate);
                cmd.Parameters.AddWithValue("@idd", vehicle.VehicleID);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int updateVehicleStatus;
                try
                {
                    updateVehicleStatus = cmd.ExecuteNonQuery();
                    Console.WriteLine(updateVehicleStatus);
                    Console.WriteLine("Vehicle Updated Successfully");
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("duplicate key value"))
                    {
                        Console.WriteLine($"Vehicle already exists");
                    }
                }
                return false;
            }
        }
        public List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select * from Vehicle";
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Vehicle vehicle = new Vehicle();
                        vehicle.VehicleID = (int)reader["VehicleID"];
                        vehicle.Model = (string)reader["Model"];
                        vehicle.Make = (string)reader["Make"];
                        vehicle.Year = (int)reader["Year"];
                        vehicle.Color = (string)reader["Color"];
                        vehicle.RegistrationNumber = (string)reader["RegistrationNumber"];
                        vehicle.Availability = (bool)reader["Availability"];
                        vehicle.DailyRate = (decimal)reader["DailyRate"];
                        vehicles.Add(vehicle);
                    }
                }
            }
            catch (VehicleNotFoundException)
            {
                Console.WriteLine("Vehicle Table is Empty");
            }

            return vehicles;
        }
    }
}
