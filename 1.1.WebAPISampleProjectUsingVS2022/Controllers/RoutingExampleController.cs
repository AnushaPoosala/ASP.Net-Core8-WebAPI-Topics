using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPISampleProjectUsingVS2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutingExampleController : ControllerBase
    {
        [HttpGet("{id}")]
        public string GetCourse(int id)
        {
            return "Hello from the Routing ExampleController";
        }
        [Route("course/All")]
        [HttpGet]
        public string GetAllCourses()
        {
            return "1.Java, 2..Net, 3.Python";
        }
        [Route("course/ById/{courseId}")]
        [HttpGet]
        public string GetCoursesById(int courseId)
        {
            if (courseId == 1)
                return "Java";
            else if (courseId == 2)
                return ".Net";
            else if (courseId == 3)
                return "Python";
            else
                return "Invalid CourseId";


        }
    }
}
