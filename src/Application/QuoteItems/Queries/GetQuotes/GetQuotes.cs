using Avengers.Application.Common.Interfaces;
using Avengers.Application.TodoLists.Queries.GetTodos;

namespace Avengers.Application.QuoteItems.Queries.GetQuotes;

public record GetQuotesQuery : IRequest<IEnumerable<QuotesVm>>
{
}

public class GetQuotesQueryValidator : AbstractValidator<GetQuotesQuery>
{
    public GetQuotesQueryValidator()
    {
    }
}

public class GetQuotesQueryHandler : IRequestHandler<GetQuotesQuery, IEnumerable<QuotesVm>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetQuotesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<QuotesVm>> Handle(GetQuotesQuery request, CancellationToken cancellationToken)
    {
        //throw new NotImplementedException();
        return await _context.QuoteItems
                .AsNoTracking()
                .ProjectTo<QuotesVm>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken);
    }
}
