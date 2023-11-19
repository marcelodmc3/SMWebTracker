using Microsoft.Extensions.Configuration;
using SMWebTracker.Data;
using SMWebTracker.Domain.Dtos;
using SMWebTracker.Domain.Entities;
using SMWebTracker.Domain.Interfaces;
using SMWebTracker.Domain.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<User> CreateUser(NewUserModel newUserModel, string loggedUserEmail)
        {
            if (newUserModel != null)
            {
                newUserModel = new NewUserModel()
                {
                    Name = newUserModel.Name?.Trim(),
                    Login = newUserModel.Login?.Trim(),
                    Password = newUserModel.Password?.Trim(),
                    IsAdmin = newUserModel.IsAdmin
                };

                var context = new ValidationContext(newUserModel, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(newUserModel, context, validationResults, true);

                if (!isValid)
                    throw new ValidationException("Campos inválidos");

                else
                {
                    User loggedUser = null;

                    if (!string.IsNullOrWhiteSpace(loggedUserEmail))
                    {
                        loggedUser = await _userRepository.FindByEmailAsyn(loggedUserEmail);
                    }

                    if ((loggedUser == null) || !loggedUser.IsAdmin && newUserModel.IsAdmin)
                        throw new UnauthorizedAccessException();

                    var existingUser = await _userRepository.FindByEmailAsyn(newUserModel.Login.ToLower());

                    if (existingUser == null)
                    {                      
                        var hash = PasswordHasher.GerarHash(newUserModel.Password);

                        User user = new User()
                        {                           
                            Name = newUserModel.Name,
                            Login = newUserModel.Login.ToLower(),
                            Hash = hash[0],
                            Salt = hash[1],
                            Active = true,
                            IsAdmin = newUserModel.IsAdmin
                        };                        

                        _userRepository.AddUser(user);

                        return user;                        
                    }
                }
            }
            else throw new ValidationException($"{nameof(newUserModel)} esta nulo.");

            return null;
        }

        public async Task<TokenModel> Login(LoginModel loginModel)
        {
            var user = await _userRepository.FindActiveByEmailAsync(loginModel.Login);

            if (user != null)
            {
                bool valid = PasswordHasher.IsValid(loginModel.Password, user.Hash, user.Salt);

                if (valid)
                {
                    var PRIVATE_KEY = _configuration.GetValue<string>("PRIVATE_KEY");

                    return TokenGenerator.GenerateUserToken(user, PRIVATE_KEY);
                }
            }

            return null;
        }
    }
}
