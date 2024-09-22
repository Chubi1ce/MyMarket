using StoragesApi.Dto;

namespace StoragesApi.Repositories
{
    public interface IStorageRepository
    {
        public void AddStorage(StorageDto storageDto);
        public IEnumerable<StorageDto> GetStorages();
        public void EditStorage(StorageDto storageDto);
        public bool CheckStorage(int storageId);
    }
}
