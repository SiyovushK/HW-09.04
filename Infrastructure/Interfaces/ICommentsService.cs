using Domain.DTOs;
using Domain.DTOs.ComentDTOs;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICommentsService
{
    Task<Response<List<GetCommentDTO>>> GetAllAsync();
    Task<Response<GetCommentDTO>> GetByIdAsync(int ID);
    Task<Response<string>> DeleteAsync(int ID);
    Task<Response<GetCommentDTO>> CreateAsync(CreateCommentDTO createComment);
    Task<Response<GetCommentDTO>> UpdateAsync(int ID, CreateCommentDTO updateComment);
}