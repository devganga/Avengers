using Avengers.Application.Common.Models;
using Avengers.Application.QuoteItems.Commands.CreateQuoteItem;
using Avengers.Application.QuoteItems.Commands.DeleteQuoteItem;
using Avengers.Application.QuoteItems.Commands.UpdateQuoteItem;
using Avengers.Application.QuoteItems.Queries.ExportQuotes;
using Avengers.Application.QuoteItems.Queries.GetQuoteItemsWithPagination;
using Avengers.Application.QuoteItems.Queries.GetQuotes;
using Avengers.Application.QuoteItems.Queries.GetQuotesExport;

namespace Avengers.Web.Endpoints;

public class QuoteItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            //.MapGet(GetQuote, "all")
            //.MapGet(ExportQuotes, "export")
            .MapGet(GetQuoteItemsWithPagination)
            .MapPost(CreateQuoteItem)
            .MapPut(UpdateQuoteItem, "{id}")
            .MapDelete(DeleteQuoteItem, "{id}");
    }

    public Task<PaginatedList<QuotesDto>> GetQuoteItemsWithPagination(ISender sender, [AsParameters] GetQuoteItemsWithPaginationQuery query)
    {
        return sender.Send(query);
    }
    public Task<IEnumerable<QuotesVm>> GetQuote(ISender sender, [AsParameters] GetQuotesQuery query)
    {
        return sender.Send(query);

    }
    public Task<IEnumerable<QuoteExportDto>> ExportQuotes(ISender sender, [AsParameters] GetQuotesExportQuery query)
    {
        return sender.Send(query);

        //var text = Encoding.UTF8.GetBytes(string.Join("\n", res.Select(x => $"{x.Title}")));
        //return Results.File(text, "text/csv", $"{Guid.NewGuid()}.txt");
    }

    public Task<int> CreateQuoteItem(ISender sender, CreateQuoteItemCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateQuoteItem(ISender sender, int id, UpdateQuoteItemCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }


    public async Task<IResult> DeleteQuoteItem(ISender sender, int id)
    {
        await sender.Send(new DeleteQuoteItemCommand(id));
        return Results.NoContent();
    }
}
