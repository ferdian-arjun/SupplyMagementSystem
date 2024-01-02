using System.Net;
using System.Text;
using API.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SupplyManagementSystem.Configurations;
using SupplyManagementSystem.Interfaces;

namespace SupplyManagementSystem.Repositories;

public class GeneralRepository<TEntity, TKey> : IGeneralRepository<TEntity, TKey> where TEntity : class
{
    private readonly BaseUrls _baseUrls;
        private readonly string request;
        private readonly HttpClient httpClient;

        public GeneralRepository(string request, IOptions<BaseUrls> connectionConfig)
        {
            this.request = request;
            _baseUrls = connectionConfig.Value;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrls.BaseUrlApis)
            };
        }

        public async Task<List<TEntity>> GetAll()
        {
            List<TEntity> entities;

            using var response = await httpClient.GetAsync(request);
            var apiResponse = await response.Content.ReadAsStringAsync();
            entities = JsonConvert.DeserializeObject<List<TEntity>>(apiResponse);
            return entities;
        }

        public async Task<TEntity> Get(TKey key)
        {
            TEntity entity = null;

            using var response = await httpClient.GetAsync(request + key);
            var apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<TEntity>(apiResponse);
            return entity;
        }

        public string Post(TEntity entity)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            return httpClient.PostAsync(request, content).Result.Content.ReadAsStringAsync().Result;
        }

        public string Put(TEntity entity)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            return httpClient.PutAsync(request, content).Result.Content.ReadAsStringAsync().Result;
        }

        public HttpStatusCode Delete(TKey key)
        {
            var result = httpClient.DeleteAsync(request + key).Result;
            return result.StatusCode;
        }
}