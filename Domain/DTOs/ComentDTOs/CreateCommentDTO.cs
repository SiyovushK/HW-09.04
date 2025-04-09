namespace Domain.DTOs.ComentDTOs;

public class CreateCommentDTO
{
    public int UserId { get; set; }
    public int PostId { get; set; }
    public string Text { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}