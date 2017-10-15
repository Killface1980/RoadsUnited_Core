namespace RoadsUnited_Core2.Statics
{
    public class SegmentSet
    {
        public SegmentSet(NetInfo.Segment segment, string mainTex, string aprMap = null)
        {
            this.segment = segment;
            this.MainTex = mainTex;
            this.APRMap = aprMap;
        }

        public NetInfo.Segment segment;

        public string path;

        public string MainTex { get; private set; }

        public string APRMap { get; private set; }
    }
}