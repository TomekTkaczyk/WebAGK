namespace WebAGK.Shared.Infrastructure.CQRS;

public record GetPageRequest(
    string SearchText, 
    int? PageNumber, 
    int? PageSize, 
    bool? IsActive) { }