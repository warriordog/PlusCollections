using System.Collections.Generic;
using System.Linq;

namespace PlusCollections.Table
{
    public abstract class Table6<T1, T2, T3, T4, T5, T6, TRow> : Table5<T1, T2, T3, T4, T5, TRow>
        where TRow : Table6Row<T1, T2, T3, T4, T5, T6, TRow>
    {
        private readonly Dictionary<T6, TRow> _key6Map = new();
        public IReadOnlyDictionary<T6, TRow> Key6Map => _key6Map;
        public IEnumerable<T6> Key6Values => this.Select(row => row.Key6);
        
        public override void Add(TRow row)
        {
            // Remove any existing mappings to support change
            RemoveByKey6(row.Key6);
            _key6Map.Add(row.Key6, row);
            base.Add(row);
        }

        public override bool Remove(TRow row)
        {
            _key6Map.Remove(row.Key6);
            return base.Remove(row);
        }

        public TRow GetByKey6(T6 key) => _key6Map[key];

        public bool TryGetByKey6(T6 key, out TRow row) => _key6Map.TryGetValue(key, out row);

        public bool ContainsKey6(T6 key) => _key6Map.ContainsKey(key);
        
        public void RemoveByKey6(T6 key)
        {
            if (_key6Map.TryGetValue(key, out var row))
            {
                Remove(row);
            }
        }

        public override void Clear()
        {
            _key6Map.Clear();
            base.Clear();
        }
    }
    
    public class Table<T1, T2, T3, T4, T5, T6> : Table6<T1, T2, T3, T4, T5, T6, TableRow<T1, T2, T3, T4, T5, T6>>
    {
        public void Add(T1 key1, T2 key2, T3 key3, T4 key4, T5 key5, T6 key6)
        {
            var row = new TableRow<T1, T2, T3, T4, T5, T6>(key1, key2, key3, key4, key5, key6);
            base.Add(row);
        }
    }

    public abstract class Table6Row<T1, T2, T3, T4, T5, T6, TRow> : Table5Row<T1, T2, T3, T4, T5, TRow>
    {
        public abstract T6 Key6 { get; }
    }

    public class TableRow<T1, T2, T3, T4, T5, T6> : Table6Row<T1, T2, T3, T4, T5, T6, TableRow<T1, T2, T3, T4, T5, T6>>
    {
        public override T1 Key1 { get; }
        public override T2 Key2 { get; }
        public override T3 Key3 { get; }
        public override T4 Key4 { get; }
        public override T5 Key5 { get; }
        public override T6 Key6 { get; }
        
        
        public TableRow(T1 key1, T2 key2, T3 key3, T4 key4, T5 key5, T6 key6)
        {
            Key1 = key1;
            Key2 = key2;
            Key3 = key3;
            Key4 = key4;
            Key5 = key5;
            Key6 = key6;
        }
    }
}