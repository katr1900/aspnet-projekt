using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Foodie.Core.Data;
using Foodie.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Foodie.Core.Models
{
    public class AdministratorModel
    {
        private DataContext dataContext;

        public AdministratorModel(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Administrator> GetUser(string username)
        {
            var user = await dataContext.Administrators.FirstOrDefaultAsync(p => p.Username == username);
            return user;
        }

        public async Task<Administrator> GetUser(int id)
        {
            var user = await dataContext.Administrators.FirstOrDefaultAsync(p => p.Id == id);
            return user;
        }

        public async Task RegisterUser(Administrator admin)
        {
            admin.Password = HashPassword(admin.Password);
            dataContext.Administrators.Add(admin);
            await dataContext.SaveChangesAsync();
        }

        public async Task<Boolean> IsCorrectPassword(int id, string password)
        {
            var user = await dataContext.Administrators.FirstOrDefaultAsync(p => p.Id == id);
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
            byte[] salt = { 0, 115, 100, 210, 255};
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA1, 120, 256 / 8));
            return hash;
        }
    }
}
