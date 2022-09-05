namespace CustomAuth;

public class UserData
{
    private readonly Dictionary<string, User> _usersList = new();

    public bool AddUser(User newUser)
    {
       return _usersList.TryAdd(newUser.Name, newUser);
    }

    public User? GetUserById(int id)
    {
        return _usersList.FirstOrDefault(x => x.Value.Id == id).Value;
    }

    public User LoginUser(string username, string password)
    {
        var result =  _usersList.FirstOrDefault(x => x.Value.Username == username && x.Value.Password == password).Value;
        return result;
    }
}

public record User(string Name, string Email, int Id, string Username, string Password);