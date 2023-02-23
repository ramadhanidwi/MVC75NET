using Microsoft.EntityFrameworkCore;
using MVC75NET.Contexts;
using MVC75NET.Models;
using MVC75NET.Repositories.Interface;
using System.Collections.Generic;
namespace MVC75NET.Repositories;

public class EmployeeRepository : iRepository<string, Employee>
{
    private readonly MyContext context;

    public EmployeeRepository(MyContext context)
    {
        this.context = context;
    }
    public int Delete(string key)
    {
        var result = 0;
        var employee = GetById(key);
        if (employee == null)
        {
            return result;
        }
        context.Remove(employee);
        result = context.SaveChanges();
        return result;
    }

    public List<Employee> GetAll()
    {
        return context.Employees.ToList();
    }

    public Employee GetById(string key)
    {
        return context.Employees.Find(key);
    }

    public int Insert(Employee entity)
    {
        int result = 0;
        context.Add(entity);
        result = context.SaveChanges();
        return result;
    }

    public int Update(Employee entity)
    {
        int result = 0;
        context.Entry(entity).State = EntityState.Modified; ;
        result = context.SaveChanges();
        return result;
    }
}
