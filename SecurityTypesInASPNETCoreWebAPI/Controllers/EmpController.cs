using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityTypesInASPNETCoreWebAPI.DataBaseContext;
using SecurityTypesInASPNETCoreWebAPI.Models;
using SecurityTypesInASPNETCoreWebAPI.Models.DTO;

namespace SecurityTypesInASPNETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        private readonly EmpDbContext _context;

        public EmpController(EmpDbContext context) 
        {
            _context = context;
        }

        [HttpPost("EmpRegister")]
        public async Task<IActionResult> EmpRegistration([FromBody] EmpRegistrationDTO empRegistrationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Check if the email is already registered
            var emp = await _context.Employees.AnyAsync(e => e.Email == empRegistrationDTO.Email);
            if (emp == true)
            {
                return BadRequest("Email is already registered");
            }

            // Create password hash and salt
            PasswordHasher.CreatePasswordHash(empRegistrationDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // Create Emp object
            var newEmp = new Employee
            {
                FirstName = empRegistrationDTO.FirstName,
                LastName = empRegistrationDTO.LastName,
                Department = empRegistrationDTO.Department,
                Description = empRegistrationDTO.Description,
                Email = empRegistrationDTO.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            // Add Emp object to the database
            _context.Employees.Add(newEmp);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Employee registered sucessfully!" });

        }

        [HttpPost("EmpLogin")]
        public async Task<IActionResult> EmpLogin([FromBody] EmpLoginDTO empLoginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the email is registered
            var emp = await _context.Employees.FirstOrDefaultAsync(e => e.Email == empLoginDTO.Email);
            if (emp == null)
            {
                return Unauthorized("Email is not registered, please register now");
            }

            // Verify password
            if (!PasswordHasher.VerifyPasswordHash(empLoginDTO.Password, emp.PasswordHash, emp.PasswordSalt))
            {
                return Unauthorized("Invalid password");
            }

            return Ok(new { Message = "Login successful!" });
        }
    }
}
