namespace RoadsUnited_Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using ColossalFramework;

    using JetBrains.Annotations;
    using UnityEngine;
    using static Strings;

    public enum TexKind
    {
        MainTex,
        APRMap
    }

    public class RoadsUnited_Core : MonoBehaviour
    {
        #region Public Fields

        public static Configuration config;

        public static Dictionary<string, Texture2D> vanillaPrefabProperties = new Dictionary<string, Texture2D>();

        #endregion Public Fields

        #region Private Fields

        private static readonly List<string> blacklist =
            new List<string>
                {
                    "Heating Pipe",
                    "Water",
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

        private static Texture2D acimapTex;

        private static Texture2D aprmapTex;

        private static Texture2D defaultmapTex;

        #endregion Private Fields

        #region Public Methods

        public static void ApplyVanillaDicts()
        {
            ApplyVanillaPropDictionary();
            ApplyVanillaRoadDictionary();
        }

        public static void ApplyVanillaPropDictionary()
        {
            uint num = 0u;
            while (num < (ulong)PrefabCollection<NetInfo>.LoadedCount())
            {
                NetInfo loaded = PrefabCollection<NetInfo>.GetLoaded(num);
                if (loaded == null)
                {
                    continue;
                }

                string text = loaded.name.Replace(" ", "_").ToLowerInvariant().Trim();
                string className = loaded.m_class.name;

                NetInfo.Node[] nodes = loaded.m_nodes;
                for (int i = 0; i < nodes.Length; i++)
                {
                    NetInfo.Node node = nodes[i];

                    if (node.m_nodeMaterial == null)
                    {
                        continue;
                    }

                    if (blacklist.Any(x => className.Contains(x)))
                    {
                        continue;
                    }

                    if (vanillaPrefabProperties.TryGetValue(
                        string.Concat(text, "_node_", i, "_nodeMaterial_MainTex"),
                        out defaultmapTex))
                    {
                        node.m_nodeMaterial.SetTexture(maintex, defaultmapTex);
                    }

                    if (vanillaPrefabProperties.TryGetValue(
                        string.Concat(text, "_node_", i, "_nodeMaterial_APRMap"),
                        out aprmapTex))
                    {
                        node.m_nodeMaterial.SetTexture(aprmap, aprmapTex);
                    }
                }

                NetInfo.Segment[] segments = loaded.m_segments;
                for (int j = 0; j < segments.Length; j++)
                {
                    NetInfo.Segment segment = segments[j];
                    {
                        if (segment.m_segmentMaterial == null)
                        {
                            continue;
                        }

                        if (blacklist.Any(x => className.Contains(x)))
                        {
                            continue;
                        }

                        if (vanillaPrefabProperties.TryGetValue(
                            string.Concat(text, "_segment_", j, "_segmentMaterial_MainTex"),
                            out defaultmapTex))
                        {
                            segment.m_segmentMaterial.SetTexture(maintex, defaultmapTex);
                        }

                        if (vanillaPrefabProperties.TryGetValue(
                            string.Concat(text, "_segment_", j, "_segmentMaterial_APRMap"),
                            out aprmapTex))
                        {
                            segment.m_segmentMaterial.SetTexture(aprmap, aprmapTex);
                        }
                    }
                }

                num += 1u;
            }

            PropCollection[] array = FindObjectsOfType<PropCollection>();
            foreach (PropCollection propCollection in array)
            {
                try
                {
                    PropInfo[] prefabs = propCollection.m_prefabs;
                    foreach (PropInfo propInfo in prefabs)
                    {
                        string name = propInfo.name;
                        if (propInfo.m_lodMaterialCombined.GetTexture(maintex) == null)
                        {
                            continue;
                        }

                        if (!name.Contains("Arrow"))
                        {
                            continue;
                        }

                        if (vanillaPrefabProperties.TryGetValue(name + "_prop_MainTex", out defaultmapTex))
                        {
                            propInfo.m_lodMaterialCombined.SetTexture(maintex, defaultmapTex);
                        }

                        if (vanillaPrefabProperties.TryGetValue(name + "_prop_ACIMap", out acimapTex))
                        {
                            propInfo.m_lodMaterialCombined.SetTexture("_ACIMap", acimapTex);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public static void ApplyVanillaRoadDictionary()
        {
            uint num = 0u;
            while (num < (ulong)PrefabCollection<NetInfo>.LoadedCount())
            {
                NetInfo loaded = PrefabCollection<NetInfo>.GetLoaded(num);
                if (loaded == null)
                {
                    continue;
                }

                string text = loaded.name.Replace(" ", "_").ToLowerInvariant().Trim();

                NetInfo.Node[] nodes = loaded.m_nodes;
                for (int i = 0; i < nodes.Length; i++)
                {
                    NetInfo.Node node = nodes[i];

                    if (node.m_nodeMaterial.GetTexture(maintex) == null)
                    {
                        continue;
                    }

                    if (blacklist.Any(x => text.Contains(x)))
                    {
                        continue;
                    }

                    if (vanillaPrefabProperties.TryGetValue(
                        string.Concat(text, "_node_", i, "_nodeMaterial_MainTex"),
                        out defaultmapTex))
                    {
                        node.m_nodeMaterial.SetTexture(maintex, defaultmapTex);
                    }

                    if (vanillaPrefabProperties.TryGetValue(
                        string.Concat(text, "_node_", i, "_nodeMaterial_APRMap"),
                        out aprmapTex))
                    {
                        node.m_nodeMaterial.SetTexture(aprmap, aprmapTex);
                    }
                }

                NetInfo.Segment[] segments = loaded.m_segments;
                for (int j = 0; j < segments.Length; j++)
                {
                    NetInfo.Segment segment = segments[j];
                    if (segment.m_segmentMaterial.GetTexture(maintex) == null)
                    {
                        continue;
                    }

                    if (blacklist.Any(x => text.Contains(x)))
                    {
                        continue;
                    }

                    if (vanillaPrefabProperties.TryGetValue(
                        string.Concat(text, "_segment_", j, "_segmentMaterial_MainTex"),
                        out defaultmapTex))
                    {
                        segment.m_segmentMaterial.SetTexture(maintex, defaultmapTex);
                    }

                    if (vanillaPrefabProperties.TryGetValue(
                        string.Concat(text, "_segment_", j, "_segmentMaterial_APRMap"),
                        out aprmapTex))
                    {
                        segment.m_segmentMaterial.SetTexture(aprmap, aprmapTex);
                    }
                }

                num += 1u;
            }
        }

        public static void CreateVanillaDictionary()
        {
            string log = "Creating vanilla dictionary: ";
            uint num = 0u;
            while (num < (ulong)PrefabCollection<NetInfo>.LoadedCount())
            {
                NetInfo loaded = PrefabCollection<NetInfo>.GetLoaded(num);
                if (loaded == null)
                {
                    continue;
                }

                string className = loaded.m_class.name;
                if (className.IsNullOrWhiteSpace())
                {
                    continue;
                }

                string collectionName = loaded.name.Replace(" ", "_").ToLowerInvariant().Trim();

                NetInfo.Node[] nodes = loaded.m_nodes;
                for (int i = 0; i < nodes.Length; i++)
                {
                    NetInfo.Node node = nodes[i];
                    {
                        if (node.m_nodeMaterial.GetTexture(maintex) == null)
                        {
                            continue;
                        }

                        if (blacklist.Any(x => className.Contains(x)))
                        {
                            continue;
                        }

                        vanillaPrefabProperties.Add(
                            string.Concat(collectionName, "_node_", i, "_nodeMaterial_MainTex"),
                            node.m_nodeMaterial.GetTexture(maintex) as Texture2D);
                        vanillaPrefabProperties.Add(
                            string.Concat(collectionName, "_node_", i, "_nodeMaterial_APRMap"),
                            node.m_nodeMaterial.GetTexture(aprmap) as Texture2D);
                        log += "\n" + string.Concat(collectionName, "_node_", i, "_nodeMaterial_MainTex");
                    }
                }

                NetInfo.Segment[] segments = loaded.m_segments;
                for (int j = 0; j < segments.Length; j++)
                {
                    NetInfo.Segment segment = segments[j];
                    {
                        if (segment.m_segmentMaterial.GetTexture(maintex) == null)
                        {
                            continue;
                        }

                        if (blacklist.Any(x => className.Contains(x)))
                        {
                            continue;
                        }

                        vanillaPrefabProperties.Add(
                            string.Concat(collectionName, "_segment_", j, "_segmentMaterial_MainTex"),
                            segment.m_segmentMaterial.GetTexture(maintex) as Texture2D);
                        vanillaPrefabProperties.Add(
                            string.Concat(collectionName, "_segment_", j, "_segmentMaterial_APRMap"),
                            segment.m_segmentMaterial.GetTexture(aprmap) as Texture2D);
                        log += "\n" + string.Concat(collectionName, "_segment_", j, "_nodeMaterial_MainTex");
                    }
                }

                num += 1u;
            }

            PropCollection[] array = FindObjectsOfType<PropCollection>();
            foreach (PropCollection propCollection in array)
            {
                try
                {
                    log += "\nPropCollection: " + propCollection;

                    PropInfo[] prefabs = propCollection.m_prefabs;
                    foreach (PropInfo propInfo in prefabs)
                    {
                        if (propInfo.m_lodMaterialCombined.GetTexture(maintex) == null)
                        {
                            continue;
                        }

                        string name = propInfo.name;
                        if (name.IsNullOrWhiteSpace() || !name.Contains("Arrow"))
                        {
                            continue;
                        }

                        vanillaPrefabProperties.Add(
                            name + "_prop_MainTex",
                            propInfo.m_lodMaterialCombined.GetTexture(maintex) as Texture2D);
                        vanillaPrefabProperties.Add(
                            name + "_prop_ACIMap",
                            propInfo.m_lodMaterialCombined.GetTexture("_ACIMap") as Texture2D);
                        log += "\n" + name + "_prop_MainTex";
                    }

                    Debug.Log(log);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }
        }

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
                {
                    foreach (NetInfo.Node node in netInfo.m_nodes)
                    {
                        string coreName = string.Empty;
                        string plusPath = string.Empty;
                        string plusName = string.Empty;

                        if (node.m_nodeMaterial.GetTexture(maintex) == null)
                        {
                            continue;
                        }

                        if (blacklist.Any(x => className.Contains(x)))
                        {
                            continue;
                        }

                        string fullPath = Path.Combine(
                            ModLoader.currentTexturesPath_default,
                            node.m_nodeMaterial.GetTexture(maintex).name + ext_DDS);

                        // string netInfoName = netInfo.fileName.Replace(" ", "_").ToLowerInvariant().Trim();
                        plusName = roadSmall + typeShitGnd;

                        if (File.Exists(fullPath))
                        {
                            {
                                node.m_nodeMaterial.SetTexture(maintex, fullPath.LoadTextureDDS());
                                log += "\n" + fullPath;
                            }

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
                        else if (File.Exists(Path.Combine(pathSmall, plusName + nodeSuffix + mainTexSuffix)))
                        {
                            SetPlusNodes(
                                netInfo,
                                pathSmall,
                                pathMedium,
                                pathLarge,
                                pathHighway,
                                out plusPath,
                                out plusName);
                        }

                        if (netInfo.name.Equals("Two-Lane Alley"))
                        {
                            coreName = "Alley2L_Ground";
                            plusPath = pathTiny;
                            plusName = roadTiny + "1L";
                        }
                        else if (netInfo.name.Equals("One-Lane Oneway"))
                        {
                            coreName = "OneWay1L_Ground";
                            plusPath = pathTiny;
                            plusName = roadTiny + "1L";
                        }
                        else if (netInfo.name.Contains("Eight-Lane Avenue"))
                        {
                            coreName = "LargeAvenue8LM_Ground";
                            plusPath = pathLarge;
                            plusName = roadLarge + "8L" + typeShitGnd;

                            if (netInfo.name.Equals("Eight-Lane Avenue Elevated"))
                            {
                                coreName = "LargeAvenue8LM_Elevated";
                                plusName = roadLarge + "8L" + typeElevated;
                            }
                            else if (netInfo.name.Equals("Eight-Lane Avenue Slope"))
                            {
                                plusName = roadLarge + "8L" + typeSlope;
                            }
                            else if (netInfo.name.Equals("Eight-Lane Avenue Tunnel"))
                            {
                                plusName = roadLarge + "8L" + typeTunnel;
                            }
                        }
                        else if (netInfo.name.Contains("Highway"))
                        {
                            if (netInfo.name.Contains("Small"))
                            {
                                coreName = "Highway2L_Ground";
                                plusPath = pathHighway;
                                plusName = roadHighway + "1L";

                                if (netInfo.name.Equals("Small Rural Highway Slope"))
                                {
                                    coreName = "Highway1L_Slope";
                                    plusName = roadHighway + "1L" + typeSlope;
                                }

                                if (netInfo.name.Equals("Small Rural Highway Tunnel"))
                                {
                                    plusName = roadHighway + "1L" + typeTunnel;
                                }
                            }
                            else if (netInfo.name.Contains("Rural"))
                            {
                                coreName = "Highway2L_Ground";
                                plusPath = pathHighway;
                                plusName = roadHighway + "2L";

                                if (netInfo.name.Equals("Rural Highway Slope"))
                                {
                                    coreName = "Highway2L_Slope";
                                    plusPath = pathHighway;
                                    plusName = roadHighway + "2L" + typeSlope;
                                }

                                if (netInfo.name.Equals("Rural Highway Tunnel"))
                                {
                                    plusPath = pathHighway;
                                    plusName = roadHighway + "2L" + typeTunnel;
                                }
                            }
                            else if (netInfo.name.Contains("Four-Lane"))
                            {
                                coreName = "Highway4L_Ground";
                                plusPath = pathHighway;
                                plusName = roadHighway + "4L" + typeShitGnd;

                                if (netInfo.name.Equals("Four-Lane Highway Elevated"))
                                {
                                    coreName = "Highway4L_Elevated";
                                    plusName = roadHighway + "4L" + typeElevated;
                                }

                                if (netInfo.name.Equals("Four-Lane Highway Slope"))
                                {
                                    coreName = "Highway4L_Slope";
                                    plusName = roadHighway + "4L" + typeSlope;
                                }

                                if (netInfo.name.Equals("Four-Lane Highway Tunnel"))
                                {
                                    plusPath = pathHighway;
                                    plusName = roadHighway + "4L" + typeTunnel;
                                }
                            }
                            else if (netInfo.name.Contains("Five-Lane"))
                            {
                                coreName = "Highway5L_Ground";
                                plusPath = pathHighway;
                                plusName = roadHighway + "5L" + typeShitGnd;

                                if (netInfo.name.Equals("Five-Lane Highway Slope"))
                                {
                                    coreName = "Highway5L_Slope";
                                    plusName = roadHighway + "5L" + typeSlope;
                                }

                                if (netInfo.name.Equals("Five-Lane Highway Tunnel"))
                                {
                                    coreName = "Highway5L_Tunnel";
                                    plusName = roadHighway + "5L" + typeTunnel;
                                }
                            }
                            else if (netInfo.name.Contains("Large"))
                            {
                                coreName = "Highway6L_Ground";
                                plusPath = pathHighway;
                                plusName = roadHighway + "6L";

                                if (netInfo.name.Equals("Large Highway Slope"))
                                {
                                    coreName = "Highway6L_Slope";
                                    plusName = roadHighway + "6L" + typeSlope;
                                }

                                if (netInfo.name.Equals("Large Highway Tunnel"))
                                {
                                    coreName = "Highway6L_Tunnel";
                                    plusName = roadHighway + "6L" + typeTunnel;
                                }
                            }
                        }

                        SetNodeTextures(node, coreName, plusPath, plusName);

                        if (node.m_nodeMaterial.GetTexture(aprmap) != null)
                        {
                            string aprPath = Path.Combine(
                                texPath,
                                node.m_nodeMaterial.GetTexture(aprmap).name + ext_DDS);
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

                        log += "\n" + node + " - " + coreName + " | " + plusPath + "/" + plusName;
                    }

                    foreach (NetInfo.Segment segment in netInfo.m_segments)
                    {
                        if (segment.m_segmentMaterial.GetTexture(maintex) == null)
                        {
                            continue;
                        }

                        string coreName = string.Empty;
                        string plusPath = string.Empty;
                        string plusName = string.Empty;
                        string fileName = segment.m_segmentMaterial.GetTexture(maintex).name;
                        if (fileName.IsNullOrWhiteSpace())
                        {
                            continue;
                        }

                        if (netInfo.name.Contains("Basic Road"))
                        {
                            plusPath = pathSmall;
                            plusName = roadSmall;

                            if (segment.m_segmentMesh.name.Equals("SmallRoadSegment"))
                            {
                                plusName += typeShitGnd;
                                if (File.Exists(Path.Combine(texPath, "RoadSmall_D.dds")))
                                {
                                    fileName = "RoadSmall_D";
                                }
                            }
                            else if (segment.m_segmentMesh.name.Equals("SmallRoadSegmentBusSide"))
                            {
                                plusName += BusSide;
                                if (File.Exists(Path.Combine(texPath, "RoadSmall_D_BusSide.dds")))
                                {
                                    fileName = "RoadSmall_D_BusSide";
                                }
                            }
                            else if (segment.m_segmentMesh.name.Equals("SmallRoadSegmentBusBoth"))
                            {
                                plusName += BusBoth;
                                if (File.Exists(Path.Combine(texPath, "RoadSmall_D_BusBoth.dds")))
                                {
                                    fileName = "RoadSmall_D_BusBoth";
                                }
                            }
                            else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                            {
                                if (netInfo.name.Equals("Basic Road Elevated")
                                    || netInfo.name.Equals("Basic Road Bridge"))
                                {
                                    plusName += typeElevated;
                                }
                                else if (netInfo.name.Equals("Basic Road Slope"))
                                {
                                    plusName += typeSlope;
                                }
                            }
                        }
                        else if (netInfo.name.Contains("Oneway Road"))
                        {
                            plusPath = pathSmall;
                            plusName = roadSmall + roadOneway;

                            if (segment.m_mesh.name.Equals("SmallRoadSegment"))
                            {
                                plusName = roadSmall + roadOneway + typeShitGnd;
                                if (File.Exists(Path.Combine(texPath, "Oneway_RoadSmallSegment")))
                                {
                                    fileName = "Oneway_RoadSmallSegment";
                                }
                            }
                            else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                            {
                                plusName = roadSmall + roadOneway + BusSide;
                                if (File.Exists(Path.Combine(texPath, "Oneway_RoadSmallSegment_BusSide.dds")))
                                {
                                    fileName = "Oneway_RoadSmallSegment_BusSide";
                                }
                            }
                            else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                            {
                                plusName = roadSmall + roadOneway + BusBoth;
                                if (File.Exists(Path.Combine(texPath, "Oneway_RoadSmallSegment_BusBoth.dds")))
                                {
                                    fileName = "Oneway_RoadSmallSegment_BusBoth";
                                }
                            }
                            else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                            {
                                if (netInfo.name.Equals("Oneway Road Elevated")
                                    || netInfo.name.Equals("Oneway Road Bridge"))
                                {
                                    plusName = roadSmall + roadOneway + typeElevated;
                                    if (File.Exists(Path.Combine(texPath, "Oneway_RoadSmallElevatedSegment_D.dds")))
                                    {
                                        fileName = "Oneway_RoadSmallElevatedSegment_D";
                                    }
                                }
                                else if (netInfo.name.Equals("Oneway Road Slope"))
                                {
                                    plusName = roadSmall + roadOneway + typeSlope;
                                    if (File.Exists(Path.Combine(texPath, "Oneway_small-tunnel_d.dds")))
                                    {
                                        fileName = "Oneway_small-tunnel_d";
                                    }
                                }
                            }

                            if (netInfo.name.Equals("Oneway Road Decoration Grass")
                                || netInfo.name.Equals("Oneway Road Decoration Trees"))
                            {
                                if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                                {
                                    plusName = roadSmall + roadOneway + Deco;
                                    if (File.Exists(Path.Combine(texPath, "Oneway_SmallRoadSegmentDeco.dds")))
                                    {
                                        fileName = "Oneway_SmallRoadSegmentDeco";
                                    }
                                }
                                else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                                {
                                    plusName = roadSmall + roadOneway + Deco + BusBoth;
                                    if (File.Exists(Path.Combine(texPath, "Oneway_SmallRoadSegmentDeco_BusSide.dds")))
                                    {
                                        fileName = "Oneway_SmallRoadSegmentDeco_BusSide";
                                    }
                                }
                                else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                                {
                                    plusName = roadSmall + roadOneway + Deco + BusBoth;
                                    if (File.Exists(Path.Combine(texPath, "Oneway_SmallRoadSegmentDeco_BusBoth.dds")))
                                    {
                                        fileName = "Oneway_SmallRoadSegmentDeco_BusBoth";
                                    }
                                }
                            }
                        }
                        else if (netInfo.name.Equals("Basic Road Bicycle"))
                        {
                            plusPath = pathSmall;
                            if (segment.m_mesh.name.Equals("SmallRoadSegment"))
                            {
                                plusName = roadSmall + Bike + typeShitGnd;
                            }
                            else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                            {
                                plusName = roadSmall + Bike + BusSide;
                                if (ModLoader.config.basic_road_parking == 1)
                                {
                                    if (fileName.Equals("RoadSmall_D_BusSide"))
                                    {
                                        fileName = "RoadSmall_D_BusSide_parking1";
                                    }
                                }
                            }
                            else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                            {
                                plusName = roadSmall + Bike + BusBoth;
                            }
                        }
                        else if (netInfo.name.Equals("Basic Road Elevated Bike"))
                        {
                            plusPath = pathSmall;
                            plusName = roadSmall + Bike + typeElevated;
                        }
                        else if (netInfo.name.Equals("Basic Road Decoration Grass")
                                 || netInfo.name.Equals("Basic Road Decoration Trees"))
                        {
                            plusPath = pathSmall;
                            if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                            {
                                plusName = roadSmall + Deco;
                            }
                            else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                            {
                                plusName = roadSmall + Deco + BusSide;
                                if (File.Exists(Path.Combine(texPath, "SmallRoadSegmentDeco_BusSide.dds")))
                                {
                                    fileName = "SmallRoadSegmentDeco_BusSide";
                                }
                            }
                            else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                            {
                                plusName = roadSmall + Deco + BusBoth;
                                if (File.Exists(Path.Combine(texPath, "SmallRoadSegmentDeco_BusSide.dds")))
                                {
                                    fileName = "SmallRoadSegmentDeco_BusBoth";
                                }
                            }
                        }
                        else if (netInfo.name.Contains("Medium Road"))
                        {
                            plusPath = pathMedium;
                            plusName = roadMedium;
                            if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                            {
                                plusName += typeShitGnd;
                            }
                            else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                            {
                                plusName += BusSide;
                                if (File.Exists(Path.Combine(texPath, "RoadMedium_D_BusSide.dds")))
                                {
                                    fileName = "RoadMedium_D_BusSide";
                                }
                            }
                            else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                            {
                                plusName += BusBoth;
                                if (File.Exists(Path.Combine(texPath, "RoadMedium_D_BusBoth.dds")))
                                {
                                    fileName = "RoadMedium_D_BusBoth";
                                }
                            }
                            else if (!segment.m_mesh.name.Equals("RoadMediumSegment"))
                            {
                                if (netInfo.name.Equals("Medium Road Elevated")
                                    || netInfo.name.ToLower().Contains("bridge"))
                                {
                                    plusName += typeElevated;
                                }

                                if (netInfo.name.Equals("Medium Road Slope"))
                                {
                                    plusName += typeSlope;
                                }
                            }

                            if (netInfo.name.Equals("Medium Road Decoration Grass")
                                || netInfo.name.Equals("Medium Road Decoration Trees"))
                            {
                                plusName = roadMedium + Deco;
                                if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                                {
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                {
                                    plusName += BusSide;
                                    if (File.Exists(Path.Combine(texPath, "RoadMediumDeco_d_BusSide.dds")))
                                    {
                                        fileName = "RoadMediumDeco_d_BusSide";
                                    }
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                {
                                    plusName += BusBoth;
                                    if (File.Exists(Path.Combine(texPath, "RoadMediumDeco_d_BusBoth.dds")))
                                    {
                                        fileName = "RoadMediumDeco_d_BusBoth";
                                    }
                                }
                            }

                            if (netInfo.name.Equals("Medium Road Bicycle"))
                            {
                                plusName = roadMedium + Bike;
                                if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                                {
                                    plusName += typeShitGnd;
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

                            if (netInfo.name.Equals("Medium Road Elevated Bike")
                                || netInfo.name.Equals("Medium Road Bridge Bike"))
                            {
                                plusName = roadMedium + Bike + typeElevated;
                            }
                        }
                        else if (netInfo.name.Contains("Large"))
                        {
                            plusPath = pathLarge;
                            plusName = roadLarge;
                            if (segment.m_mesh.name.Equals("RoadLargeSegment"))
                            {
                                plusName += typeShitGnd;
                            }
                            else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                            {
                                plusName += BusSide;
                                if (File.Exists(Path.Combine(texPath, "RoadLargeSegment_d_BusSide.dds")))
                                {
                                    fileName = "RoadLargeSegment_d_BusSide";
                                }
                            }
                            else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                            {
                                plusName += BusBoth;
                                if (File.Exists(Path.Combine(texPath, "RoadLargeSegment_d_BusBoth.dds")))
                                {
                                    fileName = "RoadLargeSegment_d_BusBoth";
                                }
                            }
                            else if (!segment.m_mesh.name.Equals("RoadLargeSegment"))
                            {
                                if (netInfo.name.Equals("Large Road Elevated")
                                    || netInfo.name.ToLower().Contains("bridge"))
                                {
                                    plusName += typeElevated;
                                }

                                if (netInfo.name.Equals("Large Road Slope"))
                                {
                                    plusName += typeSlope;
                                }
                            }

                            if (netInfo.name.Equals("Large Road Decoration Grass")
                                || netInfo.name.Equals("Large Road Decoration Trees"))
                            {
                                plusName = roadLarge + Deco;
                                if (segment.m_mesh.name.Equals("LargeRoadSegment2"))
                                {
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusSide"))
                                {
                                    // bug? no plusName defined
                                    plusName += BusSide;
                                    if (File.Exists(Path.Combine(texPath, "RoadLargeOnewaySegment_d_BusSide.dds")))
                                    {
                                        fileName = "RoadLargeOnewaySegment_d_BusSide";
                                    }
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth"))
                                {
                                    plusName += BusBoth;
                                    if (File.Exists(Path.Combine(texPath, "RoadLargeSegmentDecoBusBoth_d.dds")))
                                    {
                                        fileName = "RoadLargeSegmentDecoBusBoth_d";
                                    }
                                }
                            }

                            if (netInfo.name.Equals("Large Road Bicycle"))
                            {
                                plusName = roadLarge + Bike;
                                if (segment.m_mesh.name.Equals("RoadLargeSegment"))
                                {
                                    plusName += typeShitGnd;
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

                            if (netInfo.name.Equals("Large Road Elevated Bike")
                                || netInfo.name.Equals("Large Road Bridge Bike"))
                            {
                                plusName = roadLarge + Bike + typeElevated;
                            }

                            if (netInfo.name.Contains("Large Oneway"))
                            {
                                plusName = roadLarge + roadOneway;
                                if (segment.m_mesh.name.Equals("RoadLargeSegment"))
                                {
                                    plusName += typeShitGnd;
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                {
                                    plusName += BusSide;
                                    if (File.Exists(Path.Combine(texPath, "RoadLargeOnewaySegment_d_BusSide.dds")))
                                    {
                                        fileName = "RoadLargeOnewaySegment_d_BusSide";
                                    }
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                {
                                    plusName += BusBoth;
                                    if (File.Exists(Path.Combine(texPath, "RoadLargeOnewaySegment_d_BusBoth.dds")))
                                    {
                                        fileName = "RoadLargeOnewaySegment_d_BusBoth";
                                    }
                                }
                                else if (!segment.m_mesh.name.Equals("RoadLargeSegment"))
                                {
                                    if (netInfo.name.Equals("Large Oneway Elevated")
                                        || netInfo.name.ToLower().Contains("bridge"))
                                    {
                                        plusName += typeElevated;
                                    }
                                }
                            }

                            if (netInfo.name.Equals("Large Oneway Road Slope"))
                            {
                                plusName = roadLarge + roadOneway + typeSlope;
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
                                    if (File.Exists(Path.Combine(texPath, "RoadLargeSegmentDecoBusSide_d.dds")))
                                    {
                                        fileName = "RoadLargeSegmentDecoBusSide_d";
                                    }
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth"))
                                {
                                    plusName += BusBoth;
                                    if (File.Exists(Path.Combine(texPath, "RoadLargeSegmentDecoBusBoth_d.dds")))
                                    {
                                        fileName = "RoadLargeSegmentDecoBusBoth_d";
                                    }
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
                                plusName = roadHighway + "Ramp" + typeSlope;
                            }
                            else if (netInfo.name.Equals("Highway Barrier"))
                            {
                                plusName = roadHighway + "3L";
                            }
                            else if (netInfo.name.Equals("Highway Elevated")
                                     || segment.m_mesh.name.Equals("HighwayBridgeSegment")
                                     || segment.m_mesh.name.Equals("HighwayBaseSegment")
                                     || segment.m_mesh.name.Equals("HighwayBarrierSegment"))
                            {
                                plusName = roadHighway + "3L";
                            }
                            else if (segment.m_mesh.name.Equals("highway-tunnel-segment")
                                     || segment.m_mesh.name.Equals("highway-tunnel-slope"))
                            {
                                plusName = roadHighway + "3L" + typeSlope;
                            }

                            // NExT
                            if (netInfo.name.Contains("Rural"))
                            {
                                if (netInfo.name.Contains("Small"))
                                {
                                    coreName = "Highway1L_Ground";
                                    plusName = roadHighway + "1L";

                                    if (netInfo.name.Equals("Rural Highway Slope"))
                                    {
                                        coreName = "Highway1L_Slope";
                                        plusName = roadHighway + "1L" + typeSlope;
                                    }
                                    else if (netInfo.name.Equals("Rural Highway Tunnel"))
                                    {
                                        coreName = "Highway1L_Tunnel";
                                        plusName = roadHighway + "1L" + typeTunnel;
                                    }

                                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                    {
                                        segment.SetSegmentDirect(
                                            "Highway1L_Ground_Segment_APRMap.dds",
                                            TexKind.APRMap);
                                    }
                                }
                                else
                                {
                                    coreName = "Highway2L_Ground";
                                    plusName = roadHighway + "2L";

                                    if (netInfo.name.Equals("Rural Highway Slope"))
                                    {
                                        coreName = "Highway2L_Slope";
                                        plusName = roadHighway + "2L" + typeSlope;
                                    }
                                    else if (netInfo.name.Equals("Rural Highway Tunnel"))
                                    {
                                        coreName = "Highway2L_Tunnel";
                                        plusName = roadHighway + "2L" + typeTunnel;
                                    }

                                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                    {
                                        segment.SetSegmentDirect(
                                            "Highway2L_Ground_Segment_APRMap.dds",
                                            TexKind.APRMap);
                                    }
                                }
                            }

                            if (netInfo.name.Contains("Four-Lane"))
                            {
                                coreName = "Highway4L_Ground";
                                plusName = roadHighway + "4L" + typeShitGnd;

                                if (netInfo.name.Contains("Elevated"))
                                {
                                    coreName = "Highway4L_Elevated";
                                    plusName = roadHighway + "4L" + typeElevated;
                                }
                                else if (netInfo.name.Contains("Slope"))
                                {
                                    coreName = "Highway4L_Slope";
                                    plusName = roadHighway + "4L" + typeSlope;
                                }
                                else if (netInfo.name.Contains("Tunnel"))
                                {
                                    coreName = "Highway4L_Tunnel";
                                    plusName = roadHighway + "4L" + typeTunnel;
                                }
                            }

                            if (netInfo.name.Contains("Five-Lane"))
                            {
                                coreName = "Highway5L_Ground";
                                plusName = roadHighway + "5L";

                                if (netInfo.name.Equals("Five-Lane Highway Slope"))
                                {
                                    coreName = "Highway5L_Slope";
                                    plusName = roadHighway + "5L" + typeSlope;
                                }
                                else if (netInfo.name.Equals("Five-Lane Highway Tunnel"))
                                {
                                    coreName = "Highway5L_Tunnel";
                                    plusName = roadHighway + "5L" + typeTunnel;
                                }

                                if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                {
                                    segment.SetSegmentDirect("Highway5L_Ground_Segment_APRMap.dds", TexKind.APRMap);
                                }
                            }

                            if (netInfo.name.Contains("Large"))
                            {
                                coreName = "Highway6L_Ground";
                                plusName = roadHighway + "6L";

                                if (netInfo.name.Contains("Slope"))
                                {
                                    coreName = "Highway6L_Slope";
                                    plusName = roadHighway + "6L" + typeSlope;
                                }
                                else if (netInfo.name.Contains("Tunnel"))
                                {
                                    coreName = "Highway6L_Tunnel";
                                    plusName = roadHighway + "6L" + typeTunnel;
                                }

                                if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                {
                                    segment.SetSegmentDirect("Highway6L_Ground_Segment_APRMap.dds", TexKind.APRMap);
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
                            else if (netInfo.name.Equals("Basic Road Elevated Tram")
                                     || netInfo.name.Equals("Basic Road Bridge Tram"))
                            {
                                plusName = roadSmall + typeElevated;
                            }
                            else if (netInfo.name.Equals("Basic Road Slope Tram"))
                            {
                                plusName = roadSmall + typeSlope;
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
                            else if (netInfo.name.Equals("Oneway Road Elevated Tram")
                                     || netInfo.name.Equals("Oneway Road Bridge Tram"))
                            {
                                plusName = roadSmall + roadOneway + typeElevated;
                            }
                            else if (netInfo.name.Equals("Oneway Road Slope Tram"))
                            {
                                plusName = roadSmall + roadOneway + typeSlope;
                            }
                            else if (netInfo.name.Equals("Medium Road Tram")
                                     && segment.m_mesh.name.Equals("RoadMediumTramSegment"))
                            {
                                plusName = roadMedium + str3 + typeShitGnd;
                            }
                            else if (netInfo.name.Equals("Medium Road Elevated Tram")
                                     || netInfo.name.Equals("Medium Road Bridge Tram"))
                            {
                                plusName = roadMedium + str3 + typeElevated;
                            }
                            else if (netInfo.name.Equals("Medium Road Slope Tram"))
                            {
                                plusName = roadMedium + str3 + typeSlope;
                            }
                            else if (netInfo.name.Equals("Tram Track") || netInfo.name.Equals("Oneway Tram Track"))
                            {
                                plusName = roadSmall + Deco;
                            }
                            else if (netInfo.name.Equals("Tram Track Elevated")
                                     || netInfo.name.Equals("Oneway Tram Track Elevated"))
                            {
                                plusName = roadSmall + roadOneway + typeElevated;
                            }
                            else if (netInfo.name.Equals("Tram Track Slope")
                                     || netInfo.name.Equals("Oneway Tram Track Slope"))
                            {
                                plusName = roadSmall + roadOneway + typeSlope;
                            }
                        }
                        else if (netInfo.name.Contains("Bus"))
                        {
                            if (netInfo.name.Equals("Medium Road Bus"))
                            {
                                if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                                {
                                    plusName = roadMedium + plusBus + typeShitGnd;
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                {
                                    plusName = roadMedium + plusBus + BusSide;
                                    if (File.Exists(Path.Combine(texPath, "RoadMediumBusLane_BusSide.dds")))
                                    {
                                        fileName = "RoadMediumBusLane_BusSide";
                                    }
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                {
                                    plusName = roadMedium + plusBus + BusBoth;
                                    if (File.Exists(Path.Combine(texPath, "RoadMediumBusLane_BusBoth.dds")))
                                    {
                                        fileName = "RoadMediumBusLane_BusBoth";
                                    }
                                }
                            }
                            else if (netInfo.name.Equals("Medium Road Elevated Bus")
                                     || netInfo.name.Equals("Medium Road Bridge Bus"))
                            {
                                plusName = roadMedium + plusBus + typeElevated;
                            }
                            else if (netInfo.name.Equals("Medium Road Slope Bus"))
                            {
                                plusName = roadMedium + plusBus + typeSlope;
                            }
                            else if (netInfo.name.Contains("Large Road Bus"))
                            {
                                if (netInfo.name.Equals("Large Road Bus"))
                                {
                                    if (segment.m_mesh.name.Equals("RoadLargeSegmentBusLane"))
                                    {
                                        plusName = roadLarge + plusBus + typeShitGnd;
                                    }
                                    else if (segment.m_mesh.name.Equals("RoadLargeSegmentBusSideBusLane"))
                                    {
                                        plusName = roadLarge + plusBus + BusSide;
                                        if (File.Exists(Path.Combine(texPath, "RoadLargeBuslane_D_BusSide.dds")))
                                        {
                                            fileName = "RoadLargeBuslane_D_BusSide";
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBothBusLane"))
                                    {
                                        plusName = roadLarge + plusBus + BusBoth;
                                        if (File.Exists(Path.Combine(texPath, "RoadLargeBuslane_D_BusBoth.dds")))
                                        {
                                            fileName = "RoadLargeBuslane_D_BusBoth";
                                        }
                                    }
                                }
                                else if (netInfo.name.Equals("Large Road Elevated Bus")
                                         || netInfo.name.Equals("Large Road Bridge Bus"))
                                {
                                    plusName = roadLarge + plusBus + typeElevated;
                                }
                                else if (netInfo.name.Equals("Large Road Slope Bus"))
                                {
                                    plusName = roadLarge + plusBus + typeSlope;
                                }
                            }
                        }
                        else if (netInfo.name.Equals("Two-Lane Alley"))
                        {
                            coreName = "Alley2L_Ground";
                            plusPath = pathTiny;
                            plusName = roadTiny + "2L";

                            if (!SetSegmentTextures(segment, coreName, plusPath, plusName))
                            {
                                coreName = "OneWay1L_Ground";
                                plusName = roadTiny + "1L";
                            }
                        }
                        else if (netInfo.name.Equals("One-Lane Oneway"))
                        {
                            coreName = "OneWay1L_Ground";
                            plusPath = pathTiny;
                            plusName = roadTiny + "1L";
                        }
                        else if (netInfo.name.Equals("BasicRoadTL"))
                        {
                            plusPath = pathSmall;
                            if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Ground"))
                            {
                                coreName = "BasicRoadTL_Ground";
                                plusName = roadSmall + "TL" + typeShitGnd;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Elevated"))
                            {
                                coreName = "BasicRoadTL_Elevated";
                                plusName = roadSmall + "TL" + typeElevated;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Tunnel"))
                            {
                                coreName = "BasicRoadTL_Tunnel";
                                plusName = roadSmall + "TL" + typeTunnel;
                            }
                        }
                        else if (netInfo.name.Equals("Small Avenue"))
                        {
                            plusPath = pathSmall;
                            if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Ground"))
                            {
                                coreName = "SmallAvenue4L_Ground";
                                plusName = roadSmall + "4L" + typeShitGnd;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                            {
                                coreName = "SmallAvenue4L_Elevated";
                                plusName = roadSmall + "4L" + typeElevated;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                            {
                                coreName = "SmallAvenue4L_Tunnel";
                                plusName = roadSmall + "4L" + typeTunnel;
                            }
                        }
                        else if (netInfo.name.Equals("Oneway3L"))
                        {
                            if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                            {
                                coreName = "OneWay3L_Ground";
                                plusPath = pathSmall;
                                plusName = roadSmall + roadOneway + "3L" + typeShitGnd;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                            {
                                coreName = "OneWay3L_Elevated";
                                plusPath = pathSmall;
                                plusName = roadSmall + roadOneway + "3L" + typeElevated;
                            }
                            else if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                            {
                                coreName = "OneWay3L_Tunnel";
                                plusPath = pathSmall;
                                plusName = roadSmall + roadOneway + "3L" + typeTunnel;
                            }
                        }
                        else if (netInfo.name.Equals("Oneway4L"))
                        {
                            if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                            {
                                coreName = "OneWay4L_Ground";
                                plusPath = pathSmall;
                                plusName = roadSmall + roadOneway + "4L" + typeShitGnd;
                            }

                            if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                            {
                                coreName = "OneWay4L_Elevated";
                                plusPath = pathSmall;
                                plusName = roadSmall + roadOneway + "4L" + typeElevated;
                            }

                            if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Tunnel"))
                            {
                                coreName = "OneWay4L_Tunnel";
                                plusPath = pathSmall;
                                plusName = roadSmall + roadOneway + "4L" + typeTunnel;
                            }
                        }
                        else if (netInfo.name.Equals("AsymRoadL1R2"))
                        {
                            plusPath = pathSmall;
                            plusName = roadSmall + str4 + typeShitGnd;

                            if (netInfo.name.Equals("AsymRoadL1R2 Elevated")
                                || netInfo.name.Equals("AsymRoadL1R2 Bridge"))
                            {
                                plusName = roadSmall + str4 + typeElevated;
                            }
                            else if (netInfo.name.Equals("AsymRoadL1R2 Slope") && segment.m_mesh.name == "Slope")
                            {
                                plusName = roadSmall + str4 + typeShitGnd;
                            }
                            else if (netInfo.name.Equals("AsymRoadL1R2 Tunnel"))
                            {
                                plusName = roadSmall + str4 + typeTunnel;
                            }

                            if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Inverted"))
                            {
                                plusName += "_Inv";
                            }
                        }
                        else if (netInfo.name.Equals("AsymRoadL1R3"))
                        {
                            plusPath = pathSmall;
                            plusName = roadSmall + str5 + typeShitGnd;

                            if (netInfo.name.Equals("AsymRoadL1R3 Elevated")
                                || netInfo.name.Equals("AsymRoadL1R3 Bridge"))
                            {
                                plusName = roadSmall + str5 + typeElevated;
                            }
                            else if (netInfo.name.Equals("AsymRoadL1R3 Slope") && segment.m_mesh.name == "Slope")
                            {
                                plusName = roadSmall + str5 + typeShitGnd;
                            }
                            else if (netInfo.name.Equals("AsymRoadL1R3 Tunnel"))
                            {
                                plusName = roadSmall + str4 + typeTunnel;
                            }

                            if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Inverted"))
                            {
                                plusName += "_Inv";
                            }
                        }
                        else if (netInfo.name.Equals("Medium Avenue"))
                        {
                            plusPath = pathMedium;
                            if (!netInfo.name.Contains("TL"))
                            {
                                if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Ground"))
                                {
                                    coreName = "MediumAvenue4L_Ground";
                                    plusName = roadMedium + "4L" + typeShitGnd;
                                }
                                else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Elevated"))
                                {
                                    coreName = "MediumAvenue4L_Elevated";
                                    plusName = roadMedium + "4L" + typeElevated;
                                }
                                else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Slope"))
                                {
                                    coreName = "MediumAvenue4L_Slope";
                                    plusName = roadMedium + "4L" + typeSlope;
                                }
                                else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Tunnel"))
                                {
                                    coreName = "MediumAvenue4L_Tunnel";
                                    plusName = roadMedium + "4L" + typeTunnel;
                                }
                            }
                            else
                            {
                                // Turning lane
                                if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Ground"))
                                {
                                    coreName = "MediumAvenue4LTL_Ground";
                                    plusName = roadMedium + "4LTL" + typeShitGnd;

                                    // Alternate APR textures
                                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                    {
                                        segment.SetSegmentDirect("RoadLargeSegment-default-apr.dds", TexKind.APRMap);
                                    }
                                }
                                else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Elevated"))
                                {
                                    coreName = "MediumAvenue4LTL_Elevated";
                                    plusName = roadMedium + "4LTL" + typeElevated;
                                }
                                else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Slope"))
                                {
                                    coreName = "MediumAvenue4LTL_Slope";
                                    plusName = roadMedium + "4LTL" + typeSlope;
                                }
                                else if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Tunnel"))
                                {
                                    coreName = "MediumAvenue4LTL_Tunnel";
                                    plusName = roadMedium + "4LTL" + typeTunnel;
                                }
                            }
                        }
                        else if (netInfo.name.Equals("Eight-Lane Avenue"))
                        {
                            plusPath = pathLarge;
                            coreName = "LargeAvenue8LM_Ground";
                            plusName = roadLarge + "8L" + typeShitGnd;

                            if (netInfo.name.Equals("Eight-Lane Avenue Elevated")
                                || netInfo.name.ToLower().Contains("bridge"))
                            {
                                coreName = "LargeAvenue8LM_Elevated";
                                plusName = roadLarge + "8L" + typeElevated;
                            }
                            else if (netInfo.name.Equals("Eight-Lane Avenue Slope"))
                            {
                                coreName = "LargeAvenue8LM_Slope";
                                plusName = roadLarge + "8L" + typeSlope;
                            }
                            else if (netInfo.name.Equals("Eight-Lane Avenue Tunnel"))
                            {
                                coreName = "LargeAvenue8LM_Tunnel";
                                plusName = roadLarge + "8L" + typeTunnel;
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
                                        coreName = "Busway2L1W_DecoGrass_Ground";
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
                                    coreName = "Busway2L1W_Ground";
                                    plusName = roadSmall + roadOneway + plusBus + typeShitGnd;
                                }
                                else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                {
                                    plusName = roadSmall + roadOneway + plusBus + BusSide;
                                }
                                else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                                {
                                    // todo: texture missing?
                                    coreName = "Oneway_RoadSmallSegment_BusBoth";
                                    plusName = roadSmall + roadOneway + plusBus + BusBoth;
                                }
                                else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                                {
                                    if (netInfo.name.Contains("Elevated"))
                                    {
                                        coreName = "Busway2L1W_Elevated";
                                        plusName = roadSmall + roadOneway + plusBus + typeElevated;
                                    }
                                    else if (netInfo.name.Contains("Slope"))
                                    {
                                        coreName = "Busway2L1W_Slope";
                                        plusName = roadSmall + roadOneway + plusBus + typeSlope;
                                    }
                                    else if (netInfo.name.Contains("Tunnel"))
                                    {
                                        plusName = roadSmall + roadOneway + plusBus + typeTunnel;
                                    }
                                }
                            }
                            else
                            {
                                coreName = "Busway2L_Ground";
                                plusName = roadSmall + plusBus + typeShitGnd;

                                if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                                {
                                    if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                    {
                                        coreName = null;
                                        plusName = roadSmall + plusBus + BusSide;
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                                    {
                                        coreName = null;
                                        plusName = roadSmall + plusBus + BusBoth;
                                    }
                                    else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                                    {
                                        if (netInfo.name.Equals("Small Busway Elevated"))
                                        {
                                            coreName = "Busway2L_Elevated";
                                            plusName = roadSmall + plusBus + typeElevated;
                                        }
                                        else if (netInfo.name.Equals("Small Busway Slope"))
                                        {
                                            coreName = "Busway2L_Slope";
                                            plusName = roadSmall + plusBus + typeSlope;
                                        }
                                        else if (netInfo.name.Equals("Small Busway Tunnel"))
                                        {
                                            coreName = null;
                                            plusName = roadSmall + plusBus + typeTunnel;
                                        }
                                    }
                                }
                            }

                            if (netInfo.name.Equals("Small Busway Decoration Grass")
                                || netInfo.name.Equals("Small Busway Decoration Trees"))
                            {
                                plusPath = pathSmall;
                                coreName = "Busway2L_DecoGrass";
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
                            coreName = "RoadLargeBuslane_D";

                            if (segment.m_mesh.name.Equals("RoadLageSegment"))
                            {
                                // todo: exceptions for original names
                                plusName = roadLarge + plusBus + typeShitGnd;
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
                                coreName = "RoadLargeElevatedBus_D";
                                plusName = roadLarge + plusBus + typeElevated;
                            }

                            if (netInfo.name.Contains("Slope"))
                            {
                                coreName = "large-tunnelBus_d";
                                plusName = roadLarge + plusBus + typeSlope;
                            }

                            if (netInfo.name.Contains("Tunnel"))
                            {
                                plusName = roadLarge + plusBus + typeTunnel;
                            }

                            if (netInfo.name.Contains("Trees") || netInfo.name.Contains("Grass"))
                            {
                                coreName = "Busway6L_DecoGrass";
                                if (segment.m_mesh.name.Equals("LargeRoadSegment2"))
                                {
                                    plusName = roadLarge + plusBus + Deco;
                                    if (File.Exists(
                                        Path.Combine(texPath, "Busway6L_DecoGrass_Ground_Segment_MainTex.dds")))
                                    {
                                        segment.SetSegmentDirect("RoadLargeSegment-default-apr.dds", TexKind.APRMap);
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

                        if (ModLoader.config.basic_road_parking == 1)
                        {
                            plusName = roadSmall + typeShitGnd;
                            if (fileName.Equals("RoadSmall_D"))
                            {
                                fileName = "RoadSmall_D_parking1";
                            }

                            if (fileName.Equals("RoadSmall_D_BusSide"))
                            {
                                fileName = "RoadSmall_D_BusSide_parking1";
                            }
                        }

                        if (ModLoader.config.medium_road_parking == 1 && !netInfo.name.Contains("Grass")
                            && !netInfo.name.Contains("Trees"))
                        {
                            if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                            {
                                if (fileName.Equals("RoadMediumSegment_d")
                                    && File.Exists(Path.Combine(texPath, "RoadMedium_D_BusSide_parking1.dds")))
                                {
                                    fileName = "RoadMedium_D_BusSide_parking1";
                                }
                            }
                            else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                            {
                                if (fileName.Equals("RoadMediumSegment_d")
                                    && File.Exists(Path.Combine(texPath, "RoadMedium_D_BusBoth_parking1.dds")))
                                {
                                    fileName = "RoadMedium_D_BusBoth_parking1";
                                }
                            }
                            else if (fileName.Equals("RoadMediumSegment_d")
                                     && File.Exists(Path.Combine(texPath, "RoadMedium_D_parking1.dds")))
                            {
                                fileName = "RoadMedium_D_parking1";
                            }
                            else if (fileName.Equals("RoadMedium_D")
                                     && File.Exists(Path.Combine(texPath, "RoadMedium_D_parking1.dds")))
                            {
                                fileName = "RoadMedium_D_parking1";
                            }
                        }

                        if (ModLoader.config.medium_road_grass_parking == 1 && netInfo.name.Contains("Grass"))
                        {
                            if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                            {
                                if (fileName.Equals("RoadMedium_D")
                                    && File.Exists(Path.Combine(texPath, "RoadMedium_D_BusSide_parking1.dds")))
                                {
                                    fileName = "RoadMedium_D_BusSide_parking1";
                                }
                            }
                            else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                            {
                                if (fileName.Equals("RoadMedium_D")
                                    && File.Exists(Path.Combine(texPath, "RoadMedium_D_BusBoth_parking1.dds")))
                                {
                                    fileName = "RoadMedium_D_BusBoth_parking1";
                                }
                            }
                            else if (fileName.Equals("RoadMediumSegment_d")
                                     && File.Exists(Path.Combine(texPath, "RoadMedium_D_parking1.dds")))
                            {
                                fileName = "RoadMedium_D_parking1";
                            }
                            else if (fileName.Equals("RoadMedium_D")
                                     && File.Exists(Path.Combine(texPath, "RoadMedium_D_parking1.dds")))
                            {
                                fileName = "RoadMedium_D_parking1";
                            }
                        }

                        if (ModLoader.config.medium_road_trees_parking == 1 && netInfo.name.Contains("Trees"))
                        {
                            if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                            {
                                if (fileName.Equals("RoadMediumDeco_d")
                                    && File.Exists(Path.Combine(texPath, "RoadMediumDeco_d_BusSide_parking1.dds")))
                                {
                                    fileName = "RoadMediumDeco_d_BusSide_parking1";
                                }
                            }
                            else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth")
                                     && File.Exists(Path.Combine(texPath, "RoadMediumDeco_d.dds")))
                            {
                                fileName = "RoadMediumDeco_d";
                            }
                            else if (fileName.Equals("RoadMediumDeco_d")
                                     && File.Exists(Path.Combine(texPath, "RoadMediumDeco_d_parking1.dds")))
                            {
                                fileName = "RoadMediumDeco_d_parking1";
                            }
                        }

                        if (ModLoader.config.medium_road_bus_parking == 1)
                        {
                            if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                            {
                                if (fileName.Equals("RoadMediumBusLane")
                                    && File.Exists(Path.Combine(texPath, "RoadMediumBusLane_BusSide_parking1.dds")))
                                {
                                    fileName = "RoadMediumBusLane_BusSide_parking1";
                                }
                            }
                            else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                            {
                                if (fileName.Equals("RoadMediumBusLane")
                                    && File.Exists(Path.Combine(texPath, "RoadMediumBusLane_BusBoth_parking1.dds")))
                                {
                                    fileName = "RoadMediumBusLane_BusBoth_parking1";
                                }
                            }
                            else if (fileName.Equals("RoadMediumBusLane")
                                     && File.Exists(Path.Combine(texPath, "RoadMediumBusLane_parking1.dds")))
                            {
                                fileName = "RoadMediumBusLane_parking1";
                            }
                        }

                        if (ModLoader.config.large_road_parking == 1)
                        {
                            if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                            {
                                if (fileName.Equals("RoadLargeSegment_d")
                                    && File.Exists(Path.Combine(texPath, "RoadLargeSegment_d_BusSide_parking1.dds")))
                                {
                                    fileName = "RoadLargeSegment_d_BusSide_parking1";
                                }
                            }
                            else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                            {
                                if (fileName.Equals("RoadLargeSegment_d")
                                    && File.Exists(Path.Combine(texPath, "RoadLargeSegment_d_BusBoth_parking1.dds")))
                                {
                                    fileName = "RoadLargeSegment_d_BusBoth_parking1";
                                }
                            }
                            else if (fileName.Equals("RoadLargeSegment_d")
                                     && File.Exists(Path.Combine(texPath, "RoadLargeSegment_d_parking1.dds")))
                            {
                                fileName = "RoadLargeSegment_d_parking1";
                            }
                        }

                        if (ModLoader.config.large_oneway_parking == 1)
                        {
                            if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                            {
                                if (fileName.Equals("RoadLargeOnewaySegment_d")
                                    && File.Exists(
                                        Path.Combine(texPath, "RoadLargeOnewaySegment_d_BusSide_parking1.dds")))
                                {
                                    fileName = "RoadLargeOnewaySegment_d_BusSide_parking1";
                                }
                            }
                            else if (fileName.Equals("RoadLargeOnewaySegment_d")
                                     && File.Exists(Path.Combine(texPath, "RoadLargeOnewaySegment_d_parking1.dds")))
                            {
                                fileName = "RoadLargeOnewaySegment_d_parking1";
                            }
                        }

                        if (ModLoader.config.large_road_bus_parking == 1)
                        {
                            if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSideBusLane"))
                            {
                                if (fileName.Equals("RoadLargeBuslane_D")
                                    && File.Exists(Path.Combine(texPath, "RoadLargeBuslane_D_BusSide_parking1.dds")))
                                {
                                    fileName = "RoadLargeBuslane_D_BusSide_parking1";
                                }
                            }
                            else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBothBusLane"))
                            {
                                if (fileName.Equals("RoadLargeBuslane_D")
                                    && File.Exists(Path.Combine(texPath, "RoadLargeBuslane_D_BusBoth_parking1.dds")))
                                {
                                    fileName = "RoadLargeBuslane_D_BusBoth_parking1";
                                }
                            }
                            else if (fileName.Equals("RoadLargeBuslane_D")
                                     && File.Exists(Path.Combine(texPath, "RoadLargeBuslane_D_parking1.dds")))
                            {
                                fileName = "RoadLargeBuslane_D_parking1";
                            }
                        }

                        fileName += ext_DDS;
                        coreName += ext_DDS;

                        if (!segment.CheckAndSetSegmentMaterial(texPath, fileName))
                        {
                            SetSegmentTextures(segment, coreName, plusPath, plusName);
                        }

                        log += "\n" + segment + " - " + coreName + " | " + plusPath + "/" + plusName;

                        if (segment.m_segmentMaterial.GetTexture(aprmap) == null)
                        {
                            continue;
                        }

                        string aprPath = Path.Combine(
                            texPath,
                            segment.m_segmentMaterial.GetTexture(aprmap).name + ext_DDS);

                        if (segment.m_segmentMaterial.GetTexture(aprmap).name
                                .Equals("LargeRoadSegmentBusSide-BikeLane-apr") || segment.m_segmentMaterial
                                .GetTexture(aprmap).name.Equals("LargeRoadSegmentBusBoth-BikeLane-apr"))
                        {
                            if (File.Exists(Path.Combine(texPath, "RoadLargeSegment-BikeLane-apr.dds")))
                            {
                                aprPath = Path.Combine(texPath, "RoadLargeSegment-BikeLane-apr.dds");
                            }
                            else if (File.Exists(
                                Path.Combine(ModLoader.APRMaps_Path, "RoadLargeSegment-BikeLane-apr.dds")))
                            {
                                aprPath = Path.Combine(ModLoader.APRMaps_Path, "RoadLargeSegment-BikeLane-apr.dds");
                            }
                        }

                        if (segment.m_segmentMaterial.GetTexture(aprmap).name
                                .Equals("LargeRoadSegmentBusSide-LargeRoadSegmentBusSide-apr") || segment
                                .m_segmentMaterial.GetTexture(aprmap).name
                                .Equals("LargeRoadSegmentBusBoth-LargeRoadSegmentBusBoth-apr"))
                        {
                            if (File.Exists(Path.Combine(texPath, "RoadLargeSegment-default-apr.dds")))
                            {
                                aprPath = Path.Combine(texPath, "RoadLargeSegment-default-apr.dds");
                            }
                            else if (File.Exists(
                                Path.Combine(ModLoader.APRMaps_Path, "RoadLargeSegment-default-apr.dds")))
                            {
                                aprPath = Path.Combine(ModLoader.APRMaps_Path, "RoadLargeSegment-default-apr.dds");
                            }
                        }

                        if (File.Exists(aprPath))
                        {
                            segment.m_segmentMaterial.SetTexture(aprmap, aprPath.LoadTextureDDS());
                        }
                        else
                        {
                            aprPath = Path.Combine(
                                ModLoader.currentTexturesPath_default,
                                segment.m_segmentMaterial.GetTexture(aprmap).name + ext_DDS);

                            if (File.Exists(aprPath))
                            {
                                segment.m_segmentMaterial.SetTexture(aprmap, aprPath.LoadTextureDDS());
                            }
                        }
                    }
                }

                num += 1u;

                // Debug.Log("RU Core: " + log);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static bool CheckAndSetNodeMaterial(NetInfo.Node node, [CanBeNull] string path, [CanBeNull] string name)
        {
            string log = node + " - " + path + " - " + name;
            bool main = false;
            if (path.IsNullOrWhiteSpace() || name.IsNullOrWhiteSpace())
            {
                return false;
            }

            System.Diagnostics.Debug.Assert(path != null, "path != null");
            if (File.Exists(Path.Combine(path, name)))
            {
                log += "\nMainTex found at: " + Path.Combine(path, name);
                node.m_nodeMaterial.SetTexture(maintex, Path.Combine(path, name).LoadTextureDDS());
                main = true;
            }
            else if (File.Exists(Path.Combine(path, name + nodeSuffix + mainTexSuffix)))
            {
                log += "\nMainTex found at: " + Path.Combine(path, name + nodeSuffix + mainTexSuffix);
                node.m_nodeMaterial.SetTexture(
                    maintex,
                    Path.Combine(path, name + nodeSuffix + mainTexSuffix).LoadTextureDDS());
                main = true;
            }
            else
            {
                log += "\nNo MainTex found at: " + Path.Combine(path, name + nodeSuffix + mainTexSuffix);
            }

            if (main)
            {
                string aprPath = name + nodeSuffix + aprMapSuffix;
                if (node.m_nodeMaterial.GetTexture(aprmap) != null)
                {
                    string aprFileName = node.m_nodeMaterial.GetTexture(aprmap).name + ext_DDS;
                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, aprFileName)))
                    {
                        node.m_nodeMaterial.SetTexture(
                            aprmap,
                            Path.Combine(ModLoader.currentTexturesPath_default, aprFileName).LoadTextureDDS());
                    }
                    else if (File.Exists(Path.Combine(path, aprPath)))
                    {
                        log += "\nAPRMap found at: " + (path + " - " + aprPath);
                        node.m_nodeMaterial.SetTexture(
                            aprmap,
                            Path.Combine(path, name + nodeSuffix + aprMapSuffix).LoadTextureDDS());
                    }
                    else if (File.Exists(Path.Combine(ModLoader.APRMaps_Path, aprFileName)))
                    {
                        node.m_nodeMaterial.SetTexture(
                            aprmap,
                            Path.Combine(ModLoader.APRMaps_Path, aprFileName).LoadTextureDDS());
                    }
                    else if (File.Exists(Path.Combine(ModLoader.APRMaps_Path, aprPath)))
                    {
                        log += "\nUsing main mod APR map: " + ModLoader.APRMaps_Path + " - " + aprPath;
                        node.m_nodeMaterial.SetTexture(
                            aprmap,
                            Path.Combine(ModLoader.APRMaps_Path, aprPath).LoadTextureDDS());
                    }
                    else
                    {
                        log += "\nNo texture found at: " + Path.Combine(path, aprPath) + " or "
                               + Path.Combine(ModLoader.APRMaps_Path, aprPath);
                    }
                }
            }

            // Debug.Log(log);
            return main;
        }

        private static void SetNodeTextures(
            NetInfo.Node node,
            [CanBeNull] string coreName,
            string plusPath,
            string plusName)
        {
            if (!CheckAndSetNodeMaterial(node, ModLoader.currentTexturesPath_default, coreName))
            {
                CheckAndSetNodeMaterial(node, plusPath, plusName);
            }
        }

        private static void SetPlusNodes(
            [NotNull] NetInfo netInfo,
            string pathSmall,
            string pathMedium,
            string pathLarge,
            string pathHighway,
            out string plusPath,
            out string plusName)
        {
            plusPath = pathSmall;
            plusName = null;

            if (netInfo.name.Contains("Basic Road"))
            {
                if (netInfo.name.Equals("Basic Road Elevated"))
                {
                    plusName = roadSmall + typeElevated;
                }
                else if (netInfo.name.Equals("Basic Road Decoration Grass")
                         || netInfo.name.Equals("Basic Road Decoration Trees"))
                {
                    plusName = roadSmall + Deco;
                }
                else
                {
                    plusName = roadSmall + typeShitGnd;
                }
            }
            else if (netInfo.name.Contains("Oneway Road"))
            {
                plusName = roadSmall + roadOneway;
                if (netInfo.name.Equals("Oneway Road Elevated"))
                {
                    plusName = plusName + typeElevated;
                }
                else if (netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees"))
                {
                    plusName = roadSmall + roadOneway + Deco;
                }
                else
                {
                    plusName = plusName + typeShitGnd;
                }
            }
            else if (netInfo.name.Equals("Basic Road Bicycle"))
            {
                plusName = roadSmall + typeShitGnd;
            }
            else if (netInfo.name.Equals("Basic Road Elevated Bike"))
            {
                plusName = roadSmall + Bike + typeElevated;
            }
            else if (netInfo.name.Contains("Medium Road") && !netInfo.name.Contains("Tram"))
            {
                plusPath = pathMedium;
                plusName = roadMedium;
                if (netInfo.name.Contains("Medium Road Elevated") && !netInfo.name.Contains("Bike"))
                {
                    plusName = plusName + typeElevated;
                }
                else if (netInfo.name.Equals("Medium Road Decoration Grass")
                         || netInfo.name.Equals("Medium Road Decoration Trees"))
                {
                    plusName = roadMedium + Deco;
                }
                else
                {
                    plusName = plusName + typeShitGnd;
                }
            }
            else if (netInfo.name.Equals("Medium Road Elevated Bike"))
            {
                plusPath = pathMedium;
                plusName = roadMedium + Bike + typeElevated;
            }
            else if (netInfo.name.Contains("Large Road"))
            {
                plusPath = pathLarge;
                plusName = roadLarge;
                if (netInfo.name.Equals("Large Road Elevated"))
                {
                    plusName = plusName + typeElevated;
                }
                else if (netInfo.name.Equals("Large Road Decoration Grass")
                         || netInfo.name.Equals("Large Road Decoration Trees"))
                {
                    plusName = roadLarge + Deco;
                }
                else
                {
                    plusName = plusName + typeShitGnd;
                }
            }
            else if (netInfo.name.Equals("Large Road Elevated Bike"))
            {
                plusPath = pathLarge;
                plusName = roadLarge + Bike + typeElevated;
            }
            else if (netInfo.name.Contains("Large Oneway"))
            {
                plusPath = pathLarge;
                plusName = roadLarge + roadOneway;
                if (netInfo.name.Equals("Large Oneway Elevated"))
                {
                    plusName = plusName + typeElevated;
                }
                else if (netInfo.name.Equals("Large Oneway Decoration Grass")
                         || netInfo.name.Equals("Large Oneway Decoration Trees"))
                {
                    plusName = roadLarge + roadOneway + Deco;
                }
                else if (netInfo.name.Equals("Large Oneway Road Slope"))
                {
                    plusName = roadLarge + roadOneway + typeSlope;
                }
                else
                {
                    plusName = plusName + typeShitGnd;
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
                plusName = roadSmall + typeShitGnd;
            }
            else if (netInfo.name.Equals("Basic Road Elevated Tram"))
            {
                plusName = roadSmall + typeElevated;
            }
            else if (netInfo.name.Equals("Basic Road Slope Tram"))
            {
                plusName = roadSmall + typeShitGnd;
            }
            else if (netInfo.name.Equals("Oneway Road Tram"))
            {
                plusName = roadSmall + roadOneway + typeShitGnd;
            }
            else if (netInfo.name.Equals("Oneway Road Elevated Tram"))
            {
                plusName = roadSmall + roadOneway + typeElevated;
            }
            else if (netInfo.name.Equals("Oneway Road Slope Tram"))
            {
                plusName = roadSmall + roadOneway + typeShitGnd;
            }
            else if (netInfo.name.Equals("Medium Road Tram"))
            {
                plusPath = pathMedium;
                plusName = roadMedium + str3 + typeShitGnd;
            }
            else if (netInfo.name.Equals("Medium Road Elevated Tram"))
            {
                plusPath = pathMedium;
                plusName = roadMedium + str3 + typeElevated;
            }
            else if (netInfo.name.Equals("Medium Road Slope Tram"))
            {
                plusPath = pathMedium;
                plusName = roadMedium + str3 + typeShitGnd;
            }
            else if (netInfo.name.Equals("Tram Track"))
            {
                plusName = roadSmall + Deco;
            }
            else if (netInfo.name.Equals("Tram Track Elevated"))
            {
                plusName = roadSmall + typeElevated;
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
                plusName = roadSmall + typeElevated;
            }
            else if (netInfo.name.Equals("Oneway Tram Track Slope"))
            {
                plusName = roadSmall + Deco;
            }
        }

        private static bool SetSegmentTextures(
            NetInfo.Segment segment,
            [CanBeNull] string coreName,
            string plusPath,
            string plusName)
        {
            bool replaced = false;
            if (!segment.CheckAndSetSegmentMaterial(ModLoader.currentTexturesPath_default, coreName))
            {
                if (segment.CheckAndSetSegmentMaterial(plusPath, plusName))
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