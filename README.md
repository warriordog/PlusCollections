# PlusCollections
PlusCollections is a set of extra data structures for .NET.

### <ins>Table - Order-preserving bidirectional multi-key map</ins>
Table is an implementation of a multiple-key bidirectional map.
Unlike `Dictionary` and other traditional key-value maps, Table links two or more keys together.
Groups of related keys are inserted as a "row" that can accessed by any key.
Insertion order is preserved and respected for all key columns.
Overloaded, generic implementations exist for 2, 3, 4, 5, and 6 key tables.
More keys can be trivially added, but single-key tables are not supported (just use an ordered set).

#### Performance
* Table operates in constant time (except for iteration), but constant overhead scales linearly based on the number of keys per row.
For example, `Table<Key1, Key2>.Add()` will require a bit more than twice as much time as `Dictionary<Key, Value>.Add()`.
`Table<Key1, Key2, Key3>.Add()` will take three times as long, and so on.
* Insert / Delete complexity - `O(K)` where `K` is the number of keys supported by the implementation.
* Update complexity - `O(K^2)` where `K` is the number of keys supported by the implementation.
In cases where only one key is changed, complexity returns to `O(K)`.
* Lookup complexity - `O(1)` (equal to `Dictionary[key]`)
* Iteration complexity - `O(n)` (equal to walking a linked list)

#### Implementations

| Namespace | Class | Description |
| --- | --- | --- |
| `PlusCollections.Table` | `Table<TKey1, TKey2>` | Two-key implementation |
| `PlusCollections.Table` | `Table<TKey1, TKey2, TKey3>` | Three-key implementation |
| `PlusCollections.Table` | `Table<TKey1, TKey2, TKey3, TKey4>` | Four-key implementation |
| `PlusCollections.Table` | `Table<TKey1, TKey2, TKey3, TKey4, TKey5>` | Five-key implementation |
| `PlusCollections.Table` | `Table<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>` | Six-key implementation |
