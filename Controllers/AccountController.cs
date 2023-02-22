﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MVC75NET.Contexts;
using MVC75NET.Models;
using MVC75NET.ViewModels;

namespace MVC75NET.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyContext context;
        public AccountController(MyContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var accounts = context.Accounts.ToList();
            return View(accounts);
        }

        public IActionResult Details(int id)
        {
            var accounts = context.Accounts.Find(id);
            return View(accounts);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            var accounts = context.Accounts.Find(id);
            return View(accounts);
        }
        public IActionResult Delete(int id)
        {
            var accounts = context.Accounts.Find(id);
            return View(accounts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Account account)
        {
            context.Add(account);
            var result = context.SaveChanges();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Account account)
        {
            context.Entry(account).State = EntityState.Modified;
            var result = context.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            var account = context.Accounts.Find(id);
            context.Remove(account);
            var result = context.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();

        }

        // GET : Account/Register
        public IActionResult Register()
        {
            var genders = new List<SelectListItem>{
            new SelectListItem
            {
                Value = "0",
                Text = "Male"
            },
            new SelectListItem
            {
                Value = "1",
                Text = "Female"
            },
        };

            ViewBag.Genders = genders;
            return View();
        }

        // POST : Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                // Bikin kondisi untuk mengecek apakah data university sudah ada
                University university = new University 
                {
                    Name = registerVM.UniversityName
                };
                if (context.Universities.Any(u => u.Name == registerVM.UniversityName))
                {
                    university.Id = context.Universities.FirstOrDefault(u => u.Name == university.Name).Id;
                }
                else
                {    
                  context.Universities.Add(university);
                  context.SaveChanges();
                }

                Education education = new Education
                {
                    Major = registerVM.Major,
                    Degree = registerVM.Degree,
                    Gpa = registerVM.GPA,
                    UniversityId = university.Id
                };
                context.Educations.Add(education);
                context.SaveChanges();

                Employee employee = new Employee
                {
                    NIK = registerVM.NIK,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    BirthDate = registerVM.BirthDate,
                    Gender = (Models.GenderEnum)registerVM.Gender,
                    HiringDate = registerVM.HiringDate,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber,
                };
                context.Employees.Add(employee);
                context.SaveChanges();

                Account account = new Account
                {
                    EmployeeNIK = registerVM.NIK,
                    Password = registerVM.Password
                };
                context.Accounts.Add(account);
                context.SaveChanges();

                AccountRole accountRole = new AccountRole
                {
                    AccountNIK = registerVM.NIK,
                    RoleId = 2
                };

                context.AccountRoles.Add(accountRole);
                context.SaveChanges();

                Profiling profiling = new Profiling
                {
                    EmployeeId = registerVM.NIK,
                    EducationId = education.Id
                };
                context.Profilings.Add(profiling);
                context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        //Method Get Untuk login 
        public IActionResult Login()
        {

            return View(); ;
        }
        //Method Post Untuk Login 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVM loginVM)
        {
            var tampil = context.Accounts.Join(
                context.Employees,
                a => a.EmployeeNIK,
                e => e.NIK,
                (a, e) => new LoginVM
                {
                    Email = e.Email,
                    Password = a.Password,
                });
            if (tampil.Any(e => e.Email == loginVM.Email && e.Password == loginVM.Password))
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Account or Password Not Found!");
            return View();
        }
    }
}