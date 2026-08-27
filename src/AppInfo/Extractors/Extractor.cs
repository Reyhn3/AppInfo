using AppInformation.Helpers;


namespace AppInformation.Extractors;


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

	private bool TryProduceExtractors(out Func<Fragment>[] extractors)
	{
		try
		{
			extractors = ProduceExtractors().ToArray();
			return true;
		}
		catch (Exception ex)
		{
			InternalLogger.Log("Exception when trying to produce extractor:{0}{1}", Environment.NewLine, ex);
			extractors = [];
			return false;
		}
	}

	protected abstract IEnumerable<Func<Fragment>> ProduceExtractors();

	private static bool TryExtractSafely(Func<Fragment> extract, out Fragment? fragment)
	{
		try
		{
			fragment = extract();
			return true;
		}
		catch (Exception ex)
		{
			InternalLogger.Log("Exception when trying to extract:{0}{1}", Environment.NewLine, ex);
			fragment = null;
			return false;
		}
	}
}
