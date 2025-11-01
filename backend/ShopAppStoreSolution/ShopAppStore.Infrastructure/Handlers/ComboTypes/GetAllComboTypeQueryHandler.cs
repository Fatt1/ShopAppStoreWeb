using Microsoft.EntityFrameworkCore;
using ShopAppStore.Application.Abstractions.CQRS.Query;
using ShopAppStore.Application.Features.ComboTypes.DTOs;
using ShopAppStore.Application.Features.ComboTypes.Queries;
using ShopAppStore.Infrastructure.Repositories;
using ShopAppStore.Shared;

namespace ShopAppStore.Infrastructure.Handlers.ComboTypes
{
    public class GetAllComboTypeQueryHandler : IQueryHandler<GetAllComboTypeQuery, List<GetAllComboTypeDTO>>
    {
        private readonly AppDbContext _context;
        public GetAllComboTypeQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<GetAllComboTypeDTO>>> Handle(GetAllComboTypeQuery request, CancellationToken cancellationToken)
        {
            var comboTypes = await _context.ComboTypes
                .AsNoTracking()
                .Select(ct => new GetAllComboTypeDTO
                {
                    Id = ct.Id,
                    Name = ct.Name,
                    QuantityRequired = ct.QuantityRequired
                })
                .ToListAsync();
            return Result<List<GetAllComboTypeDTO>>.Success(comboTypes);
        }
    }
}
