using Core.Domain;
using Core.DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EP_EF.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PackageDbContext _context;

        public EmployeeRepository(PackageDbContext context)
        {
            _context = context;
        }

        public async Task Add(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public bool Exists(int employeeNr) => _context.Employees.Any(e => e.EmployeeNumber == employeeNr);

       public async Task<Employee> GetEmployeeByEmployeeNumber(int employeeNumber)
        {
            var employee = _context.Employees.Include(x=> x.Canteen).FirstOrDefault(x => x.EmployeeNumber == employeeNumber);

            await _context.SaveChangesAsync();
            return employee;
        }
    }
}
