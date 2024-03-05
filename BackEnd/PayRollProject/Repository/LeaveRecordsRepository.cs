using PayRollProject.Repository.Interfaces;
using System.Data.SqlClient;
using System.Data;
using PayRollProject.Models;

namespace PayRollProject.Repository
{
    public class LeaveRecordsRepository: ILeaveRecordsRepository
    {
        public string cstr = @"Data Source=APINP-ELPTHQFI5\SQLEXPRESS;Initial Catalog=capstone;User ID=tap2023;Password=tap2023;Encrypt=False";

        public void SubmitLeaveRequest(LeaveRecord leaveRecord)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"INSERT INTO LeaveRecords (UserName, LeaveFrom, LeaveTo, Reason, Flag, LeaveType) VALUES (@UserName, @FromDate, @ToDate, @Reason,0,@LeaveType)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", leaveRecord.UserName);
                        command.Parameters.AddWithValue("@FromDate", leaveRecord.LeaveFrom.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@ToDate", leaveRecord.LeaveTo.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Reason", leaveRecord.Reason);
                        //command.Parameters.AddWithValue("@Flag", leaveRecord.Flag);
                        command.Parameters.AddWithValue("@LeaveType", leaveRecord.LeaveType);
                        connection.Open();
                        command.ExecuteNonQuery();

                        Console.WriteLine("Leave request submitted successfully for user: " + leaveRecord.UserName);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error submitting leave request: " + ex.ToString());
            }
        }

        public List<LeaveRecord> GetUnapprovedLeaveRequests()
        {
            List<LeaveRecord> unapprovedLeaves = new List<LeaveRecord>();

            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"SELECT * FROM LeaveRecords WHERE Flag = 0";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            LeaveRecord leaveRecord = new LeaveRecord();
                            leaveRecord.UserName = reader["UserName"].ToString();
                            leaveRecord.LeaveFrom = DateOnly.FromDateTime((DateTime)reader["LeaveFrom"]);
                            leaveRecord.LeaveTo = DateOnly.FromDateTime((DateTime)reader["LeaveTo"]);
                            leaveRecord.Reason = reader["Reason"].ToString();
                            leaveRecord.Flag = Convert.ToBoolean(reader["Flag"]);
                            leaveRecord.LeaveType = Convert.ToInt32(reader["LeaveType"]);
                            unapprovedLeaves.Add(leaveRecord);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return unapprovedLeaves;
        }

        public void ApproveLeaveRequest(string UserName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"UPDATE LeaveRecords SET Flag = 1 WHERE UserName = @UserName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Console.WriteLine("No leave request found for user: " + UserName);
                        }
                        else if (rowsAffected == 1)
                        {
                            Console.WriteLine("Leave request approved successfully for user: " + UserName);
                        }
                        else
                        {
                            // Handle unexpected scenario with multiple rows affected (log or throw exception)
                            Console.WriteLine("Unexpected error: Multiple rows affected. Please investigate.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void DeleteLeaveRequest(string UserName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"DELETE FROM LeaveRecords WHERE UserName = @UserName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Console.WriteLine("No leave request found for user: " + UserName);
                        }
                        else if (rowsAffected == 1)
                        {
                            Console.WriteLine("Leave request deleted successfully for user: " + UserName);
                        }
                        else
                        {
                            // Handle unexpected scenario with multiple rows deleted (log or throw exception)
                            Console.WriteLine("Unexpected error: Multiple rows deleted. Please investigate.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public List<LeaveRecord> GetUserApprovedLeaveRequests(string UserName)
        {
            List<LeaveRecord> approvedLeaves = new List<LeaveRecord>();

            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"SELECT * FROM LeaveRecords WHERE UserName = @UserName AND Flag = 1";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            LeaveRecord leaveRecord = new LeaveRecord();
                            leaveRecord.UserName = reader["UserName"].ToString();
                            leaveRecord.LeaveFrom = DateOnly.FromDateTime((DateTime)reader["LeaveFrom"]);
                            leaveRecord.LeaveTo = DateOnly.FromDateTime((DateTime)reader["LeaveTo"]);
                            leaveRecord.Reason = reader["Reason"].ToString();
                            leaveRecord.Flag = Convert.ToBoolean(reader["Flag"]);
                            leaveRecord.LeaveType = Convert.ToInt32(reader["LeaveType"]);
                            approvedLeaves.Add(leaveRecord);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return approvedLeaves;
        }
    }
}
