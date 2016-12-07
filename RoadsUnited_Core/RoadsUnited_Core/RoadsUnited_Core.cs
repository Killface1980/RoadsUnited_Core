namespace RoadsUnited_Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using UnityEngine;

    public class RoadsUnited_Core : MonoBehaviour
    {
        public static Configuration config;

        public static Dictionary<string, Texture2D> vanillaPrefabProperties = new Dictionary<string, Texture2D>();

        private static Texture2D acimap;

        private static Texture2D aprmap;

        private static Texture2D defaultmap;

        private static Dictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D>();

        public static void ApplyVanillaDictionary()
        {
            for (uint i = 0; i < PrefabCollection<NetInfo>.LoadedCount(); i++)
            {
                var netInfo = PrefabCollection<NetInfo>.GetLoaded(i);
                if (netInfo == null) continue;
                string prefab_road_name = netInfo.name.Replace(" ", "_").ToLowerInvariant().Trim();
                NetInfo.Node[] nodes = netInfo.m_nodes;
                for (int k = 0; k < nodes.Length; k++)
                {
                    NetInfo.Node node = nodes[k];
                    if (!(

                             // netInfo.m_class.name.Contains("NExt") ||
                             netInfo.m_class.name.Contains("Heating Pipe") || netInfo.m_class.name.Contains("Water")
                             || netInfo.m_class.name.Contains("Train") || netInfo.m_class.name.Contains("Metro")
                             || netInfo.m_class.name.Contains("Transport") || netInfo.m_class.name.Contains("Bus Line")
                             || netInfo.m_class.name.Contains("Airplane") || netInfo.m_class.name.Contains("Ship")))
                    {
                        if (node.m_nodeMaterial != null)
                        {
                            if (
                                vanillaPrefabProperties.TryGetValue(
                                    prefab_road_name + "_node_" + k + "_nodeMaterial" + "_MainTex",
                                    out defaultmap)) node.m_nodeMaterial.SetTexture("_MainTex", defaultmap);
                            if (
                                vanillaPrefabProperties.TryGetValue(
                                    prefab_road_name + "_node_" + k + "_nodeMaterial" + "_APRMap",
                                    out aprmap)) node.m_nodeMaterial.SetTexture("_APRMap", aprmap);
                        }
                    }
                }

                NetInfo.Segment[] segments = netInfo.m_segments;
                for (int l = 0; l < segments.Length; l++)
                {
                    NetInfo.Segment segment = segments[l];
                    if (!(

                             // netInfo.m_class.name.Contains("NExt") ||
                             netInfo.m_class.name.Contains("Heating Pipe") || netInfo.m_class.name.Contains("Water")
                             || netInfo.m_class.name.Contains("Train") || netInfo.m_class.name.Contains("Metro")
                             || netInfo.m_class.name.Contains("Transport") || netInfo.m_class.name.Contains("Bus Line")
                             || netInfo.m_class.name.Contains("Airplane") || netInfo.m_class.name.Contains("Ship")))
                    {
                        if (segment.m_segmentMaterial != null)
                        {
                            if (
                                vanillaPrefabProperties.TryGetValue(
                                    prefab_road_name + "_segment_" + l + "_segmentMaterial" + "_MainTex",
                                    out defaultmap)) segment.m_segmentMaterial.SetTexture("_MainTex", defaultmap);
                            if (
                                vanillaPrefabProperties.TryGetValue(
                                    prefab_road_name + "_segment_" + l + "_segmentMaterial" + "_APRMap",
                                    out aprmap)) segment.m_segmentMaterial.SetTexture("_APRMap", aprmap);
                        }
                    }
                }
            }

            PropCollection[] array = FindObjectsOfType<PropCollection>();
            for (int i = 0; i < array.Length; i++)
            {
                PropCollection propCollection = array[i];
                try
                {
                    PropInfo[] prefabs = propCollection.m_prefabs;
                    for (int j = 0; j < prefabs.Length; j++)
                    {
                        PropInfo propInfo = prefabs[j];
                        string str = propInfo.name;

                        // if (propInfo.m_lodMaterialCombined != null)
                        if (propInfo.m_lodMaterialCombined.GetTexture("_MainTex").name != null)
                        {
                            if (vanillaPrefabProperties.TryGetValue(str + "_prop_" + "_MainTex", out defaultmap)) propInfo.m_lodMaterialCombined.SetTexture("_MainTex", defaultmap);
                            if (vanillaPrefabProperties.TryGetValue(str + "_prop_" + "_ACIMap", out acimap)) propInfo.m_lodMaterialCombined.SetTexture("_ACIMap", acimap);
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
            for (uint i = 0; i < PrefabCollection<NetInfo>.LoadedCount(); i++)
            {
                var netInfo = PrefabCollection<NetInfo>.GetLoaded(i);

                if (netInfo == null) continue;
                string prefab_road_name = netInfo.name.Replace(" ", "_").ToLowerInvariant().Trim();
                NetInfo.Node[] nodes = netInfo.m_nodes;
                for (int k = 0; k < nodes.Length; k++)
                {
                    NetInfo.Node node = nodes[k];
                    if (!(

                             // netInfo.m_class.name.Contains("NExt") ||
                             netInfo.m_class.name.Contains("Heating Pipe") || netInfo.m_class.name.Contains("Water")
                             || netInfo.m_class.name.Contains("Train") || netInfo.m_class.name.Contains("Metro")
                             || netInfo.m_class.name.Contains("Transport") || netInfo.m_class.name.Contains("Bus Line")
                             || netInfo.m_class.name.Contains("Airplane") || netInfo.m_class.name.Contains("Ship")))
                    {
                        if (node.m_nodeMaterial != null)
                        {
                            if (
                                vanillaPrefabProperties.TryGetValue(
                                    prefab_road_name + "_node_" + k + "_nodeMaterial" + "_MainTex",
                                    out defaultmap)) node.m_nodeMaterial.SetTexture("_MainTex", defaultmap);
                            if (
                                vanillaPrefabProperties.TryGetValue(
                                    prefab_road_name + "_node_" + k + "_nodeMaterial" + "_APRMap",
                                    out aprmap)) node.m_nodeMaterial.SetTexture("_APRMap", aprmap);
                        }
                    }
                }

                NetInfo.Segment[] segments = netInfo.m_segments;
                for (int l = 0; l < segments.Length; l++)
                {
                    NetInfo.Segment segment = segments[l];
                    if (!(

                             // netInfo.m_class.name.Contains("NExt") ||
                             netInfo.m_class.name.Contains("Heating Pipe") || netInfo.m_class.name.Contains("Water")
                             || netInfo.m_class.name.Contains("Train") || netInfo.m_class.name.Contains("Metro")
                             || netInfo.m_class.name.Contains("Transport") || netInfo.m_class.name.Contains("Bus Line")
                             || netInfo.m_class.name.Contains("Airplane") || netInfo.m_class.name.Contains("Ship")))
                    {
                        if (segment.m_segmentMaterial != null)
                        {
                            if (
                                vanillaPrefabProperties.TryGetValue(
                                    prefab_road_name + "_segment_" + l + "_segmentMaterial" + "_MainTex",
                                    out defaultmap)) segment.m_segmentMaterial.SetTexture("_MainTex", defaultmap);
                            if (
                                vanillaPrefabProperties.TryGetValue(
                                    prefab_road_name + "_segment_" + l + "_segmentMaterial" + "_APRMap",
                                    out aprmap)) segment.m_segmentMaterial.SetTexture("_APRMap", aprmap);
                        }
                    }
                }
            }
        }

        public static void CreateVanillaDictionary()
        {
            for (uint i = 0; i < PrefabCollection<NetInfo>.LoadedCount(); i++)
            {
                var netInfo = PrefabCollection<NetInfo>.GetLoaded(i);
                if (netInfo == null) continue;
                string prefab_road_name = netInfo.name.Replace(" ", "_").ToLowerInvariant().Trim();
                NetInfo.Node[] nodes = netInfo.m_nodes;
                for (int k = 0; k < nodes.Length; k++)
                {
                    NetInfo.Node node = nodes[k];
                    if (
                        !(netInfo.m_class.name.Contains("Heating Pipe") || netInfo.m_class.name.Contains("Water")
                          || netInfo.m_class.name.Contains("Train") || netInfo.m_class.name.Contains("Metro")
                          || netInfo.m_class.name.Contains("Transport") || netInfo.m_class.name.Contains("Bus Line")
                          || netInfo.m_class.name.Contains("Airplane") || netInfo.m_class.name.Contains("Ship")))
                    {
                        if (node.m_nodeMaterial != null)
                        {
                            vanillaPrefabProperties.Add(
                                prefab_road_name + "_node_" + k + "_nodeMaterial" + "_MainTex",
                                node.m_nodeMaterial.GetTexture("_MainTex") as Texture2D);
                            vanillaPrefabProperties.Add(
                                prefab_road_name + "_node_" + k + "_nodeMaterial" + "_APRMap",
                                node.m_nodeMaterial.GetTexture("_APRMap") as Texture2D);
                        }
                    }
                }

                NetInfo.Segment[] segments = netInfo.m_segments;
                for (int l = 0; l < segments.Length; l++)
                {
                    NetInfo.Segment segment = segments[l];
                    if (!(

                             // netInfo.m_class.name.Contains("NExt") ||
                             netInfo.m_class.name.Contains("Heating Pipe") || netInfo.m_class.name.Contains("Water")
                             || netInfo.m_class.name.Contains("Train") || netInfo.m_class.name.Contains("Metro")
                             || netInfo.m_class.name.Contains("Transport") || netInfo.m_class.name.Contains("Bus Line")
                             || netInfo.m_class.name.Contains("Airplane") || netInfo.m_class.name.Contains("Ship")))
                    {
                        if (segment.m_segmentMaterial != null)
                        {
                            vanillaPrefabProperties.Add(
                                prefab_road_name + "_segment_" + l + "_segmentMaterial" + "_MainTex",
                                segment.m_segmentMaterial.GetTexture("_MainTex") as Texture2D);
                            vanillaPrefabProperties.Add(
                                prefab_road_name + "_segment_" + l + "_segmentMaterial" + "_APRMap",
                                segment.m_segmentMaterial.GetTexture("_APRMap") as Texture2D);
                        }
                    }
                }
            }

            PropCollection[] array = FindObjectsOfType<PropCollection>();
            for (int i = 0; i < array.Length; i++)
            {
                PropCollection propCollection = array[i];
                try
                {
                    PropInfo[] prefabs = propCollection.m_prefabs;
                    for (int j = 0; j < prefabs.Length; j++)
                    {
                        PropInfo propInfo = prefabs[j];
                        string str = propInfo.name;

                        // if (propInfo.m_lodMaterialCombined != null)
                        if (propInfo.m_lodMaterialCombined.GetTexture("_MainTex").name != null)
                        {
                            vanillaPrefabProperties.Add(
                                str + "_prop_" + "_MainTex",
                                propInfo.m_lodMaterialCombined.GetTexture("_MainTex") as Texture2D);
                            vanillaPrefabProperties.Add(
                                str + "_prop_" + "_ACIMap",
                                propInfo.m_lodMaterialCombined.GetTexture("_ACIMap") as Texture2D);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        // deactivated for now
        public static Texture2D LoadTexture(string fullPath)
        {
            Texture2D texture2D = new Texture2D(1, 1);
            if (textureCache.TryGetValue(fullPath, out texture2D)) return texture2D;
            texture2D.LoadImage(File.ReadAllBytes(fullPath));
            texture2D.name = Path.GetFileName(fullPath);
            texture2D.anisoLevel = 8;
            texture2D.Compress(true);
            return texture2D;
        }

        public static Texture2D LoadTextureDDS(string fullPath)
        {
            // Testen ob Textur bereits geladen, in dem Fall geladene Textur zurückgeben
            Texture2D texture;
            if (textureCache.TryGetValue(fullPath, out texture)) return texture;

            // Nein? Textur laden
            var numArray = File.ReadAllBytes(fullPath);
            var width = BitConverter.ToInt32(numArray, 16);
            var height = BitConverter.ToInt32(numArray, 12);
            texture = new Texture2D(width, height, TextureFormat.DXT5, true);
            var list = new List<byte>();
            for (int index = 0; index < numArray.Length; ++index)
            {
                if (index > (int)sbyte.MaxValue) list.Add(numArray[index]);
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
            if (ModLoader.config.texturePackPath != null)
            {
                ModLoader.currentTexturesPath_default = Path.Combine(ModLoader.config.texturePackPath, "BaseTextures");
            }

            for (uint i = 0; i < PrefabCollection<NetInfo>.LoadedCount(); i++)
            {
                var netInfo = PrefabCollection<NetInfo>.GetLoaded(i);
                if (netInfo == null) continue;

                

                if (netInfo.m_class.name.Contains("NExt") || netInfo.name.Contains("Busway")
                    || (netInfo.name.Contains("Large Road") && netInfo.name.Contains("Bus Lane")))
                {
                    #region NExt Nodes

                    NetInfo.Node[] nodes = netInfo.m_nodes;
                    for (int k = 0; k < nodes.Length; k++)
                    {
                        NetInfo.Node node = nodes[k];
                        if (node.m_nodeMaterial.GetTexture("_MainTex") != null)
                        {
                            #region NExt TinyRoads Default

                            if (netInfo.name.Contains("Two-Lane Alley"))
                            {
                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Alley2L_Ground_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Alley2L_Ground_Node_MainTex.dds")));

                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Alley2L_Ground_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Alley2L_Ground_Node_APRMap.dds")));

                                node.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("One-Lane Oneway"))
                            {
                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "OneWay1L_Ground_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "OneWay1L_Ground_Node_MainTex.dds")));

                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "OneWay1L_Ground_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "OneWay1L_Ground_Node_APRMap.dds")));

                                node.m_lodRenderDistance = 2500;
                            }

                            #endregion

                            #region Avenues Nodes

                            if (netInfo.name.Contains("Eight-Lane Avenue"))
                            {
                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "LargeAvenue8LM_Ground_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "LargeAvenue8LM_Ground_Node_MainTex.dds")));

                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "LargeAvenue8LM_Elevated_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "LargeAvenue8LM_Elevated_Node_MainTex.dds")));

                                node.m_lodRenderDistance = 2500;
                            }

                            #endregion

                            #region NExt Highways Nodes Default

                            if (netInfo.name.Contains("Rural Highway") && netInfo.name.Contains("Small"))
                            {
                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Node_MainTex.dds"))) /* not an error, it uses the 2l node tex*/
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Node_MainTex.dds")));

                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Node_MainTex.dds")));

                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Slope_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Slope_Node_MainTex.dds")));

                                node.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Rural Highway") && !netInfo.name.Contains("Small"))
                            {
                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Node_MainTex.dds")));

                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Node_MainTex.dds")));

                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Slope_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Slope_Node_MainTex.dds")));

                                node.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Four-Lane Highway"))
                            {
                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway4L_Ground_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway4L_Ground_Node_MainTex.dds")));

                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway4L_Elevated_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway4L_Elevated_Node_MainTex.dds")));

                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway4L_Slope_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway4L_Slope_Node_MainTex.dds")));

                                node.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Five-Lane Highway"))
                            {
                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway5L_Ground_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway5L_Ground_Node_MainTex.dds")));

                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway5L_Ground_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway5L_Ground_Node_MainTex.dds")));

                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway5L_Slope_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway5L_Slope_Node_MainTex.dds")));

                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway5L_Tunnel_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway5L_Tunnel_Node_MainTex.dds")));

                                node.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Large Highway"))
                            {
                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway6L_Ground_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway6L_Ground_Node_MainTex.dds")));

                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway6L_Ground_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway6L_Ground_Node_MainTex.dds")));

                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway6L_Slope_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway6L_Slope_Node_MainTex.dds")));

                                if (node.m_nodeMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway5L_Tunnel_Node_MainTex.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway5L_Tunnel_Node_MainTex.dds")));
                                node.m_lodRenderDistance = 2500;
                            }

                            #endregion

                            #region NExt Highways Nodes APRMaps

                            if (netInfo.name.Contains("Rural Highway") && netInfo.name.Contains("Small"))
                            {
                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Ground_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Ground_Node_APRMap.dds")));

                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Ground_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Ground_Node_APRMap.dds")));

                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Slope_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Slope_Node_APRMap.dds")));

                                node.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Rural Highway") && !netInfo.name.Contains("Small"))
                            {
                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Node_APRMap.dds")));

                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Elevated_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Elevated_Node_APRMap.dds")));

                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Slope_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Slope_Node_APRMap.dds")));

                                node.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Four-Lane Highway"))
                            {
                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway4L_Ground_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway4L_Ground_Node_APRMap.dds")));

                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway4L_Elevated_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway4L_Elevated_Node_APRMap.dds")));

                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway4L_Slope_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway4L_Slope_Node_APRMap.dds")));

                                node.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Five-Lane Highway"))
                            {
                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway5L_Ground_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway5L_Ground_Node_APRMap.dds")));

                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway5L_Elevated_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway5L_Elevated_Node_APRMap.dds")));

                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway5L_Slope_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway5L_Slope_Node_APRMap.dds")));

                                node.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Large Highway"))
                            {
                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway6L_Ground_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway6L_Ground_Node_APRMap.dds")));

                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway6L_Elevated_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway6L_Elevated_Node_APRMap.dds")));

                                if (node.m_nodeMaterial.GetTexture("_APRMap").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway6L_Slope_Node_APRMap.dds")))
                                        node.m_nodeMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway6L_Slope_Node_APRMap.dds")));

                                node.m_lodRenderDistance = 2500;
                            }

                            #endregion
                        }
                    }

                    #endregion

                    #region NExt Segments

                    NetInfo.Segment[] segments = netInfo.m_segments;
                    for (int l = 0; l < segments.Length; l++)
                    {
                        NetInfo.Segment segment = segments[l];

                        if (segment.m_segmentMaterial.GetTexture("_MainTex") != null)
                        {
                            #region NExt TinyRoads Default

                            if (netInfo.name.Contains("Two-Lane Alley"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Alley2L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Alley2L_Ground_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("One-Lane Oneway"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "OneWay1L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "OneWay1L_Ground_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            #endregion

                            #region NExt SmallHeavyRoads Default

                            if (netInfo.name.Contains("BasicRoadTL"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "BasicRoadTL_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "BasicRoadTL_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "BasicRoadTL_Elevated_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "BasicRoadTL_Elevated_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "BasicRoadTL_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "BasicRoadTL_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Oneway3L"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "OneWay3L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "OneWay3L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))

                                    // if ((netInfo.name.Contains("Elevated") || (netInfo.name.Contains("Bridge"))))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "OneWay3L_Elevated_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "OneWay3L_Elevated_Segment_MainTex.dds")));

                                // if ((netInfo.name.Contains("Slope") || (netInfo.name.Contains("Tunnel"))))
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "OneWay3L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "OneWay3L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Oneway4L"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "OneWay4L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "OneWay4L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "OneWay4L_Elevated_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "OneWay4L_Elevated_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "OneWay4L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "OneWay4L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Small Avenue"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "SmallAvenue4L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "SmallAvenue4L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "SmallAvenue4L_Elevated_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "SmallAvenue4L_Elevated_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "SmallAvenue4L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "SmallAvenue4L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            #endregion

                            #region NExt Avenues Default

                            if (netInfo.name.Contains("Medium Avenue") && !netInfo.name.Contains("TL"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "MediumAvenue4L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "MediumAvenue4L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeSegment-default-apr.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeSegment-default-apr.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "RoadLargeSegment-default-apr.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(ModLoader.APRMaps_Path, "RoadLargeSegment-default-apr.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "MediumAvenue4L_Elevated_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "MediumAvenue4L_Elevated_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "MediumAvenue4L_Slope_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "MediumAvenue4L_Slope_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "MediumAvenue4L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "MediumAvenue4L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Medium Avenue") && netInfo.name.Contains("TL"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "MediumAvenue4LTL_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "MediumAvenue4LTL_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeSegment-default-apr.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeSegment-default-apr.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "RoadLargeSegment-default-apr.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(ModLoader.APRMaps_Path, "RoadLargeSegment-default-apr.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "MediumAvenue4LTL_Elevated_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "MediumAvenue4LTL_Elevated_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "MediumAvenue4LTL_Slope_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "MediumAvenue4LTL_Slope_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "MediumAvenue4LTL_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "MediumAvenue4LTL_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Eight-Lane Avenue"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "LargeAvenue8LM_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "LargeAvenue8LM_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "LargeAvenue8LM_Elevated_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "LargeAvenue8LM_Elevated_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "LargeAvenue8LM_Slope_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "LargeAvenue8LM_Slope_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "LargeAvenue8LM_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "LargeAvenue8LM_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            #endregion

                            #region NExt Highways Default

                            if (netInfo.name.Contains("Rural Highway") && netInfo.name.Contains("Small"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Slope_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Slope_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Rural Highway") && !netInfo.name.Contains("Small"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Slope_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Slope_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Four-Lane Highway"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway4L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway4L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway4L_Elevated_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway4L_Elevated_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway4L_Slope_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway4L_Slope_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway4L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway4L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Five-Lane Highway"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway5L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway5L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway5L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway5L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway5L_Slope_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway5L_Slope_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway5L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway5L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Large Highway"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway6L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway6L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway6L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway6L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway6L_Slope_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway6L_Slope_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway6L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway6L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            #endregion

                            #region NExt Busways Default

                            #region Small Busways

                            if (netInfo.name.Contains("Small Busway"))
                            {
                                if (netInfo.name.Contains("Oneway"))
                                {
                                    if (netInfo.name.Contains("Decoration"))
                                    {
                                        if (netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees"))
                                        {
                                            if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                                if (
                                                    File.Exists(
                                                        Path.Combine(
                                                            ModLoader.currentTexturesPath_default,
                                                            "Busway2L1W_DecoGrass_Ground_Segment_MainTex.dds")))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(
                                                        "_MainTex",
                                                        LoadTextureDDS(
                                                            Path.Combine(
                                                                ModLoader.currentTexturesPath_default,
                                                                "Busway2L1W_DecoGrass_Ground_Segment_MainTex.dds")));
                                                    segment.m_lodRenderDistance = 2500;
                                                }

                                            // if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                            // if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadBasic2-apr.dds")))
                                            // {
                                            // segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "RoadBasic2-apr.dds")));
                                            // segment.m_lodRenderDistance = 2500;
                                            // }
                                            // else if (File.Exists(Path.Combine(ModLoader.APRMaps_Path, "RoadBasic2-apr.dds")))
                                            // {
                                            // segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.APRMaps_Path, "RoadBasic2-apr.dds")));
                                            // segment.m_lodRenderDistance = 2500;
                                            // }
                                        }
                                    }
                                    else
                                    {
                                        if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                            if (
                                                File.Exists(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "Busway2L1W_Ground_Segment_MainTex.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    "_MainTex",
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.currentTexturesPath_default,
                                                            "Busway2L1W_Ground_Segment_MainTex.dds")));
                                                segment.m_lodRenderDistance = 2500;
                                            }

                                        if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                            if (
                                                File.Exists(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "Busway2L1W_Elevated_Segment_MainTex.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    "_MainTex",
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.currentTexturesPath_default,
                                                            "Busway2L1W_Elevated_Segment_MainTex.dds")));
                                                segment.m_lodRenderDistance = 2500;
                                            }

                                        if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                            if (
                                                File.Exists(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "Busway2L1W_Slope_Segment_MainTex.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    "_MainTex",
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.currentTexturesPath_default,
                                                            "Busway2L1W_Slope_Segment_MainTex.dds")));
                                                segment.m_lodRenderDistance = 2500;
                                            }

                                        // if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                        // if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmallSegment-Default-apr.dds")))
                                        // {
                                        // segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmallSegment-Default-apr.dds")));
                                        // segment.m_lodRenderDistance = 2500;
                                        // }
                                        // else if (File.Exists(Path.Combine(ModLoader.APRMaps_Path, "RoadSmallSegment-Default-apr.dds")))
                                        // {
                                        // segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.APRMaps_Path, "RoadSmallSegment-Default-apr.dds")));
                                        // segment.m_lodRenderDistance = 2500;
                                        // }
                                    }
                                }
                                else
                                {
                                    if (netInfo.name.Contains("Decoration"))
                                    {
                                        if (netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees"))
                                        {
                                            if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                                if (
                                                    File.Exists(
                                                        Path.Combine(
                                                            ModLoader.currentTexturesPath_default,
                                                            "Busway2L_DecoGrass_Ground_Segment_MainTex.dds")))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(
                                                        "_MainTex",
                                                        LoadTextureDDS(
                                                            Path.Combine(
                                                                ModLoader.currentTexturesPath_default,
                                                                "Busway2L_DecoGrass_Ground_Segment_MainTex.dds")));
                                                    segment.m_lodRenderDistance = 2500;
                                                }

                                            // if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                            // if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadBasic2-apr.dds")))
                                            // {
                                            // segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "RoadBasic2-apr.dds")));
                                            // segment.m_lodRenderDistance = 2500;
                                            // }
                                            // else if (File.Exists(Path.Combine(ModLoader.APRMaps_Path, "RoadBasic2-apr.dds")))
                                            // {
                                            // segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.APRMaps_Path, "RoadBasic2-apr.dds")));
                                            // segment.m_lodRenderDistance = 2500;
                                            // }
                                        }
                                    }
                                    else
                                    {
                                        if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                            if (
                                                File.Exists(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "Busway2L_Ground_Segment_MainTex.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    "_MainTex",
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.currentTexturesPath_default,
                                                            "Busway2L_Ground_Segment_MainTex.dds")));
                                                segment.m_lodRenderDistance = 2500;
                                            }

                                        if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                            if (
                                                File.Exists(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "Busway2L_Elevated_Segment_MainTex.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    "_MainTex",
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.currentTexturesPath_default,
                                                            "Busway2L_Elevated_Segment_MainTex.dds")));
                                                segment.m_lodRenderDistance = 2500;
                                            }

                                        if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                            if (
                                                File.Exists(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "Busway2L_Slope_Segment_MainTex.dds")))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    "_MainTex",
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.currentTexturesPath_default,
                                                            "Busway2L_Slope_Segment_MainTex.dds")));
                                                segment.m_lodRenderDistance = 2500;
                                            }

                                        // if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                        // if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmallSegment-Default-apr.dds")))
                                        // {
                                        // segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmallSegment-Default-apr.dds")));
                                        // segment.m_lodRenderDistance = 2500;
                                        // }
                                        // else if (File.Exists(Path.Combine(ModLoader.APRMaps_Path, "RoadSmallSegment-Default-apr.dds")))
                                        // {
                                        // segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.APRMaps_Path, "RoadSmallSegment-Default-apr.dds")));
                                        // segment.m_lodRenderDistance = 2500;
                                        // }
                                    }
                                }
                            }

                            #endregion

                            #region Large Busways

                            if (netInfo.name.Contains("Large Road") && netInfo.name.Contains("Bus Lanes"))
                            {
                                if (netInfo.name.Contains("Grass") || (netInfo.name.Contains("Trees")))
                                {
                                    if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                        if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Busway6L_DecoGrass_Ground_Segment_MainTex.dds")))
                                        {
                                            segment.m_segmentMaterial.SetTexture(
                                                "_MainTex",
                                                LoadTextureDDS(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "Busway6L_DecoGrass_Ground_Segment_MainTex.dds")));
                                            segment.m_lodRenderDistance = 2500;
                                        }

                                    if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                        if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeSegment-default-apr.dds")))
                                        {
                                            segment.m_segmentMaterial.SetTexture(
                                                "_APRMap",
                                                LoadTextureDDS(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "RoadLargeSegment-default-apr.dds")));
                                            segment.m_lodRenderDistance = 2500;
                                        }
                                        else if (
                                            File.Exists(
                                                Path.Combine(ModLoader.APRMaps_Path, "RoadLargeSegment-default-apr.dds")))
                                        {
                                            segment.m_segmentMaterial.SetTexture(
                                                "_APRMap",
                                                LoadTextureDDS(
                                                    Path.Combine(
                                                        ModLoader.APRMaps_Path,
                                                        "RoadLargeSegment-default-apr.dds")));
                                            segment.m_lodRenderDistance = 2500;
                                        }
                                }
                                else
                                {
                                    if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                        if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeBuslane_D.dds")))
                                        {
                                            segment.m_segmentMaterial.SetTexture(
                                                "_MainTex",
                                                LoadTextureDDS(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "RoadLargeBuslane_D.dds")));
                                            segment.m_lodRenderDistance = 2500;
                                        }

                                    if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                        if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeElevatedBus_D.dds")))
                                        {
                                            segment.m_segmentMaterial.SetTexture(
                                                "_MainTex",
                                                LoadTextureDDS(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "RoadLargeElevatedBus_D.dds")));
                                            segment.m_lodRenderDistance = 2500;
                                        }

                                    if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                        if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "large-tunnelBus_d.dds")))
                                        {
                                            segment.m_segmentMaterial.SetTexture(
                                                "_MainTex",
                                                LoadTextureDDS(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "large-tunnelBus_d.dds")));
                                            segment.m_lodRenderDistance = 2500;
                                        }

                                    if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                        if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeSegment-default-apr.dds")))
                                        {
                                            segment.m_segmentMaterial.SetTexture(
                                                "_APRMap",
                                                LoadTextureDDS(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "RoadLargeSegment-default-apr.dds")));
                                            segment.m_lodRenderDistance = 2500;
                                        }
                                        else if (
                                            File.Exists(
                                                Path.Combine(ModLoader.APRMaps_Path, "RoadLargeSegment-default-apr.dds")))
                                        {
                                            segment.m_segmentMaterial.SetTexture(
                                                "_APRMap",
                                                LoadTextureDDS(
                                                    Path.Combine(
                                                        ModLoader.APRMaps_Path,
                                                        "RoadLargeSegment-default-apr.dds")));
                                            segment.m_lodRenderDistance = 2500;
                                        }

                                    // if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    // if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmallSegment-Default-apr.dds")))
                                    // {
                                    // segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmallSegment-Default-apr.dds")));
                                    // segment.m_lodRenderDistance = 2500;
                                    // }
                                }
                            }

                            #endregion

                            #endregion
                        }

                        if (segment.m_segmentMaterial.GetTexture("_APRMap") != null)
                        {
                            #region SmallHeavyRoads APRMaps

                            if (netInfo.name.Contains("BasicRoadTL"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "BasicRoadTL_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "BasicRoadTL_Ground_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "BasicRoadTL_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "BasicRoadTL_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "BasicRoadTL_Elevated_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "BasicRoadTL_Elevated_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "BasicRoadTL_Elevated_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "BasicRoadTL_Elevated_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "BasicRoadTL_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "BasicRoadTL_Tunnel_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "BasicRoadTL_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "BasicRoadTL_Tunnel_Segment_APRMap.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Oneway3L"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "OneWay3L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "OneWay3L_Ground_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "OneWay3L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "OneWay3L_Ground_Segment_APRMap.dds")));

                                // if ((netInfo.name.Contains("Elevated") || (netInfo.name.Contains("Bridge"))))
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "OneWay3L_Elevated_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "OneWay3L_Elevated_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "OneWay3L_Elevated_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "OneWay3L_Elevated_Segment_APRMap.dds")));

                                // if ((netInfo.name.Contains("Slope") || (netInfo.name.Contains("Tunnel"))))
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "OneWay3L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "OneWay3L_Tunnel_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "OneWay3L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "OneWay3L_Tunnel_Segment_APRMap.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Oneway4L"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "OneWay4L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "OneWay4L_Ground_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "OneWay4L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "OneWay4L_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "OneWay4L_Elevated_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "OneWay4L_Elevated_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "OneWay4L_Elevated_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "OneWay4L_Elevated_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "OneWay4L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "OneWay4L_Tunnel_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "OneWay4L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "OneWay4L_Tunnel_Segment_APRMap.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Small Avenue"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "SmallAvenue4L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "SmallAvenue4L_Ground_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "SmallAvenue4L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "SmallAvenue4L_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "SmallAvenue4L_Elevated_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "SmallAvenue4L_Elevated_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "SmallAvenue4L_Elevated_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "SmallAvenue4L_Elevated_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "SmallAvenue4L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "SmallAvenue4L_Tunnel_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "SmallAvenue4L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "SmallAvenue4L_Tunnel_Segment_APRMap.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            #endregion

                            #region Avenues APRMaps

                            if (netInfo.name.Contains("Eight-Lane Avenue"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "LargeAvenue8LM_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "LargeAvenue8LM_Ground_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "LargeAvenue8LM_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "LargeAvenue8LM_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "LargeAvenue8LM_Elevated_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "LargeAvenue8LM_Elevated_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "LargeAvenue8LM_Elevated_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "LargeAvenue8LM_Elevated_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "LargeAvenue8LM_Slope_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "LargeAvenue8LM_Slope_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "LargeAvenue8LM_Slope_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "LargeAvenue8LM_Slope_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "LargeAvenue8LM_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "LargeAvenue8LM_Tunnel_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "LargeAvenue8LM_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "LargeAvenue8LM_Tunnel_Segment_APRMap.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            #endregion

                            #region NExt Highways APRMaps

                            if (netInfo.name.Contains("Rural Highway") && netInfo.name.Contains("Small"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Ground_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway1L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway1L_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Ground_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway1L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway1L_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Slope_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Slope_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway1L_Slope_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway1L_Slope_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Tunnel_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway1L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway1L_Tunnel_Segment_APRMap.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Rural Highway") && !netInfo.name.Contains("Small"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway2L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway2L_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway2L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway2L_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Slope_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Slope_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway2L_Slope_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway2L_Slope_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Tunnel_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway2L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway2L_Tunnel_Segment_APRMap.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Four-Lane Highway"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway4L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway4L_Ground_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway4L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway4L_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway4L_Elevated_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway4L_Elevated_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "Highway4L_Elevated_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway4L_Elevated_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway4L_Slope_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway4L_Slope_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway4L_Slope_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway4L_Slope_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway4L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway4L_Tunnel_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway4L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway4L_Tunnel_Segment_APRMap.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Five-Lane Highway"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway5L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway5L_Ground_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway5L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway5L_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway5L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway5L_Ground_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway5L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway5L_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway5L_Slope_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway5L_Slope_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway5L_Slope_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway5L_Slope_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway5L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway5L_Tunnel_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway5L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway5L_Tunnel_Segment_APRMap.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Large Highway"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway6L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway6L_Ground_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway6L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway6L_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway6L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway6L_Ground_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway6L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway6L_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Slope"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway6L_Slope_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway6L_Slope_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway6L_Slope_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway6L_Slope_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Tunnel"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway6L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway6L_Tunnel_Segment_APRMap.dds")));
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "Highway6L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture(
                                            "_APRMap",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway6L_Tunnel_Segment_APRMap.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            #endregion
                        }
                    }

                    #endregion
                }

                

                if (
                        !(netInfo.m_class.name.Contains("NExt") || netInfo.m_class.name.Contains("Water")
                          || netInfo.m_class.name.Contains("Train") || netInfo.m_class.name.Contains("Metro")
                          || netInfo.m_class.name.Contains("Transport") || netInfo.m_class.name.Contains("Bus Line")
                          || netInfo.m_class.name.Contains("Airplane") || netInfo.m_class.name.Contains("Ship")
                          || netInfo.name.Contains("Busway")
                          || (netInfo.name.Contains("Large Road") && netInfo.name.Contains("Bus Lane"))))
                {
                    // Only Roads specifically
                    NetInfo.Node[] nodes = netInfo.m_nodes;
                    for (int k = 0; k < nodes.Length; k++)
                    {
                        NetInfo.Node node = nodes[k];
                        if (node.m_nodeMaterial.GetTexture("_MainTex") != null)
                        {
                            string nodeMaterialTexture_name = Path.Combine(
                                ModLoader.currentTexturesPath_default,
                                node.m_nodeMaterial.GetTexture("_MainTex").name + ".dds");
                            string prefab_road_name = netInfo.name.Replace(" ", "_").ToLowerInvariant().Trim();

                            // if (File.Exists(prefab_road_name + "_" + nodeMaterialTexture_name))   //still needed?
                            // node.m_nodeMaterial.SetTexture("_MainTex", LoadTextureDDS(prefab_road_name + "_" + nodeMaterialTexture_name));
                            if (File.Exists(nodeMaterialTexture_name)) node.m_nodeMaterial.SetTexture("_MainTex", LoadTextureDDS(nodeMaterialTexture_name));

                            node.m_lodRenderDistance = 2500;
                            node.m_lodMesh = null;
                        }

                        if (node.m_nodeMaterial.GetTexture("_APRMap") != null)
                        {
                            string nodeMaterialAPRMap_name = Path.Combine(
                                ModLoader.currentTexturesPath_default,
                                node.m_nodeMaterial.GetTexture("_APRMap").name + ".dds");

                            if (File.Exists(nodeMaterialAPRMap_name)) node.m_nodeMaterial.SetTexture("_APRMap", LoadTextureDDS(nodeMaterialAPRMap_name));
                        }
                    }

                    // Look for segments
                    NetInfo.Segment[] segments = netInfo.m_segments;
                    for (int l = 0; l < segments.Length; l++)
                    {
                        NetInfo.Segment segment = segments[l];
                        if (segment.m_segmentMaterial.GetTexture("_MainTex") != null)
                        {
                            string segmentMaterialTexture_name = Path.Combine(
                                ModLoader.currentTexturesPath_default,
                                segment.m_segmentMaterial.GetTexture("_MainTex").name + ".dds");

                            // I'm combining cofiguration with this region to sort each item by Network Type/Class
                            // Also combining if statements where && is appropriate
                            // #region Oneways
                            #region exceptions

                            if (netInfo.name.Contains("Oneway"))
                            {
                                #region Small Oneway

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name == "RoadSmallSegment")
                                {
                                    if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide")
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Oneway_RoadSmallSegment_BusSide.dds")))
                                    {
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Oneway_RoadSmallSegment_BusSide.dds")));
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth")
                                             && File.Exists(
                                                 Path.Combine(
                                                     ModLoader.currentTexturesPath_default,
                                                     "Oneway_RoadSmallSegment_BusBoth.dds")))
                                    {
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Oneway_RoadSmallSegment_BusBoth.dds")));
                                    }
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Oneway_RoadSmallSegment.dds")))
                                    {
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Oneway_RoadSmallSegment.dds")));
                                    }
                                }

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name == "SmallRoadSegmentDeco")
                                {
                                    if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide")
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Oneway_SmallRoadSegmentDeco_BusSide.dds")))
                                    {
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Oneway_SmallRoadSegmentDeco_BusSide.dds")));
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth")
                                             && File.Exists(
                                                 Path.Combine(
                                                     ModLoader.currentTexturesPath_default,
                                                     "Oneway_SmallRoadSegmentDeco_BusBoth.dds")))
                                    {
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Oneway_SmallRoadSegmentDeco_BusBoth.dds")));
                                    }
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Oneway_SmallRoadSegmentDeco.dds")))
                                    {
                                        segment.m_segmentMaterial.SetTexture(
                                            "_MainTex",
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Oneway_SmallRoadSegmentDeco.dds")));
                                    }
                                }

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name == "small-tunnel_d"
                                    && File.Exists(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "Oneway_small-tunnel_d.dds")))
                                {
                                    segment.m_segmentMaterial.SetTexture(
                                        "_MainTex",
                                        LoadTextureDDS(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Oneway_small-tunnel_d.dds")));
                                }

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name == "RoadSmallElevatedSegment"
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "Oneway_RoadSmallElevatedSegment_D.dds")))
                                {
                                    segment.m_segmentMaterial.SetTexture(
                                        "_MainTex",
                                        LoadTextureDDS(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Oneway_RoadSmallElevatedSegment_D.dds")));
                                }

                                #endregion

                                #region Large Oneway

                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeOnewaySegment_d_BusSide.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeOnewaySegment_d_BusSide.dds");
                                }

                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeOnewaySegment_d_BusBoth.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeOnewaySegment_d_BusBoth.dds");
                                }

                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide")
                                    && File.Exists(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_BusBoth.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeOnewaySegment_d_BusSide.dds");
                                }

                                // this texture might not be in use
                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth")
                                    && File.Exists(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_BusBoth.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeOnewaySegment_d_BusSide.dds");
                                }

                                #endregion
                            }

                            if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                if (!netInfo.name.Contains("Bicycle")
                                    && File.Exists(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmall_D_BusSide.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadSmall_D_BusSide.dds");
                                }

                            if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                                if (!netInfo.name.Contains("Bicycle")
                                    && File.Exists(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmall_D_BusBoth.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadSmall_D_BusBoth.dds");
                                }

                            if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide")
                                && File.Exists(
                                    Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "SmallRoadSegmentDeco_BusSide.dds")))
                            {
                                segmentMaterialTexture_name = Path.Combine(
                                    ModLoader.currentTexturesPath_default,
                                    "SmallRoadSegmentDeco_BusSide.dds");
                            }

                            if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth")
                                && File.Exists(
                                    Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "SmallRoadSegmentDeco_BusSide.dds")))
                            {
                                segmentMaterialTexture_name = Path.Combine(
                                    ModLoader.currentTexturesPath_default,
                                    "SmallRoadSegmentDeco_BusBoth.dds");
                            }

                            if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                            {
                                if (netInfo.name.Contains("Trees"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMediumDeco_d_BusSide.dds")))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumDeco_d_BusSide.dds");
                                    else if (netInfo.name.Contains("Bus"))
                                        if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadMediumBusLane_BusSide.dds")))
                                            segmentMaterialTexture_name =
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadMediumBusLane_BusSide.dds");
                                        else if (!netInfo.name.Contains("Bicycle"))
                                            if (
                                                File.Exists(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "RoadMedium_D_BusSide.dds")))
                                                segmentMaterialTexture_name =
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "RoadMedium_D_BusSide.dds");

                                goto configsettings;
                            }

                            if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                            {
                                if (netInfo.name.Contains("Trees"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMediumDeco_d_BusBoth.dds")))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumDeco_d_BusBoth.dds");
                                    else if (netInfo.name.Contains("Bus"))
                                        if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadMediumBusLane_BusBoth.dds")))
                                            segmentMaterialTexture_name =
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadMediumBusLane_BusBoth.dds");
                                        else if (!netInfo.name.Contains("Bicycle"))
                                            if (
                                                File.Exists(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "RoadMedium_D_BusBoth.dds")))
                                                segmentMaterialTexture_name =
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "RoadMedium_D_BusBoth.dds");
                                goto configsettings;
                            }

                            if (!(netInfo.name.Contains("Bicycle") || netInfo.name.Contains("Oneway")))
                            {
                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegment_d_BusSide.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeSegment_d_BusSide.dds");
                                }

                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegment_d_BusBoth.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeSegment_d_BusBoth.dds");
                                }

                                if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegmentDecoBusBoth_d.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeSegmentDecoBusBoth_d.dds");
                                }

                                if (segment.m_mesh.name.Equals("RoadLargeSegmentBusSideBusLane")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeBuslane_D_BusSide.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeBuslane_D_BusSide.dds");
                                }

                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBothBusLane")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeBuslane_D_BusBoth.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeBuslane_D_BusBoth.dds");
                                }
                            }

                            Debug.Log(segmentMaterialTexture_name);

                            #endregion

                            configsettings:

                            #region configsettings

                            if (ModLoader.config.basic_road_parking == 1)
                            {
                                if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmall_D.dds")))
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadSmall_D_parking1.dds");

                                if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmall_D_BusSide.dds")))
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadSmall_D_BusSide_parking1.dds");
                            }

                            if ((ModLoader.config.medium_road_parking == 1)
                                && (!(netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees"))))
                            {
                                if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                {
                                    if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMediumSegment_d.dds"))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMedium_D_BusSide_parking1.dds")))
                                    {
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMedium_D_BusSide_parking1.dds");
                                    }
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                {
                                    if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMediumSegment_d.dds"))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMedium_D_BusBoth_parking1.dds")))
                                    {
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMedium_D_BusBoth_parking1.dds");
                                    }
                                }
                                else if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumSegment_d.dds"))
                                    && File.Exists(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_parking1.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadMedium_D_parking1.dds");
                                }
                                else if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D.dds"))
                                    && File.Exists(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_parking1.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadMedium_D_parking1.dds");
                                }
                            }

                            if ((ModLoader.config.medium_road_grass_parking == 1) && netInfo.name.Contains("Grass"))
                            {
                                if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                {
                                    if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D.dds"))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMedium_D_BusSide_parking1.dds")))
                                    {
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMedium_D_BusSide_parking1.dds");
                                    }
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                {
                                    if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D.dds"))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMedium_D_BusBoth_parking1.dds")))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMedium_D_BusBoth_parking1.dds");
                                }
                                else if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumSegment_d.dds"))
                                    && File.Exists(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_parking1.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadMedium_D_parking1.dds");
                                }
                                else if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D.dds"))
                                    && File.Exists(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_parking1.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadMedium_D_parking1.dds");
                                }
                            }

                            if ((ModLoader.config.medium_road_trees_parking == 1) && netInfo.name.Contains("Trees"))
                            {
                                if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                {
                                    if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumDeco_d.dds"))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMediumDeco_d_BusSide_parking1.dds")))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumDeco_d_BusSide_parking1.dds");
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth")
                                         && File.Exists(
                                             Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumDeco_d.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadMediumDeco_d.dds");
                                }
                                else if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumDeco_d.dds"))
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumDeco_d_parking1.dds")))
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadMediumDeco_d_parking1.dds");
                            }

                            if (ModLoader.config.medium_road_bus_parking == 1)
                            {
                                if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                {
                                    if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumBusLane.dds"))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMediumBusLane_BusSide_parking1.dds")))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumBusLane_BusSide_parking1.dds");
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                {
                                    if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumBusLane.dds"))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMediumBusLane_BusBoth_parking1.dds")))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumBusLane_BusBoth_parking1.dds");
                                }
                                else if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumBusLane.dds"))
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumBusLane_parking1.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadMediumBusLane_parking1.dds");
                                }
                            }

                            if (ModLoader.config.large_road_parking == 1)
                            {
                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                {
                                    if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeSegment_d.dds"))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeSegment_d_BusSide_parking1.dds")))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegment_d_BusSide_parking1.dds");
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                {
                                    if (
                                            segmentMaterialTexture_name.Equals(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeSegment_d.dds"))
                                            && File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeSegment_d_BusBoth_parking1.dds")))

                                        // might be changed back to default segment
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegment_d_BusBoth_parking1.dds");
                                }
                                else if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeSegment_d.dds"))
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegment_d_parking1.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeSegment_d_parking1.dds");
                                }
                            }

                            if (ModLoader.config.large_oneway_parking == 1)
                            {
                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                {
                                    if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeOnewaySegment_d.dds"))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeOnewaySegment_d_BusSide_parking1.dds")))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeOnewaySegment_d_BusSide_parking1.dds");
                                }
                                else if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeOnewaySegment_d.dds"))
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeOnewaySegment_d_parking1.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeOnewaySegment_d_parking1.dds");
                                }
                            }

                            if (ModLoader.config.large_road_bus_parking == 1)
                            {
                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSideBusLane"))
                                {
                                    if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeBuslane_D.dds"))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeBuslane_D_BusSide_parking1.dds")))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeBuslane_D_BusSide_parking1.dds");
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBothBusLane"))
                                {
                                    if (
                                            segmentMaterialTexture_name.Equals(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeBuslane_D.dds"))
                                            && File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeBuslane_D_BusBoth_parking1.dds")))

                                        // might be changed back to default segment
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeBuslane_D_BusBoth_parking1.dds");
                                }
                                else if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeBuslane_D.dds"))
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeBuslane_D_parking1.dds")))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeBuslane_D_parking1.dds");
                                }
                            }

                            #endregion

                            // Replace the default segment textures
                            if (File.Exists(segmentMaterialTexture_name))
                                segment.m_segmentMaterial.SetTexture(
                                    "_MainTex",
                                    LoadTextureDDS(segmentMaterialTexture_name));
                        }

                        if (segment.m_segmentMaterial.GetTexture("_APRMap") != null)
                        {
                            string segmentMaterialAPRMap_name = Path.Combine(
                                ModLoader.currentTexturesPath_default,
                                segment.m_segmentMaterial.GetTexture("_APRMap").name + ".dds");
                            Debug.Log(segmentMaterialAPRMap_name);
                            {
                                // APRS!!!!!
                                if (
                                    segment.m_segmentMaterial.GetTexture("_APRMap")
                                        .name.Equals("LargeRoadSegmentBusSide-BikeLane-apr")
                                    || segment.m_segmentMaterial.GetTexture("_APRMap")
                                        .name.Equals("LargeRoadSegmentBusBoth-BikeLane-apr"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeSegment-BikeLane-apr.dds")))
                                        segmentMaterialAPRMap_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegment-BikeLane-apr.dds");
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "RoadLargeSegment-BikeLane-apr.dds")))
                                        segmentMaterialAPRMap_name = Path.Combine(
                                            ModLoader.APRMaps_Path,
                                            "RoadLargeSegment-BikeLane-apr.dds");
                                if (
                                    segment.m_segmentMaterial.GetTexture("_APRMap")
                                        .name.Equals("LargeRoadSegmentBusSide-LargeRoadSegmentBusSide-apr")
                                    || segment.m_segmentMaterial.GetTexture("_APRMap")
                                        .name.Equals("LargeRoadSegmentBusBoth-LargeRoadSegmentBusBoth-apr"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeSegment-default-apr.dds")))
                                        segmentMaterialAPRMap_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegment-default-apr.dds");
                                    else if (
                                        File.Exists(
                                            Path.Combine(ModLoader.APRMaps_Path, "RoadLargeSegment-default-apr.dds")))
                                        segmentMaterialAPRMap_name = Path.Combine(
                                            ModLoader.APRMaps_Path,
                                            "RoadLargeSegment-default-apr.dds");

                                if (File.Exists(segmentMaterialAPRMap_name))
                                    segment.m_segmentMaterial.SetTexture(
                                        "_APRMap",
                                        LoadTextureDDS(segmentMaterialAPRMap_name));
                            }
                        }

                        segment.m_lodRenderDistance = 2500;
                    }
                }
            }
        }
    }
}