namespace AppInfo.Extractors;


public class ExtrasExtractor : IExtractor
{
	private readonly Extra[] _extras;

	public ExtrasExtractor(params (string Label, object? Value)[] extras)
	{
		_extras = extras.Select(e => new Extra(e.Label, e.Value)).ToArray();
	}

	public IEnumerable<Fragment> Extract() =>
		_extras
			.Where(e => !string.IsNullOrWhiteSpace(e.Label))
			.Select(e => new Fragment(e.Label, e.Value));


	private record struct Extra(string Label, object? Value);
}
