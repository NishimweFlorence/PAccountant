using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.ValueObjects;

namespace Infrastructure.Repositories 
{
    public class AssetRepository : IAsset
    {
      private readonly ApplicationDbContext _dbContext;

      public AssetRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

      //Retrieving Assets

        public async Task<List<Asset>> GetAllAssetsAsync()
        {
            return await _dbContext.Assets.ToListAsync();
        }

        public async Task<Asset> GetAssetByIdAsync(int id)
        {
            return await _dbContext.Assets.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateAssetAsync(AssetCreateDTO assetCreateDTO)
        {
            Asset asset = new()
            {
                Name = assetCreateDTO.Name,
                Category = assetCreateDTO.Category,
                PurchaseDate = assetCreateDTO.PurchaseDate,
                Currency = Currency.FromCode(assetCreateDTO.Currency),
                PurchaseValue = assetCreateDTO.PurchaseValue,
                CurrentValue = assetCreateDTO.CurrentValue,
                CreatedAt = DateTime.UtcNow,
            };
            _dbContext.Assets.Add(asset);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAssetAsync(int id, AssetUpdateDTO assetUpdateDTO)
        {
            var asset = await _dbContext.Assets.FindAsync(id);
            if (asset == null) return;

            asset.Name = assetUpdateDTO.Name;
            asset.Category = assetUpdateDTO.Category;
            asset.PurchaseDate = assetUpdateDTO.PurchaseDate;
            asset.Currency = Currency.FromCode(assetUpdateDTO.Currency);
            asset.PurchaseValue = assetUpdateDTO.PurchaseValue;
            asset.CurrentValue = assetUpdateDTO.CurrentValue;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAssetAsync(int id)
        {
            var asset = await _dbContext.Assets.FindAsync(id);
            if (asset != null)
            {
                _dbContext.Assets.Remove(asset);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
    
}