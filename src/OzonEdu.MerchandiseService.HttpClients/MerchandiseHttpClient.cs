using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.HttpClients.Interfaces;
using OzonEdu.MerchandiseService.HttpModels.Request;
using OzonEdu.MerchandiseService.HttpModels.Response;

namespace OzonEdu.HttpClients
{
    public class MerchandiseHttpClient : IMerchandiseHttpClient
    {
        private readonly HttpClient _httpClient;

        public MerchandiseHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<RequestMerchResponse> RequestMerch(RequestMerchPostViewModel postViewModel, CancellationToken token)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(postViewModel), Encoding.UTF8, "application/json");
            using var response = await _httpClient.PostAsync("v1/api/merchandise",stringContent, token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<RequestMerchResponse>(body);
        }

        public async Task<IEnumerable<MerchInfoResponse>> GetEmployeeMerchById(long employeeId, CancellationToken token)
        {
            using var response = await _httpClient.GetAsync($"v1/api/merchandise?employeeId={employeeId}", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<IEnumerable<MerchInfoResponse>>(body);
        }
    }
}