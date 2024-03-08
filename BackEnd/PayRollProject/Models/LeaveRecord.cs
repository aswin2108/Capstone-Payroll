namespace PayRollProject.Models
{
    public class LeaveRecord
    {
        public string UserName { get; set; }
        public string LeaveFrom { get; set; }
        public string LeaveTo { get; set; }
        public string Reason { get; set; }  
        public int Flag { get; set; }  
        public int LeaveType { get; set; }
    }
}
