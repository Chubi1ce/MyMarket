using AutoMapper;
using StoragesApi.Db;
using StoragesApi.Dto;

namespace StoragesApi.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private IMapper _mapper;
        private AppDbContext _context;

        public StorageRepository(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AddStorage(StorageDto storageDto)
        {
            using (_context)
            {
                _context.Storages.Add(_mapper.Map<Storage>(storageDto));
                _context.SaveChanges();
            }
        }

        public void EditStorage(StorageDto storageDto)
        {
            throw new NotImplementedException();
        }

        public bool CheckStorage(int storageId)
        {
            using(_context)
            {
                return _context.Storages.Any(x => x.Id == storageId);
            }
        }

        public IEnumerable<StorageDto> GetStorages()
        {
            using (_context)
            {
                return _context.Storages.Select(_mapper.Map<StorageDto>).ToList();
            }
        }
    }
}
