using Avengers.Domain.Entities;

namespace Avengers.Application.QuoteItems.Queries.ExportQuotes;

public class QuoteExportDto
{
    public string? Title { get; init; }
    public string? Note { get; init; }
    public int? Rating { get; init; }
    public string? Comments { get; init; }
    public string? Source { get; init; }
    public string? Author { get; init; }
    public string? ReferenceUrl { get; init; }
    public string? Tags { get; init; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<QuoteItem, QuoteExportDto>();
        }
    }
}
