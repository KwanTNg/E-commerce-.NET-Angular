using System;

namespace API.RequestHelpers;

//pagination part 2
//primary constructor
public class Pagination<T>(int PageIndex, int PageSize, int count, IReadOnlyList<T> data)
{

    public int PageIndex { get; set; } = PageIndex;
    public int PageSize { get; set; } = PageSize;
    //We need the ability to get the count of the available products after filtering has been applied but before pagination
    public int Count { get; set; } = count;
    public IReadOnlyList<T> Data { get; set; } = data;

}
