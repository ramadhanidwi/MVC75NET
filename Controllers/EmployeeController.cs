using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC75NET.Contexts;
using MVC75NET.Models;
using MVC75NET.Repositories;
using MVC75NET.ViewModels;

namespace MVC75NET.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MyContext context;
        private readonly EmployeeRepository empRepository;

        public EmployeeController(MyContext context, EmployeeRepository empRepository)
        {
            this.context = context;
            this.empRepository = empRepository;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error"); //RedirectToAction(Method,Controler)
            }
            var tampil = empRepository.GetAll()
                .Select(e => new EmployeeVM
                {
                    NIK = e.NIK,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    BirthDate = e.BirthDate,
                    Gender = (ViewModels.GenderEnum)e.Gender,
                    HiringDate = e.HiringDate,
                    Email= e.Email,
                    PhoneNumber = e.PhoneNumber
                });
            return View(tampil);
        }

        public IActionResult Details(string id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error"); //RedirectToAction(Method,Controler)
            }
            var employees = empRepository.GetById(id);
            return View(new EmployeeVM
            {
                NIK = employees.NIK,
                FirstName = employees.FirstName,
                LastName = employees.LastName,
                BirthDate = employees.BirthDate,
                Gender = (ViewModels.GenderEnum)employees.Gender,
                HiringDate = employees.HiringDate,
                Email = employees.Email,
                PhoneNumber = employees.PhoneNumber
            }) ;
        }
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error"); //RedirectToAction(Method,Controler)
            }
            return View();
        }

        public IActionResult Edit(string id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error"); //RedirectToAction(Method,Controler)
            }
            var employees = empRepository.GetById(id);
            return View(new EmployeeVM
            {
                NIK = employees.NIK,
                FirstName = employees.FirstName,
                LastName = employees.LastName,
                BirthDate = employees.BirthDate,
                Gender = (ViewModels.GenderEnum)employees.Gender,
                HiringDate = employees.HiringDate,
                Email = employees.Email,
                PhoneNumber = employees.PhoneNumber
            });
        }
        public IActionResult Delete(string id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error"); //RedirectToAction(Method,Controler)
            }
            var employees = empRepository.GetById(id);
            return View(new EmployeeVM
            {
                NIK = employees.NIK,
                FirstName = employees.FirstName,
                LastName = employees.LastName,
                BirthDate = employees.BirthDate,
                Gender = (ViewModels.GenderEnum)employees.Gender,
                HiringDate = employees.HiringDate,
                Email = employees.Email,
                PhoneNumber = employees.PhoneNumber
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeVM employee)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error"); //RedirectToAction(Method,Controler)
            }
            var result = empRepository.Insert(new Employee
            {
                NIK = employee.NIK,
                FirstName = employee.FirstName,
                LastName= employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = (Models.GenderEnum)employee.Gender,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber
            });
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeVM employee)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error"); //RedirectToAction(Method,Controler)
            }
            var result = empRepository.Update(new Employee
            {
                NIK = employee.NIK,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = (Models.GenderEnum)employee.Gender,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber
            });
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(string NIK)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error"); //RedirectToAction(Method,Controler)
            }
            var result = empRepository.Delete(NIK);
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
    }
}
