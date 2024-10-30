namespace Epal.Application.Common;

public class PaginatedResult<T>
{
    public int Take { get; set; }
    public int Skip { get; set; }
    public int Total { get; set; }
    
    public IEnumerable<T> Data { get; set; }

    public static PaginatedResult<T> Create(IEnumerable<T> data, int take, int skip, int total)
    {
        return new PaginatedResult<T>
        {
            Take = take,
            Skip = skip,
            Total = total,
            Data = data
        };
    }
}