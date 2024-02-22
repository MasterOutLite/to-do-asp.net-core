namespace Application.Common.Models;

public record PaginationResponse<T>(int Page, int Total, IEnumerable<T> Data);