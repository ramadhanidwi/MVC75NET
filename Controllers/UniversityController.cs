using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC75NET.Contexts;
using MVC75NET.Models;
using MVC75NET.Repositories;
using NuGet.Protocol.Core.Types;

//Controler hanya sebagai penunjuk arah saja, untuk hubungan ke database menggunakan interface repository

namespace MVC75NET.Controllers
{
    public class UniversityController : Controller
    {
        private readonly UniversityRepository repository;

        public UniversityController(UniversityRepository repository)
        {
            this.repository = repository;
        }

        [Authorize(Roles ="Admin,User")]
        public IActionResult Index()
        {
            var universities = repository.GetAll();
            return View(universities);
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Details(int id)
        {
            var university = repository.GetById(id);
            return View(university);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var university = repository.GetById(id);
            return View(university);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var university = repository.GetById(id);
            return View(university);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(University university)
        {
            var result = repository.Insert(university);
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(University university)
        {
            var result = repository.Update(university);
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            var result = repository.Delete(id);
            if (result == 0)
            {
                //data tidak ditemukan
            }
            else
            {
                return RedirectToAction(nameof(Index));

            }
            return View();

        }
    }
}
