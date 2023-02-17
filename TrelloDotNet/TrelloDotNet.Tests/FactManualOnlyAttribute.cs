namespace TrelloDotNet.Tests;

public sealed class FactManualOnlyAttribute : FactAttribute
{
    public FactManualOnlyAttribute()
    {
        Skip = "Manual";
    }
}