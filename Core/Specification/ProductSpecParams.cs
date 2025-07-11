﻿namespace Core.Specification
{
    public class ProductSpecParams
    {
        public const int MaxPageSize = 100;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }
        private string? _search;

        public string? Search
        {
            get { return _search?.ToLower(); }
            set { _search = value; }
        }

    }
}
