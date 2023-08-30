using Presentation.Domain;
using Presentation.Models;
using Presentation.Service.Interface;
using System;
using System.Linq;

namespace Presentation.Service
{
    public class TeacherService: ITeacherService
    {

        private ResponseDTO response;
        private readonly SchoolManagementDbContext _context;
        private readonly ILoggerService _loggerService;
        public TeacherService(SchoolManagementDbContext context, ILoggerService loggerService)
        {
            response = new ResponseDTO();
            _context = context;
            _loggerService = loggerService;
        }

        public ResponseDTO GetTeachersList()
        {
            try
            {
                response.Data = _context.Teachers.Where(x => x.IsDeleted != true).Select(e => new TeacherDTO
                {
                    TeacherId = e.TeacherId,
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
                response.Data = _context.Teachers.Where(x => x.TeacherId == id).Select(e => new TeacherDTO
                {
                    TeacherId = e.TeacherId,
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
        public ResponseDTO AddUpdate(TeacherDTO teacher)
        {
            try
            {
                response = teacher.TeacherId == 0 ? this._save(teacher) : _update(teacher);
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
                var teacher = _context.Teachers.Where(x => x.TeacherId.Equals(id)).FirstOrDefault();
                if (teacher != null)
                {
                    teacher.IsDeleted = true;
                    _context.Teachers.Update(teacher);
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
        private ResponseDTO _save(TeacherDTO teacherDTO)
        {
            try
            {
                var teacher = new Teacher();
                teacher.FirstName = teacherDTO.FirstName;
                teacher.LastName = teacherDTO.LastName;
                teacher.Address = teacherDTO.Address;
                _context.Teachers.Add(teacher);
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
        private ResponseDTO _update(TeacherDTO teacherDTO)
        {
            try
            {
                var teacher = _context.Teachers.Where(x => x.TeacherId == teacherDTO.TeacherId).FirstOrDefault();
                if (teacher != null)
                {
                    teacher.FirstName = teacherDTO.FirstName;
                    teacher.LastName = teacherDTO.LastName;
                    teacher.Address = teacherDTO.Address;

                    _context.Teachers.Update(teacher);
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
