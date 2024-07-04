using AppInfo.Renderers;
using FakeItEasy;
using Shouldly;


namespace AppInfo.Tests.Renderers;


public class LogRendererTests
{
	[Test]
	public void IsScalar_shall_return_true_for_null() =>
		LogRenderer.IsScalar(null).ShouldBeTrue();

	[Test]
	public void IsScalar_shall_return_true_for_single_element_enumerable() =>
		LogRenderer.IsScalar(Enumerable.Repeat((object?)null, 1)).ShouldBeTrue();

	[Test]
	public void IsScalar_shall_return_true_for_single_string() =>
		LogRenderer.IsScalar(new object?[]
			{
				"test"
			}).ShouldBeTrue();

	[Test]
	public void IsScalar_shall_return_false_for_multiple_strings() =>
		LogRenderer.IsScalar(Enumerable.Repeat((object?)"test", 2)).ShouldBeFalse();

	[Test]
	public void IsScalar_shall_return_false_for_multiple_element_enumerable() =>
		LogRenderer.IsScalar(Enumerable.Repeat((object?)null, 2)).ShouldBeFalse();

	[Test]
	public void IsScalar_shall_return_false_for_empty_enumerable() =>
		LogRenderer.IsScalar([]).ShouldBeFalse();

	[TestCase("myValue", "MyValue")]
	[TestCase("my Value", "MyValue")]
	[TestCase("my value", "MyValue")]
	public void FormatName_shall_convert_input_to_pascal_case(string actual, string expected) =>
		LogRenderer.FormatName(actual).ShouldBe(expected);

	[TestCase("my value", "MyValue")]
	[TestCase(" my value ", "MyValue")]
	[TestCase("\u2002my\tvalue\r\n", "MyValue")]
	public void FormatName_shall_remove_whitespace(string actual, string expected) =>
		LogRenderer.FormatName(actual).ShouldBe(expected);

	[Test]
	public void CalculateSuffix_shall_return_null_for_unique_labels()
	{
		var fragments = new Fragment[]
			{
				new("a", A.Dummy<object?>()),
				new("b", A.Dummy<object?>())
			};
		LogRenderer.CalculateSuffix(fragments, fragments.First()).ShouldBeNull();
		LogRenderer.CalculateSuffix(fragments, fragments.Last()).ShouldBeNull();
	}

	[Test]
	public void CalculateSuffix_shall_return_a_zero_based_index_for_non_unique_labels()
	{
		var fragments = new Fragment[]
			{
				new("a", A.Dummy<object?>()),
				new("b", A.Dummy<object?>()),
				new("c", A.Dummy<object?>()),
				new("b", A.Dummy<object?>()),
				new("a", A.Dummy<object?>())
			};

		LogRenderer.CalculateSuffix(fragments, fragments[0]).ShouldBe(0);
		LogRenderer.CalculateSuffix(fragments, fragments[4]).ShouldBe(1);
		LogRenderer.CalculateSuffix(fragments, fragments[1]).ShouldBe(0);
		LogRenderer.CalculateSuffix(fragments, fragments[3]).ShouldBe(1);
		LogRenderer.CalculateSuffix(fragments, fragments[2]).ShouldBeNull();
	}
}
