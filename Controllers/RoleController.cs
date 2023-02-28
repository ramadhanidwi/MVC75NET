using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC75NET.Contexts;
using MVC75NET.Models;
using System.Data;

namespace MVC75NET.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly MyContext context;
        public RoleController(MyContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var roles = context.Roles.ToList();
            return View(roles);
        }

        public IActionResult Details(int id)
        {
            var roles = context.Roles.Find(id);
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var roles = context.Roles.Find(id);
            return View(roles);
        }

        public IActionResult Delete(int id)
        {
            var roles = context.Roles.Find(id);
            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Role role)
        {
            context.Add(role);
            var result = context.SaveChanges();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Role role)
        {
            context.Entry(role).State = EntityState.Modified;
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
            var role = context.Roles.Find(id);
            context.Remove(role);
            var result = context.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
