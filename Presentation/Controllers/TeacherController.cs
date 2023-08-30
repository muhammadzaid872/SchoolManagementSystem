using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Service.Interface;
using System;

namespace Presentation.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ILoggerService _loggerService;
        private readonly ITeacherService _teacherService;
        ResponseDTO response;
        public TeacherController(ILoggerService loggerService, ITeacherService teacherService)
        {
            _loggerService = loggerService;
            response = new ResponseDTO();
            _teacherService = teacherService;
        }

        public ActionResult Index()
        {
            var response = _teacherService.GetTeachersList();
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
                response = _teacherService.Details(id);
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
        public ActionResult AddUpdate(TeacherDTO teacher)
        {
            try
            {
                response = _teacherService.AddUpdate(teacher);
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
                response = _teacherService.Delete(id);
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
