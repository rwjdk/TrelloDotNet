using System;
using System.Globalization;

namespace TrelloDotNet.Control
{
    internal static class IdToCreatedHelper
    {
        internal static DateTimeOffset GetCreatedFromId(string id)
        {
            var hexDecimaltimestapThatIsPartOfId = id.Substring(0, 8);
            var decimalVersionInEpoch = int.Parse(hexDecimaltimestapThatIsPartOfId, NumberStyles.HexNumber);
            return DateTimeOffset.FromUnixTimeSeconds(decimalVersionInEpoch);
        }
    }
}