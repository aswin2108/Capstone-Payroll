namespace PayRollProject.Models
{
    public class Audit
    {
        public string UserName { get; set; }
        public string Operation {  get; set; }
        public string Result { get; set; }
        public string ExcecutedAt { get; set; }
        public string OperatedOn { get; set; }
    }
}
