namespace MoviesApplication.Data.Base
{
    public interface IEntityBaseRepository<T>where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<T>> PaginateAsync(int count);
    }
}
