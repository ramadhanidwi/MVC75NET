using Microsoft.EntityFrameworkCore;
using MVC75NET.Contexts;
using MVC75NET.Models;
using MVC75NET.Repositories.Interface;

namespace MVC75NET.Repositories
{
    public class UniversityRepository : iRepository<int, University>
    {
        private readonly MyContext context;
        public UniversityRepository(MyContext context)
        {
            this.context = context;
        }


        public int Delete(int key)
        {
            int result = 0;
            var university = GetById(key);
            if (university == null)
            {
                return result;
            }
            context.Remove(university);
            result = context.SaveChanges();

            return result;
        }

        public List<University> GetAll()
        {
            return context.Universities.ToList()?? null; 
        }

        public University GetById(int key)
        {
            return context.Universities.Find(key)?? null;
        }

        public int Insert(University entity)
        {
            int result = 0;
            context.Add(entity);
            result = context.SaveChanges();
            return result;
        }

        public int Update(University entity)
        {
            int result = 0;
            context.Entry(entity).State = EntityState.Modified;
            result = context.SaveChanges();
            return result;
        }
    }
}
