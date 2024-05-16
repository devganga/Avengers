using Avengers.Application.Common.Interfaces;

namespace Avengers.Application.QuoteItems.Commands.UpdateQuoteItem;

public record UpdateQuoteItemCommand : IRequest<int>
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Note { get; init; }
    public int? Rating { get; init; }
    public string? Comments { get; init; }
    public string? Source { get; init; }
    public string? Author { get; init; }
    public string? ReferenceUrl { get; init; }
    public string? Tags { get; init; }
}

public class UpdateQuoteItemCommandValidator : AbstractValidator<UpdateQuoteItemCommand>
{
    public UpdateQuoteItemCommandValidator()
    {
    }
}

public class UpdateQuoteItemCommandHandler : IRequestHandler<UpdateQuoteItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdateQuoteItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateQuoteItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.QuoteItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title;
        entity.Note = request.Note;
        entity.Tags = request.Tags;
        entity.Author = request.Author;
        entity.Comments = request.Comments;
        entity.Rating = request.Rating;
        entity.Source = request.Source;
        entity.ReferenceUrl = request.ReferenceUrl;

        return await _context.SaveChangesAsync(cancellationToken);
    }
}
