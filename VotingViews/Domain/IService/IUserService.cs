using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.DTOs;

namespace VotingViews.Domain.IService
{
    public interface IUserService
    {
        public RegisteredUserDto Register(RegisterUserDto userDetails);

        public LoggedInUserDto Login(LoginUserDto userDetails);

        public bool Exists(int id);
    }
}
