using Auth.Api.Application.Interfaces;
using Auth.Api.Domain.Entities;
using Auth.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Auth.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext _context;

    public UserRepository(AuthDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
    }
}
