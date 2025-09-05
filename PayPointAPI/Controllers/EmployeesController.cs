using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPointAPi.DataAccess.Data;
using PayPointAPI.Models.Models;

namespace PayPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/employees
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _context.Employees.Include(e => e.EmployeeStore).ToList();

            if (!employees.Any())
                return NotFound("No employees found.");

            return Ok(employees);
        }

        // GET: api/employees/5
        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _context.Employees
                .Include(e => e.EmployeeStore)
                .FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound($"Employee with ID {id} not found.");

            return Ok(employee);
        }

        // POST: api/employees
        [HttpPost]
        public IActionResult CreateEmployee(Employee employee)
        {
            if (employee == null)
                return BadRequest("Employee data is null.");

            var store = _context.Stores.FirstOrDefault(s => s.StoreId == employee.EmployeeStore);
            if (store == null)
                return BadRequest("Invalid store ID. Store not found.");

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
        }

        // PUT: api/employees/5
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, Employee updatedEmployee)
        {
            if (updatedEmployee == null || updatedEmployee.EmployeeId != id)
                return BadRequest("Employee data is null or ID mismatch.");

            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
                return NotFound($"Employee with ID {id} not found.");

            // Update properties
            employee.EmployeeName = updatedEmployee.EmployeeName;
            employee.EmployeeEmail = updatedEmployee.EmployeeEmail;
            employee.EmployeePhone = updatedEmployee.EmployeePhone;
            employee.EmployeeStore = updatedEmployee.EmployeeStore;

            _context.SaveChanges();

            return Ok(employee);
        }

        // DELETE: api/employees/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
                return NotFound($"Employee with ID {id} not found.");

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return Ok($"Employee with ID {id} deleted successfully.");
        }

        // GET: api/employees/store/1
        [HttpGet("store/{storeId}")]
        public IActionResult GetEmployeesByStore(int storeId)
        {
            var employees = _context.Employees
                .Where(e => e.EmployeeStore == storeId)
                .ToList();

            if (!employees.Any())
                return NotFound($"No employees found for store with ID {storeId}.");

            return Ok(employees);
        }
    }
}
