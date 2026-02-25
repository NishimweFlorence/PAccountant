using Application.DTO;
using Application.Interfaces;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface IAsset
    {
        public List<Asset> GetAllAssets();

        public Asset GetAssetById(int id);

        void CreateAsset(AssetCreateDTO assetCreateDTO);
        void UpdateAsset(int id,AssetUpdateDTO assetUpdateDTO);
    }
}