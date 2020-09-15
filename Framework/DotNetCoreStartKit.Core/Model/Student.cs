using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DotNetCoreStartKit.Core.Model
{
    public class Student : Entity 
    {
       public string Name { get; set; }
       public string StudentCode { get; set; }
       public virtual IEnumerable<StudentCourse> StudentCourses { get; set; }
    }
}
