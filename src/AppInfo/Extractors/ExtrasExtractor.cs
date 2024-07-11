namespace AppInfo.Extractors;


public class ExtrasExtractor : Extractor
{
	private readonly Extra[] _extras;

	public ExtrasExtractor(params (string Label, object? Value)[] extras)
	{
		_extras = extras.Select(e => new Extra(e.Label, e.Value)).ToArray();
	}

	protected override IEnumerable<Func<Fragment>> ProduceExtractors() =>
		_extras
			.Where(e => !string.IsNullOrWhiteSpace(e.Label))
			.Select<Extra, Func<Fragment>>(e => () => new Fragment(e.Label, e.Value));


	private record struct Extra(string Label, object? Value);
}
