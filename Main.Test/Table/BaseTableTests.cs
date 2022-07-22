using FluentAssertions;
using PlusCollections.Table;

namespace PlusCollections.Test.Table;

public abstract class BaseTableTests<TTable, TRow>
    where TTable : BaseTable<TRow>
    where TRow : TableRow<TRow>
{
    public List<Tuple<int, string, Guid>> TestData { get; } = new()
    {
        new(1, "one", Guid.Parse("060a2563-22a9-4dff-bdff-0c3fbf72cdf6")),
        new(2, "two", Guid.Parse("9dd3204f-73da-4ba5-af7c-34d736b4429d"))
    };
    
    public abstract TTable MakeEmptyTable();
    public abstract TTable MakeHappyTable();

    public abstract void AddToTable(TTable table, int key1, string key2, Guid key3);

    [Fact]
    public void ClearShouldEmptyTheTable()
    {
        var table = MakeHappyTable();
        table.Clear();
        table.Should().BeEmpty();
    }

    [Fact]
    public void GetEnumeratorShouldReturnAllRows()
    {
        var table = MakeHappyTable();
        var contents = table.ToList(); // This calls GetEnumerator() internally
        contents.Should().HaveCount(TestData.Count);
    }

    [Fact]
    public void GetEnumeratorShouldReturnNothingWhenEmpty()
    {
        var table = MakeEmptyTable();
        var contents = table.ToList();
        contents.Should().BeEmpty();
    }
}