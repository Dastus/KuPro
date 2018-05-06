using System;

namespace KuProStore.UI
{
    public class PagingInfo
    {
        public int Total { get; set; }
        public int ProductsOnPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)Total/ ProductsOnPage); }
        }
    }
}