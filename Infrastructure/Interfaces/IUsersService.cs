using Domain.DTOs;
using Domain.DTOs.UserDTOs;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IUsersService
{
    Task<Response<GetUserDTO>> CreateUser(CreateUserDTO createUserDTO);
    Task<Response<List<GetUserDTO>>> GetAllAsync();
    Task<Response<GetUserDTO>> GetByIdAsync(int ID);
    Task<Response<string>> DeleteAsync(int ID);
    Task<Response<GetUserDTO>> UpdateAsync(int Id, GetUserDTO updateUserDTO);
}