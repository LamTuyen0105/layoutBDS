﻿using RealEstate.ViewModels.Service.Property;
using RealEstate.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.System.Users
{
    public interface IUserService
    {
        Task<string> Authencate(LoginRequest request);

        Task<string> AuthencateSocial(LoginRequest request);

        Task<bool> Register(RegisterRequest request);

        Task<bool> RegisterSocial(RegisterRequest request);

        Task<UserViewModel> GetAll(string userId);

        Task<UserViewModel> GetById(Guid userId);

        Task<int> Create(string userId, PropertyCreateRequest request);
    }
}
