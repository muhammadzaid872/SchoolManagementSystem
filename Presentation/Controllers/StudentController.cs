using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Service.Interface;
using System;

namespace Presentation.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILoggerService _loggerService;
        private readonly IStudentService _studentService;
        ResponseDTO response;
        public StudentController(ILoggerService loggerService, IStudentService studentService)
        {
            _loggerService = loggerService;
            response = new ResponseDTO();
            _studentService = studentService;
        }

        public ActionResult Index()
        {
            var response = _studentService.GetStudentsList();
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            else
            {
                return Redirect(Constants.errorPage);
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                response = _studentService.Details(id);
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLog(ex);
                response.IsSuccess = false;
                response.Message = Constants.unexpectedError;
            }
            return Json(response);
        }
        [HttpPost]
        public ActionResult AddUpdate(StudentDTO student)
        {
            try
            {
                response = _studentService.AddUpdate(student);
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLog(ex);
                response.IsSuccess = false;
                response.Message = Constants.unexpectedError;
            }
            return Json(response);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                response = _studentService.Delete(id);
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLog(ex);
                response.IsSuccess = false;
                response.Message = Constants.unexpectedError;
            }
            return Json(response);
        }
    }
}
