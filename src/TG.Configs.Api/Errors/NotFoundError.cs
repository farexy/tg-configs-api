using TG.Core.App.OperationResults;

namespace TG.Game.Api.Errors
{
    public static class GameErrors
    {
        public static readonly ErrorResult NotFound = new ErrorResult("not_found", "Not found");
    }
}