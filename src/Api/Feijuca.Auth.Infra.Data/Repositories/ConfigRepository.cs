using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Models;
using MongoDB.Driver;

namespace Feijuca.Auth.Infra.Data.Repositories
{
    public class ConfigRepository(IMongoDatabase mongoDb) : IConfigRepository
    {
        private readonly IMongoCollection<KeycloakSettings> _collection = mongoDb.GetCollection<KeycloakSettings>("Configs");

        public async Task<bool> AddConfigAsync(KeycloakSettings newConfig)
        {
            await _collection.InsertOneAsync(newConfig);

            return true;
        }

        public KeycloakSettings GetConfig()
        {
            var config = _collection.Find(Builders<KeycloakSettings>.Filter.Empty).FirstOrDefault();

            return config;
        }
    }
}
