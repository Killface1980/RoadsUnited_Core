namespace RoadsUnited_Core
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

        public string MainTex { get; private set; }

        public string APRMap { get; private set; }
    }
}