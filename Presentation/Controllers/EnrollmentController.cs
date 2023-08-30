using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Service.Interface;
using System;

namespace Presentation.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ILoggerService _loggerService;
        private readonly IEnrollmentService _enrollmentService;
        ResponseDTO response;
        public EnrollmentController(ILoggerService loggerService, IEnrollmentService enrollmentService)
        {
            _loggerService = loggerService;
            response = new ResponseDTO();
            _enrollmentService = enrollmentService;
        }

        public ActionResult Index()
        {
            response = _enrollmentService.GetEnrollmentsList();
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            else
            {
                return Redirect(Constants.errorPage);
            }
        }
        [HttpPost]
        public ActionResult AddUpdate(EnrollmentModel enrollment)
        {
            try
            {
                response = _enrollmentService.AddUpdate(enrollment);
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
                response = _enrollmentService.Delete(id);
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
