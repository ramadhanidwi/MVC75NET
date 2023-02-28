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

    public bool Login(LoginVM loginVM)
    {
        var getAccounts = context.Employees.Join(
            context.Accounts,
            e => e.NIK,
            a => a.EmployeeNIK,
            (e, a) => new LoginVM
            {
                Email = e.Email,
                Password = a.Password
            });

        return getAccounts.Any(e => e.Email == loginVM.Email && e.Password == loginVM.Password);
    }

    public int Register(RegisterVM registerVM)
    {
        int result = 0;
        University university = new University
        {
            Name = registerVM.UniversityName
        };

        // Bikin kondisi untuk mengecek apakah data university sudah ada
        if (context.Universities.Any(u => u.Name == university.Name))
        {
            university.Id = context.Universities
                .FirstOrDefault(u => u.Name == university.Name)
                .Id;
        }
        else
        {
            context.Universities.Add(university);
            result = context.SaveChanges();
        }

        Education education = new Education
        {
            Major = registerVM.Major,
            Degree = registerVM.Degree,
            Gpa = registerVM.GPA,
            UniversityId = university.Id
        };
        context.Educations.Add(education);
        result = context.SaveChanges();

        Employee employee = new Employee
        {
            NIK = registerVM.NIK,
            FirstName = registerVM.FirstName,
            LastName = registerVM.LastName,
            BirthDate = registerVM.BirthDate,
            Gender = (Models.GenderEnum)registerVM.Gender,
            HiringDate = registerVM.HiringDate,
            Email = registerVM.Email,
            PhoneNumber = registerVM.PhoneNumber,
        };
        context.Employees.Add(employee);
        result = context.SaveChanges();

        Account account = new Account
        {
            EmployeeNIK = registerVM.NIK,
            Password = registerVM.Password
        };
        context.Accounts.Add(account);
        result = context.SaveChanges();

        AccountRole accountRole = new AccountRole
        {
            AccountNIK = registerVM.NIK,
            RoleId = 2
        };

        context.AccountRoles.Add(accountRole);
        result = context.SaveChanges();

        Profiling profiling = new Profiling
        {
            EmployeeId = registerVM.NIK,
            EducationId = education.Id
        };
        context.Profilings.Add(profiling);
        result = context.SaveChanges();

        return result;
    }

    public List<AccountEmployeeVM> GetEmployeeAccount()
    {
        var results = (from a in GetAll()
                       join e in empRepository.GetAll()
                       on a.EmployeeNIK equals e.NIK
                       select new AccountEmployeeVM
                       {
                            Email = e.Email,
                            Password = a.Password
                       }).ToList();
        return results;
    }

    public UserdataVM GetUserData(string email)
    {
        //Menggunakan Method Syntax
        /*var userdataMethod = context.Employees
          .Join(context.Accounts,
          e => e.NIK,
          a => a.EmployeeNIK,
          (e, a) => new { e, a })
          .Join(context.AccountRoles,
          ea => ea.a.EmployeeNIK,
          ar => ar.AccountNIK,
          (ea, ar) => new { ea, ar })
          .Join(context.Roles,
          eaar => eaar.ar.RoleId,
          r => r.Id,
          (eaar, r) => new UserdataVM
          {
              Email = eaar.ea.e.Email,
              FullName = String.Concat(eaar.ea.e.FirstName, eaar.ea.e.LastName),
              Role = r.Name
          }).FirstOrDefault(u => u.Email == email);*/

        //Menggunakan Query Syntax 
        var userdata = (from e in context.Employees //Seharusnya jangan pake context tapi import dari repository nya table bersangkutan
                        join a in context.Accounts
                        on e.NIK equals a.EmployeeNIK
                        join ar in context.AccountRoles
                        on a.EmployeeNIK equals ar.AccountNIK
                        join r in context.Roles
                        on ar.RoleId equals r.Id
                        where e.Email == email
                        select new UserdataVM
                        {
                            Email = e.Email,
                            FullName = String.Concat(e.FirstName, " ", e.LastName)
                        }).FirstOrDefault();

        return userdata;
    }

    public List<string> GetRolesByNIK(string email)
    {
        var getNIK = context.Employees.FirstOrDefault(e => e.Email == email);
        return context.AccountRoles.Where(ar => ar.AccountNIK == getNIK.NIK).Join(
            context.Roles,
            ar => ar.RoleId,
            r => r.Id,
            (ar, r) => r.Name).ToList();
    }
}
