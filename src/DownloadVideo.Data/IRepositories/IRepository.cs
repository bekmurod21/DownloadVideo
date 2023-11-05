using DownloadVideo.Domain.Commons;
using System.Linq.Expressions;

namespace DownloadVideo.Data.IRepositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null);
    ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression);
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);

}
