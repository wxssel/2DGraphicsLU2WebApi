using _2DGraphicsLU2.WebApi.Models;

namespace _2DGraphicsLU2.WebApi.Repositories
{
    public interface IEnvironmentRepository
    {
        Task<IEnumerable<Environment2D>> ReadAsync(string userId);
        Task<Environment2D?> ReadAsync(Guid environmentId, string userId);
        Task<Environment2D?> InsertAsync(Environment2D environment, string userId);
        Task UpdateAsync(Environment2D environment, string userId);
        Task DeleteAsync(Guid environmentId, string userId);
    }
}
