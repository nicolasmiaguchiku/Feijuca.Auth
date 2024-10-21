using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Interfaces;
using MongoDB.Driver;

namespace Feijuca.Auth.Infra.Data.Repositories
{
    public class ConfigRepository(IMongoDatabase mongoDb) : IConfigRepository
    {
        private readonly IMongoCollection<KeycloakSettingsEntity> _collection = mongoDb.GetCollection<KeycloakSettingsEntity>("Configs");

        public async Task<bool> AddConfigAsync(KeycloakSettingsEntity newConfig)
        {
            await _collection.InsertOneAsync(newConfig);

            return true;
        }

        public KeycloakSettings GetConfig()
        {
            var config = _collection.Find(Builders<KeycloakSettingsEntity>.Filter.Empty).FirstOrDefault();

            return config;
        }
    }
}
