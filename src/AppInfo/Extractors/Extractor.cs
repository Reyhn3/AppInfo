using System.Diagnostics;


namespace AppInfo.Extractors;


public abstract class Extractor : IExtractor
{
	public IEnumerable<Fragment> Extract()
	{
		if (!TryProduceExtractors(out var extractors) || extractors.Length == 0)
			yield break;

		foreach (var extractor in extractors)
		{
			if (TryExtractSafely(extractor, out var fragment) && fragment != null)
				yield return fragment;
		}
	}

	protected abstract IEnumerable<Func<Fragment>> ProduceExtractors();

	private bool TryProduceExtractors(out Func<Fragment>[] extractors)
	{
		try
		{
			extractors = ProduceExtractors().ToArray();
			return true;
		}
		catch (Exception ex)
		{
			Trace.WriteLine($"Exception when trying to produce extractor:{Environment.NewLine}{ex}", Constants.LibraryName);
			extractors = [];
			return false;
		}
	}

	private static bool TryExtractSafely(Func<Fragment> extractor, out Fragment? fragment)
	{
		try
		{
			fragment = extractor();
			return true;
		}
		catch (Exception ex)
		{
			Trace.WriteLine($"Exception when trying to extract:{Environment.NewLine}{ex}", Constants.LibraryName);
			fragment = null;
			return false;
		}
	}
}
