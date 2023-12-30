using API.Context;
using API.Entities;
using API.Interface;

namespace API.Repositories;

public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepository(MyContext context) : base(context)
    {
    }
}