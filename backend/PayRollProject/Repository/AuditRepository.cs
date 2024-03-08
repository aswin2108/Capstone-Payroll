using PayRollProject.Models;
using PayRollProject.Repository.Interfaces;
using System.Data.SqlClient;

namespace PayRollProject.Repository
{
    public class AuditRepository :IAuditRepository
    {
        public string cstr = @"Data Source=APINP-ELPTHQFI5\SQLEXPRESS;Initial Catalog=capstone;User ID=tap2023;Password=tap2023;Encrypt=False";

        public List<Audit> GetAllAuditDetails()
        {
            List<Audit> auditDetails = new List<Audit>();
            try
            {
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string query = "SELECT * FROM AuditDetails";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Audit auditDetail = new Audit();

                            auditDetail.UserName = reader["UserName"].ToString();
                            auditDetail.Operation = reader["Operation"].ToString();
                            auditDetail.Result = reader["Result"].ToString();
                            auditDetail.ExcecutedAt = reader["ExcecutedAt"].ToString();
                            auditDetail.OperatedOn = reader["OperatedOn"].ToString();

                            auditDetails.Add(auditDetail);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return auditDetails;
        }
    public void CreateAuditRecord(Audit auditDetail)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string query = "INSERT INTO AuditDetails (UserName, Operation, Result, ExcecutedAt, OperatedOn) VALUES (@UserName, @Operation, @Result, @ExcecutedAt, @OperatedOn)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", auditDetail.UserName);
                    command.Parameters.AddWithValue("@Operation", auditDetail.Operation);
                    command.Parameters.AddWithValue("@Result", auditDetail.Result);
                    command.Parameters.AddWithValue("@ExcecutedAt", auditDetail.ExcecutedAt);
                    command.Parameters.AddWithValue("@OperatedOn", auditDetail.OperatedOn);

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
