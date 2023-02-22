using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC75NET.Contexts;
using MVC75NET.Models;

namespace MVC75NET.Controllers
{
    public class AccountRoleController : Controller
    {
        private readonly MyContext context;
        public AccountRoleController(MyContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var accountRoles = context.AccountRoles.ToList();
            return View(accountRoles);
        }

        public IActionResult Details(int id)
        {
            var accountRoles = context.AccountRoles.Find(id);
            return View(accountRoles);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            var accountRoles = context.AccountRoles.Find(id);
            return View(accountRoles);
        }
        public IActionResult Delete(int id)
        {
            var accountRoles = context.AccountRoles.Find(id);
            return View(accountRoles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AccountRole accountRole)
        {
            context.Add(accountRole);
            var result = context.SaveChanges();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AccountRole accountRole)
        {
            context.Entry(accountRole).State = EntityState.Modified;
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
            var accountRole = context.AccountRoles.Find(id);
            context.Remove(accountRole);
            var result = context.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
    }
}
