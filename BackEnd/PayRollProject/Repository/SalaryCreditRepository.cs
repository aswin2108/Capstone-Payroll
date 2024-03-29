﻿using PayRollProject.Models;
using PayRollProject.Repository.Interfaces;
using System.Data.SqlClient;

namespace PayRollProject.Repository
{
    public class SalaryCreditRepository : ISalaryCreditRepository
    {
        public string cstr = @"Data Source=APINP-ELPTHQFI5\SQLEXPRESS;Initial Catalog=capstone;User ID=tap2023;Password=tap2023;Encrypt=False";
        public void InsertCreditTaxDetails(string userName, DateTime creditDate, decimal creditAmount, decimal taxCut, string transactionId, int excemptionAmt, int bonus, int overTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"INSERT INTO CreditTaxDetails (UserName, CreditDate, CreditAmount, TaxCut, TransactionId, ExcemptionAmt, Bonus, OverTime)
                             VALUES (@UserName, @CreditDate, @CreditAmount, @TaxCut, @TransactionId, @ExcemptionAmt, @Bonus, @OverTime)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", userName);
                        command.Parameters.AddWithValue("@CreditDate", creditDate);
                        command.Parameters.AddWithValue("@CreditAmount", creditAmount);
                        command.Parameters.AddWithValue("@TaxCut", taxCut);
                        command.Parameters.AddWithValue("@TransactionId", transactionId);
                        command.Parameters.AddWithValue("@ExcemptionAmt", excemptionAmt);
                        command.Parameters.AddWithValue("@Bonus", bonus);
                        command.Parameters.AddWithValue("@OverTime", overTime);
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

        public List<SalaryCredit> GetCreditTaxDetailsByUserName(string userName)
        {
            List<SalaryCredit> creditUsers = new List<SalaryCredit>();

            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = @"SELECT * FROM CreditTaxDetails WHERE UserName = @UserName ORDER BY CreditDate DESC";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", userName);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            SalaryCredit user = new SalaryCredit();
                            user.UserName = reader["UserName"].ToString();
                            user.CreditDate = (DateTime)reader["CreditDate"];
                            user.CreditAmount = Convert.ToDecimal(reader["CreditAmount"]);
                            user.TaxCut = Convert.ToDecimal(reader["TaxCut"]);
                            user.TransactionId = reader["TransactionId"].ToString();
                            user.ExcemptionAmt = Convert.ToInt32(reader["ExcemptionAmt"]); ;
                            user.Bonus = Convert.ToInt32(reader["Bonus"]); ;
                            user.OverTime = Convert.ToInt32(reader["OverTime"]); ;
                            creditUsers.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return creditUsers;
        }

    }
}
