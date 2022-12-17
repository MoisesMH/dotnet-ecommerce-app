using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers;

public class Pagination<TEntity> where TEntity : class
{
    public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<TEntity> data)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public IReadOnlyList<TEntity> Data { get; set; }
}