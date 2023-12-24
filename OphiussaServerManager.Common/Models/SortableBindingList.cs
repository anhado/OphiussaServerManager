using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

public class MySortableBindingList<T> : BindingList<T> {
    // a cache of functions that perform the sorting
    // for a given type, property, and sort direction
    private static readonly Dictionary<string, Func<List<T>, IEnumerable<T>>>
        _cachedOrderByExpressions = new Dictionary<string, Func<List<T>,
            IEnumerable<T>>>();

    // function that refereshes the contents
    // of the base classes collection of elements
    private readonly Action<MySortableBindingList<T>, List<T>>
        _populateBaseList = (a, b) => a.ResetItems(b);

    // reference to the list provided at the time of instantiation
    private List<T> _originalList;

    private ListSortDirection  _sortDirection;
    private PropertyDescriptor _sortProperty;

    public MySortableBindingList() {
        _originalList = new List<T>();
    }

    public MySortableBindingList(IEnumerable<T> enumerable) {
        _originalList = enumerable.ToList();
        _populateBaseList(this, _originalList);
    }

    public MySortableBindingList(List<T> list) {
        _originalList = list;
        _populateBaseList(this, _originalList);
    }

    protected override bool SupportsSortingCore =>
        // indeed we do
        true;

    protected override ListSortDirection SortDirectionCore => _sortDirection;

    protected override PropertyDescriptor SortPropertyCore => _sortProperty;

    protected override void ApplySortCore(PropertyDescriptor prop,
                                          ListSortDirection  direction) {
        /*
         Look for an appropriate sort method in the cache if not found .
         Call CreateOrderByMethod to create one.
         Apply it to the original list.
         Notify any bound controls that the sort has been applied.
         */

        _sortProperty = prop;

        string orderByMethodName = _sortDirection ==
                                   ListSortDirection.Ascending
                                       ? "OrderBy"
                                       : "OrderByDescending";
        string cacheKey = typeof(T).GUID + prop.Name + orderByMethodName;

        if (!_cachedOrderByExpressions.ContainsKey(cacheKey)) CreateOrderByMethod(prop, orderByMethodName, cacheKey);

        ResetItems(_cachedOrderByExpressions[cacheKey](_originalList).ToList());
        ResetBindings();
        _sortDirection = _sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
    }


    private void CreateOrderByMethod(PropertyDescriptor prop,
                                     string             orderByMethodName, string cacheKey) {
        /*
         Create a generic method implementation for IEnumerable<T>.
         Cache it.
        */

        var sourceParameter = Expression.Parameter(typeof(List<T>), "source");
        var lambdaParameter = Expression.Parameter(typeof(T),       "lambdaParameter");
        var accesedMember   = typeof(T).GetProperty(prop.Name);
        var propertySelectorLambda =
            Expression.Lambda(Expression.MakeMemberAccess(lambdaParameter,
                                                          accesedMember), lambdaParameter);
        var orderByMethod = typeof(Enumerable).GetMethods()
                                              .Where(a => a.Name                   == orderByMethodName &&
                                                          a.GetParameters().Length == 2)
                                              .Single()
                                              .MakeGenericMethod(typeof(T), prop.PropertyType);

        var orderByExpression = Expression.Lambda<Func<List<T>, IEnumerable<T>>>(
                                                                                 Expression.Call(orderByMethod,
                                                                                                 new Expression[] {
                                                                                                                      sourceParameter,
                                                                                                                      propertySelectorLambda
                                                                                                                  }),
                                                                                 sourceParameter);

        _cachedOrderByExpressions.Add(cacheKey, orderByExpression.Compile());
    }

    protected override void RemoveSortCore() {
        ResetItems(_originalList);
    }

    private void ResetItems(List<T> items) {
        base.ClearItems();

        for (int i = 0; i < items.Count; i++) base.InsertItem(i, items[i]);
    }

    protected override void OnListChanged(ListChangedEventArgs e) {
        _originalList = Items.ToList();
    }
}

public static class EnumerableExtensions {
    public static BindingList<T> ToSortableBindingList<T>(this IEnumerable<T> source) {
        return new MySortableBindingList<T>(source.ToList());
    }
}