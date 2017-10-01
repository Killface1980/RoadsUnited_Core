namespace RoadsUnited_Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using ColossalFramework;

    using JetBrains.Annotations;

    using Strings;

    using TexNames;

    using UnityEngine;

    public enum TexKind
    {
        MainTex,
        APRMap
    }


    public class ShitFuckNames
    {
        #region Public Fields

        public static Configuration config;

        public static Dictionary<string, Texture2D> vanillaPrefabProperties = new Dictionary<string, Texture2D>();

        #endregion Public Fields

        #region Public Methods


        public static void ReplaceNetTextures()
        {
            if (!ModLoader.config.texturePackPath.IsNullOrWhiteSpace())
            {
                ModLoader.currentTexturesPath_default = Path.Combine(ModLoader.config.texturePackPath, "BaseTextures");
            }

            string texPath = ModLoader.currentTexturesPath_default;
            string pathTiny = texPath + "/" + roadTiny + "/";
            string pathSmall = texPath + "/" + roadSmall + "/";
            string pathMedium = texPath + "/" + roadMedium + "/";
            string pathLarge = texPath + "/" + roadLarge + "/";
            string pathHighway = texPath + "/" + roadHighway + "/";

            uint num = 0u;
            while (num < (ulong)PrefabCollection<NetInfo>.LoadedCount())
            {
                NetInfo netInfo = PrefabCollection<NetInfo>.GetLoaded(num);
                if (netInfo == null)
                {
                    continue;
                }

                string className = netInfo.m_class.name;
                string log = className;
                foreach (NetInfo.Node node in netInfo.m_nodes)
                {
                    string coreName = string.Empty;
                    string plusPath = string.Empty;
                    string plusName = string.Empty;

                    if (node.m_nodeMaterial.GetTexture(maintex) == null)
                    {
                        continue;
                    }



                    string fullPath = Path.Combine(
                        ModLoader.currentTexturesPath_default,
                        node.m_nodeMaterial.GetTexture(maintex).name + ext_DDS);

                    // string netInfoName = netInfo.fileName.Replace(" ", "_").ToLowerInvariant().Trim();
                    if (netInfo.name.Equals("Two-Lane Alley"))
                    {
                        coreName = "Alley2L" + typeGround;
                        plusPath = pathTiny;
                        plusName = roadTiny + "1L";
                    }
                    else if (netInfo.name.Equals("One-Lane Oneway"))
                    {
                        coreName = "OneWay1L" + typeGround;
                        plusPath = pathTiny;
                        plusName = roadTiny + "1L";
                    }
                    else if (netInfo.name.Contains("Eight-Lane Avenue"))
                    {
                        coreName = "LargeAvenue8LM" + typeGround;
                        plusPath = pathLarge;
                        plusName = roadLarge + "8L" + pGn;

                        if (netInfo.name.Equals("Eight-Lane Avenue Elevated"))
                        {
                            coreName = "LargeAvenue8LM" + typeElevated;
                            plusName = roadLarge + "8L" + pEl;
                        }
                        else if (netInfo.name.Equals("Eight-Lane Avenue Slope"))
                        {
                            plusName = roadLarge + "8L" + pSl;
                        }
                        else if (netInfo.name.Equals("Eight-Lane Avenue Tunnel"))
                        {
                            plusName = roadLarge + "8L" + pTu;
                        }
                    }
                    else if (netInfo.name.Contains("Highway"))
                    {
                        if (netInfo.name.Contains("Small"))
                        {
                            coreName = "Highway2L" + typeGround;

                            if (netInfo.name.Equals("Small Rural Highway Slope"))
                            {
                                coreName = "Highway1L" + typeSlope;
                            }
                        }
                        else if (netInfo.name.Contains("Rural"))
                        {
                            coreName = "Highway2L" + typeGround;

                            if (netInfo.name.Equals("Rural Highway Slope"))
                            {
                                coreName = "Highway2L" + typeSlope;
                            }
                        }
                        else if (netInfo.name.Contains("Four-Lane"))
                        {
                            coreName = "Highway4L" + typeGround;
                            if (netInfo.name.Equals("Four-Lane Highway Elevated"))
                            {
                                coreName = "Highway4L" + typeElevated;
                            }

                            if (netInfo.name.Equals("Four-Lane Highway Slope"))
                            {
                                coreName = "Highway4L" + typeSlope;
                            }
                        }
                        else if (netInfo.name.Contains("Five-Lane"))
                        {
                            coreName = "Highway5L" + typeGround;
                            if (netInfo.name.Equals("Five-Lane Highway Slope"))
                            {
                                coreName = "Highway5L" + typeSlope;
                            }

                            if (netInfo.name.Equals("Five-Lane Highway Tunnel"))
                            {
                                coreName = "Highway5L" + typeTunnel;
                            }
                        }
                        else if (netInfo.name.Contains("Large"))
                        {
                            coreName = "Highway6L" + typeGround;

                            if (netInfo.name.Equals("Large Highway Slope"))
                            {
                                coreName = "Highway6L" + typeSlope;
                            }

                            if (netInfo.name.Equals("Large Highway Tunnel"))
                            {
                                coreName = "Highway6L" + typeTunnel;
                            }
                        }
                    }

                    log += "\nEntering nodes ...";

                    if (File.Exists(fullPath))
                    {
                        node.m_nodeMaterial.SetTexture(maintex, fullPath.LoadTextureDDS());

                        if (node.m_nodeMaterial.GetTexture(aprmap) != null)
                        {
                            string aprPath = Path.Combine(
                                ModLoader.currentTexturesPath_default,
                                node.m_nodeMaterial.GetTexture(aprmap).name + ext_DDS);
                            if (File.Exists(aprPath))
                            {
                                node.m_nodeMaterial.SetTexture(aprmap, aprPath.LoadTextureDDS());
                            }
                        }
                    }
                    else if (SetNodeTextures(node, coreName, plusPath, plusName, ref log)) { }
                    else if (File.Exists(Path.Combine(pathSmall, plusName + nodeSuffix + mainTexSuffix)))
                    {
                        // Shitty plus fuck stuff
                        SetShittyNamesPlusNodes(
                            netInfo,
                            out plusPath,
                            out plusName);
                        log += "Using shitty Plus nodes, new names: " + plusPath + "/" + plusName;
                        SetNodeTextures(node, null, plusPath, plusName, ref log);
                    }



                    if (node.m_nodeMaterial.GetTexture(aprmap) != null)
                    {
                        string aprPath = Path.Combine(texPath, node.m_nodeMaterial.GetTexture(aprmap).name + ext_DDS);
                        if (File.Exists(aprPath))
                        {
                            node.m_nodeMaterial.SetTexture(aprmap, aprPath.LoadTextureDDS());
                        }
                        else
                        {
                            aprPath = Path.Combine(
                                ModLoader.APRMaps_Path,
                                node.m_nodeMaterial.GetTexture(aprmap).name + ext_DDS);
                            if (File.Exists(aprPath))
                            {
                                node.m_nodeMaterial.SetTexture(aprmap, aprPath.LoadTextureDDS());
                            }
                        }
                    }
                }

                foreach (NetInfo.Segment segment in netInfo.m_segments)
                {
                    if (segment.m_segmentMaterial.GetTexture(maintex) == null)
                    {
                        continue;
                    }

                    string fileName = segment.m_segmentMaterial.GetTexture(maintex).name;
                    if (fileName.IsNullOrWhiteSpace())
                    {
                        continue;
                    }

                    string coreName = string.Empty;
                    string plusPath = string.Empty;
                    string plusName = string.Empty;

                    log += "\nRaplacing Plus names ...";
                    ReplaceSegmentCoreNames(netInfo, segment, ref coreName, ref fileName, ref log);
                    ReplaceSegmentShitFuckNames(netInfo, segment, ref plusPath, ref plusName, ref log);

                    log += "\nNew names: " + coreName + " - " + fileName + " | " + plusPath + "/" + plusName;

                    if (ModLoader.config.basic_road_parking == 1)
                    {
                        plusName = roadSmall + pGn;
                        if (fileName.Equals("RoadSmall_D"))
                        {
                            fileName = "RoadSmall_D_parking1";
                        }

                        if (fileName.Equals("RoadSmall_D_BusSide"))
                        {
                            fileName = "RoadSmall_D_BusSide_parking1";
                        }
                    }

                    if (ModLoader.config.medium_road_parking == 1 && !netInfo.name.Contains("Deco"))
                    {
                        if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                        {
                            if (fileName.Equals(roadmediumsegmentD)
                                && File.Exists(Path.Combine(texPath, "RoadMedium_D_BusSide_parking1" + ext_DDS)))
                            {
                                fileName = "RoadMedium_D_BusSide_parking1";
                            }
                        }
                        else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                        {
                            if (fileName.Equals(roadmediumsegmentD)
                                && File.Exists(Path.Combine(texPath, "RoadMedium_D_BusBoth_parking1" + ext_DDS)))
                            {
                                fileName = "RoadMedium_D_BusBoth_parking1";
                            }
                        }
                        else if (fileName.Equals(roadmediumsegmentD)
                                 && File.Exists(Path.Combine(texPath, "RoadMedium_D_parking1" + ext_DDS)))
                        {
                            fileName = "RoadMedium_D_parking1";
                        }
                        else if (fileName.Equals("RoadMedium_D")
                                 && File.Exists(Path.Combine(texPath, "RoadMedium_D_parking1" + ext_DDS)))
                        {
                            fileName = "RoadMedium_D_parking1";
                        }
                    }

                    if (ModLoader.config.medium_road_grass_parking == 1 && netInfo.name.Contains("Grass"))
                    {
                        if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                        {
                            if (fileName.Equals("RoadMedium_D")
                                && File.Exists(Path.Combine(texPath, "RoadMedium_D_BusSide_parking1" + ext_DDS)))
                            {
                                fileName = "RoadMedium_D_BusSide_parking1";
                            }
                        }
                        else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                        {
                            if (fileName.Equals("RoadMedium_D")
                                && File.Exists(Path.Combine(texPath, "RoadMedium_D_BusBoth_parking1" + ext_DDS)))
                            {
                                fileName = "RoadMedium_D_BusBoth_parking1";
                            }
                        }
                        else if (fileName.Equals(roadmediumsegmentD)
                                 && File.Exists(Path.Combine(texPath, "RoadMedium_D_parking1" + ext_DDS)))
                        {
                            fileName = "RoadMedium_D_parking1";
                        }
                        else if (fileName.Equals("RoadMedium_D")
                                 && File.Exists(Path.Combine(texPath, "RoadMedium_D_parking1" + ext_DDS)))
                        {
                            fileName = "RoadMedium_D_parking1";
                        }
                    }

                    if (ModLoader.config.medium_road_trees_parking == 1 && netInfo.name.Contains("Trees"))
                    {
                        if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                        {
                            if (fileName.Equals("RoadMediumDeco_d")
                                && File.Exists(Path.Combine(texPath, "RoadMediumDeco_d_BusSide_parking1" + ext_DDS)))
                            {
                                fileName = "RoadMediumDeco_d_BusSide_parking1";
                            }
                        }
                        else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth")
                                 && File.Exists(Path.Combine(texPath, "RoadMediumDeco_d" + ext_DDS)))
                        {
                            fileName = "RoadMediumDeco_d";
                        }
                        else if (fileName.Equals("RoadMediumDeco_d")
                                 && File.Exists(Path.Combine(texPath, "RoadMediumDeco_d_parking1" + ext_DDS)))
                        {
                            fileName = "RoadMediumDeco_d_parking1";
                        }
                    }

                    if (ModLoader.config.medium_road_bus_parking == 1)
                    {
                        if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                        {
                            if (fileName.Equals("RoadMediumBusLane")
                                && File.Exists(Path.Combine(texPath, "RoadMediumBusLane_BusSide_parking1" + ext_DDS)))
                            {
                                fileName = "RoadMediumBusLane_BusSide_parking1";
                            }
                        }
                        else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                        {
                            if (fileName.Equals("RoadMediumBusLane")
                                && File.Exists(Path.Combine(texPath, "RoadMediumBusLane_BusBoth_parking1" + ext_DDS)))
                            {
                                fileName = "RoadMediumBusLane_BusBoth_parking1";
                            }
                        }
                        else if (fileName.Equals("RoadMediumBusLane")
                                 && File.Exists(Path.Combine(texPath, "RoadMediumBusLane_parking1" + ext_DDS)))
                        {
                            fileName = "RoadMediumBusLane_parking1";
                        }
                    }

                    if (ModLoader.config.large_road_parking == 1)
                    {
                        if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                        {
                            if (fileName.Equals(roadlargesegmentD)
                                && File.Exists(
                                    Path.Combine(texPath, roadlargesegmentD + "_BusSide_parking1" + ext_DDS)))
                            {
                                fileName = roadlargesegmentD + "_BusSide_parking1";
                            }
                        }
                        else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                        {
                            if (fileName.Equals(roadlargesegmentD)
                                && File.Exists(
                                    Path.Combine(texPath, roadlargesegmentD + "_BusBoth_parking1" + ext_DDS)))
                            {
                                fileName = roadlargesegmentD + "_BusBoth_parking1";
                            }
                        }
                        else if (fileName.Equals(roadlargesegmentD)
                                 && File.Exists(Path.Combine(texPath, roadlargesegmentD + "_parking1" + ext_DDS)))
                        {
                            fileName = roadlargesegmentD + "_parking1";
                        }
                    }

                    if (ModLoader.config.large_oneway_parking == 1)
                    {
                        if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                        {
                            if (fileName.Equals("RoadLargeOnewaySegment_d") && File.Exists(
                                    Path.Combine(texPath, "RoadLargeOnewaySegment_d_BusSide_parking1" + ext_DDS)))
                            {
                                fileName = "RoadLargeOnewaySegment_d_BusSide_parking1";
                            }
                        }
                        else if (fileName.Equals("RoadLargeOnewaySegment_d")
                                 && File.Exists(Path.Combine(texPath, "RoadLargeOnewaySegment_d_parking1" + ext_DDS)))
                        {
                            fileName = "RoadLargeOnewaySegment_d_parking1";
                        }
                    }

                    if (ModLoader.config.large_road_bus_parking == 1)
                    {
                        if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSideBusLane"))
                        {
                            if (fileName.Equals("RoadLargeBuslane_D")
                                && File.Exists(Path.Combine(texPath, "RoadLargeBuslane_D_BusSide_parking1" + ext_DDS)))
                            {
                                fileName = "RoadLargeBuslane_D_BusSide_parking1";
                            }
                        }
                        else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBothBusLane"))
                        {
                            if (fileName.Equals("RoadLargeBuslane_D")
                                && File.Exists(Path.Combine(texPath, "RoadLargeBuslane_D_BusBoth_parking1" + ext_DDS)))
                            {
                                fileName = "RoadLargeBuslane_D_BusBoth_parking1";
                            }
                        }
                        else if (fileName.Equals("RoadLargeBuslane_D")
                                 && File.Exists(Path.Combine(texPath, "RoadLargeBuslane_D_parking1" + ext_DDS)))
                        {
                            fileName = "RoadLargeBuslane_D_parking1";
                        }
                    }

                    fileName += ext_DDS;
                    coreName += ext_DDS;

                    log += "\nTry Core filename: " + texPath + "/" + fileName;
                    if (!segment.CheckAndSetSegmentMaterial(texPath, fileName, ref log))
                    {
                        log += "\nFile name failed, trying corename: " + texPath + "/" + coreName;
                        if (!segment.CheckAndSetSegmentMaterial(texPath, coreName, ref log))
                        {
                            log += "\nCore failed, trying Plus: ";
                            SetSegmentTextures(segment, null, plusPath, plusName, ref log);
                        }
                    }

                    if (segment.m_segmentMaterial.GetTexture(aprmap) == null)
                    {
                        continue;
                    }

                    fileName = segment.m_segmentMaterial.GetTexture(aprmap).name + ext_DDS;

                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Equals("LargeRoadSegmentBusSide-BikeLane-apr")
                        || segment.m_segmentMaterial.GetTexture(aprmap).name
                            .Equals("LargeRoadSegmentBusBoth-BikeLane-apr"))
                    {
                        if (File.Exists(Path.Combine(texPath, roadlargesegmentBikelaneApr + ext_DDS))
                            || File.Exists(Path.Combine(ModLoader.APRMaps_Path, roadlargesegmentBikelaneApr + ext_DDS)))
                        {
                            fileName = roadlargesegmentBikelaneApr + ext_DDS;
                        }
                    }
                    else if (segment.m_segmentMaterial.GetTexture(aprmap).name
                                 .Equals("LargeRoadSegmentBusSide-LargeRoadSegmentBusSide-apr") || segment
                                 .m_segmentMaterial.GetTexture(aprmap).name
                                 .Equals("LargeRoadSegmentBusBoth-LargeRoadSegmentBusBoth-apr"))
                    {
                        if (File.Exists(Path.Combine(texPath, roadlargesegmentDefaultApr + ext_DDS))
                            || File.Exists(Path.Combine(ModLoader.APRMaps_Path, roadlargesegmentDefaultApr + ext_DDS)))
                        {
                            fileName = roadlargesegmentDefaultApr + ext_DDS;
                        }
                    }

                    log += "\nSetting final segment apr ...";
                    segment.SetSegmentDirect(fileName, TexKind.APRMap, ref log);
                }

                num += 1u;

                Debug.Log("RU Core: \n" + log);
            }
        }

        private static void ReplaceSegmentShitFuckNames(NetInfo netInfo, NetInfo.Segment segment, ref string plusPath, ref string plusName, ref string log)
        {
            string texPath = ModLoader.currentTexturesPath_default;
            string pathTiny = texPath + "/" + roadTiny + "/";
            string pathSmall = texPath + "/" + roadSmall + "/";
            string pathMedium = texPath + "/" + roadMedium + "/";
            string pathLarge = texPath + "/" + roadLarge + "/";
            string pathHighway = texPath + "/" + roadHighway + "/";


            if (netInfo.name.Equals("BasicRoadTL"))
            {
                plusPath = pathSmall;
                if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Ground"))
                {
                    plusName = roadSmall + "TL" + pGn;
                }
                else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Elevated"))
                {
                    plusName = roadSmall + "TL" + pEl;
                }
                else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Tunnel"))
                {
                    plusName = roadSmall + "TL" + pTu;
                }
            }
            else if (netInfo.name.Contains("Basic Road"))
            {
                plusPath = pathSmall;
                plusName = roadSmall;

                if (segment.m_segmentMesh.name.Equals("SmallRoadSegment"))
                {
                    plusName += pGn;
                }
                else if (segment.m_segmentMesh.name.Equals("SmallRoadSegmentBusSide"))
                {
                    plusName += BusSide;
                }
                else if (segment.m_segmentMesh.name.Equals("SmallRoadSegmentBusBoth"))
                {
                    plusName += BusBoth;
                }
                else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                {
                    if (netInfo.name.Equals("Basic Road Elevated") || netInfo.name.Equals("Basic Road Bridge"))
                    {
                        plusName += pEl;
                    }
                    else if (netInfo.name.Equals("Basic Road Slope"))
                    {
                        plusName += pSl;
                    }
                }

                if (netInfo.name.Contains("Bicycle"))
                {
                    plusPath = pathSmall;
                    if (segment.m_mesh.name.Equals("SmallRoadSegment"))
                    {
                        plusName = roadSmall + Bike + pGn;
                    }
                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                    {
                        plusName = roadSmall + Bike + BusSide;
                    }
                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                    {
                        plusName = roadSmall + Bike + BusBoth;
                    }
                }
                else if (netInfo.name.Contains("Elevated Bike"))
                {
                    plusPath = pathSmall;
                    plusName = roadSmall + Bike + pEl;
                }
                else if (netInfo.name.Contains("Decoration"))
                {
                    plusPath = pathSmall;
                    if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                    {
                        plusName = roadSmall + Deco;
                    }
                }
            }
            else if (netInfo.name.Contains("Oneway"))
            {
                if (netInfo.name.Equals("One-Lane Oneway"))
                {
                    plusPath = pathTiny;
                    plusName = roadTiny + "1L";
                }
                else if (netInfo.name.Equals("Oneway3L"))
                {
                    plusPath = pathSmall;
                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                    {
                        plusPath = pathSmall;
                        plusName = roadSmall + roadOneway + "3L" + pGn;
                    }
                    else if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                    {
                        plusPath = pathSmall;
                        plusName = roadSmall + roadOneway + "3L" + pEl;
                    }
                    else if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                    {
                        plusPath = pathSmall;
                        plusName = roadSmall + roadOneway + "3L" + pTu;
                    }
                }
                else if (netInfo.name.Equals("Oneway4L"))
                {
                    plusPath = pathSmall;
                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                    {
                        plusPath = pathSmall;
                        plusName = roadSmall + roadOneway + "4L" + pGn;
                    }

                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                    {
                        plusPath = pathSmall;
                        plusName = roadSmall + roadOneway + "4L" + pEl;
                    }

                    if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Tunnel"))
                    {
                        plusPath = pathSmall;
                        plusName = roadSmall + roadOneway + "4L" + pTu;
                    }
                }
                else if (netInfo.name.Equals("Oneway Road"))
                {
                    plusPath = pathSmall;
                    plusName = roadSmall + roadOneway;

                    if (segment.m_mesh.name.Equals("SmallRoadSegment"))
                    {
                        plusName = roadSmall + roadOneway + pGn;
                    }
                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                    {
                        plusName = roadSmall + roadOneway + BusSide;
                    }
                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                    {
                        plusName = roadSmall + roadOneway + BusBoth;
                    }
                    else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                    {
                        if (netInfo.name.Equals("Oneway Road Elevated") || netInfo.name.Equals("Oneway Road Bridge"))
                        {
                            plusName = roadSmall + roadOneway + pEl;
                        }
                        else if (netInfo.name.Equals("Oneway Road Slope"))
                        {
                            plusName = roadSmall + roadOneway + pSl;
                        }
                    }
                }
            }
            else if (netInfo.name.Contains("Medium Road"))
            {
                plusPath = pathMedium;
                plusName = roadMedium;
                if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                {
                    plusName += pGn;
                }
                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                {
                    plusName += BusSide;
                }
                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                {
                    plusName += BusBoth;
                }
                else if (!segment.m_mesh.name.Equals("RoadMediumSegment"))
                {
                    if (netInfo.name.Equals("Medium Road Elevated") || netInfo.name.ToLower().Contains("bridge"))
                    {
                        plusName += pEl;
                    }

                    if (netInfo.name.Equals("Medium Road Slope"))
                    {
                        plusName += pSl;
                    }
                }

                if (netInfo.name.Equals("Medium Road Decoration Grass") || netInfo.name.Equals("Medium Road Decoration Trees"))
                {
                    plusName = roadMedium + Deco;
                    if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                    {
                    }
                    else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                    {
                        plusName += BusSide;
                    }
                    else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                    {
                        plusName += BusBoth;
                    }
                }

                if (netInfo.name.Equals("Medium Road Bicycle"))
                {
                    plusName = roadMedium + Bike;
                    if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                    {
                        plusName += pGn;
                    }
                    else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                    {
                        plusName += BusSide;
                    }
                    else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                    {
                        plusName += BusBoth;
                    }
                }

                if (netInfo.name.Equals("Medium Road Elevated Bike") || netInfo.name.Equals("Medium Road Bridge Bike"))
                {
                    plusName = roadMedium + Bike + pEl;
                }
            }
            else if (netInfo.name.Contains("Large"))
            {
                plusPath = pathLarge;
                plusName = roadLarge;
                if (segment.m_mesh.name.Equals("RoadLargeSegment"))
                {
                    plusName += pGn;
                }
                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                {
                    plusName += BusSide;
                }
                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                {
                    plusName += BusBoth;
                }
                else if (!segment.m_mesh.name.Equals("RoadLargeSegment"))
                {
                    if (netInfo.name.Equals("Large Road Elevated") || netInfo.name.ToLower().Contains("bridge"))
                    {
                        plusName += pEl;
                    }

                    if (netInfo.name.Equals("Large Road Slope"))
                    {
                        plusName += pSl;
                    }
                }

                if (netInfo.name.Equals("Large Road Decoration Grass") || netInfo.name.Equals("Large Road Decoration Trees"))
                {
                    plusName = roadLarge + Deco;
                    if (segment.m_mesh.name.Equals("LargeRoadSegment2"))
                    {
                    }
                    else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusSide"))
                    {
                        // bug? no plusName defined
                        plusName += BusSide;
                    }
                    else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth"))
                    {
                        plusName += BusBoth;
                    }
                }

                if (netInfo.name.Equals("Large Road Bicycle"))
                {
                    plusName = roadLarge + Bike;
                    if (segment.m_mesh.name.Equals("RoadLargeSegment"))
                    {
                        plusName += pGn;
                    }
                    else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                    {
                        plusName += BusSide;
                    }
                    else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                    {
                        plusName += BusBoth;
                    }
                }

                if (netInfo.name.Equals("Large Road Elevated Bike") || netInfo.name.Equals("Large Road Bridge Bike"))
                {
                    plusName = roadLarge + Bike + pEl;
                }

                if (netInfo.name.Contains("Large Oneway"))
                {
                    plusName = roadLarge + roadOneway;
                    if (segment.m_mesh.name.Equals("RoadLargeSegment"))
                    {
                        plusName += pGn;
                    }
                    else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                    {
                        plusName += BusSide;
                    }
                    else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                    {
                        plusName += BusBoth;
                    }
                    else if (!segment.m_mesh.name.Equals("RoadLargeSegment"))
                    {
                        if (netInfo.name.Equals("Large Oneway Elevated") || netInfo.name.ToLower().Contains("bridge"))
                        {
                            plusName += pEl;
                        }
                    }
                }

                if (netInfo.name.Equals("Large Oneway Road Slope"))
                {
                    plusName = roadLarge + roadOneway + pSl;
                }

                if (netInfo.name.Equals("Large Oneway Decoration Grass")
                    || netInfo.name.Equals("Large Oneway Decoration Trees"))
                {
                    plusName = roadLarge + roadOneway + Deco;
                    if (segment.m_mesh.name.Equals("LargeRoadSegment2"))
                    {
                    }
                    else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusSide"))
                    {
                        plusName += BusSide;
                    }
                    else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth"))
                    {
                        plusName += BusBoth;
                    }
                }
            }
            else if (netInfo.name.Contains("Highway"))
            {
                // Vanilla
                plusPath = pathHighway;
                if (netInfo.name.Equals("HighwayRamp") || netInfo.name.Equals("HighwayRampElevated"))
                {
                    plusName = roadHighway + "Ramp";
                }
                else if (netInfo.name.Equals("HighwayRamp Slope"))
                {
                    plusName = roadHighway + "Ramp" + pSl;
                }
                else if (netInfo.name.Equals("Highway Barrier"))
                {
                    plusName = roadHighway + "3L";
                }
                else if (netInfo.name.Equals("Highway Elevated") || segment.m_mesh.name.Equals("HighwayBridgeSegment")
                         || segment.m_mesh.name.Equals("HighwayBaseSegment")
                         || segment.m_mesh.name.Equals("HighwayBarrierSegment"))
                {
                    plusName = roadHighway + "3L";
                }
                else if (segment.m_mesh.name.Equals("highway-tunnel-segment")
                         || segment.m_mesh.name.Equals("highway-tunnel-slope"))
                {
                    plusName = roadHighway + "3L" + pSl;
                }

                // NExT
                if (netInfo.name.Contains("Rural"))
                {
                    if (netInfo.name.Contains("Small"))
                    {
                        plusName = roadHighway + "1L";

                        if (netInfo.name.Equals("Rural Highway Slope"))
                        {
                            plusName = roadHighway + "1L" + pSl;
                        }
                        else if (netInfo.name.Equals("Rural Highway Tunnel"))
                        {
                            plusName = roadHighway + "1L" + pTu;
                        }

                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                        {
                            segment.SetSegmentDirect("Highway1L_Ground_Segment_APRMap" + ext_DDS, TexKind.APRMap, ref log);
                        }
                    }
                    else
                    {
                        plusName = roadHighway + "2L";

                        if (netInfo.name.Equals("Rural Highway Slope"))
                        {
                            plusName = roadHighway + "2L" + pSl;
                        }
                        else if (netInfo.name.Equals("Rural Highway Tunnel"))
                        {
                            plusName = roadHighway + "2L" + pTu;
                        }

                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                        {
                            segment.SetSegmentDirect("Highway2L_Ground_Segment_APRMap" + ext_DDS, TexKind.APRMap, ref log);
                        }
                    }
                }

                if (netInfo.name.Contains("Four-Lane"))
                {
                    plusName = roadHighway + "4L" + pGn;

                    if (netInfo.name.Contains("Elevated"))
                    {
                        plusName = roadHighway + "4L" + pEl;
                    }
                    else if (netInfo.name.Contains("Slope"))
                    {
                        plusName = roadHighway + "4L" + pSl;
                    }
                    else if (netInfo.name.Contains("Tunnel"))
                    {
                        plusName = roadHighway + "4L" + pTu;
                    }
                }

                if (netInfo.name.Contains("Five-Lane"))
                {
                    plusName = roadHighway + "5L";

                    if (netInfo.name.Equals("Five-Lane Highway Slope"))
                    {
                        plusName = roadHighway + "5L" + pSl;
                    }
                    else if (netInfo.name.Equals("Five-Lane Highway Tunnel"))
                    {
                        plusName = roadHighway + "5L" + pTu;
                    }

                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                    {
                        segment.SetSegmentDirect("Highway5L_Ground_Segment_APRMap" + ext_DDS, TexKind.APRMap, ref log);
                    }
                }

                if (netInfo.name.Contains("Large"))
                {
                    plusName = roadHighway + "6L";

                    if (netInfo.name.Contains("Slope"))
                    {
                        plusName = roadHighway + "6L" + pSl;
                    }
                    else if (netInfo.name.Contains("Tunnel"))
                    {
                        plusName = roadHighway + "6L" + pTu;
                    }

                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                    {
                        segment.SetSegmentDirect("Highway6L_Ground_Segment_APRMap" + ext_DDS, TexKind.APRMap, ref log);
                    }
                }
            }
            else if (netInfo.name.Equals("Small Busway"))
            {
                plusPath = pathSmall;
                if (netInfo.name.Contains("OneWay"))
                {
                    if (netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees"))
                    {
                        if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                        {
                            plusName = roadSmall + roadOneway + plusBus + Deco;
                        }
                        else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                        {
                            plusName = roadSmall + roadOneway + Deco + BusSide;
                        }
                        else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                        {
                            plusName = roadSmall + roadOneway + Deco + BusBoth;
                        }
                    }
                    else if (segment.m_mesh.name.Equals("SmallRoadSegment"))
                    {
                        plusName = roadSmall + roadOneway + plusBus + pGn;
                    }
                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                    {
                        plusName = roadSmall + roadOneway + plusBus + BusSide;
                    }
                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                    {
                        plusName = roadSmall + roadOneway + plusBus + BusBoth;
                    }
                    else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                    {
                        if (netInfo.name.Contains("Elevated"))
                        {
                            plusName = roadSmall + roadOneway + plusBus + pEl;
                        }
                        else if (netInfo.name.Contains("Slope"))
                        {
                            plusName = roadSmall + roadOneway + plusBus + pSl;
                        }
                        else if (netInfo.name.Contains("Tunnel"))
                        {
                            plusName = roadSmall + roadOneway + plusBus + pTu;
                        }
                    }
                }
                else
                {
                    plusName = roadSmall + plusBus + pGn;

                    if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                    {
                        if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                        {
                            plusName = roadSmall + plusBus + BusSide;
                        }
                        else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                        {
                            plusName = roadSmall + plusBus + BusBoth;
                        }
                        else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                        {
                            if (netInfo.name.Equals("Small Busway Elevated"))
                            {
                                plusName = roadSmall + plusBus + pEl;
                            }
                            else if (netInfo.name.Equals("Small Busway Slope"))
                            {
                                plusName = roadSmall + plusBus + pSl;
                            }
                            else if (netInfo.name.Equals("Small Busway Tunnel"))
                            {
                                plusName = roadSmall + plusBus + pTu;
                            }
                        }
                    }
                }

                if (netInfo.name.Equals("Small Busway Decoration Grass")
                    || netInfo.name.Equals("Small Busway Decoration Trees"))
                {
                    plusPath = pathSmall;
                    plusName = roadSmall + plusBus + Deco;

                    if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                    {
                    }
                    else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                    {
                        plusName = roadSmall + plusBus + Deco + BusSide;
                    }
                    else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                    {
                        plusName = roadSmall + plusBus + Deco + BusBoth;
                    }
                }
            }
            else if (netInfo.name.Equals("Large Road With Bus Lanes"))
            {
                plusPath = pathLarge;

                if (segment.m_mesh.name.Equals("RoadLageSegment"))
                {
                    // todo: exceptions for original names
                    plusName = roadLarge + plusBus + pGn;
                }
                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                {
                    plusName = roadLarge + plusBus + BusSide;
                }
                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                {
                    plusName = roadLarge + plusBus + BusBoth;
                }

                if (netInfo.name.Contains("Elevated"))
                {
                    plusName = roadLarge + plusBus + pEl;
                }

                if (netInfo.name.Contains("Slope"))
                {
                    plusName = roadLarge + plusBus + pSl;
                }

                if (netInfo.name.Contains("Tunnel"))
                {
                    plusName = roadLarge + plusBus + pTu;
                }

                if (netInfo.name.Contains("Trees") || netInfo.name.Contains("Grass"))
                {
                    if (segment.m_mesh.name.Equals("LargeRoadSegment2"))
                    {
                        plusName = roadLarge + plusBus + Deco;
                        if (File.Exists(Path.Combine(texPath, "Busway6L_DecoGrass_Ground_Segment_MainTex" + ext_DDS)))
                        {
                            segment.SetSegmentDirect(roadlargesegmentDefaultApr + ext_DDS, TexKind.APRMap, ref log);
                        }
                    }
                    else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusSide"))
                    {
                        plusName = roadLarge + plusBus + Deco + BusSide;
                    }
                    else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth"))
                    {
                        plusName = roadLarge + plusBus + Deco + BusBoth;
                    }
                }
            }
            else if (netInfo.name.Contains("Tram"))
            {
                if (netInfo.name.Equals("Basic Road Tram"))
                {
                    if (segment.m_mesh.name.Equals("RoadSmallTramStopSingle")
                        || segment.m_mesh.name.Equals("RoadSmallTramStopDouble")
                        || segment.m_mesh.name.Equals("RoadSmallTramAndBusStop"))
                    {
                        plusName = roadSmall + BusBoth;
                    }
                }
                else if (netInfo.name.Equals("Basic Road Elevated Tram") || netInfo.name.Equals("Basic Road Bridge Tram"))
                {
                    plusName = roadSmall + pEl;
                }
                else if (netInfo.name.Equals("Basic Road Slope Tram"))
                {
                    plusName = roadSmall + pSl;
                }
                else if (netInfo.name.Equals("Oneway Road Tram"))
                {
                    plusName = roadSmall + roadOneway;
                    if (segment.m_mesh.name.Equals("RoadSmallTramStopSingle")
                        || segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                    {
                        plusName = roadSmall + roadOneway + BusBoth;
                    }
                }
                else if (netInfo.name.Equals("Oneway Road Elevated Tram") || netInfo.name.Equals("Oneway Road Bridge Tram"))
                {
                    plusName = roadSmall + roadOneway + pEl;
                }
                else if (netInfo.name.Equals("Oneway Road Slope Tram"))
                {
                    plusName = roadSmall + roadOneway + pSl;
                }
                else if (netInfo.name.Equals("Medium Road Tram") && segment.m_mesh.name.Equals("RoadMediumTramSegment"))
                {
                    plusName = roadMedium + str3 + pGn;
                }
                else if (netInfo.name.Equals("Medium Road Elevated Tram") || netInfo.name.Equals("Medium Road Bridge Tram"))
                {
                    plusName = roadMedium + str3 + pEl;
                }
                else if (netInfo.name.Equals("Medium Road Slope Tram"))
                {
                    plusName = roadMedium + str3 + pSl;
                }
                else if (netInfo.name.Equals("Tram Track") || netInfo.name.Equals("Oneway Tram Track"))
                {
                    plusName = roadSmall + Deco;
                }
                else if (netInfo.name.Equals("Tram Track Elevated") || netInfo.name.Equals("Oneway Tram Track Elevated"))
                {
                    plusName = roadSmall + roadOneway + pEl;
                }
                else if (netInfo.name.Equals("Tram Track Slope") || netInfo.name.Equals("Oneway Tram Track Slope"))
                {
                    plusName = roadSmall + roadOneway + pSl;
                }
            }
            else if (netInfo.name.Contains("Bus"))
            {
                if (netInfo.name.Equals("Medium Road Bus"))
                {
                    if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                    {
                        plusName = roadMedium + plusBus + pGn;
                    }
                    else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                    {
                        plusName = roadMedium + plusBus + BusSide;
                    }
                    else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                    {
                        plusName = roadMedium + plusBus + BusBoth;
                    }
                }
                else if (netInfo.name.Equals("Medium Road Elevated Bus") || netInfo.name.Equals("Medium Road Bridge Bus"))
                {
                    plusName = roadMedium + plusBus + pEl;
                }
                else if (netInfo.name.Equals("Medium Road Slope Bus"))
                {
                    plusName = roadMedium + plusBus + pSl;
                }
                else if (netInfo.name.Contains("Large Road Bus"))
                {
                    if (netInfo.name.Equals("Large Road Bus"))
                    {
                        if (segment.m_mesh.name.Equals("RoadLargeSegmentBusLane"))
                        {
                            plusName = roadLarge + plusBus + pGn;
                        }
                        else if (segment.m_mesh.name.Equals("RoadLargeSegmentBusSideBusLane"))
                        {
                            plusName = roadLarge + plusBus + BusSide;
                        }
                        else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBothBusLane"))
                        {
                            plusName = roadLarge + plusBus + BusBoth;
                        }
                    }
                    else if (netInfo.name.Equals("Large Road Elevated Bus") || netInfo.name.Equals("Large Road Bridge Bus"))
                    {
                        plusName = roadLarge + plusBus + pEl;
                    }
                    else if (netInfo.name.Equals("Large Road Slope Bus"))
                    {
                        plusName = roadLarge + plusBus + pSl;
                    }
                }
            }
            else if (netInfo.name.Equals("Two-Lane Alley"))
            {
                plusPath = pathTiny;
                plusName = roadTiny + "2L";

                if (!SetSegmentTextures(segment, allNames.coreName, plusPath, plusName, ref log))
                {
                    allNames.coreName = "OneWay1L" + typeGround;
                    plusName = roadTiny + "1L";
                }
            }
            else if (netInfo.name.Equals("AsymRoadL1R2"))
            {
                plusPath = pathSmall;
                plusName = roadSmall + str4 + pGn;

                if (netInfo.name.Equals("AsymRoadL1R2 Elevated") || netInfo.name.Equals("AsymRoadL1R2 Bridge"))
                {
                    plusName = roadSmall + str4 + pEl;
                }
                else if (netInfo.name.Equals("AsymRoadL1R2 Slope") && segment.m_mesh.name == "Slope")
                {
                    plusName = roadSmall + str4 + pGn;
                }
                else if (netInfo.name.Equals("AsymRoadL1R2 Tunnel"))
                {
                    plusName = roadSmall + str4 + pTu;
                }

                if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Inverted"))
                {
                    plusName += "_Inv";
                }
            }
            else if (netInfo.name.Equals("AsymRoadL1R3"))
            {
                plusPath = pathSmall;
                plusName = roadSmall + str5 + pGn;

                if (netInfo.name.Equals("AsymRoadL1R3 Elevated") || netInfo.name.Equals("AsymRoadL1R3 Bridge"))
                {
                    plusName = roadSmall + str5 + pEl;
                }
                else if (netInfo.name.Equals("AsymRoadL1R3 Slope") && segment.m_mesh.name == "Slope")
                {
                    plusName = roadSmall + str5 + pGn;
                }
                else if (netInfo.name.Equals("AsymRoadL1R3 Tunnel"))
                {
                    plusName = roadSmall + str4 + pTu;
                }

                if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Inverted"))
                {
                    plusName += "_Inv";
                }
            }
            else if (netInfo.name.Contains("Avenue"))
            {
                if (netInfo.name.Contains("Small"))
                {
                    plusPath = pathSmall;
                    if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Ground"))
                    {
                        allNames.coreName = "SmallAvenue4L" + typeGround;
                        plusName = roadSmall + "4L" + pGn;
                    }
                    else if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                    {
                        allNames.coreName = "SmallAvenue4L" + typeElevated;
                        plusName = roadSmall + "4L" + pEl;
                    }
                    else if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                    {
                        allNames.coreName = "SmallAvenue4L" + typeTunnel;
                        plusName = roadSmall + "4L" + pTu;
                    }
                }
                else if (netInfo.name.Contains("Medium"))
                {
                    plusPath = pathMedium;
                    if (!netInfo.name.Contains("TL"))
                    {
                        if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Ground"))
                        {
                            allNames.coreName = "MediumAvenue4L" + typeGround;
                            plusName = roadMedium + "4L" + pGn;
                        }
                        else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Elevated"))
                        {
                            allNames.coreName = "MediumAvenue4L" + typeElevated;
                            plusName = roadMedium + "4L" + pEl;
                        }
                        else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Slope"))
                        {
                            allNames.coreName = "MediumAvenue4L" + typeSlope;
                            plusName = roadMedium + "4L" + pSl;
                        }
                        else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Tunnel"))
                        {
                            allNames.coreName = "MediumAvenue4L" + typeTunnel;
                            plusName = roadMedium + "4L" + pTu;
                        }
                    }
                    else
                    {
                        // Turning lane
                        if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Ground"))
                        {
                            allNames.coreName = "MediumAvenue4LTL" + typeGround;
                            plusName = roadMedium + "4LTL" + pGn;

                            // Alternate APR textures
                            if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                            {
                                segment.SetSegmentDirect(roadlargesegmentDefaultApr + ext_DDS, TexKind.APRMap, ref log);
                            }
                        }
                        else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Elevated"))
                        {
                            allNames.coreName = "MediumAvenue4LTL" + typeElevated;
                            plusName = roadMedium + "4LTL" + pEl;
                        }
                        else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Slope"))
                        {
                            allNames.coreName = "MediumAvenue4LTL" + typeSlope;
                            plusName = roadMedium + "4LTL" + pSl;
                        }
                        else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Tunnel"))
                        {
                            allNames.coreName = "MediumAvenue4LTL" + typeTunnel;
                            plusName = roadMedium + "4LTL" + pTu;
                        }
                    }
                }
                else if (netInfo.name.Contains("Eight-Lane"))
                {
                    plusPath = pathLarge;
                    allNames.coreName = "LargeAvenue8LM" + typeGround;
                    plusName = roadLarge + "8L" + pGn;

                    if (netInfo.name.Contains("Elevated") || netInfo.name.ToLower().Contains("bridge"))
                    {
                        allNames.coreName = "LargeAvenue8LM" + typeElevated;
                        plusName = roadLarge + "8L" + pEl;
                    }
                    else if (netInfo.name.Contains("Slope"))
                    {
                        allNames.coreName = "LargeAvenue8LM" + typeSlope;
                        plusName = roadLarge + "8L" + pSl;
                    }
                    else if (netInfo.name.Contains("Tunnel"))
                    {
                        allNames.coreName = "LargeAvenue8LM" + typeTunnel;
                        plusName = roadLarge + "8L" + pTu;
                    }
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static bool CheckAndSetNodeMaterial(NetInfo.Node node, [CanBeNull] string requestPath, [CanBeNull] string fileName, ref string log)
        {
            bool main = false;
            if (requestPath.IsNullOrWhiteSpace() || fileName.IsNullOrWhiteSpace())
            {
                return false;
            }

            System.Diagnostics.Debug.Assert(requestPath != null, "requestPath != null");
            if (File.Exists(Path.Combine(requestPath, fileName)))
            {
                log += "\nMainTex found at: " + Path.Combine(requestPath, fileName) + ", returning.";
                node.m_nodeMaterial.SetTexture(maintex, Path.Combine(requestPath, fileName).LoadTextureDDS());
                main = true;
            }
            else if (File.Exists(Path.Combine(requestPath, fileName + nodeSuffix + mainTexSuffix)))
            {
                log += "\nMainTex found at: " + Path.Combine(requestPath, fileName + nodeSuffix + mainTexSuffix + ", returning.");
                node.m_nodeMaterial.SetTexture(
                    maintex,
                    Path.Combine(requestPath, fileName + nodeSuffix + mainTexSuffix).LoadTextureDDS());
                main = true;
            }
            else
            {
                log += "\nNo MainTex found for: " + node + " - " + requestPath + "/" + fileName;
            }

            if (main)
            {
                string aprPath = fileName + nodeSuffix + aprMapSuffix;
                if (node.m_nodeMaterial.GetTexture(aprmap) != null)
                {
                    string aprFileName = node.m_nodeMaterial.GetTexture(aprmap).name + ext_DDS;
                    string path = Path.Combine(ModLoader.currentTexturesPath_default, aprFileName);

                    if (File.Exists(path))
                    {
                        log += "\nAPRMap found at: " + path;
                        node.m_nodeMaterial.SetTexture(
                            aprmap,
                            path.LoadTextureDDS());
                        return true;
                    }

                    string path2 = Path.Combine(requestPath, aprPath);
                    if (File.Exists(path2))
                    {
                        log += "\nAPRMap found at: " + path2;
                        node.m_nodeMaterial.SetTexture(
                            aprmap,
                            Path.Combine(requestPath, fileName + nodeSuffix + aprMapSuffix).LoadTextureDDS());
                        return true;
                    }

                    string path3 = Path.Combine(ModLoader.APRMaps_Path, aprFileName);
                    if (File.Exists(path3))
                    {
                        log += "\nAPRMap found at: " + path3;
                        node.m_nodeMaterial.SetTexture(
                            aprmap,
                            path3.LoadTextureDDS());
                        return true;
                    }

                    string path4 = Path.Combine(ModLoader.APRMaps_Path, aprPath);
                    if (File.Exists(path4))
                    {
                        log += "\nUsing main mod APR map: " + path4;
                        node.m_nodeMaterial.SetTexture(
                            aprmap,
                            path4.LoadTextureDDS());
                        return true;
                    }

                    log += "\nNo apr texture found :(";
                }
            }

            // Debug.Log(log);
            return main;
        }

        private static bool SetNodeTextures(
            NetInfo.Node node,
            [CanBeNull] string coreName,
            string plusPath,
            string plusName, ref string log)
        {
            log += "\nTrying default tex: " + coreName;
            if (coreName == null
                || !CheckAndSetNodeMaterial(node, ModLoader.currentTexturesPath_default, coreName, ref log))
            {
                log += "\nDefault tex not found, trying shitty plus name: " + plusPath + "/" + plusName;

                if (CheckAndSetNodeMaterial(node, plusPath, plusName, ref log))
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        private static void SetShittyNamesPlusNodes(
            [NotNull] NetInfo netInfo,
            out string plusPath,
            out string plusName)
        {
            string texPath = ModLoader.currentTexturesPath_default;
            string pathTiny = texPath + "/" + roadTiny + "/";
            string pathSmall = texPath + "/" + roadSmall + "/";
            string pathMedium = texPath + "/" + roadMedium + "/";
            string pathLarge = texPath + "/" + roadLarge + "/";
            string pathHighway = texPath + "/" + roadHighway + "/";


            plusPath = pathSmall;
            plusName = null;

            if (netInfo.name.Contains("Basic Road"))
            {
                if (netInfo.name.Equals("Basic Road Elevated"))
                {
                    plusName = roadSmall + pEl;
                }
                else if (netInfo.name.Equals("Basic Road Decoration Grass")
                         || netInfo.name.Equals("Basic Road Decoration Trees"))
                {
                    plusName = roadSmall + Deco;
                }
                else
                {
                    plusName = roadSmall + pGn;
                }
            }
            else if (netInfo.name.Contains("Oneway Road"))
            {
                plusName = roadSmall + roadOneway;
                if (netInfo.name.Equals("Oneway Road Elevated"))
                {
                    plusName = plusName + pEl;
                }
                else if (netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees"))
                {
                    plusName = roadSmall + roadOneway + Deco;
                }
                else
                {
                    plusName = plusName + pGn;
                }
            }
            else if (netInfo.name.Equals("Basic Road Bicycle"))
            {
                plusName = roadSmall + pGn;
            }
            else if (netInfo.name.Equals("Basic Road Elevated Bike"))
            {
                plusName = roadSmall + Bike + pEl;
            }
            else if (netInfo.name.Contains("Medium Road") && !netInfo.name.Contains("Tram"))
            {
                plusPath = pathMedium;
                plusName = roadMedium;
                if (netInfo.name.Contains("Medium Road Elevated") && !netInfo.name.Contains("Bike"))
                {
                    plusName = plusName + pEl;
                }
                else if (netInfo.name.Equals("Medium Road Decoration Grass")
                         || netInfo.name.Equals("Medium Road Decoration Trees"))
                {
                    plusName = roadMedium + Deco;
                }
                else
                {
                    plusName = plusName + pGn;
                }
            }
            else if (netInfo.name.Equals("Medium Road Elevated Bike"))
            {
                plusPath = pathMedium;
                plusName = roadMedium + Bike + pEl;
            }
            else if (netInfo.name.Contains("Large Road"))
            {
                plusPath = pathLarge;
                plusName = roadLarge;
                if (netInfo.name.Equals("Large Road Elevated"))
                {
                    plusName = plusName + pEl;
                }
                else if (netInfo.name.Equals("Large Road Decoration Grass")
                         || netInfo.name.Equals("Large Road Decoration Trees"))
                {
                    plusName = roadLarge + Deco;
                }
                else
                {
                    plusName = plusName + pGn;
                }
            }
            else if (netInfo.name.Equals("Large Road Elevated Bike"))
            {
                plusPath = pathLarge;
                plusName = roadLarge + Bike + pEl;
            }
            else if (netInfo.name.Contains("Large Oneway"))
            {
                plusPath = pathLarge;
                plusName = roadLarge + roadOneway;
                if (netInfo.name.Equals("Large Oneway Elevated"))
                {
                    plusName = plusName + pEl;
                }
                else if (netInfo.name.Equals("Large Oneway Decoration Grass")
                         || netInfo.name.Equals("Large Oneway Decoration Trees"))
                {
                    plusName = roadLarge + roadOneway + Deco;
                }
                else if (netInfo.name.Equals("Large Oneway Road Slope"))
                {
                    plusName = roadLarge + roadOneway + pSl;
                }
                else
                {
                    plusName = plusName + pGn;
                }
            }
            else if (netInfo.name.Contains("Highway"))
            {
                plusPath = pathHighway;
                plusName = roadHighway + "3L";

                if (netInfo.name.Equals("HighwayRamp") || netInfo.name.Equals("HighwayRampElevated") || netInfo.name.Equals("HighwayRamp Slope"))
                {
                    plusName = roadHighway + "Ramp";
                }
                else if (netInfo.name.Equals("Highway Elevated"))
                {
                    plusName = roadHighway + "3L";
                }
                else if (netInfo.name.Equals("Highway Slope"))
                {
                    plusName = roadHighway + "3L";
                }
                else if (netInfo.name.Equals("Highway Barrier"))
                {
                    plusName = roadHighway + "3L";
                }
            }
            else if (netInfo.name.Equals("Basic Road Tram"))
            {
                plusName = roadSmall + pGn;
            }
            else if (netInfo.name.Equals("Basic Road Elevated Tram"))
            {
                plusName = roadSmall + pEl;
            }
            else if (netInfo.name.Equals("Basic Road Slope Tram"))
            {
                plusName = roadSmall + pGn;
            }
            else if (netInfo.name.Equals("Oneway Road Tram"))
            {
                plusName = roadSmall + roadOneway + pGn;
            }
            else if (netInfo.name.Equals("Oneway Road Elevated Tram"))
            {
                plusName = roadSmall + roadOneway + pEl;
            }
            else if (netInfo.name.Equals("Oneway Road Slope Tram"))
            {
                plusName = roadSmall + roadOneway + pGn;
            }
            else if (netInfo.name.Equals("Medium Road Tram"))
            {
                plusPath = pathMedium;
                plusName = roadMedium + str3 + pGn;
            }
            else if (netInfo.name.Equals("Medium Road Elevated Tram"))
            {
                plusPath = pathMedium;
                plusName = roadMedium + str3 + pEl;
            }
            else if (netInfo.name.Equals("Medium Road Slope Tram"))
            {
                plusPath = pathMedium;
                plusName = roadMedium + str3 + pGn;
            }
            else if (netInfo.name.Equals("Tram Track"))
            {
                plusName = roadSmall + Deco;
            }
            else if (netInfo.name.Equals("Tram Track Elevated"))
            {
                plusName = roadSmall + pEl;
            }
            else if (netInfo.name.Equals("Tram Track Slope"))
            {
                plusName = roadSmall + Deco;
            }
            else if (netInfo.name.Equals("Oneway Tram Track"))
            {
                plusName = roadSmall + Deco;
            }
            else if (netInfo.name.Equals("Oneway Tram Track Elevated"))
            {
                plusName = roadSmall + pEl;
            }
            else if (netInfo.name.Equals("Oneway Tram Track Slope"))
            {
                plusName = roadSmall + Deco;
            }
            else if (netInfo.name.Equals("Two-Lane Alley"))
            {
                plusPath = pathTiny;
                plusName = roadTiny + "1L";
            }
            else if (netInfo.name.Equals("One-Lane Oneway"))
            {
                plusPath = pathTiny;
                plusName = roadTiny + "1L";
            }
            else if (netInfo.name.Contains("Eight-Lane Avenue"))
            {
                plusPath = pathLarge;
                plusName = roadLarge + "8L" + pGn;

                if (netInfo.name.Equals("Eight-Lane Avenue Elevated"))
                {
                    plusName = roadLarge + "8L" + pEl;
                }
                else if (netInfo.name.Equals("Eight-Lane Avenue Slope"))
                {
                    plusName = roadLarge + "8L" + pSl;
                }
                else if (netInfo.name.Equals("Eight-Lane Avenue Tunnel"))
                {
                    plusName = roadLarge + "8L" + pTu;
                }
            }
            else if (netInfo.name.Contains("Highway"))
            {
                if (netInfo.name.Contains("Small"))
                {
                    plusPath = pathHighway;
                    plusName = roadHighway + "1L";

                    if (netInfo.name.Equals("Small Rural Highway Slope"))
                    {
                        plusName = roadHighway + "1L" + pSl;
                    }

                    if (netInfo.name.Equals("Small Rural Highway Tunnel"))
                    {
                        plusName = roadHighway + "1L" + pTu;
                    }
                }
                else if (netInfo.name.Contains("Rural"))
                {
                    plusPath = pathHighway;
                    plusName = roadHighway + "2L";

                    if (netInfo.name.Equals("Rural Highway Slope"))
                    {
                        plusPath = pathHighway;
                        plusName = roadHighway + "2L" + pSl;
                    }

                    if (netInfo.name.Equals("Rural Highway Tunnel"))
                    {
                        plusPath = pathHighway;
                        plusName = roadHighway + "2L" + pTu;
                    }
                }
                else if (netInfo.name.Contains("Four-Lane"))
                {
                    plusPath = pathHighway;
                    plusName = roadHighway + "4L" + pGn;

                    if (netInfo.name.Equals("Four-Lane Highway Elevated"))
                    {
                        plusName = roadHighway + "4L" + pEl;
                    }

                    if (netInfo.name.Equals("Four-Lane Highway Slope"))
                    {
                        plusName = roadHighway + "4L" + pSl;
                    }

                    if (netInfo.name.Equals("Four-Lane Highway Tunnel"))
                    {
                        plusPath = pathHighway;
                        plusName = roadHighway + "4L" + pTu;
                    }
                }
                else if (netInfo.name.Contains("Five-Lane"))
                {
                    plusPath = pathHighway;
                    plusName = roadHighway + "5L" + pGn;

                    if (netInfo.name.Equals("Five-Lane Highway Slope"))
                    {
                        plusName = roadHighway + "5L" + pSl;
                    }

                    if (netInfo.name.Equals("Five-Lane Highway Tunnel"))
                    {
                        plusName = roadHighway + "5L" + pTu;
                    }
                }
                else if (netInfo.name.Contains("Large"))
                {
                    plusPath = pathHighway;
                    plusName = roadHighway + "6L";

                    if (netInfo.name.Equals("Large Highway Slope"))
                    {
                        plusName = roadHighway + "6L" + pSl;
                    }

                    if (netInfo.name.Equals("Large Highway Tunnel"))
                    {
                        plusName = roadHighway + "6L" + pTu;
                    }
                }
            }

        }

        private static bool SetSegmentTextures(
            NetInfo.Segment segment,
            [CanBeNull] string coreName,
            string plusPath,
            string plusName, ref string log)
        {
            bool replaced = false;
            if (coreName.IsNullOrWhiteSpace() || !segment.CheckAndSetSegmentMaterial(ModLoader.currentTexturesPath_default, coreName, ref log))
            {
                if (segment.CheckAndSetSegmentMaterial(plusPath, plusName, ref log))
                {
                    replaced = true;
                }
            }
            else
            {
                replaced = true;
            }

            return replaced;
        }

        #endregion Private Methods
    }
}