using Avengers.Application.Common.Interfaces;
using Avengers.Application.Common.Mappings;
using Avengers.Application.Common.Models;

namespace Avengers.Application.QuoteItems.Queries.GetQuoteItemsWithPagination;

public record GetQuoteItemsWithPaginationQuery : IRequest<PaginatedList<QuotesDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetQuoteItemsWithPaginationQueryValidator : AbstractValidator<GetQuoteItemsWithPaginationQuery>
{
    public GetQuoteItemsWithPaginationQueryValidator()
    {
    }
}

public class GetQuoteItemsWithPaginationQueryHandler : IRequestHandler<GetQuoteItemsWithPaginationQuery, PaginatedList<QuotesDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetQuoteItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<QuotesDto>> Handle(GetQuoteItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.QuoteItems
            .OrderBy(x => x.Title)
            .ProjectTo<QuotesDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
