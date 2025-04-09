namespace Domain.DTOs.PostDTOs;

public class LatestPostsDto
{
    public string Content { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string Username { get; set; }
}