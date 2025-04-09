using Domain.DTOs;
using Domain.DTOs.PostDTOs;
using Domain.DTOs.UserDTOs;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController(IPostsService postService) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetPostDTO>>> GetAllAsync()
    {
        return await postService.GetAllAsync();
    }
    
    [HttpGet("id")]
    public async Task<Response<GetPostDTO>> GetByIdAsync(int ID)
    {
        return await postService.GetByIdAsync(ID);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int ID)
    {
        return await postService.DeleteAsync(ID);
    }
    
    [HttpPost]
    public async Task<Response<GetPostDTO>> CreateAsync(CreatePostDTO createPost)
    {
        return await postService.CreateAsync(createPost);
    }
    
    [HttpPut]
    public async Task<Response<GetPostDTO>> UpdateAsync(int ID, CreatePostDTO updatePost)
    {
        return await postService.UpdateAsync(ID, updatePost);
    }
    
    [HttpGet("latest")]
    public async Task<Response<List<LatestPostsDto>>> GetLatestPostsAsync()
    {
        return await postService.GetLatestPostsAsync();
    }
    
    [HttpGet("all-posts")]
    public async Task<Response<List<UserPostsDto>>> GetAllUserPostsAsync(int UserId)
    {
        return await postService.GetAllUserPostsAsync(UserId);
    }
    
    [HttpGet("with-author")]
    public async Task<Response<PostAuthorDto>> FindPosterWithAuthorAsync(int PostId)
    {
        return await postService.FindPosterWithAuthorAsync(PostId);
    }
    
    [HttpGet("top-posters")]
    public async Task<Response<List<TopPosterDto>>> TopThreeUsersWithMostPosts()
    {
        return await postService.TopThreeUsersWithMostPosts();
    }
    
    [HttpGet("comment-with-users")]
    public async Task<Response<PostCommentsDto>> CommentsWithUsers(int PostId)
    {
        return await postService.CommentsWithUsers(PostId);
    }
    
}