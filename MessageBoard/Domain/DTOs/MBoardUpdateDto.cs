namespace Domain.DTOs;

public class MBoardUpdateDto
{
    public int Id { get; }
    public int? OwnerId { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }

    public MBoardUpdateDto(int id)
    {
        Id = id;
    }
}