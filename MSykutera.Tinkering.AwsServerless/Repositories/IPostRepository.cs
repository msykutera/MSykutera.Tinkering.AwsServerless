namespace MSykutera.Tinkering.AwsServerless.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> GetAsync(int id);

    Task<bool> CreateAsync(T post);

    Task<bool> UpdateAsync(T post);

    Task<bool> DeleteAsync(int id);
}