using System.Linq.Expressions;
using API.Context;
using API.Entities;
using API.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public abstract class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : class
{
    protected readonly MyContext Context;

    public GeneralRepository(MyContext context)
    {
        Context = context;
    }

    public TEntity? Create(TEntity entity)
    {
        try
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public bool Update(TEntity entity)
    {
        try
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool HardDelete(TEntity entity)
    {
        try
        {
            Context.Set<TEntity>()
                .Remove(entity);
            Context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public bool SoftDelete(TEntity entity)
    {
        try
        {
            if (entity is ISoftDeletable softDeletable)
            {
                softDeletable.DeletedAt = DateTime.Now;
                Context.Set<TEntity>().Update(entity);
            }
            Context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<TEntity> GetAll()
    {
        return Context.Set<TEntity>().ToList()
            .Where(entity =>
                !(entity is ISoftDeletable) ||
                !((ISoftDeletable)entity).DeletedAt.HasValue);
    }

    public TEntity? GetByGuid(string guid)
    {
        var entity = Context.Set<TEntity>().Find(guid);
        Context.ChangeTracker.Clear();
        return entity;
    }
    
    public IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? where = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes
    )
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (typeof(ISoftDeletable).IsAssignableFrom(typeof(TEntity)))
        {
            // Add a filter for soft-deleted entities
            query = query.IgnoreQueryFilters().Where(entity =>
                !(entity is ISoftDeletable) || 
                !((ISoftDeletable)entity).DeletedAt.HasValue);
        }

        if (where != null)
        {
            query = query.Where(where);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return query.ToList();
    }
}