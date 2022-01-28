using AutoMapper;
using TG.Configs.Api.Models.Dto;
using TG.Configs.Api.Models.Response;

namespace TG.Configs.Api.Config.Mapper
{
    public class ConfigProfile : Profile
    {
        public ConfigProfile()
        {
            CreateMap<Entities.Config, ConfigManagementResponse>();
            CreateMap<Entities.Config, ConfigItemResponse>();
            CreateMap<ConfigData, ConfigResponse>();
        }
    }
}