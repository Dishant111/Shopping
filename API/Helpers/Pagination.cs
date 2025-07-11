﻿using API.Dtos;
using Core.Entities;

namespace API.Helpers
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageIndex, int pageSize, int totalItem, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            this.Count = totalItem;
            this.Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
