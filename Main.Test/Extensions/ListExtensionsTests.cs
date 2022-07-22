using PlusCollections.Extensions;

namespace PlusCollections.Test.Extensions;

public class ListExtensionsTests
{
    protected IList<int> MakeHappyList() => new List<int> { 1, 2, 3, 4, 5 };
    protected IList<int> MakeEmptyList() => new List<int>();
    
    public class ContainsFirstShould : ListExtensionsTests
    {
        [Fact]
        public void ReturnTrueWhenTargetIsFirst()
        {
            var list = MakeHappyList();
            var result = list.ContainsFirst(1);
            result.Should().BeTrue();
        }

        [Fact]
        public void ReturnFalseWhenTargetIsNotFirst()
        {
            var list = MakeHappyList();
            var result = list.ContainsFirst(3);
            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnFalseWhenTargetIsNotInList()
        {
            var list = MakeHappyList();
            var result = list.ContainsFirst(-1);
            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnFalseWhenListIsEmpty()
        {
            var list = MakeEmptyList();
            var result = list.ContainsFirst(1);
            result.Should().BeFalse();
        }
    }

    public class ContainsLastShould : ListExtensionsTests
    {
        [Fact]
        public void ReturnTrueWhenTargetIsLast()
        {
            var list = MakeHappyList();
            var result = list.ContainsLast(5);
            result.Should().BeTrue();
        }

        [Fact]
        public void ReturnFalseWhenTargetIsNotLast()
        {
            var list = MakeHappyList();
            var result = list.ContainsLast(3);
            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnFalseWhenTargetIsNotInList()
        {
            var list = MakeHappyList();
            var result = list.ContainsLast(-1);
            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnFalseWhenListIsEmpty()
        {
            var list = MakeEmptyList();
            var result = list.ContainsLast(1);
            result.Should().BeFalse();
        }
    }

    public class ContainsNotFirstShould : ListExtensionsTests
    {
        [Fact]
        public void ReturnFalseWhenTargetIsFirst()
        {
            var list = MakeHappyList();
            var result = list.ContainsNotFirst(1);
            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnTrueWhenTargetIsNotFirst()
        {
            var list = MakeHappyList();
            var result = list.ContainsNotFirst(3);
            result.Should().BeTrue();
        }

        [Fact]
        public void ReturnFalseWhenTargetIsNotInList()
        {
            var list = MakeHappyList();
            var result = list.ContainsNotFirst(-1);
            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnFalseWhenListIsEmpty()
        {
            var list = MakeEmptyList();
            var result = list.ContainsNotFirst(1);
            result.Should().BeFalse();
        }
    }

    public class ContainsNotLastShould : ListExtensionsTests
    {
        [Fact]
        public void ReturnFalseWhenTargetIsLast()
        {
            var list = MakeHappyList();
            var result = list.ContainsNotLast(5);
            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnTrueWhenTargetIsNotLast()
        {
            var list = MakeHappyList();
            var result = list.ContainsNotLast(3);
            result.Should().BeTrue();
        }

        [Fact]
        public void ReturnFalseWhenTargetIsNotInList()
        {
            var list = MakeHappyList();
            var result = list.ContainsNotLast(-1);
            result.Should().BeFalse();
        }

        [Fact]
        public void ReturnFalseWhenListIsEmpty()
        {
            var list = MakeEmptyList();
            var result = list.ContainsNotLast(1);
            result.Should().BeFalse();
        }
    }
}