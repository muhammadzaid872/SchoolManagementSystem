using Presentation.Domain;
using Presentation.Models;
using Presentation.Service.Interface;
using System;
using System.Linq;

namespace Presentation.Service
{
    public class CourseService : ICourseService
    {
        private ResponseDTO response;
        private readonly SchoolManagementDbContext _context;
        private readonly ILoggerService _loggerService;
        public CourseService(SchoolManagementDbContext context, ILoggerService loggerService)
        {
            response = new ResponseDTO();
            _context = context;
            _loggerService = loggerService;
        }
        public ResponseDTO GetCouresList()
        {
            try
            {
                response.Data = _context.Courses.Where(x => x.IsDeleted != true).Select(e => new CourseDTO
                {
                    CourseId = e.CourseId,
                    CourseName = e.CourseName,
                    Credits = e.Credits,
                    Description = e.Description
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
                response.Data = _context.Courses.Where(x => x.CourseId == id).Select(e => new CourseDTO
                {
                    CourseId = e.CourseId,
                    CourseName = e.CourseName,
                    Credits = e.Credits,
                    Description = e.Description
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

        public ResponseDTO AddUpdate(CourseDTO course)
        {
            try
            {
                response = course.CourseId == 0 ? this._save(course) : _update(course);
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
                var course = _context.Courses.Where(x => x.CourseId.Equals(id)).FirstOrDefault();
                if (course != null)
                {
                    course.IsDeleted = true;
                    _context.Courses.Update(course);
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




        private ResponseDTO _save(CourseDTO courseDTO)
        {
            try
            {
                var course = new Course();
                course.CourseName = courseDTO.CourseName;
                course.Description = courseDTO.Description;
                course.Credits = courseDTO.Credits;
                _context.Courses.Add(course);
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
        private ResponseDTO _update(CourseDTO courseDTO)
        {
            try
            {
                var course = _context.Courses.Where(x => x.CourseId == courseDTO.CourseId).FirstOrDefault();
                if (course != null)
                {
                    course.CourseName = courseDTO.CourseName;
                    course.Description = courseDTO.Description;
                    course.Credits = courseDTO.Credits;
                    _context.Courses.Update(course);
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
