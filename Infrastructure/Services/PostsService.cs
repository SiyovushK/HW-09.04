using System.Net;
using Domain.DTOs;
using Domain.DTOs.PostDTOs;
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

        var info = posts
            .Select(u => new GetPostDTO()
            {
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

        var post = new GetPostDTO()
        {
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
    
}