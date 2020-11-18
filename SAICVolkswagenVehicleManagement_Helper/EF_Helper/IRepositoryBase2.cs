using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public interface IRepositoryBase2<T,TId>
    {
        Task<bool> IsExistAsync(TId id);
        Task<T> GetFirstInfo(TId id);
    }
}
