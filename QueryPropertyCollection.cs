using System.Collections;

namespace Loxifi
{
    internal class QueryPropertyCollection : IEnumerable<QueryProperty>, IDictionary<string, QueryProperty>
    {
        private readonly List<QueryProperty> _backing = new();

        public int Count => _backing.Count;

        public bool IsReadOnly => false;

        public ICollection<string> Keys => _backing.Select(q => q.Name).ToList();

        public ICollection<QueryProperty> Values => _backing;

        public QueryProperty this[string key]
        {
            get => _backing.SingleOrDefault(p => p.Name == key);
            set
            {
                if (!ContainsKey(key))
                {
                    throw new KeyNotFoundException($"Query Property with name '{key}' not found");
                }

                _ = Remove(key);
                Add(key, value);
            }
        }

        public void Add(string key, QueryProperty value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (key != value.Name)
            {
                throw new ArgumentException("Can not add query property where name does not match value.Name");
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Add(value);
        }

        public void Add(QueryProperty value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (ContainsKey(value.Name))
            {
                throw new ArgumentException("Query property with given name exists", nameof(value));
            }

            _backing.Add(value);
        }

        public void Add(KeyValuePair<string, QueryProperty> item) => Add(item.Key, item.Value);

        public void Clear() => _backing.Clear();

        public bool Contains(KeyValuePair<string, QueryProperty> item) => ContainsKey(item.Key);

        public bool ContainsKey(string key) => _backing.Any(p => p.Name == key);

        public void CopyTo(KeyValuePair<string, QueryProperty>[] array, int arrayIndex) => throw new NotImplementedException();

        public IEnumerator<QueryProperty> GetEnumerator() => _backing.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<KeyValuePair<string, QueryProperty>> IEnumerable<KeyValuePair<string, QueryProperty>>.GetEnumerator() => _backing.ToDictionary(k => k.Name, v => v).GetEnumerator();

        public bool Remove(string key) => _backing.Remove(_backing.SingleOrDefault(p => p.Name == key));

        public bool Remove(KeyValuePair<string, QueryProperty> item) => Remove(item.Value.Name);

        public bool TryGetValue(string key, out QueryProperty value)
        {
            value = _backing.FirstOrDefault(p => p.Name == key);

            return value != null;
        }
    }
}