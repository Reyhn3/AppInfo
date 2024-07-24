using System.Text.RegularExpressions;


namespace AppInfo.Renderers;


public class LogRenderer(Action<string, object?[]> logger) : Renderer
{
	private static readonly Regex s_whitespace = new(@"\s+", RegexOptions.Compiled);
	private readonly Action<string, object?[]> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

	protected override void RenderAppInfo(IAppInfo info)
	{
		var fragments = info.Fragments.ToArray();
		var names = fragments.ToDictionary(k => k, v => FormatName(v.Label));
		var args = fragments.Select(FormatValue).ToArray();

		var format = info.Fragments.Aggregate(
			ConcatenateTitle(info),
			(current, next) =>
//BUG: Add missing label
				current + $"{Environment.NewLine}{{{(IsScalar(next.Value) ? string.Empty : "@")}{names[next]}{CalculateSuffix(fragments, next)}}}");

		_logger(format, args);
	}

	private static object? FormatValue(Fragment fragment) =>
		fragment.Value == null
			? null
			: IsScalar(fragment.Value)
				? fragment.Value.SingleOrDefault()
				: fragment.Value.ToArray();

	private static string ConcatenateTitle(IAppInfo info)
	{
		var (lead, name, tail) = GenerateTitleParts(info);
		return lead + name + tail;
	}

	internal static bool IsScalar(IEnumerable<object?>? value) =>
		value == null || value.Count() == 1;

//TODO: Move this logic to a output-shared pre-render
	internal static int? CalculateSuffix(IEnumerable<Fragment> all, Fragment current)
	{
		var duplicates = all
			.GroupBy(f => f.Label.Trim())
			.Where(g => g.Count() > 1)
			.ToDictionary(k => k.Key);

		if (!duplicates.TryGetValue(current.Label.Trim(), out var fragmentDuplicates))
			return null;

		return fragmentDuplicates
			.Select((f, index) => new
				{
					Fragment = f,
					Index = index
				})
			.Single(x => x.Fragment == current)
			.Index;
	}

//BUG: This must clean away dashes and other invalid characters
	internal static string FormatName(string label)
	{
		var components = s_whitespace.Split(label).Where(s => !string.IsNullOrWhiteSpace(s));
		return string.Join(string.Empty, components.Select(c => char.ToUpper(c[0]) + c[1..]));
	}
}
