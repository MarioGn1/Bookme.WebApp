using Bookme.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bookme.Test.Data
{
    public static class Category
    {
        public static IEnumerable<ServiceCategory> SixCategories
            => Enumerable.Range(0, 10).Select(c => new ServiceCategory());
    }
}
