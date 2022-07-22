using System.Collections;
using System.Collections.Generic;

namespace PlusCollections.Table
{
    public abstract class BaseTable<TRow> : IEnumerable<TRow>
        where TRow : TableRow<TRow>
    {
        private readonly LinkedList<TRow> _rowIndex = new();

        protected virtual void Add(TRow row)
        {
            if (row.IndexNode == null)
            {
                var node = _rowIndex.AddLast(row);
                row.IndexNode = node;
            }
        }

        protected virtual void Remove(TRow row)
        {
            if (row.IndexNode != null)
            {
                _rowIndex.Remove(row.IndexNode);
                row.IndexNode = null;
            }
        }

        public virtual void Clear()
        {
            _rowIndex.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<TRow> GetEnumerator() => _rowIndex.GetEnumerator();
    }

    public class TableRow<TRow>
    {
        internal LinkedListNode<TRow>? IndexNode;
    }
}