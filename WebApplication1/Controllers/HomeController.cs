﻿using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace EmployeeManagement.Controllers
{
    //[Route("[controller]/[action]")] //we can use token home will be taken here from controller name
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository,
            IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        /* public JsonResult Index()
         {
             return Json(new { id=1, name="project" });
         }*/
        /*public string Index()
        {
            //return Json(new { id=1, name="project" });
            return _employeeRepository.GetEmployee(3).Name;
        }*/

        //[Route("~/Home")]
        //[Route("~/")]
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }

        public JsonResult JsonDetails()
        {
            Employee empModel = _employeeRepository.GetEmployee(2);
            return Json(empModel);
        }

        public ViewResult Details()
        {
            Employee empModel = _employeeRepository.GetEmployee(2);
            return View(empModel);// by default View/Home/Details.cshtl will be looked
        }

        public ViewResult TestDetails()
        {
            Employee empModel = _employeeRepository.GetEmployee(2);
            //return View("../Test/Test")// relative path ../ mean one directory up from Home and look in mentioned folder
            return View("Test");// to break view naming convention to vies test.cshtml in case we are using generic path mention comlete path with cshtm extension
        }

        //View Data - Dictionary of weakly typed objects resolved only at runtime chances of spelling error are high
        //Default is string else we have to mention type on View Page
        public ViewResult ViewDataDetails()
        {
            Employee empModel = _employeeRepository.GetEmployee(2);
            ViewData["Employee"] = empModel;
            ViewData["PageTitle"] = "View Data Employee Details";
            return View(empModel);
        }

        //ViewBag - Dynamically allocates value will not check spelling mistake, type casting is also not required
        //Wealky Typed - No compile time checking
        public ViewResult ViewBagDetails()
        {
            Employee empModel = _employeeRepository.GetEmployee(3);
            ViewBag.Employee = empModel;
            ViewBag.PageTitle = "View Bag Details";
            return View(empModel);
        }

        // View Model - It is strongly typed. Better option to use
        // Allows compile time error checking
        //[Route("{id?}")]
        public ViewResult ViewModelDetails(int? id)
        {
            HomeViewModelDetails homeViewModelDetails = new HomeViewModelDetails()
            {
                Employee = _employeeRepository.GetEmployee(id ?? 1), //if null use default 1
                PageTitle = "View Model Details"
            };
            //Employee empModel = _employeeRepository.GetEmployee(3);
            //return View(empModel);
            return View(homeViewModelDetails);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    model.Photo.CopyToAsync(new FileStream(filePath, FileMode.Create));
                }
                Employee newEmployee = new Employee();
                newEmployee.Name = model.Name;
                newEmployee.Email = model.Email;
                newEmployee.Department = model.Department;
                newEmployee.Photopath = uniqueFileName;
                _employeeRepository.AddEmployee(newEmployee);            
                return RedirectToAction("viewmodeldetails", new { id = newEmployee.Id });//to test AddSindleton service injection
        }
            return View();
        }
    }
} 