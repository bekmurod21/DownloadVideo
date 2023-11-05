using DownloadVideo.Data.IRepositories;
using DownloadVideo.Domain.Commons;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using DownloadVideo.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DownloadVideo.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly DownloadVideoDbContext dbContext;
    private readonly DbSet<TEntity> dbSet;
    public Repository(DownloadVideoDbContext dbContext)
    {
        this.dbContext = dbContext;
        dbSet = dbContext.Set<TEntity>();
    }

    public async Task<TEntity> InsertAsync(TEntity value)
    {
        var entity = (await dbSet.AddAsync(value)).Entity;
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await dbSet.FirstOrDefaultAsync(expression);
        if (entity is null)
        {
            return false;
        }
        entity.IsDeleted = true;
        await dbContext.SaveChangesAsync();
        return true;
    }
    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null)
    {
        IQueryable<TEntity> query = expression is null ? this.dbSet : this.dbSet.Where(expression);

        return query;
    }

    public async ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression)
    => await this.SelectAll(expression).FirstOrDefaultAsync(t => !t.IsDeleted);


    public async Task<TEntity> UpdateAsync(TEntity value)
    {
        var entity = this.dbSet.Update(value).Entity;
        await dbContext.SaveChangesAsync();
        return entity;
    }
}
