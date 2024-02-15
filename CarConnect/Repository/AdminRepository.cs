using CarConnect.Exceptions;
using CarConnect.Model;
using CarConnect.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Repository
{
    public class AdminRepository : IAdminRepository
    {
        public string connectionString;
        SqlCommand cmd = null;

        public AdminRepository()
        {
            connectionString = DBConnectionUtility.GetConnectedString();
            cmd = new SqlCommand();
        }
        public Admin Authenticate(string username, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select * from admin where username=@username";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Connection = conn;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        return new Admin
                        {
                            AdminID = (int)reader["AdminID"],
                            UserName = (string)reader["UserName"],
                            Password = (string)reader["Password"]
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public bool DeleteAdminByID(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "delete from admin where AdminID=@id";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = conn;
                    conn.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    return rowsEffected > 0;
                }
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public Admin GetAdminById(int AdminID)
        {
            try
            {
                Admin admin = null;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    cmd.CommandText = "select * from Admin where AdminID=@adminID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@adminID", AdminID);
                    cmd.Connection = conn;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            admin = new Admin
                            {
                                AdminID = (int)reader["AdminID"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                Email = (string)reader["Email"],
                                PhoneNumber = (string)reader["PhoneNumber"],
                                UserName = (string)reader["Username"],
                                Password = (string)reader["Password"],
                                Role = (string)reader["Role"],
                                JoinDate = (DateTime)reader["JoinDate"]
                            };
                        }
                    }
                }
                return admin;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public Admin GetAdminByUsername(string userName)
        {
            try
            {
                Admin admin = null;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    cmd.CommandText = "select * from Admin where UserName=@userName";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@userName", userName);
                    cmd.Connection = conn;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            admin = new Admin
                            {
                                AdminID = (int)reader["AdminID"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                Email = (string)reader["Email"],
                                PhoneNumber = (string)reader["PhoneNumber"],
                                UserName = (string)reader["Username"],
                                Password = (string)reader["Password"],
                                Role = (string)reader["Role"],
                                JoinDate = (DateTime)reader["JoinDate"]
                            };
                        }
                    }
                }
                return admin;
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public bool RegisterAdmin(Admin admin)
        {

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "insert into Admin( AdminID,FirstName,LastName,Email,PhoneNumber,Username,Password,Role,JoinDate) values(@id,@fName,@lName,@email,@phone,@user,@pw,@role,@join_date)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", admin.AdminID);
                    cmd.Parameters.AddWithValue("@fName", admin.FirstName);
                    cmd.Parameters.AddWithValue("@lName", admin.LastName);
                    cmd.Parameters.AddWithValue("@email", admin.Email);
                    cmd.Parameters.AddWithValue("@phone", admin.PhoneNumber);
                    cmd.Parameters.AddWithValue("@user", admin.UserName);
                    cmd.Parameters.AddWithValue("@pw", admin.Password);
                    cmd.Parameters.AddWithValue("@role", admin.Role);
                    cmd.Parameters.AddWithValue("@join_date", admin.JoinDate);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    return rowsEffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool UpdateAdmin(Admin adminData, string username)
        {
            try
            {
                using(SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "UPDATE admin SET PhoneNumber=@phone, Role=@r, FirstName=@fname, LastName=@lname, Email=@eml WHERE Username=@uname";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@fname", adminData.FirstName);
                    cmd.Parameters.AddWithValue("@lname", adminData.LastName);
                    cmd.Parameters.AddWithValue("@phone", adminData.PhoneNumber);
                    cmd.Parameters.AddWithValue("@eml", adminData.Email);
                    cmd.Parameters.AddWithValue("@uname", username);
                    cmd.Parameters.AddWithValue("@r", adminData.Role);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    int updatedRows = cmd.ExecuteNonQuery();
                    Console.WriteLine(updatedRows);
                    return updatedRows > 0;
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           
        return false;
        }
        
    }
}

