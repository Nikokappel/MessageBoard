namespace Domain.DTOs;

public class SearchMBoardParametersDto
{
    public string? Username { get; }
    public int? UserId { get; }
    public string? TitleContains { get; }
    public string? BodyContains { get; }

    public SearchMBoardParametersDto(string? username, int? userId, string? titleContains, string? bodyContains)
    {
        Username = username;
        UserId = userId;
        TitleContains = titleContains;
        BodyContains = bodyContains;
    }
}