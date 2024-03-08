using PayRollProject.Models;
using PayRollProject.Repository.Interfaces;
using System.Data.SqlClient;

namespace PayRollProject.Repository
{
    public class AuthCredRepository:IAuthCredRepository
    {
        public string cstr = @"Data Source=APINP-ELPTHQFI5\SQLEXPRESS;Initial Catalog=capstone;User ID=tap2023;Password=tap2023;Encrypt=False";

        public AuthCred GetUserDetails(string UserName)
        {
            AuthCred authCred = new AuthCred(); 

            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))  // Replace cstr with your actual connection string
                {
                    string query = @"SELECT * FROM AuthCredentials 
                                WHERE UserName = @UserName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                            authCred.UserName = reader["UserName"].ToString();
                            authCred.HashedPassword = reader["HashedPassword"].ToString();
                            authCred.Salt = reader["Salt"].ToString();
                            authCred.UserRole = Convert.ToInt32(reader["UserRole"]);
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());  // Log the exception for debugging
                                                   // Consider throwing a custom exception for better error handling
            }

            return authCred;
        }

        public void DeleteUserDetails(string UserName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))  // Replace cstr with your actual connection string
                {
                    string query = @"DELETE FROM AuthCredentials 
                             WHERE UserName = @UserName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);
                        connection.Open();

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());  
                                                   
            }
        }

        public void InsertUser(AuthCred authCred)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))  // Replace cstr with your actual connection string
                {
                    string query = @"INSERT INTO AuthCredentials (UserName, HashedPassword, Salt, UserRole) 
                                VALUES (@UserName, @HashedPassword, @Salt, @UserRole)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", authCred.UserName);
                        command.Parameters.AddWithValue("@HashedPassword", authCred.HashedPassword);
                        command.Parameters.AddWithValue("@Salt", authCred.Salt);
                        command.Parameters.AddWithValue("@UserRole", authCred.UserRole);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());  // Log the exception for debugging
                                                   // Consider throwing a custom exception for better error handling
            }

            return;
        }

    }
}
