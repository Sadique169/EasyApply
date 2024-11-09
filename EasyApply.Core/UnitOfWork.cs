using EasyApply.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApply.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;     
        public ICompanyRepository Company { get; }

        public UnitOfWork(
          AppDbContext context,
          ICompanyRepository company

         )
        {
            _context = context; 
            Company = company;
        }


        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
