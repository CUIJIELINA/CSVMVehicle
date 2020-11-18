using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public class RepositoryBase<T,TId> : IRepositoryBase1<T>,IRepositoryBase2<T,TId> where T : class
    {
        private readonly DbContext dbContext;

        public RepositoryBase(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateInfo(T entity)
        {
            dbContext.Set<T>().Add(entity);
        }

        public void DeleteInfo(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

        public Task<IEnumerable<T>> GetAllInfoAsync()
        {
            return Task.FromResult(dbContext.Set<T>().AsEnumerable());
        }

        public async Task<T> GetFirstInfo(TId id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task<bool> IsExistAsync(TId id)
        {
            return await dbContext.Set<T>().FindAsync(id) != null;
        }

        public async Task<bool> SaveAsync()
        {
            return await dbContext.SaveChangesAsync() > 0;
        }

        public void UpdateInfo(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }
    }
}
