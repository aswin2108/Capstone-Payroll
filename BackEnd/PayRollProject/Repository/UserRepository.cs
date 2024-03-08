//using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using PayRollProject.Models;
using PayRollProject.Repository.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PayRollProject.Repository
{
    public class UserRepository : IUserDetailsRepository
    {
        public string cstr = @"Data Source=APINP-ELPTHQFI5\SQLEXPRESS;Initial Catalog=capstone;User ID=tap2023;Password=tap2023;Encrypt=False";


        public List<UserDetails> GetAllUsers()
        {
            List<UserDetails> users = new List<UserDetails>();
            try {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = "Select * from Users";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            UserDetails user = new UserDetails();

                            user.UserName = reader["UserName"].ToString();
                            user.Age = Convert.ToInt32(reader["Age"]);
                            user.Email = reader["Email"].ToString();
                            user.AccNo = reader["AccNo"].ToString();
                            user.FirstName = reader["FirstName"].ToString();
                            user.LastName = reader["LastName"].ToString();
                            user.IFSC = reader["IFSC"].ToString();
                            user.NextPayDate = DateOnly.FromDateTime((DateTime)reader["NextPayDate"]);
                            user.DOB = DateOnly.FromDateTime((DateTime)reader["DOB"]);
                            user.Phone = reader["Phone"].ToString();
                            user.Salary = Convert.ToInt32(reader["Salary"]);
                            user.TaxPercent = Convert.ToInt32(reader["TaxPercent"]);
                            user.Bonus = Convert.ToInt32(reader["Bonus"]);
                            user.ExcemptionAmt = Convert.ToInt32(reader["ExcemptionAmt"]);
                            user.PayFreq = Convert.ToInt32(reader["PayFreq"]);
                            user.OverTime = Convert.ToInt32(reader["OverTime"]);
                            user.Role = reader["Role"].ToString();

                            users.Add(user);
                        }
                    }
                }

            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
            return users;
        }

        public UserDetails GetUserByUserName(string UserName)
        {
            UserDetails user = new UserDetails();

            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"Select * from Users WHERE UserName=@UserName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            user.UserName = reader["UserName"].ToString();
                            user.Age = Convert.ToInt32(reader["Age"]);
                            user.Email = reader["Email"].ToString();
                            user.AccNo = reader["AccNo"].ToString();
                            user.FirstName = reader["FirstName"].ToString();
                            user.LastName = reader["LastName"].ToString();
                            user.IFSC = reader["IFSC"].ToString();
                            user.NextPayDate = DateOnly.FromDateTime((DateTime)reader["NextPayDate"]);
                            user.DOB = DateOnly.FromDateTime((DateTime)reader["DOB"]);
                            user.Phone = reader["Phone"].ToString();
                            user.Salary = Convert.ToInt32(reader["Salary"]);
                            user.TaxPercent = Convert.ToInt32(reader["TaxPercent"]);
                            user.Bonus = Convert.ToInt32(reader["Bonus"]);
                            user.ExcemptionAmt = Convert.ToInt32(reader["ExcemptionAmt"]);
                            user.PayFreq = Convert.ToInt32(reader["PayFreq"]);
                            user.OverTime = Convert.ToInt32(reader["OverTime"]);
                            user.Role = reader["Role"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            return user;
        }

        public void UpdateUserBonus(string UserName, int newBonus)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string updateQuery = @"UPDATE Users SET Bonus = @NewBonus WHERE UserName = @UserName";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@NewBonus", newBonus);
                        command.Parameters.AddWithValue("@UserName", UserName);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Console.WriteLine("No user found with the given UserName");
                        }
                        else
                        {
                            Console.WriteLine("Bonus updated successfully");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void UpdateUserExcemptionAmtAndTaxPercent(string UserName, int newExcemptionAmt, int newTaxPercent, int overTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string updateQuery = @"UPDATE Users SET ExcemptionAmt = @NewExcemptionAmt, OverTime=@OverTime, TaxPercent = @NewTaxPercent WHERE UserName = @UserName";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@NewExcemptionAmt", newExcemptionAmt);
                        command.Parameters.AddWithValue("@NewTaxPercent", newTaxPercent);
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@OverTime", overTime);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Console.WriteLine("No user found with the given UserName");
                        }
                        else
                        {
                            Console.WriteLine("ExcemptionAmt and TaxPercent updated successfully");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void UpdateUserNextPayDate(string UserName, DateTime newNextPayDate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string updateQuery = @"UPDATE Users SET NextPayDate =  @DateParameter WHERE UserName = @UserName";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        SqlParameter parameter = new SqlParameter("@DateParameter", SqlDbType.Date);
                        parameter.ParameterName = "@DateParameter";
                        parameter.Value = newNextPayDate.Date;
                        command.Parameters.Add(parameter);
                        command.Parameters.AddWithValue("@UserName", UserName);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Console.WriteLine("No user found with the given UserName");
                        }
                        else
                        {
                            Console.WriteLine("NextPayDate updated successfully");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void InsertUserDetails(UserDetails user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    Console.WriteLine(user.UserName);
                    Console.WriteLine(user.DOB);
                    string insertQuery = @"INSERT INTO Users (UserName, Age, Email, AccNo, FirstName, LastName, IFSC, NextPayDate, DOB, Phone, Salary, TaxPercent, Bonus, ExcemptionAmt, PayFreq,OverTime, Role)
                                  VALUES (@UserName, @Age, @Email, @AccNo, @FirstName, @LastName, @IFSC, @NextPayDate, @DOB, @Phone, @Salary, @TaxPercent, @Bonus, @ExcemptionAmt, @PayFreq,@OverTime, @Role)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", user.UserName);
                        command.Parameters.AddWithValue("@Age", user.Age);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@AccNo", user.AccNo);
                        command.Parameters.AddWithValue("@FirstName", user.FirstName);
                        command.Parameters.AddWithValue("@LastName", user.LastName);
                        command.Parameters.AddWithValue("@IFSC", user.IFSC);
                        command.Parameters.AddWithValue("@NextPayDate", user.NextPayDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@DOB", user.DOB.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Phone", user.Phone);
                        command.Parameters.AddWithValue("@Salary", user.Salary);
                        command.Parameters.AddWithValue("@TaxPercent", user.TaxPercent);
                        command.Parameters.AddWithValue("@Bonus", user.Bonus);
                        command.Parameters.AddWithValue("@ExcemptionAmt", user.ExcemptionAmt);
                        command.Parameters.AddWithValue("@PayFreq", user.PayFreq);
                        command.Parameters.AddWithValue("@OverTime", user.OverTime);
                        command.Parameters.AddWithValue("@Role", user.Role);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine("New user inserted successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void UpdateUserDetails(UserDetails user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    Console.WriteLine("-------------------------------"+user.UserName);
                    Console.WriteLine(user.DOB);
                    string updateQuery = @"UPDATE Users 
                                 SET Age = @Age, 
                                      Email = @Email, 
                                      AccNo = @AccNo, 
                                      FirstName = @FirstName, 
                                      LastName = @LastName, 
                                      IFSC = @IFSC, 
                                      NextPayDate = @NextPayDate, 
                                      DOB = @DOB, 
                                      Phone = @Phone, 
                                      Salary = @Salary, 
                                      TaxPercent = @TaxPercent,  
                                      PayFreq = @PayFreq,
                                      Bonus = @Bonus
                                 WHERE UserName = @UserName";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", user.UserName);
                        command.Parameters.AddWithValue("@Age", user.Age);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@AccNo", user.AccNo);
                        command.Parameters.AddWithValue("@FirstName", user.FirstName);
                        command.Parameters.AddWithValue("@LastName", user.LastName);
                        command.Parameters.AddWithValue("@IFSC", user.IFSC);
                        command.Parameters.AddWithValue("@NextPayDate", user.NextPayDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@DOB", user.DOB.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Phone", user.Phone);
                        command.Parameters.AddWithValue("@Salary", user.Salary);
                        command.Parameters.AddWithValue("@TaxPercent", user.TaxPercent);
                        command.Parameters.AddWithValue("@Bonus", user.Bonus);
                        command.Parameters.AddWithValue("@ExcemptionAmt", user.ExcemptionAmt);
                        command.Parameters.AddWithValue("@PayFreq", user.PayFreq);
                        command.Parameters.AddWithValue("@OverTime", user.OverTime);
                        command.Parameters.AddWithValue("@Role", user.Role);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine("User details updated successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void DeleteUserByUserName(string UserName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = "DELETE FROM Users WHERE UserName = @UserName";

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

        public List<UserDetails> GetUsersByDate(DateTime date)
        {
            List<UserDetails> users = new List<UserDetails>();

            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"Select * from Users WHERE NextPayDate=@NextPayDate";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlParameter parameter = new SqlParameter("@NextPayDate", SqlDbType.Date);
                        parameter.ParameterName = "@NextPayDate";
                        parameter.Value = date.Date;
                        command.Parameters.Add(parameter);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            UserDetails user = new UserDetails();
                            user.UserName = reader["UserName"].ToString();
                            user.Age = Convert.ToInt32(reader["Age"]);
                            user.Email = reader["Email"].ToString();
                            user.AccNo = reader["AccNo"].ToString();
                            user.FirstName = reader["FirstName"].ToString();
                            user.LastName = reader["LastName"].ToString();
                            user.IFSC = reader["IFSC"].ToString();
                            user.NextPayDate = DateOnly.FromDateTime((DateTime)reader["NextPayDate"]);
                            user.DOB = DateOnly.FromDateTime((DateTime)reader["DOB"]);
                            user.Phone = reader["Phone"].ToString();
                            user.Salary = Convert.ToInt32(reader["Salary"]);
                            user.TaxPercent = Convert.ToInt32(reader["TaxPercent"]);
                            user.Bonus = Convert.ToInt32(reader["Bonus"]);
                            user.ExcemptionAmt = Convert.ToInt32(reader["ExcemptionAmt"]);
                            user.PayFreq = Convert.ToInt32(reader["PayFreq"]);
                            user.OverTime = Convert.ToInt32(reader["OverTime"]);
                            user.Role = reader["Role"].ToString();
                            Console.WriteLine(user.NextPayDate);
                            users.Add(user);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return users;
        }

        public void UpdateOvertimeToZero(string UserName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"UPDATE Users SET OverTime = 0, Bonus = 0 WHERE UserName = @UserName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Console.WriteLine("No user with the given username was found.");
                        }
                        else
                        {
                            Console.WriteLine("Overtime updated successfully for user: " + UserName);
                        }
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












   


/*
update the pay date
public void UpdateUserNextPayDate(string UserName, DateOnly newNextPayDate)
{
    try
    {
        using (SqlConnection connection = new SqlConnection(cstr))
        {
            string updateQuery = @"UPDATE Users SET NextPayDate = @NewNextPayDate WHERE UserName = @UserName";
            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@NewNextPayDate", newNextPayDate.DateTime);
                command.Parameters.AddWithValue("@UserName", UserName);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    Console.WriteLine("No user found with the given UserName");
                }
                else
                {
                    Console.WriteLine("NextPayDate updated successfully");
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}



Insert Data
public void InsertUserDetails(UserDetails user)
{
    try
    {
        using (SqlConnection connection = new SqlConnection(cstr))
        {
            string insertQuery = @"INSERT INTO Users (UserName, Age, Email, AccNo, FirstName, LastName, IFSC, NextPayDate, DOB, Phone, Salary, TaxPercent, Bonus, ExcemptionAmt)
                                  VALUES (@UserName, @Age, @Email, @AccNo, @FirstName, @LastName, @IFSC, @NextPayDate, @DOB, @Phone, @Salary, @TaxPercent, @Bonus, @ExcemptionAmt)";
            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Age", user.Age);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@AccNo", user.AccNo);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@IFSC", user.IFSC);
                command.Parameters.AddWithValue("@NextPayDate", user.NextPayDate.DateTime);
                command.Parameters.AddWithValue("@DOB", user.DOB.DateTime);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@Salary", user.Salary);
                command.Parameters.AddWithValue("@TaxPercent", user.TaxPercent);
                command.Parameters.AddWithValue("@Bonus", user.Bonus);
                command.Parameters.AddWithValue("@ExcemptionAmt", user.ExcemptionAmt);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine("New user inserted successfully");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}


update the pay date
public void UpdateUserNextPayDate(string UserName)
{
    // Fetch the current user details from the database
    UserDetails user = GetUserByUserName(UserName);
    
    // Calculate the new NextPayDate using the CalculateNextPayDate() function
    DateOnly newNextPayDate = CalculateNextPayDate(user.NextPayDate);

    try
    {
        using (SqlConnection connection = new SqlConnection(cstr))
        {
            string updateQuery = @"UPDATE Users SET NextPayDate = @NewNextPayDate WHERE UserName = @UserName";
            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@NewNextPayDate", newNextPayDate.DateTime);
                command.Parameters.AddWithValue("@UserName", UserName);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    Console.WriteLine("No user found with the given UserName");
                }
                else
                {
                    Console.WriteLine("NextPayDate updated successfully");
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}

public DateOnly CalculateNextPayDate(DateOnly currentNextPayDate)
{
    int currentDay = currentNextPayDate.Day;
    int currentMonth = currentNextPayDate.Month;
    int currentYear = currentNextPayDate.Year;

    int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);

    if (currentDay == 1)
    {
        // Add 7 days to the current NextPayDate
        return currentNextPayDate.AddDays(7);
    }
    else if (currentDay == 21)
    {
        // Set the NextPayDate to the last day of the month
        return new DateOnly(currentYear, currentMonth, daysInMonth);
    }
    else if (currentDay == daysInMonth)
    {
        // If the current NextPayDate is the last day of the month
        // and currentDay is 2, set the NextPayDate to 14
        // otherwise, set the NextPayDate to the last day of the month
        if (currentDay == 2)
        {
            return new DateOnly(currentYear, currentMonth, 14);
        }
        
        // Set the NextPayDate to the 14th of the next month
        int nextMonth = currentMonth + 1;
        int nextYear = currentYear;
        if (nextMonth > 12)
        {
            nextMonth = 1;
            nextYear++;
        }
        return new DateOnly(nextYear, nextMonth, 14);
    }
    else if (currentDay == 14)
    {
        // If the current NextPayDate is 14
        // and currentDay is 3, set the NextPayDate to the last day of the month
        // otherwise, set the NextPayDate to 14
        if (currentDay == 3)
        {
            return new DateOnly(currentYear, currentMonth, daysInMonth);
        }
        
        // Set the NextPayDate to 14
        return currentNextPayDate;
    }
    else if (currentDay == 3)
    {
        // Set the NextPayDate to the last day of the next month
        int nextMonth = currentMonth + 1;
        int nextYear = currentYear;
        if (nextMonth > 12)
        {
            nextMonth = 1;
            nextYear++;
        }
        return new DateOnly(nextYear, nextMonth, daysInMonth);
    }

    // Default case: return the current NextPayDate
    return currentNextPayDate;
}


*/