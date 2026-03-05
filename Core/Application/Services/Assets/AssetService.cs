using System.Security.Cryptography.X509Certificates;
using Application.Interfaces;
using Domain.Entities;
using Application.DTO;


namespace Application.Services.Assets
{
    
    public class AssetService : IAssetService
    {
        private readonly IAsset _asset;

        //Constructor
        public AssetService(IAsset asset)
        {
            _asset = asset;
        }
        
        public async Task<List<Asset>> GetAllAssetsAsync()
        {
            return await _asset.GetAllAssetsAsync();
        }

        public async Task<Asset> GetAssetByIdAsync(int id)
        {
            return await _asset.GetAssetByIdAsync(id);
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
    }
}
