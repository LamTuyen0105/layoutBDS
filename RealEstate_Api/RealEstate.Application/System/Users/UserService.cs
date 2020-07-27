using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RealEstate.Application.Common;
using RealEstate.Data.EF;
using RealEstate.Data.Entity;
using RealEstate.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RealEstate.ViewModels.Service.Property;
using System.Net.Http;
using Newtonsoft.Json;

namespace RealEstate.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _config;
        private readonly RealEstateDbContext _context;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, IStorageService storageService, IConfiguration config, RealEstateDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _storageService = storageService;
            _config = config;
            _context = context;
        }

        public async Task<string> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim("UserID",user.Id.ToString()),
                new Claim(ClaimTypes.GivenName,user.FullName),
                new Claim(ClaimTypes.Role, string.Join(";",roles))
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<int> Create(string userId, PropertyCreateRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var property = new Property()
            {
                Title = request.Title,
                ApartmentNumber = request.ApartmentNumber,
                StreetNames = request.StreetNames,
                WardId = request.WardId,
                Area = request.Area,
                AreaFrom = request.AreaFrom,
                AreaTo = request.AreaTo,
                Length = request.Length,
                Width = request.Width,
                Facade = request.Facade,
                Price = request.Price,
                PriceFrom = request.PriceFrom,
                PriceTo = request.PriceTo,
                Description = request.Description,
                EvaluationStatusId = request.EvaluationStatusId,
                LegalPapersId = request.LegalPapersId,
                TypeOfPropertyId = request.TypeOfPropertyId,
                TypeOfTransactionId = request.TypeOfTransactionId,
                StartDate = DateTime.Now,
                EndDate = request.EndDate,
                NumberOfStoreys = request.NumberOfStoreys,
                NumberOfBedrooms = request.NumberOfBedrooms,
                NumberOfWCs = request.NumberOfWCs,
                HouseDirectionId = request.HouseDirectionId,
                Lat = request.Lat,
                Lng = request.Lng,
                ContactName = request.ContactName,
                EmailContact = request.EmailContact,
                ContactPhone = request.ContactPhone,
                UserID = user.Id,
            };
            if (request.ThumbnailImage != null && request.ThumbnailImage.Count > 0)
            {
                property.PropertyImages = new List<PropertyImage>();
                foreach (var item in request.ThumbnailImage)
                {
                    if (property.PropertyImages.Count == 0)
                    {
                        property.PropertyImages.Add(new PropertyImage()
                        {
                            Caption = "Thumbnail image",
                            DateCreated = DateTime.Now,
                            FileSize = item.Length,
                            LinkName = await this.SaveFile(item),
                            IsDefault = true,
                            SortOrder = 1
                        });
                    }
                    else
                    {
                        property.PropertyImages.Add(new PropertyImage()
                        {
                            Caption = "Thumbnail image",
                            DateCreated = DateTime.Now,
                            FileSize = item.Length,
                            LinkName = await this.SaveFile(item),
                            IsDefault = false,
                            SortOrder = 2
                        });
                    }
                }
            }
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
            return property.Id;
        }

        public async Task<UserViewModel> GetAll( string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userViewModel = new UserViewModel() 
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.UserName,
                Gender = user.Gender,
                IdentityNumber = user.IdentityNumber
                
            };
            return userViewModel;
        }

        public async Task<UserViewModel> GetById(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.UserName,
                Gender = user.Gender,
                IdentityNumber = user.IdentityNumber
            };
            return userViewModel;
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var user = new AppUser()
            {
                FullName = request.FullName,
                Gender = request.Gender,
                WardId = request.WardId,
                IdentityNumber = request.IdentityNumber,
                UserName = request.UserName,
               
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            var IsValid_capcha = await validateCaptchaAsync(request.checkReCaptCha ?? "");
            if (!IsValid_capcha)
            {
                return false;
            }
            else {
                if (result.Succeeded)
                {
                    return true;
                }
                return false; 
            }
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
        private async Task<bool> validateCaptchaAsync(string captchares)
        {
            object jres = new object();
            if (String.IsNullOrEmpty(captchares))
            {
                return false;
            }
            string secret_key = "6LcoSAEVAAAAAKkUicWjcqefDkcoKuEOz-FMFGv9";
            if (string.IsNullOrEmpty(secret_key))
            {                
                return false;
            }
            var content = new FormUrlEncodedContent(new[]
              {
                new KeyValuePair<string, string>("secret",  secret_key),
                new KeyValuePair<string, string>("response", captchares)
              });
            HttpClient client = new HttpClient();
            var res = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
            captchaResult captchaRes = JsonConvert.DeserializeObject<captchaResult>(res.Content.ReadAsStringAsync().Result);

            if (!captchaRes.success)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> RegisterSocial(RegisterRequest request)
        {
            var user = new AppUser()
            {
                FullName = request.FullName,
                UserName = request.UserName,
                Provider = request.Provider,
                SocialId = request.SocialId
            };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<string> AuthencateSocial(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return null;
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim("UserID",user.Id.ToString()),
                new Claim(ClaimTypes.GivenName,user.FullName),
                new Claim(ClaimTypes.Role, string.Join(";",roles))
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}