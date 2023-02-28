using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC75NET.Contexts;
using MVC75NET.Models;
using MVC75NET.Repositories;
using MVC75NET.ViewModels;
using System.Data;

namespace MVC75NET.Controllers
{
    public class EducationController : Controller
    {
        private readonly MyContext context;
        private readonly EducationRepository eduRepository;
        private readonly UniversityRepository universityRepository;

        public EducationController(MyContext context, EducationRepository eduRepository, UniversityRepository universityRepository)
        {
            this.context = context;
            this.eduRepository = eduRepository;
            this.universityRepository = universityRepository;
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Index() //melakukan penjoinan pada tabel education dan
                                     //university menggunakan join method syntax 
        {
            var results = eduRepository.GetEducationUniversities();
            return View(results);
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Details(int id)
        {
            return View(eduRepository.GetEduUnivById(id));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var universities = universityRepository.GetAll()
            .Select(u => new SelectListItem //buat menampilkan nama dari univ nya saja
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
            ViewBag.UniversityId = universities;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var universities = universityRepository.GetAll()
            .Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
            ViewBag.UniversityId = universities;

            var educations = eduRepository.GetById(id);
            return View(new EducationUnivVM
            {
                Id = educations.Id,
                Degree = educations.Degree,
                Gpa = educations.Gpa,
                Major = educations.Major,
                UniversityName = context.Universities.Find(educations.UniversityId).Name
            });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var educations = context.Educations.Find(id);
            return View(new EducationUnivVM
            {
                Id = educations.Id,
                Degree = educations.Degree,
                Gpa = educations.Gpa,
                Major = educations.Major,
                UniversityName = context.Universities.Find(educations.UniversityId).Name
            }) ; 
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EducationUnivVM education) // Melakukan Mapping ulang agar data yang dimasukanke dalam 
                                                           // database merupakan data original table university 
        {
            var result = eduRepository.Insert(
                new Education{
                Id = education.Id,
                Degree = education.Degree,
                Gpa = education.Gpa,
                Major = education.Major, 
                UniversityId = Convert.ToInt16(education.UniversityName)
            });
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EducationUnivVM education)
        {
            var result = eduRepository.Update(new Education
            {
                Id = education.Id,
                Degree = education.Degree,
                Gpa = education.Gpa,
                Major = education.Major,
                UniversityId =Convert.ToInt32(education.UniversityName)
            }); 

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
            var result = eduRepository.Delete(id);
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
