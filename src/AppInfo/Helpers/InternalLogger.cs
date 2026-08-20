using System.Diagnostics;


namespace AppInfo.Helpers;


internal static class InternalLogger
{
	public static void Log(string format, params object[] args)
	{
		try
		{
			// Note to self:
			// Traces will not appear anywhere when running the IDE in Debug mode.
			// To see the traces, run directly from command line.

			Debug.WriteLine(string.Format(format, args), Constants.TraceCategory);
			Trace.WriteLine(string.Format(format, args), Constants.TraceCategory);
		}
		catch
		{
			// Ignore
		}
	}
}
