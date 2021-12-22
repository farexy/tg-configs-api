namespace TG.Configs.Api.Services
{
    public interface IConfigContentCache
    {
        string? Find(string configId, string secret);
        string? Set(string configId, string secret, string? content);
        void Reset();
    }
}