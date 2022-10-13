using Microsoft.EntityFrameworkCore;
using MTechTest.Modelos;

namespace MTechTest.Data
{
    public class EmployeesAPIdbContext: DbContext
    {
        public EmployeesAPIdbContext(DbContextOptions options): base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
