using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC75NET.Contexts;
using MVC75NET.Models;
using MVC75NET.Repositories;
using MVC75NET.ViewModels;

namespace MVC75NET.Controllers
{
    public class EducationController : Controller
    {
        private readonly MyContext context;
        private readonly EducationRepository eduRepository;

        public EducationController(MyContext context, EducationRepository eduRepository)
        {
            this.context = context;
            this.eduRepository = eduRepository;
        }

        public IActionResult Index() //melakukan penjoinan pada tabel education dan
                                     //university menggunakan join method syntax 
        {
            var results = eduRepository.GetEducationUniversities();
            return View(results);
        }

        public IActionResult Details(int id)
        {
            return View(eduRepository.GetEduUnivById(id));
        }
        public IActionResult Create()
        {
            var universities = context.Universities.ToList()
            .Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
                
                ViewBag.UniversityId = universities;
            return View();
        }
        public IActionResult Edit(int id)
        {
            var universities = context.Universities.ToList()
            .Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
            ViewBag.UniversityId = universities;

            var educations = context.Educations.Find(id);
            return View(new EducationUnivVM
            {
                Id = educations.Id,
                Degree = educations.Degree,
                Gpa = educations.Gpa,
                Major = educations.Major,
                UniversityName = context.Universities.Find(educations.UniversityId).Name
            });
        }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EducationUnivVM education) // Melakukan Mapping ulang agar data yang dimasukanke dalam 
                                                           // database merupakan data original table university 
        {
            context.Add(new Education{
                Id = education.Id,
                Degree = education.Degree,
                Gpa = education.Gpa,
                Major = education.Major, 
                UniversityId = Convert.ToInt16(education.UniversityName)
            });
            var result = context.SaveChanges();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EducationUnivVM education)
        {
            context.Entry(new Education
            {
                Id = education.Id,
                Degree = education.Degree,
                Gpa = education.Gpa,
                Major = education.Major,
                UniversityId =Convert.ToInt32(education.UniversityName)
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
        public IActionResult Remove(int id)
        {
            var education = context.Educations.Find(id);
            context.Remove(education);
            var result = context.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
