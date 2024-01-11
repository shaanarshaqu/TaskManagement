using TaskManagement.Dependancies;

namespace TaskManagement.Models
{
    public interface IUsers
    {
        List<UserDto> DisplayUsers();
        UserDto DisplayUserById(int id);
        void AddUser(UserDto user);
        UserDto ValidateUser(LoginCredentials user);

    }
}
