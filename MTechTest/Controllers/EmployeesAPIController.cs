using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTechTest.Data;
using MTechTest.Modelos;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace MTechTest.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class EmployeesAPIController : Controller
    {
        private readonly EmployeesAPIdbContext dbContext;
        private Regex regRFC = new Regex(@"^[A-Za-zñÑ&]{3,4}\d{6}\w{3}$");


        public EmployeesAPIController(EmployeesAPIdbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> getEmployees()
        {
            return Ok(await dbContext.Employees.OrderBy(d => d.BornDate).ToListAsync());
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> getEmployees([FromRoute] string name)
        {
            return Ok(await dbContext.Employees.Where(d => d.Name.Contains(name)).OrderBy(d => d.BornDate).ToListAsync());
        }
        

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {

            var emp = dbContext.Employees.Where(d => d.RFC == employee.RFC);
            if (emp.Count() >= 0)
            {
                var emp2 = await dbContext.Employees.FindAsync(employee.ID);

                if (emp2 == null)
                {
                    if (employee.RFC.Length == 13)
                    {
                        if (regRFC.IsMatch(employee.RFC))
                        {
                            await dbContext.Employees.AddAsync(employee);
                            await dbContext.SaveChangesAsync();
                            return Ok(employee);
                        }
                        else
                        {
                            return StatusCode(501, "The format doesn't match.");
                        }
                    }
                    else
                    {
                        return StatusCode(501, "The string doesn't meet the lenght.");
                    }
                }
                else
                {
                    return StatusCode(501, "ID Exist.");
                }
            }

            return StatusCode(501,"RFC Exist.");
        }

    }
}
