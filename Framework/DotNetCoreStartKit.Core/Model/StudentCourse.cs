using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreStartKit.Core.Model
{
    public class StudentCourse : Entity
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
