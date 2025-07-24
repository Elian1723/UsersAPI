using Microsoft.EntityFrameworkCore;
using UsersAPI.Data;
using UsersAPI.Models;

namespace UsersAPI.Repository;

public class UserRepository : IUserRepository
{
    private readonly UsersAPIDbContext _context;

    public UserRepository(UsersAPIDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync(int page, int pageSize)
    {
        var users = await _context.Users
            .OrderBy(u => u.FirstName)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return users;
    }

    public async Task<(IEnumerable<User> users, int totalCount)> GetAllWithCountAsync(int page, int pageSize)
    {
        var totalCount = await _context.Users.CountAsync();
        var users = await _context.Users
            .OrderBy(u => u.FirstName)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (users, totalCount);
    }

    public async Task<User?> GetByIdAsync(int id) => await _context.Users.FindAsync(id);

    public async Task<User?> GetByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<IEnumerable<User>> GetActiveUsersAsync(int page, int pageSize)
    {
        var activateUsers = await _context.Users.Where(u => u.IsActive)
                                                .OrderBy(u => u.FirstName)
                                                .Skip(page * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync();

        return activateUsers;
    }

    public async Task<(IEnumerable<User> users, int totalCount)> GetActiveUsersWithCountAsync(int page, int pageSize)
    {
        var totalCount = await _context.Users.Where(u => u.IsActive).CountAsync();
        var activateUsers = await _context.Users.Where(u => u.IsActive)
                                                .OrderBy(u => u.FirstName)
                                                .Skip(page * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync();

        return (activateUsers, totalCount);
    }

    public async Task<User> CreateAsync(User user)
    {
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> EmailExistsAsync(string email) => await _context.Users.AnyAsync(u => u.Email == email);
}
