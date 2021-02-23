using CustomAuthenticationFilter.Data;
using CustomAuthenticationFilter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAuthenticationFilter.Services
{
    public interface ISenitFxManagerService
    {
        Task<SenitFxKey> GetApiKey(string key);

        Task<bool> KillApiKey(string key);

        Task<bool> EnableApiKey(string key);

        Task KillAllKeys();

        Task<SenitFxKey> GenerateNewApiKey();
    }

    public class SenitFxManagerService : ISenitFxManagerService
    {
        private readonly SenitFxDbContext _dbContext;

        public SenitFxManagerService(SenitFxDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> EnableApiKey(string key)
        {
            var enabledKey = await _dbContext.SenitFxKeys.Where(x => x.Key.ToLower().Equals(key.ToLower())).FirstOrDefaultAsync();
            if (enabledKey == null)
                return false;

            enabledKey.IsActive = true;
            _dbContext.SenitFxKeys.Update(enabledKey);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<SenitFxKey> GenerateNewApiKey()
        {
            var key = Guid.NewGuid().ToString().Replace("-", "");
            var newKey = new SenitFxKey
            {
                Key = Convert.ToBase64String(Encoding.UTF8.GetBytes(key)),
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _dbContext.SenitFxKeys.Add(newKey);
            await _dbContext.SaveChangesAsync();

            return newKey;
        }

        public async Task<SenitFxKey> GetApiKey(string key)
        {
            return await _dbContext.SenitFxKeys.Where(x => x.Key.ToLower().Equals(key.ToLower()) && x.IsActive)
                .FirstOrDefaultAsync();
        }

        public async Task KillAllKeys()
        {
            var items = await _dbContext.SenitFxKeys.ToListAsync();
            foreach (var item in items)
            {
                item.IsActive = false;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> KillApiKey(string key)
        {
            var enabledKey = await GetApiKey(key);
            if (enabledKey == null)
                return false;

            enabledKey.IsActive = false;
            _dbContext.SenitFxKeys.Update(enabledKey);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}