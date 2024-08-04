namespace Application.Common;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}