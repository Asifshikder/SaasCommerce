namespace Project.Core.Persistence;
public interface IConnectionStringValidator
{
    bool TryValidate(string connectionString);
}
