using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC75NET.Contexts;
using MVC75NET.Models;

namespace MVC75NET.Controllers
{
    public class ProfilingController : Controller
    {
        private readonly MyContext context;
        public ProfilingController(MyContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var profilings = context.Profilings.ToList();
            return View(profilings);
        }

        public IActionResult Details(int id)
        {
            var profilings = context.Profilings.Find(id);
            return View(profilings);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            var profilings = context.Profilings.Find(id);
            return View(profilings);
        }
        public IActionResult Delete(int id)
        {
            var profilings = context.Profilings.Find(id);
            return View(profilings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Profiling profiling)
        {
            context.Add(profiling);
            var result = context.SaveChanges();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Profiling profiling)
        {
            context.Entry(profiling).State = EntityState.Modified;
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
            var profiling = context.Profilings.Find(id);
            context.Remove(profiling);
            var result = context.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
    }
}
