using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shared.Extensions
{
    public static class ObservableExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
        {
            return new ObservableCollection<T>(col.NotNull());
        }

        public static ReadOnlyObservableCollection<T> ToReadonlyObservableCollection<T>(this ObservableCollection<T> col)
        {
            return new ReadOnlyObservableCollection<T>(col.NotNull());
        }
    }
}
