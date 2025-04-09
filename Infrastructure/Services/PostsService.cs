using System.Net;
using Domain.DTOs;
using Domain.DTOs.PostDTOs;
using Domain.DTOs.UserDTOs;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class PostsService(DataContext context) : IPostsService
{
    public async Task<Response<List<GetPostDTO>>> GetAllAsync()
    {
        var posts = await context.Posts.ToListAsync();

        var info = posts.Select(u => new GetPostDTO(){
                Id = u.Id,
                UserId = u.UserId,
                Content = u.Content,
                CreatedAt = u.CreatedAt
            }).ToList();

        return new Response<List<GetPostDTO>>(info);
    }

    public async Task<Response<GetPostDTO>> GetByIdAsync(int ID)
    {
        var info = await context.Posts.FindAsync(ID);
        if (info == null)
        {
            return new Response<GetPostDTO>(HttpStatusCode.NotFound, "Not found");
        }

        var post = new GetPostDTO(){
            Id = info.Id,
            UserId = info.UserId,
            Content = info.Content,
            CreatedAt = info.CreatedAt
        };

        return new Response<GetPostDTO>(post);
    }

    public async Task<Response<string>> DeleteAsync(int ID)
    {
        var info = await context.Posts.FindAsync(ID);
        if (info == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Not found");
        }

        context.Remove(info);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Error, not deleted")
            : new Response<string>("Deleted successefully");
    }

    public async Task<Response<GetPostDTO>> CreateAsync(CreatePostDTO createPost)
    {
        var post = new Post()
        {
            UserId = createPost.UserId,
            Content = createPost.Content,
            CreatedAt = createPost.CreatedAt
        };

        await context.Posts.AddAsync(post);
        var result = await context.SaveChangesAsync();

        var getPostDto = new GetPostDTO()
        {
            Id = post.Id,
            UserId = post.UserId,
            Content = post.Content,
            CreatedAt = post.CreatedAt
        };

        return result == 0
            ? new Response<GetPostDTO>(HttpStatusCode.InternalServerError, "Error, not created")
            : new Response<GetPostDTO>(getPostDto);
    }

    public async Task<Response<GetPostDTO>> UpdateAsync(int ID, CreatePostDTO updatePost)
    {
        var info = await context.Posts.FindAsync(ID);
        if (info == null)
        {
            return new Response<GetPostDTO>(HttpStatusCode.NotFound, "Not found");
        }

        info.UserId = updatePost.UserId;
        info.Content = updatePost.Content;
        info.CreatedAt = updatePost.CreatedAt;

        var result = await context.SaveChangesAsync();

        var getPostDto = new GetPostDTO()
        {
            Id = info.Id,
            UserId = info.UserId,
            Content = info.Content,
            CreatedAt = info.CreatedAt
        };

        return result == 0
            ? new Response<GetPostDTO>(HttpStatusCode.InternalServerError, "Error, not created")
            : new Response<GetPostDTO>(getPostDto);
    }

    public async Task<Response<List<LatestPostsDto>>> GetLatestPostsAsync()
    {
        var posts = await context.Posts
            .Include(p => p.User)
            .OrderByDescending(p => p.CreatedAt)
            .Take(5)
            .Select(p => new LatestPostsDto
            {
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                Username = p.User.Username
            })
            .ToListAsync();

        return new Response<List<LatestPostsDto>>(posts);
    }

    public async Task<Response<List<UserPostsDto>>> GetAllUserPostsAsync(int UserId)
    {
        var user = await context.Users.FindAsync(UserId);
        if (user == null)
        {
            return new Response<List<UserPostsDto>>(HttpStatusCode.NotFound, "User not found");
        }

        var posts = await context.Posts
            .Where(p => p.UserId == UserId)
            .Include(p => p.User)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new UserPostsDto 
            {
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                Username = p.User.Username
            })
            .ToListAsync();

        return new Response<List<UserPostsDto>>(posts);
    }

    public async Task<Response<PostAuthorDto>> FindPosterWithAuthorAsync(int PostId)
    {
        var info = await context.Posts.FindAsync(PostId);
        if (info == null)
        {
            return new Response<PostAuthorDto>(HttpStatusCode.NotFound, "Post not found");
        }

        var post = await context.Posts
            .Where(p => p.Id == PostId)
            .Include(p => p.User)
            .Select(p => new PostAuthorDto
            {
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                Username = p.User.Username
            })
            .FirstOrDefaultAsync();

        return new Response<PostAuthorDto>(post);
    }

    public async Task<Response<List<TopPosterDto>>> TopThreeUsersWithMostPosts()
    {
        var users = await context.Users
            .OrderByDescending(u => u.Posts.Count())
            .Take(3)
            .Select(u => new TopPosterDto
            {
                Username = u.Username,
                Email = u.Email,
                PostCount = u.Posts.Count
            })
            .ToListAsync();

        return new Response<List<TopPosterDto>>(users);
    }

    public async Task<Response<PostCommentsDto>> CommentsWithUsers(int PostId)
    {
        var info = await context.Posts.FindAsync(PostId);
        if (info == null)
        {
            return new Response<PostCommentsDto>(HttpStatusCode.NotFound, "Post not found");
        }

        var post = await context.Posts
            .Where(p => p.Id == PostId)
            .Include(p => p.User)
            .Include(p => p.Comments)
            .ThenInclude(c => c.User)
            .Select(p => new PostCommentsDto
            {
                Content = p.Content,
                Username = p.User.Username,
                Comments = p.Comments
                    .Select(c => new CommentInfo
                    {
                        Text = c.Text,
                        CommentAuthor = c.User.Username
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();
        
        return new Response<PostCommentsDto>(post);
    }

}