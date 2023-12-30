using API.Context;
using API.Entities;
using API.Interface;

namespace API.Repositories;

public class ProjectVendorRepository : GeneralRepository<ProjectVendor>, IProjectVendorRepository   
{
    public ProjectVendorRepository(MyContext context) : base(context)
    {
    }
}