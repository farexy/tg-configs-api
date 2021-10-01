namespace TG.Configs.Api.Config
{
    public static class ServiceConst
    {
        public const string ServiceName = "configs";
        public const string ProjectName = "TG.Configs.Api";

        public const string RoutePrefix = ServiceName + "/v{version:apiVersion}/[controller]";
        public const string InternalRoutePrefix = "internal/" + ServiceName + "/v{version:apiVersion}/[controller]";
    }
}