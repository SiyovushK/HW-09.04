namespace Domain.DTOs.PostDTOs;

public class PostCommentsDto
{
    public string Content { get; set; }
    public string Username { get; set; }
    public List<CommentInfo> Comments { get; set; }
}