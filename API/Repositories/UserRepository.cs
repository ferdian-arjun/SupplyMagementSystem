using API.Context;
using API.Entities;
using API.Interface;

namespace API.Repositories;

public class UserRepository : GeneralRepository<User>, IUserRepository
{
    public UserRepository(MyContext context) : base(context)
    {
    }
}