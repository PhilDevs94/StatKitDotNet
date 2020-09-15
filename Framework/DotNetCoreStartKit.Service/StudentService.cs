using DotNetCoreStartKit.Core.DataContext;
using DotNetCoreStartKit.Core.Model;
using DotNetCoreStartKit.Core.Repository;
using DotNetCoreStartKit.Data;
using DotNetCoreStartKit.Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using static DotNetCoreStartKit.Service.StudentService;

namespace DotNetCoreStartKit.Service
{
    public class StudentService :Service<Student>, IStudentService
    {
        public interface IStudentService :IService<Student>
        {
            Task<IQueryable<StudentViewModel>> GetAllStudentAsync();
            Task<Student> InsertAsync(StudentViewModel model);
            Task<StudentViewModel> UpdateAsync(StudentViewModel model);
            bool Delete(Guid Id);
        }
        private readonly IRepositoryAsync<Student> _repository;
        private readonly IRepository<ApplicationUser> _userRepository;
        protected readonly DataContext db;
        protected UserManager<ApplicationUser> userManager;

        public StudentService (
            IRepositoryAsync<Student> repository,
            IRepositoryAsync<ApplicationUser> userRepository,
            DbContextOptions<DataContext> dbOptions
        ) : base(repository)
        {
            _repository = repository;
            _userRepository = userRepository;
            db = new DataContext(dbOptions);
        }

        public IQueryable<StudentViewModel> GetAllStudent()
        {
            return _repository.Queryable().Where(x => x.Delete == false)
                .Select( x => new StudentViewModel()
                    {
                        Id = x.Id,
                        StudentName = x.Name,
                        StudentCode = x.StudentCode,
                    }
                );
        }

        public Task<IQueryable<StudentViewModel>> GetAllStudentAsync()
        {
           return Task.Run(() => GetAllStudent());
        }

        public Student Insert(StudentViewModel model)
        {
            var findResult = Queryable().Where(x => x.Name.ToLower().Trim() == model.StudentName.ToLower().Trim() && x.Delete == false).FirstOrDefault();
            if(findResult == null )
            {
                var data = new Student
                {
                    CreatDate = DateTime.Now,
                    StudentCode = model.StudentCode,
                    Name = model.StudentName,
                    Delete = false,
                    LastModifiedDate = DateTime.Now
                };
                base.Insert(data);
                return data;
            } 
            else
            {
                throw new Exception("The student name is existed in the databae");
            }
        
        }

        public Task<Student> InsertAsync(StudentViewModel model)
        {
            return Task.Run(() => Insert(model));
        }

        public bool Update(StudentViewModel model)
        {
            var data = Find(model.Id);
            if(data != null)
            {
                if(model.StudentName.ToLower().Trim() != "")
                {
                    data.Name = model.StudentName;
                }
                if(model.StudentCode.ToLower().Trim() != "")
                {
                    data.StudentCode = model.StudentCode;
                }
                return true;
            }
            else
            {
                throw new Exception("No student record found");
            }
        }

        public async Task<StudentViewModel> UpdateAsync(StudentViewModel model)
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
            if(result != null)
            {
                result.Delete = true;
                result.LastModifiedDate = DateTime.Now;
                return true;
            } else
            {
                throw new Exception("No student records found");
            }
        }
    }
}
