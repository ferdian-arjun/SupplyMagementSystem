using System.Net;

namespace SupplyManagementSystem.Interfaces;

public interface IGeneralRepository<TEntity, TKey> where TEntity : class
{
    Task<List<TEntity>> GetAll();
    Task<TEntity> Get(TKey key);
    string Post(TEntity entity);
    string Put(TEntity entity);
    HttpStatusCode Delete(TKey key);
}