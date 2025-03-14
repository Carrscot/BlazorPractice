using PracticeApp.Models;

namespace PracticeApp.Services
{
    public interface IAppUserService
    {
        Task<List<AppUser>> GetAllAsync();
        Task<AppUser> GetByIdAsync(int id);
        Task<AppUser> CreateAsync(AppUser user);
        Task UpdateAsync(int id, AppUser user);
        Task DeleteAsync(int id);
    }
}