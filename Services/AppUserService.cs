using Microsoft.EntityFrameworkCore;
using PracticeApp.Data;
using PracticeApp.Models;

namespace PracticeApp.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public AppUserService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<AppUser>> GetAllAsync()
        {
            using var db = _factory.CreateDbContext();
            return await db.AppUsers.ToListAsync();
        }

        public async Task<AppUser> GetByIdAsync(int id)
        {
            using var db = _factory.CreateDbContext();
            return await db.AppUsers.FindAsync(id) ?? throw new KeyNotFoundException($"User {id} not found");
        }

        public async Task<AppUser> CreateAsync(AppUser user)
        {
            using var db = _factory.CreateDbContext();
            user.CreatedDate = DateTime.UtcNow; // Store UTC
            user.UpdatedDate = DateTime.UtcNow; // Store UTC
            db.AppUsers.Add(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(int id, AppUser updatedUser)
        {
            if (id != updatedUser.Id) throw new ArgumentException("ID mismatch");
            using var db = _factory.CreateDbContext();
            var user = await db.AppUsers.FindAsync(id);
            if (user == null) throw new KeyNotFoundException($"User {id} not found");
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.PhoneNo = updatedUser.PhoneNo;
            user.Role = updatedUser.Role;
            user.UpdatedDate = DateTime.UtcNow; // Store UTC
            db.AppUsers.Update(user);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var db = _factory.CreateDbContext();
            var user = await db.AppUsers.FindAsync(id);
            if (user == null) throw new KeyNotFoundException($"User {id} not found");
            db.AppUsers.Remove(user);
            await db.SaveChangesAsync();
        }
    }
}