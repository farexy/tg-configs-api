using System.Collections.Generic;

namespace TG.Configs.Api.Models.Dto
{
    public record AppEndpointAddressesDto(IEnumerable<AppEndpointAddressDto>? Endpoints);

    public class AppEndpointAddressDto
    {
        public string Ip { get; set; } = default!;
    }
}