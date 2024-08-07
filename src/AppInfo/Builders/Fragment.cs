namespace AppInfo;


public sealed class Fragment
{
	public Fragment(string label, object? value)
		: this(label, value == null ? null : [value])
	{}

	public Fragment(string label, params object?[]? values)
	{
		if (string.IsNullOrWhiteSpace(label))
			throw new ArgumentNullException(label);

		Label = label.Trim();
		Value = values?.ToArray() ?? null;
	}

	public void Deconstruct(out string label, out IEnumerable<object?>? value)
	{
		label = Label;
		value = Value;
	}

	public string Label { get; }
	public IEnumerable<object?>? Value { get; }
}
