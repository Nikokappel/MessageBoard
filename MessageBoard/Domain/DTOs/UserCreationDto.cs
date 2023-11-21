namespace Domain.DTOs;

public class UserCreationDto
{
    
    public string FirstName { get; }
    public string LastName { get; }
    public string UserName { get; }
    public string Password { get;  }
    
    public UserCreationDto(string userName, string password, string firstName, string lastName)
    {
        UserName = userName;
        Password = password;
        FirstName = firstName;
        LastName = lastName;

    }
}