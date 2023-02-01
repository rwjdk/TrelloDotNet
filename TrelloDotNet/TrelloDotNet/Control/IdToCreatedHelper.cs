using System;
using System.Globalization;

namespace TrelloDotNet.Control
{
    public static class IdToCreatedHelper
    {
        public static DateTimeOffset GetCreatedFromId(string id)
        {
            var hexDecimaltimestapThatIsPartOfId = id.Substring(0, 8);
            var decimalVersionInEpoc = int.Parse(hexDecimaltimestapThatIsPartOfId, NumberStyles.HexNumber);
            return DateTimeOffset.FromUnixTimeSeconds(decimalVersionInEpoc);
        }
    }
}