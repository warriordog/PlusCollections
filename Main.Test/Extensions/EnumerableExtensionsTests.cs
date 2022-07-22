using PlusCollections.Extensions;

namespace PlusCollections.Test.Extensions;

public class EnumerableExtensionsTests
{
    public class MaxOrDefaultTests : EnumerableExtensionsTests
    {
        public class WithCallbackAndDefaultShould : MaxOrDefaultTests
        {
            [Fact]
            public void ReturnMaxWhenEnumerableHasContents()
            {
                var list = new List<int> { -1, 3, 0, 1 };
                var max = list.MaxOrDefault("", num => num.ToString());
                max.Should().Be("3");
            }

            [Fact]
            public void ReturnDefaultWhenEnumerableIsEmpty()
            {
                var enumerable = Enumerable.Empty<int>();
                var max = enumerable.MaxOrDefault("", num => num.ToString());
                max.Should().Be("");
            }
        }

        public class WithCallbackOnlyShould : MaxOrDefaultTests
        {
            [Fact]
            public void ReturnMaxWhenEnumerableHasContents()
            {
                var list = new List<int> { -1, 3, 0, 1 };
                var max = list.MaxOrDefault(num => num.ToString());
                max.Should().Be("3");
            }

            [Fact]
            public void ReturnDefaultOfTypeWhenEnumerableIsEmpty()
            {
                var enumerable = Enumerable.Empty<int>();
                var max = enumerable.MaxOrDefault(num => num.ToString());
                max.Should().Be(null);
            }
        }

        public class WithDefaultOnlyShould : MaxOrDefaultTests
        {
            [Fact]
            public void ReturnMaxWhenEnumerableHasContents()
            {
                var list = new List<int> { -1, 3, 0, 1 };
                var max = list.MaxOrDefault(0);
                max.Should().Be(3);
            }

            [Fact]
            public void ReturnDefaultWhenEnumerableIsEmpty()
            {
                var enumerable = Enumerable.Empty<int>();
                var max = enumerable.MaxOrDefault(12);
                max.Should().Be(12);
            }
        }

        public class WithoutCallbackOrDefaultShould : MaxOrDefaultTests
        {
            [Fact]
            public void ReturnMaxWhenEnumerableHasContents()
            {
                var list = new List<int> { -1, 3, 0, 1 };
                var max = list.MaxOrDefault();
                max.Should().Be(3);
            }

            [Fact]
            public void ReturnDefaultWhenEnumerableIsEmpty()
            {
                var enumerable = Enumerable.Empty<int>();
                var max = enumerable.MaxOrDefault();
                max.Should().Be(default);
            }
        }
    }
}