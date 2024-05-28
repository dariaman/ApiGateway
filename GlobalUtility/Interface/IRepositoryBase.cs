using GlobalUtility.Entity;
using Microsoft.EntityFrameworkCore;

namespace GlobalUtility.Interface
{
    public interface IRepositoryBase<TModel, TDatabase>
        where TModel : ModelBase
        where TDatabase : DbContext
    {
        // CRUD
        Task<TModel> AddAsync(TModel Tmodel);
        Task<TModel> UpdateAsync(TModel Tmodel);
        Task<TModel> DeleteAsync(TModel Tmodel);

        // Query
        Task<TModel?> FindByIdAsync(Int64 Id);
    }
}
