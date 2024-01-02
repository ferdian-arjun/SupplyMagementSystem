using System.Linq.Expressions;
using API.Dtos.Role;
using API.Dtos.UserRole;
using API.Entities;
using API.Interface;

namespace API.Services;

public class UserRoleService
{
    public readonly IUserRoleRepository _userRoleRepository;

    public UserRoleService(IUserRoleRepository userRoleRepository)
    {
        _userRoleRepository = userRoleRepository;
    }

    public IEnumerable<GetUserByRoleDto> GetUserByRole(string roleGuid)
    {
        var userRoles =
            _userRoleRepository.Get(where: userRole => userRole.RoleGuid.Equals(roleGuid),
                includes: new Expression<Func<UserRole, object>>[]
                {
                    userRole => userRole.User!,
                    userRole => userRole.Role!
                });
        if (!userRoles.Any()) return Enumerable.Empty<GetUserByRoleDto>();

        List<GetUserByRoleDto> getUserByRoleDtos = new();
        foreach (var userRole in userRoles)
        {
            getUserByRoleDtos.Add(new GetUserByRoleDto()
            {
                Guid = userRole.Guid,
                UserGuid = userRole.UserGuid,
                RoleGuid = userRole.RoleGuid,
                RoleName = userRole.Role.Name,
                Username = userRole.User.Username,
                FullName = userRole.User.FullName,
                Email = userRole.User.Email,
                CreatedAt = userRole.CreatedAt,
                UpdatedAt = userRole.UpdatedAt
            });
        }

        return getUserByRoleDtos;
    }
    
    public IEnumerable<GetRoleByUserDto> GetRoleByUser(string userGuid)
    {
        var userRoles =
            _userRoleRepository.Get(where: userRole => userRole.UserGuid.Equals(userGuid),
                includes: new Expression<Func<UserRole, object>>[]
                {
                    userRole => userRole.User!,
                    userRole => userRole.Role!
                });
        if (!userRoles.Any()) return Enumerable.Empty<GetRoleByUserDto>();

        List<GetRoleByUserDto> getRoleByUserDtos = new();
        foreach (var userRole in userRoles)
        {
            getRoleByUserDtos.Add(new GetRoleByUserDto()
            {
                Guid = userRole.Guid,
                UserGuid = userRole.UserGuid,
                RoleGuid = userRole.RoleGuid,
                RoleName = userRole.Role.Name,
                Username = userRole.User.Username,
                FullName = userRole.User.FullName,
                Email = userRole.User.Email,
                CreatedAt = userRole.CreatedAt,
                UpdatedAt = userRole.UpdatedAt
            });
        }

        return getRoleByUserDtos;
    }
    
    public GetUserRoleDto? CreateUserRole(CreateUserRoleDto create)
    {
        var createUserRole = _userRoleRepository.Create(create);
        if (createUserRole is null) return null; 
        return (GetUserRoleDto)createUserRole;
    }

    public int RemoveRelationUserRole(RemoveRelationUserRoleDto removeRelationUserRole)
    {
        var userRole = _userRoleRepository
            .GetAll().FirstOrDefault(userRole => userRole.UserGuid.Equals(removeRelationUserRole.UserGuid) &&
                                                 userRole.RoleGuid.Equals(removeRelationUserRole.RoleGuid));
        if (userRole is null) return -1;

        var removeUserRole = _userRoleRepository.HardDelete(userRole);
        return (removeUserRole) ? 1 : 0;
    }
}