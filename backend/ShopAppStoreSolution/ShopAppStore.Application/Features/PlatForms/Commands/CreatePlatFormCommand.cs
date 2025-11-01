using ShopAppStore.Application.Abstractions.CQRS.Command;
using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;
using ShopAppStore.Shared;

namespace ShopAppStore.Application.Features.PlatForms.Commands
{
    public class CreatePlatFormCommand : ICommand<Guid>
    {
        public string PlatFormName { get; set; } = null!;
    }

    public class CreatePlatFormCommandHandler : ICommandHandler<CreatePlatFormCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlatFormRepository _platFormRepository;
        public CreatePlatFormCommandHandler(IUnitOfWork unitOfWork, IPlatFormRepository platFormRepository)
        {
            _unitOfWork = unitOfWork;
            _platFormRepository = platFormRepository;
        }
        public async Task<Result<Guid>> Handle(CreatePlatFormCommand request, CancellationToken cancellationToken)
        {
            var platForm = new PlatForm
            {
                Id = Guid.NewGuid(),
                PlatFormName = request.PlatFormName,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,

            };
            await _platFormRepository.AddAsync(platForm, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Guid>.Success(platForm.Id);
        }
    }

}
