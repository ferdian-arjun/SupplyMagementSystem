using API.Context;
using API.Entities;
using API.Interface;

namespace API.Repositories;

public class UserRoleRepository : GeneralRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(MyContext context) : base(context)
    {
    }
}