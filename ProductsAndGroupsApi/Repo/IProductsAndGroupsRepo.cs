using ProductsAndGroupsApi.Dto;

namespace ProductsAndGroupsApi.Repo
{
    public interface IProductsAndGroupsRepo
    {
        public void AddProduct(ProductDto product);
        public bool CheckProduct(int productId);
        public IEnumerable<ProductDto> GetProducts();
        public void AddProductGroup(ProductGroupDto productGroupDto);
        public IEnumerable<ProductGroupDto> GetProductGroups();
    }
}
