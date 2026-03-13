using Application.Interfaces;
using Domain.Entities;
using Application.DTO;


namespace Application.Services.Assets
{
    public class AssetService : IAssetService
    {
        private readonly IAsset _asset;

        public AssetService(IAsset asset)
        {
            _asset = asset;
        }

        public async Task<List<Asset>> GetAllAssetsAsync()
        {
            var assets = await _asset.GetAllAssetsAsync();
            foreach (var asset in assets)
            {
                CalculateCurrentValue(asset);
            }
            return assets;
        }

        public async Task<Asset?> GetAssetByIdAsync(int id)
        {
            var asset = await _asset.GetAssetByIdAsync(id);
            if (asset != null)
            {
                CalculateCurrentValue(asset);
            }
            return asset;
        }

        public async Task CreateAssetAsync(AssetCreateDTO assetDTO)
        {
            await _asset.CreateAssetAsync(assetDTO);
        }

        public async Task UpdateAssetAsync(int id, AssetUpdateDTO assetUpdateDTO)
        {
            await _asset.UpdateAssetAsync(id, assetUpdateDTO);
        }

        public async Task DeleteAssetAsync(int id)
        {
            await _asset.DeleteAssetAsync(id);
        }

        private void CalculateCurrentValue(Asset asset)
        {
            if (asset.ValueChange == null || asset.ValueChange == Domain.ValueObjects.ValueChangeType.None || asset.Rate == 0)
            {
                asset.CurrentValue = asset.PurchaseValue;
                return;
            }

            var daysPassed = (DateTime.Now - asset.PurchaseDate).TotalDays;
            if (daysPassed < 0) daysPassed = 0;
            
            double yearsPassed = daysPassed / 365.25;
            double rate = (double)asset.Rate / 100.0;
            double compoundMultiplier = 1.0;

            if (asset.ValueChange == Domain.ValueObjects.ValueChangeType.Appreciate)
            {
                compoundMultiplier = Math.Pow(1.0 + rate, yearsPassed);
            }
            else if (asset.ValueChange == Domain.ValueObjects.ValueChangeType.Depreciate)
            {
                compoundMultiplier = Math.Pow(1.0 - rate, yearsPassed);
                if (compoundMultiplier < 0) compoundMultiplier = 0; // Value cannot drop below 0
            }

            asset.CurrentValue = asset.PurchaseValue * (decimal)compoundMultiplier;
        }
    }
}
