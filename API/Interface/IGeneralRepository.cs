using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace API.Interface;

public interface IGeneralRepository<TEntity>
{
    TEntity? Create(TEntity entity);
    bool Update(TEntity entity);
    bool HardDelete(TEntity entity);
    bool SoftDelete(TEntity entity);
    IEnumerable<TEntity> GetAll();
    TEntity? GetByGuid(string guid);
    IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? where = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes);
}