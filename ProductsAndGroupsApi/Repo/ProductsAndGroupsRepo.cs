using AutoMapper;
using ProductsAndGroupsApi.Db;
using ProductsAndGroupsApi.Dto;

namespace ProductsAndGroupsApi.Repo
{
    public class ProductsAndGroupsRepo : IProductsAndGroupsRepo
    {
        private IMapper _mapper;
        private AppDbContext _context;

        public ProductsAndGroupsRepo(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public void AddProduct(ProductDto productDto)
        {
            using (_context)
            {
                _context.Products.Add(_mapper.Map<Product>(productDto));
                _context.SaveChanges();
            }
        }
        public void AddProductGroup(ProductGroupDto productGroupDto)
        {
            using (_context)
            {
                _context.ProductGroups.Add(_mapper.Map<ProductGroup>(productGroupDto));
                _context.SaveChanges();
            }
        }

        public bool CheckProduct(int productId)
        {
            using (_context)
            {
                return _context.Products.Any(x => x.Id == productId);
            }
        }

        public IEnumerable<ProductGroupDto> GetProductGroups()
        {
            using(_context)
            {
                return _context.ProductGroups.Select(_mapper.Map<ProductGroupDto>).ToList();
            }
        }
        public IEnumerable<ProductDto> GetProducts()
        {
            using (_context)
            {
                return _context.Products.Select(_mapper.Map<ProductDto>).ToList();
            }
        }

    }
}
