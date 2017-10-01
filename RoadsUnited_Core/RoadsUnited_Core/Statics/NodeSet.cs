namespace RoadsUnited_Core
{
    public class NodeSet
    {
        public NodeSet(NetInfo.Node node, string mainTex, string aprMap = null)
        {
            this.node = node;
            this.MainTex = mainTex;
            this.APRMap = aprMap;
        }

        public NetInfo.Node node;

        public string MainTex { get; private set; }

        public string APRMap { get; private set; }
    }
}