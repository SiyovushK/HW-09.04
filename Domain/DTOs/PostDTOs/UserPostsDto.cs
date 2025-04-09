namespace Domain.DTOs.UserDTOs;

public class UserPostsDto
{
    public string Content { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string Username { get; set; }
}