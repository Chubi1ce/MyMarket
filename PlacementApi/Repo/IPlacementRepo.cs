using PlacementApi.Db;
using PlacementApi.Dto;

namespace PlacementApi.Repo
{
    public interface IPlacementRepo
    {
        public void AddProductToStorage(PlacementDto placementDto);
        public void DeleteProduct(int productId);
        public IEnumerable<int> GetProductInStorage(int storageId);
    }
}
