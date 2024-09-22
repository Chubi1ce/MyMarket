
namespace PlacementApi.Client
{
    public class ProductClient : IProductClient
    {
        readonly HttpClient client = new HttpClient();
        public async Task<bool> Exists(int productId)
        {
            using HttpResponseMessage resp = await client.GetAsync($"https://localhost:7109/ProductsAndGroups/CheckProduct?productId={productId.ToString()}");
            resp.EnsureSuccessStatusCode();

            string respBody = await resp.Content.ReadAsStringAsync();
            
            if (respBody=="true")
                return true;
            if (respBody == "false")
                return false;

            throw new Exception("Unknow response");
        }
    }
}
