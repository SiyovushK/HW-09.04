namespace Domain.DTOs;

public class GetCommentDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public string Text { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}