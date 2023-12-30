using API.Context;
using API.Entities;
using API.Interface;

namespace API.Repositories;

public class ProjectRepository : GeneralRepository<Project>, IProjectRepository
{
    public ProjectRepository(MyContext context) : base(context)
    {
    }
}