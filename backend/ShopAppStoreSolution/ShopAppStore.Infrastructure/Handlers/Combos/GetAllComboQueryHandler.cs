using Microsoft.EntityFrameworkCore;
using ShopAppStore.Application.Abstractions.CQRS.Query;
using ShopAppStore.Application.Features.Combos.DTOs;
using ShopAppStore.Application.Features.Combos.Queries;
using ShopAppStore.Domain.Constants;
using ShopAppStore.Infrastructure.Repositories;
using ShopAppStore.Shared;

namespace ShopAppStore.Infrastructure.Handlers.Combos
{

    public class GetAllComboQueryHandler : IQueryHandler<GetAllComboQuery, List<GetAllComboDTO>>
    {
        private readonly AppDbContext _context;
        public GetAllComboQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<GetAllComboDTO>>> Handle(GetAllComboQuery request, CancellationToken cancellationToken)
        {
            var combos = await _context.Combos
                    .Where(c => !c.IsDeleted)
                    .Include(c => c.ComboApps)
                        .ThenInclude(ca => ca.App)
                            .ThenInclude(a => a.AppAccounts)
                    .Select(c => new GetAllComboDTO
                    {
                        Id = c.Id,
                        Description = c.Description,
                        ComboName = c.ComboName,
                        ComboTypeId = c.ComboTypeId,
                        ComboPrice = c.ComboPrice,
                        // Tính stock = số lượng tồn kho thấp nhất của các app trong combo
                        Stock = c.ComboApps
                            .Select(ca => ca.App.AppAccounts
                                .Count(acc => acc.Status == AppAccountStatus.Available))
                            .Min(),
                        Apps = c.ComboApps.Select(ca => new ComboAppInfoDTO
                        {
                            AppId = ca.AppId,
                            AppName = ca.App.AppName,
                            ThumbnailUrl = ca.App.ThumbnailUrl,
                            CurrentPrice = ca.App.CurrentPrice
                        }).ToList()
                    })
                    .ToListAsync(cancellationToken);
            return Result<List<GetAllComboDTO>>.Success(combos);
        }
    }
}
