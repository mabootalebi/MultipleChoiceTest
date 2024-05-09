using Contracts.DTOs;
using Contracts.DTOs.User;
using Domains.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
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
                Parameter = MakeFetchDtoInstance(user)                    ,                
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
        

        //public async Task<ResultDto<List<IdentityError>>> UpdateUserInfo()
        //{

        //}
    }
}
