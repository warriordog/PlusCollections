using FluentAssertions;
using PlusCollections.Table;

namespace PlusCollections.Test.Table;

public abstract class Table3Tests<TTable, TRow> : Table2Tests<TTable, TRow>
    where TTable : Table3<int, string, Guid, TRow>
    where TRow : Table3Row<int, string, Guid, TRow>
{
    [Fact]
    public void Key3MapShouldIncludeAllRows()
    {
        var table = MakeHappyTable();
        var map = table.Key3Map;
        foreach (var test in TestData)
        {
            map.Should().ContainKey(test.Item3);
        }
    } 
        
    [Fact]
    public void Key3ValuesShouldIncludeAllKeys()
    {
        var expectedKeys = TestData.Select(data => data.Item3);
        var table = MakeHappyTable();
        
        var keys = table.Key3Values;

        keys.Should().BeEquivalentTo(expectedKeys);
    }
    
    [Fact]
    public void GetByKey3ShouldGetTheCorrectRow()
    {
        var table = MakeHappyTable();

        foreach (var test in TestData)
        {
            table.GetByKey3(test.Item3).Key3.Should().Be(test.Item3);
        }
    }
    
    [Fact]
    public void GetByKey3ShouldThrowIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        Assert.Throws<KeyNotFoundException>(() => table.GetByKey3(new Guid()));
    }
    
    [Fact]
    public void TryGetByKey3ShouldGetTheCorrectRow()
    {
        var expectedKeys = TestData.Select(test => test.Item3);
        var table = MakeHappyTable();

        foreach (var key in expectedKeys)
        {
            var result = table.TryGetByKey3(key, out var resultRow);
            result.Should().BeTrue();
            resultRow.Should().NotBeNull();
            resultRow.Key3.Should().Be(key);
        }
    }
    
    [Fact]
    public void TryGetByKey3ShouldReturnFalseIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        var result = table.TryGetByKey3(new Guid(), out _);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void ContainsKey3ShouldReturnTrueIfKeyIsPresent()
    {
        var expectedKeys = TestData.Select(test => test.Item3);
        var table = MakeHappyTable();

        foreach (var key in expectedKeys)
        {
            table.ContainsKey3(key).Should().BeTrue();
        }
    }

    [Fact]
    public void ContainsKey3ShouldReturnFalseIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        var result = table.ContainsKey3(new Guid());
        result.Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey3ShouldRemoveKey()
    {
        var keyToRemove = TestData[0].Item3;
        var table = MakeHappyTable();
        
        table.RemoveByKey3(keyToRemove);

        table.ContainsKey3(keyToRemove).Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey3ShouldRemoveWholeRow()
    {
        var testToRemove = TestData[0];
        var table = MakeHappyTable();
        
        table.RemoveByKey3(testToRemove.Item3);

        table.ContainsKey1(testToRemove.Item1).Should().BeFalse();
        table.ContainsKey2(testToRemove.Item2).Should().BeFalse();
        table.ContainsKey3(testToRemove.Item3).Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey3ShouldLeaveOtherRows()
    {
        var keyToRemove = TestData[0].Item3;
        var keyToKeep = TestData[1].Item3;
        var table = MakeHappyTable();
        
        table.RemoveByKey3(keyToRemove);

        table.ContainsKey3(keyToKeep).Should().BeTrue();
    }

    [Fact]
    public void RemoveByKey3ShouldNotThrowIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        
        table.RemoveByKey3(new Guid());
    }
    
    [Fact]
    public void ClearShouldRemoveAllKey3()
    {
        var table = MakeHappyTable();
        
        table.Clear();

        table.Key3Values.Should().BeEmpty();
    }
    
    [Fact]
    public void AddShouldAddByKey3()
    {
        var guid = Guid.NewGuid();
        var table = MakeEmptyTable();
        
        AddToTable(table, -1, "negative", guid);

        var row = table.GetByKey3(guid);
        row.Key1.Should().Be(-1);
        row.Key2.Should().Be("negative");
        row.Key3.Should().Be(guid);
    }
}

public class Table3Tests : Table3Tests<Table<int, string, Guid>, TableRow<int, string, Guid>>
{
    public override Table<int, string, Guid> MakeEmptyTable() => new();

    public override Table<int, string, Guid> MakeHappyTable()
    {
        var table = MakeEmptyTable();
        foreach (var data in TestData)
        {
            table.Add(data.Item1, data.Item2, data.Item3);
        }

        return table;
    }

    public override void AddToTable(Table<int, string, Guid> table, int key1, string key2, Guid key3)
    {
        table.Add(key1, key2, key3);
    }
}