using FluentAssertions;

namespace Questao5.Tests;

public static class ComparerObjectExtension
{

    public static bool CompareObjects(this object expected, object mockObject)
    {
        expected.Should().BeEquivalentTo(mockObject);
        return true;
    }
}
