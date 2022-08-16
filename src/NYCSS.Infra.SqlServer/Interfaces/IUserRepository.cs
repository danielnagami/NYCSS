using NYCSS.Domain.Entities;
using NYCSS.Utils.Data;

namespace NYCSS.Infra.SqlServer.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        void Add(User user);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByUsername(string username);
        Task<User> GetById(Guid id);
    }
}