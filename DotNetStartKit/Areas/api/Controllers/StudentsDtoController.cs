using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreStartKit.Core.UnitOfWork;
using DotNetCoreStartKit.Domain.ViewModels;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using static DotNetCoreStartKit.Service.StudentService;

namespace DotNetStartKit.Areas.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsDtoController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public StudentsDtoController(
            IStudentService studentService, 
            IUnitOfWorkAsync unitOfWorkAsync
        )
        {
            _studentService = studentService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }
        // GET: api/<StudentsDtoController>
        [HttpGet]
        [EnableQuery()]
        public async Task<IQueryable<StudentViewModel>> Get()
        {
            return await _studentService.GetAllStudentAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post(StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var student = await _studentService.InsertAsync(model);
                _unitOfWorkAsync.Commit();
                return Created("Created new Student", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
