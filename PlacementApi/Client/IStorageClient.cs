namespace PlacementApi.Client
{
    public interface IStorageClient
    {
        public Task<bool> Exists(int storageId);
    }
}
