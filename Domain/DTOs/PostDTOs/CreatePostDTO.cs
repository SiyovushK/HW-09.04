namespace Domain.DTOs.PostDTOs;

public class CreatePostDTO
{
    public int UserId { get; set; }
    public string Content { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}