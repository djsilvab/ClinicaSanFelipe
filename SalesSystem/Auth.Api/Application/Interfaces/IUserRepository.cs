using Auth.Api.Domain.Entities;

namespace Auth.Api.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUserNameAsync(string userName);
}
