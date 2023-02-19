using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace TrelloDotNet.Control
{
    internal static class IdToCreatedHelper
    {
        internal static DateTimeOffset? GetCreatedFromId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            var hexDecimaltimestapThatIsPartOfId = id.Substring(0, 8);
            var decimalVersionInEpoch = int.Parse(hexDecimaltimestapThatIsPartOfId, NumberStyles.HexNumber);
            return DateTimeOffset.FromUnixTimeSeconds(decimalVersionInEpoch);
        }
    }
}