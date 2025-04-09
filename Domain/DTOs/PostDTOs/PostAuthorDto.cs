namespace Domain.DTOs.PostDTOs;

public class PostAuthorDto
{
    public string Content { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string Username { get; set; }
}