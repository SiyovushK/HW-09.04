using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    [Required, MaxLength(500)]
    public string Content { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public User User { get; set; }
    public List<Comment> Comments { get; set; }
}