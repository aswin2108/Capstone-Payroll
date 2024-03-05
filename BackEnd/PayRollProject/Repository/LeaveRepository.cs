using Microsoft.AspNetCore.Mvc;
using PayRollProject.Models;
using PayRollProject.Repository.Interfaces;
using System.Data.SqlClient;

namespace PayRollProject.Repository
{
    public class LeaveRepository:ILeaveDetailsRepository
    {
        public string cstr = @"Data Source=APINP-ELPTHQFI5\SQLEXPRESS;Initial Catalog=capstone;User ID=tap2023;Password=tap2023;Encrypt=False";

        public LeaveDetails GetLeaveDetails(string userName)
        {
            LeaveDetails leaveDetails = new LeaveDetails();

            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"SELECT * FROM EmployeeLeaves WHERE UserName = @UserName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", userName);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            leaveDetails.UserName = reader["UserName"].ToString();
                            leaveDetails.SickLeave = Convert.ToInt32(reader["SickLeave"]);
                            leaveDetails.CasualLeave = Convert.ToInt32(reader["CasualLeave"]);
                            leaveDetails.EarnedLeave = Convert.ToInt32(reader["EarnedLeave"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return leaveDetails;
        }

        public void CreateLeaveDetails(LeaveDetails leaveDetails)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"INSERT INTO EmployeeLeaves (UserName, SickLeave, CasualLeave, EarnedLeave)
                             VALUES (@UserName, @SickLeave, @CasualLeave, @EarnedLeave)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", leaveDetails.UserName);
                        command.Parameters.AddWithValue("@SickLeave", leaveDetails.SickLeave);
                        command.Parameters.AddWithValue("@CasualLeave", leaveDetails.CasualLeave);
                        command.Parameters.AddWithValue("@EarnedLeave", leaveDetails.EarnedLeave);

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

        public void UpdateLeaveDetails(LeaveDetails leaveDetails)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"UPDATE EmployeeLeaves
                             SET SickLeave = @SickLeave,
                                 CasualLeave = @CasualLeave,
                                 EarnedLeave = @EarnedLeave
                             WHERE UserName = @UserName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", leaveDetails.UserName);
                        command.Parameters.AddWithValue("@SickLeave", leaveDetails.SickLeave);
                        command.Parameters.AddWithValue("@CasualLeave", leaveDetails.CasualLeave);
                        command.Parameters.AddWithValue("@EarnedLeave", leaveDetails.EarnedLeave);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // Handle any exceptions that occur during the database operation
            }
        }
    }
}
