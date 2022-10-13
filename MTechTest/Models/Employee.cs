namespace MTechTest.Modelos
{
    public class Employee
    {
        public string RFC { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public EmployeeStatus Status { get; set; }
    }

    public enum EmployeeStatus
    {
        NotSet, Active, Inactive
    }
}
