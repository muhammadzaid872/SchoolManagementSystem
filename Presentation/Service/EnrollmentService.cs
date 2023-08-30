using Presentation.Domain;
using Presentation.Models;
using Presentation.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Service
{
    public class EnrollmentService: IEnrollmentService
    {
        private ResponseDTO response;
        private readonly SchoolManagementDbContext _context;
        private readonly ILoggerService _loggerService;
        public EnrollmentService(SchoolManagementDbContext context, ILoggerService loggerService)
        {
            response = new ResponseDTO();
            _context = context;
            _loggerService = loggerService;
        }
        public ResponseDTO GetEnrollmentsList()
        {
            try
            {
                var enrollmentViewDTO = new EnrollmentViewDTO();
                enrollmentViewDTO.enrollmentList = new List<EnrollmentDTO>();
                enrollmentViewDTO.enrollmentList = _context.Enrollments.Where(x => x.IsDeleted != true).Select(e => new EnrollmentDTO
                {
                    EnrollmentId = e.EnrollmentId,
                    CourseName = e.Course.CourseName,
                    TeacherName = e.Teacher.FirstName+" "+e.Teacher.LastName,
                    StudentName = e.Student.FirstName+" "+e.Student.LastName,
                }).ToList();

                enrollmentViewDTO.studentList = _context.Students.Where(x => x.IsDeleted != true).Select(e => new IdNameDTO {
                Id=e.StudentId,
                Name=e.FirstName+" "+e.LastName
                }).ToList();

                enrollmentViewDTO.couresList = _context.Courses.Where(x => x.IsDeleted != true).Select(e => new IdNameDTO
                {
                    Id = e.CourseId,
                    Name = e.CourseName
                }).ToList();

                enrollmentViewDTO.teacherList = _context.Teachers.Where(x => x.IsDeleted != true).Select(e => new IdNameDTO
                {
                    Id = e.TeacherId,
                    Name = e.FirstName + " " + e.LastName
                }).ToList();

                response.Data = enrollmentViewDTO;
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

        public ResponseDTO AddUpdate(EnrollmentModel enrollmentDTO)
        {
            try
            {
                var enrollment = new Enrollment();
                enrollment.CourseId=enrollmentDTO.CourseId;
                enrollment.StudentId = enrollmentDTO.StudentId;
                enrollment.TeacherId = enrollmentDTO.TeacherId;

                _context.Enrollments.Add(enrollment);
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
        public ResponseDTO Delete(int id)
        {
            try
            {
                var enrollment = _context.Enrollments.Where(x => x.EnrollmentId.Equals(id)).FirstOrDefault();
                if (enrollment != null)
                {
                    enrollment.IsDeleted = true;
                    _context.Enrollments.Update(enrollment);
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
    }
}
