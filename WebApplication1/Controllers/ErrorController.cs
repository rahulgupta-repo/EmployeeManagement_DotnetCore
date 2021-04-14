using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger logger ;

        public  ErrorController(ILogger<ErrorController> logger) {
            this.logger = logger;
       }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            string targetPage="index";
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    /*ViewBag.ErrorMessage = "Sorry, resource cannot be found";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;*/ // Displaying error in page
                    logger.LogWarning($"404 Error Occured. Path = {statusCodeResult.OriginalPath} and QueryString={statusCodeResult.OriginalQueryString}");
                    targetPage = "NotFound";
                    break;

            }
            return View(targetPage);
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            logger.LogError($"The Path {exceptionDetails.Path} threw an exception {exceptionDetails.Error}");
            /*ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.Stacktrace = exceptionDetails.Error.StackTrace; 
            return View("ErrorView");*/                              // Displaying Error in view
            return View("Error");
        }
    }
}
