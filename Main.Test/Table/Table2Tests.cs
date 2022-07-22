using FluentAssertions;
using PlusCollections.Table;

namespace PlusCollections.Test.Table;

public abstract class Table2Tests<TTable, TRow> : BaseTableTests<TTable, TRow>
    where TTable : Table2<int, string, TRow>
    where TRow : Table2Row<int, string, TRow>
{

    [Fact]
    public void Key1MapShouldIncludeAllRows()
    {
        var table = MakeHappyTable();
        var map = table.Key1Map;
        foreach (var test in TestData)
        {
            map.Should().ContainKey(test.Item1);
        }
    } 
        
    [Fact]
    public void Key1ValuesShouldIncludeAllKeys()
    {
        var expectedKeys = TestData.Select(data => data.Item1);
        var table = MakeHappyTable();
        
        var keys = table.Key1Values;

        keys.Should().BeEquivalentTo(expectedKeys);
    }
    
    [Fact]
    public void Key2MapShouldIncludeAllRows()
    {
        var table = MakeHappyTable();
        var map = table.Key2Map;
        foreach (var test in TestData)
        {
            map.Should().ContainKey(test.Item2);
        }
    } 
        
    [Fact]
    public void Key2ValuesShouldIncludeAllKeys()
    {
        var expectedKeys = TestData.Select(data => data.Item2);
        var table = MakeHappyTable();
        
        var keys = table.Key2Values;

        keys.Should().BeEquivalentTo(expectedKeys);
    }
    
    [Fact]
    public void GetByKey1ShouldGetTheCorrectRow()
    {
        var table = MakeHappyTable();

        foreach (var test in TestData)
        {
            table.GetByKey1(test.Item1).Key1.Should().Be(test.Item1);
        }
    }
    
    [Fact]
    public void GetByKey1ShouldThrowIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        Assert.Throws<KeyNotFoundException>(() => table.GetByKey1(0));
    }
    
    [Fact]
    public void GetByKey2ShouldGetTheCorrectRow()
    {
        var table = MakeHappyTable();

        foreach (var test in TestData)
        {
            table.GetByKey2(test.Item2).Key2.Should().Be(test.Item2);
        }
    }
    
    [Fact]
    public void GetByKey2ShouldThrowIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        Assert.Throws<KeyNotFoundException>(() => table.GetByKey2(""));
    }
    
    [Fact]
    public void TryGetByKey1ShouldGetTheCorrectRow()
    {
        var expectedKeys = TestData.Select(test => test.Item1);
        var table = MakeHappyTable();

        foreach (var key in expectedKeys)
        {
            var result = table.TryGetByKey1(key, out var resultRow);
            result.Should().BeTrue();
            resultRow.Should().NotBeNull();
            resultRow.Key1.Should().Be(key);
        }
    }
    
    [Fact]
    public void TryGetByKey1ShouldReturnFalseIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        var result = table.TryGetByKey1(0, out _);
        result.Should().BeFalse();
    }
    
    
    [Fact]
    public void TryGetByKey2ShouldGetTheCorrectRow()
    {
        var expectedKeys = TestData.Select(test => test.Item2);
        var table = MakeHappyTable();

        foreach (var key in expectedKeys)
        {
            var result = table.TryGetByKey2(key, out var resultRow);
            result.Should().BeTrue();
            resultRow.Should().NotBeNull();
            resultRow.Key2.Should().Be(key);
        }
    }
    
    [Fact]
    public void TryGetByKey2ShouldReturnFalseIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        var result = table.TryGetByKey2("", out _);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void ContainsKey1ShouldReturnTrueIfKeyIsPresent()
    {
        var expectedKeys = TestData.Select(test => test.Item1);
        var table = MakeHappyTable();

        foreach (var key in expectedKeys)
        {
            table.ContainsKey1(key).Should().BeTrue();
        }
    }

    [Fact]
    public void ContainsKey1ShouldReturnFalseIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        var result = table.ContainsKey1(0);
        result.Should().BeFalse();
    }
    
    [Fact]
    public void ContainsKey2ShouldReturnTrueIfKeyIsPresent()
    {
        var expectedKeys = TestData.Select(test => test.Item2);
        var table = MakeHappyTable();

        foreach (var key in expectedKeys)
        {
            table.ContainsKey2(key).Should().BeTrue();
        }
    }

    [Fact]
    public void ContainsKey2ShouldReturnFalseIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        var result = table.ContainsKey2("");
        result.Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey1ShouldRemoveKey()
    {
        var keyToRemove = TestData[0].Item1;
        var table = MakeHappyTable();
        
        table.RemoveByKey1(keyToRemove);

        table.ContainsKey1(keyToRemove).Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey1ShouldRemoveWholeRow()
    {
        var testToRemove = TestData[0];
        var table = MakeHappyTable();
        
        table.RemoveByKey1(testToRemove.Item1);

        table.ContainsKey1(testToRemove.Item1).Should().BeFalse();
        table.ContainsKey2(testToRemove.Item2).Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey1ShouldLeaveOtherRows()
    {
        var keyToRemove = TestData[0].Item1;
        var keyToKeep = TestData[1].Item1;
        var table = MakeHappyTable();
        
        table.RemoveByKey1(keyToRemove);

        table.ContainsKey1(keyToKeep).Should().BeTrue();
    }

    [Fact]
    public void RemoveByKey1ShouldNotThrowIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        
        table.RemoveByKey1(0);
    }

    [Fact]
    public void RemoveByKey2ShouldRemoveKey()
    {
        var keyToRemove = TestData[0].Item2;
        var table = MakeHappyTable();
        
        table.RemoveByKey2(keyToRemove);

        table.ContainsKey2(keyToRemove).Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey2ShouldRemoveWholeRow()
    {
        var testToRemove = TestData[0];
        var table = MakeHappyTable();
        
        table.RemoveByKey2(testToRemove.Item2);

        table.ContainsKey1(testToRemove.Item1).Should().BeFalse();
        table.ContainsKey2(testToRemove.Item2).Should().BeFalse();
    }

    [Fact]
    public void RemoveByKey2ShouldNotThrowIfKeyIsMissing()
    {
        var table = MakeEmptyTable();
        
        table.RemoveByKey2("");
    }
    
    [Fact]
    public void ClearShouldRemoveAllKey1()
    {
        var table = MakeHappyTable();
        
        table.Clear();

        table.Key1Values.Should().BeEmpty();
    }

    [Fact]
    public void ClearShouldRemoveAllKey2()
    {
        var table = MakeHappyTable();
        
        table.Clear();

        table.Key2Values.Should().BeEmpty();
    }
    
    [Fact]
    public void AddShouldAddByKey1()
    {
        var table = MakeEmptyTable();
        
        AddToTable(table, -1, "negative");

        var row = table.GetByKey1(-1);
        row.Key1.Should().Be(-1);
        row.Key2.Should().Be("negative");
    }
    
    [Fact]
    public void AddShouldAddByKey2()
    {
        var table = MakeEmptyTable();
        
        AddToTable(table, -1, "negative");

        var row = table.GetByKey2("negative");
        row.Key1.Should().Be(-1);
        row.Key2.Should().Be("negative");
    }

    [Fact]
    public void AddShouldAddToEnd()
    {
        var table = MakeHappyTable();
        
        AddToTable(table, -1, "negative");

        table.Key1Values.Last().Should().Be(-1);
    }

    [Fact]
    public void AddShouldPreserveRowOrder()
    {
        var expectedKeys = TestData.Select(test => test.Item1).ToList();
        expectedKeys.Add(-1);
        var table = MakeHappyTable();
        
        AddToTable(table, -1, "negative");

        var keys = table.Key1Values.ToList();
        keys.Should().BeEquivalentTo(expectedKeys);
    }
    
    [Fact]
    public void AddShouldReplaceDuplicates()
    {
        var table = MakeHappyTable();
        
        AddToTable(table, 1, "replaced");

        table.GetByKey1(1).Key2.Should().Be("replaced");
    }

    [Fact]
    public void AddShouldRemoveEntireRowOfDuplicates()
    {
        var table = MakeHappyTable();
        
        AddToTable(table, 1, "replaced");

        table.ContainsKey2("one").Should().BeFalse();
    }
}

public class Table2Tests : Table2Tests<Table<int, string>, TableRow<int, string>>
{
    public override Table<int, string> MakeEmptyTable() => new();

    public override Table<int, string> MakeHappyTable()
    {
        var table = MakeEmptyTable();
        foreach (var data in TestData)
        {
            table.Add(data.Item1, data.Item2);
        }
        return table;
    }

    public override void AddToTable(Table<int, string> table, int key1, string key2, Guid key3, int key4, float key5, object key6)
    {
        table.Add(key1, key2);
    }
}
