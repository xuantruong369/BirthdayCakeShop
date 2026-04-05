using DataAccess.Context;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly BirthdayCakeShopDbContext _context;

        public Repository(BirthdayCakeShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            try
            {
                // Detach any existing tracking to avoid conflicts
                var existingEntity = _context.ChangeTracker.Entries<T>()
                    .FirstOrDefault(e => e.Entity == entity);
                
                if (existingEntity != null && existingEntity.State != EntityState.Detached)
                {
                    existingEntity.State = EntityState.Detached;
                }

                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("already being tracked"))
            {
                // If still tracking issue, get the tracked entity and update its values
                _context.ChangeTracker.Clear();
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);

            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }  
}