using Microsoft.Extensions.Configuration;

using MongoDB.Driver;

using NYCSS.Domain.Entities;
using NYCSS.Infra.MongoDB.Data;
using NYCSS.Infra.MongoDB.Interfaces;
using NYCSS.Infra.MongoDB.Models;

namespace NYCSS.Infra.MongoDB.Services
{
    public class MongoService : IMongoService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Subway> _subwayCollection;
        private readonly IMongoCollection<UserFrequency> _userFrequencyCollection;

        public MongoService(IConfiguration configuration)
        {
            _configuration = configuration;

            var mongoClient = new MongoClient(_configuration.GetConnectionString("MongoDb"));

            var mongoDatabase = mongoClient.GetDatabase("NYCSS");

            _subwayCollection = mongoDatabase.GetCollection<Subway>("Subways");

            SubwaySeed.Seed(_subwayCollection);

            _userFrequencyCollection = mongoDatabase.GetCollection<UserFrequency>("UserFrequency");
        }

        public async Task<Subway> GetSubwayAsync(Guid id) => await _subwayCollection.Find(x => x.ID == id).FirstOrDefaultAsync();
        public async Task<Subway> GetSubwayAsync(string name) => await _subwayCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task<IEnumerable<Subway>> GetSubwaysAsync() => await _subwayCollection.Find(_ => true).ToListAsync();

        public async Task InsertSubway(Subway subway)
        {
            var subwayExists = await GetSubwayAsync(subway.Name);

            if (subwayExists == null)
            {
                await _subwayCollection.InsertOneAsync(subway);
            }
        }

        public async Task InsertUserFrequency(UserFrequency user)
        {
            var userExists = await GetUserFrequencyAsync(user.Username);

            if (userExists == null)
            {
                await _userFrequencyCollection.InsertOneAsync(user);
            }
        }

        public async Task<UserFrequency> GetUserFrequencyAsync(string username)
        {
            var uf = await _userFrequencyCollection.FindAsync(x => x.Username.Equals(username));
            return await uf.FirstOrDefaultAsync();
        }

        public async Task IncreaseUserFrequency(Subway subway, string username)
        {
            var user = await GetUserFrequencyAsync(username);

            var subwayId = subway.ID.ToString();

            if (user == null)
                return;

            if (user.SubwayFrequency.ContainsKey(subwayId))
                user.SubwayFrequency[subwayId]++;
            else
                user.SubwayFrequency.Add(subwayId, 1);

            await _userFrequencyCollection.ReplaceOneAsync(x => x.Username == username, user);
        }

        public async Task DecreaseUserFrequency(Subway subway, string username)
        {
            var user = await GetUserFrequencyAsync(username);

            if (user == null)
                return;

            var subwayId = subway.ID.ToString();

            if (user.SubwayFrequency.ContainsKey(subwayId))
            {
                user.SubwayFrequency[subwayId]--;
            }

            await _userFrequencyCollection.ReplaceOneAsync(x => x.Username == username, user);
        }
    }
}