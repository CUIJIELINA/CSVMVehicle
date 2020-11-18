using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public interface IRepositoryBase1<T>
    {
        Task<IEnumerable<T>> GetAllInfoAsync();
        void CreateInfo(T entity);
        void DeleteInfo(T entity);
        void UpdateInfo(T entity);
        Task<bool> SaveAsync();
    }
}
