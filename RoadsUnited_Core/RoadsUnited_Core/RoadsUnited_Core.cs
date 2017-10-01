namespace RoadsUnited_Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using ColossalFramework;
    using UnityEngine;

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
        private static Dictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D>();

        public static readonly string ExtDDS = ".dds";

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
                        node.m_nodeMaterial.SetTexture(TexType._MainTex, defaultmapTex);
                    }

                    if (vanillaPrefabProperties.TryGetValue(
                        string.Concat(text, "_node_", i, "_nodeMaterial_APRMap"),
                        out aprmapTex))
                    {
                        node.m_nodeMaterial.SetTexture(TexType._APRMap, aprmapTex);
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
                            segment.m_segmentMaterial.SetTexture(TexType._MainTex, defaultmapTex);
                        }

                        if (vanillaPrefabProperties.TryGetValue(
                            string.Concat(text, "_segment_", j, "_segmentMaterial_APRMap"),
                            out aprmapTex))
                        {
                            segment.m_segmentMaterial.SetTexture(TexType._APRMap, aprmapTex);
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
                        if (propInfo.m_lodMaterialCombined.GetTexture(TexType._MainTex) == null)
                        {
                            continue;
                        }

                        if (!name.Contains("Arrow"))
                        {
                            continue;
                        }

                        if (vanillaPrefabProperties.TryGetValue(name + "_prop_MainTex", out defaultmapTex))
                        {
                            propInfo.m_lodMaterialCombined.SetTexture(TexType._MainTex, defaultmapTex);
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

                    if (node.m_nodeMaterial.GetTexture(TexType._MainTex) == null)
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
                        node.m_nodeMaterial.SetTexture(TexType._MainTex, defaultmapTex);
                    }

                    if (vanillaPrefabProperties.TryGetValue(
                        string.Concat(text, "_node_", i, "_nodeMaterial_APRMap"),
                        out aprmapTex))
                    {
                        node.m_nodeMaterial.SetTexture(TexType._APRMap, aprmapTex);
                    }
                }

                NetInfo.Segment[] segments = loaded.m_segments;
                for (int j = 0; j < segments.Length; j++)
                {
                    NetInfo.Segment segment = segments[j];
                    if (segment.m_segmentMaterial.GetTexture(TexType._MainTex) == null)
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
                        segment.m_segmentMaterial.SetTexture(TexType._MainTex, defaultmapTex);
                    }

                    if (vanillaPrefabProperties.TryGetValue(
                        string.Concat(text, "_segment_", j, "_segmentMaterial_APRMap"),
                        out aprmapTex))
                    {
                        segment.m_segmentMaterial.SetTexture(TexType._APRMap, aprmapTex);
                    }
                }

                num += 1u;
            }
        }

        public static void CreateVanillaDictionary()
        {
            if (vanillaPrefabProperties.Any())
            {
                return;
            }

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
                        if (node.m_nodeMaterial.GetTexture(TexType._MainTex) == null)
                        {
                            continue;
                        }

                        if (blacklist.Any(x => className.Contains(x)))
                        {
                            continue;
                        }

                        vanillaPrefabProperties.Add(
                            string.Concat(collectionName, "_node_", i, "_nodeMaterial_MainTex"),
                            node.m_nodeMaterial.GetTexture(TexType._MainTex) as Texture2D);
                        vanillaPrefabProperties.Add(
                            string.Concat(collectionName, "_node_", i, "_nodeMaterial_APRMap"),
                            node.m_nodeMaterial.GetTexture(TexType._APRMap) as Texture2D);
                        log += "\n" + string.Concat(collectionName, "_node_", i, "_nodeMaterial_MainTex");
                    }
                }

                NetInfo.Segment[] segments = loaded.m_segments;
                for (int j = 0; j < segments.Length; j++)
                {
                    NetInfo.Segment segment = segments[j];
                    {
                        if (segment.m_segmentMaterial.GetTexture(TexType._MainTex) == null)
                        {
                            continue;
                        }

                        if (blacklist.Any(x => className.Contains(x)))
                        {
                            continue;
                        }

                        vanillaPrefabProperties.Add(
                            string.Concat(collectionName, "_segment_", j, "_segmentMaterial_MainTex"),
                            segment.m_segmentMaterial.GetTexture(TexType._MainTex) as Texture2D);
                        vanillaPrefabProperties.Add(
                            string.Concat(collectionName, "_segment_", j, "_segmentMaterial_APRMap"),
                            segment.m_segmentMaterial.GetTexture(TexType._APRMap) as Texture2D);
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
                        if (propInfo.m_lodMaterialCombined.GetTexture(TexType._MainTex) == null)
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
                            propInfo.m_lodMaterialCombined.GetTexture(TexType._MainTex) as Texture2D);
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

        // deactivated for now
        public static Texture2D LoadTexture(string fullPath)
        {
            Texture2D texture2D = new Texture2D(1, 1);
            if (textureCache.TryGetValue(fullPath, out texture2D))
            {
                return texture2D;
            }

            texture2D.LoadImage(File.ReadAllBytes(fullPath));
            texture2D.name = Path.GetFileName(fullPath);
            texture2D.anisoLevel = 8;
            texture2D.Compress(true);
            return texture2D;
        }

        public static Texture2D LoadTextureDDS(string fullPath)
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
            texture = new Texture2D(width, height, TextureFormat.DXT5, true);
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

        public static void ReplaceNetTextures()
        {
            if (!ModLoader.config.texturePackPath.IsNullOrWhiteSpace())
            {
                ModLoader.currentTexturesPath_default = Path.Combine(ModLoader.config.texturePackPath, "BaseTextures");
            }

            List<SegmentSet> segList = new List<SegmentSet>();
            List<NodeSet> nodeList = new List<NodeSet>();
            string log = "RU Core replacing: ";
            string nextLog = "Next Replacements: ";
            string allNodes = "All nodes:";
            string allSegments = "All segments:";

            for (uint i = 0; i < PrefabCollection<NetInfo>.LoadedCount(); i++)
            {
                NetInfo netInfo = PrefabCollection<NetInfo>.GetLoaded(i);
                if (netInfo == null)
                {
                    continue;
                }

                allNodes += "\nNExt nodes: ";
                allSegments += "\nNExt segments: ";
                string className = netInfo.m_class.name;

                NetInfo.Node[] nodes = netInfo.m_nodes;
                foreach (NetInfo.Node node in nodes.Where(node => !blacklist.Any(x => className.Contains(x)))
                    .Where(
                        node => node.m_nodeMaterial.GetTexture(TexType._MainTex) != null
                                && node.m_nodeMaterial.GetTexture(TexType._APRMap) != null))
                {
                    // Just look up the name and set the textures accordingly, no magic needed
                    string nodeMatName = node.m_nodeMaterial.GetTexture(TexType._MainTex).name;
                    string nodeAprName = node.m_nodeMaterial.GetTexture(TexType._APRMap).name;
                    {
                        allNodes += "\n" + className + " | " + netInfo.name + " | " + nodeMatName + " | "
                                    + node.m_nodeMesh.name;
                        allNodes += "\n" + nodeAprName;

                        #region NExt nodes

                        ReplaceNExtNodes(netInfo, node, ref nextLog);
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

                        #endregion

                        nodeList.Add(new NodeSet(node, nodeMatName, nodeAprName));
                    }
                }

                // Look for segments
                NetInfo.Segment[] segments = netInfo.m_segments;
                foreach (NetInfo.Segment segment in segments
                    .Where(segment => !blacklist.Any(x => className.Contains(x))).Where(
                        segment => segment.m_segmentMaterial.GetTexture(TexType._MainTex) != null
                                   && segment.m_segmentMaterial.GetTexture(TexType._APRMap) != null))
                {
                    string mainTexName = segment.m_segmentMaterial.GetTexture(TexType._MainTex).name;
                    string aprName = segment.m_segmentMaterial.GetTexture(TexType._APRMap).name;

                    segList.Add(new SegmentSet(segment, mainTexName, aprName));

                    #region NExt

                    ReplaceNExtSegments(netInfo, segment, ref nextLog);

                    if (netInfo.name.Contains("Medium Avenue"))
                    {
                        if (netInfo.name.Contains("TL"))
                        {
                            if (netInfo.name.Contains(RoadPos.Ground))
                            {
                                segList.Add(
                                    new SegmentSet(
                                        segment,
                                        "MediumAvenue4LTL_Ground_Segment",
                                        "RoadLargeSegment-default-apr"));
                            }
                            else if (netInfo.name.Contains(RoadPos.Elevated))
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
                        }
                        else
                        {
                            if (netInfo.name.Contains(RoadPos.Ground))
                            {
                                segList.Add(
                                    new SegmentSet(
                                        segment,
                                        "MediumAvenue4L_Ground_Segment",
                                        "RoadLargeSegment-default-apr"));
                            }
                            else if (netInfo.name.Contains(RoadPos.Elevated))
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
                        }
                    }

                    if (netInfo.name.Contains("Rural Highway"))
                    {
                        if (netInfo.name.Contains("Small"))
                        {
                            if (mainTexName.Contains(RoadPos.Ground) || mainTexName.Contains(RoadPos.Elevated))
                            {
                                segList.Add(new SegmentSet(segment, "Highway1L_Ground_Segment"));
                            }

                            if (mainTexName.Contains(RoadPos.Slope))
                            {
                                segList.Add(new SegmentSet(segment, "Highway1L_Slope_Segment"));
                            }

                            if (mainTexName.Contains(RoadPos.Tunnel))
                            {
                                segList.Add(new SegmentSet(segment, "Highway1L_Tunnel_Segment"));
                            }
                        }
                        else
                        {
                            if (mainTexName.Contains(RoadPos.Ground) || mainTexName.Contains(RoadPos.Elevated))
                            {
                                segList.Add(new SegmentSet(segment, "Highway2L_Ground_Segment"));
                            }

                            if (mainTexName.Contains(RoadPos.Slope))
                            {
                                segList.Add(new SegmentSet(segment, "Highway2L_Slope_Segment"));
                            }

                            if (mainTexName.Contains(RoadPos.Tunnel))
                            {
                                segList.Add(new SegmentSet(segment, "Highway2L_Tunnel_Segment"));
                            }
                        }
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
                                    // segment.m_segmentMaterial.SetTexture(TexType._APRMap, LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "RoadBasic2-apr"+ ext_DDS)));
                                    // segment.m_lodRenderDistance = 2500;
                                    // }
                                    // else if (File.Exists(Path.Combine(ModLoader.APRMaps_Path, "RoadBasic2-apr"+ ext_DDS)))
                                    // {
                                    // segment.m_segmentMaterial.SetTexture(TexType._APRMap, LoadTextureDDS(Path.Combine(ModLoader.APRMaps_Path, "RoadBasic2-apr"+ ext_DDS)));
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

                    if (netInfo.name.Contains("Large Road") && netInfo.name.Contains("Bus Lanes"))
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

                    #endregion

                    allSegments += "\n" + className + " | " + netInfo.name + " | " + mainTexName + " | "
                                   + segment.m_segmentMesh.name;

                    // I'm combining cofiguration with this region to sort each item by Network Type/Class
                    // Also combining if statements where && is appropriate
                    // Killface: won't use your code as it's bloated. don't want 10,000 lines of a crappy forced naming scheme which breaks working functions
                    // #region Oneways
                    string meshName = segment.m_mesh.name;
                    if (netInfo.name.Contains("Oneway"))
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

                        if (mainTexName == "small-tunnel_d")
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

                    if (meshName.Equals("SmallRoadSegmentBusBoth"))
                    {
                        if (!netInfo.name.Contains("Bicycle"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadSmall_D_BusBoth"));
                        }
                    }

                    if (meshName.Equals("SmallRoadSegment2BusBoth"))
                    {
                        segList.Add(new SegmentSet(segment, "SmallRoadSegmentDeco_BusBoth"));
                    }

                    if (meshName.Equals("RoadMediumSegmentBusSide"))
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

                        goto configsettings;
                    }

                    if (meshName.Equals("RoadMediumSegmentBusBoth"))
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

                        goto configsettings;
                    }

                    if (!(netInfo.name.Contains("Bicycle") || netInfo.name.Contains("Oneway")))
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

                    configsettings:

                    if (ModLoader.config.basic_road_parking == 1)
                    {
                        if (mainTexName.Equals("RoadSmall_D"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadSmall_D_parking1"));
                        }

                        if (mainTexName.Equals("RoadSmall_D_BusSide"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadSmall_D_BusSide_parking1"));
                        }
                    }

                    if (ModLoader.config.medium_road_parking == 1)
                    {
                        if (!(netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees")))
                        {
                            if (meshName.Equals("RoadMediumSegmentBusSide"))
                            {
                                if (mainTexName.Equals("RoadMediumSegment_d"))
                                {
                                    segList.Add(new SegmentSet(segment, "RoadMedium_D_BusSide_parking1"));
                                }
                            }
                            else if (meshName.Equals("RoadMediumSegmentBusBoth"))
                            {
                                if (mainTexName.Equals("RoadMediumSegment_d"))
                                {
                                    segList.Add(new SegmentSet(segment, "RoadMedium_D_BusBoth_parking1"));
                                }
                            }
                            else if (mainTexName.Equals("RoadMediumSegment_d") || mainTexName.Equals("RoadMedium_D"))
                            {
                                segList.Add(new SegmentSet(segment, "RoadMedium_D_parking1"));
                            }
                        }
                    }

                    if (ModLoader.config.medium_road_grass_parking == 1 && netInfo.name.Contains("Grass"))
                    {
                        if (meshName.Equals("RoadMediumSegmentBusSide"))
                        {
                            if (mainTexName.Equals("RoadMedium_D"))
                            {
                                segList.Add(new SegmentSet(segment, "RoadMedium_D_BusSide_parking1"));
                            }
                        }
                        else if (meshName.Equals("RoadMediumSegmentBusBoth"))
                        {
                            if (mainTexName.Equals("RoadMedium_D"))
                            {
                                segList.Add(new SegmentSet(segment, "RoadMedium_D_BusBoth_parking1"));
                            }
                        }
                        else if (mainTexName.Equals("RoadMediumSegment_d"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadMedium_D_parking1"));
                        }
                        else if (mainTexName.Equals("RoadMedium_D"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadMedium_D_parking1"));
                        }
                    }

                    if (ModLoader.config.medium_road_trees_parking == 1 && netInfo.name.Contains("Trees"))
                    {
                        if (meshName.Equals("RoadMediumSegmentBusSide"))
                        {
                            if (mainTexName.Equals("RoadMediumDeco_d"))
                            {
                                segList.Add(new SegmentSet(segment, "RoadMediumDeco_d_BusSide_parking1"));
                            }
                        }
                        else if (meshName.Equals("RoadMediumSegmentBusBoth"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadMediumDeco_d"));
                        }
                        else if (mainTexName.Equals("RoadMediumDeco_d"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadMediumDeco_d_parking1"));
                        }
                    }

                    if (ModLoader.config.medium_road_bus_parking == 1)
                    {
                        if (meshName.Equals("RoadMediumSegmentBusSide"))
                        {
                            if (mainTexName.Equals("RoadMediumBusLane"))
                            {
                                segList.Add(new SegmentSet(segment, "RoadMediumBusLane_BusSide_parking1"));
                            }
                        }
                        else if (meshName.Equals("RoadMediumSegmentBusBoth"))
                        {
                            if (mainTexName.Equals("RoadMediumBusLane"))
                            {
                                segList.Add(new SegmentSet(segment, "RoadMediumBusLane_BusBoth_parking1"));
                            }
                        }
                        else if (mainTexName.Equals("RoadMediumBusLane"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadMediumBusLane_parking1"));
                        }
                    }

                    if (ModLoader.config.large_road_parking == 1)
                    {
                        if (meshName.Equals("LargeRoadSegmentBusSide"))
                        {
                            if (mainTexName.Equals("RoadLargeSegment_d"))
                            {
                                segList.Add(new SegmentSet(segment, "RoadLargeSegment_d_BusSide_parking1"));
                            }
                        }
                        else if (meshName.Equals("LargeRoadSegmentBusBoth"))
                        {
                            if (mainTexName.Equals("RoadLargeSegment_d"))
                            {
                                segList.Add(new SegmentSet(segment, "RoadLargeSegment_d_BusBoth_parking1"));
                            }
                        }
                        else if (mainTexName.Equals("RoadLargeSegment_d"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadLargeSegment_d_parking1"));
                        }
                    }

                    if (ModLoader.config.large_oneway_parking == 1)
                    {
                        if (meshName.Equals("LargeRoadSegmentBusSide"))
                        {
                            if (mainTexName.Equals("RoadLargeOnewaySegment_d"))
                            {
                                segList.Add(new SegmentSet(segment, "RoadLargeOnewaySegment_d_BusSide_parking1"));
                            }
                        }
                        else if (mainTexName.Equals("RoadLargeOnewaySegment_d"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadLargeOnewaySegment_d_parking1"));
                        }
                    }

                    if (ModLoader.config.large_road_bus_parking == 1)
                    {
                        if (meshName.Equals("LargeRoadSegmentBusSideBusLane"))
                        {
                            if (mainTexName.Equals("RoadLargeBuslane_D"))
                            {
                                segList.Add(new SegmentSet(segment, "RoadLargeBuslane_D_BusSide_parking1"));
                            }
                        }
                        else if (meshName.Equals("LargeRoadSegmentBusBothBusLane"))
                        {
                            if (mainTexName.Equals("RoadLargeBuslane_D"))
                            {
                                segList.Add(new SegmentSet(segment, "RoadLargeBuslane_D_BusBoth_parking1"));
                            }
                        }
                        else if (mainTexName.Equals("RoadLargeBuslane_D"))
                        {
                            segList.Add(new SegmentSet(segment, "RoadLargeBuslane_D_parking1"));
                        }
                    }

                    allSegments += "\n" + aprName;

                    if (aprName.Equals("LargeRoadSegmentBusSide-BikeLane-apr")
                        || aprName.Equals("LargeRoadSegmentBusBoth-BikeLane-apr"))
                    {
                        segList.Add(new SegmentSet(segment, null, "RoadLargeSegment-BikeLane-apr"));
                    }

                    if (aprName.Equals("LargeRoadSegmentBusSide-LargeRoadSegmentBusSide-apr")
                        || aprName.Equals("LargeRoadSegmentBusBoth-LargeRoadSegmentBusBoth-apr"))
                    {
                        segList.Add(new SegmentSet(segment, null, "RoadLargeSegment-default-apr"));
                    }
                }
            }

            foreach (SegmentSet set in segList)
            {
                SetSegmentDirect(set);
                log += "\n" + set.segment + " | " + set.MainTex + " - " + set.APRMap;
            }

            foreach (NodeSet set in nodeList)
            {
                SetNodeDirect(set);
                log += "\n" + set.node + " | " + set.MainTex + " - " + set.APRMap;
            }

            Debug.Log(log);

            // Debug.Log(allNodes);
            // Debug.Log(allSegments);
            Debug.Log(nextLog);

            // Singleton<NetManager>.instance.m_lodAprAtlas = null;


            // Debug.Log(log);

            ShittyPlusFuck.ReplaceShitFuckingPlus();
            LODResetter.ResetLOD();
          //  Singleton<NetManager>.instance.InitRenderData();

        }

        private static void SetSegmentDirect(SegmentSet set)
        {
            NetInfo.Segment segment = set.segment;
            string maintex = set.MainTex;
            string apr = set.APRMap;

            string currentTextures = ModLoader.currentTexturesPath_default;
            if (!maintex.IsNullOrWhiteSpace())
            {
                string type = TexType._MainTex;
                if (File.Exists(Path.Combine(currentTextures, maintex + ExtDDS)))
                {
                    segment.m_segmentMaterial.SetTexture(
                        type,
                        LoadTextureDDS(Path.Combine(currentTextures, maintex + ExtDDS)));
                    string filename_lod = Path.Combine(
                        currentTextures + "/LOD",
                        maintex + "_lod" + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        segment.m_lodMaterial.SetTexture(type, LoadTextureDDS(filename_lod));
                    }
                }
                else if (File.Exists(Path.Combine(currentTextures, maintex + type + ExtDDS)))
                {
                    segment.m_segmentMaterial.SetTexture(
                        type,
                        LoadTextureDDS(Path.Combine(currentTextures, maintex + type + ExtDDS)));

                    string filename_lod = Path.Combine(
                        currentTextures + "/LOD",
                        maintex + type + "_lod" + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        segment.m_lodMaterial.SetTexture(type, LoadTextureDDS(filename_lod));
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
                string type = TexType._APRMap;
                string path = Path.Combine(currentTextures, apr + ExtDDS);
                string path2 = Path.Combine(ModLoader.APRMaps_Path, apr + ExtDDS);
                string path3 = Path.Combine(currentTextures, apr + type + ExtDDS);
                string path4 = Path.Combine(ModLoader.APRMaps_Path, apr + type + ExtDDS);

                if (File.Exists(path))
                {
                    segment.m_segmentMaterial.SetTexture(type, LoadTextureDDS(path));
                    string filename_lod = Path.Combine(
                        currentTextures + "/LOD",
                        apr + "_lod" + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        segment.m_lodMaterial.SetTexture(type, LoadTextureDDS(filename_lod));
                    }
                }
                else if (File.Exists(path2))
                {
                    segment.m_segmentMaterial.SetTexture(type, LoadTextureDDS(path2));
                    string filename_lod = Path.Combine(
                        ModLoader.APRMaps_Path + "/LOD",
                        apr + "_lod" + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        segment.m_lodMaterial.SetTexture(type, LoadTextureDDS(filename_lod));
                    }
                }
                else if (File.Exists(path3))
                {
                    segment.m_segmentMaterial.SetTexture(type, LoadTextureDDS(path3));
                    string filename_lod = Path.Combine(
                        currentTextures + "/LOD",
                        apr +type + "_lod" + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        segment.m_lodMaterial.SetTexture(type, LoadTextureDDS(filename_lod));
                    }
                }
                else if (File.Exists(path4))
                {
                    segment.m_segmentMaterial.SetTexture(type, LoadTextureDDS(path4));
                    string filename_lod = Path.Combine(
                        ModLoader.APRMaps_Path + "/LOD",
                        apr + type + "_lod" + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        segment.m_lodMaterial.SetTexture(type, LoadTextureDDS(filename_lod));
                    }
                }
            }
        }

        private static void SetNodeDirect(NodeSet set)
        {
            NetInfo.Node node = set.node;
            string maintex = set.MainTex;
            string apr = set.APRMap;

            string texPath = ModLoader.currentTexturesPath_default;
            if (!maintex.IsNullOrWhiteSpace())
            {
                if (File.Exists(Path.Combine(texPath, maintex + ExtDDS)))
                {
                    node.m_nodeMaterial.SetTexture(
                        TexType._MainTex,
                        LoadTextureDDS(Path.Combine(texPath, maintex + ExtDDS)));

                    string filename_lod = Path.Combine(
                        texPath + "/LOD", maintex + "_lod" + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        node.m_lodMaterial.SetTexture(maintex, LoadTextureDDS(filename_lod));
                    }
                }
                else if (File.Exists(Path.Combine(texPath, maintex + TexType._MainTex + ExtDDS)))
                {
                    node.m_nodeMaterial.SetTexture(
                        TexType._MainTex,
                        LoadTextureDDS(Path.Combine(texPath, maintex + TexType._MainTex + ExtDDS)));

                    string filename_lod = Path.Combine(texPath + "/LOD", maintex + TexType._MainTex + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        node.m_lodMaterial.SetTexture(maintex, LoadTextureDDS(filename_lod));
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
                string path = Path.Combine(texPath, apr + ExtDDS);
                string path2 = Path.Combine(ModLoader.APRMaps_Path, apr + ExtDDS);
                string path3 = Path.Combine(texPath, apr + TexType._APRMap + ExtDDS);
                string path4 = Path.Combine(ModLoader.APRMaps_Path, apr + TexType._APRMap + ExtDDS);

                if (File.Exists(path))
                {
                    node.m_nodeMaterial.SetTexture(TexType._APRMap, LoadTextureDDS(path));
                    string filename_lod = Path.Combine(texPath + "/LOD", apr + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        node.m_lodMaterial.SetTexture(apr, LoadTextureDDS(filename_lod));
                    }
                }
                else if (File.Exists(path2))
                {
                    node.m_nodeMaterial.SetTexture(TexType._APRMap, LoadTextureDDS(path2));
                    string filename_lod = Path.Combine(ModLoader.APRMaps_Path + "/LOD", apr + TexType._APRMap + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        node.m_lodMaterial.SetTexture(apr, LoadTextureDDS(filename_lod));
                    }
                }
                else if (File.Exists(path3))
                {
                    node.m_nodeMaterial.SetTexture(TexType._APRMap, LoadTextureDDS(path3));
                    string filename_lod = Path.Combine(texPath + "/LOD", apr + TexType._APRMap + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        node.m_lodMaterial.SetTexture(apr, LoadTextureDDS(filename_lod));
                    }
                }
                else if (File.Exists(path4))
                {
                    node.m_nodeMaterial.SetTexture(TexType._APRMap, LoadTextureDDS(path4));
                    string filename_lod = Path.Combine(ModLoader.APRMaps_Path + "/LOD", apr + TexType._APRMap + ExtDDS);
                    if (File.Exists(filename_lod))
                    {
                        node.m_lodMaterial.SetTexture(maintex, LoadTextureDDS(filename_lod));
                    }
                }
            }
        }


        #endregion Public Methods

        #region Private Methods

        private static void ReplaceNExtNodes(NetInfo netInfo, NetInfo.Node node, ref string log)
        {
            foreach (KeyValuePair<string, string> road in NExtRoads)
            {
                if (netInfo.name.Contains(road.Key))
                {
                    foreach (string roadPosition in RoadPos.AllPositions)
                    {
                        foreach (string textype in TexType.AllTex)
                        {
                            if (node.m_nodeMaterial.GetTexture(textype) == null)
                            {
                                continue;
                            }

                            string filename = road.Value + "_" + roadPosition + "_" + "Node" + textype + ExtDDS;
                            string fullPath = Path.Combine(ModLoader.currentTexturesPath_default, filename);
                            string aprPath = Path.Combine(ModLoader.APRMaps_Path, filename);

                            if (node.m_nodeMaterial.GetTexture(textype).name.Contains(roadPosition))
                            {
                                if (File.Exists(fullPath))
                                {
                                    node.m_nodeMaterial.SetTexture(textype, LoadTextureDDS(fullPath));
                                    string filename_lod = Path.Combine(ModLoader.currentTexturesPath_default + "/LOD", filename + "_lod" + ExtDDS);
                                    if (File.Exists(filename_lod))
                                    {
                                        node.m_lodMaterial.SetTexture(textype, LoadTextureDDS(filename_lod));
                                    }

                                    log += "\nNExt replacing " + textype + " - " + fullPath;
                                }
                                else if (textype == TexType._APRMap && File.Exists(aprPath))
                                {
                                    node.m_nodeMaterial.SetTexture(TexType._APRMap, LoadTextureDDS(aprPath));
                                    string filename_lod = Path.Combine(ModLoader.APRMaps_Path + "/LOD", filename + "_lod" + ExtDDS);
                                    if (File.Exists(filename_lod))
                                    {
                                        node.m_lodMaterial.SetTexture(textype, LoadTextureDDS(filename_lod));
                                    }
                                    log += "\nNExt replacing " + TexType._APRMap + " - " + aprPath;
                                }

                            }
                        }
                    }
                }
            }
        }
        public static readonly Dictionary<string, string> NExtRoads = new Dictionary<string, string>
                                                                          {
                                                                              { "Two-Lane Alley", "Alley2L" },
                                                                              { "BasicRoadTL", "BasicRoadTL" },
                                                                              { "One-Lane Oneway", "OneWay1L" },
                                                                              { "Oneway3L", "Oneway3L" },
                                                                              { "Oneway4L", "Oneway4L" },
                                                                              { "Small Avenue", "SmallAvenue4L" },
                                                                              { "Medium Avenue", "MediumAvenue4L" },
                                                                              { "Medium Avenue TL", "MediumAvenue4LTL" },
                                                                              { "Eight-Lane Avenue", "LargeAvenue8LM" },
                                                                              { "Four-Lane Highway", "Highway4L" },
                                                                              { "Five-Lane Highway", "Highway5L" },
                                                                              { "Large Highway", "Highway6L" }
                                                                          };

        private static void ReplaceNExtSegments(NetInfo netInfo, NetInfo.Segment segment, ref string log)
        {
            foreach (KeyValuePair<string, string> road in NExtRoads)
            {
                if (netInfo.name.Contains(road.Key))
                {
                    foreach (string roadPosition in RoadPos.AllPositions)
                    {
                        foreach (string textype in TexType.AllTex)
                        {
                            string file = road.Value + "_" + roadPosition + "_" + "Segment" + textype;
                            string filename = Path.Combine(ModLoader.currentTexturesPath_default, file + ExtDDS);
                            if (segment.m_segmentMaterial.GetTexture(textype).name.Contains(roadPosition))
                            {
                                if (File.Exists(filename))
                                {
                                    segment.m_segmentMaterial.SetTexture(textype, LoadTextureDDS(filename));

                                    string filename_lod = Path.Combine(ModLoader.currentTexturesPath_default + "/LOD", file + "_lod" + ExtDDS);
                                    if (File.Exists(filename_lod))
                                    {
                                        segment.m_lodMaterial.SetTexture(textype, LoadTextureDDS(filename_lod));
                                    }
                                }
                                else if (textype.Equals(TexType._APRMap))
                                {
                                    // APR Maps only
                                    filename = Path.Combine(ModLoader.APRMaps_Path, file + ExtDDS);
                                    log += "\nNExt Segments APR looking for: " + filename;

                                    if (File.Exists(filename))
                                    {
                                        segment.m_segmentMaterial.SetTexture(textype, LoadTextureDDS(filename));

                                        string filename_lod = Path.Combine(
                                            ModLoader.APRMaps_Path + "/LOD",
                                            file + "_lod" + ExtDDS);
                                        if (File.Exists(filename_lod))
                                        {
                                            segment.m_lodMaterial.SetTexture(textype, LoadTextureDDS(filename_lod));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion Private Methods
    }
}