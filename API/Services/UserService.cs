using System.Transactions;
using API.Dtos;
using API.Dtos.User;
using API.Entities;
using API.Interface;

namespace API.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
    }

    public IEnumerable<GetUserDto> Get()
    {
        var users = _userRepository.Get(includes: user => user.TblTrUserRoles);
        if (!users.Any()) return Enumerable.Empty<GetUserDto>();
        
        List<GetUserDto> getUserDtos = new();
        foreach (var user in users) getUserDtos.Add((GetUserDto)user);
        return getUserDtos;
    }

    public GetUserDto? CreateUser(CreateUserDto createUserDto)
    {
        var createUser = _userRepository.Create(createUserDto);
        if (createUser is null) return null; 
        return (GetUserDto)createUser;
    }

    public int UpdateUser(UpdateUserDto updateUserDto)
    {
        var getUser = _userRepository.GetByGuid(updateUserDto.Guid);
        if (getUser is null) return -1;
        
        //update
        getUser.Username = updateUserDto.Username;
        getUser.Email = updateUserDto.Email;
        getUser.FullName = updateUserDto.FullName;
        getUser.UpdatedAt = DateTime.Now;
        
        var isUpdate = _userRepository.Update(getUser);
        return isUpdate ? 1 : 0;
    }

    public GetUserDto? GetByGuid(string guid)
    {
        var getUser = _userRepository.GetByGuid(guid);
        if (getUser is null) return null;
        return (GetUserDto)getUser;
    }

    public int DeleteUser(string guid)
    {
        var getUser = _userRepository.GetByGuid(guid);
        if (getUser is null) return -1;
        var isDelete = _userRepository.SoftDelete(getUser);
        return isDelete ? 1 : 0;
    }
}