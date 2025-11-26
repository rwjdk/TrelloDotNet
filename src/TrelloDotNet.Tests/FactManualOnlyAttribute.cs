using System.Runtime.CompilerServices;

namespace TrelloDotNet.Tests;

public sealed class FactManualOnlyAttribute : FactAttribute
{
    public FactManualOnlyAttribute([CallerFilePath] string? sourceFilePath = null, [CallerLineNumber] int sourceLineNumber = -1) : base(sourceFilePath, sourceLineNumber)
    {
        if (!Environment.MachineName.ToLowerInvariant().Contains("rwj", StringComparison.CurrentCultureIgnoreCase))
        {
            Skip = "Manual";
        }
    }
}