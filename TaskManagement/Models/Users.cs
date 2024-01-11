using TaskManagement.Dependancies;

namespace TaskManagement.Models
{
    public class Users:IUsers
    {
        List<UserDto> users_list = new List<UserDto>
        {
            new UserDto { Id = 1,Name="babi",Password="agddbjh",Role="user"},
            new UserDto { Id = 2,Name="thameem",Password="12345678",Role="user"},
            new UserDto { Id = 3,Name="rishal",Password="sfsd",Role="user"},
            new UserDto { Id = 4,Name="shaan",Password="123455678",Role="admin"},
        };

        public List<UserDto> DisplayUsers()
        {
            return users_list;
        }
        public UserDto DisplayUserById(int id)
        {
            var perticular_user = users_list.FirstOrDefault(x=>x.Id == id);
            return perticular_user;
        }
        public void AddUser(UserDto user)
        {
            users_list.Add(user);
        }
        public UserDto ValidateUser(LoginCredentials user)
        {
            var perticular_user = users_list.FirstOrDefault(x => x.Name == user.Name && x.Password == user.Password);
            return perticular_user;
        }
    }
}
