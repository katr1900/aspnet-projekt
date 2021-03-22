using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Foodie.Core.Data;
using Foodie.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Foodie.Core.Models
{
    public class UserModel
    {
        private DataContext dataContext;

        public UserModel(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<User>> GetAll()
        {
            return await dataContext.Users.ToListAsync();
        }

        public async Task<User> GetUser(string username)
        {
            var user = await dataContext.Users.Include(u => u.Person).Include(u => u.Address).FirstOrDefaultAsync(p => p.Username == username);
            return user;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await dataContext.Users.Include(u => u.Person).Include(u => u.Address).FirstOrDefaultAsync(p => p.Id == id);
            return user;
        }

        public async Task RegisterUser(User user)
        {
            user.Password = HashPassword(user.Password);
            dataContext.Users.Add(user);
            await dataContext.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await dataContext.Users.FirstOrDefaultAsync(c => c.Id == id);
            dataContext.Users.Remove(user);
            await dataContext.SaveChangesAsync();
        }

        public async Task<Boolean> IsCorrectPassword(int id, string password)
        {
            var user = await dataContext.Users.FirstOrDefaultAsync(p => p.Id == id);
            if(user == null)
            {
                return false;
            }

            if(user.Password == HashPassword(password))
            {
                return true;
            }

            return false;
        }

        public string HashPassword(string password)
        {
            byte[] salt = { 0, 100, 120, 210, 255};
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA1, 120, 256 / 8));
            return hash;
        }
    }
}
