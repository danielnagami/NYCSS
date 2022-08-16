using Microsoft.EntityFrameworkCore;

using NYCSS.Domain.Entities;
using NYCSS.Infra.SqlServer.Interfaces;
using NYCSS.Utils.Data;

namespace NYCSS.Infra.SqlServer.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public UserRepository(UsersContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Add(User user)
        {
            _context.Add(user);
        }

        public async Task<IEnumerable<User>> GetAll() => await _context.Users.AsNoTracking().ToListAsync();

        public async Task<User> GetByUsername(string username) => await _context.Users.FirstOrDefaultAsync(c => c.Username == username);

        public async Task<User> GetById(Guid id) => await _context.Users.FirstOrDefaultAsync(c => c.ID == id);
    }
}