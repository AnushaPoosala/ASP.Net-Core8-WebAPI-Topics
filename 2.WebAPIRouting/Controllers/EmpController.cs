using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPISampleProjectUsingVS2022.Models;

namespace WebAPISampleProjectUsingVS2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {

        private static List<Emp> _empList = new List<Emp>
        {
        new Emp{EmpNo=1,EmpName="Shiva",job="IT", Salary=3000 },
        new Emp{ EmpNo=2,EmpName="Rama",job="Clerk",Salary=800 },
        new Emp{EmpNo=3,EmpName="Krishna",job="Manager",Salary=8008 }
        };

        //GET: api/Emp
        [HttpGet]
        public ActionResult<IEnumerable<Emp>> GetEmpList()
        {
            if(_empList==null)
            {
                return BadRequest(_empList);
            }
            return Ok(_empList);
        }

        //Get: api/Emp/5
        [HttpGet("{id}")]
        public ActionResult<Emp> GetEmp(int id) 
        {
            var emp = _empList.FirstOrDefault(e => e.EmpNo == id);
            if (emp == null)
            {
                return NotFound(new {Message=$"Emp with Id{id} is not found"});
            }
            return Ok(emp);
        }

        //Post:api/Emp
        [HttpPost]
        public ActionResult<Emp> PostEmp([FromBody]Emp emp)
        {
            if (emp == null) { return BadRequest(new {Message="Employee object should not be empty or null"}); }
            emp.EmpNo = _empList.Max(e => e.EmpNo) + 1;
            _empList.Add(emp);
            return CreatedAtAction("GetEmp", new { id = emp.EmpNo }, emp);
        }

        //Put: api/Emp/5
        [HttpPut("{id}")]
        public ActionResult<Emp> PutEmp(int id, [FromBody] Emp emp)
        {
            if(emp==null || emp.EmpNo!=id) 
            {
                return BadRequest(new {Message="Id mismatch between route and body" });
            }
            var existingEmp= _empList.FirstOrDefault (e=>e.EmpNo==id);
            if (existingEmp==null)
            {
                return NotFound(new { Message=$"Emp with Id{id} not found."});
            }
            _empList.Remove(existingEmp);

            existingEmp.EmpName = emp.EmpName;
            existingEmp.job = emp.job;
            existingEmp.Salary= emp.Salary;

            _empList.Add(existingEmp);

            return Ok(new { Message=$"Emp with Id{id} data is updated."});
        }

        //Delete :api/Emp/5
        [HttpDelete("{id}")]
        public ActionResult<Emp> DeleteEmp(int id) 
        {
            var empToBeDeleted= _empList.FirstOrDefault(e=>e.EmpNo==id);

            if (empToBeDeleted==null)
            {
                return NotFound(new { Message = $"Emp with Id{id} is not found." });
            }
            _empList.Remove(empToBeDeleted);
            return Ok(new { Message = $"Emp with Id{id} data is Deleted." });
        }


        
    }
}
