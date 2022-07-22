using System.Collections.Generic;
using System.Linq;

namespace PlusCollections.Table
{
    public abstract class Table2<T1, T2, TRow> : BaseTable<TRow>
        where TRow : Table2Row<T1, T2, TRow>
    {
        private readonly Dictionary<T1, TRow> _key1Map = new();
        public IReadOnlyDictionary<T1, TRow> Key1Map => _key1Map;
        public IEnumerable<T1> Key1Values => this.Select(row => row.Key1);

        private readonly Dictionary<T2, TRow> _key2Map = new();
        public IReadOnlyDictionary<T2, TRow> Key2Map => _key2Map;
        public IEnumerable<T2> Key2Values => this.Select(row => row.Key2);
        
        public override void Add(TRow row)
        {
            // Remove any existing mappings to support change
            RemoveByKey1(row.Key1);
            RemoveByKey2(row.Key2);
            
            _key1Map.Add(row.Key1, row);
            _key2Map.Add(row.Key2, row);
            
            base.Add(row);
        }

        public override bool Remove(TRow row)
        {
            _key1Map.Remove(row.Key1);
            _key2Map.Remove(row.Key2);
            return base.Remove(row);
        }

        public TRow GetByKey1(T1 key) => _key1Map[key];
        public TRow GetByKey2(T2 key) => _key2Map[key];

        public bool TryGetByKey1(T1 key, out TRow row) => _key1Map.TryGetValue(key, out row);
        public bool TryGetByKey2(T2 key, out TRow row) => _key2Map.TryGetValue(key, out row);

        public bool ContainsKey1(T1 key) => _key1Map.ContainsKey(key);
        public bool ContainsKey2(T2 key) => _key2Map.ContainsKey(key);

        public void RemoveByKey1(T1 key)
        {
            if (_key1Map.TryGetValue(key, out var row))
            {
                Remove(row);
            }
        }
        public void RemoveByKey2(T2 key)
        {
            if (_key2Map.TryGetValue(key, out var row))
            {
                Remove(row);
            }
        }

        public override void Clear()
        {
            _key1Map.Clear();
            _key2Map.Clear();
            base.Clear();
        }
    }

    public class Table<T1, T2> : Table2<T1, T2, TableRow<T1, T2>>
    {
        public void Add(T1 key1, T2 key2)
        {
            var row = new TableRow<T1, T2>(key1, key2);
            base.Add(row);
        }
    }

    public abstract class Table2Row<T1, T2, TRow> : TableRow<TRow>
    {
        public abstract T1 Key1 { get; }
        public abstract T2 Key2 { get; }
    }

    public class TableRow<T1, T2> : Table2Row<T1, T2, TableRow<T1, T2>>
    {
        public override T1 Key1 { get; }
        public override T2 Key2 { get; }
        
        public TableRow(T1 key1, T2 key2)
        {
            Key1 = key1;
            Key2 = key2;
        }
    }
}