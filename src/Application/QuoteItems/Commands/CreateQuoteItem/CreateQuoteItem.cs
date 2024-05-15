using Avengers.Application.Common.Interfaces;
using Avengers.Domain.Entities;
using Avengers.Domain.Events;

namespace Avengers.Application.QuoteItems.Commands.CreateQuoteItem;

public record CreateQuoteItemCommand : IRequest<int>
{
    public string? Title { get; init; }
    public string? Note { get; init; }
    public int? Rating { get; init; }
    public string? Comments { get; init; }
    public string? Source { get; init; }
    public string? Author { get; init; }
    public string? ReferenceUrl { get; init; }
    public string? Tags { get; init; }
}

public class CreateQuoteItemCommandValidator : AbstractValidator<CreateQuoteItemCommand>
{
    public CreateQuoteItemCommandValidator()
    {
    }
}

public class CreateQuoteItemCommandHandler : IRequestHandler<CreateQuoteItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateQuoteItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateQuoteItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new QuoteItem
        {
            Title = request.Title,
            Note = request.Note,
            Tags = request.Tags,
            Author = request.Author,
            Comments = request.Comments,           
            Rating = request.Rating,    
            Source = request.Source,
           ReferenceUrl = request.ReferenceUrl,
        };

        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        _context.QuoteItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
