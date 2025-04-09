namespace Domain.DTOs.UserDTOs;

public class HighInteractionUserDto
{
    public string Username { get; set; }
    public int PostCount { get; set; }
    public double AvgCommentsPerPost { get; set; }
}