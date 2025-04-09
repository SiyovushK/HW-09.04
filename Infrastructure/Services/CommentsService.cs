using System.Data;
using System.Net;
using Domain.DTOs;
using Domain.DTOs.ComentDTOs;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CommentsService(DataContext context) : ICommentsService
{
    public async Task<Response<List<GetCommentDTO>>> GetAllAsync()
    {
        var comments = await context.Comments.ToListAsync();

        var data = comments
            .Select(c => new GetCommentDTO()
            {
                Id = c.Id,
                UserId = c.UserId,
                PostId = c.PostId,
                Text = c.Text,
                CreatedAt = c.CreatedAt
            }).ToList();

        return new Response<List<GetCommentDTO>>(data);
    }

    public async Task<Response<GetCommentDTO>> GetByIdAsync(int ID)
    {
        var info = await context.Comments.FindAsync(ID);
        if (info == null)
        {
            return new Response<GetCommentDTO>(HttpStatusCode.BadRequest, "Comment not found");
        }

        var comment = new GetCommentDTO()
        {
            Id = info.Id,
            UserId = info.UserId,
            PostId = info.PostId,
            Text = info.Text,
            CreatedAt = info.CreatedAt
        };

        return new Response<GetCommentDTO>(comment);
    }   

    public async Task<Response<string>> DeleteAsync(int ID)
    {
        var info = await context.Comments.FindAsync(ID);
        if (info == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Comment not found");
        }

        context.Remove(info);
        var result = await context.SaveChangesAsync();

        return result == 0 
            ? new Response<string>(HttpStatusCode.BadRequest, "Comment not deleted") 
            : new Response<string>("Comment deleted successfully");
    }

    public async Task<Response<GetCommentDTO>> CreateAsync(CreateCommentDTO createComment)
    {
        var comment = new Comment()
        {
            UserId = createComment.UserId,
            PostId = createComment.PostId,
            Text = createComment.Text,
            CreatedAt = createComment.CreatedAt
        };

        await context.Comments.AddAsync(comment);
        var result = await context.SaveChangesAsync();

        var getCommentDto = new GetCommentDTO()
        {
            Id = comment.Id,
            UserId = createComment.UserId,
            PostId = createComment.PostId,
            Text = createComment.Text,
            CreatedAt = createComment.CreatedAt
        };

        return result == 0
            ? new Response<GetCommentDTO>(HttpStatusCode.InternalServerError, "Comment not created")
            : new Response<GetCommentDTO>(getCommentDto);
    }

    public async Task<Response<GetCommentDTO>> UpdateAsync(int ID, CreateCommentDTO updateComment)
    {
        var info = await context.Comments.FindAsync(ID);
        if (info == null)
        {
            return new Response<GetCommentDTO>(HttpStatusCode.BadRequest, "Comment not found");
        }

        info.UserId = updateComment.UserId;
        info.PostId = updateComment.PostId;
        info.Text = updateComment.Text;
        info.CreatedAt = updateComment.CreatedAt;

        var result = await context.SaveChangesAsync();

        var getCommentDto = new GetCommentDTO()
        {
            Id = info.Id,
            UserId = info.UserId,
            PostId = info.PostId,
            Text = info.Text,
            CreatedAt = info.CreatedAt
        };
        
        return result == 0
            ? new Response<GetCommentDTO>(HttpStatusCode.InternalServerError, "Comment not updated")
            : new Response<GetCommentDTO>(getCommentDto);
    }
}