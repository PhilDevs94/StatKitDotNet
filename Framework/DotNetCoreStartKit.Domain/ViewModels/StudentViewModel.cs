using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreStartKit.Domain.ViewModels
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; }
        public string StudentCode { get; set; }
        public bool Delete { get; set; }
        public List<CourseViewModel> CoursesList { get; set; }
    }
}
