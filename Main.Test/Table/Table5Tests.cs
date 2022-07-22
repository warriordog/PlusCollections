using FluentAssertions;
using PlusCollections.Table;

namespace PlusCollections.Test.Table;

public abstract class Table5Tests<TTable, TRow> : Table4Tests<TTable, TRow>
    where TTable : Table5<int, string, Guid, int, float, TRow>
    where TRow : Table5Row<int, string, Guid, int, float, TRow>
{
    [Fact]
    public void Key5MapShouldIncludeAllRows()
    {
        var table = MakeHappyTable();
        var map = table.Key5Map;
        foreach (var test in TestData)
        {
            map.Should().ContainKey(test.Item5);
        }
    } 
        
    [Fact]
    public void Key5ValuesShouldIncludeAllKeys()
    {
        var expectedKeys = TestData.Select(data => data.Item5);
        var table = MakeHappyTable();
        
        var keys = table.Key5Values;

        keys.Should().BeEquivalentTo(expectedKeys);
    }
    
    [Fact]
    public void GetByKey5ShouldGetTheCorrectRow()
    {
        var table = MakeHappyTable();

        foreach (var test in TestData)
        {
            table.GetByKey5(test.Item5).Key5.Should().Be(test.Item5);
        }
    }
    
    [Fact]
    public void GetByKey5ShouldThrowIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        Assert.Throws<KeyNotFoundException>(() => table.GetByKey5(0.0f));
    }
    
    [Fact]
    public void TryGetByKey5ShouldGetTheCorrectRow()
    {
        var expectedKeys = TestData.Select(test => test.Item5);
        var table = MakeHappyTable();

        foreach (var key in expectedKeys)
        {
            var result = table.TryGetByKey5(key, out var resultRow);
            result.Should().BeTrue();
            resultRow.Should().NotBeNull();
            resultRow.Key5.Should().Be(key);
        }
    }
    
    [Fact]
    public void TryGetByKey5ShouldReturnFalseIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        var result = table.TryGetByKey5(0.0f, out _);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void ContainsKey5ShouldReturnTrueIfKeyIsPresent()
    {
        var expectedKeys = TestData.Select(test => test.Item5);
        var table = MakeHappyTable();

        foreach (var key in expectedKeys)
        {
            table.ContainsKey5(key).Should().BeTrue();
        }
    }

    [Fact]
    public void ContainsKey5ShouldReturnFalseIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        var result = table.ContainsKey5(0.0f);
        result.Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey5ShouldRemoveKey()
    {
        var keyToRemove = TestData[0].Item5;
        var table = MakeHappyTable();
        
        table.RemoveByKey5(keyToRemove);

        table.ContainsKey5(keyToRemove).Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey5ShouldRemoveWholeRow()
    {
        var testToRemove = TestData[0];
        var table = MakeHappyTable();
        
        table.RemoveByKey5(testToRemove.Item5);

        table.ContainsKey1(testToRemove.Item1).Should().BeFalse();
        table.ContainsKey2(testToRemove.Item2).Should().BeFalse();
        table.ContainsKey3(testToRemove.Item3).Should().BeFalse();
        table.ContainsKey4(testToRemove.Item4).Should().BeFalse();
        table.ContainsKey5(testToRemove.Item5).Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey5ShouldLeaveOtherRows()
    {
        var keyToRemove = TestData[0].Item5;
        var keyToKeep = TestData[1].Item5;
        var table = MakeHappyTable();
        
        table.RemoveByKey5(keyToRemove);

        table.ContainsKey5(keyToKeep).Should().BeTrue();
    }

    [Fact]
    public void RemoveByKey5ShouldNotThrowIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        
        table.RemoveByKey5(0.0f);
    }
    
    [Fact]
    public void ClearShouldRemoveAllKey5()
    {
        var table = MakeHappyTable();
        
        table.Clear();

        table.Key5Values.Should().BeEmpty();
    }
    
    [Fact]
    public void AddShouldAddByKey5()
    {
        var guid = Guid.NewGuid();
        var table = MakeEmptyTable();
        
        AddToTable(table, -1, "negative", guid, 1, 1.0f);

        var row = table.GetByKey5(1.0f);
        row.Key1.Should().Be(-1);
        row.Key2.Should().Be("negative");
        row.Key3.Should().Be(guid);
        row.Key4.Should().Be(1);
        row.Key5.Should().Be(1.0f);
    }
}

public class Table5Tests : Table5Tests<Table<int, string, Guid, int, float>, TableRow<int, string, Guid, int, float>>
{
    public override Table<int, string, Guid, int, float> MakeEmptyTable() => new();

    public override Table<int, string, Guid, int, float> MakeHappyTable()
    {
        var table = MakeEmptyTable();
        foreach (var data in TestData)
        {
            table.Add(data.Item1, data.Item2, data.Item3, data.Item4, data.Item5);
        }

        return table;
    }

    public override void AddToTable(Table<int, string, Guid, int, float> table, int key1, string key2, Guid key3, int key4, float key5, object key6)
    {
        table.Add(key1, key2, key3, key4, key5);
    }
}