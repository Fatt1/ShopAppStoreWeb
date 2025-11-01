using ShopAppStore.Application.Abstractions.CQRS.Command;
using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;
using ShopAppStore.Shared;

namespace ShopAppStore.Application.Features.Durations.Commands.CreateDuration
{
    public class CreateDurationCommand : ICommand<Guid>
    {
        public string DurationName { get; set; } = null!;
    }

    public class CreateDurationCommandHandler : ICommandHandler<CreateDurationCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDurationRepository _durationRepository;
        public CreateDurationCommandHandler(IUnitOfWork unitOfWork, IDurationRepository durationRepository)
        {
            _unitOfWork = unitOfWork;
            _durationRepository = durationRepository;
        }
        public async Task<Result<Guid>> Handle(CreateDurationCommand request, CancellationToken cancellationToken)
        {
            var duration = new Duration
            {
                Id = Guid.NewGuid(),
                DurationName = request.DurationName,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                IsDeleted = false
            };
            await _durationRepository.AddAsync(duration, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Guid>.Success(duration.Id);
        }
    }
}
