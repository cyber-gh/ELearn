using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearn.Application.Generic;
using ELearn.Application.Repositories;
using ELearn.Domain;
using Microsoft.EntityFrameworkCore;

namespace ELearn.Infrastructure.Entity.Repositories
{
    public class CategoryRepo: ICategoriesRepo
    {

        private readonly EntityContext _context;

        public CategoryRepo(EntityContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var l = await _context.Categories.Select(p => new Domain.Category(p.Id, p.Name)).ToListAsync();

            return l;
        }

        public async Task<Category> Create(Category value)
        {
            await _context.Categories.AddAsync(new Models.Category(value.Id, value.Name));

            await _context.SaveChangesAsync();
            return value;
        }

        public async Task<Category?> Read(Guid idx)
        {
            var c = await _context.Categories.FirstOrDefaultAsync(p => p.Id == idx);

            if (c == null) return null;

            return new Domain.Category(c.Id, c.Name);
        }

        public async Task<Category> Update(Guid idx, Category value)
        {

            var c = new Models.Category(value.Id, value.Name);
            _context.Entry(await _context.Categories.FirstOrDefaultAsync(x => x.Id == idx)).CurrentValues.SetValues(c);
           await _context.SaveChangesAsync();

            return value;
        }

        public async Task Delete(Guid idx)
        {
            var toDelete = await _context.Categories.FirstOrDefaultAsync(p => p.Id == idx);
            if (toDelete != null)
            {
                _context.Categories.Remove(toDelete);
            }
            await _context.SaveChangesAsync();
        }
    }
}