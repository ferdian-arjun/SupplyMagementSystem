using API.Dtos;
using API.Interface;

namespace API.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<GetUsersDto> Get()
    {
        var users = _userRepository.GetAll();
        if (!users.Any()) return Enumerable.Empty<GetUsersDto>();
        
        List<GetUsersDto> getUsersDtos = new();
        foreach (var user in users) getUsersDtos.Add((GetUsersDto)user);
        return getUsersDtos;
    }

    public GetUsersDto? CreateUser(CreateUserDto createUserDto)
    {
        var createAccountStatus = _userRepository.Create(createUserDto);
        if (createAccountStatus is null) return null; 
        return (GetUsersDto)createAccountStatus;
    }

    public int UpdateUser(UpdateUserDto updateUserDto)
    {
        var getUser = _userRepository.GetByGuid(updateUserDto.Guid);
        if (getUser is null) return -1;

        var isUpdate = _userRepository.Update(updateUserDto);
        return isUpdate ? 1 : 0;
    }

    public GetUsersDto? GetByGuid(string guid)
    {
        var getUser = _userRepository.GetByGuid(guid);
        if (getUser is null) return null;
        return (GetUsersDto)getUser;
    }

    public int DeleteUser(string guid)
    {
        var getUser = _userRepository.GetByGuid(guid);
        if (getUser is null) return -1;
        var isDelete = _userRepository.SoftDelete(getUser);
        return isDelete ? 1 : 0;
    }
}