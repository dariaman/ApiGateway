using GlobalUtility.Interface;
using Microsoft.EntityFrameworkCore;

namespace GlobalUtility.Entity
{
    public abstract class RepositoryBase<TModel, TDatabase>(TDatabase tDB) : IRepositoryBase<TModel, TDatabase>, IDisposable
        where TModel : ModelBase
        where TDatabase : DbContext
    {
        public readonly TDatabase _tDB = tDB;

        // CRUD
        public async Task<TModel> AddAsync(TModel Tmodel)
        {
            Tmodel.CreateBy = "Not Yet Set By Session";

            _tDB.Set<TModel>().Add(Tmodel);
            await _tDB.SaveChangesAsync();
            return Tmodel;
        }

        public async Task<TModel> UpdateAsync(TModel Tmodel)
        {
            var existingTModel = await FindByIdAsync(Tmodel.Id) ?? throw new DbUpdateException("No Data Found For Updated");

            existingTModel.LastUpdateDate = DateTime.Now;
            existingTModel.LastUpdateBy = "Not Yet Set By Session";

            await _tDB.SaveChangesAsync();
            return existingTModel;
        }

        public async Task<TModel> DeleteAsync(TModel Tmodel)
        {
            var existingTModel = await FindByIdAsync(Tmodel.Id) ?? throw new DbUpdateException("No Data Found For Deleted");

            existingTModel.IsDelete = true;
            existingTModel.DeletedDate = DateTime.Now;
            existingTModel.DeletedBy = "Not Yet Set By Session";

            await _tDB.SaveChangesAsync();

            return existingTModel;
        }

        ////==== Query
        public async Task<TModel?> FindByIdAsync(Int64 Id)
        {
            return await _tDB.Set<TModel>().FirstOrDefaultAsync(x => x.Id.Equals(Id) && !x.IsDelete);
        }

        public void Dispose()
        {
            _tDB.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
