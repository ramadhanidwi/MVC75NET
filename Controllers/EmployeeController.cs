using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC75NET.Contexts;
using MVC75NET.Models;
using MVC75NET.ViewModels;

namespace MVC75NET.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MyContext context;
        public EmployeeController(MyContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var tampil = context.Employees.ToList()
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
            var employees = context.Employees.Find(id);
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
            //var employees = context.Employees.ToList();

            //ViewBag.Employees = employees;
            return View();
        }
        public IActionResult Edit(string id)
        {
            var employees = context.Employees.Find(id);
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
            var employees = context.Employees.Find(id);
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
            context.Add(new Employee
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
            var result = context.SaveChanges();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeVM employee)
        {
            context.Entry(new Employee
            {
                NIK = employee.NIK,
                FirstName = employee.FirstName, 
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = (Models.GenderEnum)employee.Gender,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber
            }).State = EntityState.Modified;
            var result = context.SaveChanges();
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
            var employee = context.Employees.Find(NIK);
            context.Remove(employee);
            var result = context.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
    }
}
