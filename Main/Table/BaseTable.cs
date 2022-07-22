using System.Collections;
using System.Collections.Generic;

namespace PlusCollections.Table
{
    public abstract class BaseTable<TRow> : ICollection<TRow>
        where TRow : TableRow<TRow>
    {
        private readonly LinkedList<TRow> _rowIndex = new();

        public virtual void Add(TRow row)
        {
            if (row.IndexNode == null)
            {
                var node = _rowIndex.AddLast(row);
                row.IndexNode = node;
            }
        }

        public virtual bool Remove(TRow row)
        {
            if (row.IndexNode == null) return false;
            
            _rowIndex.Remove(row.IndexNode);
            row.IndexNode = null;
            return true;
        }

        public bool Contains(TRow item) => item.IndexNode?.List == _rowIndex;
        public virtual void Clear() => _rowIndex.Clear();

        
        public void CopyTo(TRow[] array, int arrayIndex) => _rowIndex.CopyTo(array, arrayIndex);
        public int Count => _rowIndex.Count;
        public bool IsReadOnly => false;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<TRow> GetEnumerator() => _rowIndex.GetEnumerator();
    }

    public class TableRow<TRow>
    {
        internal LinkedListNode<TRow>? IndexNode;
    }
}