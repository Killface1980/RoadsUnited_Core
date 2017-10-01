using System.IO;

using UnityEngine;

namespace RoadsUnited_Core
{
    public static class ShittyPlusFuck
    {
        // I'm not touching this overcomplicated pice of shit code
        public static void ReplaceShitFuckingPlus()
        {
            string texturesPathDefault = ModLoader.currentTexturesPath_default;
            string maintex = "_MainTex";
            string aprmap = "_APRMap";
            string roadtiny = "RoadTiny";
            string roadsmall = "RoadSmall";
            string roadmedium = "RoadMedium";
            string roadlarge = "RoadLarge";
            string highway = "Highway";
            string str = "Oneway";
            string str2 = "Bike";
            string text6 = "BusLane";
            string str3 = "Tram";
            string str4 = "L1R2";
            string str5 = "L1R3";
            string text7 = "Gnd";
            string deco = "Deco";
            string busside = "BusSide";
            string busboth = "BusBoth";
            string elevated = "Elevated";
            string slope = "Slope";
            string tunnel = "Tunnel";
            string text14 = "_Node";
            string tramRailDoubleWnNoName = "tram-rail-double-wn-No Name";
            string value2 = "tram-rail-double-No Name";
            string maintexDds = "_MainTex.dds";
            string aprmapDds = "_APRMap.dds";
            string lodMaintexDds = "_LOD_MainTex.dds";
            string tex = string.Empty;
            string path = string.Empty;
            string path2 = texturesPathDefault + "/" + roadtiny + "/";
            string path3 = texturesPathDefault + "/" + roadsmall + "/";
            string path4 = texturesPathDefault + "/" + roadmedium + "/";
            string path5 = texturesPathDefault + "/" + roadlarge + "/";
            string path6 = texturesPathDefault + "/" + highway + "/";


            uint num = 0u;
            while ((ulong)num < (ulong)((long)PrefabCollection<NetInfo>.LoadedCount()))
            {
                NetInfo loaded = PrefabCollection<NetInfo>.GetLoaded(num);
                if (!(loaded == null))
                {
                    if (loaded.m_class.name.Contains("NExt") || loaded.name.Contains("Busway") || (loaded.name.Contains("Large Road") && loaded.name.Contains("Bus Lane")))
                    {
                        NetInfo.Node[] nodes = loaded.m_nodes;
                        for (int i = 0; i < nodes.Length; i++)
                        {
                            NetInfo.Node node = nodes[i];
                            if (node.m_nodeMaterial.GetTexture(maintex) != null && !node.m_nodeMaterial.name.Contains("rail"))
                            {
                                if (loaded.name.Equals("Two-Lane Alley"))
                                {
                                    tex = roadtiny + "2L" + text14;
                                    if (File.Exists(Path.Combine(path2, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path2, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path2, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path2, tex + aprmapDds)));
                                        }
                                    }
                                    else
                                    {
                                        tex = roadtiny + "1L" + text14;
                                        if (File.Exists(Path.Combine(path2, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path2, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path2, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path2, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("One-Lane Oneway"))
                                {
                                    tex = roadtiny + "1L" + text14;

                                    if (File.Exists(Path.Combine(path2, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path2, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path2, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path2, tex + aprmapDds)));
                                        }
                                    }
                                    else
                                    {
                                        if (File.Exists(Path.Combine(path2, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path2, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Contains("Eight-Lane Avenue"))
                                {
                                    tex = roadlarge + "8L";
                                    if (loaded.name.Equals("Eight-Lane Avenue Elevated"))
                                    {
                                        tex = tex + elevated + text14;
                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Eight-Lane Avenue Slope"))
                                    {
                                        tex = tex + slope + text14;
                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Eight-Lane Avenue Tunnel"))
                                    {
                                        tex = tex + tunnel + text14;
                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        tex = tex + text7 + text14;

                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }

    
                                }

                                if (loaded.name.Contains("Small Rural Highway"))
                                {
                                    tex = highway + "1L" + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Small Rural Highway Elevated"))
                                {
                                    tex = highway + "1L" + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Small Rural Highway Slope") && loaded.name.Contains("Small"))
                                {
                                    tex = highway + "1L" + slope + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Small Rural Highway Tunnel"))
                                {
                                    tex = highway + "1L" + tunnel + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Rural Highway"))
                                {
                                    tex = highway + "2L" + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Rural Highway Elevated"))
                                {
                                    tex = highway + "2L" + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Rural Highway Slope"))
                                {
                                    tex = highway + "2L" + slope + text14;

                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Rural Highway Tunnel"))
                                {
                                    tex = highway + "2L" + tunnel + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Four-Lane Highway"))
                                {
                                    tex = highway + "4L" + text7 + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Four-Lane Highway Elevated"))
                                {
                                    tex = highway + "4L" + elevated + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Four-Lane Highway Slope"))
                                {
                                    tex = highway + "4L" + slope + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Four-Lane Highway Tunnel"))
                                {
                                    tex = highway + "4L" + tunnel + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Five-Lane Highway"))
                                {
                                    tex = highway + "5L" + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        tex = highway + "5L" + text7 + text14;
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Five-Lane Highway Elevated"))
                                {
                                    tex = highway + "5L" + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        tex = highway + "5L" + elevated + text14;
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Five-Lane Highway Slope"))
                                {
                                    tex = highway + "5L" + text7 + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Five-Lane Highway Tunnel"))
                                {
                                    tex = highway + "5L" + tunnel + text14;

                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Large Highway"))
                                {
                                    tex = highway + "6L" + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        tex = highway + "6L" + text7 + text14;
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Large Highway Elevated"))
                                {
                                    tex = highway + "6L" + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        tex = highway + "6L" + elevated + text14;
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Large Highway Slope"))
                                {
                                    tex = highway + "6L" + text7 + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                                if (loaded.name.Equals("Large Highway Tunnel"))
                                {
                                    tex = highway + "6L" + tunnel + text14;
                                    if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }

    
                                }

                            }
                        }

                        NetInfo.Segment[] segments = loaded.m_segments;
                        for (int j = 0; j < segments.Length; j++)
                        {
                            NetInfo.Segment segment = segments[j];
                            if (segment.m_segmentMaterial.GetTexture("_MainTex") != null && !segment.m_material.name.ToLower().Contains("cable"))
                            {
                                if (loaded.name.Equals("Two-Lane Alley"))
                                {
                                    tex = roadtiny + "2L";
                                    if (File.Exists(Path.Combine(path2, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path2, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path2, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path2, tex + aprmapDds)));
                                        }
                                    }
                                    else
                                    {
                                        tex = roadtiny + "1L";
                                        if (File.Exists(Path.Combine(path2, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path2, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path2, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path2, tex + aprmapDds)));
                                        }
                                    }
                                }

                                if (loaded.name.Equals("One-Lane Oneway"))
                                {
                                    tex = roadtiny + "1L";

                                    if (File.Exists(Path.Combine(path2, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path2, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path2, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path2, tex + aprmapDds)));
                                        }
                                    }
                                }

                                if (loaded.name.Contains("BasicRoadTL"))
                                {
                                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                    {
                                        tex = roadsmall + "TL" + text7;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                    else if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                    {
                                        tex = roadsmall + "TL" + elevated;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                    else if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                                    {
                                        tex = roadsmall + "TL" + tunnel;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }


                                }

                                if (loaded.name.Contains("Small Avenue"))
                                {
                                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                    {
                                        tex = roadsmall + "4L" + text7;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                    else if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                    {
                                        tex = roadsmall + "4L" + elevated;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                    else if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                                    {
                                        tex = roadsmall + "4L" + tunnel;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }


                                }

                                if (loaded.name.Contains("Oneway3L"))
                                {
                                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                    {
                                        tex = roadsmall + str + "3L" + text7;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                    else if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                    {
                                        tex = roadsmall + str + "3L" + elevated;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                    else if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                                    {
                                        tex = roadsmall + str + "3L" + tunnel;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }


                                }

                                if (loaded.name.Contains("Oneway4L"))
                                {
                                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                    {
                                        tex = roadsmall + str + "4L" + text7;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }

                                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                    {
                                        tex = roadsmall + str + "4L" + elevated;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }

                                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                                    {
                                        tex = roadsmall + str + "4L" + tunnel;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }


                                }

                                if (loaded.name.Contains("AsymRoadL1R2"))
                                {
                                    if (loaded.name.Equals("AsymRoadL1R2"))
                                    {
                                        tex = roadsmall + str4 + text7;
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Inverted"))
                                        {
                                            tex += "_Inv";

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("AsymRoadL1R2 Elevated") || loaded.name.Equals("AsymRoadL1R2 Bridge"))
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Inverted"))
                                        {
                                            tex = roadsmall + str4 + elevated + "_Inv";

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tex = roadsmall + str4 + elevated;

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("AsymRoadL1R2 Slope") && segment.m_mesh.name == "Slope")
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Inverted"))
                                        {
                                            tex = roadsmall + str4 + text7 + "_Inv";

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tex = roadsmall + str4 + text7;

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("AsymRoadL1R2 Tunnel"))
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Inverted"))
                                        {
                                            tex = roadsmall + str4 + tunnel + "_Inv";

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tex = roadsmall + str4 + tunnel;

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }


                                }

                                if (loaded.name.Contains("AsymRoadL1R3"))
                                {
                                    if (loaded.name.Equals("AsymRoadL1R3"))
                                    {
                                        tex = roadsmall + str5 + text7;
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Inverted"))
                                        {
                                            tex += "_Inv";

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("AsymRoadL1R3 Elevated") || loaded.name.Equals("AsymRoadL1R3 Bridge"))
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Inverted"))
                                        {
                                            tex = roadsmall + str5 + elevated + "_Inv";

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tex = roadsmall + str5 + elevated;

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("AsymRoadL1R3 Slope") && segment.m_mesh.name == "Slope")
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Inverted"))
                                        {
                                            tex = roadsmall + str5 + text7 + "_Inv";

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tex = roadsmall + str5 + text7;

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("AsymRoadL1R3 Tunnel"))
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Inverted"))
                                        {
                                            tex = roadsmall + str4 + tunnel + "_Inv";

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tex = roadsmall + str4 + tunnel;

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }


                                }

                                if (loaded.name.Contains("Medium Avenue") && !loaded.name.Contains("TL"))
                                {
                                    if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Ground"))
                                    {
                                        tex = roadmedium + "4L" + text7;

                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Elevated"))
                                    {
                                        tex = roadmedium + "4L" + elevated;

                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Slope"))
                                    {
                                        tex = roadmedium + "4L" + slope;

                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Tunnel"))
                                    {
                                        tex = roadmedium + "4L" + tunnel;

                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }


                                }

                                if (loaded.name.Contains("Medium Avenue") && loaded.name.Contains("TL"))
                                {
                                    if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Ground"))
                                    {
                                        tex = roadmedium + "4LTL" + text7;

                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Elevated"))
                                    {
                                        tex = roadmedium + "4LTL" + elevated;

                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Slope"))
                                    {
                                        tex = roadmedium + "4LTL" + slope;

                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Tunnel"))
                                    {
                                        tex = roadmedium + "4LTL" + tunnel;

                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }


                                }

                                if (loaded.name.Contains("Eight-Lane Avenue"))
                                {
                                    tex = roadlarge + "8L";

                                    if (loaded.name.Equals("Eight-Lane Avenue Elevated") || loaded.name.ToLower().Contains("bridge"))
                                    {
                                        tex += elevated;

                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Eight-Lane Avenue Slope"))
                                    {
                                        tex += slope;

                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                        }
                                    }
                                    else if (loaded.name.Equals("Eight-Lane Avenue Tunnel"))
                                    {
                                        tex += tunnel;

                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        tex += text7;

                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                }

                                if (loaded.name.Contains("Rural Highway") && loaded.name.Contains("Small"))
                                {
                                    if (loaded.name.Equals("Rural Highway Elevated"))
                                    {
                                        tex = highway + "1L";

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }
                                    else if (loaded.name.Equals("Rural Highway Slope"))
                                    {
                                        tex = highway + "1L" + slope;

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }
                                    else if (loaded.name.Equals("Rural Highway Tunnel"))
                                    {
                                        tex = highway + "1L" + tunnel;

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }
                                    else
                                    {
                                        tex = highway + "1L";

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }


                                }

                                if (loaded.name.Contains("Rural Highway") && !loaded.name.Contains("Small"))
                                {
                                    if (loaded.name.Equals("Rural Highway Elevated"))
                                    {
                                        tex = highway + "2L";

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }
                                    else if (loaded.name.Equals("Rural Highway Slope"))
                                    {
                                        tex = highway + "2L" + slope;

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }
                                    else if (loaded.name.Equals("Rural Highway Tunnel"))
                                    {
                                        tex = highway + "2L" + tunnel;

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }
                                    else
                                    {
                                        tex = highway + "2L";

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }


                                }

                                if (loaded.name.Contains("Four-Lane Highway"))
                                {
                                    if (loaded.name.Equals("Four-Lane Highway Elevated"))
                                    {
                                        tex = highway + "4L" + elevated;

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }
                                    else if (loaded.name.Equals("Four-Lane Highway Slope"))
                                    {
                                        tex = highway + "4L" + slope;

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }
                                    else if (loaded.name.Equals("Four-Lane Highway Tunnel"))
                                    {
                                        tex = highway + "4L" + tunnel;

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }
                                    else
                                    {
                                        tex = highway + "4L" + text7;

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }


                                }

                                if (loaded.name.Contains("Five-Lane Highway"))
                                {
                                    if (loaded.name.Equals("Five-Lane Highway Elevated"))
                                    {
                                        tex = highway + "5L";

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }
                                    else if (loaded.name.Equals("Five-Lane Highway Slope"))
                                    {
                                        tex = highway + "5L" + slope;

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }
                                    else if (loaded.name.Equals("Five-Lane Highway Tunnel"))
                                    {
                                        tex = highway + "5L" + tunnel;

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }
                                    else
                                    {
                                        tex = highway + "5L";

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }


                                }

                                if (loaded.name.Contains("Large Highway"))
                                {
                                    if (loaded.name.Equals("Large Highway Elevated"))
                                    {
                                        tex = highway + "6L";

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }
                                    else if (loaded.name.Equals("Large Highway Slope"))
                                    {
                                        tex = highway + "6L" + slope;

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }
                                    else if (loaded.name.Equals("Large Highway Tunnel"))
                                    {
                                        tex = highway + "6L" + tunnel;

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }
                                    else
                                    {
                                        tex = highway + "6L";

                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }
                                    }


                                }

                                if (loaded.name.Contains("Small Busway") && !loaded.name.Contains("OneWay"))
                                {
                                    if (segment.m_mesh.name.Equals("SmallRoadSegment"))
                                    {
                                        tex = roadsmall + text6 + text7;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                    {
                                        tex = roadsmall + text6 + busside;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                                    {
                                        tex = roadsmall + text6 + busboth;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                                    {
                                        if (loaded.name.Equals("Small Busway Elevated"))
                                        {
                                            tex = roadsmall + text6 + elevated;

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("Small Busway Slope"))
                                        {
                                            tex = roadsmall + text6 + slope;

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("Small Busway Tunnel"))
                                        {
                                            tex = roadsmall + text6 + tunnel;
                                        }

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }


                                }

                                if (loaded.name.Equals("Small Busway Decoration Grass") || loaded.name.Equals("Small Busway Decoration Trees"))
                                {
                                    if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                                    {
                                        tex = roadsmall + text6 + deco;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                                    {
                                        tex = roadsmall + text6 + deco + busside;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                                    {
                                        tex = roadsmall + text6 + deco + busboth;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }


                                }

                                if (loaded.name.Contains("Small Busway OneWay"))
                                {
                                    if (segment.m_mesh.name.Equals("SmallRoadSegment"))
                                    {
                                        tex = roadsmall + str + text6 + text7;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                    {
                                        tex = roadsmall + str + text6 + busside;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                                    {
                                        tex = roadsmall + str + text6 + busboth;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                                    {
                                        if (loaded.name.Equals("Small Busway OneWay Elevated"))
                                        {
                                            tex = roadsmall + str + text6 + elevated;

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("Small Busway OneWay Slope"))
                                        {
                                            tex = roadsmall + str + text6 + slope;

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("Small Busway OneWay Tunnel"))
                                        {
                                            tex = roadsmall + str + text6 + tunnel;
                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }


                                }

                                if (loaded.name.Equals("Small Busway OneWay Decoration Grass") || loaded.name.Equals("Small Busway OneWay Decoration Trees"))
                                {
                                    if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                                    {
                                        tex = roadsmall + str + text6 + deco;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                                    {
                                        tex = roadsmall + str + deco + busside;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                                    {
                                        tex = roadsmall + str + deco + busboth;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }


                                }

                                if (loaded.name.Equals("Large Road With Bus Lanes"))
                                {
                                    if (segment.m_mesh.name.Equals("RoadLageSegment"))
                                    {
                                        tex = roadlarge + text6 + text7;

                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                    {
                                        tex = roadlarge + text6 + busside;
                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                    {
                                        tex = roadlarge + text6 + busboth;
                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }


                                }

                                if (loaded.name.Equals("Large Road Elevated With Bus Lanes"))
                                {
                                    tex = roadlarge + text6 + elevated;

                                    if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                        }
                                    }


                                }

                                if (loaded.name.Equals("Large Road Slope With Bus Lanes"))
                                {
                                    tex = roadlarge + text6 + slope;

                                    if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                        }
                                    }


                                }

                                if (loaded.name.Equals("Large Road Tunnel With Bus Lanes"))
                                {
                                    tex = roadlarge + text6 + tunnel;
                                    if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                        }
                                    }


                                }

                                if (loaded.name.Equals("Large Road Decoration Trees With Bus Lanes") || loaded.name.Equals("Large Road Decoration Grass With Bus Lanes"))
                                {
                                    if (segment.m_mesh.name.Equals("LargeRoadSegment2"))
                                    {
                                        tex = roadlarge + text6 + deco;

                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusSide"))
                                    {
                                        tex = roadlarge + text6 + deco + busside;
                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth"))
                                    {
                                        tex = roadlarge + text6 + deco + busboth;
                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }


                                }
                            }

                        }
                    }

                    if (!loaded.m_class.name.Contains("NExt") && !loaded.m_class.name.Contains("Water") && !loaded.m_class.name.Contains("Train") && !loaded.m_class.name.Contains("Metro") && !loaded.m_class.name.Contains("Transport") && !loaded.m_class.name.Contains("Bus Line") && !loaded.m_class.name.Contains("Airplane") && !loaded.m_class.name.Contains("Ship") && !loaded.name.Contains("Busway") && (!loaded.name.Contains("Large Road") || !loaded.name.Contains("Bus Lane")))
                    {
                        NetInfo.Node[] nodes = loaded.m_nodes;
                        for (int i = 0; i < nodes.Length; i++)
                        {
                            NetInfo.Node node = nodes[i];
                            if (node.m_nodeMaterial.GetTexture(maintex) != null && !node.m_nodeMaterial.name.Equals(tramRailDoubleWnNoName) && !node.m_nodeMaterial.name.Equals(value2))
                            {
                                string str10 = loaded.name.Replace(" ", "_").ToLowerInvariant().Trim();


                                tex = roadsmall + text7 + text14;

                                if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                {
                                    if (loaded.name.Contains("Basic Road"))
                                    {
                                        if (loaded.name.Equals("Basic Road Elevated"))
                                        {
                                            tex = roadsmall + elevated + text14;
                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("Basic Road Decoration Grass") || loaded.name.Equals("Basic Road Decoration Trees"))
                                        {
                                            tex = roadsmall + deco + text14;
                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tex = roadsmall + text7 + text14;
                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }
                                    else if (loaded.name.Contains("Oneway Road"))
                                    {
                                        tex = roadsmall + str;
                                        if (loaded.name.Equals("Oneway Road Elevated"))
                                        {
                                            tex = tex + elevated + text14;
                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("Oneway Road Decoration Grass") || loaded.name.Equals("Oneway Road Decoration Trees"))
                                        {
                                            tex = roadsmall + str + deco + text14;
                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tex = tex + text7 + text14;
                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Basic Road Bicycle"))
                                    {
                                        tex = roadsmall + text7 + text14;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Basic Road Elevated Bike"))
                                    {
                                        tex = roadsmall + str2 + elevated + text14;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Contains("Medium Road") && !loaded.name.Contains("Tram"))
                                    {
                                        tex = roadmedium;
                                        if (loaded.name.Contains("Medium Road Elevated") && !loaded.name.Contains("Bike"))
                                        {
                                            tex = tex + elevated + text14;
                                            if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("Medium Road Decoration Grass") || loaded.name.Equals("Medium Road Decoration Trees"))
                                        {
                                            tex = roadmedium + deco + text14;
                                            if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tex = tex + text7 + text14;
                                            if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Medium Road Elevated Bike"))
                                    {
                                        tex = roadmedium + str2 + elevated + text14;
                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Contains("Large Road"))
                                    {
                                        tex = roadlarge;
                                        if (loaded.name.Equals("Large Road Elevated"))
                                        {
                                            tex = tex + elevated + text14;
                                            if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("Large Road Decoration Grass") || loaded.name.Equals("Large Road Decoration Trees"))
                                        {
                                            tex = roadlarge + deco + text14;
                                            if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tex = tex + text7 + text14;
                                            if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Large Road Elevated Bike"))
                                    {
                                        tex = roadlarge + str2 + elevated + text14;
                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Contains("Large Oneway"))
                                    {
                                        tex = roadlarge + str;
                                        if (loaded.name.Equals("Large Oneway Elevated"))
                                        {
                                            tex = tex + elevated + text14;
                                            if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("Large Oneway Decoration Grass") || loaded.name.Equals("Large Oneway Decoration Trees"))
                                        {
                                            tex = roadlarge + str + deco + text14;
                                            if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("Large Oneway Road Slope"))
                                        {
                                            tex = roadlarge + str + slope + text14;
                                            if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tex = tex + text7 + text14;
                                            if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }
                                    else if (loaded.name.Contains("Highway"))
                                    {
                                        if (loaded.name.Equals("HighwayRamp"))
                                        {
                                            tex = highway + "Ramp" + text14;
                                            if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("HighwayRampElevated"))
                                        {
                                            tex = highway + "Ramp" + text14;
                                            if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("HighwayRamp Slope"))
                                        {
                                            tex = highway + "Ramp" + text14;
                                            if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("Highway Elevated"))
                                        {
                                            tex = highway + "3L" + text14;
                                            if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("Highway Slope"))
                                        {
                                            tex = highway + "3L" + text14;
                                            if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("Highway Barrier"))
                                        {
                                            tex = highway + "3L" + text14;
                                            if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("Highway"))
                                        {
                                            tex = highway + "3L" + text14;
                                            if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                                {
                                                    node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Basic Road Tram"))
                                    {
                                        tex = roadsmall + text7 + text14;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Basic Road Elevated Tram"))
                                    {
                                        tex = roadsmall + elevated + text14;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Basic Road Slope Tram"))
                                    {
                                        tex = roadsmall + text7 + text14;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Oneway Road Tram"))
                                    {
                                        tex = roadsmall + str + text7 + text14;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Oneway Road Elevated Tram"))
                                    {
                                        tex = roadsmall + str + elevated + text14;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Oneway Road Slope Tram"))
                                    {
                                        tex = roadsmall + str + text7 + text14;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Medium Road Tram"))
                                    {
                                        tex = roadmedium + str3 + text7 + text14;
                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Medium Road Elevated Tram"))
                                    {
                                        tex = roadmedium + str3 + elevated + text14;
                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Medium Road Slope Tram"))
                                    {
                                        tex = roadmedium + str3 + text7 + text14;
                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Tram Track"))
                                    {
                                        tex = roadsmall + deco + text14;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Tram Track Elevated"))
                                    {
                                        tex = roadsmall + elevated + text14;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Tram Track Slope"))
                                    {
                                        tex = roadsmall + deco + text14;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Oneway Tram Track"))
                                    {
                                        tex = roadsmall + deco + text14;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Oneway Tram Track Elevated"))
                                    {
                                        tex = roadsmall + elevated + text14;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (loaded.name.Equals("Oneway Tram Track Slope"))
                                    {
                                        tex = roadsmall + deco + text14;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            node.m_nodeMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                node.m_nodeMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                }

                            }
                        }

                        NetInfo.Segment[] segments = loaded.m_segments;
                        for (int j = 0; j < segments.Length; j++)
                        {
                            NetInfo.Segment segment = segments[j];
                            if (segment.m_segmentMaterial.GetTexture("_MainTex") != null && !segment.m_segmentMaterial.name.Contains("rail") && !segment.m_material.name.ToLower().Contains("cable"))
                            {
                                if (loaded.name.Contains("Basic Road"))
                                {
                                    tex = roadsmall;
                                    if (segment.m_segmentMesh.name.Equals("SmallRoadSegment"))
                                    {
                                        tex += text7;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_segmentMesh.name.Equals("SmallRoadSegmentBusSide"))
                                    {
                                        tex += busside;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_segmentMesh.name.Equals("SmallRoadSegmentBusBoth"))
                                    {
                                        tex += busboth;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                                    {
                                        if (loaded.name.Equals("Basic Road Elevated") || loaded.name.Equals("Basic Road Bridge"))
                                        {
                                            tex += elevated;
                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("Basic Road Slope"))
                                        {
                                            tex += slope;
                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }
                                }

                                if (loaded.name.Contains("Oneway Road"))
                                {
                                    tex = roadsmall + str;
                                    if (segment.m_mesh.name.Equals("SmallRoadSegment"))
                                    {
                                        tex = roadsmall + str + text7;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                    {
                                        tex = roadsmall + str + busside;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                                    {
                                        tex = roadsmall + str + busboth;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                                    {
                                        if (loaded.name.Equals("Oneway Road Elevated") || loaded.name.Equals("Oneway Road Bridge"))
                                        {
                                            tex = roadsmall + str + elevated;

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                        else if (loaded.name.Equals("Oneway Road Slope"))
                                        {
                                            tex = roadsmall + str + slope;

                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }
                                }

                                if (loaded.name.Equals("Basic Road Bicycle"))
                                {
                                    if (segment.m_mesh.name.Equals("SmallRoadSegment"))
                                    {
                                        tex = roadsmall + str2 + text7;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                    {
                                        tex = roadsmall + str2 + busside;
                                        if (ModLoader.config.basic_road_parking == 1)
                                        {
                                            if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                                    {
                                        tex = roadsmall + str2 + busboth;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                }

                                if (loaded.name.Equals("Basic Road Elevated Bike"))
                                {
                                    tex = roadsmall + str2 + elevated;
                                    if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                }

                                if (loaded.name.Equals("Basic Road Decoration Grass") || loaded.name.Equals("Basic Road Decoration Trees"))
                                {
                                    if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                                    {
                                        tex = roadsmall + deco;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                                    {
                                        tex = roadsmall + deco + busside;
 if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                                    {
                                        tex = roadsmall + deco + busboth;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                }

                                if (loaded.name.Equals("Oneway Road Decoration Grass") || loaded.name.Equals("Oneway Road Decoration Trees"))
                                {
                                    if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                                    {
                                        tex = roadsmall + str + deco;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                                    {
                                        tex = roadsmall + str + deco + busboth;

                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                                    {
                                        tex = roadsmall + str + deco + busboth;

                                        if (File.Exists(Path.Combine(path, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                }

                                if (loaded.name.Contains("Medium Road"))
                                {
                                    tex = roadmedium;
                                    if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                                    {
                                        tex += text7;
                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                    {
                                        tex += busside;

                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                    {
                                        tex += busboth;

                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (!segment.m_mesh.name.Equals("RoadMediumSegment"))
                                    {
                                        if (loaded.name.Equals("Medium Road Elevated") || loaded.name.ToLower().Contains("bridge"))
                                        {
                                            tex += elevated;
                                            if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                                }

                                                if (File.Exists(Path.Combine(path4, tex + lodMaintexDds)))
                                                {
                                                    segment.m_lodMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + lodMaintexDds)));
                                                }
                                            }
                                        }

                                        if (loaded.name.Equals("Medium Road Slope"))
                                        {
                                            tex += slope;
                                            if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }
                                }

                                if (loaded.name.Equals("Medium Road Decoration Grass") || loaded.name.Equals("Medium Road Decoration Trees"))
                                {
                                    tex = roadmedium + deco;
                                    if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                                    {
                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                    {
                                        tex += busside;

                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                    {
                                        tex += busboth;

                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                }

                                if (loaded.name.Equals("Medium Road Bicycle"))
                                {
                                    tex = roadmedium + str2;
                                    if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                                    {
                                        tex += text7;
                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                    {
                                        tex += busside;
                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                    {
                                        tex += busboth;
                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                }

                                if (loaded.name.Equals("Medium Road Elevated Bike") || loaded.name.Equals("Medium Road Bridge Bike"))
                                {
                                    tex = roadmedium + str2 + elevated;
                                    if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                        }
                                    }
                                }

                                if (loaded.name.Contains("Large Road"))
                                {
                                    tex = roadlarge;
                                    if (segment.m_mesh.name.Equals("RoadLargeSegment"))
                                    {
                                        tex += text7;
                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                    {
                                        tex += busside;

                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                    {
                                        tex += busboth;

                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (!segment.m_mesh.name.Equals("RoadLargeSegment"))
                                    {
                                        if (loaded.name.Equals("Large Road Elevated") || loaded.name.ToLower().Contains("bridge"))
                                        {
                                            tex += elevated;
                                            if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                                }
                                            }
                                        }

                                        if (loaded.name.Equals("Large Road Slope"))
                                        {
                                            tex += slope;
                                            if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                                }
                                            }
                                        }
                                    }
                                }

                                if (loaded.name.Equals("Large Road Decoration Grass") || loaded.name.Equals("Large Road Decoration Trees"))
                                {
                                    tex = roadlarge + deco;
                                    if (segment.m_mesh.name.Equals("LargeRoadSegment2"))
                                    {
                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }

                                            if (File.Exists(Path.Combine(path5, tex + lodMaintexDds)))
                                            {
                                                segment.m_lodMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + lodMaintexDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusSide"))
                                    {
                                        tex += busside;

                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }

                                            if (File.Exists(Path.Combine(path5, tex + lodMaintexDds)))
                                            {
                                                segment.m_lodMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + lodMaintexDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth"))
                                    {
                                        tex += busboth;

                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }

                                            if (File.Exists(Path.Combine(path5, tex + lodMaintexDds)))
                                            {
                                                segment.m_lodMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + lodMaintexDds)));
                                            }
                                        }
                                    }
                                }

                                if (loaded.name.Equals("Large Road Bicycle"))
                                {
                                    tex = roadlarge + str2;
                                    if (segment.m_mesh.name.Equals("RoadLargeSegment"))
                                    {
                                        tex += text7;
                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }

                                            if (File.Exists(Path.Combine(path5, tex + lodMaintexDds)))
                                            {
                                                segment.m_lodMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + lodMaintexDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                    {
                                        tex += busside;
                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }

                                            if (File.Exists(Path.Combine(path5, tex + lodMaintexDds)))
                                            {
                                                segment.m_lodMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + lodMaintexDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                    {
                                        tex += busboth;
                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }

                                            if (File.Exists(Path.Combine(path5, tex + lodMaintexDds)))
                                            {
                                                segment.m_lodMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + lodMaintexDds)));
                                            }
                                        }
                                    }
                                }

                                if (loaded.name.Equals("Large Road Elevated Bike") || loaded.name.Equals("Large Road Bridge Bike"))
                                {
                                    tex = roadlarge + str2 + elevated;
                                    if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                        }

                                        if (File.Exists(Path.Combine(path5, tex + lodMaintexDds)))
                                        {
                                            segment.m_lodMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + lodMaintexDds)));
                                        }
                                    }
                                }

                                if (loaded.name.Contains("Large Oneway"))
                                {
                                    tex = roadlarge + str;
                                    if (segment.m_mesh.name.Equals("RoadLargeSegment"))
                                    {
                                        tex += text7;
                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }

                                            if (File.Exists(Path.Combine(path5, tex + lodMaintexDds)))
                                            {
                                                segment.m_lodMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + lodMaintexDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                    {
                                        tex += busside;

                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }

                                            if (File.Exists(Path.Combine(path5, tex + lodMaintexDds)))
                                            {
                                                segment.m_lodMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + lodMaintexDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                    {
                                        tex += busboth;

                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }

                                            if (File.Exists(Path.Combine(path5, tex + lodMaintexDds)))
                                            {
                                                segment.m_lodMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + lodMaintexDds)));
                                            }
                                        }
                                    }
                                    else if (!segment.m_mesh.name.Equals("RoadLargeSegment"))
                                    {
                                        if (loaded.name.Equals("Large Oneway Elevated") || loaded.name.ToLower().Contains("bridge"))
                                        {
                                            tex += elevated;
                                            if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                                if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                                }

                                                if (File.Exists(Path.Combine(path5, tex + lodMaintexDds)))
                                                {
                                                    segment.m_lodMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + lodMaintexDds)));
                                                }
                                            }
                                        }
                                    }
                                }

                                if (loaded.name.Equals("Large Oneway Road Slope"))
                                {
                                    tex = roadlarge + str + slope;
                                    if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                        }
                                    }
                                }

                                if (loaded.name.Equals("Large Oneway Decoration Grass") || loaded.name.Equals("Large Oneway Decoration Trees"))
                                {
                                    tex = roadlarge + str + deco;
                                    if (segment.m_mesh.name.Equals("LargeRoadSegment2"))
                                    {
                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusSide"))
                                    {
                                        tex += busside;


                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth"))
                                    {
                                        tex += busboth;

                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                }
                                else if (loaded.name.Contains("Highway"))
                                {
                                    if (loaded.name.Equals("HighwayRamp") || loaded.name.Equals("HighwayRampElevated"))
                                    {
                                        tex = highway + "Ramp";
                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }
                                    else if (loaded.name.Equals("HighwayRamp Slope"))
                                    {
                                        tex = highway + "Ramp" + slope;
                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }
                                    else if (loaded.name.Equals("Highway Barrier"))
                                    {
                                        tex = highway + "3L";
                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                        }

                                        if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                        }
                                    }
                                    else if (loaded.name.Equals("Highway Elevated") || segment.m_mesh.name.Equals("HighwayBridgeSegment") || segment.m_mesh.name.Equals("HighwayBaseSegment") || segment.m_mesh.name.Equals("HighwayBarrierSegment"))
                                    {
                                        tex = highway + "3L";
                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("highway-tunnel-segment") || segment.m_mesh.name.Equals("highway-tunnel-slope"))
                                    {
                                        tex = highway + "3L" + slope;
                                        if (File.Exists(Path.Combine(path6, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path6, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path6, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                }

                                if (loaded.name.Equals("Basic Road Tram"))
                                {
                                    if (segment.m_mesh.name.Equals("RoadSmallTramStopSingle"))
                                    {
                                        tex = roadsmall + busboth;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("RoadSmallTramStopDouble"))
                                    {
                                        tex = roadsmall + busboth;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("RoadSmallTramAndBusStop"))
                                    {
                                        tex = roadsmall + busboth;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Basic Road Elevated Tram") || loaded.name.Equals("Basic Road Bridge Tram"))
                                {
                                    tex = roadsmall + elevated;
                                    if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Basic Road Slope Tram"))
                                {
                                    tex = roadsmall + slope;
                                    if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Oneway Road Tram"))
                                {
                                    tex = roadsmall + str;
                                    if (segment.m_mesh.name.Equals("RoadSmallTramStopSingle"))
                                    {
                                        tex = roadsmall + str + busboth;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                    {
                                        tex = roadsmall + str + busboth;
                                        if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Oneway Road Elevated Tram") || loaded.name.Equals("Oneway Road Bridge Tram"))
                                {
                                    tex = roadsmall + str + elevated;
                                    if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Oneway Road Slope Tram"))
                                {
                                    tex = roadsmall + str + slope;
                                    if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Medium Road Tram"))
                                {
                                    if (segment.m_mesh.name.Equals("RoadMediumTramSegment"))
                                    {
                                        tex = roadmedium + str3 + text7;
                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Medium Road Elevated Tram") || loaded.name.Equals("Medium Road Bridge Tram"))
                                {
                                    tex = roadmedium + str3 + elevated;
                                    if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Medium Road Slope Tram"))
                                {
                                    tex = roadmedium + str3 + slope;
                                    if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Tram Track"))
                                {
                                    tex = roadsmall + deco;
                                    if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Tram Track Elevated"))
                                {
                                    tex = roadsmall + str + elevated;
                                    if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Tram Track Slope"))
                                {
                                    tex = roadsmall + str + slope;
                                    if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Oneway Tram Track"))
                                {
                                    tex = roadsmall + deco;
                                    if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Oneway Tram Track Elevated"))
                                {
                                    tex = roadsmall + str + elevated;
                                    if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Oneway Tram Track Slope"))
                                {
                                    tex = roadsmall + str + slope;
                                    if (File.Exists(Path.Combine(path3, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path3, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path3, tex + aprmapDds)));
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Medium Road Bus"))
                                {
                                    if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                                    {
                                        tex = roadmedium + text6 + text7;
                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                    {
                                        tex = roadmedium + text6 + busside;

                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                    {
                                        tex = roadmedium + text6 + busboth;

                                        if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Medium Road Elevated Bus") || loaded.name.Equals("Medium Road Bridge Bus"))
                                {
                                    tex = roadmedium + text6 + elevated;
                                    if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Medium Road Slope Bus"))
                                {
                                    tex = roadmedium + text6 + slope;
                                    if (File.Exists(Path.Combine(path4, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path4, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path4, tex + aprmapDds)));
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Large Road Bus"))
                                {
                                    if (segment.m_mesh.name.Equals("RoadLargeSegmentBusLane"))
                                    {
                                        tex = roadlarge + text6 + text7;
                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("RoadLargeSegmentBusSideBusLane"))
                                    {
                                        tex = roadlarge + text6 + busside;

                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBothBusLane"))
                                    {
                                        tex = roadlarge + text6 + busboth;

                                        if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                            if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                            }
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Large Road Elevated Bus") || loaded.name.Equals("Large Road Bridge Bus"))
                                {
                                    tex = roadlarge + text6 + elevated;
                                    if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                        }
                                    }
                                }
                                else if (loaded.name.Equals("Large Road Slope Bus"))
                                {
                                    tex = roadlarge + text6 + slope;
                                    if (File.Exists(Path.Combine(path5, tex + maintexDds)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + maintexDds)));
                                        if (File.Exists(Path.Combine(path5, tex + aprmapDds)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(aprmap, RoadsUnited_Core.LoadTextureDDS(Path.Combine(path5, tex + aprmapDds)));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                num += 1u;
            }
        }

    }
}
