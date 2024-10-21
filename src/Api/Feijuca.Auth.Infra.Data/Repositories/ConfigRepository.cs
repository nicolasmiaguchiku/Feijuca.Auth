using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Models;
using MongoDB.Driver;

namespace Feijuca.Auth.Infra.Data.Repositories
{
    public class ConfigRepository(IMongoDatabase mongoDb) : IConfigRepository
    {
        private readonly IMongoCollection<AuthSettingsEntity> _collection = mongoDb.GetCollection<AuthSettingsEntity>("Configs");

        public async Task<bool> AddConfigAsync(AuthSettingsEntity newConfig)
        {
            await _collection.InsertOneAsync(newConfig);

            return true;
        }

        public AuthSettingsEntity GetConfig()
        {
            var config = _collection.Find(Builders<AuthSettingsEntity>.Filter.Empty).FirstOrDefault();

            return config;
        }
    }
}
