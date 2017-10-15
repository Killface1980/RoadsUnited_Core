namespace RoadsUnited_Core2.Statics
{
    public class NodeSet
    {
        public NodeSet(NetInfo.Node node, string mainTex, string aprMap = null)
        {
            this.node = node;
            this.MainTex = mainTex;
            this.APRMap = aprMap;
            this.path = path;
        }

        public NetInfo.Node node;

        public string path;

        public string MainTex { get; private set; }

        public string APRMap { get; private set; }
    }
}