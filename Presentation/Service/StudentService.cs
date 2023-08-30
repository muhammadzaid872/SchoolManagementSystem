using Presentation.Domain;
using Presentation.Models;
using Presentation.Service.Interface;
using System;
using System.Linq;

namespace Presentation.Service
{
    public class StudentService : IStudentService
    {
        private ResponseDTO response;
        private readonly SchoolManagementDbContext _context;
        private readonly ILoggerService _loggerService;
        public StudentService(SchoolManagementDbContext context, ILoggerService loggerService)
        {
            response = new ResponseDTO();
            _context = context;
            _loggerService = loggerService;
        }
        public ResponseDTO GetStudentsList()
        {
            try
            {
                response.Data = _context.Students.Where(x => x.IsDeleted != true).Select(e => new StudentDTO
                {
                    StudentId = e.StudentId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Address = e.Address
                }).ToList();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLog(ex);
                response.IsSuccess = false;
                response.Message = Constants.unexpectedError;
            }
            return response;
        }
        public ResponseDTO Details(int id)
        {
            try
            {
                response.Data = _context.Students.Where(x => x.StudentId == id).Select(e => new StudentDTO
                {
                    StudentId = e.StudentId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Address = e.Address
                }).FirstOrDefault();
                response.IsSuccess = true;
                response.Message = Constants.recordFetched;
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLog(ex);
                response.IsSuccess = false;
                response.Message = Constants.unexpectedError;
            }
            return response;
        }
        public ResponseDTO AddUpdate(StudentDTO student)
        {
            try
            {
                response = student.StudentId == 0 ? this._save(student) : _update(student);
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLog(ex);
                response.IsSuccess = false;
                response.Message = Constants.unexpectedError;
            }
            return response;
        }
        public ResponseDTO Delete(int id)
        {
            try
            {
                var student = _context.Students.Where(x => x.StudentId.Equals(id)).FirstOrDefault();
                if (student != null)
                {
                    student.IsDeleted = true;
                    _context.Students.Update(student);
                    _context.SaveChanges();
                }
                response.IsSuccess = true;
                response.Message = Constants.recordDeleted;
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLog(ex);
                response.IsSuccess = false;
                response.Message = Constants.unexpectedError;
            }
            return response;
        }
        private ResponseDTO _save(StudentDTO studentDTO)
        {
            try
            {
                var student = new Student();
                student.FirstName=studentDTO.FirstName;
                student.LastName = studentDTO.LastName;
                student.Address = studentDTO.Address;
                _context.Students.Add(student);
                _context.SaveChanges();

                response.IsSuccess = true;
                response.Message = Constants.recordCreated;
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLog(ex);
                response.IsSuccess = false;
                response.Message = Constants.unexpectedError;
            }
            return response;
        }
        private ResponseDTO _update(StudentDTO studentDTO)
        {
            try
            {
                var student = _context.Students.Where(x => x.StudentId == studentDTO.StudentId).FirstOrDefault();
                if (student != null)
                {
                    student.FirstName = studentDTO.FirstName;
                    student.LastName = studentDTO.LastName;
                    student.Address = studentDTO.Address;

                    _context.Students.Update(student);
                    _context.SaveChanges();
                }
                response.IsSuccess = true;
                response.Message = Constants.recordUpdated;
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLog(ex);
                response.IsSuccess = false;
                response.Message = Constants.unexpectedError;
            }
            return response;
        }
    }
}
