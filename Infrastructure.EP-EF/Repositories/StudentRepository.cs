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
    public class StudentRepository : IStudentRepository
    {
        private readonly PackageDbContext _context;

        public StudentRepository(PackageDbContext context)
        {
            _context = context;
        }

        public async Task Add(Student newStudent)
        {
            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();
        }
    }
}
