using AutoMapper;
using PlacementApi.Db;
using PlacementApi.Dto;

namespace PlacementApi.Repo
{
    public class PlacementRepo : IPlacementRepo
    {
        private IMapper _mapper;
        private AppDbContext _context;

        public PlacementRepo(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AddProductToStorage(PlacementDto placementDto)
        {
            using (_context)
            {
                _context.Add(_mapper.Map<Placement>(placementDto));
                _context.SaveChanges();
            }
        }

        public void DeleteProduct(int productId)
        {
            using (_context)
            {
                var product = _context.Placements.First(x => x.ProductId == productId);
                _context.Placements.Remove(product);
                _context.SaveChanges();
            }
        }

        public IEnumerable<int> GetProductInStorage(int storageId)
        {
            using (_context)
            {
                return _context.Placements.Where(x => x.StorageId == storageId).Select(x=>x.ProductId).ToList(); ;
            }
        }
    }
}
