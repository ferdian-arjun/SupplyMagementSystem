using API.Dtos.Role;
using API.Interface;

namespace API.Services;

public class RoleService
{
    public readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    
    public IEnumerable<GetRoleDto> Get()
    {
        var roles = _roleRepository.GetAll();
        if (!roles.Any()) return Enumerable.Empty<GetRoleDto>();
        
        List<GetRoleDto> getRoleDtos = new();
        foreach (var role in roles) getRoleDtos.Add((GetRoleDto)role);
        return getRoleDtos;
    }

    public GetRoleDto? CreateRole(CreateRoleDto createRoleDto)
    {
        var createRole = _roleRepository.Create(createRoleDto);
        if (createRole is null) return null; 
        return (GetRoleDto)createRole;
    }

    public int UpdateRole(UpdateRoleDto updateRoleDto)
    {
        var getRole = _roleRepository.GetByGuid(updateRoleDto.guid);
        if (getRole is null) return -1;

        var isUpdate = _roleRepository.Update(updateRoleDto);
        return isUpdate ? 1 : 0;
    }

    public GetRoleDto? GetByGuid(string guid)
    {
        var getRole = _roleRepository.GetByGuid(guid);
        if (getRole is null) return null;
        return (GetRoleDto)getRole;
    }

    public int DeleteRole(string guid)
    {
        var getRole = _roleRepository.GetByGuid(guid);
        if (getRole is null) return -1;
        var isDelete = _roleRepository.SoftDelete(getRole);
        return isDelete ? 1 : 0;
    }
}