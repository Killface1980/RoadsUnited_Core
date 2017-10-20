namespace RoadsUnited_Core2
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using ColossalFramework;

    using RoadsUnited_Core2.Statics;

    public static class ShittyPlusFuck
    {
        private const string roadtiny = "RoadTiny";
        private const string roadsmall = "RoadSmall";
        private const string roadmedium = "RoadMedium";
        private const string roadlarge = "RoadLarge";
        private const string highway = "Highway";
        private const string str = "Oneway";
        private const string str2 = "Bike";
        private const string text6 = "BusLane";
        private const string str3 = "Tram";
        private const string str4 = "L1R2";
        private const string str5 = "L1R3";
        private const string gnd = "Gnd";
        private const string deco = "Deco";
        private const string busside = "BusSide";
        private const string busboth = "BusBoth";
        private const string elevated = "Elevated";
        private const string slope = "Slope";
        private const string tunnel = "Tunnel";
        private const string text14 = "_Node";
        private const string tramRailDoubleWnNoName = "tram-rail-double-wn-No Name";
        private const string value2 = "tram-rail-double-No Name";
        private const string lodMaintexDds = "_LOD_MainTex.dds";


        public static void ReplacePlus(NetInfo netInfo, ref List<SegmentSet> segList, ref List<NodeSet> nodeList)
        {
            string tex = string.Empty;

            if (netInfo.m_class.name.Contains("NExt") || netInfo.name.Contains("Busway")
                || (netInfo.name.Contains("Large Road") && netInfo.name.Contains("Bus Lane")))
            {
                NetInfo.Node[] nodes = netInfo.m_nodes;
                for (int i = 0; i < nodes.Length; i++)
                {
                    NetInfo.Node node = nodes[i];
                    if (node.m_nodeMaterial.GetTexture(TexType.MainTex) != null && !node.m_nodeMaterial.name.Contains("rail"))
                    {

                        if (netInfo.name.Equals("Two-Lane Alley"))
                        {
                            tex = roadtiny + "2L" + text14;

                            // tex = roadtiny + "1L" + text14;
                        }

                        if (netInfo.name.Equals("One-Lane Oneway"))
                        {
                            tex = roadtiny + "1L" + text14;
                        }

                        if (netInfo.name.Contains("Eight-Lane Avenue"))
                        {
                            tex = roadlarge + "8L";
                            if (netInfo.name.Equals("Eight-Lane Avenue Elevated"))
                            {
                                tex += elevated + text14;
                            }
                            else if (netInfo.name.Equals("Eight-Lane Avenue Slope"))
                            {
                                tex = tex + slope + text14;
                            }
                            else if (netInfo.name.Equals("Eight-Lane Avenue Tunnel"))
                            {
                                tex = tex + tunnel + text14;
                            }
                            else
                            {
                                tex = tex + gnd + text14;
                            }
                        }

                        if (netInfo.name.Contains("Highway"))
                        {
                            if (netInfo.name.Contains("Small Rural Highway"))
                            {
                                tex = highway + "1L" + text14;
                            }

                            if (netInfo.name.Equals("Small Rural Highway Elevated"))
                            {
                                tex = highway + "1L" + text14;
                            }

                            if (netInfo.name.Equals("Small Rural Highway Slope") && netInfo.name.Contains("Small"))
                            {
                                tex = highway + "1L" + slope + text14;
                            }

                            if (netInfo.name.Equals("Small Rural Highway Tunnel"))
                            {
                                tex = highway + "1L" + tunnel + text14;
                            }

                            if (netInfo.name.Equals("Rural Highway"))
                            {
                                tex = highway + "2L" + text14;
                            }

                            if (netInfo.name.Equals("Rural Highway Elevated"))
                            {
                                tex = highway + "2L" + text14;
                            }

                            if (netInfo.name.Equals("Rural Highway Slope"))
                            {
                                tex = highway + "2L" + slope + text14;
                            }

                            if (netInfo.name.Equals("Rural Highway Tunnel"))
                            {
                                tex = highway + "2L" + tunnel + text14;
                            }

                            if (netInfo.name.Equals("Four-Lane Highway"))
                            {
                                tex = highway + "4L" + gnd + text14;
                            }

                            if (netInfo.name.Equals("Four-Lane Highway Elevated"))
                            {
                                tex = highway + "4L" + elevated + text14;
                            }

                            if (netInfo.name.Equals("Four-Lane Highway Slope"))
                            {
                                tex = highway + "4L" + slope + text14;
                            }

                            if (netInfo.name.Equals("Four-Lane Highway Tunnel"))
                            {
                                tex = highway + "4L" + tunnel + text14;
                            }

                            if (netInfo.name.Equals("Five-Lane Highway"))
                            {
                                tex = highway + "5L" + text14;
                            }

                            if (netInfo.name.Equals("Five-Lane Highway Elevated"))
                            {
                                tex = highway + "5L" + text14;
                            }

                            if (netInfo.name.Equals("Five-Lane Highway Slope"))
                            {
                                tex = highway + "5L" + gnd + text14;
                            }

                            if (netInfo.name.Equals("Five-Lane Highway Tunnel"))
                            {
                                tex = highway + "5L" + tunnel + text14;
                            }

                            if (netInfo.name.Equals("Large Highway"))
                            {
                                tex = highway + "6L" + text14;
                            }

                            if (netInfo.name.Equals("Large Highway Elevated"))
                            {
                                tex = highway + "6L" + text14;
                            }

                            if (netInfo.name.Equals("Large Highway Slope"))
                            {
                                tex = highway + "6L" + gnd + text14;
                            }

                            if (netInfo.name.Equals("Large Highway Tunnel"))
                            {
                                tex = highway + "6L" + tunnel + text14;
                            }
                        }

                        if (!tex.IsNullOrWhiteSpace())
                        {
                            nodeList.Add(new NodeSet(node, tex + TexType.MainTex, tex + TexType.APRMap));
                        }
                    }
                }

                NetInfo.Segment[] segments = netInfo.m_segments;
                for (int j = 0; j < segments.Length; j++)
                {
                    NetInfo.Segment segment = segments[j];
                    if (segment.m_segmentMaterial.GetTexture("_MainTex") != null
                        && !segment.m_material.name.ToLower().Contains("cable"))
                    {
                        if (netInfo.name.Equals("Two-Lane Alley"))
                        {
                            tex = roadtiny + "2L";

                            // tex = roadtiny + "1L";
                        }

                        if (netInfo.name.Equals("One-Lane Oneway"))
                        {
                            tex = roadtiny + "1L";
                        }

                        if (netInfo.name.Contains("BasicRoadTL"))
                        {
                            if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Ground"))
                            {
                                tex = roadsmall + "TL" + gnd;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Elevated"))
                            {
                                tex = roadsmall + "TL" + elevated;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Tunnel"))
                            {
                                tex = roadsmall + "TL" + tunnel;
                            }
                        }

                        if (netInfo.name.Contains("Small Avenue"))
                        {
                            if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Ground"))
                            {
                                tex = roadsmall + "4L" + gnd;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Elevated"))
                            {
                                tex = roadsmall + "4L" + elevated;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Tunnel"))
                            {
                                tex = roadsmall + "4L" + tunnel;
                            }
                        }

                        if (netInfo.name.Contains("Oneway3L"))
                        {
                            if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Ground"))
                            {
                                tex = roadsmall + str + "3L" + gnd;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Elevated"))
                            {
                                tex = roadsmall + str + "3L" + elevated;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Tunnel"))
                            {
                                tex = roadsmall + str + "3L" + tunnel;
                            }
                        }

                        if (netInfo.name.Contains("Oneway4L"))
                        {
                            if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Ground"))
                            {
                                tex = roadsmall + str + "4L" + gnd;
                            }

                            if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Elevated"))
                            {
                                tex = roadsmall + str + "4L" + elevated;
                            }

                            if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Tunnel"))
                            {
                                tex = roadsmall + str + "4L" + tunnel;
                            }
                        }

                        if (netInfo.name.Contains("AsymRoadL1R2"))
                        {
                            if (netInfo.name.Equals("AsymRoadL1R2"))
                            {
                                tex = roadsmall + str4 + gnd;
                                if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Inverted"))
                                {
                                    tex += "_Inv";
                                }
                            }
                            else if (netInfo.name.Equals("AsymRoadL1R2 Elevated")
                                     || netInfo.name.Equals("AsymRoadL1R2 Bridge"))
                            {
                                if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Inverted"))
                                {
                                    tex = roadsmall + str4 + elevated + "_Inv";
                                }
                                else
                                {
                                    tex = roadsmall + str4 + elevated;
                                }
                            }
                            else if (netInfo.name.Equals("AsymRoadL1R2 Slope") && segment.m_mesh.name == "Slope")
                            {
                                if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Inverted"))
                                {
                                    tex = roadsmall + str4 + gnd + "_Inv";
                                }
                                else
                                {
                                    tex = roadsmall + str4 + gnd;
                                }
                            }
                            else if (netInfo.name.Equals("AsymRoadL1R2 Tunnel"))
                            {
                                if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Inverted"))
                                {
                                    tex = roadsmall + str4 + tunnel + "_Inv";
                                }
                                else
                                {
                                    tex = roadsmall + str4 + tunnel;
                                }
                            }
                        }

                        if (netInfo.name.Contains("AsymRoadL1R3"))
                        {
                            if (netInfo.name.Equals("AsymRoadL1R3"))
                            {
                                tex = roadsmall + str5 + gnd;
                                if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Inverted"))
                                {
                                    tex += "_Inv";
                                }
                            }
                            else if (netInfo.name.Equals("AsymRoadL1R3 Elevated")
                                     || netInfo.name.Equals("AsymRoadL1R3 Bridge"))
                            {
                                if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Inverted"))
                                {
                                    tex = roadsmall + str5 + elevated + "_Inv";
                                }
                                else
                                {
                                    tex = roadsmall + str5 + elevated;
                                }
                            }
                            else if (netInfo.name.Equals("AsymRoadL1R3 Slope") && segment.m_mesh.name == "Slope")
                            {
                                if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Inverted"))
                                {
                                    tex = roadsmall + str5 + gnd + "_Inv";
                                }
                                else
                                {
                                    tex = roadsmall + str5 + gnd;
                                }
                            }
                            else if (netInfo.name.Equals("AsymRoadL1R3 Tunnel"))
                            {
                                if (segment.m_segmentMaterial.GetTexture(TexType.APRMap).name.Contains("Inverted"))
                                {
                                    tex = roadsmall + str4 + tunnel + "_Inv";
                                }
                                else
                                {
                                    tex = roadsmall + str4 + tunnel;
                                }
                            }
                        }

                        if (netInfo.name.Contains("Medium Avenue") && !netInfo.name.Contains("TL"))
                        {
                            if (segment.m_segmentMaterial.GetTexture(TexType.MainTex).name.Contains("Ground"))
                            {
                                tex = roadmedium + "4L" + gnd;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(TexType.MainTex).name.Contains("Elevated"))
                            {
                                tex = roadmedium + "4L" + elevated;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(TexType.MainTex).name.Contains("Slope"))
                            {
                                tex = roadmedium + "4L" + slope;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(TexType.MainTex).name.Contains("Tunnel"))
                            {
                                tex = roadmedium + "4L" + tunnel;
                            }
                        }

                        if (netInfo.name.Contains("Medium Avenue") && netInfo.name.Contains("TL"))
                        {
                            if (segment.m_segmentMaterial.GetTexture(TexType.MainTex).name.Contains("Ground"))
                            {
                                tex = roadmedium + "4LTL" + gnd;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(TexType.MainTex).name.Contains("Elevated"))
                            {
                                tex = roadmedium + "4LTL" + elevated;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(TexType.MainTex).name.Contains("Slope"))
                            {
                                tex = roadmedium + "4LTL" + slope;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(TexType.MainTex).name.Contains("Tunnel"))
                            {
                                tex = roadmedium + "4LTL" + tunnel;
                            }
                        }

                        if (netInfo.name.Contains("Eight-Lane Avenue"))
                        {
                            tex = roadlarge + "8L";

                            if (netInfo.name.Equals("Eight-Lane Avenue Elevated")
                                || netInfo.name.ToLower().Contains("bridge"))
                            {
                                tex += elevated;
                            }
                            else if (netInfo.name.Equals("Eight-Lane Avenue Slope"))
                            {
                                tex += slope;
                            }
                            else if (netInfo.name.Equals("Eight-Lane Avenue Tunnel"))
                            {
                                tex += tunnel;
                            }
                            else
                            {
                                tex += gnd;
                            }
                        }

                        if (netInfo.name.Contains("Highway"))
                        {
                            if (netInfo.name.Contains("Rural Highway") && netInfo.name.Contains("Small"))
                            {
                                if (netInfo.name.Equals("Rural Highway Elevated"))
                                {
                                    tex = highway + "1L";
                                }
                                else if (netInfo.name.Equals("Rural Highway Slope"))
                                {
                                    tex = highway + "1L" + slope;
                                }
                                else if (netInfo.name.Equals("Rural Highway Tunnel"))
                                {
                                    tex = highway + "1L" + tunnel;
                                }
                                else
                                {
                                    tex = highway + "1L";
                                }
                            }

                            if (netInfo.name.Contains("Rural Highway") && !netInfo.name.Contains("Small"))
                            {
                                if (netInfo.name.Equals("Rural Highway Elevated"))
                                {
                                    tex = highway + "2L";
                                }
                                else if (netInfo.name.Equals("Rural Highway Slope"))
                                {
                                    tex = highway + "2L" + slope;
                                }
                                else if (netInfo.name.Equals("Rural Highway Tunnel"))
                                {
                                    tex = highway + "2L" + tunnel;
                                }
                                else
                                {
                                    tex = highway + "2L";
                                }
                            }

                            if (netInfo.name.Contains("Four-Lane Highway"))
                            {
                                if (netInfo.name.Equals("Four-Lane Highway Elevated"))
                                {
                                    tex = highway + "4L" + elevated;
                                }
                                else if (netInfo.name.Equals("Four-Lane Highway Slope"))
                                {
                                    tex = highway + "4L" + slope;
                                }
                                else if (netInfo.name.Equals("Four-Lane Highway Tunnel"))
                                {
                                    tex = highway + "4L" + tunnel;
                                }
                                else
                                {
                                    tex = highway + "4L" + gnd;
                                }
                            }

                            if (netInfo.name.Contains("Five-Lane Highway"))
                            {
                                if (netInfo.name.Equals("Five-Lane Highway Elevated"))
                                {
                                    tex = highway + "5L";
                                }
                                else if (netInfo.name.Equals("Five-Lane Highway Slope"))
                                {
                                    tex = highway + "5L" + slope;
                                }
                                else if (netInfo.name.Equals("Five-Lane Highway Tunnel"))
                                {
                                    tex = highway + "5L" + tunnel;
                                }
                                else
                                {
                                    tex = highway + "5L";
                                }
                            }

                            if (netInfo.name.Contains("Large Highway"))
                            {
                                if (netInfo.name.Equals("Large Highway Elevated"))
                                {
                                    tex = highway + "6L";
                                }
                                else if (netInfo.name.Equals("Large Highway Slope"))
                                {
                                    tex = highway + "6L" + slope;
                                }
                                else if (netInfo.name.Equals("Large Highway Tunnel"))
                                {
                                    tex = highway + "6L" + tunnel;
                                }
                                else
                                {
                                    tex = highway + "6L";
                                }
                            }
                        }

                        if (netInfo.name.Contains("Small"))
                        {
                            if (netInfo.name.Contains("Small Busway") && !netInfo.name.Contains("OneWay"))
                            {
                                if (segment.m_mesh.name.Equals("SmallRoadSegment"))
                                {
                                    tex = roadsmall + text6 + gnd;
                                }
                                else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                {
                                    tex = roadsmall + text6 + busside;
                                }
                                else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                                {
                                    tex = roadsmall + text6 + busboth;
                                }
                                else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                                {
                                    if (netInfo.name.Equals("Small Busway Elevated"))
                                    {
                                        tex = roadsmall + text6 + elevated;
                                    }
                                    else if (netInfo.name.Equals("Small Busway Slope"))
                                    {
                                        tex = roadsmall + text6 + slope;
                                    }
                                    else if (netInfo.name.Equals("Small Busway Tunnel"))
                                    {
                                        tex = roadsmall + text6 + tunnel;
                                    }
                                }
                            }

                            if (netInfo.name.Equals("Small Busway Decoration Grass")
                                || netInfo.name.Equals("Small Busway Decoration Trees"))
                            {
                                if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                                {
                                    tex = roadsmall + text6 + deco;
                                }
                                else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                                {
                                    tex = roadsmall + text6 + deco + busside;
                                }
                                else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                                {
                                    tex = roadsmall + text6 + deco + busboth;
                                }
                            }

                            if (netInfo.name.Contains("Small Busway OneWay"))
                            {
                                if (segment.m_mesh.name.Equals("SmallRoadSegment"))
                                {
                                    tex = roadsmall + str + text6 + gnd;
                                }
                                else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                {
                                    tex = roadsmall + str + text6 + busside;
                                }
                                else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                                {
                                    tex = roadsmall + str + text6 + busboth;
                                }
                                else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                                {
                                    if (netInfo.name.Equals("Small Busway OneWay Elevated"))
                                    {
                                        tex = roadsmall + str + text6 + elevated;
                                    }
                                    else if (netInfo.name.Equals("Small Busway OneWay Slope"))
                                    {
                                        tex = roadsmall + str + text6 + slope;
                                    }
                                    else if (netInfo.name.Equals("Small Busway OneWay Tunnel"))
                                    {
                                        tex = roadsmall + str + text6 + tunnel;
                                    }
                                }
                            }

                            if (netInfo.name.Equals("Small Busway OneWay Decoration Grass")
                                || netInfo.name.Equals("Small Busway OneWay Decoration Trees"))
                            {
                                if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                                {
                                    tex = roadsmall + str + text6 + deco;
                                }
                                else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                                {
                                    tex = roadsmall + str + deco + busside;
                                }
                                else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                                {
                                    tex = roadsmall + str + deco + busboth;
                                }
                            }
                        }

                        if (netInfo.name.Contains("Large"))
                        {
                            if (netInfo.name.Equals("Large Road With Bus Lanes"))
                            {
                                if (segment.m_mesh.name.Equals("RoadLargeSegment"))
                                {
                                    tex = roadlarge + text6 + gnd;
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                {
                                    tex = roadlarge + text6 + busside;
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                {
                                    tex = roadlarge + text6 + busboth;
                                }
                            }

                            if (netInfo.name.Equals("Large Road Elevated With Bus Lanes"))
                            {
                                tex = roadlarge + text6 + elevated;
                            }

                            if (netInfo.name.Equals("Large Road Slope With Bus Lanes"))
                            {
                                tex = roadlarge + text6 + slope;
                            }

                            if (netInfo.name.Equals("Large Road Tunnel With Bus Lanes"))
                            {
                                tex = roadlarge + text6 + tunnel;
                            }

                            if (netInfo.name.Equals("Large Road Decoration Trees With Bus Lanes")
                                || netInfo.name.Equals("Large Road Decoration Grass With Bus Lanes"))
                            {
                                if (segment.m_mesh.name.Equals("LargeRoadSegment2"))
                                {
                                    tex = roadlarge + text6 + deco;
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusSide"))
                                {
                                    tex = roadlarge + text6 + deco + busside;
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth"))
                                {
                                    tex = roadlarge + text6 + deco + busboth;
                                }
                            }
                        }

                        if (!tex.IsNullOrWhiteSpace())
                        {
                            segList.Add(new SegmentSet(segment, tex + TexType.MainTex, tex + TexType.APRMap));
                        }
                    }
                }
            }

            if (!netInfo.m_class.name.Contains("NExt") && !netInfo.m_class.name.Contains("Water")
                && !netInfo.m_class.name.Contains("Train") && !netInfo.m_class.name.Contains("Metro")
                && !netInfo.m_class.name.Contains("Transport") && !netInfo.m_class.name.Contains("Bus Line")
                && !netInfo.m_class.name.Contains("Airplane") && !netInfo.m_class.name.Contains("Ship")
                && !netInfo.name.Contains("Busway") && (!netInfo.name.Contains("Large Road")
                                                        || !netInfo.name.Contains("Bus Lane")))
            {
                NetInfo.Node[] nodes = netInfo.m_nodes;
                for (int i = 0; i < nodes.Length; i++)
                {
                    NetInfo.Node node = nodes[i];
                    if (node.m_nodeMaterial.GetTexture(TexType.MainTex) != null
                        && !node.m_nodeMaterial.name.Equals(tramRailDoubleWnNoName)
                        && !node.m_nodeMaterial.name.Equals(value2))
                    {
                        tex = roadsmall + gnd + text14;

                        if (netInfo.name.Contains("Basic Road"))
                        {
                            if (netInfo.name.Equals("Basic Road Elevated"))
                            {
                                tex = roadsmall + elevated + text14;
                            }
                            else if (netInfo.name.Equals("Basic Road Decoration Grass")
                                     || netInfo.name.Equals("Basic Road Decoration Trees"))
                            {
                                tex = roadsmall + deco + text14;
                            }
                            else if (netInfo.name.Equals("Basic Road Tram"))
                            {
                                tex = roadsmall + gnd + text14;
                            }
                            else if (netInfo.name.Equals("Basic Road Elevated Tram"))
                            {
                                tex = roadsmall + elevated + text14;
                            }
                            else if (netInfo.name.Equals("Basic Road Slope Tram"))
                            {
                                tex = roadsmall + gnd + text14;
                            }
                            else
                            {
                                tex = roadsmall + gnd + text14;
                            }
                        }
                        else if (netInfo.name.Contains("Oneway Road"))
                        {
                            tex = roadsmall + str;
                            if (netInfo.name.Equals("Oneway Road Elevated"))
                            {
                                tex = tex + elevated + text14;
                            }
                            else if (netInfo.name.Equals("Oneway Road Decoration Grass")
                                     || netInfo.name.Equals("Oneway Road Decoration Trees"))
                            {
                                tex = roadsmall + str + deco + text14;
                            }
                            else
                            {
                                tex += gnd + text14;
                            }
                        }
                        else if (netInfo.name.Equals("Basic Road Bicycle"))
                        {
                            tex = roadsmall + gnd + text14;
                        }
                        else if (netInfo.name.Equals("Basic Road Elevated Bike"))
                        {
                            tex = roadsmall + str2 + elevated + text14;
                        }
                        else if (netInfo.name.Contains("Large"))
                        {
                            if (netInfo.name.Equals("Large Road Elevated Bike"))
                            {
                                tex = roadlarge + str2 + elevated + text14;
                            }
                            else if (netInfo.name.Contains("Large Road"))
                            {
                                tex = roadlarge;
                                if (netInfo.name.Equals("Large Road Elevated"))
                                {
                                    tex = tex + elevated + text14;
                                }
                                else if (netInfo.name.Equals("Large Road Decoration Grass")
                                         || netInfo.name.Equals("Large Road Decoration Trees"))
                                {
                                    tex = roadlarge + deco + text14;
                                }
                                else
                                {
                                    tex += gnd + text14;
                                }
                            }
                            else if (netInfo.name.Contains("Large Oneway"))
                            {
                                tex = roadlarge + str;
                                if (netInfo.name.Equals("Large Oneway Elevated"))
                                {
                                    tex = tex + elevated + text14;
                                }
                                else if (netInfo.name.Equals("Large Oneway Decoration Grass")
                                         || netInfo.name.Equals("Large Oneway Decoration Trees"))
                                {
                                    tex = roadlarge + str + deco + text14;
                                }
                                else if (netInfo.name.Equals("Large Oneway Road Slope"))
                                {
                                    tex = roadlarge + str + slope + text14;
                                }
                                else
                                {
                                    tex = tex + gnd + text14;
                                }
                            }
                        }
                        else if (netInfo.name.Contains("Highway"))
                        {
                            if (netInfo.name.Equals("HighwayRamp"))
                            {
                                tex = highway + "Ramp" + text14;
                            }
                            else if (netInfo.name.Equals("HighwayRampElevated"))
                            {
                                tex = highway + "Ramp" + text14;
                            }
                            else if (netInfo.name.Equals("HighwayRamp Slope"))
                            {
                                tex = highway + "Ramp" + text14;
                            }
                            else if (netInfo.name.Equals("Highway Elevated"))
                            {
                                tex = highway + "3L" + text14;
                            }
                            else if (netInfo.name.Equals("Highway Slope"))
                            {
                                tex = highway + "3L" + text14;
                            }
                            else if (netInfo.name.Equals("Highway Barrier"))
                            {
                                tex = highway + "3L" + text14;
                            }
                            else if (netInfo.name.Equals("Highway"))
                            {
                                tex = highway + "3L" + text14;
                            }
                        }
                        else if (netInfo.name.Equals("Oneway Road Tram"))
                        {
                            tex = roadsmall + str + gnd + text14;
                        }
                        else if (netInfo.name.Equals("Oneway Road Elevated Tram"))
                        {
                            tex = roadsmall + str + elevated + text14;
                        }
                        else if (netInfo.name.Equals("Oneway Road Slope Tram"))
                        {
                            tex = roadsmall + str + gnd + text14;
                        }
                        else if (netInfo.name.Contains("Medium"))
                        {
                            if (netInfo.name.Equals("Medium Road Tram"))
                            {
                                tex = roadmedium + str3 + gnd + text14;
                            }
                            else if (netInfo.name.Equals("Medium Road Elevated Tram"))
                            {
                                tex = roadmedium + str3 + elevated + text14;
                            }
                            else if (netInfo.name.Equals("Medium Road Slope Tram"))
                            {
                                tex = roadmedium + str3 + gnd + text14;
                            }
                            else if (netInfo.name.Contains("Medium Road") && !netInfo.name.Contains("Tram"))
                            {
                                tex = roadmedium;
                                if (netInfo.name.Contains("Medium Road Elevated") && !netInfo.name.Contains("Bike"))
                                {
                                    tex = tex + elevated + text14;
                                }
                                else if (netInfo.name.Equals("Medium Road Decoration Grass")
                                         || netInfo.name.Equals("Medium Road Decoration Trees"))
                                {
                                    tex = roadmedium + deco + text14;
                                }
                                else
                                {
                                    tex = tex + gnd + text14;
                                }
                            }
                            else if (netInfo.name.Equals("Medium Road Elevated Bike"))
                            {
                                tex = roadmedium + str2 + elevated + text14;
                            }
                        }
                        else if (netInfo.name.Equals("Tram Track"))
                        {
                            tex = roadsmall + deco + text14;
                        }
                        else if (netInfo.name.Equals("Tram Track Elevated"))
                        {
                            tex = roadsmall + elevated + text14;
                        }
                        else if (netInfo.name.Equals("Tram Track Slope"))
                        {
                            tex = roadsmall + deco + text14;
                        }
                        else if (netInfo.name.Equals("Oneway Tram Track"))
                        {
                            tex = roadsmall + deco + text14;
                        }
                        else if (netInfo.name.Equals("Oneway Tram Track Elevated"))
                        {
                            tex = roadsmall + elevated + text14;
                        }
                        else if (netInfo.name.Equals("Oneway Tram Track Slope"))
                        {
                            tex = roadsmall + deco + text14;
                        }

                        if (!tex.IsNullOrWhiteSpace())
                        {
                            nodeList.Add(new NodeSet(node, tex + TexType.MainTex, tex + TexType.APRMap));
                        }
                    }
                }

                NetInfo.Segment[] segments = netInfo.m_segments;
                for (int j = 0; j < segments.Length; j++)
                {
                    NetInfo.Segment segment = segments[j];
                    if (segment.m_segmentMaterial.GetTexture("_MainTex") != null
                        && !segment.m_segmentMaterial.name.Contains("rail")
                        && !segment.m_material.name.ToLower().Contains("cable"))
                    {
                        if (netInfo.name.Contains("Basic Road"))
                        {
                            tex = roadsmall;
                            if (segment.m_segmentMesh.name.Equals("SmallRoadSegment"))
                            {
                                tex += gnd;
                            }
                            else if (segment.m_segmentMesh.name.Equals("SmallRoadSegmentBusSide"))
                            {
                                tex += busside;
                            }
                            else if (segment.m_segmentMesh.name.Equals("SmallRoadSegmentBusBoth"))
                            {
                                tex += busboth;
                            }
                            else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                            {
                                if (netInfo.name.Equals("Basic Road Elevated")
                                    || netInfo.name.Equals("Basic Road Bridge"))
                                {
                                    tex += elevated;
                                }
                                else if (netInfo.name.Equals("Basic Road Slope"))
                                {
                                    tex += slope;
                                }
                            }

                            if (netInfo.name.Equals("Basic Road Bicycle"))
                            {
                                if (segment.m_mesh.name.Equals("SmallRoadSegment"))
                                {
                                    tex = roadsmall + str2 + gnd;
                                }
                                else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                {
                                    tex = roadsmall + str2 + busside;
                                    if (ModLoader.Config.basic_road_parking == 1)
                                    {
                                    }
                                }
                                else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                                {
                                    tex = roadsmall + str2 + busboth;
                                }
                            }

                            if (netInfo.name.Equals("Basic Road Elevated Bike"))
                            {
                                tex = roadsmall + str2 + elevated;
                            }

                            if (netInfo.name.Equals("Basic Road Decoration Grass")
                                || netInfo.name.Equals("Basic Road Decoration Trees"))
                            {
                                if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                                {
                                    tex = roadsmall + deco;
                                }
                                else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                                {
                                    tex = roadsmall + deco + busside;
                                }
                                else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                                {
                                    tex = roadsmall + deco + busboth;
                                }
                            }

                            if (netInfo.name.Equals("Basic Road Tram"))
                            {
                                if (segment.m_mesh.name.Equals("RoadSmallTramStopSingle"))
                                {
                                    tex = roadsmall + busboth;
                                }
                                else if (segment.m_mesh.name.Equals("RoadSmallTramStopDouble"))
                                {
                                    tex = roadsmall + busboth;
                                }
                                else if (segment.m_mesh.name.Equals("RoadSmallTramAndBusStop"))
                                {
                                    tex = roadsmall + busboth;
                                }
                            }
                            else if (netInfo.name.Equals("Basic Road Elevated Tram")
                                     || netInfo.name.Equals("Basic Road Bridge Tram"))
                            {
                                tex = roadsmall + elevated;
                            }
                            else if (netInfo.name.Equals("Basic Road Slope Tram"))
                            {
                                tex = roadsmall + slope;
                            }
                        }

                        if (netInfo.name.Contains("Oneway Road"))
                        {
                            tex = roadsmall + str;
                            if (segment.m_mesh.name.Equals("SmallRoadSegment"))
                            {
                                tex += gnd;
                            }
                            else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                            {
                                tex = roadsmall + str + busside;
                            }
                            else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                            {
                                tex = roadsmall + str + busboth;
                            }
                            else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                            {
                                if (netInfo.name.Equals("Oneway Road Elevated")
                                    || netInfo.name.Equals("Oneway Road Bridge"))
                                {
                                    tex = roadsmall + str + elevated;
                                }
                                else if (netInfo.name.Equals("Oneway Road Slope"))
                                {
                                    tex = roadsmall + str + slope;
                                }
                            }
                        }

                        if (netInfo.name.Equals("Oneway Road Decoration Grass")
                            || netInfo.name.Equals("Oneway Road Decoration Trees"))
                        {
                            if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                            {
                                tex = roadsmall + str + deco;
                            }
                            else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                            {
                                tex = roadsmall + str + deco + busboth;
                            }
                            else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                            {
                                tex = roadsmall + str + deco + busboth;
                            }
                        }

                        if (netInfo.name.Contains("Medium"))
                        {

                            if (netInfo.name.Contains("Medium Road"))
                            {
                                tex = roadmedium;
                                if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                                {
                                    tex += gnd;
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                {
                                    tex += busside;
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                {
                                    tex += busboth;
                                }
                                else if (!segment.m_mesh.name.Equals("RoadMediumSegment"))
                                {
                                    if (netInfo.name.Equals("Medium Road Elevated")
                                        || netInfo.name.ToLower().Contains("bridge"))
                                    {
                                        tex += elevated;
                                    }

                                    if (netInfo.name.Equals("Medium Road Slope"))
                                    {
                                        tex += slope;
                                    }
                                }
                            }

                            if (netInfo.name.Equals("Medium Road Decoration Grass")
                                || netInfo.name.Equals("Medium Road Decoration Trees"))
                            {
                                tex = roadmedium + deco;
                                if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                                {
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                {
                                    tex += busside;
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                {
                                    tex += busboth;
                                }
                            }

                            if (netInfo.name.Equals("Medium Road Bicycle"))
                            {
                                tex = roadmedium + str2;
                                if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                                {
                                    tex += gnd;
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                {
                                    tex += busside;
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                {
                                    tex += busboth;
                                }
                            }

                            if (netInfo.name.Equals("Medium Road Elevated Bike")
                                || netInfo.name.Equals("Medium Road Bridge Bike"))
                            {
                                tex = roadmedium + str2 + elevated;
                            }

                            if (netInfo.name.Equals("Medium Road Tram"))
                            {
                                if (segment.m_mesh.name.Equals("RoadMediumTramSegment"))
                                {
                                    tex = roadmedium + str3 + gnd;
                                }
                            }
                            else if (netInfo.name.Equals("Medium Road Elevated Tram")
                                     || netInfo.name.Equals("Medium Road Bridge Tram"))
                            {
                                tex = roadmedium + str3 + elevated;
                            }
                            else if (netInfo.name.Equals("Medium Road Slope Tram"))
                            {
                                tex = roadmedium + str3 + slope;
                            }

                            if (netInfo.name.Equals("Medium Road Bus"))
                            {
                                if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                                {
                                    tex = roadmedium + text6 + gnd;
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                {
                                    tex = roadmedium + text6 + busside;
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                {
                                    tex = roadmedium + text6 + busboth;
                                }
                            }
                            else if (netInfo.name.Equals("Medium Road Elevated Bus")
                                     || netInfo.name.Equals("Medium Road Bridge Bus"))
                            {
                                tex = roadmedium + text6 + elevated;
                            }
                            else if (netInfo.name.Equals("Medium Road Slope Bus"))
                            {
                                tex = roadmedium + text6 + slope;
                            }
                        }

                        if (netInfo.name.Contains("Large"))
                        {
                            if (netInfo.name.Contains("Large Road"))
                            {
                                tex = roadlarge;
                                if (segment.m_mesh.name.Equals("RoadLargeSegment"))
                                {
                                    tex += gnd;
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                {
                                    tex += busside;
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                {
                                    tex += busboth;
                                }
                                else if (!segment.m_mesh.name.Equals("RoadLargeSegment"))
                                {
                                    if (netInfo.name.Equals("Large Road Elevated")
                                        || netInfo.name.ToLower().Contains("bridge"))
                                    {
                                        tex += elevated;
                                    }

                                    if (netInfo.name.Equals("Large Road Slope"))
                                    {
                                        tex += slope;
                                    }
                                }
                            }

                            if (netInfo.name.Equals("Large Road Decoration Grass")
                                || netInfo.name.Equals("Large Road Decoration Trees"))
                            {
                                tex = roadlarge + deco;
                                if (segment.m_mesh.name.Equals("LargeRoadSegment2"))
                                {
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusSide"))
                                {
                                    tex += busside;
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth"))
                                {
                                    tex += busboth;
                                }
                            }

                            if (netInfo.name.Equals("Large Road Bicycle"))
                            {
                                tex = roadlarge + str2;
                                if (segment.m_mesh.name.Equals("RoadLargeSegment"))
                                {
                                    tex += gnd;
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                {
                                    tex += busside;
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                {
                                    tex += busboth;
                                }
                            }

                            if (netInfo.name.Equals("Large Road Elevated Bike")
                                || netInfo.name.Equals("Large Road Bridge Bike"))
                            {
                                tex = roadlarge + str2 + elevated;
                            }

                            if (netInfo.name.Contains("Large Oneway"))
                            {
                                tex = roadlarge + str;
                                if (segment.m_mesh.name.Equals("RoadLargeSegment"))
                                {
                                    tex += gnd;
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                {
                                    tex += busside;
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                {
                                    tex += busboth;
                                }
                                else if (!segment.m_mesh.name.Equals("RoadLargeSegment"))
                                {
                                    if (netInfo.name.Equals("Large Oneway Elevated")
                                        || netInfo.name.ToLower().Contains("bridge"))
                                    {
                                        tex += elevated;
                                    }
                                }
                            }

                            if (netInfo.name.Equals("Large Oneway Road Slope"))
                            {
                                tex = roadlarge + str + slope;
                            }

                            if (netInfo.name.Equals("Large Oneway Decoration Grass")
                                || netInfo.name.Equals("Large Oneway Decoration Trees"))
                            {
                                tex = roadlarge + str + deco;
                                if (segment.m_mesh.name.Equals("LargeRoadSegment2"))
                                {
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusSide"))
                                {
                                    tex += busside;
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth"))
                                {
                                    tex += busboth;
                                }
                            }

                            if (netInfo.name.Equals("Large Road Bus"))
                            {
                                if (segment.m_mesh.name.Equals("RoadLargeSegmentBusLane"))
                                {
                                    tex = roadlarge + text6 + gnd;
                                }
                                else if (segment.m_mesh.name.Equals("RoadLargeSegmentBusSideBusLane"))
                                {
                                    tex = roadlarge + text6 + busside;
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBothBusLane"))
                                {
                                    tex = roadlarge + text6 + busboth;
                                }
                            }
                            else if (netInfo.name.Equals("Large Road Elevated Bus")
                                     || netInfo.name.Equals("Large Road Bridge Bus"))
                            {
                                tex = roadlarge + text6 + elevated;
                            }
                            else if (netInfo.name.Equals("Large Road Slope Bus"))
                            {
                                tex = roadlarge + text6 + slope;
                            }
                        }

                        if (netInfo.name.Contains("Highway"))
                        {
                            if (netInfo.name.Equals("HighwayRamp") || netInfo.name.Equals("HighwayRampElevated"))
                            {
                                tex = highway + "Ramp";
                            }
                            else if (netInfo.name.Equals("HighwayRamp Slope"))
                            {
                                tex = highway + "Ramp" + slope;
                            }
                            else if (netInfo.name.Equals("Highway Barrier"))
                            {
                                tex = highway + "3L";
                            }
                            else if (netInfo.name.Equals("Highway Elevated")
                                     || segment.m_mesh.name.Equals("HighwayBridgeSegment")
                                     || segment.m_mesh.name.Equals("HighwayBaseSegment")
                                     || segment.m_mesh.name.Equals("HighwayBarrierSegment"))
                            {
                                tex = highway + "3L";
                            }
                            else if (segment.m_mesh.name.Equals("highway-tunnel-segment")
                                     || segment.m_mesh.name.Equals("highway-tunnel-slope"))
                            {
                                tex = highway + "3L" + slope;
                            }
                        }

                        if (netInfo.name.Equals("Oneway Road Tram"))
                        {
                            tex = roadsmall + str;
                            if (segment.m_mesh.name.Equals("RoadSmallTramStopSingle"))
                            {
                                tex = roadsmall + str + busboth;
                            }
                            else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                            {
                                tex = roadsmall + str + busboth;
                            }
                        }
                        else if (netInfo.name.Equals("Oneway Road Elevated Tram")
                                 || netInfo.name.Equals("Oneway Road Bridge Tram"))
                        {
                            tex = roadsmall + str + elevated;
                        }
                        else if (netInfo.name.Equals("Oneway Road Slope Tram"))
                        {
                            tex = roadsmall + str + slope;
                        }

                        if (netInfo.name.Contains("Tram Track"))
                        {
                            tex = roadsmall + deco;
                            if (netInfo.name.Equals("Tram Track Elevated"))
                            {
                                tex = roadsmall + str + elevated;
                            }
                            else if (netInfo.name.Equals("Tram Track Slope"))
                            {
                                tex = roadsmall + str + slope;
                            }
                            else if (netInfo.name.Equals("Oneway Tram Track"))
                            {
                                tex = roadsmall + deco;
                            }
                            else if (netInfo.name.Equals("Oneway Tram Track Elevated"))
                            {
                                tex = roadsmall + str + elevated;
                            }
                            else if (netInfo.name.Equals("Oneway Tram Track Slope"))
                            {
                                tex = roadsmall + str + slope;
                            }
                        }

                        if (!tex.IsNullOrWhiteSpace())
                        {
                            segList.Add(new SegmentSet(segment, tex + TexType.MainTex, tex + TexType.APRMap));
                        }
                    }
                }
            }
        }
    }
}

