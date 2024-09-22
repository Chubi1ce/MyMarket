namespace PlacementApi.Client
{
    public interface IProductClient
    {
       public Task<bool> Exists(int productId);
    }
}
