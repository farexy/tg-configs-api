namespace TG.Configs.Api.Config
{
    public static class ServiceConst
    {
        public const string ServiceName = "configs";
        public const string ProjectName = "TG.Configs.Api";

        public const string BaseRoutePrefix = ServiceName + "/v{version:apiVersion}";
        public const string RoutePrefix = BaseRoutePrefix + "/[controller]";
        public const string BattleServerRoutePrefix =  "/bs/" + BaseRoutePrefix;
        public const string BaseInternalRoutePrefix = "internal/" +  BaseRoutePrefix;
        public const string InternalRoutePrefix = BaseInternalRoutePrefix + "/[controller]";
    }
}