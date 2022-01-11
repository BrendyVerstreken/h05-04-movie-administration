using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.Oefening.RateAMovie.Core.Entities;
using Wba.Oefening.RateAMovie.Web.Data;
using Wba.Oefening.RateAMovie.Web.Services.Interfaces;
using Wba.Oefening.RateAMovie.Web.ViewModels;

namespace Wba.Oefening.RateAMovie.Web.Services
{
    public class AccountService : IAccountService
    {
        private readonly MovieContext _movieContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

      
        public AccountService(MovieContext movieContext, IHttpContextAccessor httpContextAccessor)
        {
            //inject moviecontext
            _movieContext = movieContext;
            //inject session manager
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> RegisterUser(AccountRegisterViewModel accountRegisterViewModel)
        {
            User user = new User();
            user.Username = accountRegisterViewModel.Username;
            user.FirstName = accountRegisterViewModel.Firstname;
            user.LastName = accountRegisterViewModel.Lastname;
            user.Password = HashPassword(accountRegisterViewModel.Password);
            _movieContext.Users.Add(user);
            try
            {
                await _movieContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
        //hash
        public string HashPassword(string password)
        {
            return Argon2.Hash(password);
        }

        public async Task<bool> Login(AccountLoginViewModel accountLoginViewModel)
        {
            var user = await _movieContext.Users.FirstOrDefaultAsync(u => u.Username.Equals(accountLoginViewModel.Username));
            //put user in session
            if (user == null || !Argon2.Verify(user?.Password, accountLoginViewModel.Password))
            {
                return false;
            }
            //set favorite movies session
            return true;
        }
    }
}
