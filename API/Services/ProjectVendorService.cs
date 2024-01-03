using System.Transactions;
using API.Dtos.ProjectVendor;
using API.Interface;

namespace API.Services;

public class ProjectVendorService
{
    public readonly IProjectVendorRepository _projectVendorRepository;
    public readonly IProjectRepository _projectRepository;
    public readonly IVendorRepository _vendorRepository;

    public ProjectVendorService(IProjectVendorRepository projectVendorRepository, IProjectRepository projectRepository, IVendorRepository vendorRepository)
    {
        _projectVendorRepository = projectVendorRepository;
        _projectRepository = projectRepository;
        _vendorRepository = vendorRepository;
    }
    
    public IEnumerable<GetProjectVendorDto> Get()
    {
        var projectVendors = _projectVendorRepository.GetAll();
        if (!projectVendors.Any()) return Enumerable.Empty<GetProjectVendorDto>();
        
        List<GetProjectVendorDto> getProjectVendorDtos = new();
        foreach (var projectVendor in projectVendors) getProjectVendorDtos.Add((GetProjectVendorDto)projectVendor);
        return getProjectVendorDtos;
    }

    public GetProjectVendorDto? CreateProjectVendor(CreateProjectVendorDto createProjectVendorDto)
    {
        using var scope = new TransactionScope();
        var checkProjectVendor = _projectVendorRepository.Get(
            where: pv => pv.ProjectGuid == createProjectVendorDto.ProjectGuid).LastOrDefault();

        if (checkProjectVendor != null)
        {
            var deleteOldData = _projectVendorRepository.HardDelete(checkProjectVendor);
            if (!deleteOldData) return null;
        }
        
        var createProjectVendor = _projectVendorRepository.Create(createProjectVendorDto);
        if (createProjectVendor is null) return null; 
        
        scope.Complete();
        
        return (GetProjectVendorDto)createProjectVendor;
       
    }

    public int UpdateProjectVendor(UpdateProjectVendorDto updateProjectVendorDto)
    {
        var getProjectVendor = _projectVendorRepository.GetAll()
            .FirstOrDefault(projectVendor => 
                projectVendor.ProjectGuid.Equals(updateProjectVendorDto.ProjectGuid) && 
                projectVendor.VendorGuid.Equals(updateProjectVendorDto.VendorGuid)
            );
        
        if (getProjectVendor is null) return -1;

        var isUpdate = _projectVendorRepository.Update(updateProjectVendorDto);
        return isUpdate ? 1 : 0;
    }

    public IEnumerable<GetProjectByVendorDto> GetProjectByVendor(string vendorGuid)
    {
        var projectVendors = _projectVendorRepository.GetAll().Where(projectVendor => projectVendor.VendorGuid.Equals(vendorGuid));
        if (!projectVendors.Any()) return Enumerable.Empty<GetProjectByVendorDto>();
        
        
        List<GetProjectByVendorDto> getProjectVendorDtos = new();
        foreach (var projectVendor in projectVendors)
        {
            var project = _projectRepository.GetByGuid(projectVendor.ProjectGuid);
            if (project is null) continue;

            var vendor = _vendorRepository.Get(where: vendor => vendor.Guid.Equals(projectVendor.VendorGuid),includes: vendor => vendor.Company).FirstOrDefault();
            if (vendor is null) continue;
            
         
            getProjectVendorDtos.Add(new GetProjectByVendorDto()
            {
                ProjectGuid = project.Guid,
                ProjectName = project.Name,
                ProjectStatus = project.Status.ToString(),
                ProjectDescription = project.Description,
                ProjectEndDate = project.StartDate,
                ProjectStartDate = project.StartDate,
                
                VendorGuid = vendor.Guid,
                VendorName = vendor.Company.Name,
                VendorEmail = vendor.Company.Email,
                VendorBusinessType = vendor.Company.BusinessType,
                VendorImage = vendor.Company.Image,
                VendorTelp = vendor.Company.Telp,
                VendorType = vendor.Company.Type,
                
                CreatedAt = projectVendor.CreatedAt,
                UpdatedAt = projectVendor.UpdatedAt,
                
            });
        }
        
        return getProjectVendorDtos;
    }
    
    public IEnumerable<GetVendorByProjectDto> GetVendorByProject(string projectGuid)
    {
        var projectVendors = _projectVendorRepository.GetAll().Where(projectVendor => projectVendor.ProjectGuid.Equals(projectGuid));
        if (!projectVendors.Any()) return Enumerable.Empty<GetVendorByProjectDto>();
        
        
        List<GetVendorByProjectDto> getProjectVendorDtos = new();
        foreach (var projectVendor in projectVendors)
        {
            var project = _projectRepository.GetByGuid(projectVendor.ProjectGuid);
            if (project is null) continue;

            var vendor = _vendorRepository.Get(where: vendor => vendor.Guid.Equals(projectVendor.VendorGuid),includes: vendor => vendor.Company).FirstOrDefault();
            if (vendor is null) continue;
            
         
            getProjectVendorDtos.Add(new GetVendorByProjectDto()
            {
                ProjectGuid = project.Guid,
                ProjectName = project.Name,
                ProjectStatus = project.Status.ToString(),
                ProjectDescription = project.Description,
                ProjectEndDate = project.StartDate,
                ProjectStartDate = project.StartDate,
                
                VendorGuid = vendor.Guid,
                VendorName = vendor.Company.Name,
                VendorEmail = vendor.Company.Email,
                VendorBusinessType = vendor.Company.BusinessType,
                VendorImage = vendor.Company.Image,
                VendorTelp = vendor.Company.Telp,
                VendorType = vendor.Company.Type,
                
                CreatedAt = projectVendor.CreatedAt,
                UpdatedAt = projectVendor.UpdatedAt,
                
            });
        }
        
        return getProjectVendorDtos;
    }

    public int DeleteProjectVendor(string vendorGuid, string projectGuid)
    {
        var getProjectVendor = _projectVendorRepository.Get(where: projectVendor => projectVendor.ProjectGuid.Equals(projectGuid) && projectVendor.VendorGuid.Equals(vendorGuid)).FirstOrDefault();
        if (getProjectVendor is null) return -1;
        var isDelete = _projectVendorRepository.HardDelete(getProjectVendor);
        return isDelete ? 1 : 0;
    }
}