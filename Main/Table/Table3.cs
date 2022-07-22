using System.Collections.Generic;
using System.Linq;

namespace PlusCollections.Table
{
    public abstract class Table3<T1, T2, T3, TRow> : Table2<T1, T2, TRow>
        where TRow : Table3Row<T1, T2, T3, TRow>
    {
        private readonly Dictionary<T3, TRow> _key3Map = new();
        public IReadOnlyDictionary<T3, TRow> Key3Map => _key3Map;
        public IEnumerable<T3> Key3Values => this.Select(row => row.Key3);
        
        public override void Add(TRow row)
        {
            // Remove any existing mappings to support change
            RemoveByKey3(row.Key3);
            _key3Map.Add(row.Key3, row);
            base.Add(row);
        }

        public override bool Remove(TRow row)
        {
            _key3Map.Remove(row.Key3);
            return base.Remove(row);
        }

        public TRow GetByKey3(T3 key) => _key3Map[key];

        public bool TryGetByKey3(T3 key, out TRow row) => _key3Map.TryGetValue(key, out row);

        public bool ContainsKey3(T3 key) => _key3Map.ContainsKey(key);
        
        public void RemoveByKey3(T3 key)
        {
            if (_key3Map.TryGetValue(key, out var row))
            {
                Remove(row);
            }
        }

        public override void Clear()
        {
            _key3Map.Clear();
            base.Clear();
        }
    }

    public class Table<T1, T2, T3>:  Table3<T1, T2, T3, TableRow<T1, T2, T3>>
    {
        public void Add(T1 key1, T2 key2, T3 key3)
        {
            var row = new TableRow<T1, T2, T3>(key1, key2, key3);
            base.Add(row);
        }
    }

    public abstract class Table3Row<T1, T2, T3, TRow> : Table2Row<T1, T2, TRow>
    {
        public abstract T3 Key3 { get; }
    }

    public class TableRow<T1, T2, T3> : Table3Row<T1, T2, T3, TableRow<T1, T2, T3>>
    {
        public override T1 Key1 { get; }
        public override T2 Key2 { get; }
        public override T3 Key3 { get; }
        
        public TableRow(T1 key1, T2 key2, T3 key3)
        {
            Key1 = key1;
            Key2 = key2;
            Key3 = key3;
        }
    }
}