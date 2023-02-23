using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MVC75NET.Contexts;
using MVC75NET.Models;
using MVC75NET.Repositories;
using MVC75NET.ViewModels;
using NuGet.Protocol.Core.Types;

namespace MVC75NET.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyContext context;
        private readonly AccountRepository accRepository;

        public AccountController(MyContext context, AccountRepository accRepository)
        {
            this.context = context;
            this.accRepository = accRepository;
        }

        public IActionResult Index()
        {
            var accounts = accRepository.GetEmployeeAccount();
            return View(accounts);
        }

        public IActionResult Details(string id)
        {
            var accounts = accRepository.GetById(id);
            return View(accounts);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(string id)
        {
            var accounts = accRepository.GetById(id);
            return View(accounts);
        }
        public IActionResult Delete(string id)
        {
            var accounts = accRepository.GetById(id);
            return View(accounts);
        }

        //Method Get Untuk login 
        public IActionResult Login()
        {

            return View(); ;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Account account)
        {
            var result = accRepository.Insert(account);
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Account account)
        {
            var result = accRepository.Update(account);
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(string id)
        {
            var result = accRepository.Delete(id);
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
                var result = accRepository.Register(registerVM);
                if (result >0)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        //Method Post Untuk Login 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVM loginVM)
        {
            if (accRepository.Login(loginVM)) //Secara default if akan membandingkan nilai dengan true 
            {
                var userdata = accRepository.GetUserData(loginVM.Email);
                const string email = "email";
                const string fullname = "fullname";
                const string role = "role";

                HttpContext.Session.SetString(email, userdata.Email);
                HttpContext.Session.SetString(fullname, userdata.FullName);
                HttpContext.Session.SetString(role, userdata.Role);


                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Account or Password Not Found!");
            return View();
        }

    }
}
