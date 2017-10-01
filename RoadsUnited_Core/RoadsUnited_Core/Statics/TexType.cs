namespace RoadsUnited_Core
{
    using System.Collections.Generic;

    public static class TexType
    {
        #region Public Fields

        public static string _APRMap = "_APRMap";

        public static string _MainTex = "_MainTex";

        public static string _XYSMap = "_XYSMap";

        public static List<string> AllTex = new List<string>();

        static TexType()
        {
            AllTex = new List<string> { _MainTex, _APRMap /* no , _XYSMap for now => null reference!!! */};
        }

        #endregion Public Fields
    }
}