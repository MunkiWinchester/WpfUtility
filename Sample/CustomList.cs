using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sample
{
    public class CustomList<T> : ICollection<T>
    {
        private readonly ICollection<T> _items;

        public CustomList()
        {
            _items = new List<T>();
        }

        public CustomList(ICollection<T> collection)
        {
            _items = collection;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public void Add(T item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        public int Count => _items.Count;
        public bool IsReadOnly => false;
        

        public void AddRange(ICollection<T> collection)
        {
            foreach (var item in collection)
            {
                _items.Add(item);
            }
        }

        public override string ToString()
        {
            var baseValue = ConnectedString(_items);
            return baseValue;
        }

        public static string ConnectedString(object value)
        {
            var list = value as List<T>;
            var connectedString = "";
            if (list != null && list.Any())
            {
                connectedString = list.Aggregate(connectedString, (current, entry) => current + $"{entry}; ");
                connectedString = connectedString.TrimEnd(' ', ';');
            }
            return connectedString;
        }
    }
}