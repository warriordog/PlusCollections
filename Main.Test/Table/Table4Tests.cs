using PlusCollections.Table;

namespace PlusCollections.Test.Table;

public abstract class Table4Tests<TTable, TRow> : Table3Tests<TTable, TRow>
    where TTable : Table4<int, string, Guid, int, TRow>
    where TRow : Table4Row<int, string, Guid, int, TRow>
{
    [Fact]
    public void Key4MapShouldIncludeAllRows()
    {
        var table = MakeHappyTable();
        var map = table.Key4Map;
        foreach (var test in TestData)
        {
            map.Should().ContainKey(test.Item4);
        }
    } 
        
    [Fact]
    public void Key4ValuesShouldIncludeAllKeys()
    {
        var expectedKeys = TestData.Select(data => data.Item4);
        var table = MakeHappyTable();
        
        var keys = table.Key4Values;

        keys.Should().BeEquivalentTo(expectedKeys);
    }
    
    [Fact]
    public void GetByKey4ShouldGetTheCorrectRow()
    {
        var table = MakeHappyTable();

        foreach (var test in TestData)
        {
            table.GetByKey4(test.Item4).Key4.Should().Be(test.Item4);
        }
    }
    
    [Fact]
    public void GetByKey4ShouldThrowIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        Assert.Throws<KeyNotFoundException>(() => table.GetByKey4(0));
    }
    
    [Fact]
    public void TryGetByKey4ShouldGetTheCorrectRow()
    {
        var expectedKeys = TestData.Select(test => test.Item4);
        var table = MakeHappyTable();

        foreach (var key in expectedKeys)
        {
            var result = table.TryGetByKey4(key, out var resultRow);
            result.Should().BeTrue();
            resultRow.Should().NotBeNull();
            resultRow.Key4.Should().Be(key);
        }
    }
    
    [Fact]
    public void TryGetByKey4ShouldReturnFalseIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        var result = table.TryGetByKey4(0, out _);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void ContainsKey4ShouldReturnTrueIfKeyIsPresent()
    {
        var expectedKeys = TestData.Select(test => test.Item4);
        var table = MakeHappyTable();

        foreach (var key in expectedKeys)
        {
            table.ContainsKey4(key).Should().BeTrue();
        }
    }

    [Fact]
    public void ContainsKey4ShouldReturnFalseIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        var result = table.ContainsKey4(0);
        result.Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey4ShouldRemoveKey()
    {
        var keyToRemove = TestData[0].Item4;
        var table = MakeHappyTable();
        
        table.RemoveByKey4(keyToRemove);

        table.ContainsKey4(keyToRemove).Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey4ShouldRemoveWholeRow()
    {
        var testToRemove = TestData[0];
        var table = MakeHappyTable();
        
        table.RemoveByKey4(testToRemove.Item4);

        table.ContainsKey1(testToRemove.Item1).Should().BeFalse();
        table.ContainsKey2(testToRemove.Item2).Should().BeFalse();
        table.ContainsKey3(testToRemove.Item3).Should().BeFalse();
        table.ContainsKey4(testToRemove.Item4).Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey4ShouldLeaveOtherRows()
    {
        var keyToRemove = TestData[0].Item4;
        var keyToKeep = TestData[1].Item4;
        var table = MakeHappyTable();
        
        table.RemoveByKey4(keyToRemove);

        table.ContainsKey4(keyToKeep).Should().BeTrue();
    }

    [Fact]
    public void RemoveByKey4ShouldNotThrowIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        
        table.RemoveByKey4(0);
    }
    
    [Fact]
    public void ClearShouldRemoveAllKey4()
    {
        var table = MakeHappyTable();
        
        table.Clear();

        table.Key4Values.Should().BeEmpty();
    }
    
    [Fact]
    public void AddShouldAddByKey4()
    {
        var guid = Guid.NewGuid();
        var table = MakeEmptyTable();
        
        AddToTable(table, -1, "negative", guid, 1);

        var row = table.GetByKey4(1);
        row.Key1.Should().Be(-1);
        row.Key2.Should().Be("negative");
        row.Key3.Should().Be(guid);
        row.Key4.Should().Be(1);
    }
}

public class Table4Tests : Table4Tests<Table<int, string, Guid, int>, TableRow<int, string, Guid, int>>
{
    public override Table<int, string, Guid, int> MakeEmptyTable() => new();

    public override Table<int, string, Guid, int> MakeHappyTable()
    {
        var table = MakeEmptyTable();
        foreach (var data in TestData)
        {
            table.Add(data.Item1, data.Item2, data.Item3, data.Item4);
        }

        return table;
    }

    public override void AddToTable(Table<int, string, Guid, int> table, int key1, string key2, Guid key3, int key4, float key5, object key6)
    {
        table.Add(key1, key2, key3, key4);
    }
}