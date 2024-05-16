using Avengers.Application.Common.Interfaces;
using Avengers.Application.QuoteItems.Queries.ExportQuotes;

namespace Avengers.Application.QuoteItems.Queries.GetQuotesExport;

public record GetQuotesExportQuery : IRequest<IEnumerable<QuoteExportDto>>
{
}

public class GetQuotesExportQueryValidator : AbstractValidator<GetQuotesExportQuery>
{
    public GetQuotesExportQueryValidator()
    {
    }
}

public class GetQuotesExportQueryHandler : IRequestHandler<GetQuotesExportQuery, IEnumerable<QuoteExportDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetQuotesExportQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<QuoteExportDto>> Handle(GetQuotesExportQuery request, CancellationToken cancellationToken)
    {
        return await _context.QuoteItems
         .AsNoTracking()
         .ProjectTo<QuoteExportDto>(_mapper.ConfigurationProvider)
         .OrderBy(t => t.Title)
         .ToListAsync(cancellationToken);
    }
}
