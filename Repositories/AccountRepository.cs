using Microsoft.EntityFrameworkCore;
using MVC75NET.Contexts;
using MVC75NET.Models;
using MVC75NET.Repositories.Interface;
using MVC75NET.ViewModels;

namespace MVC75NET.Repositories;

public class AccountRepository : iRepository<string, Account>
{
    private readonly MyContext context;
    private readonly EmployeeRepository empRepository;

    public AccountRepository (MyContext context, EmployeeRepository empRepository)
    {
        this.context = context;
        this.empRepository = empRepository;
    }

    public int Delete(string key)
    {
        var result = 0;
        var account = GetById(key);
        if (account == null)
        {
            return result;
        }
        context.Remove(account);
        result = context.SaveChanges();
        return result;
    }

    public List<Account> GetAll()
    {
        return context.Accounts.ToList();
    }

    public Account GetById(string key)
    {
        return context.Accounts.Find(key);
    }

    public int Insert(Account entity)
    {
        int result = 0;
        context.Add(entity);
        result = context.SaveChanges();
        return result;
    }

    public int Update(Account entity)
    {
        int result = 0;
        context.Entry(entity).State = EntityState.Modified; ;
        result = context.SaveChanges();
        return result;
    }

    
}
