using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

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

      public List<Asset> GetAllAssets()
        {
           List<Asset> Assets = _dbContext.Assets.ToList();
              return Assets;
             
        }

        public Asset GetAssetById(int id)
        {
            return _dbContext.Assets.FirstOrDefault(c => c.Id == id);
        }

        public void CreateAsset(AssetCreateDTO assetCreateDTO)
        {
            Asset asset = new()
            {
                Name = assetCreateDTO.Name,
                Category = assetCreateDTO.Category,
                PurchaseDate = assetCreateDTO.PurchaseDate,
                Currency = assetCreateDTO.Currency,
                PurchaseValue = assetCreateDTO.PurchaseValue,
                 CurrentValue= assetCreateDTO.CurrentValue,
                CreatedAt = DateTime.UtcNow,
                
            
            };
            _dbContext.Assets.Add(asset);
            _dbContext.SaveChanges();
        }
    

    public void UpdateAsset(int id, AssetUpdateDTO assetUpdateDTO)
        {
            var asset = _dbContext.Assets.Find(id);
            if (asset == null) return;
            {
                asset.Name = assetUpdateDTO.Name;
                asset.Category = assetUpdateDTO.Category;
                asset.PurchaseDate = assetUpdateDTO.PurchaseDate;
                asset.Currency = assetUpdateDTO.Currency;
                asset.PurchaseValue = assetUpdateDTO.PurchaseValue;
                asset.CurrentValue = assetUpdateDTO.CurrentValue;
                _dbContext.SaveChanges();
            }
        }


    }
    
}