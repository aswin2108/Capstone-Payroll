namespace PayRollProject.Models
{
    public class LeaveRecord
    {
        public string UserName { get; set; }
        public DateOnly LeaveFrom { get; set; }
        public DateOnly LeaveTo { get; set; }
        public string Reason { get; set; }  
        public bool Flag { get; set; }  
        public int LeaveType { get; set; }
    }
}
