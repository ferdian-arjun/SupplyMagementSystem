using API.Dtos.Project;
using API.Interface;

namespace API.Services;

public class ProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public IEnumerable<GetProjectDto> Get()
    {
        var projects = _projectRepository.GetAll();
        if (!projects.Any()) return Enumerable.Empty<GetProjectDto>();
        
        List<GetProjectDto> getProjectDtos = new();
        foreach (var project in projects) getProjectDtos.Add((GetProjectDto)project);
        return getProjectDtos;
    }

    public GetProjectDto? CreateProject(CreateProjectDto createProjectDto)
    {
        var createAccountStatus = _projectRepository.Create(createProjectDto);
        if (createAccountStatus is null) return null; 
        return (GetProjectDto)createAccountStatus;
    }

    public int UpdateProject(UpdateProjectDto updateProjectDto)
    {
        var getProject = _projectRepository.GetByGuid(updateProjectDto.Guid);
        if (getProject is null) return -1;

        var isUpdate = _projectRepository.Update(updateProjectDto);
        return isUpdate ? 1 : 0;
    }

    public GetProjectDto? GetByGuid(string guid)
    {
        var getProject = _projectRepository.GetByGuid(guid);
        if (getProject is null) return null;
        return (GetProjectDto)getProject;
    }

    public int DeleteProject(string guid)
    {
        var getProject = _projectRepository.GetByGuid(guid);
        if (getProject is null) return -1;
        var isDelete = _projectRepository.SoftDelete(getProject);
        return isDelete ? 1 : 0;
    }
}