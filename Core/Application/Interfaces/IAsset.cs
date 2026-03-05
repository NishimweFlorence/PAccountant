using Application.DTO;
using Application.Interfaces;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface IAsset
    {
        Task<List<Asset>> GetAllAssetsAsync();
        Task<Asset> GetAssetByIdAsync(int id);
        Task CreateAssetAsync(AssetCreateDTO assetCreateDTO);
        Task UpdateAssetAsync(int id, AssetUpdateDTO assetUpdateDTO);
        Task DeleteAssetAsync(int id);
    }
}
