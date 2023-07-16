using Innoloft.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using static Innoloft.Services.EventService;
using System.Security.Principal;
using Innoloft.Services.Abstractions;
using Innoloft.Repositories.Abstractions;
using Innoloft.DTOs;
using Innoloft.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Innoloft.DTOs.Responses;
using Innoloft.DTOs.Requests;
using Innoloft.Configurations;

namespace Innoloft.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UserService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }



        public async Task<IEnumerable<UserDtoResponse>> GetAllAsync(PaginationFilter filter)
        {
            IEnumerable<User> users = await _repositoryManager.UserRepository.GetAllAsync(filter);

            IEnumerable<UserDtoResponse> userDtoResponse = _mapper.Map<IEnumerable<User>, IEnumerable<UserDtoResponse>>(users);
            return userDtoResponse;
        }

        public async Task<UserDtoResponse> GetByIdAsync(string userId)
        {
            User user = await _repositoryManager.UserRepository.GetByIdAsync(userId);
            if (user is null) return null;
            UserDtoResponse userDtoResponse = _mapper.Map<User, UserDtoResponse>(user);
            return userDtoResponse;
        }

        public async Task<UserDtoResponse> CreateAsync(UserDtoRequest userDtoRequest)
        {

            User user = _mapper.Map<UserDtoRequest, User>(userDtoRequest);
            user = await _repositoryManager.UserRepository.InsertAsync(user);
            UserDtoResponse userDtoResponse = _mapper.Map<User, UserDtoResponse>(user);
            return userDtoResponse;
        }
        public async Task UpdateAsync(string userId, UserDtoRequest userDtoRequest)
        {
            var user = await _repositoryManager.UserRepository.GetByIdAsync(userId);
            if (user is null)
            {
                throw new Exception();
            }
            await _repositoryManager.UserRepository.UpdateAsync(userDtoRequest);
        }
        public async Task DeleteAsync(string userId)
        {
            User user = await _repositoryManager.UserRepository.GetByIdAsync(userId);
            if (user is null)
            {
                throw new Exception();
            }
            await _repositoryManager.UserRepository.RemoveAsync(user);

        }






    }
}






