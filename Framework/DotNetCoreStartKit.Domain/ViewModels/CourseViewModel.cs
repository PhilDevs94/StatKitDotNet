using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreStartKit.Domain.ViewModels
{
    public class CourseViewModel
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public List<StudentViewModel> StudentsList { get; set; }
    }
}
