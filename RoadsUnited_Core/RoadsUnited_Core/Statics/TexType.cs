namespace RoadsUnited_Core2.Statics
{
    using System.Collections.Generic;

    public static class TexType
    {
        #region Public Fields

        public const string APRMap = "_APRMap";

        public const string MainTex = "_MainTex";

        public const string ACIMap = "_ACIMap";

        public const string XYSMap = "_XYSMap";

        public static List<string> AllTex = new List<string>();

        static TexType()
        {
            AllTex = new List<string> { MainTex, APRMap /* no , _XYSMap for now => null reference!!! */};
        }

        #endregion Public Fields
    }
}