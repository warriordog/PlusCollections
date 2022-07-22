using FluentAssertions;
using PlusCollections.Table;

namespace PlusCollections.Test.Table;

public abstract class Table6Tests<TTable, TRow> : Table5Tests<TTable, TRow>
    where TTable : Table6<int, string, Guid, int, float, object, TRow>
    where TRow : Table6Row<int, string, Guid, int, float, object, TRow>
{
    [Fact]
    public void Key6MapShouldIncludeAllRows()
    {
        var table = MakeHappyTable();
        var map = table.Key6Map;
        foreach (var test in TestData)
        {
            map.Should().ContainKey(test.Item6);
        }
    } 
        
    [Fact]
    public void Key6ValuesShouldIncludeAllKeys()
    {
        var expectedKeys = TestData.Select(data => data.Item6);
        var table = MakeHappyTable();
        
        var keys = table.Key6Values;

        keys.Should().BeEquivalentTo(expectedKeys);
    }
    
    [Fact]
    public void GetByKey6ShouldGetTheCorrectRow()
    {
        var table = MakeHappyTable();

        foreach (var test in TestData)
        {
            table.GetByKey6(test.Item6).Key6.Should().Be(test.Item6);
        }
    }
    
    [Fact]
    public void GetByKey6ShouldThrowIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        Assert.Throws<KeyNotFoundException>(() => table.GetByKey6(new object()));
    }
    
    [Fact]
    public void TryGetByKey6ShouldGetTheCorrectRow()
    {
        var expectedKeys = TestData.Select(test => test.Item6);
        var table = MakeHappyTable();

        foreach (var key in expectedKeys)
        {
            var result = table.TryGetByKey6(key, out var resultRow);
            result.Should().BeTrue();
            resultRow.Should().NotBeNull();
            resultRow.Key6.Should().Be(key);
        }
    }
    
    [Fact]
    public void TryGetByKey6ShouldReturnFalseIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        var result = table.TryGetByKey6(new object(), out _);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void ContainsKey6ShouldReturnTrueIfKeyIsPresent()
    {
        var expectedKeys = TestData.Select(test => test.Item6);
        var table = MakeHappyTable();

        foreach (var key in expectedKeys)
        {
            table.ContainsKey6(key).Should().BeTrue();
        }
    }

    [Fact]
    public void ContainsKey6ShouldReturnFalseIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        var result = table.ContainsKey6(new object());
        result.Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey6ShouldRemoveKey()
    {
        var keyToRemove = TestData[0].Item6;
        var table = MakeHappyTable();
        
        table.RemoveByKey6(keyToRemove);

        table.ContainsKey6(keyToRemove).Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey6ShouldRemoveWholeRow()
    {
        var testToRemove = TestData[0];
        var table = MakeHappyTable();
        
        table.RemoveByKey6(testToRemove.Item6);

        table.ContainsKey1(testToRemove.Item1).Should().BeFalse();
        table.ContainsKey2(testToRemove.Item2).Should().BeFalse();
        table.ContainsKey3(testToRemove.Item3).Should().BeFalse();
        table.ContainsKey4(testToRemove.Item4).Should().BeFalse();
        table.ContainsKey5(testToRemove.Item5).Should().BeFalse();
        table.ContainsKey6(testToRemove.Item6).Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey6ShouldLeaveOtherRows()
    {
        var keyToRemove = TestData[0].Item6;
        var keyToKeep = TestData[1].Item6;
        var table = MakeHappyTable();
        
        table.RemoveByKey6(keyToRemove);

        table.ContainsKey6(keyToKeep).Should().BeTrue();
    }

    [Fact]
    public void RemoveByKey6ShouldNotThrowIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        
        table.RemoveByKey6(new object());
    }
    
    [Fact]
    public void ClearShouldRemoveAllKey6()
    {
        var table = MakeHappyTable();
        
        table.Clear();

        table.Key6Values.Should().BeEmpty();
    }
    
    [Fact]
    public void AddShouldAddByKey6()
    {
        var guid = Guid.NewGuid();
        var obj = new object();
        var table = MakeEmptyTable();
        
        AddToTable(table, -1, "negative", guid, 1, 1.0f, obj);

        var row = table.GetByKey6(obj);
        row.Key1.Should().Be(-1);
        row.Key2.Should().Be("negative");
        row.Key3.Should().Be(guid);
        row.Key4.Should().Be(1);
        row.Key5.Should().Be(1.0f);
        row.Key6.Should().Be(obj);
    }
}

public class Table6Tests : Table6Tests<Table<int, string, Guid, int, float, object>, TableRow<int, string, Guid, int, float, object>>
{
    public override Table<int, string, Guid, int, float, object> MakeEmptyTable() => new();

    public override Table<int, string, Guid, int, float, object> MakeHappyTable()
    {
        var table = MakeEmptyTable();
        foreach (var data in TestData)
        {
            table.Add(data.Item1, data.Item2, data.Item3, data.Item4, data.Item5, data.Item6);
        }

        return table;
    }

    public override void AddToTable(Table<int, string, Guid, int, float, object> table, int key1, string key2, Guid key3, int key4, float key5, object key6)
    {
        table.Add(key1, key2, key3, key4, key5, key6);
    }
}