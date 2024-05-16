using Avengers.Application.Common.Interfaces;
using Avengers.Domain.Events;

namespace Avengers.Application.QuoteItems.Commands.DeleteQuoteItem;

public record DeleteQuoteItemCommand(int Id) : IRequest<int>
{
}

public class DeleteQuoteItemCommandValidator : AbstractValidator<DeleteQuoteItemCommand>
{
    public DeleteQuoteItemCommandValidator()
    {
    }
}

public class DeleteQuoteItemCommandHandler : IRequestHandler<DeleteQuoteItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public DeleteQuoteItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(DeleteQuoteItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.QuoteItems
             .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.QuoteItems.Remove(entity);

        //entity.AddDomainEvent(new TodoItemDeletedEvent(entity));

        return await _context.SaveChangesAsync(cancellationToken);
    }
}
