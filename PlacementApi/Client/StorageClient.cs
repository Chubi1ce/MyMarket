
namespace PlacementApi.Client
{
    public class StorageClient : IStorageClient
    {
        readonly HttpClient client = new HttpClient();
        public async Task<bool> Exists(int storageId)
        {
            using HttpResponseMessage resp = await client.GetAsync($"https://localhost:7296/Storage/CheckStorage?storageId={storageId.ToString()}");
            resp.EnsureSuccessStatusCode();

            string respBody = await resp.Content.ReadAsStringAsync();

            if (respBody == "true")
                return true;
            if (respBody == "false")
                return false;

            throw new Exception("Unknow response");
        }
    }
}
