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
                        command.Parameters.AddWithValue("@FromDate", leaveRecord.LeaveFrom);
                        command.Parameters.AddWithValue("@ToDate", leaveRecord.LeaveTo);
                        command.Parameters.AddWithValue("@Reason", leaveRecord.Reason);
                        command.Parameters.AddWithValue("@Flag", 0);
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

        public List<LeaveRecord> GetUnapprovedLeaveRequests(string empType)
        {
            List<LeaveRecord> unapprovedLeaves = new List<LeaveRecord>();

            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"SELECT * FROM LeaveRecords WHERE Flag = 0 AND UserName LIKE @EmpTypePattern";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmpTypePattern", empType + "%");
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            LeaveRecord leaveRecord = new LeaveRecord();
                            leaveRecord.UserName = reader["UserName"].ToString();
                            leaveRecord.LeaveFrom = reader["LeaveFrom"].ToString();
                            leaveRecord.LeaveTo = reader["LeaveTo"].ToString();
                            leaveRecord.Reason = reader["Reason"].ToString();
                            leaveRecord.Flag = Convert.ToInt32(reader["Flag"]);
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

        public void ApproveLeaveRequest(string UserName, string FromDate, int Flag)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"UPDATE LeaveRecords SET Flag = @Flag WHERE UserName = @UserName AND LeaveFrom=@FromDate";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@FromDate", FromDate);
                        command.Parameters.AddWithValue("@Flag", Flag);
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
                    string query = @"SELECT * FROM LeaveRecords WHERE UserName = @UserName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            LeaveRecord leaveRecord = new LeaveRecord();
                            leaveRecord.UserName = reader["UserName"].ToString();
                            leaveRecord.LeaveFrom = reader["LeaveFrom"].ToString();
                            leaveRecord.LeaveTo = reader["LeaveTo"].ToString();
                            leaveRecord.Reason = reader["Reason"].ToString();
                            leaveRecord.Flag = Convert.ToInt32(reader["Flag"]);
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
