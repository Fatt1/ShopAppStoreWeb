using Microsoft.EntityFrameworkCore;
using ShopAppStore.Application.Abstractions.CQRS.Query;
using ShopAppStore.Application.Features.Durations.DTOs;
using ShopAppStore.Application.Features.Durations.Queries.GetAllDuration;
using ShopAppStore.Infrastructure.Repositories;
using ShopAppStore.Shared;

namespace ShopAppStore.Infrastructure.Handlers.Durations
{
    public class GetAllDurationQueryHandler : IQueryHandler<GetAllDurationQuery, List<GetAllDurationDTO>>
    {
        private readonly AppDbContext _context;
        public GetAllDurationQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<GetAllDurationDTO>>> Handle(GetAllDurationQuery request, CancellationToken cancellationToken)
        {
            var durations = await _context.Durations
                .Where(d => !d.IsDeleted)
                .AsNoTracking()
                .OrderBy(d => d.DurationName)
                .Select(d => new GetAllDurationDTO
                {
                    Id = d.Id,
                    DurationName = d.DurationName
                })
                .ToListAsync();
            return Result<List<GetAllDurationDTO>>.Success(durations);
        }
    }
}
