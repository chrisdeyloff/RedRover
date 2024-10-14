using FluentAssertions;
using RedRover.Parse;

namespace RedRover.Tests.Unit;

public class ParserTests
{
    private const string standardExpected = @"- id
- name
- email
- type
  - id
  - name
  - customFields
    - c1
    - c2
    - c3
- externalId";

    private const string sortedExpected = @"- email
- externalId
- id
- name
- type
  - customFields
    - c1
    - c2
    - c3
  - id
  - name";

    [Theory]
    [InlineData(Constants.Input, false, standardExpected)]
    [InlineData(Constants.Input, true, sortedExpected)]
    [InlineData("", false, "")]
    public void ParsingInputResult_ShouldMatch_ExpectedOutput(string input, bool sort, string expectedOutput)
    {
        // Arrange

        // Act
        var result = Parser.ParseToMultilineNested(input, sort);
        
        // Assert
        result.Should().Be(expectedOutput);
    }

    [Theory]
    [InlineData(Constants.Input, false, $"blah-{standardExpected}-blah")]
    [InlineData(Constants.Input, true, $"blah-{sortedExpected}-blah")]
    public void ParsingInputResult_Should_NOT_Match_ExpectedOutput(string input, bool sort, string expectedOutput)
    {
        // Arrange

        // Act
        var result = Parser.ParseToMultilineNested(input, sort);

        // Assert
        result.Should().NotBe(expectedOutput);
    }
}