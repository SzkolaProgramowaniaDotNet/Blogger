using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Filters
{
    public class SortingFilter
    {
        public string SortField { get; set; }
        public bool Ascending { get; set; } = true;

        public SortingFilter()
        {
            SortField = "Id";
        }

        public SortingFilter(string sortField, bool ascending)
        {
            var sortFields = SortingHelper.GetSortFields();

            sortField = sortField.ToLower();

            if (sortFields.Select(x => x.Key).Contains(sortField.ToLower()))
                sortField = sortFields.Where(x => x.Key == sortField).Select(x => x.Value).SingleOrDefault();
            else
                sortField = "Id";

            SortField = sortField;
            Ascending = ascending;
        }
    }
}
