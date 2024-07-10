using Shouldly;


namespace AppInfo.Tests.Fragments;


public class FragmentTests
{
	[TestCase(null)]
	[TestCase("")]
	[TestCase("\t")]
	public void Ctor_shall_throw_exception_if_label_is_null_or_empty(string? label) =>
		Should.Throw<ArgumentNullException>(() => new Fragment(label, []));

	[Test]
	public void Ctor_shall_trim_label() =>
		new Fragment("\ttest" + Environment.NewLine, []).Label.ShouldBe("test");

	[Test]
	public void Ctor_shall_use_null_if_value_is_null()
	{
		var result = new Fragment("test", null);
		result.Value.ShouldBeNull();
	}

	[Test]
	public void Ctor_shall_use_empty_array_if_value_is_empty()
	{
		var result = new Fragment("test");
		result.Value.ShouldNotBeNull();
		result.Value.ShouldBeEmpty();
	}

	[Test]
	public void Ctor_shall_place_single_value_inside_array()
	{
		var expected = new object();


		var result = new Fragment("test", expected).Value?.ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintValues(result);
		result.Length.ShouldBe(1);
		result.First().ShouldBe(expected);
	}

	[Test]
	public void Ctor_shall_place_multiple_values_inside_array()
	{
		var expected = new object[]
			{
				1,
				"two",
				new()
			};


		var result = new Fragment("test", expected).Value?.ToArray();


		result.ShouldNotBeNull();
		result.ShouldNotBeEmpty();
		Helpers.PrintValues(result);
		result.Length.ShouldBe(expected.Length);
		result.ShouldBeEquivalentTo(expected);
	}
}
