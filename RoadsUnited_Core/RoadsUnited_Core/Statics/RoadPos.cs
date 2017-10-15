namespace RoadsUnited_Core2.Statics
{
    using System.Collections.Generic;

    public static class RoadPos
    {
        #region Public Fields

        public const string Elevated = "Elevated";

        public const string Ground = "Ground";

        public const string Slope = "Slope";

        public const string Tunnel = "Tunnel";

        public static readonly List<string> AllPositions;

        static RoadPos()
        {
            AllPositions = new List<string> { Ground, Elevated, Slope, Tunnel };
        }

        #endregion Public Fields
    }
}