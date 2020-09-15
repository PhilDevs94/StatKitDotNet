using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreStartKit.Core.Model
{
    public class Course : Entity
    {
        
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public virtual IEnumerable<StudentCourse> StudentCourses { get; set; }
    }
}
