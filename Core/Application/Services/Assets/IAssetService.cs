using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAssetService
    {
        Task<List<Asset>> GetAllAssetsAsync();
        Task<Asset?> GetAssetByIdAsync(int id);
        Task CreateAssetAsync(AssetCreateDTO assetCreateDTO);
        Task UpdateAssetAsync(int id, AssetUpdateDTO assetUpdateDTO);
        Task DeleteAssetAsync(int id);
    }
}
