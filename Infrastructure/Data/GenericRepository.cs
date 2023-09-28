using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<T> GetEntitywithSpec(ISpecification<T> spec)
        {
            return await AppplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification)
        {
            return await AppplySpecification(specification).ToListAsync();
        }

        async Task<T> IGenericRepository<T>.GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        async Task<IReadOnlyList<T>> IGenericRepository<T>.ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        private IQueryable<T> AppplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
        }
    }
}