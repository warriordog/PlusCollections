using System.Collections.Generic;
using System.Linq;

namespace PlusCollections.Table
{
    public abstract class Table5<T1, T2, T3, T4, T5, TRow> : Table4<T1, T2, T3, T4, TRow>
        where TRow : Table5Row<T1, T2, T3, T4, T5, TRow>
    {
        private readonly Dictionary<T5, TRow> _key5Map = new();
        public IReadOnlyDictionary<T5, TRow> Key5Map => _key5Map;
        public IEnumerable<T5> Key5Values => this.Select(row => row.Key5);
        
        public override void Add(TRow row)
        {
            // Remove any existing mappings to support change
            RemoveByKey5(row.Key5);
            _key5Map.Add(row.Key5, row);
            base.Add(row);
        }

        public override bool Remove(TRow row)
        {
            _key5Map.Remove(row.Key5);
            return base.Remove(row);
        }

        public TRow GetByKey5(T5 key) => _key5Map[key];

        public bool TryGetByKey5(T5 key, out TRow row) => _key5Map.TryGetValue(key, out row);

        public bool ContainsKey5(T5 key) => _key5Map.ContainsKey(key);
        
        public void RemoveByKey5(T5 key)
        {
            if (_key5Map.TryGetValue(key, out var row))
            {
                Remove(row);
            }
        }

        public override void Clear()
        {
            _key5Map.Clear();
            base.Clear();
        }
    }
    
    public class Table<T1, T2, T3, T4, T5> : Table5<T1, T2, T3, T4, T5, TableRow<T1, T2, T3, T4, T5>>
    {
        public void Add(T1 key1, T2 key2, T3 key3, T4 key4, T5 key5)
        {
            var row = new TableRow<T1, T2, T3, T4, T5>(key1, key2, key3, key4, key5);
            base.Add(row);
        }
    }

    public abstract class Table5Row<T1, T2, T3, T4, T5, TRow> : Table4Row<T1, T2, T3, T4, TRow>
    {
        public abstract T5 Key5 { get; }
    }

    public class TableRow<T1, T2, T3, T4, T5> : Table5Row<T1, T2, T3, T4, T5, TableRow<T1, T2, T3, T4, T5>>
    {
        public override T1 Key1 { get; }
        public override T2 Key2 { get; }
        public override T3 Key3 { get; }
        public override T4 Key4 { get; }
        public override T5 Key5 { get; }
        
        public TableRow(T1 key1, T2 key2, T3 key3, T4 key4, T5 key5)
        {
            Key1 = key1;
            Key2 = key2;
            Key3 = key3;
            Key4 = key4;
            Key5 = key5;
        }
    }
}