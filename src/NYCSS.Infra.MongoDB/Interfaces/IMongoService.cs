using NYCSS.Domain.Entities;
using NYCSS.Infra.MongoDB.Models;

namespace NYCSS.Infra.MongoDB.Interfaces
{
    public interface IMongoService
    {
        Task<Subway> GetSubwayAsync(Guid id);
        Task<Subway> GetSubwayAsync(string name);
        Task<IEnumerable<Subway>> GetSubwaysAsync();
        Task InsertSubway(Subway subway);
        Task InsertUserFrequency(UserFrequency user);
        Task<UserFrequency> GetUserFrequencyAsync(string username);
        Task IncreaseUserFrequency(Subway subway, string username);
        Task DecreaseUserFrequency(Subway subway, string username);
    }
}
