using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using WebAPISampleProjectUsingVS2022.Models;
using WebAPISampleProjectUsingVS2022.Models.Repository_Pattern;

namespace WebAPISampleProjectUsingVS2022.Controllers
{
    [Route("api/[controller]")]     //it will be the base URL/Route for all the methods in the controller
    [ApiController]
    public class EmpRepositoryExampleController : ControllerBase
    {
        private readonly IEmpRepository _empRepository;
        public EmpRepositoryExampleController(IEmpRepository empRepository)
        {
            _empRepository = empRepository;
        }

        //REtrieve all Employees
        [HttpGet]
        [Route("GetAllEmp")]
        public IActionResult GetAllEmp()
        {
            var emps = _empRepository.GetAllEmp();
            return Ok(emps);
        }
        [HttpGet]
        [Route("GetEmpByEmpNo/{EmpNo}")]
        public IActionResult GetEmpByEmpNo(int EmpNo)
        {
            var emp = _empRepository.GetEmpById(EmpNo);
            if (emp == null)
                return NotFound($"Emp with Id {EmpNo} not found");
            return Ok(emp);
        }
        //Add new emp
        [HttpPost]
        [Route("AddEmp")]
        public IActionResult AddEmp([FromBody] Emp emp)
        {
            if (emp == null)
            {
                return BadRequest("Emp object should not be null");
            }
            _empRepository.AddEmp(emp);
            return CreatedAtAction("GetEmpByEmpNo", new { EmpNo = emp.EmpNo }, emp);
        }

        //Update emp
        [HttpPut]
        [Route("UpdateEmp")]
        public IActionResult UpdateEmp([FromBody] Emp emp)
        {
            if (emp == null)
            {
                return BadRequest("Emp object should not be null");
            }
            _empRepository.UpdateEmp(emp);
            return Ok($"Emp with Id {emp.EmpNo} is updated successfully");
        }

        //Delete emp
        [HttpDelete]
        [Route("DeleteEmp/{EmpNo}")]
        public IActionResult DeleteEmp(int EmpNo)
        {
            if (!_empRepository.EmpExists(EmpNo))
            {
                return NotFound($"Emp with Id {EmpNo} is not found");
            }
            _empRepository.DeleteEmp(EmpNo);
            return Ok($"Emp with Id {EmpNo} is deleted successfully");
        }

        //Route data
        //[HttpGet]
        //[Route("GetEmpDetailsByEmpId /{empId}")]
        [HttpGet("GetEmpDetailsByEmpId /{empId}")]
        public ActionResult<Emp> GetEmpDetailsByEmpId(int empId)
        {

            return _empRepository.GetEmpById(empId);
        }

        [HttpGet("GetEmpByIdOrName/EmpId/{empId}/Name/{name}")]
        public ActionResult<Emp> GetEmpByIdOrName(int empId, string name)
        {
            return _empRepository.GetEmpByIdOrName(empId, name);
        }

        [HttpGet("GetEmpDetailsByEmpName")]
        public ActionResult<Emp> GetEmpDetailsByEmpName([FromQuery]string empName)
        {
            return _empRepository.GetEmpByIdOrName(0, empName);
        }
        [HttpGet("GetEmpByIdOrNameByQueryParams")]
        public ActionResult<Emp> GetEmpByIdOrNameByQueryParams([FromQuery]int empId, [FromQuery]string name)
        {
            return _empRepository.GetEmpByIdOrName(empId, name);
        }

        //using query string with Model binding
        [HttpGet("InsertEmpByIdOrNameByModelBinding")]
        public ActionResult<Emp> InsertEmpByIdOrNameByModelBinding([FromQuery] Emp emp)
        {
            _empRepository.AddEmp(emp);
            return _empRepository.GetEmpById(emp.EmpNo);
        }

        //Reading Query string data from Httpcontext
        [HttpGet("GetEmpDataUsingHttpContext")]
        public ActionResult<Emp> SearchEmp()
        {
            var empName = HttpContext.Request.Query["empname"].ToString();
            var empId = int.Parse(HttpContext.Request.Query["id"].ToString());

            if(empName=="" ||  empName==null)
            {
                return BadRequest("EmpName is required");
            }
            if(empId==0)
            { return BadRequest("empId is required"); }

            return _empRepository.GetEmpByIdOrName(empId, empName);
        }

        [HttpGet("GetTopEmp/{empId}")]
        public ActionResult<Emp> GetTopEmp([FromRoute]int empId, [FromQuery]string empname)
        {
            return _empRepository.GetEmpByIdOrName(empId, empname);
        }
    }
}
