using Domain.DTOs;
using Domain.DTOs.PostDTOs;
using Domain.DTOs.UserDTOs;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IPostsService
{
    Task<Response<List<GetPostDTO>>> GetAllAsync();
    Task<Response<GetPostDTO>> GetByIdAsync(int ID);
    Task<Response<string>> DeleteAsync(int ID);
    Task<Response<GetPostDTO>> CreateAsync(CreatePostDTO createPost);
    Task<Response<GetPostDTO>> UpdateAsync(int ID, CreatePostDTO updatePost);
    Task<Response<List<LatestPostsDto>>> GetLatestPostsAsync();
    Task<Response<List<UserPostsDto>>> GetAllUserPostsAsync(int UserId);
    Task<Response<PostAuthorDto>> FindPosterWithAuthorAsync(int PostId);
    Task<Response<List<TopPosterDto>>> TopThreeUsersWithMostPosts();
    Task<Response<PostCommentsDto>> CommentsWithUsers(int PostId);
}