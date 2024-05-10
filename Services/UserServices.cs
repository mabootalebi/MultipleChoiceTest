using Contracts.DTOs;
using Contracts.DTOs.User;
using Domains.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System.Text.Json;

namespace Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthenticationServices> _logger;

        public UserServices(UserManager<User> userManager,
                                      RoleManager<IdentityRole> roleManager,
                                      ILogger<AuthenticationServices> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        public ResultDto<List<FetchDto>> FetchList()
        {
            return new ResultDto<List<FetchDto>>
            {
                Status = CustomStatuses.Success,
                Parameter =

                [
                    .. _userManager.Users.Select(t => new FetchDto
                    {
                        Id = t.Id,
                        Username = t.UserName!,
                        Address = t.Address,
                        Email = t.Email,
                        FatherName = t.FatherName,
                        FirstName = t.FirstName,
                        LastName = t.LastName,
                        NationalCode = t.NationalCode,
                        PhoneNumber = t.PhoneNumber
                    }),
                ]
            };
        }

        public async Task<ResultDto<FetchDto>> FetchByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return new ResultDto<FetchDto>
                {
                    Status = CustomStatuses.UserWithGivenIdNotFound,
                };

            return new ResultDto<FetchDto>
            {
                Status = CustomStatuses.Success,
                Parameter = MakeFetchDtoInstance(user),
            };
        }

        private FetchDto MakeFetchDtoInstance(User user) =>
            new FetchDto
            {
                Id = user?.Id,
                Username = user?.UserName!,
                Address = user?.Address,
                Email = user?.Email,
                FatherName = user?.FatherName,
                FirstName = user?.FirstName,
                LastName = user?.LastName,
                NationalCode = user?.NationalCode,
                PhoneNumber = user?.PhoneNumber
            };


        public async Task<ResultDto<List<IdentityError>>> UpdateUserInfoAsync(string id, UpdateDto updateDto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return new ResultDto<List<IdentityError>>
                {
                    Status = CustomStatuses.UserWithGivenIdNotFound,
                };

            user.FirstName = updateDto.FirstName;
            user.LastName = updateDto.LastName;
            user.FatherName = updateDto.FatherName;
            user.NationalCode = updateDto.NationalCode;
            user.PhoneNumber = updateDto.PhoneNumber;
            user.Address = updateDto.Address;
            user.Email = updateDto.Email;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var model = new
                {
                    id,
                    updateDto
                };

                var serializedInput = JsonSerializer.Serialize(model);
                var errors = string.Join(" / ", result.Errors.Select(t => $"code: {t.Code}, description: {t.Description}"));
                _logger.LogError($"Error Occured while Updating user Info. Entered Data: {serializedInput}. ErrorsList: {errors}");

                return new ResultDto<List<IdentityError>>
                {
                    Status = CustomStatuses.ErrorOccuredWhileUpdatingUser,
                    Parameter = result.Errors.ToList()
                };
            }

            return new ResultDto<List<IdentityError>>
            {
                Status = CustomStatuses.Success,
            };
        }

    }
}
