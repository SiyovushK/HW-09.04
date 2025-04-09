namespace Domain.DTOs;

public class GetUserDTO
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Bio { get; set; }
    public DateTimeOffset JoinDate { get; set; }
}