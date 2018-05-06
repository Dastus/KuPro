using System.Collections.Generic;

namespace KuProStore.BL.Service
{
    public class SearchFilter
    {
        public string FilterString { get; set; } //= null;
        public string SearchOptions { get; set; } //= Constants.Options[1];
        public string TownName { get; set; }// = null;
        public int Page { get; set; } //= 1;
        public int Regions { get; set; }// = 1;
        public int UserId { get; set; } = 0;
        public bool IncludeInactive { get; set; }
    }
}