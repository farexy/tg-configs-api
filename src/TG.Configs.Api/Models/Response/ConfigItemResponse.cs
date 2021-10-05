using System.Collections.Generic;

namespace TG.Configs.Api.Models.Response
{
    public class ConfigItemsResponse
    {
        public ConfigItemsResponse(IEnumerable<ConfigItemResponse> items)
        {
            Items = items;
        }

        public IEnumerable<ConfigItemResponse> Items { get; }
    }

    public class ConfigItemResponse
    {
        public string Id { get; set; } = default!;
    }
}