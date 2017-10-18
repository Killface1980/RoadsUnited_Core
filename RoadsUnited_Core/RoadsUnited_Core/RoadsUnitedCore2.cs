namespace RoadsUnited_Core
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using ColossalFramework;

    using JetBrains.Annotations;

    using RoadsUnited_Core2;
    using RoadsUnited_Core2.Statics;

    using UnityEngine;

    public partial class RoadsUnitedCore2 : MonoBehaviour
    {
        private const string ExtDDS = ".dds";

        private const float NewLodRenderDistance = 5000f;

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
                                                                                   "OneWay3L"
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
                                                                                   "OneWay4L"
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

        private static bool isDirty = true;

        private static readonly List<ReplacementStateNode> nodeChanges = new List<ReplacementStateNode>();

        private static readonly List<ReplacementStateProp> propChanges = new List<ReplacementStateProp>();

        private static readonly List<ReplacementStateSegment> segmentChanges = new List<ReplacementStateSegment>();

        public static void ChangeArrowProp([CanBeNull] PropInfo propInfo)
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

        public static void MarkFilelistDirty()
        {
            isDirty = true;
        }

        public static void ReplaceNetTextures(NetInfo netInfo, bool parkingReset = false)
        {
            if (netInfo == null)
            {
                return;
            }

            if (isDirty)
            {
                ModLoader.AllTexturesAvailable.Clear();

                DirectoryInfo directory = new DirectoryInfo(ModLoader.Config.currentTexturesPath_default);
                DirectoryInfo directory3 = new DirectoryInfo(ModLoader.APRMaps_Path);

                IEnumerable<FileInfo> files = directory.GetFiles("*.dds", SearchOption.AllDirectories);
                IEnumerable<FileInfo> extraApr = directory3.GetFiles("*.dds", SearchOption.AllDirectories);

                List<FileInfo> allfiles = files.Concat(extraApr).ToList();

                // string name = "RoadLargeSegment-default-apr" + ExtDDS;
                // var test = allfiles.Any(x => x.Name == name);
                // if (test)
                // {
                // Debug.Log("RoadLargeSegment-default-apr EXISTS! " + allfiles.Where(x => x.Name.Equals(name)).FirstOrDefault().Name);
                // }
                string names = "RU Core 2: files";
                foreach (FileInfo s in allfiles)
                {
                    names += "\n" + s;
                }

                Debug.Log(names);

                ModLoader.AllTexturesAvailable = allfiles;
                isDirty = false;
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
            var list = new List<object>();

            if (!parkingReset)
            {
                NetInfo.Node[] nodes = netInfo.m_nodes;
                foreach (NetInfo.Node node in nodes.Where(node => !Blacklist.Any(x => className.Contains(x)))
                    .Where(
                        node => node.m_nodeMaterial.GetTexture(TexType.MainTex) != null
                                && node.m_nodeMaterial.GetTexture(TexType.APRMap) != null))
                {
                    if (!list.Contains(node) && !netInfo.gameObject.name.Equals("TAM Road"))
                    {
                        TextureExporter.ExportPrefabTextures(node);
                        list.Add(node);
                    }

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
                                nodeList.Add(new NodeSet(node, "Highway2L_Ground_Node", "Highway2L_Ground_Node"));
                            }

                            if (nodeMatName.Contains(RoadPos.Slope))
                            {
                                nodeList.Add(new NodeSet(node, "Highway1L_Slope_Node"));
                            }
                        }
                    }

                    nodeList.Add(new NodeSet(node, nodeMatName, nodeAprName));
                }
            }

            // Look for segments
            NetInfo.Segment[] segments = netInfo.m_segments;
            foreach (NetInfo.Segment segment in segments.Where(segment => !Blacklist.Any(x => className.Contains(x)))
                .Where(
                    segment => segment.m_segmentMaterial.GetTexture(TexType.MainTex) != null
                               && segment.m_segmentMaterial.GetTexture(TexType.APRMap) != null))
            {
                if (!list.Contains(segment) && !netInfo.gameObject.name.Equals("TAM Road"))
                {
                    TextureExporter.ExportPrefabTextures(segment);
                    list.Add(segment);
                }

                string mainTexName = segment.m_segmentMaterial.GetTexture(TexType.MainTex).name;
                string aprName = segment.m_segmentMaterial.GetTexture(TexType.APRMap).name;

                segList.Add(new SegmentSet(segment, mainTexName, aprName));

                allSegments += "\n" + className + " | " + netInfo.name + " | " + mainTexName + " | "
                               + segment.m_segmentMesh.name;

                string meshName = segment.m_mesh.name;

                ReplaceNExtSegment(netInfo, segment, ref nextLog, ref segList);

                if (netInfo.name.Contains("Medium Avenue"))
                {
                    segList.Add(new SegmentSet(segment, null, "RoadLargeSegment-default-apr"));
                }

                if (netInfo.name.Contains("Small Busway"))
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
                    segmentChanges.Add(
                        new ReplacementStateSegment
                        {
                            segment = set.segment,
                            mainTex =
                                    set.segment.m_segmentMaterial
                                        .GetTexture(TexType.MainTex) as Texture2D,
                            aprMap =
                                    set.segment.m_segmentMaterial
                                        .GetTexture(TexType.APRMap) as Texture2D,
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
                    nodeChanges.Add(
                        new ReplacementStateNode
                        {
                            node = set.node,
                            mainTex =
                                    set.node.m_nodeMaterial
                                        .GetTexture(TexType.MainTex) as Texture2D,
                            aprMap =
                                    set.node.m_nodeMaterial
                                        .GetTexture(TexType.APRMap) as Texture2D,
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

        public static void ReplacePropTextures([CanBeNull] PropInfo propInfo, string path)
        {
            if (propInfo == null)
            {
                return;
            }

            if (path.IsNullOrWhiteSpace())
            {
                return;
            }

            if (propInfo.m_lodMaterialCombined == null || propInfo.m_lodMaterialCombined.GetTexture(TexType.MainTex)
                == null)
            {
                return;
            }

            string filename = propInfo.m_lodMaterialCombined.GetTexture(TexType.MainTex).name;
            if (filename.IsNullOrWhiteSpace())
            {
                return;
            }

            if (filename.Equals("BusLaneText"))
            {
                filename = "BusLane";
            }

            string fullName = ModLoader.AllTexturesAvailable.FirstOrDefault(x => x.Name.Equals(filename + ExtDDS))
                ?.FullName;

            if (!fullName.IsNullOrWhiteSpace())
            {
                UpdatePropChanges(propInfo);
                SetMatWithFileList(filename, propInfo.m_lodMaterialCombined, TexType.MainTex);

                string fileaci = propInfo.m_lodMaterialCombined.GetTexture(TexType.ACIMap).name;
                string aciName = ModLoader.AllTexturesAvailable.FirstOrDefault(x => x.Name.Equals(fileaci + ExtDDS))
                    ?.FullName;
                if (!aciName.IsNullOrWhiteSpace())
                {
                    SetMatWithFileList(fileaci, propInfo.m_lodMaterialCombined, TexType.ACIMap);
                }
            }
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

        private static void ReplaceNExtNodes(
            NetInfo netInfo,
            NetInfo.Node node,
            ref string log,
            ref List<NodeSet> nodeList)
        {
            if (node.m_nodeMaterial.GetTexture(TexType.MainTex) == null)
            {
                return;
            }

            string texname = node.m_nodeMaterial.GetTexture(TexType.MainTex).name;
            foreach (KeyValuePair<string, string> road in NExtRoads)
            {
                if (!netInfo.name.StartsWith(road.Key))
                {
                    continue;
                }

                string roadPosition = RoadPos.Ground;

                if (texname.Contains(RoadPos.Slope))
                {
                    roadPosition = RoadPos.Slope;
                }

                if (texname.Contains(RoadPos.Tunnel))
                {
                    roadPosition = RoadPos.Tunnel;
                }

                if (texname.Contains(RoadPos.Elevated))
                {
                    roadPosition = RoadPos.Elevated;
                }

                string filename = road.Value + "_" + roadPosition + "_" + "Node";

                string[] arr = node.m_nodeMaterial.GetTexture(TexType.MainTex).name.Split('_');

                // ToDo: add lod material
                nodeList.Add(new NodeSet(node, filename + TexType.MainTex, filename + TexType.APRMap));
            }
        }

        private static void ReplaceNExtSegment(
            NetInfo netInfo,
            NetInfo.Segment segment,
            ref string log,
            ref List<SegmentSet> segList)
        {
            if (segment.m_segmentMaterial.GetTexture(TexType.MainTex) == null)
            {
                return;
            }

            string texname = segment.m_segmentMaterial.GetTexture(TexType.MainTex).name;

            foreach (KeyValuePair<string, string> road in NExtRoads)
            {
                if (!netInfo.name.StartsWith(road.Key))
                {
                    continue;
                }

                string roadPosition = RoadPos.Ground;

                if (texname.Contains(RoadPos.Slope))
                {
                    roadPosition = RoadPos.Slope;
                }

                if (texname.Contains(RoadPos.Tunnel))
                {
                    roadPosition = RoadPos.Tunnel;
                }

                if (texname.Contains(RoadPos.Elevated))
                {
                    roadPosition = RoadPos.Elevated;
                }

                string file = road.Value + "_" + roadPosition + "_" + "Segment";

                string[] arr = segment.m_segmentMaterial.GetTexture(TexType.MainTex).name.Split('_');

                if (arr.Contains("Inverted"))
                {
                    file += "_Inverted";
                }

                segList.Add(new SegmentSet(segment, file + TexType.MainTex, file + TexType.APRMap));
            }
        }

        private static bool SetMatWithFileList(string filename, Material mat, string type)
        {
            string fullName = ModLoader.AllTexturesAvailable.FirstOrDefault(x => x.Name.Equals(filename + ExtDDS))
                ?.FullName;

            if (!fullName.IsNullOrWhiteSpace())
            {
                mat.SetTexture(type, fullName?.LoadDDS());
                Debug.Log("Set with file list: " + fullName);
                return true;
            }

            // Debug.Log("Requested file " + filename + " for " + mat + " not found.");
            return false;
        }

        private static void SetNodeDirect(NodeSet set)
        {
            NetInfo.Node node = set.node;
            string mainTex = set.MainTex;
            string aprMap = set.APRMap;

            if (!mainTex.IsNullOrWhiteSpace())
            {
                string type = TexType.MainTex;

                if (!SetMatWithFileList(mainTex, node.m_nodeMaterial, type) && mainTex.Contains(RoadPos.Elevated))
                {
                    mainTex = mainTex.Replace(RoadPos.Elevated, RoadPos.Ground);
                    SetMatWithFileList(mainTex, node.m_nodeMaterial, type);
                }

                string lodMainTex = node.m_lodMaterial.GetTexture(TexType.MainTex)?.name;
                if (!lodMainTex.IsNullOrWhiteSpace())
                {
                    if (!SetMatWithFileList(lodMainTex, node.m_lodMaterial, type))
                    {
                        lodMainTex = mainTex + "_lod";
                        if (!SetMatWithFileList(lodMainTex, node.m_lodMaterial, type))
                        {
                            node.m_lodRenderDistance = NewLodRenderDistance;
                        }
                    }
                }
            }

            if (!aprMap.IsNullOrWhiteSpace())
            {
                string type = TexType.APRMap;

                if (!SetMatWithFileList(aprMap, node.m_nodeMaterial, type) && aprMap.Contains(RoadPos.Elevated))
                {
                    aprMap = aprMap.Replace(RoadPos.Elevated, RoadPos.Ground);
                    SetMatWithFileList(aprMap, node.m_nodeMaterial, type);
                }

                string lodAprTex = node.m_lodMaterial.GetTexture(TexType.APRMap)?.name;
                if (!lodAprTex.IsNullOrWhiteSpace())
                {
                    if (!SetMatWithFileList(lodAprTex, node.m_lodMaterial, type))
                    {
                        lodAprTex = aprMap + "_lod";
                        if (!SetMatWithFileList(lodAprTex, node.m_lodMaterial, type))
                        {
                            node.m_lodRenderDistance = NewLodRenderDistance;
                        }
                    }
                }
            }
        }

        private static void SetSegmentDirect(SegmentSet set)
        {
            NetInfo.Segment segment = set.segment;
            string mainTex = set.MainTex;
            string aprMap = set.APRMap;

            if (!mainTex.IsNullOrWhiteSpace())
            {
                string type = TexType.MainTex;

                if (!SetMatWithFileList(mainTex, segment.m_segmentMaterial, type) && mainTex.Contains(RoadPos.Elevated))
                {
                    mainTex = mainTex.Replace(RoadPos.Elevated, RoadPos.Ground);
                    SetMatWithFileList(mainTex, segment.m_segmentMaterial, type);
                }

                string lodMainTex = segment.m_lodMaterial.GetTexture(TexType.MainTex)?.name;
                if (!lodMainTex.IsNullOrWhiteSpace())
                {
                    if (!SetMatWithFileList(lodMainTex, segment.m_lodMaterial, type))
                    {
                        lodMainTex = mainTex + "_lod";
                        if (!SetMatWithFileList(lodMainTex, segment.m_lodMaterial, type))
                        {
                            segment.m_lodRenderDistance = NewLodRenderDistance;
                        }
                    }
                }
            }

            if (!aprMap.IsNullOrWhiteSpace())
            {
                string type = TexType.APRMap;

                if (!SetMatWithFileList(aprMap, segment.m_segmentMaterial, type) && aprMap.Contains(RoadPos.Elevated))
                {
                    aprMap = aprMap.Replace(RoadPos.Elevated, RoadPos.Ground);
                    SetMatWithFileList(aprMap, segment.m_segmentMaterial, type);
                }

                string lodAprTex = segment.m_lodMaterial.GetTexture(TexType.APRMap)?.name;
                if (!lodAprTex.IsNullOrWhiteSpace())
                {
                    if (!SetMatWithFileList(lodAprTex, segment.m_lodMaterial, type))
                    {
                        lodAprTex = aprMap + "_lod";
                        if (!SetMatWithFileList(lodAprTex, segment.m_lodMaterial, type))
                        {
                            segment.m_lodRenderDistance = NewLodRenderDistance;
                        }
                    }
                }
            }
        }

        private static void UpdatePropChanges([NotNull] PropInfo propInfo)
        {
            propChanges.Add(
                new ReplacementStateProp
                {
                    propInfo = propInfo,
                    mainTex =
                            propInfo.m_lodMaterialCombined
                                .GetTexture(TexType.MainTex) as Texture2D,
                    aciMap =
                            propInfo.m_lodMaterialCombined.GetTexture(TexType.ACIMap) as Texture2D
                });
        }

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


    }
}