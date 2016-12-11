namespace RoadsUnited_Core
{
    using System.Collections.Generic;

    public static class RoadPos
    {
        public static string Elevated = "Elevated";

        public static string Ground = "Ground";

        public static string Slope = "Slope";

        public static string Tunnel = "Tunnel";

        public static List<string> AllPositions = new List<string> { Ground, Elevated, Slope, Tunnel };
    }

    public static class TexType
    {
        public static string _APRMap = "_APRMap";

        public static string _MainTex = "_MainTex";

        public static List<string> AllTex = new List<string> { _MainTex, _APRMap };
    }

    public static class RU_CoreDicts
    {
        public static readonly Dictionary<string, string> NExtRoads = new Dictionary<string, string>
                                                                          {
                                                                                  { "Two-Lane Alley", "Alley2L" },
                                                                                  { "One-Lane Oneway", "OneWay1L" },
                                                                                  { "Eight-Lane Avenue", "LargeAvenue8LM" },
                                                                                  { "Four-Lane Highway", "Highway4L" },
                                                                                  { "Five-Lane Highway", "Highway5L" },
                                                                                  { "Large Highway", "Highway6L" },
                                                                                  { "BasicRoadTL", "BasicRoadTL" },
                                                                                  { "Oneway3L", "Oneway3L" },
                                                                                  { "Oneway4L", "Oneway4L" },
                                                                                  { "Small Avenue", "SmallAvenue4L" }
                                                                          };
    }
}