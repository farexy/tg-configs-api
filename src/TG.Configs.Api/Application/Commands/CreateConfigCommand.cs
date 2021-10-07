using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TG.Configs.Api.Db;
using TG.Configs.Api.Errors;
using TG.Configs.Api.Models.Response;
using TG.Core.App.OperationResults;
using TG.Core.App.Services;

namespace TG.Configs.Api.Application.Commands
{
    public record CreateConfigCommand(string Id, string? Content, string UserEmail) : IRequest<OperationResult<ConfigManagementResponse>>;
    
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
            if (!ContentValidator.IsValid(request.Content))
            {
                return AppErrors.InvalidContent;
            }
            var config = new Entities.Config
            {
                Id = request.Id,
                Secret = _cryptoStringGenerator.Generate(SecretLength),
                Content = request.Content,
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