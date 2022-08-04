using NYCSS.Domain.Entities;
using NYCSS.Utils.Data;

namespace NYCSS.Infra.SqlServer.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        void Adicionar(User cliente);
        Task<IEnumerable<User>> ObterTodos();
        Task<User> ObterPorCpf(string cpf);
    }
}