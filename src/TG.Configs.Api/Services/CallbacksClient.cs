using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TG.Configs.Api.Config;
using TG.Configs.Api.Entities;
using TG.Configs.Api.Helpers;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Services
{
    public class CallbacksClient : ICallbacksClient
    {
        private readonly HttpClient _httpClient;

        public CallbacksClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OperationResult> ReloadCallbackAsync(Callback callback, string configSecret, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(callback.Url + callback.ConfigId),
                Method = HttpMethod.Post,
                Headers =
                {
                    {ConfigHeaders.Signature, Sha256Helper.GetSha256Hash(callback.ConfigId + configSecret)}
                }
            };
            var response = await _httpClient.SendAsync(request, cancellationToken);
            return response.IsSuccessStatusCode ? OperationResult.Success() : OperationResult.Fail(ErrorResult.Create(response.ReasonPhrase!));
        }
    }
}