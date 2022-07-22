using System.Collections.Generic;
using System.Linq;

namespace PlusCollections.Table
{
    public abstract class Table4<T1, T2, T3, T4, TRow> : Table3<T1, T2, T3, TRow>
        where TRow : Table4Row<T1, T2, T3, T4, TRow>
    {
        private readonly Dictionary<T4, TRow> _key4Map = new();
        public IReadOnlyDictionary<T4, TRow> Key4Map => _key4Map;
        public IEnumerable<T4> Key4Values => this.Select(row => row.Key4);
        
        protected override void Add(TRow row)
        {
            // Remove any existing mappings to support change
            RemoveByKey4(row.Key4);
            _key4Map.Add(row.Key4, row);
            base.Add(row);
        }

        protected override void Remove(TRow row)
        {
            _key4Map.Remove(row.Key4);
            base.Remove(row);
        }

        public TRow GetByKey4(T4 key) => _key4Map[key];

        public bool TryGetByKey4(T4 key, out TRow row) => _key4Map.TryGetValue(key, out row);

        public bool ContainsKey4(T4 key) => _key4Map.ContainsKey(key);
        
        public void RemoveByKey4(T4 key)
        {
            if (_key4Map.TryGetValue(key, out var row))
            {
                Remove(row);
            }
        }

        public override void Clear()
        {
            _key4Map.Clear();
            base.Clear();
        }
    }
    
    public class Table<T1, T2, T3, T4> : Table4<T1, T2, T3, T4, TableRow<T1, T2, T3, T4>>
    {
        public void Add(T1 key1, T2 key2, T3 key3, T4 key4)
        {
            var row = new TableRow<T1, T2, T3, T4>(key1, key2, key3, key4);
            base.Add(row);
        }
    }

    public abstract class Table4Row<T1, T2, T3, T4, TRow> : Table3Row<T1, T2, T3, TRow>
    {
        public abstract T4 Key4 { get; }
    }

    public class TableRow<T1, T2, T3, T4> : Table4Row<T1, T2, T3, T4, TableRow<T1, T2, T3, T4>>
    {
        public override T1 Key1 { get; }
        public override T2 Key2 { get; }
        public override T3 Key3 { get; }
        public override T4 Key4 { get; }
        
        public TableRow(T1 key1, T2 key2, T3 key3, T4 key4)
        {
            Key1 = key1;
            Key2 = key2;
            Key3 = key3;
            Key4 = key4;
        }

    }
}