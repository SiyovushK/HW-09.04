using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }

    [Required, MaxLength(300)]
    public string Text { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public User User { get; set; }
    public Post Post { get; set; }
}