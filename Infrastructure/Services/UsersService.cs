using System.Net;
using Domain.DTOs;
using Domain.DTOs.UserDTOs;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UsersService(DataContext context) : IUsersService
{
    public async Task<Response<GetUserDTO>> CreateUser(CreateUserDTO createUser)
    {
        var user = new User()
        {
            Username = createUser.Username,
            Email = createUser.Email,
            Bio = createUser.Bio
        };

        await context.Users.AddAsync(user);
        var result = await context.SaveChangesAsync();

        var getUserDto = new GetUserDTO()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Bio = user.Bio
        };

        return result == 0
            ? new Response<GetUserDTO>(HttpStatusCode.BadRequest, "Student not created")
            : new Response<GetUserDTO>(getUserDto);
    }

    public async Task<Response<List<GetUserDTO>>> GetAllAsync()
    {
        var users = await context.Users.ToListAsync();

        var data = users
            .Select(u => new GetUserDTO()
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Bio = u.Bio
            }).ToList();

        return new Response<List<GetUserDTO>>(data);
    }

    public async Task<Response<GetUserDTO>> GetByIdAsync(int ID)
    {
        var user = await context.Users.FindAsync(ID);
        if (user == null)
        {
            return new Response<GetUserDTO>(HttpStatusCode.BadRequest, "User not found");
        }

        var getUserDto = new GetUserDTO()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Bio = user.Bio
        };

        return new Response<GetUserDTO>(getUserDto);
    }

    public async Task<Response<string>> DeleteAsync(int ID)
    {
        var user = await context.Users.FindAsync(ID);
        if (user == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "User not found");
        }

        context.Remove(user);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "User not deleted")
            : new Response<string>("User deleted successefully");
    }

    public async Task<Response<GetUserDTO>> UpdateAsync(int Id, GetUserDTO updateUserDTO)
    {
        var user = await context.Users.FindAsync(Id);
        if (user == null)
        {
            return new Response<GetUserDTO>(HttpStatusCode.NotFound, "User not found");
        }

        user.Username = updateUserDTO.Username;
        user.Email = updateUserDTO.Email;
        user.Bio = updateUserDTO.Bio;

        var result = await context.SaveChangesAsync();

        var getUserDto = new GetUserDTO()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Bio = user.Bio
        };

        return result == 0
            ? new Response<GetUserDTO>(HttpStatusCode.BadRequest, "User not updated")
            : new Response<GetUserDTO>(getUserDto);
    }

    public async Task<Response<List<GetUserDTO>>> GetNewRegisters()
    {
        var now = DateTime.Now;
        
        var users = await context.Users
            .Where(u => now.AddDays(-14) <= u.JoinDate).ToListAsync();
        
        var data = users
            .Select(u => new GetUserDTO()
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Bio = u.Bio,
                JoinDate = u.JoinDate
            }).ToList();
        
        return new Response<List<GetUserDTO>>(data);
    }

    public async Task<Response<List<ActivePosterDto>>> ActivePosters()
    {
        var activeUsers = await context.Users
            .Include(u => u.Posts)
            .Where(u => u.Posts.Count > 0).ToListAsync();

        var data = activeUsers
            .Select(u => new ActivePosterDto()
            {
                Username = u.Username,
                PostCount = u.Posts.Count
            }).ToList();
        
        return new Response<List<ActivePosterDto>>(data);
    }


    public async Task<Response<List<RecentlyActiveUserDto>>> RecentlyActive()
    {
        var sevenDaysAgo = DateTime.UtcNow.AddDays(-7);

        var activeUsers = await context.Users
        .Where(u => u.Posts.Any(p => p.CreatedAt >= sevenDaysAgo))
        .Select(u => new RecentlyActiveUserDto
        {
            Username = u.Username,
            LastPostDate = u.Posts
                .Where(p => p.CreatedAt >= sevenDaysAgo)
                .Max(p => p.CreatedAt),
            PostCount = u.Posts
                .Count(p => p.CreatedAt >= sevenDaysAgo)
        })
        .OrderByDescending(u => u.LastPostDate)
        .ToListAsync();

        return new Response<List<RecentlyActiveUserDto>>(activeUsers);
    }

    public async Task<Response<List<TopCreatorDto>>> TopCreatorsAsync()
    {
        var topCreators = await context.Users
            .OrderByDescending(u => u.Posts.Count)
            .Take(5)
            .Select(u => new TopCreatorDto
            {
                Username = u.Username,
                PostCount = u.Posts.Count
            })
            .ToListAsync();

        return new Response<List<TopCreatorDto>>(topCreators);
    }

    public async Task<Response<List<HighInteractionUserDto>>> HighInteraction()
    {
        var users = await context.Users
            .Include(u => u.Posts)
            .ThenInclude(post => post.Comments)
            .Where(u => u.Posts.Count > 0).ToListAsync();
        
        var list = new List<HighInteractionUserDto>();
        
        foreach (var user in users) // {1 => 1, umar;  2 => 3, komron}
        {
            var postCount = user.Posts.Count;
            var commentSum = user.Posts.Sum(post => post.Comments.Count);
            
            var averageCommentsPerPost = commentSum /(double) postCount;

            if (averageCommentsPerPost <= 5) continue;
            
            var highInteractionUserDto = new HighInteractionUserDto()
            {
                PostCount = postCount,
                Username = user.Username,
                AvgCommentsPerPost = averageCommentsPerPost,
            };
                
            list.Add(highInteractionUserDto);
        }
        
        return new Response<List<HighInteractionUserDto>>(list);
    }
    
}