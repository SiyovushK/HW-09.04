using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Username { get; set; }

    [Required, MaxLength(100)]
    public string Email { get; set; }

    [MaxLength(200)]
    public string Bio { get; set; }

    public DateTimeOffset JoinDate { get; set; }

    public List<Post> Posts { get; set; }
}