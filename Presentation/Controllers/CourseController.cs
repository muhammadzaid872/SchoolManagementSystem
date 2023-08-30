using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Service.Interface;
using System.Collections.Generic;
using Presentation.Domain;
using Presentation.Models;
using System;

namespace Presentation.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _couserService;
        private readonly ILoggerService _loggerService;
        ResponseDTO response;

        public CourseController(ICourseService couserService, ILoggerService loggerService)
        {
            _couserService = couserService;
            _loggerService = loggerService;
            response = new ResponseDTO();
        }
        public ActionResult Index()
        {
            var response = _couserService.GetCouresList();
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
                response= _couserService.Details(id);
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
        public ActionResult AddUpdate(CourseDTO course)
        {
            try
            {
               response=_couserService.AddUpdate(course);
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
                response = _couserService.Delete(id);
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
