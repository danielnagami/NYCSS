namespace NYCSS.Utils.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}