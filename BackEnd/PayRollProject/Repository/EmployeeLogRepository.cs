using PayRollProject.Models;
using PayRollProject.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace PayRollProject.Repository
{
    public class EmployeeLogRepository: IEmployeeLogRepository
    {
        public string cstr = @"Data Source=APINP-ELPTHQFI5\SQLEXPRESS;Initial Catalog=capstone;User ID=tap2023;Password=tap2023;Encrypt=False";

        public EmployeeLog GetAttendanceByDateAndUserName(string date, string userName)
        {
            EmployeeLog attendance = new EmployeeLog();

            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"SELECT * FROM Attendance WHERE UserName = @UserName AND LogDate=@Date" ;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Date", date);
                        command.Parameters.AddWithValue("@UserName", userName);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            attendance.UserName = reader["UserName"].ToString();
                            attendance.Date = reader["LogDate"].ToString();
                            attendance.CheckInTime = reader["CheckInTime"].ToString();
                            attendance.CheckOutTime = reader["CheckOutTime"].ToString();
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return attendance;
        }

        public void UpdateAttendance(string date, string userName, string checkInTime, string checkOutTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"UPDATE Attendance SET CheckInTime = @CheckInTime, CheckOutTime = @CheckOutTime WHERE UserName = @UserName AND LogDate = @Date";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CheckInTime", checkInTime);
                        command.Parameters.AddWithValue("@CheckOutTime", checkOutTime);
                        command.Parameters.AddWithValue("@UserName", userName);
                        command.Parameters.AddWithValue("@Date", date);
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

        public void InsertAttendance(string userName, string checkInTime, string date)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"INSERT INTO Attendance (UserName, CheckInTime, LogDate, CheckOutTime) VALUES (@UserName, @CheckInTime, @Date, @CheckOutTime)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", userName);
                        command.Parameters.AddWithValue("@CheckInTime", checkInTime);
                        command.Parameters.AddWithValue("@CheckOutTime", "CheckedIn");
                        command.Parameters.AddWithValue("@Date", date);

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

    }
}
