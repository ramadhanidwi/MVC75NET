using MVC75NET.Contexts;
using MVC75NET.Models;
using MVC75NET.Repositories.Interface;
using MVC75NET.ViewModels;

namespace MVC75NET.Repositories
{
    public class EducationRepository : iRepository<int, Education>
    {
        private readonly MyContext context;
        private readonly UniversityRepository universityRepository;

        public EducationRepository(MyContext context, UniversityRepository universityRepository)
        {
            this.context = context;
            this.universityRepository = universityRepository;
        }

        public int Delete(int key)
        {
            throw new NotImplementedException();
        }

        public List<Education> GetAll()
        {
            return context.Educations.ToList(); 
        }


        public Education GetById(int key)
        {
            return context.Educations.Find(key);
        }

        public int Insert(Education entity)
        {
            throw new NotImplementedException();
        }

        public int Update(Education entity)
        {
            throw new NotImplementedException();
        }

        public List<EducationUnivVM> GetEducationUniversities()
        {
            var results = (from e in GetAll()
                           join u in universityRepository.GetAll()
                           on e.UniversityId equals u.Id
                           select new EducationUnivVM
                           {
                               Id = e.Id,
                               Degree = e.Degree,
                               Gpa = e.Gpa,
                               Major = e.Major,
                               UniversityName = u.Name
                           }).ToList();
            return results;
        }

        public EducationUnivVM GetEduUnivById(int key) 
        {
            var educations = GetById(key);
            var results = new EducationUnivVM
            {
                Id = educations.Id,
                Degree = educations.Degree,
                Gpa = educations.Gpa,
                Major = educations.Major,
                UniversityName = universityRepository.GetById(educations.UniversityId).Name
            };
            return results;
        }




    }
}
