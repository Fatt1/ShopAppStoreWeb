using Microsoft.EntityFrameworkCore;
using ShopAppStore.Application.Abstractions.CQRS.Query;
using ShopAppStore.Application.Features.PlatForms.DTOs;
using ShopAppStore.Application.Features.PlatForms.Queries;
using ShopAppStore.Infrastructure.Repositories;
using ShopAppStore.Shared;

namespace ShopAppStore.Infrastructure.Handlers.PlatForms
{
    public class GetAllPlatFormQueryHandler : IQueryHandler<GetAllPlatFormQuery, List<GetAllPlatFormDTO>>
    {
        private readonly AppDbContext _context;
        public GetAllPlatFormQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<GetAllPlatFormDTO>>> Handle(GetAllPlatFormQuery request, CancellationToken cancellationToken)
        {
            var platForms = await _context.PlatForms
                .AsNoTracking()
                .OrderBy(p => p.PlatFormName)
                .Select(p => new GetAllPlatFormDTO
                {
                    Id = p.Id,
                    PlatFormName = p.PlatFormName
                }).ToListAsync();

            return Result<List<GetAllPlatFormDTO>>.Success(platForms);
        }
    }
}
