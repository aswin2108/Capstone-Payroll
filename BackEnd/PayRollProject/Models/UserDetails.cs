using System.ComponentModel.DataAnnotations;

namespace PayRollProject.Models
{
    public class UserDetails
    {
        public string UserName { get; set; } //
        public int Age { get; set; }   //
        public string Email { get; set; } //
        public string AccNo { get; set; } //
        public string FirstName { get; set; } //
        public string LastName { get; set; } //
        public string IFSC { get; set; } //

        public DateOnly NextPayDate { get; set; } //
        public DateOnly DOB { get; set; } //
        public string Phone { get; set; } //
        public int Salary { get; set; } //
        public int TaxPercent { get; set; } //
        public int Bonus { get; set; } //
        public int ExcemptionAmt { get; set; }
        public int PayFreq {  get; set; }//
        public int OverTime {  get; set; }
        public string Role { get; set; }
    }


}