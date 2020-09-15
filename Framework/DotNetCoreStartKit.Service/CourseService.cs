using DotNetCoreStartKit.Core.DataContext;
using DotNetCoreStartKit.Core.Model;
using DotNetCoreStartKit.Core.Repository;
using DotNetCoreStartKit.Data;
using DotNetCoreStartKit.Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using static DotNetCoreStartKit.Service.CourseService;

namespace DotNetCoreStartKit.Service
{
    public class CourseService : Service<Course>, ICourseService
    {
        public interface ICourseService : IService<Course>
        {
            Task<IQueryable<CourseViewModel>> GetAllCourseAsync();
            Task<Course> InsertAsync(CourseViewModel model);
            Task<CourseViewModel> UpdateAsync(CourseViewModel model);
            bool Delete(Guid Id);
        }
        private readonly IRepositoryAsync<Course> _repository;
        private readonly IRepository<ApplicationUser> _userRepository;
        protected readonly DataContext db;

        public CourseService(
            IRepositoryAsync<Course> repository,
            IRepositoryAsync<ApplicationUser> userRepository,
            IDataContext dataContext
        ) : base(repository)
        {
            _repository = repository;
            _userRepository = userRepository;
            db = dataContext as DataContext;
        }

        public IQueryable<CourseViewModel> GetAllCourse()
        {
            return _repository.Queryable().Include(x => x.StudentCourses)
                .Where(x => x.Delete == false).Select(x => new CourseViewModel()
                {
                    Id = x.Id,
                    CourseName = x.CourseName,
                    CourseCode = x.CourseCode,
                    StudentsList = x.StudentCourses.Where(y => y.Delete == false)
                    .Select(y => new StudentViewModel()
                    {
                        Id = y.Id,
                        StudentName = y.Student.Name,
                        StudentCode = y.Student.StudentCode
                    }).ToList()
                });
        }
        public Task<IQueryable<CourseViewModel>> GetAllCourseAsync()
        {
            return Task.Run(() => GetAllCourse());
        }
        public Course Insert(CourseViewModel model)
        {
            var findResult = Queryable().Where(x => x.CourseName.ToLower().Trim() == model.CourseName.ToLower().Trim() && x.Delete == false).FirstOrDefault();
            if(findResult != null)
            {
                throw new Exception("Course existed");
            } else
            {
                var data = new Course();
                data.CourseName = model.CourseName;
                data.CourseCode = model.CourseCode;
                data.CreatDate = DateTime.Now;
                data.LastModifiedDate = DateTime.Now;
                base.Insert(data);
                return data;
            }
        }
        public Task<Course> InsertAsync(CourseViewModel model)
        {
            return Task.Run(() => Insert(model));
        }
        public bool Update(CourseViewModel model)
        {
            var data = Find(model.Id);
            if (data != null)
            {
                if (model.CourseName.ToLower().Trim() != "")
                {
                    data.CourseName = model.CourseName;
                }
                if (model.CourseCode.ToLower().Trim() != "")
                {
                    data.CourseCode = model.CourseCode;
                }
                return true;
            }
            else
            {
                throw new Exception("No course record found");
            }
        }
        public async Task<CourseViewModel> UpdateAsync(CourseViewModel model)
        {
           try
            {
                await Task.Run(() => Update(model));
                return model;
            }
            catch(Exception e)
            {
                throw (e);
            }
        }
        public bool Delete(Guid Id)
        {
            var result = Find(Id);
            if (result != null)
            {
                result.Delete = true;
                result.LastModifiedDate = DateTime.Now;
                return true;
            }
            else
            {
                throw new Exception("No course records found");
            }
        }
    }
}
