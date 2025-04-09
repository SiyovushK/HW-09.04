using Domain.DTOs;
using Domain.DTOs.UserDTOs;
using Domain.Responses;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUsersService usersService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetUserDTO>> CreateAsync(CreateUserDTO createUserDTO)
    {
        var result = await usersService.CreateUser(createUserDTO);
        return result;
    }

    [HttpGet]
    public async Task<Response<List<GetUserDTO>>> GetAllAsync()
    {
        return await usersService.GetAllAsync();
    }

    [HttpGet("id")]
    public async Task<Response<GetUserDTO>> GetByIdAsync(int ID)
    {
        return await usersService.GetByIdAsync(ID);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int ID)
    {
        return await usersService.DeleteAsync(ID);
    }
    
    [HttpPut]
    public async Task<Response<GetUserDTO>> UpdateAsync(int Id, GetUserDTO updateUserDTO)
    {
        return await usersService.UpdateAsync(Id, updateUserDTO);
    }
}