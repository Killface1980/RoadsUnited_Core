namespace RoadsUnited_Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using ColossalFramework;

    using JetBrains.Annotations;

    using UnityEngine;

    public class RoadsUnited_Core : MonoBehaviour
    {
        private const string aprmap = "_APRMap";

        private const string aprMapSuffix = "_APRMap.dds";

        private const string maintex = "_MainTex";

        private const string mainTexSuffix = "_MainTex.dds";

        private const string nodeSuffix = "_Node";

        private const string plusBus = "BusLane";

        private const string roadHighway = "Highway";

        private const string roadLarge = "RoadLarge";

        private const string roadMedium = "RoadMedium";

        private const string roadOneway = "Oneway";

        private const string roadSmall = "RoadSmall";

        private const string roadTiny = "RoadTiny";

        private const string segmentSuffix = "_Segment";

        private const string str2 = "Bike";

        private const string str3 = "Tram";

        private const string str4 = "L1R2";

        private const string str5 = "L1R3";

        private const string str8 = "_LOD_MainTex.dds";

        private const string text10 = "BusBoth";

        private const string text8 = "Deco";

        private const string text9 = "BusSide";

        private const string typeElevated = "Elevated";

        private const string typeGround = "Gnd";

        private const string typeSlope = "Slope";

        private const string typeTunnel = "Tunnel";

        private const string value = "tram-rail-double-wn-No Name";

        private const string value2 = "tram-rail-double-No Name";

        public static Configuration config;

        public static Dictionary<string, Texture2D> vanillaPrefabProperties = new Dictionary<string, Texture2D>();

        private static readonly Dictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D>();

        private static Texture2D acimapTex;

        private static Texture2D aprmapTex;

        private static string currentTexturesPath_default;

        private static Texture2D defaultmapTex;

        public static void ApplyVanillaDictionary()
        {
            uint num = 0u;
            while (num < (ulong)PrefabCollection<NetInfo>.LoadedCount())
            {
                NetInfo loaded = PrefabCollection<NetInfo>.GetLoaded(num);
                if (!(loaded == null))
                {
                    string text = loaded.name.Replace(" ", "_").ToLowerInvariant().Trim();
                    NetInfo.Node[] nodes = loaded.m_nodes;
                    for (int i = 0; i < nodes.Length; i++)
                    {
                        NetInfo.Node node = nodes[i];
                        if (!loaded.m_class.name.Contains("Heating Pipe") && !loaded.m_class.name.Contains("Water")
                            && !loaded.m_class.name.Contains("Train") && !loaded.m_class.name.Contains("Metro")
                            && !loaded.m_class.name.Contains("Transport") && !loaded.m_class.name.Contains("Bus Line")
                            && !loaded.m_class.name.Contains("Airplane") && !loaded.m_class.name.Contains("Ship"))
                        {
                            if (node.m_nodeMaterial != null && !node.m_nodeMaterial.name.Contains("rail"))
                            {
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
                        }
                    }

                    NetInfo.Segment[] segments = loaded.m_segments;
                    for (int j = 0; j < segments.Length; j++)
                    {
                        NetInfo.Segment segment = segments[j];
                        if (!loaded.m_class.name.Contains("Heating Pipe") && !loaded.m_class.name.Contains("Water")
                            && !loaded.m_class.name.Contains("Train") && !loaded.m_class.name.Contains("Metro")
                            && !loaded.m_class.name.Contains("Transport") && !loaded.m_class.name.Contains("Bus Line")
                            && !loaded.m_class.name.Contains("Airplane") && !loaded.m_class.name.Contains("Ship"))
                        {
                            if (segment.m_segmentMaterial != null && !segment.m_segmentMaterial.name.Contains("rail"))
                            {
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
                    }
                }

                num += 1u;
            }

            PropCollection[] array = FindObjectsOfType<PropCollection>();
            for (int k = 0; k < array.Length; k++)
            {
                PropCollection propCollection = array[k];
                try
                {
                    PropInfo[] prefabs = propCollection.m_prefabs;
                    for (int l = 0; l < prefabs.Length; l++)
                    {
                        PropInfo propInfo = prefabs[l];
                        string name = propInfo.name;
                        if (propInfo.m_lodMaterialCombined.GetTexture(maintex).name != null)
                        {
                            if (vanillaPrefabProperties.TryGetValue(name + "_prop__MainTex", out defaultmapTex))
                            {
                                propInfo.m_lodMaterialCombined.SetTexture(maintex, defaultmapTex);
                            }

                            if (vanillaPrefabProperties.TryGetValue(name + "_prop__ACIMap", out acimapTex))
                            {
                                propInfo.m_lodMaterialCombined.SetTexture("_ACIMap", acimapTex);
                            }
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
                if (!(loaded == null))
                {
                    string text = loaded.name.Replace(" ", "_").ToLowerInvariant().Trim();
                    NetInfo.Node[] nodes = loaded.m_nodes;
                    for (int i = 0; i < nodes.Length; i++)
                    {
                        NetInfo.Node node = nodes[i];
                        if (!loaded.m_class.name.Contains("Heating Pipe") && !loaded.m_class.name.Contains("Water")
                            && !loaded.m_class.name.Contains("Train") && !loaded.m_class.name.Contains("Metro")
                            && !loaded.m_class.name.Contains("Transport") && !loaded.m_class.name.Contains("Bus Line")
                            && !loaded.m_class.name.Contains("Airplane") && !loaded.m_class.name.Contains("Ship"))
                        {
                            if (node.m_nodeMaterial != null && !node.m_nodeMaterial.name.Contains("rail"))
                            {
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
                        }
                    }

                    NetInfo.Segment[] segments = loaded.m_segments;
                    for (int j = 0; j < segments.Length; j++)
                    {
                        NetInfo.Segment segment = segments[j];
                        if (!loaded.m_class.name.Contains("Heating Pipe") && !loaded.m_class.name.Contains("Water")
                            && !loaded.m_class.name.Contains("Train") && !loaded.m_class.name.Contains("Metro")
                            && !loaded.m_class.name.Contains("Transport") && !loaded.m_class.name.Contains("Bus Line")
                            && !loaded.m_class.name.Contains("Airplane") && !loaded.m_class.name.Contains("Ship"))
                        {
                            if (segment.m_segmentMaterial != null && !segment.m_segmentMaterial.name.Contains("rail"))
                            {
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
                    }
                }

                num += 1u;
            }
        }

        public static void CreateVanillaDictionary()
        {
            uint num = 0u;
            while (num < (ulong)PrefabCollection<NetInfo>.LoadedCount())
            {
                NetInfo loaded = PrefabCollection<NetInfo>.GetLoaded(num);
                if (!(loaded == null))
                {
                    string text = loaded.name.Replace(" ", "_").ToLowerInvariant().Trim();
                    NetInfo.Node[] nodes = loaded.m_nodes;
                    for (int i = 0; i < nodes.Length; i++)
                    {
                        NetInfo.Node node = nodes[i];
                        if (!loaded.m_class.name.Contains("Heating Pipe") && !loaded.m_class.name.Contains("Water")
                            && !loaded.m_class.name.Contains("Train") && !loaded.m_class.name.Contains("Metro")
                            && !loaded.m_class.name.Contains("Transport") && !loaded.m_class.name.Contains("Bus Line")
                            && !loaded.m_class.name.Contains("Airplane") && !loaded.m_class.name.Contains("Ship"))
                        {
                            if (node.m_nodeMaterial != null && !node.m_nodeMaterial.name.Contains("rail"))
                            {
                                vanillaPrefabProperties.Add(
                                    string.Concat(text, "_node_", i, "_nodeMaterial_MainTex"),
                                    node.m_nodeMaterial.GetTexture(maintex) as Texture2D);
                                vanillaPrefabProperties.Add(
                                    string.Concat(text, "_node_", i, "_nodeMaterial_APRMap"),
                                    node.m_nodeMaterial.GetTexture(aprmap) as Texture2D);
                            }
                        }
                    }

                    NetInfo.Segment[] segments = loaded.m_segments;
                    for (int j = 0; j < segments.Length; j++)
                    {
                        NetInfo.Segment segment = segments[j];
                        if (!loaded.m_class.name.Contains("Heating Pipe") && !loaded.m_class.name.Contains("Water")
                            && !loaded.m_class.name.Contains("Train") && !loaded.m_class.name.Contains("Metro")
                            && !loaded.m_class.name.Contains("Transport") && !loaded.m_class.name.Contains("Bus Line")
                            && !loaded.m_class.name.Contains("Airplane") && !loaded.m_class.name.Contains("Ship"))
                        {
                            if (segment.m_segmentMaterial != null && !segment.m_segmentMaterial.name.Contains("rail"))
                            {
                                vanillaPrefabProperties.Add(
                                    string.Concat(text, "_segment_", j, "_segmentMaterial_MainTex"),
                                    segment.m_segmentMaterial.GetTexture(maintex) as Texture2D);
                                vanillaPrefabProperties.Add(
                                    string.Concat(text, "_segment_", j, "_segmentMaterial_APRMap"),
                                    segment.m_segmentMaterial.GetTexture(aprmap) as Texture2D);
                            }
                        }
                    }
                }

                num += 1u;
            }

            PropCollection[] array = FindObjectsOfType<PropCollection>();
            for (int k = 0; k < array.Length; k++)
            {
                PropCollection propCollection = array[k];
                try
                {
                    PropInfo[] prefabs = propCollection.m_prefabs;
                    for (int l = 0; l < prefabs.Length; l++)
                    {
                        PropInfo propInfo = prefabs[l];
                        string name = propInfo.name;
                        if (propInfo.m_lodMaterialCombined.GetTexture(maintex).name != null)
                        {
                            vanillaPrefabProperties.Add(
                                name + "_prop__MainTex",
                                propInfo.m_lodMaterialCombined.GetTexture(maintex) as Texture2D);
                            vanillaPrefabProperties.Add(
                                name + "_prop__ACIMap",
                                propInfo.m_lodMaterialCombined.GetTexture("_ACIMap") as Texture2D);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public static Texture2D LoadTexture(string fullPath)
        {
            Texture2D texture2D = new Texture2D(1, 1);
            Texture2D result;
            if (textureCache.TryGetValue(fullPath, out texture2D))
            {
                result = texture2D;
            }
            else
            {
                texture2D.LoadImage(File.ReadAllBytes(fullPath));
                texture2D.name = Path.GetFileName(fullPath);
                texture2D.anisoLevel = 8;
                texture2D.Compress(true);
                result = texture2D;
            }

            return result;
        }

        public static Texture2D LoadTextureDDS(string fullPath)
        {
            Texture2D result;
            if (textureCache.TryGetValue(fullPath, out Texture2D texture2D))
            {
                result = texture2D;
            }
            else
            {
                byte[] array = File.ReadAllBytes(fullPath);
                int width = BitConverter.ToInt32(array, 16);
                int height = BitConverter.ToInt32(array, 12);
                texture2D = new Texture2D(width, height, TextureFormat.DXT5, true);
                List<byte> list = new List<byte>();
                for (int i = 0; i < array.Length; i++)
                {
                    if (i > 127)
                    {
                        list.Add(array[i]);
                    }
                }

                texture2D.LoadRawTextureData(list.ToArray());
                texture2D.name = Path.GetFileName(fullPath);
                texture2D.anisoLevel = 8;
                texture2D.Apply();
                textureCache.Add(fullPath, texture2D);
                result = texture2D;
            }

            return result;
        }

        public static void ReplaceNetTextures()
        {
            string tex2A = ModLoader.Tex2A;
            string tex2B = ModLoader.Tex2B;
            string tex2C = ModLoader.Tex2C;
            string path = string.Empty;

            if (!ModLoader.config.texturePackPath.IsNullOrWhiteSpace())
            {
                ModLoader.currentTexturesPath_default = Path.Combine(ModLoader.config.texturePackPath, "BaseTextures");
            }

            string texPath = ModLoader.currentTexturesPath_default;
            string propTexPath = texPath + "/PropTextures/";
            string pathTiny = texPath + "/" + roadTiny + "/";
            string pathSmall = texPath + "/" + roadSmall + "/";
            string pathMedium = texPath + "/" + roadMedium + "/";
            string pathLarge = texPath + "/" + roadLarge + "/";
            string pathHighway = texPath + "/" + roadHighway + "/";

            uint num = 0u;
            while (num < (ulong)PrefabCollection<NetInfo>.LoadedCount())
            {
                NetInfo netInfo = PrefabCollection<NetInfo>.GetLoaded(num);
                if (netInfo != null)
                {
                    if (netInfo.m_class.name.Contains("NExt") || netInfo.name.Contains("Busway")
                        || netInfo.name.Contains("Large Road") && netInfo.name.Contains("Bus Lane"))
                    {
                        NetInfo.Node[] nodes = netInfo.m_nodes;
                        for (int i = 0; i < nodes.Length; i++)
                        {
                            string coreName = string.Empty;
                            string plusPath = string.Empty;
                            string plusName = string.Empty;

                            NetInfo.Node node = nodes[i];
                            if (node.m_nodeMaterial.GetTexture(maintex) != null
                                && !node.m_nodeMaterial.name.Contains("rail"))
                            {
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
                                    plusName = roadLarge + "8L" + typeGround;

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
                                else if (netInfo.name.Contains("Small Rural Highway"))
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
                                else if (netInfo.name.Contains("Rural Highway"))
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
                                else if (netInfo.name.Contains("Four-Lane Highway"))
                                {
                                    coreName = "Highway4L_Ground";
                                    plusPath = pathHighway;
                                    plusName = roadHighway + "4L" + typeGround;

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
                                else if (netInfo.name.Contains("Five-Lane Highway"))
                                {
                                    coreName = "Highway5L_Ground";
                                    plusPath = pathHighway;
                                    plusName = roadHighway + "5L" + typeGround;

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
                                else if (netInfo.name.Contains("Large Highway"))
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

                                SetNodeTextures(node, coreName, plusPath, plusName);
                            }
                        }

                        NetInfo.Segment[] segments = netInfo.m_segments;
                        for (int j = 0; j < segments.Length; j++)
                        {
                            NetInfo.Segment segment = segments[j];
                            if (segment.m_segmentMaterial.GetTexture(maintex) != null
                                && !segment.m_material.name.ToLower().Contains("cable"))
                            {
                                string coreName = string.Empty;
                                string plusPath = string.Empty;
                                string plusName = string.Empty;

                                if (netInfo.name.Equals("Two-Lane Alley"))
                                {
                                    coreName = "Alley2L_Ground";
                                    plusPath = pathTiny;
                                    plusName = roadTiny + "2L";

                                    if (!SetSegmenTextures(segment, coreName, plusPath, plusName))
                                    {
                                        coreName = "OneWay1L_Ground";
                                        plusName = roadTiny + "1L";
                                    }
                                }

                                if (netInfo.name.Equals("One-Lane Oneway"))
                                {
                                    coreName = "OneWay1L_Ground";
                                    plusPath = pathTiny;
                                    plusName = roadTiny + "1L";
                                }

                                if (netInfo.name.Contains("BasicRoadTL"))
                                {
                                    plusPath = pathSmall;
                                    if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Ground"))
                                    {
                                        coreName = "BasicRoadTL_Ground";
                                        plusName = roadSmall + "TL" + typeGround;
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

                                if (netInfo.name.Contains("Small Avenue"))
                                {
                                    plusPath = pathSmall;
                                    if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Ground"))
                                    {
                                        coreName = "SmallAvenue4L_Ground";
                                        plusName = roadSmall + "4L" + typeGround;
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

                                if (netInfo.name.Contains("Oneway3L"))
                                {
                                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                    {
                                        coreName = "OneWay3L_Ground";
                                        plusPath = pathSmall;
                                        plusName = roadSmall + roadOneway + "3L" + typeGround;
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

                                if (netInfo.name.Contains("Oneway4L"))
                                {
                                    if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                    {
                                        coreName = "OneWay4L_Ground";
                                        plusPath = pathSmall;
                                        plusName = roadSmall + roadOneway + "4L" + typeGround;
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

                                if (netInfo.name.Contains("AsymRoadL1R2"))
                                {
                                    plusPath = pathSmall;
                                    plusName = roadSmall + str4 + typeGround;

                                    if (netInfo.name.Equals("AsymRoadL1R2 Elevated")
                                        || netInfo.name.Equals("AsymRoadL1R2 Bridge"))
                                    {
                                        plusName = roadSmall + str4 + typeElevated;
                                    }
                                    else if (netInfo.name.Equals("AsymRoadL1R2 Slope")
                                             && segment.m_mesh.name == "Slope")
                                    {
                                        plusName = roadSmall + str4 + typeGround;
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

                                if (netInfo.name.Contains("AsymRoadL1R3"))
                                {
                                    plusPath = pathSmall;
                                    plusName = roadSmall + str5 + typeGround;

                                    if (netInfo.name.Equals("AsymRoadL1R3 Elevated")
                                        || netInfo.name.Equals("AsymRoadL1R3 Bridge"))
                                    {
                                        plusName = roadSmall + str5 + typeElevated;
                                    }
                                    else if (netInfo.name.Equals("AsymRoadL1R3 Slope")
                                             && segment.m_mesh.name == "Slope")
                                    {
                                        plusName = roadSmall + str5 + typeGround;
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

                                if (netInfo.name.Contains("Medium Avenue"))
                                {
                                    plusPath = pathMedium;
                                    if (!netInfo.name.Contains("TL"))
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(maintex).name.Contains("Ground"))
                                        {
                                            coreName = "MediumAvenue4L_Ground";
                                            plusName = roadMedium + "4L" + typeGround;
                                        }
                                        else if (segment.m_segmentMaterial.GetTexture(maintex).name
                                            .Contains("Elevated"))
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
                                            plusName = roadMedium + "4LTL" + typeGround;

                                            // Alternate APR textures
                                            if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                            {
                                                if (File.Exists(
                                                    Path.Combine(texPath, "RoadLargeSegment-default-apr.dds")))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(
                                                        aprmap,
                                                        LoadTextureDDS(
                                                            Path.Combine(texPath, "RoadLargeSegment-default-apr.dds")));
                                                }
                                                else if (File.Exists(
                                                    Path.Combine(
                                                        ModLoader.APRMaps_Path,
                                                        "RoadLargeSegment-default-apr.dds")))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(
                                                        aprmap,
                                                        LoadTextureDDS(
                                                            Path.Combine(
                                                                ModLoader.APRMaps_Path,
                                                                "RoadLargeSegment-default-apr.dds")));
                                                }
                                            }
                                        }
                                        else if (segment.m_segmentMaterial.GetTexture(maintex).name
                                            .Contains("Elevated"))
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

                                if (netInfo.name.Contains("Eight-Lane Avenue"))
                                {
                                    plusPath = pathLarge;
                                    coreName = "LargeAvenue8LM_Ground";
                                    plusName = roadLarge + "8L" + typeGround;

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

                                if (netInfo.name.Contains("Highway"))
                                {
                                    plusPath = pathHighway;

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
                                        }
                                    }

                                    if (netInfo.name.Contains("Four-Lane"))
                                    {
                                        coreName = "Highway4L_Ground";
                                        plusName = roadHighway + "4L" + typeGround;

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

                                        segment.m_lodRenderDistance = 2500f;
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
                                    }
                                }

                                if (netInfo.name.Contains("Small Busway"))
                                {
                                    plusPath = pathSmall;
                                    if (netInfo.name.Contains("OneWay"))
                                    {
                                        if (netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees"))
                                        {
                                            if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                                            {
                                                coreName = "Busway2L1W_DecoGrass_Ground";
                                                plusName = roadSmall + roadOneway + plusBus + text8;
                                            }
                                            else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                                            {
                                                plusName = roadSmall + roadOneway + text8 + text9;
                                            }
                                            else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                                            {
                                                plusName = roadSmall + roadOneway + text8 + text10;
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("SmallRoadSegment"))
                                        {
                                            coreName = "Busway2L1W_Ground";
                                            plusName = roadSmall + roadOneway + plusBus + typeGround;
                                        }
                                        else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                        {
                                            plusName = roadSmall + roadOneway + plusBus + text9;
                                        }
                                        else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                                        {
                                            // todo: texture missing?
                                            coreName = "Oneway_RoadSmallSegment_BusBoth";
                                            plusName = roadSmall + roadOneway + plusBus + text10;
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
                                        plusName = roadSmall + plusBus + typeGround;

                                        if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                                        {
                                            if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                            {
                                                coreName = null;
                                                plusName = roadSmall + plusBus + text9;
                                            }
                                            else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                                            {
                                                coreName = null;
                                                plusName = roadSmall + plusBus + text10;
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
                                        plusName = roadSmall + plusBus + text8;

                                        if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                                        {
                                        }
                                        else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                                        {
                                            plusName = roadSmall + plusBus + text8 + text9;
                                        }
                                        else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                                        {
                                            plusName = roadSmall + plusBus + text8 + text10;
                                        }
                                    }
                                }

                                if (netInfo.name.Contains("Large Road With Bus Lanes"))
                                {
                                    plusPath = pathLarge;
                                    coreName = "RoadLargeBuslane_D";

                                    if (segment.m_mesh.name.Equals("RoadLageSegment"))
                                    {
                                        // todo: exceptions for original names
                                        plusName = roadLarge + plusBus + typeGround;
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                    {
                                        plusName = roadLarge + plusBus + text9;
                                    }
                                    else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                    {
                                        plusName = roadLarge + plusBus + text10;
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
                                            plusName = roadLarge + plusBus + text8;
                                            if (File.Exists(
                                                Path.Combine(texPath, "Busway6L_DecoGrass_Ground_Segment_MainTex.dds")))
                                            {
                                                // todo: special apr maps!!!
                                                if (File.Exists(
                                                    Path.Combine(texPath, "RoadLargeSegment-default-apr.dds")))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(
                                                        aprmap,
                                                        LoadTextureDDS(
                                                            Path.Combine(texPath, "RoadLargeSegment-default-apr.dds")));
                                                }

                                                if (File.Exists(
                                                    Path.Combine(
                                                        ModLoader.APRMaps_Path,
                                                        "RoadLargeSegment-default-apr.dds")))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(
                                                        aprmap,
                                                        LoadTextureDDS(
                                                            Path.Combine(
                                                                ModLoader.APRMaps_Path,
                                                                "RoadLargeSegment-default-apr.dds")));
                                                }

                                                if (File.Exists(
                                                    Path.Combine(texPath, "RoadLargeSegment-default-apr.dds")))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(
                                                        aprmap,
                                                        LoadTextureDDS(
                                                            Path.Combine(texPath, "RoadLargeSegment-default-apr.dds")));
                                                }

                                                if (File.Exists(
                                                    Path.Combine(
                                                        ModLoader.APRMaps_Path,
                                                        "RoadLargeSegment-default-apr.dds")))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(
                                                        aprmap,
                                                        LoadTextureDDS(
                                                            Path.Combine(
                                                                ModLoader.APRMaps_Path,
                                                                "RoadLargeSegment-default-apr.dds")));
                                                }
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusSide"))
                                        {
                                            plusName = roadLarge + plusBus + text8 + text9;
                                        }
                                        else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth"))
                                        {
                                            plusName = roadLarge + plusBus + text8 + text10;
                                        }
                                    }
                                }

                                SetSegmenTextures(segment, coreName, plusPath, plusName);

                                if (segment.m_segmentMaterial.GetTexture(aprmap) != null)
                                {
                                    if (netInfo.name.Contains("BasicRoadTL"))
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "BasicRoadTL_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            texPath,
                                                            "BasicRoadTL_Ground_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "BasicRoadTL_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "BasicRoadTL_Ground_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "BasicRoadTL_Elevated_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            texPath,
                                                            "BasicRoadTL_Elevated_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "BasicRoadTL_Elevated_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "BasicRoadTL_Elevated_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "BasicRoadTL_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            texPath,
                                                            "BasicRoadTL_Tunnel_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "BasicRoadTL_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "BasicRoadTL_Tunnel_Segment_APRMap.dds")));
                                            }
                                        }

                                        segment.m_lodRenderDistance = 2500f;
                                    }

                                    if (netInfo.name.Contains("Oneway3L"))
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "OneWay3L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "OneWay3L_Ground_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "OneWay3L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "OneWay3L_Ground_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "OneWay3L_Elevated_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "OneWay3L_Elevated_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "OneWay3L_Elevated_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "OneWay3L_Elevated_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "OneWay3L_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "OneWay3L_Tunnel_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "OneWay3L_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "OneWay3L_Tunnel_Segment_APRMap.dds")));
                                            }
                                        }

                                        segment.m_lodRenderDistance = 2500f;
                                    }

                                    if (netInfo.name.Contains("Oneway4L"))
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "OneWay4L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "OneWay4L_Ground_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "OneWay4L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "OneWay4L_Ground_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "OneWay4L_Elevated_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "OneWay4L_Elevated_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "OneWay4L_Elevated_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "OneWay4L_Elevated_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "OneWay4L_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "OneWay4L_Tunnel_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(texPath, "OneWay4L_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "OneWay4L_Tunnel_Segment_APRMap.dds")));
                                            }
                                        }

                                        segment.m_lodRenderDistance = 2500f;
                                    }

                                    if (netInfo.name.Contains("Small Avenue"))
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "SmallAvenue4L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            texPath,
                                                            "SmallAvenue4L_Ground_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "SmallAvenue4L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "SmallAvenue4L_Ground_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "SmallAvenue4L_Elevated_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            texPath,
                                                            "SmallAvenue4L_Elevated_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "SmallAvenue4L_Elevated_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "SmallAvenue4L_Elevated_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "SmallAvenue4L_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            texPath,
                                                            "SmallAvenue4L_Tunnel_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "SmallAvenue4L_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "SmallAvenue4L_Tunnel_Segment_APRMap.dds")));
                                            }
                                        }

                                        segment.m_lodRenderDistance = 2500f;
                                    }

                                    if (netInfo.name.Contains("Eight-Lane Avenue"))
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "LargeAvenue8LM_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            texPath,
                                                            "LargeAvenue8LM_Ground_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "LargeAvenue8LM_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "LargeAvenue8LM_Ground_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "LargeAvenue8LM_Elevated_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            texPath,
                                                            "LargeAvenue8LM_Elevated_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "LargeAvenue8LM_Elevated_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "LargeAvenue8LM_Elevated_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Slope"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "LargeAvenue8LM_Slope_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            texPath,
                                                            "LargeAvenue8LM_Slope_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "LargeAvenue8LM_Slope_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "LargeAvenue8LM_Slope_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "LargeAvenue8LM_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            texPath,
                                                            "LargeAvenue8LM_Tunnel_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "LargeAvenue8LM_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "LargeAvenue8LM_Tunnel_Segment_APRMap.dds")));
                                            }
                                        }

                                        segment.m_lodRenderDistance = 2500f;
                                    }

                                    if (netInfo.name.Contains("Rural Highway") && netInfo.name.Contains("Small"))
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway1L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway1L_Ground_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway1L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway1L_Ground_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway1L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway1L_Ground_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway1L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway1L_Ground_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Slope"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway1L_Slope_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway1L_Slope_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway1L_Slope_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway1L_Slope_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway1L_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway1L_Tunnel_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway1L_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway1L_Tunnel_Segment_APRMap.dds")));
                                            }
                                        }

                                        segment.m_lodRenderDistance = 2500f;
                                    }

                                    if (netInfo.name.Contains("Rural Highway") && !netInfo.name.Contains("Small"))
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway2L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway2L_Ground_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway2L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway2L_Ground_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway2L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway2L_Ground_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway2L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway2L_Ground_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Slope"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway2L_Slope_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway2L_Slope_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway2L_Slope_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway2L_Slope_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway2L_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway2L_Tunnel_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway2L_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway2L_Tunnel_Segment_APRMap.dds")));
                                            }
                                        }

                                        segment.m_lodRenderDistance = 2500f;
                                    }

                                    if (netInfo.name.Contains("Four-Lane Highway"))
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway4L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway4L_Ground_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway4L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway4L_Ground_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway4L_Elevated_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            texPath,
                                                            "Highway4L_Elevated_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway4L_Elevated_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway4L_Elevated_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Slope"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway4L_Slope_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway4L_Slope_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway4L_Slope_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway4L_Slope_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway4L_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway4L_Tunnel_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway4L_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway4L_Tunnel_Segment_APRMap.dds")));
                                            }
                                        }

                                        segment.m_lodRenderDistance = 2500f;
                                    }

                                    if (netInfo.name.Contains("Five-Lane Highway"))
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway5L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway5L_Ground_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway5L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway5L_Ground_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway5L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway5L_Ground_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway5L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway5L_Ground_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Slope"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway5L_Slope_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway5L_Slope_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway5L_Slope_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway5L_Slope_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway5L_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway5L_Tunnel_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway5L_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway5L_Tunnel_Segment_APRMap.dds")));
                                            }
                                        }

                                        segment.m_lodRenderDistance = 2500f;
                                    }

                                    if (netInfo.name.Contains("Large Highway"))
                                    {
                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Ground"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway6L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway6L_Ground_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway6L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway6L_Ground_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Elevated"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway6L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway6L_Ground_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway6L_Ground_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway6L_Ground_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Slope"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway6L_Slope_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway6L_Slope_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway6L_Slope_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway6L_Slope_Segment_APRMap.dds")));
                                            }
                                        }

                                        if (segment.m_segmentMaterial.GetTexture(aprmap).name.Contains("Tunnel"))
                                        {
                                            if (File.Exists(
                                                Path.Combine(texPath, "Highway6L_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(texPath, "Highway6L_Tunnel_Segment_APRMap.dds")));
                                            }
                                            else if (File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway6L_Tunnel_Segment_APRMap.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    aprmap,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.APRMaps_Path,
                                                            "Highway6L_Tunnel_Segment_APRMap.dds")));
                                            }
                                        }

                                        segment.m_lodRenderDistance = 2500f;
                                    }
                                }
                            }
                        }
                    }

                    if (!netInfo.m_class.name.Contains("NExt") && !netInfo.m_class.name.Contains("Water")
                        && !netInfo.m_class.name.Contains("Train") && !netInfo.m_class.name.Contains("Metro")
                        && !netInfo.m_class.name.Contains("Transport") && !netInfo.m_class.name.Contains("Bus Line")
                        && !netInfo.m_class.name.Contains("Airplane") && !netInfo.m_class.name.Contains("Ship")
                        && !netInfo.name.Contains("Busway")
                        && (!netInfo.name.Contains("Large Road") || !netInfo.name.Contains("Bus Lane")))
                    {
                        NetInfo.Node[] nodes = netInfo.m_nodes;
                        for (int i = 0; i < nodes.Length; i++)
                        {
                            NetInfo.Node node = nodes[i];
                            if (node.m_nodeMaterial.GetTexture(maintex) != null
                                && !node.m_nodeMaterial.name.Equals(value) && !node.m_nodeMaterial.name.Equals(value2))
                            {
                                string fullPath = Path.Combine(
                                    ModLoader.currentTexturesPath_default,
                                    node.m_nodeMaterial.GetTexture(maintex).name + ".dds");
                                string netInfoName = netInfo.name.Replace(" ", "_").ToLowerInvariant().Trim();

                                if (File.Exists(netInfoName + "_" + fullPath))
                                {
                                    node.m_nodeMaterial.SetTexture(
                                        maintex,
                                        LoadTextureDDS(netInfoName + "_" + fullPath));
                                }

                                string plusName = roadSmall + typeGround + nodeSuffix;

                                if (File.Exists(Path.Combine(pathSmall, plusName + mainTexSuffix)))
                                {
                                    SetPlusNodes(
                                        netInfo,
                                        node,
                                        pathSmall,
                                        pathMedium,
                                        pathLarge,
                                        pathHighway,
                                        out string plusPath,
                                        out plusName);
                                    CheckAndSetNodeMaterial(node, plusPath, plusName);
                                }
                                else
                                {
                                    if (File.Exists(fullPath))
                                    {
                                        node.m_nodeMaterial.SetTexture(maintex, LoadTextureDDS(fullPath));
                                    }

                                    if (node.m_nodeMaterial.GetTexture(aprmap) != null
                                        && !node.m_nodeMaterial.name.Equals(value)
                                        && !node.m_nodeMaterial.name.Equals(value2))
                                    {
                                        string text17 = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            node.m_nodeMaterial.GetTexture(aprmap).name + ".dds");
                                        if (File.Exists(text17))
                                        {
                                            node.m_nodeMaterial.SetTexture(aprmap, LoadTextureDDS(text17));
                                        }
                                    }
                                }
                            }

                            if (node.m_nodeMaterial.GetTexture(aprmap) != null
                                && !node.m_nodeMaterial.name.Contains("rail"))
                            {
                                string text17 = Path.Combine(
                                    texPath,
                                    node.m_nodeMaterial.GetTexture(aprmap).name + ".dds");
                                if (File.Exists(text17))
                                {
                                    node.m_nodeMaterial.SetTexture(aprmap, LoadTextureDDS(text17));
                                }
                            }
                        }

                        NetInfo.Segment[] segments = netInfo.m_segments;
                        for (int j = 0; j < segments.Length; j++)
                        {
                            NetInfo.Segment segment = segments[j];
                            if (segment.m_segmentMaterial.GetTexture(maintex) != null
                                && !segment.m_segmentMaterial.name.Contains("rail")
                                && !segment.m_material.name.ToLower().Contains("cable"))
                            {
                                string fullPath = Path.Combine(
                                    texPath,
                                    segment.m_segmentMaterial.GetTexture(maintex).name + ".dds");

                                string plusPath = string.Empty;
                                string plusName = string.Empty;

                                if (netInfo.name.Contains("Basic Road"))
                                {
                                    plusPath = pathSmall;
                                    plusName = roadSmall;

                                    if (segment.m_segmentMesh.name.Equals("SmallRoadSegment"))
                                    {
                                        plusName += typeGround;
                                        if (File.Exists(Path.Combine(texPath, "RoadSmall_D.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "RoadSmall_D.dds");
                                        }
                                    }
                                    else if (segment.m_segmentMesh.name.Equals("SmallRoadSegmentBusSide"))
                                    {
                                        plusName += text9;
                                        if (File.Exists(Path.Combine(texPath, "RoadSmall_D_BusSide.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "RoadSmall_D_BusSide.dds");
                                        }
                                    }
                                    else if (segment.m_segmentMesh.name.Equals("SmallRoadSegmentBusBoth"))
                                    {
                                        plusName += text10;
                                        if (File.Exists(Path.Combine(texPath, "RoadSmall_D_BusBoth.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "RoadSmall_D_BusBoth.dds");
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

                                if (netInfo.name.Contains("Oneway Road"))
                                {
                                    plusPath = pathSmall;
                                    plusName = roadSmall + roadOneway;

                                    if (segment.m_mesh.name.Equals("SmallRoadSegment"))
                                    {
                                        plusName = roadSmall + roadOneway + typeGround;
                                        if (File.Exists(Path.Combine(texPath, "Oneway_RoadSmallSegment.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "Oneway_RoadSmallSegment.dds");
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                    {
                                        plusName = roadSmall + roadOneway + text9;
                                        if (File.Exists(Path.Combine(texPath, "Oneway_RoadSmallSegment_BusSide.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "Oneway_RoadSmallSegment_BusSide.dds");
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                                    {
                                        plusName = roadSmall + roadOneway + text10;
                                        if (File.Exists(Path.Combine(texPath, "Oneway_RoadSmallSegment_BusBoth.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "Oneway_RoadSmallSegment_BusBoth.dds");
                                        }
                                    }
                                    else if (!segment.m_mesh.name.Equals("SmallRoadSegment"))
                                    {
                                        if (netInfo.name.Equals("Oneway Road Elevated")
                                            || netInfo.name.Equals("Oneway Road Bridge"))
                                        {
                                            plusName = roadSmall + roadOneway + typeElevated;
                                            if (File.Exists(
                                                Path.Combine(texPath, "Oneway_RoadSmallElevatedSegment_D.dds")))
                                            {
                                                fullPath = Path.Combine(
                                                    texPath,
                                                    "Oneway_RoadSmallElevatedSegment_D.dds");
                                            }
                                        }
                                        else if (netInfo.name.Equals("Oneway Road Slope"))
                                        {
                                            plusName = roadSmall + roadOneway + typeSlope;
                                            if (File.Exists(Path.Combine(texPath, "Oneway_small-tunnel_d.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "Oneway_small-tunnel_d.dds");
                                            }
                                        }
                                    }

                                    if (netInfo.name.Equals("Basic Road Bicycle"))
                                    {
                                        if (segment.m_mesh.name.Equals("SmallRoadSegment"))
                                        {
                                            plusName = roadSmall + str2 + typeGround;
                                        }
                                        else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                        {
                                            plusName = roadSmall + str2 + text9;
                                            if (ModLoader.config.basic_road_parking == 1)
                                            {
                                                if (fullPath.Equals(Path.Combine(texPath, "RoadSmall_D_BusSide.dds")))
                                                {
                                                    fullPath = Path.Combine(
                                                        texPath,
                                                        "RoadSmall_D_BusSide_parking1.dds");
                                                }
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                                        {
                                            plusName = roadSmall + str2 + text10;
                                        }
                                    }

                                    if (netInfo.name.Equals("Basic Road Elevated Bike"))
                                    {
                                        plusName = roadSmall + str2 + typeElevated;
                                    }

                                    if (netInfo.name.Equals("Basic Road Decoration Grass")
                                        || netInfo.name.Equals("Basic Road Decoration Trees"))
                                    {
                                        if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                                        {
                                            plusName = roadSmall + text8;
                                        }
                                        else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                                        {
                                            plusName = roadSmall + text8 + text9;
                                            if (File.Exists(Path.Combine(texPath, "SmallRoadSegmentDeco_BusSide.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "SmallRoadSegmentDeco_BusSide.dds");
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                                        {
                                            plusName = roadSmall + text8 + text10;
                                            if (File.Exists(Path.Combine(texPath, "SmallRoadSegmentDeco_BusSide.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "SmallRoadSegmentDeco_BusBoth.dds");
                                            }
                                        }
                                    }

                                    if (netInfo.name.Equals("Oneway Road Decoration Grass")
                                        || netInfo.name.Equals("Oneway Road Decoration Trees"))
                                    {
                                        if (segment.m_mesh.name.Equals("SmallRoadSegment2"))
                                        {
                                            plusName = roadSmall + roadOneway + text8;
                                            if (File.Exists(Path.Combine(texPath, "Oneway_SmallRoadSegmentDeco.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "Oneway_SmallRoadSegmentDeco.dds");
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                                        {
                                            plusName = roadSmall + roadOneway + text8 + text10;
                                            if (File.Exists(
                                                Path.Combine(texPath, "Oneway_SmallRoadSegmentDeco_BusSide.dds")))
                                            {
                                                fullPath = Path.Combine(
                                                    texPath,
                                                    "Oneway_SmallRoadSegmentDeco_BusSide.dds");
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                                        {
                                            plusName = roadSmall + roadOneway + text8 + text10;
                                            if (File.Exists(
                                                Path.Combine(texPath, "Oneway_SmallRoadSegmentDeco_BusBoth.dds")))
                                            {
                                                fullPath = Path.Combine(
                                                    texPath,
                                                    "Oneway_SmallRoadSegmentDeco_BusBoth.dds");
                                            }
                                        }
                                    }

                                    if (netInfo.name.Contains("Medium Road"))
                                    {
                                        plusName = roadMedium;
                                        if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                                        {
                                            plusName += typeGround;
                                        }
                                        else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                        {
                                            plusName += text9;
                                            if (File.Exists(Path.Combine(texPath, "RoadMedium_D_BusSide.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "RoadMedium_D_BusSide.dds");
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                        {
                                            plusName += text10;
                                            if (File.Exists(Path.Combine(texPath, "RoadMedium_D_BusBoth.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "RoadMedium_D_BusBoth.dds");
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
                                    }

                                    if (netInfo.name.Equals("Medium Road Decoration Grass")
                                        || netInfo.name.Equals("Medium Road Decoration Trees"))
                                    {
                                        plusName = roadMedium + text8;
                                        if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                                        {
                                        }
                                        else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                        {
                                            plusName += text9;
                                            if (File.Exists(Path.Combine(texPath, "RoadMediumDeco_d_BusSide.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "RoadMediumDeco_d_BusSide.dds");
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                        {
                                            plusName += text10;
                                            if (File.Exists(Path.Combine(texPath, "RoadMediumDeco_d_BusBoth.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "RoadMediumDeco_d_BusBoth.dds");
                                            }
                                        }
                                    }

                                    if (netInfo.name.Equals("Medium Road Bicycle"))
                                    {
                                        plusName = roadMedium + str2;
                                        if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                                        {
                                            plusName += typeGround;
                                        }
                                        else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                        {
                                            plusName += text9;
                                        }
                                        else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                        {
                                            plusName += text10;
                                        }
                                    }

                                    if (netInfo.name.Equals("Medium Road Elevated Bike")
                                        || netInfo.name.Equals("Medium Road Bridge Bike"))
                                    {
                                        plusName = roadMedium + str2 + typeElevated;
                                    }

                                    if (netInfo.name.Contains("Large Road"))
                                    {
                                        plusName = roadLarge;
                                        if (segment.m_mesh.name.Equals("RoadLargeSegment"))
                                        {
                                            plusName += typeGround;
                                        }
                                        else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                        {
                                            plusName += text9;
                                            if (File.Exists(Path.Combine(texPath, "RoadLargeSegment_d_BusSide.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "RoadLargeSegment_d_BusSide.dds");
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                        {
                                            plusName += text10;
                                            if (File.Exists(Path.Combine(texPath, "RoadLargeSegment_d_BusBoth.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "RoadLargeSegment_d_BusBoth.dds");
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
                                    }

                                    if (netInfo.name.Equals("Large Road Decoration Grass")
                                        || netInfo.name.Equals("Large Road Decoration Trees"))
                                    {
                                        plusName = roadLarge + text8;
                                        if (segment.m_mesh.name.Equals("LargeRoadSegment2"))
                                        {
                                        }
                                        else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusSide"))
                                        {
                                            // bug? no plusName defined
                                            plusName += text9;
                                            if (File.Exists(
                                                Path.Combine(texPath, "RoadLargeOnewaySegment_d_BusSide.dds")))
                                            {
                                                fullPath = Path.Combine(
                                                    texPath,
                                                    "RoadLargeOnewaySegment_d_BusSide.dds");
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth"))
                                        {
                                            plusName += text10;
                                            if (File.Exists(Path.Combine(texPath, "RoadLargeSegmentDecoBusBoth_d.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "RoadLargeSegmentDecoBusBoth_d.dds");
                                            }
                                        }
                                    }

                                    if (netInfo.name.Equals("Large Road Bicycle"))
                                    {
                                        plusName = roadLarge + str2;
                                        if (segment.m_mesh.name.Equals("RoadLargeSegment"))
                                        {
                                            plusName += typeGround;
                                        }
                                        else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                        {
                                            plusName += text9;
                                        }
                                        else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                        {
                                            plusName += text10;
                                        }
                                    }

                                    if (netInfo.name.Equals("Large Road Elevated Bike")
                                        || netInfo.name.Equals("Large Road Bridge Bike"))
                                    {
                                        plusName = roadLarge + str2 + typeElevated;
                                    }

                                    if (netInfo.name.Contains("Large Oneway"))
                                    {
                                        plusName = roadLarge + roadOneway;
                                        if (segment.m_mesh.name.Equals("RoadLargeSegment"))
                                        {
                                            plusName += typeGround;
                                        }
                                        else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                        {
                                            plusName += text9;
                                            if (File.Exists(
                                                Path.Combine(texPath, "RoadLargeOnewaySegment_d_BusSide.dds")))
                                            {
                                                fullPath = Path.Combine(
                                                    texPath,
                                                    "RoadLargeOnewaySegment_d_BusSide.dds");
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                        {
                                            plusName += text10;
                                            if (File.Exists(
                                                Path.Combine(texPath, "RoadLargeOnewaySegment_d_BusBoth.dds")))
                                            {
                                                fullPath = Path.Combine(
                                                    texPath,
                                                    "RoadLargeOnewaySegment_d_BusBoth.dds");
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
                                        plusName = roadLarge + roadOneway + text8;
                                        if (segment.m_mesh.name.Equals("LargeRoadSegment2"))
                                        {
                                        }
                                        else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusSide"))
                                        {
                                            plusName += text9;
                                            if (File.Exists(Path.Combine(texPath, "RoadLargeSegmentDecoBusSide_d.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "RoadLargeSegmentDecoBusSide_d.dds");
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth"))
                                        {
                                            plusName += text10;
                                            if (File.Exists(Path.Combine(texPath, "RoadLargeSegmentDecoBusBoth_d.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "RoadLargeSegmentDecoBusBoth_d.dds");
                                            }
                                        }
                                    }
                                    else if (netInfo.name.Contains("Highway"))
                                    {
                                        if (netInfo.name.Equals("HighwayRamp")
                                            || netInfo.name.Equals("HighwayRampElevated"))
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
                                    }

                                    if (netInfo.name.Equals("Basic Road Tram"))
                                    {
                                        if (segment.m_mesh.name.Equals("RoadSmallTramStopSingle"))
                                        {
                                            plusName = roadSmall + text10;
                                        }
                                        else if (segment.m_mesh.name.Equals("RoadSmallTramStopDouble"))
                                        {
                                            plusName = roadSmall + text10;
                                        }
                                        else if (segment.m_mesh.name.Equals("RoadSmallTramAndBusStop"))
                                        {
                                            plusName = roadSmall + text10;
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
                                        if (segment.m_mesh.name.Equals("RoadSmallTramStopSingle"))
                                        {
                                            plusName = roadSmall + roadOneway + text10;
                                        }
                                        else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                        {
                                            plusName = roadSmall + roadOneway + text10;
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
                                    else if (netInfo.name.Equals("Medium Road Tram"))
                                    {
                                        if (segment.m_mesh.name.Equals("RoadMediumTramSegment"))
                                        {
                                            plusName = roadMedium + str3 + typeGround;
                                        }
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
                                    else if (netInfo.name.Equals("Tram Track"))
                                    {
                                        plusName = roadSmall + text8;
                                    }
                                    else if (netInfo.name.Equals("Tram Track Elevated"))
                                    {
                                        plusName = roadSmall + roadOneway + typeElevated;
                                    }
                                    else if (netInfo.name.Equals("Tram Track Slope"))
                                    {
                                        plusName = roadSmall + roadOneway + typeSlope;
                                    }
                                    else if (netInfo.name.Equals("Oneway Tram Track"))
                                    {
                                        plusName = roadSmall + text8;
                                    }
                                    else if (netInfo.name.Equals("Oneway Tram Track Elevated"))
                                    {
                                        plusName = roadSmall + roadOneway + typeElevated;
                                    }
                                    else if (netInfo.name.Equals("Oneway Tram Track Slope"))
                                    {
                                        plusName = roadSmall + roadOneway + typeSlope;
                                    }
                                    else if (netInfo.name.Equals("Medium Road Bus"))
                                    {
                                        if (segment.m_mesh.name.Equals("RoadMediumSegment"))
                                        {
                                            plusName = roadMedium + plusBus + typeGround;
                                        }
                                        else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                        {
                                            plusName = roadMedium + plusBus + text9;
                                            if (File.Exists(Path.Combine(texPath, "RoadMediumBusLane_BusSide.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "RoadMediumBusLane_BusSide.dds");
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                        {
                                            plusName = roadMedium + plusBus + text10;
                                            if (File.Exists(Path.Combine(texPath, "RoadMediumBusLane_BusBoth.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "RoadMediumBusLane_BusBoth.dds");
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
                                                plusName = roadLarge + plusBus + typeGround;
                                            }
                                            else if (segment.m_mesh.name.Equals("RoadLargeSegmentBusSideBusLane"))
                                            {
                                                plusName = roadLarge + plusBus + text9;
                                                if (File.Exists(
                                                    Path.Combine(texPath, "RoadLargeBuslane_D_BusSide.dds")))
                                                {
                                                    fullPath = Path.Combine(texPath, "RoadLargeBuslane_D_BusSide.dds");
                                                }
                                            }
                                            else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBothBusLane"))
                                            {
                                                plusName = roadLarge + plusBus + text10;
                                                if (File.Exists(
                                                    Path.Combine(texPath, "RoadLargeBuslane_D_BusBoth.dds")))
                                                {
                                                    fullPath = Path.Combine(texPath, "RoadLargeBuslane_D_BusBoth.dds");
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

                                    // Debug.Log(text18);
                                    if (ModLoader.config.basic_road_parking == 1)
                                    {
                                        plusName = roadSmall + typeGround;
                                        if (fullPath.Equals(Path.Combine(texPath, "RoadSmall_D.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "RoadSmall_D_parking1.dds");
                                        }

                                        if (fullPath.Equals(Path.Combine(texPath, "RoadSmall_D_BusSide.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "RoadSmall_D_BusSide_parking1.dds");
                                        }
                                    }

                                    if (ModLoader.config.medium_road_parking == 1 && !netInfo.name.Contains("Grass")
                                        && !netInfo.name.Contains("Trees"))
                                    {
                                        if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                        {
                                            if (fullPath.Equals(Path.Combine(texPath, "RoadMediumSegment_d.dds"))
                                                && File.Exists(
                                                    Path.Combine(texPath, "RoadMedium_D_BusSide_parking1.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "RoadMedium_D_BusSide_parking1.dds");
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                        {
                                            if (fullPath.Equals(Path.Combine(texPath, "RoadMediumSegment_d.dds"))
                                                && File.Exists(
                                                    Path.Combine(texPath, "RoadMedium_D_BusBoth_parking1.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "RoadMedium_D_BusBoth_parking1.dds");
                                            }
                                        }
                                        else if (fullPath.Equals(Path.Combine(texPath, "RoadMediumSegment_d.dds"))
                                                 && File.Exists(Path.Combine(texPath, "RoadMedium_D_parking1.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "RoadMedium_D_parking1.dds");
                                        }
                                        else if (fullPath.Equals(Path.Combine(texPath, "RoadMedium_D.dds"))
                                                 && File.Exists(Path.Combine(texPath, "RoadMedium_D_parking1.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "RoadMedium_D_parking1.dds");
                                        }
                                    }

                                    if (ModLoader.config.medium_road_grass_parking == 1
                                        && netInfo.name.Contains("Grass"))
                                    {
                                        if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                        {
                                            if (fullPath.Equals(Path.Combine(texPath, "RoadMedium_D.dds"))
                                                && File.Exists(
                                                    Path.Combine(texPath, "RoadMedium_D_BusSide_parking1.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "RoadMedium_D_BusSide_parking1.dds");
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                        {
                                            if (fullPath.Equals(Path.Combine(texPath, "RoadMedium_D.dds"))
                                                && File.Exists(
                                                    Path.Combine(texPath, "RoadMedium_D_BusBoth_parking1.dds")))
                                            {
                                                fullPath = Path.Combine(texPath, "RoadMedium_D_BusBoth_parking1.dds");
                                            }
                                        }
                                        else if (fullPath.Equals(Path.Combine(texPath, "RoadMediumSegment_d.dds"))
                                                 && File.Exists(Path.Combine(texPath, "RoadMedium_D_parking1.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "RoadMedium_D_parking1.dds");
                                        }
                                        else if (fullPath.Equals(Path.Combine(texPath, "RoadMedium_D.dds"))
                                                 && File.Exists(Path.Combine(texPath, "RoadMedium_D_parking1.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "RoadMedium_D_parking1.dds");
                                        }
                                    }

                                    if (ModLoader.config.medium_road_trees_parking == 1
                                        && netInfo.name.Contains("Trees"))
                                    {
                                        if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                        {
                                            if (fullPath.Equals(Path.Combine(texPath, "RoadMediumDeco_d.dds"))
                                                && File.Exists(
                                                    Path.Combine(texPath, "RoadMediumDeco_d_BusSide_parking1.dds")))
                                            {
                                                fullPath = Path.Combine(
                                                    texPath,
                                                    "RoadMediumDeco_d_BusSide_parking1.dds");
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth")
                                                 && File.Exists(Path.Combine(texPath, "RoadMediumDeco_d.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "RoadMediumDeco_d.dds");
                                        }
                                        else if (fullPath.Equals(Path.Combine(texPath, "RoadMediumDeco_d.dds"))
                                                 && File.Exists(Path.Combine(texPath, "RoadMediumDeco_d_parking1.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "RoadMediumDeco_d_parking1.dds");
                                        }
                                    }

                                    if (ModLoader.config.medium_road_bus_parking == 1)
                                    {
                                        if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                        {
                                            if (fullPath.Equals(Path.Combine(texPath, "RoadMediumBusLane.dds"))
                                                && File.Exists(
                                                    Path.Combine(texPath, "RoadMediumBusLane_BusSide_parking1.dds")))
                                            {
                                                fullPath = Path.Combine(
                                                    texPath,
                                                    "RoadMediumBusLane_BusSide_parking1.dds");
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                        {
                                            if (fullPath.Equals(Path.Combine(texPath, "RoadMediumBusLane.dds"))
                                                && File.Exists(
                                                    Path.Combine(texPath, "RoadMediumBusLane_BusBoth_parking1.dds")))
                                            {
                                                fullPath = Path.Combine(
                                                    texPath,
                                                    "RoadMediumBusLane_BusBoth_parking1.dds");
                                            }
                                        }
                                        else if (fullPath.Equals(Path.Combine(texPath, "RoadMediumBusLane.dds"))
                                                 && File.Exists(
                                                     Path.Combine(texPath, "RoadMediumBusLane_parking1.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "RoadMediumBusLane_parking1.dds");
                                        }
                                    }

                                    if (ModLoader.config.large_road_parking == 1)
                                    {
                                        if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                        {
                                            if (fullPath.Equals(Path.Combine(texPath, "RoadLargeSegment_d.dds"))
                                                && File.Exists(
                                                    Path.Combine(texPath, "RoadLargeSegment_d_BusSide_parking1.dds")))
                                            {
                                                fullPath = Path.Combine(
                                                    texPath,
                                                    "RoadLargeSegment_d_BusSide_parking1.dds");
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                        {
                                            if (fullPath.Equals(Path.Combine(texPath, "RoadLargeSegment_d.dds"))
                                                && File.Exists(
                                                    Path.Combine(texPath, "RoadLargeSegment_d_BusBoth_parking1.dds")))
                                            {
                                                fullPath = Path.Combine(
                                                    texPath,
                                                    "RoadLargeSegment_d_BusBoth_parking1.dds");
                                            }
                                        }
                                        else if (fullPath.Equals(Path.Combine(texPath, "RoadLargeSegment_d.dds"))
                                                 && File.Exists(
                                                     Path.Combine(texPath, "RoadLargeSegment_d_parking1.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "RoadLargeSegment_d_parking1.dds");
                                        }
                                    }

                                    if (ModLoader.config.large_oneway_parking == 1)
                                    {
                                        if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                        {
                                            if (fullPath.Equals(Path.Combine(texPath, "RoadLargeOnewaySegment_d.dds"))
                                                && File.Exists(
                                                    Path.Combine(
                                                        texPath,
                                                        "RoadLargeOnewaySegment_d_BusSide_parking1.dds")))
                                            {
                                                fullPath = Path.Combine(
                                                    texPath,
                                                    "RoadLargeOnewaySegment_d_BusSide_parking1.dds");
                                            }
                                        }
                                        else if (fullPath.Equals(Path.Combine(texPath, "RoadLargeOnewaySegment_d.dds"))
                                                 && File.Exists(
                                                     Path.Combine(texPath, "RoadLargeOnewaySegment_d_parking1.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "RoadLargeOnewaySegment_d_parking1.dds");
                                        }
                                    }

                                    if (ModLoader.config.large_road_bus_parking == 1)
                                    {
                                        if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSideBusLane"))
                                        {
                                            if (fullPath.Equals(Path.Combine(texPath, "RoadLargeBuslane_D.dds"))
                                                && File.Exists(
                                                    Path.Combine(texPath, "RoadLargeBuslane_D_BusSide_parking1.dds")))
                                            {
                                                fullPath = Path.Combine(
                                                    texPath,
                                                    "RoadLargeBuslane_D_BusSide_parking1.dds");
                                            }
                                        }
                                        else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBothBusLane"))
                                        {
                                            if (fullPath.Equals(Path.Combine(texPath, "RoadLargeBuslane_D.dds"))
                                                && File.Exists(
                                                    Path.Combine(texPath, "RoadLargeBuslane_D_BusBoth_parking1.dds")))
                                            {
                                                fullPath = Path.Combine(
                                                    texPath,
                                                    "RoadLargeBuslane_D_BusBoth_parking1.dds");
                                            }
                                        }
                                        else if (fullPath.Equals(Path.Combine(texPath, "RoadLargeBuslane_D.dds"))
                                                 && File.Exists(
                                                     Path.Combine(texPath, "RoadLargeBuslane_D_parking1.dds")))
                                        {
                                            fullPath = Path.Combine(texPath, "RoadLargeBuslane_D_parking1.dds");
                                        }
                                    }

                                    if (File.Exists(fullPath))
                                    {
                                        segment.m_segmentMaterial.SetTexture(maintex, LoadTextureDDS(fullPath));
                                    }
                                }

                                // Replace the default segment textures
                                if (File.Exists(fullPath))
                                {
                                    segment.m_segmentMaterial.SetTexture(TexType._MainTex, LoadTextureDDS(fullPath));
                                }

                                CheckAndSetSegmentMaterial(segment, plusPath, plusName);

                                if (segment.m_segmentMaterial.GetTexture(aprmap) != null)
                                {
                                    string text19 = Path.Combine(
                                        texPath,
                                        segment.m_segmentMaterial.GetTexture(aprmap).name + ".dds");

                                    // Debug.Log(text19);
                                    if (segment.m_segmentMaterial.GetTexture(aprmap).name
                                            .Equals("LargeRoadSegmentBusSide-BikeLane-apr") || segment.m_segmentMaterial
                                            .GetTexture(aprmap).name.Equals("LargeRoadSegmentBusBoth-BikeLane-apr"))
                                    {
                                        if (File.Exists(Path.Combine(texPath, "RoadLargeSegment-BikeLane-apr.dds")))
                                        {
                                            text19 = Path.Combine(texPath, "RoadLargeSegment-BikeLane-apr.dds");
                                        }
                                        else if (File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "RoadLargeSegment-BikeLane-apr.dds")))
                                        {
                                            text19 = Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "RoadLargeSegment-BikeLane-apr.dds");
                                        }
                                    }

                                    if (segment.m_segmentMaterial.GetTexture(aprmap).name
                                            .Equals("LargeRoadSegmentBusSide-LargeRoadSegmentBusSide-apr") || segment
                                            .m_segmentMaterial.GetTexture(aprmap).name
                                            .Equals("LargeRoadSegmentBusBoth-LargeRoadSegmentBusBoth-apr"))
                                    {
                                        if (File.Exists(Path.Combine(texPath, "RoadLargeSegment-default-apr.dds")))
                                        {
                                            text19 = Path.Combine(texPath, "RoadLargeSegment-default-apr.dds");
                                        }
                                        else if (File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "RoadLargeSegment-default-apr.dds")))
                                        {
                                            text19 = Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "RoadLargeSegment-default-apr.dds");
                                        }
                                    }

                                    if (File.Exists(text19))
                                    {
                                        segment.m_segmentMaterial.SetTexture(aprmap, LoadTextureDDS(text19));
                                    }
                                }
                            }
                        }
                    }

                    num += 1u;
                }
            }
        }

        private static bool CheckAndSetNodeMaterial(NetInfo.Node node, [CanBeNull] string path, [NotNull] string name)
        {
            bool main = false;
            if (path.IsNullOrWhiteSpace() || name.IsNullOrWhiteSpace())
            {
                return main;
            }

            if (File.Exists(Path.Combine(path, name + nodeSuffix + mainTexSuffix)))
            {
                Debug.Log(path + " - " + name + nodeSuffix + mainTexSuffix);
                node.m_nodeMaterial.SetTexture(
                    maintex,
                    LoadTextureDDS(Path.Combine(path, name + nodeSuffix + mainTexSuffix)));
                main = true;
            }

            if (File.Exists(Path.Combine(path, name + nodeSuffix + aprMapSuffix)))
            {
                node.m_nodeMaterial.SetTexture(
                    aprmap,
                    LoadTextureDDS(Path.Combine(path, name + nodeSuffix + aprMapSuffix)));
            }

            return main;
        }

        private static bool CheckAndSetSegmentMaterial(NetInfo.Segment segment, [CanBeNull] string path, string name)
        {
            bool main = false;
            if (path.IsNullOrWhiteSpace())
            {
                return main;
            }

            if (File.Exists(Path.Combine(path, name + segmentSuffix + mainTexSuffix)))
            {
                Debug.Log(path + " - " + name + segmentSuffix + mainTexSuffix);

                segment.m_segmentMaterial.SetTexture(
                    maintex,
                    LoadTextureDDS(Path.Combine(path, name + segmentSuffix + mainTexSuffix)));
                main = true;
            }

            if (File.Exists(Path.Combine(path, name + segmentSuffix + aprMapSuffix)))
            {
                segment.m_segmentMaterial.SetTexture(
                    aprmap,
                    LoadTextureDDS(Path.Combine(path, name + segmentSuffix + aprMapSuffix)));
            }

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
            NetInfo netInfo,
            NetInfo.Node node,
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
                    plusName = roadSmall + typeElevated + nodeSuffix;
                }
                else if (netInfo.name.Equals("Basic Road Decoration Grass")
                         || netInfo.name.Equals("Basic Road Decoration Trees"))
                {
                    plusName = roadSmall + text8 + nodeSuffix;
                }
                else
                {
                    plusName = roadSmall + typeGround + nodeSuffix;
                }
            }
            else if (netInfo.name.Contains("Oneway Road"))
            {
                plusName = roadSmall + roadOneway;
                if (netInfo.name.Equals("Oneway Road Elevated"))
                {
                    plusName = plusName + typeElevated + nodeSuffix;
                }
                else if (netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees"))
                {
                    plusName = roadSmall + roadOneway + text8 + nodeSuffix;
                }
                else
                {
                    plusName = plusName + typeGround + nodeSuffix;
                }
            }
            else if (netInfo.name.Equals("Basic Road Bicycle"))
            {
                plusName = roadSmall + typeGround + nodeSuffix;
            }
            else if (netInfo.name.Equals("Basic Road Elevated Bike"))
            {
                plusName = roadSmall + str2 + typeElevated + nodeSuffix;
            }
            else if (netInfo.name.Contains("Medium Road") && !netInfo.name.Contains("Tram"))
            {
                plusPath = pathMedium;
                plusName = roadMedium;
                if (netInfo.name.Contains("Medium Road Elevated") && !netInfo.name.Contains("Bike"))
                {
                    plusName = plusName + typeElevated + nodeSuffix;
                }
                else if (netInfo.name.Equals("Medium Road Decoration Grass")
                         || netInfo.name.Equals("Medium Road Decoration Trees"))
                {
                    plusName = roadMedium + text8 + nodeSuffix;
                }
                else
                {
                    plusName = plusName + typeGround + nodeSuffix;
                }
            }
            else if (netInfo.name.Equals("Medium Road Elevated Bike"))
            {
                plusPath = pathMedium;
                plusName = roadMedium + str2 + typeElevated + nodeSuffix;
            }
            else if (netInfo.name.Contains("Large Road"))
            {
                plusPath = pathLarge;
                plusName = roadLarge;
                if (netInfo.name.Equals("Large Road Elevated"))
                {
                    plusName = plusName + typeElevated + nodeSuffix;
                }
                else if (netInfo.name.Equals("Large Road Decoration Grass")
                         || netInfo.name.Equals("Large Road Decoration Trees"))
                {
                    plusName = roadLarge + text8 + nodeSuffix;
                }
                else
                {
                    plusName = plusName + typeGround + nodeSuffix;
                }
            }
            else if (netInfo.name.Equals("Large Road Elevated Bike"))
            {
                plusPath = pathLarge;
                plusName = roadLarge + str2 + typeElevated + nodeSuffix;
            }
            else if (netInfo.name.Contains("Large Oneway"))
            {
                plusPath = pathLarge;
                plusName = roadLarge + roadOneway;
                if (netInfo.name.Equals("Large Oneway Elevated"))
                {
                    plusName = plusName + typeElevated + nodeSuffix;
                }
                else if (netInfo.name.Equals("Large Oneway Decoration Grass")
                         || netInfo.name.Equals("Large Oneway Decoration Trees"))
                {
                    plusName = roadLarge + roadOneway + text8 + nodeSuffix;
                }
                else if (netInfo.name.Equals("Large Oneway Road Slope"))
                {
                    plusName = roadLarge + roadOneway + typeSlope + nodeSuffix;
                }
                else
                {
                    plusName = plusName + typeGround + nodeSuffix;
                }
            }
            else if (netInfo.name.Contains("Highway"))
            {
                plusPath = pathHighway;
                plusName = roadHighway + "3L" + nodeSuffix;

                if (netInfo.name.Equals("HighwayRamp"))
                {
                    plusName = roadHighway + "Ramp" + nodeSuffix;
                }
                else if (netInfo.name.Equals("HighwayRampElevated"))
                {
                    plusName = roadHighway + "Ramp" + nodeSuffix;
                }
                else if (netInfo.name.Equals("HighwayRamp Slope"))
                {
                    plusName = roadHighway + "Ramp" + nodeSuffix;
                }
                else if (netInfo.name.Equals("Highway Elevated"))
                {
                    plusName = roadHighway + "3L" + nodeSuffix;
                }
                else if (netInfo.name.Equals("Highway Slope"))
                {
                    plusName = roadHighway + "3L" + nodeSuffix;
                }
                else if (netInfo.name.Equals("Highway Barrier"))
                {
                    plusName = roadHighway + "3L" + nodeSuffix;
                }
            }
            else if (netInfo.name.Equals("Basic Road Tram"))
            {
                plusName = roadSmall + typeGround + nodeSuffix;
            }
            else if (netInfo.name.Equals("Basic Road Elevated Tram"))
            {
                plusName = roadSmall + typeElevated + nodeSuffix;
            }
            else if (netInfo.name.Equals("Basic Road Slope Tram"))
            {
                plusName = roadSmall + typeGround + nodeSuffix;
            }
            else if (netInfo.name.Equals("Oneway Road Tram"))
            {
                plusName = roadSmall + roadOneway + typeGround + nodeSuffix;
            }
            else if (netInfo.name.Equals("Oneway Road Elevated Tram"))
            {
                plusName = roadSmall + roadOneway + typeElevated + nodeSuffix;
            }
            else if (netInfo.name.Equals("Oneway Road Slope Tram"))
            {
                plusName = roadSmall + roadOneway + typeGround + nodeSuffix;
            }
            else if (netInfo.name.Equals("Medium Road Tram"))
            {
                plusPath = pathMedium;
                plusName = roadMedium + str3 + typeGround + nodeSuffix;
            }
            else if (netInfo.name.Equals("Medium Road Elevated Tram"))
            {
                plusPath = pathMedium;
                plusName = roadMedium + str3 + typeElevated + nodeSuffix;
            }
            else if (netInfo.name.Equals("Medium Road Slope Tram"))
            {
                plusPath = pathMedium;
                plusName = roadMedium + str3 + typeGround + nodeSuffix;
            }
            else if (netInfo.name.Equals("Tram Track"))
            {
                plusName = roadSmall + text8 + nodeSuffix;
            }
            else if (netInfo.name.Equals("Tram Track Elevated"))
            {
                plusName = roadSmall + typeElevated + nodeSuffix;
            }
            else if (netInfo.name.Equals("Tram Track Slope"))
            {
                plusName = roadSmall + text8 + nodeSuffix;
            }
            else if (netInfo.name.Equals("Oneway Tram Track"))
            {
                plusName = roadSmall + text8 + nodeSuffix;
            }
            else if (netInfo.name.Equals("Oneway Tram Track Elevated"))
            {
                plusName = roadSmall + typeElevated + nodeSuffix;
            }
            else if (netInfo.name.Equals("Oneway Tram Track Slope"))
            {
                plusName = roadSmall + text8 + nodeSuffix;
            }
        }

        private static bool SetSegmenTextures(
            NetInfo.Segment segment,
            [CanBeNull] string coreName,
            string plusPath,
            string plusName)
        {
            bool replaced = false;
            if (!CheckAndSetSegmentMaterial(segment, ModLoader.currentTexturesPath_default, coreName))
            {
                if (CheckAndSetSegmentMaterial(segment, plusPath, plusName))
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

        private struct RoadType
        {
        }
    }
}