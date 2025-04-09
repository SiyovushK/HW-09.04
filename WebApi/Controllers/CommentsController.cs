using Domain.DTOs;
using Domain.DTOs.ComentDTOs;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController(ICommentsService commentsService) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetCommentDTO>>> GetAllAsync()
    {
        return await commentsService.GetAllAsync();
    }
    
    [HttpGet("id")]
    public async Task<Response<GetCommentDTO>> GetByIdAsync(int ID)
    {
        return await commentsService.GetByIdAsync(ID);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int ID)
    {
        return await commentsService.DeleteAsync(ID);
    }
    
    [HttpPost]
    public async Task<Response<GetCommentDTO>> CreateAsync(CreateCommentDTO createComment)
    {
        return await commentsService.CreateAsync(createComment);
    }
    
    [HttpPut]
    public async Task<Response<GetCommentDTO>> UpdateAsync(int ID, CreateCommentDTO updateComment)
    {
        return await commentsService.UpdateAsync(ID, updateComment);
    }
}