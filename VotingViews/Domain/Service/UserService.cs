using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Threading.Tasks;
using VotingViews.Domain.IRepository;
using VotingViews.Domain.IService;
using VotingViews.DTOs;
using VotingViews.Model.Entity;

namespace VotingViews.Domain.Service
{
    public class UserService : IUserService
    {
        private readonly IAdminService _admin;
        private readonly IVoterService _voter;
        private readonly IUserRepository _user;
        private readonly IRoleRepository _role;

        public UserService(IAdminService admin, IVoterService voter, IUserRepository user, IRoleRepository role)
        {
            _admin = admin;
            _voter = voter;
            _user = user;
            _role = role;
        }

        public bool Exists(int id)
        {
            return _user.Exists(id);
        }

        public LoggedInUserDto Login(LoginUserDto userDetails)
        {
            var user = _user.FindByEmail(userDetails.Email);

            if (user == null)
                throw new Exception("Invalid Email");

            string hashedPassword = HashPassword(userDetails.Password, user.HashSalt);

            if (hashedPassword != user.PasswordHash)
                throw new Exception("Invalid Password");

            return new LoggedInUserDto
            {
                Id = user.Id,
                Role = _role.GetRole(user.RoleId)
            };
        }

        public RegisteredUserDto Register(RegisterUserDto userDetails)
        {
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string saltString = Convert.ToBase64String(salt);

            string hashedPassword = HashPassword(userDetails.Password, saltString);

            User newUser = new User
            {
                Email = userDetails.Email,
                HashSalt = saltString,
                PasswordHash = hashedPassword,
                Role = _role.GetByName(userDetails.Type)
            };
            var user = _user.CreateUser(newUser);

            if (userDetails.Type.ToLower().Equals("voter"))
            {
                Voter newVoter = new Voter
                {
                    UserId = user.Id,
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    Email = userDetails.Email,
                    Password = userDetails.Password,
                    MiddleName = userDetails.MiddleName,
                    Address = userDetails.Address
                };

                _voter.AddVoter(newVoter);

                var registeredUser = new RegisteredUserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = _role.GetRole(user.RoleId)
                };

                return registeredUser;
            }
            else if (userDetails.Type.ToLower().Equals("admin"))
            {
                Admin newAdmin = new Admin
                {
                    UserId = user.Id,
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    MiddleName = userDetails.MiddleName,
                    Address = userDetails.Address
                };

                _admin.AddAdmin(newAdmin);

                var registeredUser = new RegisteredUserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = _role.GetRole(user.RoleId)
                };

                return registeredUser;
            }
            else
            {
                throw new Exception("Invalid User Type");
            }
        }

        private string HashPassword(string password, string salt)
        {
            byte[] saltByte = Convert.FromBase64String(salt);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltByte,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        protected string GenerateVoterNumber()
        {
            Random random = new Random();

            string alpha = "VN";
            string number = random.Next(1, 10000).ToString("00000");

            string generatedNumber = $"{alpha}{number}";

            return generatedNumber;
        }
    }
}
