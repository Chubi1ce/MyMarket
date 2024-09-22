using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PlacementApi.Client;
using PlacementApi.Dto;
using PlacementApi.Repo;

namespace PlacementApi.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class PlacementController:ControllerBase
    {
        private IPlacementRepo _repo;

        public PlacementController(IPlacementRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("AddToStorage")]
        public async Task<AddToStorageResultDto> AddToStorage(PlacementDto placementDto)
        {
            var productExistsTask = new ProductClient().Exists(placementDto.ProductId);
            var storageExistsTask = new StorageClient().Exists(placementDto.StorageId);

            var productExists = await productExistsTask;
            var storageExists = await storageExistsTask;

            if (productExists && storageExists)
            {
                try
                {
                    _repo.AddProductToStorage(placementDto);
                    return new AddToStorageResultDto { Success = true };
                }
                catch (Exception ex)
                {
                    if (ex is DbUpdateException && ex.InnerException is PostgresException && ex?.InnerException?.Message?.Contains("duplicate")==true)
                    {
                        return new AddToStorageResultDto { Error = "Такой товар уже размещен" };
                    }
                    throw;
                }
                
            }
            else
            {
                if (!productExists)
                    return new AddToStorageResultDto { Error = "Товар не найден" };
                else
                    return new AddToStorageResultDto { Error = "Склад не найден" };
            }
        }

        [HttpPost("DeleteProductFromStorage")]
        public ActionResult DeleteProductFromStorage(int productId)
        {
            _repo.DeleteProduct(productId);
            return Ok();
        }

        [HttpGet("GetProductInStorage")]
        public ActionResult<IEnumerable<int>> GetProductInStorage(int storageId)
        {
            return Ok(_repo.GetProductInStorage(storageId));
        }
    }
}
