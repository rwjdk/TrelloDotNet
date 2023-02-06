using System.Diagnostics;

namespace TrelloDotNet.Tests;

public class RunnableInDebugOnlyAttribute : FactAttribute
{
    public RunnableInDebugOnlyAttribute()
    {
#if !DEBUG
            Skip = "Only running in interactive mode.";
#endif
    }
}