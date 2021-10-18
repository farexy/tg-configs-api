using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TG.Configs.Api.Config;
using TG.Configs.Api.Entities;
using TG.Configs.Api.Helpers;
using TG.Core.App.InternalCalls;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Services
{
    public class CallbacksClient : ICallbacksClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public CallbacksClient(HttpClient httpClient, IOptions<InternalCallsOptions> opt)
        {
            _httpClient = httpClient;
            _apiKey = opt.Value.ApiKey;
        }

        public async Task<OperationResult> ReloadCallbackAsync(Callback callback, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(callback.Url + callback.ConfigId),
                Method = HttpMethod.Post,
                Headers =
                {
                    {ConfigHeaders.Signature, Sha256Helper.GetSha256Hash(callback.ConfigId + _apiKey)}
                }
            };
            var response = await _httpClient.SendAsync(request, cancellationToken);
            return response.IsSuccessStatusCode ? OperationResult.Success() : OperationResult.Fail(ErrorResult.Create(response.ReasonPhrase!));
        }
    }
}