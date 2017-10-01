namespace RoadsUnited_Core
{
    using System.Collections.Generic;

    public static class RoadPos
    {
        #region Public Fields

        public static string Elevated = "Elevated";

        public static string Ground = "Ground";

        public static string Slope = "Slope";

        public static string Tunnel = "Tunnel";

        public static readonly List<string> AllPositions;

        static RoadPos()
        {
            AllPositions = new List<string> { Ground, Elevated, Slope, Tunnel };
        }

        #endregion Public Fields
    }
}