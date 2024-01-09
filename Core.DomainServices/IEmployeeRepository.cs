using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;


namespace Core.DomainServices
{
    public interface IEmployeeRepository
    {
        Task Add(Employee newEmployee);
        bool Exists(int employeeNr);
        IEnumerable<Employee> GetAll();
        Task Remove(Employee employee);
    }
}
