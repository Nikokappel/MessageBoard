namespace Domain.Models;

public class MessageBoard
{
    public int Id { get; set; }
    public User Owner { get; private set; }
    public int OwnerUserId { get; private set; }
    public string Title { get; private set; }
    public string Body { get; private set; }
    public string Time { get; private set; }
    
    public MessageBoard(int ownerUserId, string title, string body, string time)
    {
        OwnerUserId = ownerUserId;
        Title = title;
        Body = body;
        Time = time;
    }

    private MessageBoard(){}
}