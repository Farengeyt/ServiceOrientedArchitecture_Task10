using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Xml.Serialization;

namespace WebApiService.Controllers
{
    public class StudentListWebApiServiceController : ApiController
    {

        public StudentListWebApiServiceController()
        {
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.End();
            }
        }

        [HttpGet]
        [Route("GetStudentGratherThan")]
        public IEnumerable<Models.Student> GetStudentGratherThan(float mark)
        {
            var pathToFile = HostingEnvironment.MapPath("~/App_Data/StudentsXML.xml");
            XmlSerializer formatter = new XmlSerializer(typeof(Models.Student[]));
            Models.Student[] students;
            using (FileStream fs = new FileStream(pathToFile, FileMode.OpenOrCreate))
            {
                students = (Models.Student[])formatter.Deserialize(fs);
            }
            return students.Where(s => s.AvgMark > mark).ToArray();
        }

        [HttpGet]
        [Route("GetStudentLowerThan")]
        public IEnumerable<Models.Student> GetStudentLowerThan(float mark)
        {
            var pathToFile = HostingEnvironment.MapPath("~/App_Data/StudentsXML.xml");
            XmlSerializer formatter = new XmlSerializer(typeof(Models.Student[]));
            Models.Student[] students;
            using (FileStream fs = new FileStream(pathToFile, FileMode.OpenOrCreate))
            {
                students = (Models.Student[])formatter.Deserialize(fs);
            }
            return students.Where(s => s.AvgMark < mark).ToArray();
        }

        [HttpPost]
        [Route("GetStudentInRange")]
        public IEnumerable<Models.Student> GetStudentInRange([FromBody] (float minMark, float maxMark) paramsTuple)
        {
            var pathToFile = HostingEnvironment.MapPath("~/App_Data/StudentsXML.xml");
            XmlSerializer formatter = new XmlSerializer(typeof(Models.Student[]));
            Models.Student[] students;
            using (FileStream fs = new FileStream(pathToFile, FileMode.OpenOrCreate))
            {
                students = (Models.Student[])formatter.Deserialize(fs);
            }
            return students.Where(s => s.AvgMark >= paramsTuple.minMark && s.AvgMark <= paramsTuple.maxMark).ToArray();
        }
    }
}
