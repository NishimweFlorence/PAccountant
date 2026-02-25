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
        
            public List<Asset> GetAllAssets()
            {
                List<Asset> assets = _asset.GetAllAssets();
               return assets;
            }

            public Asset GetAssetById(int id)
            {
               return _asset.GetAssetById(id);
            }   

            public void CreateAsset(AssetCreateDTO assetDTO)
            {  
              _asset.CreateAsset(assetDTO);
            }

            public void UpdateAsset(int id, AssetUpdateDTO assetUpdateDTO)
            {
               _asset.UpdateAsset(id, assetUpdateDTO);
            }

    //     List<Domain.Entities.Asset> IAssetService.GetAllAssets()
    //     {
    //         throw new NotImplementedException();
    //     }

    //     Domain.Entities.Asset IAssetService.GetAssetById(int id)
    //     {
    //         throw new NotImplementedException();
    //     }
    // }
}}