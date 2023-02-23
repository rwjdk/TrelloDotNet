namespace TrelloDotNet.Tests;

public sealed class FactManualOnlyAttribute : FactAttribute
{
    public FactManualOnlyAttribute()
    {
        if (!Environment.MachineName.ToLowerInvariant().Contains("rwj"))
        {
            Skip = "Manual";
        }
    }
}