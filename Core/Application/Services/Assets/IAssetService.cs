using Application.DTO;
using Application.Interfaces;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface IAssetService
    {
         List<Asset> GetAllAssets();

        Asset GetAssetById(int id);

        void CreateAsset(AssetCreateDTO assetCreateDTO);
        void UpdateAsset(int id,AssetUpdateDTO assetUpdateDTO);
    }
}