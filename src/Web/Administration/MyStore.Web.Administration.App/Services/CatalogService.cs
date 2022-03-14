using MyStore.Web.Administration.App.Models.Categories;
using Newtonsoft.Json;

#nullable disable
namespace MyStore.Web.Administration.App.Services
{
	public class CatalogService
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public CatalogService(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
		}

		public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync(int count = 20, int offset = 0)
		{
			using var client = CreateClient();

			var response = await client.GetAsync($"api/categories?count={count}&offset={offset}");

			return await ReadResponseAsync<IEnumerable<CategoryViewModel>>(response);
		}

		public async Task<CategoryViewModel> CreateCategoryAsync(CreateCategoryViewModel category)
		{
			using var client = CreateClient();

			var response = await client.PostAsync("api/categories", new StringContent(JsonConvert.SerializeObject(category), System.Text.Encoding.UTF8, "application/json"));

			return await ReadResponseAsync<CategoryViewModel>(response);
		}

		private HttpClient CreateClient()
		{
			return _httpClientFactory.CreateClient("GatewayClient");
		}

		private static async Task<T> ReadResponseAsync<T>(HttpResponseMessage responseMessage)
		{
			responseMessage.EnsureSuccessStatusCode();

			string content = await responseMessage.Content.ReadAsStringAsync();

			T result = JsonConvert.DeserializeObject<T>(content);

			return result;
		}
	}
}
