using Microsoft.AspNetCore.Mvc;
using ProductsAndGroupsApi.Dto;
using ProductsAndGroupsApi.Repo;

namespace ProductsAndGroupsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsAndGroupsController:ControllerBase
    {
        private IProductsAndGroupsRepo _productsAndGroupsRepo;

        public ProductsAndGroupsController(IProductsAndGroupsRepo productsAndGroupsRepo)
        {
            _productsAndGroupsRepo = productsAndGroupsRepo;
        }

        [HttpPost("AddProduct")]
        public ActionResult AddProduct(ProductDto productDto)
        {
            _productsAndGroupsRepo.AddProduct(productDto);
            return Ok();
        }

        [HttpGet("CheckProduct")]
        public ActionResult<bool> CheckProduct(int productId)
        {
            return Ok(_productsAndGroupsRepo.CheckProduct(productId));
        }

        [HttpGet("GetProducts")]
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            return Ok(_productsAndGroupsRepo.GetProducts());
        }

        [HttpPost("AddProductGroup")]
        public ActionResult AddProductGroup(ProductGroupDto productGroupDto)
        {
            _productsAndGroupsRepo.AddProductGroup(productGroupDto);
            return Ok();
        }

        [HttpGet("GetProductGroups")]
        public ActionResult<IEnumerable<ProductDto>> GetProductGroups()
        {
            return Ok(_productsAndGroupsRepo.GetProductGroups());
        }
    }
}
