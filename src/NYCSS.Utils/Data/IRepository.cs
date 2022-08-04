using NYCSS.Utils.DomainObjects;

namespace NYCSS.Utils.Data
{
    public interface IRepository<T>: IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}