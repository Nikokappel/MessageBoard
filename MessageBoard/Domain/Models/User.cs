using System.Text.Json.Serialization;

namespace Domain.Models;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    [JsonIgnore]
    public ICollection<MessageBoard> MessageBoards { get; set; }

    
}