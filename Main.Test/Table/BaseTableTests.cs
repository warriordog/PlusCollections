using PlusCollections.Table;

namespace PlusCollections.Test.Table;

public abstract class BaseTableTests<TTable, TRow>
    where TTable : BaseTable<TRow>
    where TRow : TableRow<TRow>
{
    public List<Tuple<int, string, Guid, int, float, object>> TestData { get; } = new()
    {
        new(1, "one", Guid.Parse("060a2563-22a9-4dff-bdff-0c3fbf72cdf6"), -1, 1.0f, new object()),
        new(2, "two", Guid.Parse("9dd3204f-73da-4ba5-af7c-34d736b4429d"), -2, 2.0f, new object())
    };
    
    public abstract TTable MakeEmptyTable();
    public abstract TTable MakeHappyTable();

    public abstract void AddToTable(TTable table, int key1, string key2, Guid key3, int key4, float key5, object key6);

    protected void AddToTable(TTable table, int key1, string key2, Guid key3 = new(), int? key4 = null, float? key5 = null, object? key6 = null)
    {
        key4 ??= key1 * -1;
        key5 ??= key1;
        key6 ??= new object();
        AddToTable(table, key1, key2, key3, (int)key4, (float)key5, key6);
    }

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

    [Fact]
    public void IsReadOnlyShouldBeFalse()
    {
        var table = MakeHappyTable();
        table.IsReadOnly.Should().BeFalse();
    }

    [Fact]
    public void ContainsShouldReturnTrueForAllRows()
    {
        var table = MakeHappyTable();

        foreach (var row in table)
        {
            table.Contains(row).Should().BeTrue();
        }
    }

    [Fact]
    public void ContainsShouldReturnFalseAfterRowIsRemoved()
    {
        var table = MakeHappyTable();
        var removedRows = table.ToList();
        table.Clear();

        foreach (var row in removedRows)
        {
            table.Contains(row).Should().BeFalse();
        }
    }

    [Fact]
    public void RemoveShouldReturnTrueIfRowIsInTable()
    {
        var table = MakeHappyTable();
        var rows = table.ToList();

        foreach (var row in rows)
        {
            var result = table.Remove(row);
            result.Should().BeTrue();
        }
    }

    [Fact]
    public void RemoveShouldReturnFalseIfRowIsNotInTable()
    {
        var table = MakeHappyTable();
        var rows = table.ToList();
        table.Clear();
        
        foreach (var row in rows)
        {
            var result = table.Remove(row);
            result.Should().BeFalse();
        }
    }

    [Fact]
    public void CountShouldReturnTheSizeOfTheTable()
    {
        var expectedCount = TestData.Count;
        var table = MakeHappyTable();
        var count = table.Count;
        count.Should().Be(expectedCount);
    }

    [Fact]
    public void AddShouldSkipIfRowIsAlreadyAdded()
    {
        var table = MakeHappyTable();
        var initialRows = table.ToList();

        foreach (var row in initialRows)
        {
            table.Add(row);
        }

        var actualRows = table.ToList();
        actualRows.Should().BeEquivalentTo(initialRows);
    }
}