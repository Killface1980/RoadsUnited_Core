namespace RoadsUnited_Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using ColossalFramework;

    using PrefabHook;

    using RoadsUnited_Core2;
    using RoadsUnited_Core2.Statics;

    using UnityEngine;

    public class RoadsUnitedCore2 : MonoBehaviour
    {

        #region Private Fields

        private const string ExtDDS = ".dds";

        private const string NextRoadsPath = "/NExt_Roads";

        private static readonly List<string> Blacklist =
            new List<string>
                {
                    "Heating Pipe",
                    "Train",
                    "Metro",
                    "Transport",
                    "Bus Line",
                    "Water",
                    "Airplane",
                    "Ship",
                    "Landscaping",
                    "Beautification",
                    "Pedestrian",
                    "Electricity"
                };

        private static readonly Dictionary<string, string> NExtRoads = new Dictionary<string, string>
                                                                           {
                                                                               {
                                                                                   "Two-Lane Alley",
                                                                                   "Alley2L"
                                                                               },
                                                                               {
                                                                                   "One-Lane Oneway",
                                                                                   "OneWay1L"
                                                                               },
                                                                               {
                                                                                   "One-Lane Oneway With Parking",
                                                                                   "OneWay1Lp"
                                                                               },
                                                                               {
                                                                                   "PlainStreet2L",
                                                                                   "PlainStreet2L"
                                                                               },
                                                                               {
                                                                                   "BasicRoadPntMdn",
                                                                                   "BasicRoadPntMdn"
                                                                               },
                                                                               {
                                                                                   "BasicRoadTL",
                                                                                   "BasicRoadTL"
                                                                               },
                                                                               {
                                                                                   "AsymRoadL1R2",
                                                                                   "AsymRoadL1R2"
                                                                               },
                                                                               {
                                                                                   // todo el, br, t, sl?
                                                                                   "BasicRoadMdn",
                                                                                   "BasicRoadMdn"
                                                                               },
                                                                               {
                                                                                   // todo el, br, t, sl, deco gr, t?
                                                                                   "Oneway3L",
                                                                                   "Oneway3L"
                                                                               },
                                                                               {
                                                                                   "Small Avenue",
                                                                                   "SmallAvenue4L"
                                                                               },
                                                                               {
                                                                                   "AsymAvenueL2R4",
                                                                                   "AsymAvenueL2R4"
                                                                               },
                                                                               {
                                                                                   // todo el, br, t, sl?
                                                                                   "AsymAvenueL2R3",
                                                                                   "AsymAvenueL2R3"
                                                                               },
                                                                               {
                                                                                   // todo el, br, t, sl?
                                                                                   "AsymRoadL1R3",
                                                                                   "AsymRoadL1R3"
                                                                               },
                                                                               {
                                                                                   // todo el, br, t, sl?
                                                                                   "Oneway4L",
                                                                                   "Oneway4L"
                                                                               },
                                                                               {
                                                                                   "Medium Avenue",
                                                                                   "MediumAvenue4L"
                                                                               },
                                                                               {
                                                                                   "Medium Avenue TL",
                                                                                   "MediumAvenue4LTL"
                                                                               },
                                                                               {
                                                                                   "Eight-Lane Avenue",
                                                                                   "LargeAvenue8LM"
                                                                               },
                                                                               {
                                                                                   "Small Rural Highway",
                                                                                   "Highway1L"
                                                                               },
                                                                               {
                                                                                   "Rural Highway",
                                                                                   "Highway2L"
                                                                               },
                                                                               {
                                                                                   "Four-Lane Highway",
                                                                                   "Highway4L"
                                                                               },
                                                                               {
                                                                                   "Five-Lane Highway",
                                                                                   "Highway5L"
                                                                               },
                                                                               {
                                                                                   "Large Highway",
                                                                                   "Highway6L"
                                                                               }

                                                                               // todo el, br, t, sl?
                                                                               // Small Busway handled with exceptions
                                                                               // also Large Road With Bus
                                                                               // and Medium Avenue ...?
                                                                           };
        private static List<ReplacementStateNode> nodeChanges = new List<ReplacementStateNode>();
        private static List<ReplacementStateProp> propChanges = new List<ReplacementStateProp>();
        private static List<ReplacementStateSegment> segmentChanges = new List<ReplacementStateSegment>();
        private static float newLodRenderDistance = 7000f;

        #endregion Private Fields

        // deactivated for now
        /*
        public static Texture2D LoadTexture(string fullPath)
        {
            Texture2D texture2D = new Texture2D(1, 1);
            if (TextureCache.TryGetValue(fullPath, out texture2D))
            {
                return texture2D;
            }

            texture2D.LoadImage(File.ReadAllBytes(fullPath));
            texture2D.name = Path.GetFileName(fullPath);
            texture2D.anisoLevel = 8;
            texture2D.Compress(true);
            return texture2D;
        }
        public static Texture2D DDSLoader.LoadDDS(string fullPath)
        {
            // Testen ob Textur bereits geladen, in dem Fall geladene Textur zurückgeben
            if (textureCache.TryGetValue(fullPath, out Texture2D texture))
            {
                return texture;
            }

            // Nein? Textur laden
            byte[] numArray = File.ReadAllBytes(fullPath);
            int width = BitConverter.ToInt32(numArray, 16);
            int height = BitConverter.ToInt32(numArray, 12);
            texture = new Texture2D(width, height, TextureFormat.DXT1, true);
            List<byte> list = new List<byte>();
            for (int index = 0; index < numArray.Length; ++index)
            {
                if (index > (int)sbyte.MaxValue)
                {
                    list.Add(numArray[index]);
                }
            }

            texture.LoadRawTextureData(list.ToArray());
            texture.name = Path.GetFileName(fullPath);
            texture.anisoLevel = 8;
            texture.Apply();
            textureCache.Add(fullPath, texture); // Neu geladene Textur in den Cache packen
            return texture;
        }
        */
        #region Public Methods

        public static void ChangeArrowProp(PropInfo propInfo)
        {
            if (propInfo == null)
            {
                return;
            }

            Debug.Log("RU Core2: changing arrow props ...");
            {
                if (propInfo.name.Equals("Road Arrow LFR"))
                {
                    if (ModLoader.Config.disable_optional_arrow_lfr)
                    {
                        propInfo.m_maxRenderDistance = 0f;
                        propInfo.m_maxScale = 0f;
                        propInfo.m_minScale = 0f;
                    }
                    else
                    {
                        propInfo.m_maxRenderDistance = 1000f;
                        propInfo.m_maxScale = 1f;
                        propInfo.m_minScale = 1f;
                    }
                }

                if (propInfo.name.Equals("Road Arrow LR"))
                {
                    if (ModLoader.Config.disable_optional_arrow_lr)
                    {
                        propInfo.m_maxRenderDistance = 0f;
                        propInfo.m_maxScale = 0f;
                        propInfo.m_minScale = 0f;
                    }
                    else
                    {
                        propInfo.m_maxRenderDistance = 1000f;
                        propInfo.m_maxScale = 1f;
                        propInfo.m_minScale = 1f;
                    }
                }
            }
        }

        public static void ReplaceNetTextures(NetInfo netInfo)
        {
            if (netInfo == null)
            {
                return;
            }

            List<SegmentSet> segList = new List<SegmentSet>();
            List<NodeSet> nodeList = new List<NodeSet>();
            string log = "RU Core replacing: ";
            string nextLog = "Next Replacements: ";
            string allNodes = "All nodes:";
            string allSegments = "All segments:";

            allNodes += "\nNExt nodes: ";
            allSegments += "\nNExt segments: ";
            string className = netInfo.m_class.name;

            NetInfo.Node[] nodes = netInfo.m_nodes;
            foreach (NetInfo.Node node in nodes.Where(node => !Blacklist.Any(x => className.Contains(x))).Where(
                node => node.m_nodeMaterial.GetTexture(TexType.MainTex) != null
                        && node.m_nodeMaterial.GetTexture(TexType.APRMap) != null))
            {
                TextureExporter.ExportPrefabTextures(node);

                // Just look up the name and set the textures accordingly, no magic needed
                string nodeMatName = node.m_nodeMaterial.GetTexture(TexType.MainTex).name;
                string nodeAprName = node.m_nodeMaterial.GetTexture(TexType.APRMap).name;

                allNodes += "\n" + className + " | " + netInfo.name + " | " + nodeMatName + " | "
                            + node.m_nodeMesh.name;
                allNodes += "\n" + nodeAprName;

                ReplaceNExtNodes(netInfo, node, ref nextLog, ref nodeList);

                if (netInfo.name.Contains("Rural Highway"))
                {
                    if (netInfo.name.Contains("Small"))
                    {
                        if (nodeMatName.Contains(RoadPos.Ground) || nodeMatName.Contains(RoadPos.Elevated))
                        {
                            // not an error, use the 2l node tex
                            nodeList.Add(new NodeSet(node, "Highway2L_Ground_Node", "Highway1L_Ground_Node"));
                        }

                        if (nodeMatName.Contains(RoadPos.Slope))
                        {
                            nodeList.Add(new NodeSet(node, "Highway1L_Slope_Node"));
                        }
                    }
                    else
                    {
                        if (nodeMatName.Contains(RoadPos.Ground) || nodeMatName.Contains(RoadPos.Elevated))
                        {
                            nodeList.Add(new NodeSet(node, "Highway2L_Ground_Node"));
                        }

                        if (nodeMatName.Contains(RoadPos.Slope))
                        {
                            nodeList.Add(new NodeSet(node, "Highway2L_Slope_Node"));
                        }
                    }
                }



                nodeList.Add(new NodeSet(node, nodeMatName, nodeAprName));
            }

            // Look for segments
            NetInfo.Segment[] segments = netInfo.m_segments;
            foreach (NetInfo.Segment segment in segments.Where(segment => !Blacklist.Any(x => className.Contains(x)))
                .Where(
                    segment => segment.m_segmentMaterial.GetTexture(TexType.MainTex) != null
                               && segment.m_segmentMaterial.GetTexture(TexType.APRMap) != null))
            {
                TextureExporter.ExportPrefabTextures(segment);

                string mainTexName = segment.m_segmentMaterial.GetTexture(TexType.MainTex).name;
                string aprName = segment.m_segmentMaterial.GetTexture(TexType.APRMap).name;

                segList.Add(new SegmentSet(segment, mainTexName, aprName));



                allSegments += "\n" + className + " | " + netInfo.name + " | " + mainTexName + " | "
                               + segment.m_segmentMesh.name;

                string meshName = segment.m_mesh.name;

                ReplaceNExtSegments(netInfo, segment, ref nextLog, ref segList);

                if (netInfo.name.Contains("Medium Avenue"))
                {
                    if (netInfo.name.Contains("TL"))
                    {
                        if (netInfo.name.Contains(RoadPos.Elevated))
                        {
                            segList.Add(new SegmentSet(segment, "MediumAvenue4LTL_Elevated_Segment"));
                        }
                        else if (netInfo.name.Contains(RoadPos.Slope))
                        {
                            segList.Add(new SegmentSet(segment, "MediumAvenue4LTL_Slope_Segment"));
                        }
                        else if (netInfo.name.Contains(RoadPos.Tunnel))
                        {
                            segList.Add(new SegmentSet(segment, "MediumAvenue4LTL_Tunnel_Segment"));
                        }
                        else
                        {
                            segList.Add(
                                new SegmentSet(
                                    segment,
                                    "MediumAvenue4LTL_Ground_Segment",
                                    "RoadLargeSegment-default-apr"));
                        }
                    }
                    else
                    {
                        if (netInfo.name.Contains(RoadPos.Elevated))
                        {
                            segList.Add(new SegmentSet(segment, "MediumAvenue4L_Elevated_Segment"));
                        }
                        else if (netInfo.name.Contains(RoadPos.Slope))
                        {
                            segList.Add(new SegmentSet(segment, "MediumAvenue4L_Slope_Segment"));
                        }
                        else if (netInfo.name.Contains(RoadPos.Tunnel))
                        {
                            segList.Add(new SegmentSet(segment, "MediumAvenue4L_Tunnel_Segment"));
                        }
                        else
                        {
                            segList.Add(
                                new SegmentSet(
                                    segment,
                                    "MediumAvenue4L_Ground_Segment",
                                    "RoadLargeSegment-default-apr"));
                        }
                    }
                }
                else if (netInfo.name.Contains("Rural Highway"))
                {
                    if (netInfo.name.Contains("Small"))
                    {
                        if (mainTexName.Contains(RoadPos.Slope))
                        {
                            segList.Add(new SegmentSet(segment, "Highway1L_Slope_Segment"));
                        }
                        else if (mainTexName.Contains(RoadPos.Tunnel))
                        {
                            segList.Add(new SegmentSet(segment, "Highway1L_Tunnel_Segment"));
                        }
                        else
                        {
                            segList.Add(new SegmentSet(segment, "Highway1L_Ground_Segment"));
                        }
                    }
                    else
                    {
                        if (mainTexName.Contains(RoadPos.Slope))
                        {
                            segList.Add(new SegmentSet(segment, "Highway2L_Slope_Segment"));
                        }
                        else if (mainTexName.Contains(RoadPos.Tunnel))
                        {
                            segList.Add(new SegmentSet(segment, "Highway2L_Tunnel_Segment"));
                        }
                        else
                        {
                            segList.Add(new SegmentSet(segment, "Highway2L_Ground_Segment"));
                        }
                    }
                }
                else if (netInfo.name.Contains("Small Busway"))
                {
                    if (netInfo.name.Contains("Oneway"))
                    {
                        if (netInfo.name.Contains("Decoration"))
                        {
                            if (netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees"))
                            {
                                if (mainTexName.Contains(RoadPos.Ground))
                                {
                                    segList.Add(new SegmentSet(segment, "Busway2L1W_DecoGrass_Ground_Segment"));
                                }
                            }
                        }
                        else
                        {
                            if (mainTexName.Contains(RoadPos.Ground))
                            {
                                segList.Add(new SegmentSet(segment, "Busway2L1W_Ground_Segment"));
                            }

                            if (mainTexName.Contains(RoadPos.Elevated))
                            {
                                segList.Add(new SegmentSet(segment, "Busway2L1W_Elevated_Segment"));
                            }

                            if (mainTexName.Contains(RoadPos.Slope))
                            {
                                segList.Add(new SegmentSet(segment, "Busway2L1W_Slope_Segment"));
                            }
                        }
                    }
                    else
                    {
                        if (netInfo.name.Contains("Decoration"))
                        {
                            if (netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees"))
                            {
                                if (mainTexName.Contains(RoadPos.Ground))
                                {
                                    segList.Add(new SegmentSet(segment, "Busway2L_DecoGrass_Ground_Segment"));
                                }

                                // if (segment.m_segmentMaterial.GetTexture(TexType._APRMap).name.Contains(RoadPos.Ground))
                                // if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadBasic2-apr"+ ext_DDS)))
                                // {
                                // segment.m_segmentMaterial.SetTexture(TexType._APRMap, DDSLoader.LoadDDS(Path.Combine(ModLoader.currentTexturesPath_default, "RoadBasic2-apr"+ ext_DDS)));
                                // segment.m_lodRenderDistance = 2500;
                                // }
                                // else if (File.Exists(Path.Combine(ModLoader.APRMaps_Path, "RoadBasic2-apr"+ ext_DDS)))
                                // {
                                // segment.m_segmentMaterial.SetTexture(TexType._APRMap, DDSLoader.LoadDDS(Path.Combine(ModLoader.APRMaps_Path, "RoadBasic2-apr"+ ext_DDS)));
                                // segment.m_lodRenderDistance = 2500;
                                // }
                            }
                        }
                        else
                        {
                            if (mainTexName.Contains(RoadPos.Ground))
                            {
                                segList.Add(new SegmentSet(segment, "Busway2L_Ground_Segment"));
                            }

                            if (mainTexName.Contains(RoadPos.Elevated))
                            {
                                segList.Add(new SegmentSet(segment, "Busway2L_Elevated_Segment"));
                            }

                            if (mainTexName.Contains(RoadPos.Slope))
                            {
                                segList.Add(new SegmentSet(segment, "Busway2L_Slope_Segment"));
                            }
                        }
                    }
                }
                else if (netInfo.name.Contains("Large Road") && netInfo.name.Contains("Bus Lanes"))
                {
                    if (netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees"))
                    {
                        if (mainTexName.Contains(RoadPos.Ground))
                        {
                            segList.Add(new SegmentSet(segment, "Busway6L_DecoGrass_Ground_Segment"));
                        }

                        if (aprName.Contains(RoadPos.Ground))
                        {
                            segList.Add(new SegmentSet(segment, null, "RoadLargeSegment-default-apr"));
                        }
                    }
                    else
                    {
                        if (mainTexName.Contains(RoadPos.Ground))
                        {
                            segList.Add(new SegmentSet(segment, "RoadLargeBuslane_D"));
                        }

                        if (mainTexName.Contains(RoadPos.Elevated))
                        {
                            segList.Add(new SegmentSet(segment, "RoadLargeElevatedBus_D"));
                        }

                        if (mainTexName.Contains(RoadPos.Slope))
                        {
                            segList.Add(new SegmentSet(segment, "large-tunnelBus_d"));
                        }

                        if (aprName.Contains(RoadPos.Ground))
                        {
                            segList.Add(new SegmentSet(segment, null, "RoadLargeSegment-default-apr"));
                        }
                    }
                }
                else if (netInfo.name.Contains("Oneway"))
                {
                    if (mainTexName.Equals("RoadSmallSegment"))
                    {
                        segList.Add(new SegmentSet(segment, "Oneway_RoadSmallSegment"));

                        if (meshName.Equals("SmallRoadSegmentBusSide"))
                        {
                            segList.Add(new SegmentSet(segment, "Oneway_RoadSmallSegment_BusSide"));
                        }

                        if (meshName.Equals("SmallRoadSegmentBusBoth"))
                        {
                            segList.Add(new SegmentSet(segment, "Oneway_RoadSmallSegment_BusBoth"));
                        }
                    }

                    if (mainTexName.Equals("SmallRoadSegmentDeco"))
                    {
                        if (meshName.Equals("SmallRoadSegment2BusSide"))
                        {
                            segList.Add(new SegmentSet(segment, "Oneway_SmallRoadSegmentDeco_BusSide"));
                        }
                    }
                    else if (meshName.Equals("SmallRoadSegment2BusBoth"))
                    {
                        segList.Add(new SegmentSet(segment, "Oneway_SmallRoadSegmentDeco_BusBoth"));
                    }
                    else
                    {
                        segList.Add(new SegmentSet(segment, "Oneway_SmallRoadSegmentDeco"));
                    }

                    if (mainTexName.Equals("small-tunnel_d"))
                    {
                        segList.Add(new SegmentSet(segment, "Oneway_small-tunnel_d"));
                    }

                    if (mainTexName.Equals("RoadSmallElevatedSegment"))
                    {
                        segList.Add(new SegmentSet(segment, "Oneway_RoadSmallElevatedSegment_D"));
                    }

                    if (meshName.Equals("LargeRoadSegmentBusSide"))
                    {
                        segList.Add(new SegmentSet(segment, "RoadLargeOnewaySegment_d_BusSide"));
                    }

                    if (meshName.Equals("LargeRoadSegmentBusBoth"))
                    {
                        segList.Add(new SegmentSet(segment, "RoadLargeOnewaySegment_d_BusBoth"));
                    }
                }
                else if (!netInfo.name.Contains("Bicycle"))
                {
                    if (meshName.Equals("LargeRoadSegmentBusSide"))
                    {
                        segList.Add(new SegmentSet(segment, "RoadLargeSegment_d_BusSide"));
                    }

                    if (meshName.Equals("LargeRoadSegmentBusBoth"))
                    {
                        segList.Add(new SegmentSet(segment, "RoadLargeSegment_d_BusBoth"));
                    }

                    if (meshName.Equals("LargeRoadSegment2BusBoth"))
                    {
                        segList.Add(new SegmentSet(segment, "RoadLargeSegmentDecoBusBoth_d"));
                    }

                    if (meshName.Equals("RoadLargeSegmentBusSideBusLane"))
                    {
                        segList.Add(new SegmentSet(segment, "RoadLargeBuslane_D_BusSide"));
                    }

                    if (meshName.Equals("LargeRoadSegmentBusBothBusLane"))
                    {
                        segList.Add(new SegmentSet(segment, "RoadLargeBuslane_D_BusBoth"));
                    }
                }

                if (meshName.Equals("SmallRoadSegmentBusSide"))
                {
                    if (!netInfo.name.Contains("Bicycle"))
                    {
                        segList.Add(new SegmentSet(segment, "RoadSmall_D_BusSide"));
                    }
                    else
                    {
                        segList.Add(new SegmentSet(segment, "SmallRoadSegmentDeco_BusSide"));
                    }
                }
                else if (meshName.Equals("SmallRoadSegmentBusBoth"))
                {
                    if (!netInfo.name.Contains("Bicycle"))
                    {
                        segList.Add(new SegmentSet(segment, "RoadSmall_D_BusBoth"));
                    }
                }
                else if (meshName.Equals("SmallRoadSegment2BusBoth"))
                {
                    segList.Add(new SegmentSet(segment, "SmallRoadSegmentDeco_BusBoth"));
                }
                else if (meshName.Equals("RoadMediumSegmentBusSide"))
                {
                    if (netInfo.name.Contains("Trees"))
                    {
                        segList.Add(new SegmentSet(segment, "RoadMediumDeco_d_BusSide"));
                        if (netInfo.name.Contains("Bus"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadMediumBusLane_BusSide"));
                        }

                        if (!netInfo.name.Contains("Bicycle"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadMedium_D_BusSide"));
                        }
                    }
                }
                else if (meshName.Equals("RoadMediumSegmentBusBoth"))
                {
                    if (netInfo.name.Contains("Trees"))
                    {
                        segList.Add(new SegmentSet(segment, "RoadMediumDeco_d_BusBoth"));
                        if (netInfo.name.Contains("Bus"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadMediumBusLane_BusBoth"));
                        }

                        if (!netInfo.name.Contains("Bicycle"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadMedium_D_BusBoth"));
                        }
                    }
                }

                if (ModLoader.Config.basic_road_parking == 1)
                {
                    if (mainTexName.Equals("RoadSmall_D"))
                    {
                        segList.Add(new SegmentSet(segment, "RoadSmall_D_parking1"));
                    }
                    else if (mainTexName.Equals("RoadSmall_D_BusSide"))
                    {
                        segList.Add(new SegmentSet(segment, "RoadSmall_D_BusSide_parking1"));
                    }
                }

                if (ModLoader.Config.medium_road_parking == 1)
                {
                    if (!(netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees")))
                    {
                        if (mainTexName.Equals("RoadMediumSegment_d") || mainTexName.Equals("RoadMedium_D"))
                        {
                            if (meshName.Equals("RoadMediumSegmentBusSide"))
                            {
                                segList.Add(new SegmentSet(segment, "RoadMedium_D_BusSide_parking1"));
                            }
                            else if (meshName.Equals("RoadMediumSegmentBusBoth"))
                            {
                                segList.Add(new SegmentSet(segment, "RoadMedium_D_BusBoth_parking1"));
                            }
                            else
                            {
                                segList.Add(new SegmentSet(segment, "RoadMedium_D_parking1"));
                            }
                        }
                    }
                }

                if (ModLoader.Config.medium_road_grass_parking == 1 && netInfo.name.Contains("Grass"))
                {
                    if (mainTexName.Equals("RoadMedium_D") || mainTexName.Equals("RoadMediumSegment_d"))
                    {
                        if (meshName.Equals("RoadMediumSegmentBusSide"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadMedium_D_BusSide_parking1"));
                        }
                        else if (meshName.Equals("RoadMediumSegmentBusBoth"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadMedium_D_BusBoth_parking1"));
                        }
                        else
                        {
                            segList.Add(new SegmentSet(segment, "RoadMedium_D_parking1"));
                        }
                    }
                }

                if (ModLoader.Config.medium_road_trees_parking == 1 && netInfo.name.Contains("Trees"))
                {
                    if (mainTexName.Equals("RoadMediumDeco_d"))
                    {
                        if (meshName.Equals("RoadMediumSegmentBusSide"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadMediumDeco_d_BusSide_parking1"));
                        }
                        else if (meshName.Equals("RoadMediumSegmentBusBoth"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadMediumDeco_d"));
                        }
                        else
                        {
                            segList.Add(new SegmentSet(segment, "RoadMediumDeco_d_parking1"));
                        }
                    }
                }

                if (ModLoader.Config.medium_road_bus_parking == 1)
                {
                    if (mainTexName.Equals("RoadMediumBusLane"))
                    {
                        if (meshName.Equals("RoadMediumSegmentBusSide"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadMediumBusLane_BusSide_parking1"));
                        }
                        else if (meshName.Equals("RoadMediumSegmentBusBoth"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadMediumBusLane_BusBoth_parking1"));
                        }
                        else
                        {
                            segList.Add(new SegmentSet(segment, "RoadMediumBusLane_parking1"));
                        }
                    }
                }

                if (ModLoader.Config.large_road_parking == 1)
                {
                    if (mainTexName.Equals("RoadLargeSegment_d"))
                    {
                        if (meshName.Equals("LargeRoadSegmentBusSide"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadLargeSegment_d_BusSide_parking1"));
                        }
                        else if (meshName.Equals("LargeRoadSegmentBusBoth"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadLargeSegment_d_BusBoth_parking1"));
                        }
                        else
                        {
                            segList.Add(new SegmentSet(segment, "RoadLargeSegment_d_parking1"));
                        }
                    }
                }

                if (ModLoader.Config.large_oneway_parking == 1)
                {
                    if (mainTexName.Equals("RoadLargeOnewaySegment_d"))
                    {
                        if (meshName.Equals("LargeRoadSegmentBusSide"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadLargeOnewaySegment_d_BusSide_parking1"));
                        }
                        else
                        {
                            segList.Add(new SegmentSet(segment, "RoadLargeOnewaySegment_d_parking1"));
                        }
                    }
                }

                if (ModLoader.Config.large_road_bus_parking == 1)
                {
                    if (mainTexName.Equals("RoadLargeBuslane_D"))
                    {
                        if (meshName.Equals("LargeRoadSegmentBusSideBusLane"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadLargeBuslane_D_BusSide_parking1"));
                        }
                        else if (meshName.Equals("LargeRoadSegmentBusBothBusLane"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadLargeBuslane_D_BusBoth_parking1"));
                        }
                        else
                        {
                            segList.Add(new SegmentSet(segment, "RoadLargeBuslane_D_parking1"));
                        }
                    }
                }

                allSegments += "\n" + aprName;

                if (aprName.Equals("LargeRoadSegmentBusSide-BikeLane-apr")
                    || aprName.Equals("LargeRoadSegmentBusBoth-BikeLane-apr"))
                {
                    segList.Add(new SegmentSet(segment, null, "RoadLargeSegment-BikeLane-apr"));
                }
                else if (aprName.Equals("LargeRoadSegmentBusSide-LargeRoadSegmentBusSide-apr")
                         || aprName.Equals("LargeRoadSegmentBusBoth-LargeRoadSegmentBusBoth-apr"))
                {
                    segList.Add(new SegmentSet(segment, null, "RoadLargeSegment-default-apr"));
                }
            }

            ShittyPlusFuck.ReplacePlus(netInfo, ref segList, ref nodeList);


            foreach (SegmentSet set in segList)
            {
                if (!segmentChanges.Any(x => x.segment.Equals(set.segment)))
                {
                    segmentChanges.Add(new ReplacementStateSegment()
                    {
                        segment = set.segment,
                        mainTex = set.segment.m_segmentMaterial.GetTexture(TexType.MainTex) as Texture2D,
                        aprMap = set.segment.m_segmentMaterial.GetTexture(TexType.APRMap) as Texture2D,
                        m_lodMaterial = set.segment.m_lodMaterial,
                        m_lodRenderDistance = set.segment.m_lodRenderDistance
                    });
                }

                SetSegmentDirect(set);
                log += "\n" + set.segment + " | " + set.MainTex + " - " + set.APRMap;
            }

            foreach (NodeSet set in nodeList)
            {
                if (!nodeChanges.Any(x => x.node.Equals(set.node)))
                {
                    nodeChanges.Add(new ReplacementStateNode()
                    {
                        node = set.node,
                        mainTex = set.node.m_nodeMaterial.GetTexture(TexType.MainTex) as Texture2D,
                        aprMap = set.node.m_nodeMaterial.GetTexture(TexType.APRMap) as Texture2D,
                        m_lodMaterial = set.node.m_lodMaterial,
                        m_lodRenderDistance = set.node.m_lodRenderDistance
                    });
                }

                SetNodeDirect(set);
                log += "\n" + set.node + " | " + set.MainTex + " - " + set.APRMap;
            }

            Debug.Log(log);

            Debug.Log(nextLog);

            // Debug.Log(log);

            // LODResetter.ResetLOD();
            // Singleton<NetManager>.instance.InitRenderData();
        }

        public static void ReplacePropTextures(PropInfo propInfo, string path)
        {
            if (propInfo == null)
            {
                return;
            }

            if (path.IsNullOrWhiteSpace())
            {
                return;
            }

            string path2 = path + "/PropTextures";

            if (propInfo.m_lodMaterialCombined == null || propInfo.m_lodMaterialCombined.GetTexture(TexType.MainTex)
                == null)
            {
                return;
            }

            string defaultname = propInfo.m_lodMaterialCombined.GetTexture(TexType.MainTex).name;
            if (defaultname.IsNullOrWhiteSpace())
            {
                return;
            }

            if (defaultname.Equals("BusLaneText"))
            {
                defaultname = "BusLane";
            }

            string propLodTexture = Path.Combine(path, defaultname + ExtDDS);
            string propLodTexture2 = Path.Combine(path2, defaultname + ExtDDS);
            string propLodACIMapTexture = Path.Combine(path, defaultname + "-aci" + ExtDDS);
            string propLodACIMapTexture2 = Path.Combine(path2, defaultname + "-aci" + ExtDDS);


            if (File.Exists(propLodTexture))
            {
                UpdatePropChanges(propInfo);

                // only the m_lodMaterialCombined texture is visible
                propInfo.m_lodMaterialCombined.SetTexture(TexType.MainTex, propLodTexture.LoadDDS());
            }
            else if (File.Exists(propLodTexture2))
            {
                UpdatePropChanges(propInfo);

                // only the m_lodMaterialCombined texture is visible
                propInfo.m_lodMaterialCombined.SetTexture(TexType.MainTex, propLodTexture2.LoadDDS());
            }

            if (File.Exists(propLodACIMapTexture))
            {
                propInfo.m_lodMaterialCombined.SetTexture(TexType.ACIMap, propLodACIMapTexture.LoadDDS());
            }
            else if (File.Exists(propLodACIMapTexture2))
            {
                propInfo.m_lodMaterialCombined.SetTexture(TexType.ACIMap, propLodACIMapTexture2.LoadDDS());
            }
        }

        private static void UpdatePropChanges(PropInfo propInfo)
        {
            propChanges.Add(
                new ReplacementStateProp
                    {
                        propInfo = propInfo,
                        mainTex =
                            propInfo.m_lodMaterialCombined
                                .GetTexture(TexType.MainTex) as Texture2D,
                        aciMap =
                            propInfo.m_lodMaterialCombined.GetTexture(TexType.ACIMap) as Texture2D,
                    });
        }

        public static void RevertNodes()
        {
            foreach (ReplacementStateNode state in nodeChanges)
            {
                state.node.m_nodeMaterial.SetTexture(TexType.MainTex, state.mainTex);
                state.node.m_nodeMaterial.SetTexture(TexType.APRMap, state.aprMap);
                state.node.m_lodMaterial = state.m_lodMaterial;
                state.node.m_lodRenderDistance = state.m_lodRenderDistance;
            }
        }

        public static void RevertProps()
        {
            foreach (ReplacementStateProp state in propChanges)
            {
                state.propInfo.m_lodMaterialCombined.SetTexture(TexType.MainTex, state.mainTex);
                state.propInfo.m_lodMaterialCombined.SetTexture(TexType.ACIMap, state.aciMap);
            }

            propChanges.Clear();
        }

        public static void RevertSegments()
        {
            foreach (ReplacementStateSegment state in segmentChanges)
            {
                state.segment.m_segmentMaterial.SetTexture(TexType.MainTex, state.mainTex);
                state.segment.m_segmentMaterial.SetTexture(TexType.APRMap, state.aprMap);

                // state.segment.m_lodMaterial = state.m_lodMaterial;
                state.segment.m_lodRenderDistance = state.m_lodRenderDistance;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static void ReplaceNExtNodes(NetInfo netInfo, NetInfo.Node node, ref string log, ref List<NodeSet> nodeList)
        {
            foreach (KeyValuePair<string, string> road in NExtRoads)
            {
                if (!netInfo.name.Contains(road.Key))
                {
                    continue;
                }

                foreach (string roadPosition in RoadPos.AllPositions)
                {
                    foreach (string textype in TexType.AllTex)
                    {
                        if (node.m_nodeMaterial.GetTexture(textype) == null)
                        {
                            continue;
                        }

                        string filename = road.Value + "_" + roadPosition + "_" + "Node" + textype + ExtDDS;
                        string fullPath = Path.Combine(ModLoader.Config.currentTexturesPath_default + NextRoadsPath, filename);
                        string aprPath = Path.Combine(ModLoader.APRMaps_Path + NextRoadsPath, filename);

                        if (!node.m_nodeMaterial.GetTexture(textype).name.Contains(roadPosition))
                        {
                            continue;
                        }

                        // ToDo: add lod material
                        nodeList.Add(new NodeSet(node, fullPath, aprPath));
                        if (File.Exists(fullPath))
                        {
                            // string filename_lod = Path.Combine(ModLoader.currentTexturesPath_default + "/NExt_Roads/LOD", filename + "_lod" + ExtDDS);
                            // if (File.Exists(filename_lod))
                            // {
                            // node.m_lodMaterial.SetTexture(textype, DDSLoader.LoadDDS(filename_lod));
                            // }
                            log += "\nNExt replacing " + textype + " - " + fullPath;
                        }
                        else if (textype == TexType.APRMap && File.Exists(aprPath))
                        {
                            // string filename_lod = Path.Combine(ModLoader.APRMaps_Path + "/NExt_Roads/LOD", filename + "_lod" + ExtDDS);
                            // if (File.Exists(filename_lod))
                            // {
                            // node.m_lodMaterial.SetTexture(textype, DDSLoader.LoadDDS(filename_lod));
                            // }
                            // log += "\nNExt replacing " + TexType._APRMap + " - " + aprPath;
                        }
                    }
                }
            }
        }

        private static void ReplaceNExtSegments(NetInfo netInfo, NetInfo.Segment segment, ref string log, ref List<SegmentSet> segList)
        {
            foreach (KeyValuePair<string, string> road in NExtRoads)
            {
                if (!netInfo.name.Contains(road.Key))
                {
                    continue;
                }

                foreach (string roadPosition in RoadPos.AllPositions)
                {
                    foreach (string textype in TexType.AllTex)
                    {
                        string file = road.Value + "_" + roadPosition + "_" + "Segment" + textype;
                        string mainTex = Path.Combine(
                            ModLoader.Config.currentTexturesPath_default + NextRoadsPath,
                            file + ExtDDS);
                        string aprMap = Path.Combine(ModLoader.APRMaps_Path + NextRoadsPath, file + ExtDDS);

                        if (!segment.m_segmentMaterial.GetTexture(textype).name.Contains(roadPosition))
                        {
                            continue;
                        }

                        segList.Add(new SegmentSet(segment, mainTex, aprMap));

                        // ToDo: add lod

                        // if (File.Exists(filename))
                        // {
                        // segment.m_segmentMaterial.SetTexture(textype, DDSLoader.LoadDDS(filename));
                        // string filename_lod = Path.Combine(ModLoader.currentTexturesPath_default + "/NExt_Roads/LOD", file + "_lod" + ExtDDS);
                        // if (File.Exists(filename_lod))
                        // {
                        // segment.m_lodMaterial.SetTexture(textype, DDSLoader.LoadDDS(filename_lod));
                        // }
                        // }
                        // else if (textype.Equals(TexType._APRMap))
                        // {
                        // // APR Maps only
                        // filename = Path.Combine(ModLoader.APRMaps_Path + NextRoadsPath, file + ExtDDS);
                        // log += "\nNExt Segments APR looking for: " + filename;
                        // if (File.Exists(filename))
                        // {
                        // segment.m_segmentMaterial.SetTexture(textype, DDSLoader.LoadDDS(filename));
                        // string filename_lod = Path.Combine(
                        // ModLoader.APRMaps_Path + "/NExt_Roads/LOD",
                        // file + "_lod" + ExtDDS);
                        // if (File.Exists(filename_lod))
                        // {
                        // segment.m_lodMaterial.SetTexture(textype, DDSLoader.LoadDDS(filename_lod));
                        // }
                        // }
                        // }
                    }
                }
            }
        }

        private static void SetNodeDirect(NodeSet set)
        {
            NetInfo.Node node = set.node;
            string maintex = set.MainTex;
            string apr = set.APRMap;
            bool isPlus = !set.path.IsNullOrWhiteSpace();

            string currentTextures = isPlus ? set.path : ModLoader.Config.currentTexturesPath_default;
            if (!maintex.IsNullOrWhiteSpace())
            {
                if (File.Exists(Path.Combine(currentTextures, maintex + ExtDDS)))
                {
                    node.m_nodeMaterial.SetTexture(
                        TexType.MainTex,
                        Path.Combine(currentTextures, maintex + ExtDDS).LoadDDS());

                    string filename_lod = Path.Combine(
                        currentTextures + "/LOD", maintex + "_lod" + ExtDDS);

                    if (File.Exists(filename_lod))
                    {
                        node.m_lodMaterial.SetTexture(maintex, filename_lod.LoadDDS());
                    }
                    else
                    {
                        node.m_lodRenderDistance = newLodRenderDistance;
                    }
                }
                else if (File.Exists(Path.Combine(currentTextures, maintex + TexType.MainTex + ExtDDS)))
                {
                    node.m_nodeMaterial.SetTexture(
                        TexType.MainTex,
                        Path.Combine(currentTextures, maintex + TexType.MainTex + ExtDDS).LoadDDS());

                    string filename_lod = Path.Combine(currentTextures + "/LOD", maintex + TexType.MainTex + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        node.m_lodMaterial.SetTexture(maintex, filename_lod.LoadDDS());
                    }
                    else
                    {
                        node.m_lodRenderDistance = newLodRenderDistance;
                    }

                    // if the mod has to add the _MainTex, use the name for the APRs => only applies to NExt or custom tex
                    if (apr.IsNullOrWhiteSpace())
                    {
                        apr = maintex;
                    }
                }
            }

            if (!apr.IsNullOrWhiteSpace())
            {
                string path = Path.Combine(currentTextures, apr + ExtDDS);
                string path2 = Path.Combine(ModLoader.APRMaps_Path, apr + ExtDDS);
                string path3 = Path.Combine(currentTextures, apr + TexType.APRMap + ExtDDS);
                string path4 = Path.Combine(ModLoader.APRMaps_Path, apr + TexType.APRMap + ExtDDS);
                string path5 = Path.Combine(currentTextures + "/APR", apr + ExtDDS);

                if (File.Exists(path5))
                {
                    node.m_nodeMaterial.SetTexture(TexType.APRMap, path5.LoadDDS());
                    string filename_lod = Path.Combine(currentTextures + "/LOD", apr + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        node.m_lodMaterial.SetTexture(apr, filename_lod.LoadDDS());
                    }
                    else
                    {
                        node.m_lodRenderDistance = newLodRenderDistance;
                    }
                }
                else if (File.Exists(path))
                {
                    node.m_nodeMaterial.SetTexture(TexType.APRMap, path.LoadDDS());
                    string filename_lod = Path.Combine(currentTextures + "/LOD", apr + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        node.m_lodMaterial.SetTexture(apr, filename_lod.LoadDDS());
                    }
                    else
                    {
                        node.m_lodRenderDistance = newLodRenderDistance;
                    }
                }
                else if (File.Exists(path2))
                {
                    node.m_nodeMaterial.SetTexture(TexType.APRMap, path2.LoadDDS());
                    string filename_lod = Path.Combine(ModLoader.APRMaps_Path + "/LOD", apr + TexType.APRMap + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        node.m_lodMaterial.SetTexture(apr, filename_lod.LoadDDS());
                    }
                    else
                    {
                        node.m_lodRenderDistance = newLodRenderDistance;
                    }
                }
                else if (File.Exists(path3))
                {
                    node.m_nodeMaterial.SetTexture(TexType.APRMap, path3.LoadDDS());
                    string filename_lod = Path.Combine(currentTextures + "/LOD", apr + TexType.APRMap + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        node.m_lodMaterial.SetTexture(apr, filename_lod.LoadDDS());
                    }
                    else
                    {
                        node.m_lodRenderDistance = newLodRenderDistance;
                    }
                }
                else if (File.Exists(path4))
                {
                    node.m_nodeMaterial.SetTexture(TexType.APRMap, path4.LoadDDS());
                    string filename_lod = Path.Combine(ModLoader.APRMaps_Path + "/LOD", apr + TexType.APRMap + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        node.m_lodMaterial.SetTexture(maintex, filename_lod.LoadDDS());
                    }
                    else
                    {
                        node.m_lodRenderDistance = newLodRenderDistance;
                    }
                }
            }
        }

        private static void SetSegmentDirect(SegmentSet set)
        {
            NetInfo.Segment segment = set.segment;
            string maintex = set.MainTex;
            string apr = set.APRMap;
            bool isPlus = !set.path.IsNullOrWhiteSpace();

            string currentTextures = isPlus ? set.path : ModLoader.Config.currentTexturesPath_default;

            if (!maintex.IsNullOrWhiteSpace())
            {
                string type = TexType.MainTex;
                if (File.Exists(Path.Combine(currentTextures, maintex + ExtDDS)))
                {
                    segment.m_segmentMaterial.SetTexture(
                        type,
                        Path.Combine(currentTextures, maintex + ExtDDS).LoadDDS());
                    string filename_lod = Path.Combine(
                        currentTextures + "/LOD",
                        maintex + "_lod" + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        segment.m_lodMaterial.SetTexture(type, filename_lod.LoadDDS());
                    }
                    else
                    {
                        segment.m_lodRenderDistance = newLodRenderDistance;
                    }
                }
                else if (File.Exists(Path.Combine(currentTextures, maintex + type + ExtDDS)))
                {
                    segment.m_segmentMaterial.SetTexture(
                        type,
                        Path.Combine(currentTextures, maintex + type + ExtDDS).LoadDDS());

                    string filename_lod = Path.Combine(
                        currentTextures + "/LOD",
                        maintex + type + "_lod" + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        segment.m_lodMaterial.SetTexture(type, filename_lod.LoadDDS());
                    }
                    else
                    {
                        segment.m_lodRenderDistance = newLodRenderDistance;
                    }

                    // if the mod has to add the _MainTex, use the name for the APRs => only applies to NExt or custom tex
                    if (apr.IsNullOrWhiteSpace())
                    {
                        apr = maintex;
                    }
                }
            }

            if (!apr.IsNullOrWhiteSpace())
            {
                string type = TexType.APRMap;
                string path = Path.Combine(currentTextures, apr + ExtDDS);
                string path2 = Path.Combine(ModLoader.APRMaps_Path, apr + ExtDDS);
                string path3 = Path.Combine(currentTextures, apr + type + ExtDDS);
                string path4 = Path.Combine(ModLoader.APRMaps_Path, apr + type + ExtDDS);
                string path5 = Path.Combine(currentTextures + "/APR", apr + ExtDDS);

                 if (File.Exists(path5))
                {
                    segment.m_segmentMaterial.SetTexture(type, path5.LoadDDS());
                    string filename_lod = Path.Combine(
                        currentTextures + "/LOD",
                        apr + "_lod" + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        segment.m_lodMaterial.SetTexture(type, filename_lod.LoadDDS());
                    }
                    else
                    {
                        segment.m_lodRenderDistance = newLodRenderDistance;
                    }
                }
                else if (File.Exists(path))
                {
                    segment.m_segmentMaterial.SetTexture(type, path.LoadDDS());
                    string filename_lod = Path.Combine(
                        currentTextures + "/LOD",
                        apr + "_lod" + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        segment.m_lodMaterial.SetTexture(type, filename_lod.LoadDDS());
                    }
                    else
                    {
                        segment.m_lodRenderDistance = newLodRenderDistance;
                    }
                }
                else if (File.Exists(path2))
                {
                    segment.m_segmentMaterial.SetTexture(type, path2.LoadDDS());
                    string filename_lod = Path.Combine(
                        ModLoader.APRMaps_Path + "/LOD",
                        apr + "_lod" + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        segment.m_lodMaterial.SetTexture(type, filename_lod.LoadDDS());
                    }
                    else
                    {
                        segment.m_lodRenderDistance = newLodRenderDistance;
                    }
                }
                else if (File.Exists(path3))
                {
                    segment.m_segmentMaterial.SetTexture(type, path3.LoadDDS());
                    string filename_lod = Path.Combine(
                        currentTextures + "/LOD",
                        apr + type + "_lod" + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        segment.m_lodMaterial.SetTexture(type, filename_lod.LoadDDS());
                    }
                    else
                    {
                        segment.m_lodRenderDistance = newLodRenderDistance;
                    }
                }
                else if (File.Exists(path4))
                {
                    segment.m_segmentMaterial.SetTexture(type, path4.LoadDDS());
                    string filename_lod = Path.Combine(
                        ModLoader.APRMaps_Path + "/LOD",
                        apr + type + "_lod" + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        segment.m_lodMaterial.SetTexture(type, filename_lod.LoadDDS());
                    }
                    else
                    {
                        segment.m_lodRenderDistance = newLodRenderDistance;
                    }
                }
            }

        }

        #endregion Private Methods

        #region Private Structs

        private struct ReplacementStateNode
        {
            #region Public Fields

            public Texture2D aprMap;
            public Material m_lodMaterial;
            public float m_lodRenderDistance;
            public Texture2D mainTex;
            public NetInfo.Node node;

            #endregion Public Fields
        }

        private struct ReplacementStateProp
        {
            #region Public Fields

            public int laneIndex;
            public Texture2D mainTex;
            public float originalAngle;
            public PropInfo originalProp;
            public int propIndex;
            public PropInfo propInfo;
            public PropInfo replacementProp;

            public Texture2D aciMap;

            #endregion Public Fields
        }

        private struct ReplacementStateSegment
        {
            #region Public Fields

            public Texture aprMap;
            public Material m_lodMaterial;
            public float m_lodRenderDistance;
            public Material m_segmentMaterial;
            public Texture mainTex;
            public NetInfo.Segment segment;

            #endregion Public Fields
        }

        #endregion Private Structs
    }
}