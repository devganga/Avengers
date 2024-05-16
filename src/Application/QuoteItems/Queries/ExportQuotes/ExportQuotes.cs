using Avengers.Application.Common.Interfaces;
using Avengers.Application.QuoteItems.Queries.GetQuotes;

namespace Avengers.Application.QuoteItems.Queries.ExportQuotes;

public record ExportQuotesQuery : IRequest<IEnumerable<QuoteExportDto>>
{
}

public class ExportQuotesQueryValidator : AbstractValidator<ExportQuotesQuery>
{
    public ExportQuotesQueryValidator()
    {
    }
}

public class ExportQuotesQueryHandler : IRequestHandler<ExportQuotesQuery, IEnumerable<QuoteExportDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ExportQuotesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<QuoteExportDto>> Handle(ExportQuotesQuery request, CancellationToken cancellationToken)
    {
        //results
        return await _context.QuoteItems
         .AsNoTracking()
         .ProjectTo<QuoteExportDto>(_mapper.ConfigurationProvider)
         .OrderBy(t => t.Title)
         .ToListAsync(cancellationToken);
    }
}
