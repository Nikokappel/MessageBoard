namespace Domain.DTOs;

public class MBoardCreationDto
{
    public int OwnerId { get; }
    public string Title { get; }
    
    public string Body { get; }
    
    public string Time { get; }
    

    public MBoardCreationDto(int ownerId, string title, string body, string time)
    {
        OwnerId = ownerId;
        Title = title;
        Body = body;
        Time = time;
    }
}