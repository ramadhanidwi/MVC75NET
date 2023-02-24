using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC75NET.Contexts;
using MVC75NET.Models;

namespace MVC75NET.Controllers
{
    public class RoleController : Controller
    {
        private readonly MyContext context;
        public RoleController(MyContext context)
        {
            this.context = context;
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
            var roles = context.Roles.ToList();
            return View(roles);
        }

        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error"); //RedirectToAction(Method,Controler)
            }
            var roles = context.Roles.Find(id);
            return View(roles);
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

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error"); //RedirectToAction(Method,Controler)
            }
            var roles = context.Roles.Find(id);
            return View(roles);
        }

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error"); //RedirectToAction(Method,Controler)
            }
            var roles = context.Roles.Find(id);
            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Role role)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error"); //RedirectToAction(Method,Controler)
            }
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
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error"); //RedirectToAction(Method,Controler)
            }
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
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("Forbidden", "Error"); //RedirectToAction(Method,Controler)
            }
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
