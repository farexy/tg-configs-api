using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TG.Configs.Api.Db;
using TG.Configs.Api.Entities;
using TG.Configs.Api.Errors;
using TG.Configs.Api.Models.Response;
using TG.Core.App.OperationResults;
using TG.Core.App.Services;

namespace TG.Configs.Api.Application.Commands
{
    public record CreateConfigCommand(string Id, string? Content, ConfigFormat Format, bool HasSecrets, string UserEmail) : IRequest<OperationResult<ConfigManagementResponse>>;
    
    public class CreateConfigCommandHandler : IRequestHandler<CreateConfigCommand, OperationResult<ConfigManagementResponse>>
    {
        private const int SecretLength = 32;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICryptoResistantStringGenerator _cryptoStringGenerator;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateConfigCommandHandler(ApplicationDbContext dbContext, IMapper mapper, ICryptoResistantStringGenerator cryptoStringGenerator,
            IDateTimeProvider dateTimeProvider)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _cryptoStringGenerator = cryptoStringGenerator;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<OperationResult<ConfigManagementResponse>> Handle(CreateConfigCommand request, CancellationToken cancellationToken)
        {
            string? optimizedContent = request.Content;
            if (request.Format is ConfigFormat.Json && !ContentValidator.IsValid(request.Content, out optimizedContent))
            {
                return AppErrors.InvalidContent;
            }
            var config = new Entities.Config
            {
                Id = request.Id,
                Secret = _cryptoStringGenerator.Generate(SecretLength),
                Content = optimizedContent,
                Format = request.Format,
                HasSecrets = request.HasSecrets,
                CreatedBy = request.UserEmail,
                CreatedAt = _dateTimeProvider.UtcNow,
                UpdatedBy = request.UserEmail,
                UpdatedAt = _dateTimeProvider.UtcNow,
            };

            await _dbContext.AddAsync(config, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ConfigManagementResponse>(config);
        }
    }
}