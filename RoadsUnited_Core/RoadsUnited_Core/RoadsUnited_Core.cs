namespace RoadsUnited_Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using UnityEngine;

    public class RoadsUnited_Core : MonoBehaviour
    {
        public static Configuration config;

        public static string ext_DDS = ".dds";

        public static Dictionary<string, Texture2D> vanillaPrefabProperties = new Dictionary<string, Texture2D>();

        private static Texture2D acimap;

        private static Texture2D aprmap;

        private static Texture2D defaultmap;

        private static Dictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D>();

        public static void ApplyVanillaDictionary()
        {
            for (uint i = 0; i < PrefabCollection<NetInfo>.LoadedCount(); i++)
            {
                NetInfo netInfo = PrefabCollection<NetInfo>.GetLoaded(i);
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
                                    prefab_road_name + "_node_" + k + "_nodeMaterial" + TexType._MainTex,
                                    out defaultmap)) node.m_nodeMaterial.SetTexture(TexType._MainTex, defaultmap);
                            if (
                                vanillaPrefabProperties.TryGetValue(
                                    prefab_road_name + "_node_" + k + "_nodeMaterial" + TexType._APRMap,
                                    out aprmap)) node.m_nodeMaterial.SetTexture(TexType._APRMap, aprmap);
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
                                    prefab_road_name + "_segment_" + l + "_segmentMaterial" + TexType._MainTex,
                                    out defaultmap)) segment.m_segmentMaterial.SetTexture(TexType._MainTex, defaultmap);
                            if (
                                vanillaPrefabProperties.TryGetValue(
                                    prefab_road_name + "_segment_" + l + "_segmentMaterial" + TexType._APRMap,
                                    out aprmap)) segment.m_segmentMaterial.SetTexture(TexType._APRMap, aprmap);
                        }
                    }
                }
            }

            PropCollection[] array = FindObjectsOfType<PropCollection>();
            foreach (PropCollection propCollection in array)
            {
                try
                {
                    PropInfo[] prefabs = propCollection.m_prefabs;
                    foreach (PropInfo propInfo in prefabs)
                    {
                        string str = propInfo.name;

                        // if (propInfo.m_lodMaterialCombined != null)
                        if (propInfo.m_lodMaterialCombined.GetTexture(TexType._MainTex).name != null)
                        {
                            if (vanillaPrefabProperties.TryGetValue(str + "_prop_" + TexType._MainTex, out defaultmap))
                            {
                                propInfo.m_lodMaterialCombined.SetTexture(TexType._MainTex, defaultmap);
                            }

                            if (vanillaPrefabProperties.TryGetValue(str + "_prop_" + "_ACIMap", out acimap))
                            {
                                propInfo.m_lodMaterialCombined.SetTexture("_ACIMap", acimap);
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
            for (uint i = 0; i < PrefabCollection<NetInfo>.LoadedCount(); i++)
            {
                NetInfo netInfo = PrefabCollection<NetInfo>.GetLoaded(i);

                if (netInfo == null)
                {
                    continue;
                }

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
                                    prefab_road_name + "_node_" + k + "_nodeMaterial" + TexType._MainTex,
                                    out defaultmap)) node.m_nodeMaterial.SetTexture(TexType._MainTex, defaultmap);
                            if (
                                vanillaPrefabProperties.TryGetValue(
                                    prefab_road_name + "_node_" + k + "_nodeMaterial" + TexType._APRMap,
                                    out aprmap)) node.m_nodeMaterial.SetTexture(TexType._APRMap, aprmap);
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
                                    prefab_road_name + "_segment_" + l + "_segmentMaterial" + TexType._MainTex,
                                    out defaultmap)) segment.m_segmentMaterial.SetTexture(TexType._MainTex, defaultmap);
                            if (
                                vanillaPrefabProperties.TryGetValue(
                                    prefab_road_name + "_segment_" + l + "_segmentMaterial" + TexType._APRMap,
                                    out aprmap)) segment.m_segmentMaterial.SetTexture(TexType._APRMap, aprmap);
                        }
                    }
                }
            }
        }

        public static void CreateVanillaDictionary()
        {
            for (uint i = 0; i < PrefabCollection<NetInfo>.LoadedCount(); i++)
            {
                NetInfo netInfo = PrefabCollection<NetInfo>.GetLoaded(i);
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
                                prefab_road_name + "_node_" + k + "_nodeMaterial" + TexType._MainTex,
                                node.m_nodeMaterial.GetTexture(TexType._MainTex) as Texture2D);
                            vanillaPrefabProperties.Add(
                                prefab_road_name + "_node_" + k + "_nodeMaterial" + TexType._APRMap,
                                node.m_nodeMaterial.GetTexture(TexType._APRMap) as Texture2D);
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
                                prefab_road_name + "_segment_" + l + "_segmentMaterial" + TexType._MainTex,
                                segment.m_segmentMaterial.GetTexture(TexType._MainTex) as Texture2D);
                            vanillaPrefabProperties.Add(
                                prefab_road_name + "_segment_" + l + "_segmentMaterial" + TexType._APRMap,
                                segment.m_segmentMaterial.GetTexture(TexType._APRMap) as Texture2D);
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
                        if (propInfo.m_lodMaterialCombined.GetTexture(TexType._MainTex).name != null)
                        {
                            vanillaPrefabProperties.Add(
                                str + "_prop_" + TexType._MainTex,
                                propInfo.m_lodMaterialCombined.GetTexture(TexType._MainTex) as Texture2D);
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
            byte[] numArray = File.ReadAllBytes(fullPath);
            int width = BitConverter.ToInt32(numArray, 16);
            int height = BitConverter.ToInt32(numArray, 12);
            texture = new Texture2D(width, height, TextureFormat.DXT5, true);
            List<byte> list = new List<byte>();
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
            string filename = String.Empty;
            string filename2 = String.Empty;
            for (uint i = 0; i < PrefabCollection<NetInfo>.LoadedCount(); i++)
            {
                NetInfo netInfo = PrefabCollection<NetInfo>.GetLoaded(i);
                if (netInfo == null)
                {
                    continue;
                }

                if (netInfo.m_class.name.Contains("NExt") || netInfo.name.Contains("Busway")
                    || (netInfo.name.Contains("Large Road") && netInfo.name.Contains("Bus Lane")))
                {
                    NetInfo.Node[] nodes = netInfo.m_nodes;
                    foreach (NetInfo.Node node in nodes)
                    {
                        if (node.m_nodeMaterial.GetTexture(TexType._MainTex) != null)
                        {
                            ReplaceNExtNodes(netInfo, node);

                            if (netInfo.name.Contains("Rural Highway") && netInfo.name.Contains("Small"))
                            {
                                filename2 = "Highway2L_Ground_Node" + TexType._MainTex + ext_DDS;
                                if (node.m_nodeMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Ground))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                filename2))) /* not an error, it uses the 2l node tex*/
                                        filename=filename2;
                                        node.m_nodeMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Node" + TexType._MainTex + ext_DDS)));

                                if (node.m_nodeMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Elevated))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Node" + TexType._MainTex + ext_DDS)))
                                        node.m_nodeMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Node" + TexType._MainTex + ext_DDS)));

                                if (node.m_nodeMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Slope))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Slope_Node" + TexType._MainTex + ext_DDS)))
                                        node.m_nodeMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Slope_Node" + TexType._MainTex + ext_DDS)));

                                node.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Rural Highway") && !netInfo.name.Contains("Small"))
                            {
                                if (node.m_nodeMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Ground))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Node" + TexType._MainTex + ext_DDS)))
                                        node.m_nodeMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Node" + TexType._MainTex + ext_DDS)));

                                if (node.m_nodeMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Elevated))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Node" + TexType._MainTex + ext_DDS)))
                                        node.m_nodeMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Node" + TexType._MainTex + ext_DDS)));

                                if (node.m_nodeMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Slope))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Slope_Node" + TexType._MainTex + ext_DDS)))
                                        node.m_nodeMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Slope_Node" + TexType._MainTex + ext_DDS)));

                                node.m_lodRenderDistance = 2500;
                            }



                            #region NExt Highways Nodes APRMaps

                            if (netInfo.name.Contains("Rural Highway") && netInfo.name.Contains("Small"))
                            {
                                if (node.m_nodeMaterial.GetTexture(TexType._APRMap).name.Contains(RoadPos.Ground))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Ground_Node" + TexType._APRMap + ext_DDS)))
                                        node.m_nodeMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Ground_Node" + TexType._APRMap + ext_DDS)));

                                if (node.m_nodeMaterial.GetTexture(TexType._APRMap).name.Contains(RoadPos.Elevated))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Ground_Node" + TexType._APRMap + ext_DDS)))
                                        node.m_nodeMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Ground_Node" + TexType._APRMap + ext_DDS)));

                                if (node.m_nodeMaterial.GetTexture(TexType._APRMap).name.Contains(RoadPos.Slope))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Slope_Node" + TexType._APRMap + ext_DDS)))
                                        node.m_nodeMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Slope_Node" + TexType._APRMap + ext_DDS)));

                                node.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Rural Highway") && !netInfo.name.Contains("Small"))
                            {
                                if (node.m_nodeMaterial.GetTexture(TexType._APRMap).name.Contains(RoadPos.Ground))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Node" + TexType._APRMap + ext_DDS)))
                                        node.m_nodeMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Node" + TexType._APRMap + ext_DDS)));

                                if (node.m_nodeMaterial.GetTexture(TexType._APRMap).name.Contains(RoadPos.Elevated))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Elevated_Node" + TexType._APRMap + ext_DDS)))
                                        node.m_nodeMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Elevated_Node" + TexType._APRMap + ext_DDS)));

                                if (node.m_nodeMaterial.GetTexture(TexType._APRMap).name.Contains(RoadPos.Slope))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Slope_Node" + TexType._APRMap + ext_DDS)))
                                        node.m_nodeMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Slope_Node" + TexType._APRMap + ext_DDS)));

                                node.m_lodRenderDistance = 2500;
                            }

                            #endregion
                        }
                    }



                    NetInfo.Segment[] segments = netInfo.m_segments;
                    foreach (NetInfo.Segment segment in segments)
                    {
                        if (segment.m_segmentMaterial.GetTexture(TexType._MainTex) != null)
                        {
                            #region NExt TinyRoads Default

                            ReplaceNExtSegments(netInfo, segment);

                            #endregion

                            #region NExt Avenues Default

                            if (netInfo.name.Contains("Medium Avenue") && !netInfo.name.Contains("TL"))
                            {
                                if (segment.m_segmentMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Ground))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "MediumAvenue4L_Ground_Segment" + TexType._MainTex + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "MediumAvenue4L_Ground_Segment" + TexType._MainTex + ext_DDS)));

                                if (segment.m_segmentMaterial.GetTexture(TexType._APRMap).name.Contains(RoadPos.Ground))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeSegment-default-apr" + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeSegment-default-apr" + ext_DDS)));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "RoadLargeSegment-default-apr" + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "RoadLargeSegment-default-apr" + ext_DDS)));

                                if (
                                    segment.m_segmentMaterial.GetTexture(TexType._MainTex)
                                        .name.Contains(RoadPos.Elevated))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "MediumAvenue4L_Elevated_Segment" + TexType._MainTex + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "MediumAvenue4L_Elevated_Segment" + TexType._MainTex + ext_DDS)));

                                if (segment.m_segmentMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Slope))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "MediumAvenue4L_Slope_Segment" + TexType._MainTex + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "MediumAvenue4L_Slope_Segment" + TexType._MainTex + ext_DDS)));

                                if (segment.m_segmentMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Tunnel))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "MediumAvenue4L_Tunnel_Segment" + TexType._MainTex + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "MediumAvenue4L_Tunnel_Segment" + TexType._MainTex + ext_DDS)));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Medium Avenue") && netInfo.name.Contains("TL"))
                            {
                                if (segment.m_segmentMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Ground))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "MediumAvenue4LTL_Ground_Segment" + TexType._MainTex + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "MediumAvenue4LTL_Ground_Segment" + TexType._MainTex + ext_DDS)));

                                if (segment.m_segmentMaterial.GetTexture(TexType._APRMap).name.Contains(RoadPos.Ground))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeSegment-default-apr" + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeSegment-default-apr" + ext_DDS)));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "RoadLargeSegment-default-apr" + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "RoadLargeSegment-default-apr" + ext_DDS)));

                                if (
                                    segment.m_segmentMaterial.GetTexture(TexType._MainTex)
                                        .name.Contains(RoadPos.Elevated))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "MediumAvenue4LTL_Elevated_Segment" + TexType._MainTex + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "MediumAvenue4LTL_Elevated_Segment" + TexType._MainTex + ext_DDS)));

                                if (segment.m_segmentMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Slope))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "MediumAvenue4LTL_Slope_Segment" + TexType._MainTex + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "MediumAvenue4LTL_Slope_Segment" + TexType._MainTex + ext_DDS)));

                                if (segment.m_segmentMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Tunnel))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "MediumAvenue4LTL_Tunnel_Segment" + TexType._MainTex + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "MediumAvenue4LTL_Tunnel_Segment" + TexType._MainTex + ext_DDS)));

                                segment.m_lodRenderDistance = 2500;
                            }

                            #endregion

                            #region NExt Highways Default

                            if (netInfo.name.Contains("Rural Highway") && netInfo.name.Contains("Small"))
                            {
                                if (segment.m_segmentMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Ground))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Ground_Segment" + TexType._MainTex + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Ground_Segment" + TexType._MainTex + ext_DDS)));

                                if (
                                    segment.m_segmentMaterial.GetTexture(TexType._MainTex)
                                        .name.Contains(RoadPos.Elevated))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Ground_Segment" + TexType._MainTex + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Ground_Segment" + TexType._MainTex + ext_DDS)));

                                if (segment.m_segmentMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Slope))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Slope_Segment" + TexType._MainTex + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Slope_Segment" + TexType._MainTex + ext_DDS)));

                                if (segment.m_segmentMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Tunnel))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Tunnel_Segment" + TexType._MainTex + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Tunnel_Segment" + TexType._MainTex + ext_DDS)));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Rural Highway") && !netInfo.name.Contains("Small"))
                            {
                                if (segment.m_segmentMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Ground))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Segment" + TexType._MainTex + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Segment" + TexType._MainTex + ext_DDS)));

                                if (
                                    segment.m_segmentMaterial.GetTexture(TexType._MainTex)
                                        .name.Contains(RoadPos.Elevated))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Segment" + TexType._MainTex + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Segment" + TexType._MainTex + ext_DDS)));

                                if (segment.m_segmentMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Slope))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Slope_Segment" + TexType._MainTex + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Slope_Segment" + TexType._MainTex + ext_DDS)));

                                if (segment.m_segmentMaterial.GetTexture(TexType._MainTex).name.Contains(RoadPos.Tunnel))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Tunnel_Segment" + TexType._MainTex + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Tunnel_Segment" + TexType._MainTex + ext_DDS)));

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
                                            if (
                                                segment.m_segmentMaterial.GetTexture(TexType._MainTex)
                                                    .name.Contains(RoadPos.Ground))
                                            {
                                                if (
                                                    File.Exists(
                                                        Path.Combine(
                                                            ModLoader.currentTexturesPath_default,
                                                            "Busway2L1W_DecoGrass_Ground_Segment" + TexType._MainTex
                                                            + ext_DDS)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(
                                                        TexType._MainTex,
                                                        LoadTextureDDS(
                                                            Path.Combine(
                                                                ModLoader.currentTexturesPath_default,
                                                                "Busway2L1W_DecoGrass_Ground_Segment" + TexType._MainTex
                                                                + ext_DDS)));
                                                    segment.m_lodRenderDistance = 2500;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (
                                            segment.m_segmentMaterial.GetTexture(TexType._MainTex)
                                                .name.Contains(RoadPos.Ground))
                                        {
                                            if (
                                                File.Exists(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "Busway2L1W_Ground_Segment" + TexType._MainTex + ext_DDS)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    TexType._MainTex,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.currentTexturesPath_default,
                                                            "Busway2L1W_Ground_Segment" + TexType._MainTex + ext_DDS)));
                                                segment.m_lodRenderDistance = 2500;
                                            }
                                        }

                                        if (
                                            segment.m_segmentMaterial.GetTexture(TexType._MainTex)
                                                .name.Contains(RoadPos.Elevated))
                                        {
                                            if (
                                                File.Exists(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "Busway2L1W_Elevated_Segment" + TexType._MainTex + ext_DDS)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    TexType._MainTex,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.currentTexturesPath_default,
                                                            "Busway2L1W_Elevated_Segment" + TexType._MainTex + ext_DDS)));
                                                segment.m_lodRenderDistance = 2500;
                                            }
                                        }

                                        if (
                                            segment.m_segmentMaterial.GetTexture(TexType._MainTex)
                                                .name.Contains(RoadPos.Slope))
                                        {
                                            if (
                                                File.Exists(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "Busway2L1W_Slope_Segment" + TexType._MainTex + ext_DDS)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    TexType._MainTex,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.currentTexturesPath_default,
                                                            "Busway2L1W_Slope_Segment" + TexType._MainTex + ext_DDS)));
                                                segment.m_lodRenderDistance = 2500;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (netInfo.name.Contains("Decoration"))
                                    {
                                        if (netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees"))
                                        {
                                            if (
                                                segment.m_segmentMaterial.GetTexture(TexType._MainTex)
                                                    .name.Contains(RoadPos.Ground))
                                            {
                                                if (
                                                    File.Exists(
                                                        Path.Combine(
                                                            ModLoader.currentTexturesPath_default,
                                                            "Busway2L_DecoGrass_Ground_Segment" + TexType._MainTex
                                                            + ext_DDS)))
                                                {
                                                    segment.m_segmentMaterial.SetTexture(
                                                        TexType._MainTex,
                                                        LoadTextureDDS(
                                                            Path.Combine(
                                                                ModLoader.currentTexturesPath_default,
                                                                "Busway2L_DecoGrass_Ground_Segment" + TexType._MainTex
                                                                + ext_DDS)));
                                                    segment.m_lodRenderDistance = 2500;
                                                }
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
                                        if (
                                            segment.m_segmentMaterial.GetTexture(TexType._MainTex)
                                                .name.Contains(RoadPos.Ground))
                                            if (
                                                File.Exists(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "Busway2L_Ground_Segment" + TexType._MainTex + ext_DDS)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    TexType._MainTex,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.currentTexturesPath_default,
                                                            "Busway2L_Ground_Segment" + TexType._MainTex + ext_DDS)));
                                                segment.m_lodRenderDistance = 2500;
                                            }

                                        if (
                                            segment.m_segmentMaterial.GetTexture(TexType._MainTex)
                                                .name.Contains(RoadPos.Elevated))
                                            if (
                                                File.Exists(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "Busway2L_Elevated_Segment" + TexType._MainTex + ext_DDS)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    TexType._MainTex,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.currentTexturesPath_default,
                                                            "Busway2L_Elevated_Segment" + TexType._MainTex + ext_DDS)));
                                                segment.m_lodRenderDistance = 2500;
                                            }

                                        if (
                                            segment.m_segmentMaterial.GetTexture(TexType._MainTex)
                                                .name.Contains(RoadPos.Slope))
                                            if (
                                                File.Exists(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "Busway2L_Slope_Segment" + TexType._MainTex + ext_DDS)))
                                            {
                                                segment.m_segmentMaterial.SetTexture(
                                                    TexType._MainTex,
                                                    LoadTextureDDS(
                                                        Path.Combine(
                                                            ModLoader.currentTexturesPath_default,
                                                            "Busway2L_Slope_Segment" + TexType._MainTex + ext_DDS)));
                                                segment.m_lodRenderDistance = 2500;
                                            }
                                    }
                                }
                            }

                            #endregion

                            #region Large Busways

                            if (netInfo.name.Contains("Large Road") && netInfo.name.Contains("Bus Lanes"))
                            {
                                if (netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees"))
                                {
                                    if (
                                        segment.m_segmentMaterial.GetTexture(TexType._MainTex)
                                            .name.Contains(RoadPos.Ground))
                                        if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Busway6L_DecoGrass_Ground_Segment" + TexType._MainTex + ext_DDS)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(
                                                TexType._MainTex,
                                                LoadTextureDDS(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "Busway6L_DecoGrass_Ground_Segment" + TexType._MainTex + ext_DDS)));
                                            segment.m_lodRenderDistance = 2500;
                                        }

                                    if (
                                        segment.m_segmentMaterial.GetTexture(TexType._APRMap)
                                            .name.Contains(RoadPos.Ground))
                                        if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeSegment-default-apr" + ext_DDS)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(
                                                TexType._APRMap,
                                                LoadTextureDDS(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "RoadLargeSegment-default-apr" + ext_DDS)));
                                            segment.m_lodRenderDistance = 2500;
                                        }
                                        else if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "RoadLargeSegment-default-apr" + ext_DDS)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(
                                                TexType._APRMap,
                                                LoadTextureDDS(
                                                    Path.Combine(
                                                        ModLoader.APRMaps_Path,
                                                        "RoadLargeSegment-default-apr" + ext_DDS)));
                                            segment.m_lodRenderDistance = 2500;
                                        }
                                }
                                else
                                {
                                    if (
                                        segment.m_segmentMaterial.GetTexture(TexType._MainTex)
                                            .name.Contains(RoadPos.Ground))
                                        if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeBuslane_D" + ext_DDS)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(
                                                TexType._MainTex,
                                                LoadTextureDDS(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "RoadLargeBuslane_D" + ext_DDS)));
                                            segment.m_lodRenderDistance = 2500;
                                        }

                                    if (
                                        segment.m_segmentMaterial.GetTexture(TexType._MainTex)
                                            .name.Contains(RoadPos.Elevated))
                                        if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeElevatedBus_D" + ext_DDS)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(
                                                TexType._MainTex,
                                                LoadTextureDDS(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "RoadLargeElevatedBus_D" + ext_DDS)));
                                            segment.m_lodRenderDistance = 2500;
                                        }

                                    if (
                                        segment.m_segmentMaterial.GetTexture(TexType._MainTex)
                                            .name.Contains(RoadPos.Slope))
                                        if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "large-tunnelBus_d" + ext_DDS)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(
                                                TexType._MainTex,
                                                LoadTextureDDS(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "large-tunnelBus_d" + ext_DDS)));
                                            segment.m_lodRenderDistance = 2500;
                                        }

                                    if (
                                        segment.m_segmentMaterial.GetTexture(TexType._APRMap)
                                            .name.Contains(RoadPos.Ground))
                                        if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeSegment-default-apr" + ext_DDS)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(
                                                TexType._APRMap,
                                                LoadTextureDDS(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "RoadLargeSegment-default-apr" + ext_DDS)));
                                            segment.m_lodRenderDistance = 2500;
                                        }
                                        else if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "RoadLargeSegment-default-apr" + ext_DDS)))
                                        {
                                            segment.m_segmentMaterial.SetTexture(
                                                TexType._APRMap,
                                                LoadTextureDDS(
                                                    Path.Combine(
                                                        ModLoader.APRMaps_Path,
                                                        "RoadLargeSegment-default-apr" + ext_DDS)));
                                            segment.m_lodRenderDistance = 2500;
                                        }
                                }
                            }

                            #endregion

                            #endregion
                        }

                        if (segment.m_segmentMaterial.GetTexture(TexType._APRMap) != null)
                        {
                            #region NExt Highways APRMaps

                            if (netInfo.name.Contains("Rural Highway") && netInfo.name.Contains("Small"))
                            {
                                if (segment.m_segmentMaterial.GetTexture(TexType._APRMap).name.Contains(RoadPos.Ground))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Ground_Segment" + TexType._APRMap + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Ground_Segment" + TexType._APRMap + ext_DDS)));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "Highway1L_Ground_Segment" + TexType._APRMap + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway1L_Ground_Segment" + TexType._APRMap + ext_DDS)));

                                if (segment.m_segmentMaterial.GetTexture(TexType._APRMap)
                                    .name.Contains(RoadPos.Elevated))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Ground_Segment" + TexType._APRMap + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Ground_Segment" + TexType._APRMap + ext_DDS)));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "Highway1L_Ground_Segment" + TexType._APRMap + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway1L_Ground_Segment" + TexType._APRMap + ext_DDS)));

                                if (segment.m_segmentMaterial.GetTexture(TexType._APRMap).name.Contains(RoadPos.Slope))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Slope_Segment" + TexType._APRMap + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Slope_Segment" + TexType._APRMap + ext_DDS)));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "Highway1L_Slope_Segment" + TexType._APRMap + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway1L_Slope_Segment" + TexType._APRMap + ext_DDS)));

                                if (segment.m_segmentMaterial.GetTexture(TexType._APRMap).name.Contains(RoadPos.Tunnel))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway1L_Tunnel_Segment" + TexType._APRMap + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway1L_Tunnel_Segment" + TexType._APRMap + ext_DDS)));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "Highway1L_Tunnel_Segment" + TexType._APRMap + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway1L_Tunnel_Segment" + TexType._APRMap + ext_DDS)));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Rural Highway") && !netInfo.name.Contains("Small"))
                            {
                                if (segment.m_segmentMaterial.GetTexture(TexType._APRMap).name.Contains(RoadPos.Ground))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Segment" + TexType._APRMap + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Segment" + TexType._APRMap + ext_DDS)));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "Highway2L_Ground_Segment" + TexType._APRMap + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway2L_Ground_Segment" + TexType._APRMap + ext_DDS)));

                                if (segment.m_segmentMaterial.GetTexture(TexType._APRMap)
                                    .name.Contains(RoadPos.Elevated))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Ground_Segment" + TexType._APRMap + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Ground_Segment" + TexType._APRMap + ext_DDS)));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "Highway2L_Ground_Segment" + TexType._APRMap + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway2L_Ground_Segment" + TexType._APRMap + ext_DDS)));

                                if (segment.m_segmentMaterial.GetTexture(TexType._APRMap).name.Contains(RoadPos.Slope))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Slope_Segment" + TexType._APRMap + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Slope_Segment" + TexType._APRMap + ext_DDS)));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "Highway2L_Slope_Segment" + TexType._APRMap + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway2L_Slope_Segment" + TexType._APRMap + ext_DDS)));

                                if (segment.m_segmentMaterial.GetTexture(TexType._APRMap).name.Contains(RoadPos.Tunnel))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Highway2L_Tunnel_Segment" + TexType._APRMap + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Highway2L_Tunnel_Segment" + TexType._APRMap + ext_DDS)));
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "Highway2L_Tunnel_Segment" + TexType._APRMap + ext_DDS)))
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._APRMap,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.APRMaps_Path,
                                                    "Highway2L_Tunnel_Segment" + TexType._APRMap + ext_DDS)));

                                segment.m_lodRenderDistance = 2500;
                            }

                            #endregion
                        }
                    }


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
                    foreach (NetInfo.Node node in nodes)
                    {
                        if (node.m_nodeMaterial.GetTexture(TexType._MainTex) != null)
                        {
                            string nodeMaterialTexture_name = Path.Combine(
                                ModLoader.currentTexturesPath_default,
                                node.m_nodeMaterial.GetTexture(TexType._MainTex).name + ext_DDS);

                            // string prefab_road_name = netInfo.name.Replace(" ", "_").ToLowerInvariant().Trim();

                            // if (File.Exists(prefab_road_name + "_" + nodeMaterialTexture_name))   //still needed?
                            // node.m_nodeMaterial.SetTexture(TexType._MainTex, LoadTextureDDS(prefab_road_name + "_" + nodeMaterialTexture_name));
                            if (File.Exists(nodeMaterialTexture_name))
                                node.m_nodeMaterial.SetTexture(
                                    TexType._MainTex,
                                    LoadTextureDDS(nodeMaterialTexture_name));

                            node.m_lodRenderDistance = 2500;
                            node.m_lodMesh = null;
                        }

                        if (node.m_nodeMaterial.GetTexture(TexType._APRMap) != null)
                        {
                            string nodeMaterialAPRMap_name = Path.Combine(
                                ModLoader.currentTexturesPath_default,
                                node.m_nodeMaterial.GetTexture(TexType._APRMap).name + ext_DDS);

                            if (File.Exists(nodeMaterialAPRMap_name))
                            {
                                node.m_nodeMaterial.SetTexture(TexType._APRMap, LoadTextureDDS(nodeMaterialAPRMap_name));
                            }
                        }
                    }

                    // Look for segments
                    NetInfo.Segment[] segments = netInfo.m_segments;
                    foreach (NetInfo.Segment segment in segments)
                    {
                        if (segment.m_segmentMaterial.GetTexture(TexType._MainTex) != null)
                        {
                            string segmentMaterialTexture_name = Path.Combine(
                                ModLoader.currentTexturesPath_default,
                                segment.m_segmentMaterial.GetTexture(TexType._MainTex).name + ext_DDS);

                            // I'm combining cofiguration with this region to sort each item by Network Type/Class
                            // Also combining if statements where && is appropriate
                            // Killface: won't use your code as it's bloated. don't want 10,000 lines of a crappy forced naming scheme which breaks working functions
                            // #region Oneways


                            if (netInfo.name.Contains("Oneway"))
                            {
                                #region Small Oneway

                                if (segment.m_segmentMaterial.GetTexture(TexType._MainTex).name == "RoadSmallSegment")
                                {
                                    filename2 = "Oneway_RoadSmallSegment" + ext_DDS;
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, filename2)))
                                    {
                                        filename = filename2;
                                    }

                                    if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                    {
                                        filename2 = "Oneway_RoadSmallSegment_BusSide" + ext_DDS;
                                        if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, filename2)))
                                        {
                                            filename = filename2;
                                        }
                                    }

                                    if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                                    {
                                        filename2 = "Oneway_RoadSmallSegment_BusBoth" + ext_DDS;
                                        if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, filename2)))
                                        {
                                            filename = filename2;
                                        }
                                    }

                                }

                                if (segment.m_segmentMaterial.GetTexture(TexType._MainTex).name
                                    == "SmallRoadSegmentDeco")
                                {
                                    if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide")
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Oneway_SmallRoadSegmentDeco_BusSide" + ext_DDS)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Oneway_SmallRoadSegmentDeco_BusSide" + ext_DDS)));
                                    }
                                    else if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth")
                                             && File.Exists(
                                                 Path.Combine(
                                                     ModLoader.currentTexturesPath_default,
                                                     "Oneway_SmallRoadSegmentDeco_BusBoth" + ext_DDS)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Oneway_SmallRoadSegmentDeco_BusBoth" + ext_DDS)));
                                    }
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Oneway_SmallRoadSegmentDeco" + ext_DDS)))
                                    {
                                        segment.m_segmentMaterial.SetTexture(
                                            TexType._MainTex,
                                            LoadTextureDDS(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "Oneway_SmallRoadSegmentDeco" + ext_DDS)));
                                    }
                                }

                                if (segment.m_segmentMaterial.GetTexture(TexType._MainTex).name == "small-tunnel_d"
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "Oneway_small-tunnel_d" + ext_DDS)))
                                {
                                    segment.m_segmentMaterial.SetTexture(
                                        TexType._MainTex,
                                        LoadTextureDDS(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Oneway_small-tunnel_d" + ext_DDS)));
                                }

                                if (segment.m_segmentMaterial.GetTexture(TexType._MainTex).name
                                    == "RoadSmallElevatedSegment"
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "Oneway_RoadSmallElevatedSegment_D" + ext_DDS)))
                                {
                                    segment.m_segmentMaterial.SetTexture(
                                        TexType._MainTex,
                                        LoadTextureDDS(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "Oneway_RoadSmallElevatedSegment_D" + ext_DDS)));
                                }

                                #endregion

                                #region Large Oneway

                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeOnewaySegment_d_BusSide" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeOnewaySegment_d_BusSide" + ext_DDS);
                                }

                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeOnewaySegment_d_BusBoth" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeOnewaySegment_d_BusBoth" + ext_DDS);
                                }

                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMedium_D_BusBoth" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeOnewaySegment_d_BusSide" + ext_DDS);
                                }

                                // this texture might not be in use
                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMedium_D_BusBoth" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeOnewaySegment_d_BusSide" + ext_DDS);
                                }

                                #endregion
                            }

                            if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                            {
                                if (!netInfo.name.Contains("Bicycle")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadSmall_D_BusSide" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadSmall_D_BusSide" + ext_DDS);
                                }

                                if (
                                    File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "SmallRoadSegmentDeco_BusSide" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "SmallRoadSegmentDeco_BusSide" + ext_DDS);
                                }
                            }

                            if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                            {
                                if (!netInfo.name.Contains("Bicycle")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadSmall_D_BusBoth" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadSmall_D_BusBoth" + ext_DDS);
                                }
                            }

                            if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth")
                                && File.Exists(
                                    Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "SmallRoadSegmentDeco_BusSide" + ext_DDS)))
                            {
                                segmentMaterialTexture_name = Path.Combine(
                                    ModLoader.currentTexturesPath_default,
                                    "SmallRoadSegmentDeco_BusBoth" + ext_DDS);
                            }

                            if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                            {
                                if (netInfo.name.Contains("Trees"))
                                {
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMediumDeco_d_BusSide" + ext_DDS)))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumDeco_d_BusSide" + ext_DDS);
                                    else if (netInfo.name.Contains("Bus"))
                                        if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadMediumBusLane_BusSide" + ext_DDS)))
                                            segmentMaterialTexture_name =
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadMediumBusLane_BusSide" + ext_DDS);
                                        else if (!netInfo.name.Contains("Bicycle"))
                                            if (
                                                File.Exists(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "RoadMedium_D_BusSide" + ext_DDS)))
                                                segmentMaterialTexture_name =
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "RoadMedium_D_BusSide" + ext_DDS);
                                }

                                goto configsettings;
                            }

                            if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                            {
                                if (netInfo.name.Contains("Trees"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMediumDeco_d_BusBoth" + ext_DDS)))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumDeco_d_BusBoth" + ext_DDS);
                                    else if (netInfo.name.Contains("Bus"))
                                        if (
                                            File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadMediumBusLane_BusBoth" + ext_DDS)))
                                            segmentMaterialTexture_name =
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadMediumBusLane_BusBoth" + ext_DDS);
                                        else if (!netInfo.name.Contains("Bicycle"))
                                            if (
                                                File.Exists(
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "RoadMedium_D_BusBoth" + ext_DDS)))
                                                segmentMaterialTexture_name =
                                                    Path.Combine(
                                                        ModLoader.currentTexturesPath_default,
                                                        "RoadMedium_D_BusBoth" + ext_DDS);
                                goto configsettings;
                            }

                            if (!(netInfo.name.Contains("Bicycle") || netInfo.name.Contains("Oneway")))
                            {
                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegment_d_BusSide" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeSegment_d_BusSide" + ext_DDS);
                                }

                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegment_d_BusBoth" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeSegment_d_BusBoth" + ext_DDS);
                                }

                                if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegmentDecoBusBoth_d" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeSegmentDecoBusBoth_d" + ext_DDS);
                                }

                                if (segment.m_mesh.name.Equals("RoadLargeSegmentBusSideBusLane")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeBuslane_D_BusSide" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeBuslane_D_BusSide" + ext_DDS);
                                }

                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBothBusLane")
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeBuslane_D_BusBoth" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeBuslane_D_BusBoth" + ext_DDS);
                                }
                            }

                            Debug.Log(segmentMaterialTexture_name);



                            configsettings:

                            #region configsettings

                            if (ModLoader.config.basic_road_parking == 1)
                            {
                                if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmall_D" + ext_DDS)))
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadSmall_D_parking1" + ext_DDS);

                                if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadSmall_D_BusSide" + ext_DDS)))
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadSmall_D_BusSide_parking1" + ext_DDS);
                            }

                            if (ModLoader.config.medium_road_parking == 1)
                            {
                                if (!(netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees")))
                                {
                                    if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                    {
                                        if (
                                            segmentMaterialTexture_name.Equals(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadMediumSegment_d" + ext_DDS))
                                            && File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadMedium_D_BusSide_parking1" + ext_DDS)))
                                        {
                                            segmentMaterialTexture_name =
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadMedium_D_BusSide_parking1" + ext_DDS);
                                        }
                                    }
                                    else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                    {
                                        if (
                                            segmentMaterialTexture_name.Equals(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadMediumSegment_d" + ext_DDS))
                                            && File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadMedium_D_BusBoth_parking1" + ext_DDS)))
                                        {
                                            segmentMaterialTexture_name =
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadMedium_D_BusBoth_parking1" + ext_DDS);
                                        }
                                    }
                                    else if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMediumSegment_d" + ext_DDS))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMedium_D_parking1" + ext_DDS)))
                                    {
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMedium_D_parking1" + ext_DDS);
                                    }
                                    else if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMedium_D" + ext_DDS))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMedium_D_parking1" + ext_DDS)))
                                    {
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMedium_D_parking1" + ext_DDS);
                                    }
                                }
                            }

                            if ((ModLoader.config.medium_road_grass_parking == 1) && netInfo.name.Contains("Grass"))
                            {
                                if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                {
                                    if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMedium_D" + ext_DDS))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMedium_D_BusSide_parking1" + ext_DDS)))
                                    {
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMedium_D_BusSide_parking1" + ext_DDS);
                                    }
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                {
                                    if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMedium_D" + ext_DDS))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMedium_D_BusBoth_parking1" + ext_DDS)))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMedium_D_BusBoth_parking1" + ext_DDS);
                                }
                                else if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumSegment_d" + ext_DDS))
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMedium_D_parking1" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadMedium_D_parking1" + ext_DDS);
                                }
                                else if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D" + ext_DDS))
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMedium_D_parking1" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadMedium_D_parking1" + ext_DDS);
                                }
                            }

                            if ((ModLoader.config.medium_road_trees_parking == 1) && netInfo.name.Contains("Trees"))
                            {
                                if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                {
                                    if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMediumDeco_d" + ext_DDS))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMediumDeco_d_BusSide_parking1" + ext_DDS)))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumDeco_d_BusSide_parking1" + ext_DDS);
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth")
                                         && File.Exists(
                                             Path.Combine(
                                                 ModLoader.currentTexturesPath_default,
                                                 "RoadMediumDeco_d" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadMediumDeco_d" + ext_DDS);
                                }
                                else if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumDeco_d" + ext_DDS))
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumDeco_d_parking1" + ext_DDS)))
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadMediumDeco_d_parking1" + ext_DDS);
                            }

                            if (ModLoader.config.medium_road_bus_parking == 1)
                            {
                                if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                                {
                                    if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMediumBusLane" + ext_DDS))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMediumBusLane_BusSide_parking1" + ext_DDS)))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumBusLane_BusSide_parking1" + ext_DDS);
                                }
                                else if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                                {
                                    if (
                                        segmentMaterialTexture_name.Equals(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMediumBusLane" + ext_DDS))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadMediumBusLane_BusBoth_parking1" + ext_DDS)))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumBusLane_BusBoth_parking1" + ext_DDS);
                                }
                                else if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumBusLane" + ext_DDS))
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadMediumBusLane_parking1" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadMediumBusLane_parking1" + ext_DDS);
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
                                                "RoadLargeSegment_d" + ext_DDS))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeSegment_d_BusSide_parking1" + ext_DDS)))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegment_d_BusSide_parking1" + ext_DDS);
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                {
                                    if (
                                            segmentMaterialTexture_name.Equals(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeSegment_d" + ext_DDS))
                                            && File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeSegment_d_BusBoth_parking1" + ext_DDS)))

                                        // might be changed back to default segment
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegment_d_BusBoth_parking1" + ext_DDS);
                                }
                                else if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegment_d" + ext_DDS))
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegment_d_parking1" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeSegment_d_parking1" + ext_DDS);
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
                                                "RoadLargeOnewaySegment_d" + ext_DDS))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeOnewaySegment_d_BusSide_parking1" + ext_DDS)))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeOnewaySegment_d_BusSide_parking1" + ext_DDS);
                                }
                                else if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeOnewaySegment_d" + ext_DDS))
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeOnewaySegment_d_parking1" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeOnewaySegment_d_parking1" + ext_DDS);
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
                                                "RoadLargeBuslane_D" + ext_DDS))
                                        && File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeBuslane_D_BusSide_parking1" + ext_DDS)))
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeBuslane_D_BusSide_parking1" + ext_DDS);
                                }
                                else if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBothBusLane"))
                                {
                                    if (
                                            segmentMaterialTexture_name.Equals(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeBuslane_D" + ext_DDS))
                                            && File.Exists(
                                                Path.Combine(
                                                    ModLoader.currentTexturesPath_default,
                                                    "RoadLargeBuslane_D_BusBoth_parking1" + ext_DDS)))

                                        // might be changed back to default segment
                                        segmentMaterialTexture_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeBuslane_D_BusBoth_parking1" + ext_DDS);
                                }
                                else if (
                                    segmentMaterialTexture_name.Equals(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeBuslane_D" + ext_DDS))
                                    && File.Exists(
                                        Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeBuslane_D_parking1" + ext_DDS)))
                                {
                                    segmentMaterialTexture_name = Path.Combine(
                                        ModLoader.currentTexturesPath_default,
                                        "RoadLargeBuslane_D_parking1" + ext_DDS);
                                }
                            }

                            #endregion

                            // Replace the default segment textures
                            if (File.Exists(segmentMaterialTexture_name))
                                segment.m_segmentMaterial.SetTexture(
                                    TexType._MainTex,
                                    LoadTextureDDS(segmentMaterialTexture_name));



                        }

                        if (segment.m_segmentMaterial.GetTexture(TexType._APRMap) != null)
                        {
                            string segmentMaterialAPRMap_name = Path.Combine(
                                ModLoader.currentTexturesPath_default,
                                segment.m_segmentMaterial.GetTexture(TexType._APRMap).name + ext_DDS);
                            Debug.Log(segmentMaterialAPRMap_name);
                            {
                                // APRs!!!!!
                                if (
                                    segment.m_segmentMaterial.GetTexture(TexType._APRMap)
                                        .name.Equals("LargeRoadSegmentBusSide-BikeLane-apr")
                                    || segment.m_segmentMaterial.GetTexture(TexType._APRMap)
                                        .name.Equals("LargeRoadSegmentBusBoth-BikeLane-apr"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeSegment-BikeLane-apr" + ext_DDS)))
                                        segmentMaterialAPRMap_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegment-BikeLane-apr" + ext_DDS);
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "RoadLargeSegment-BikeLane-apr" + ext_DDS)))
                                        segmentMaterialAPRMap_name = Path.Combine(
                                            ModLoader.APRMaps_Path,
                                            "RoadLargeSegment-BikeLane-apr" + ext_DDS);
                                if (
                                    segment.m_segmentMaterial.GetTexture(TexType._APRMap)
                                        .name.Equals("LargeRoadSegmentBusSide-LargeRoadSegmentBusSide-apr")
                                    || segment.m_segmentMaterial.GetTexture(TexType._APRMap)
                                        .name.Equals("LargeRoadSegmentBusBoth-LargeRoadSegmentBusBoth-apr"))
                                    if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.currentTexturesPath_default,
                                                "RoadLargeSegment-default-apr" + ext_DDS)))
                                        segmentMaterialAPRMap_name = Path.Combine(
                                            ModLoader.currentTexturesPath_default,
                                            "RoadLargeSegment-default-apr" + ext_DDS);
                                    else if (
                                        File.Exists(
                                            Path.Combine(
                                                ModLoader.APRMaps_Path,
                                                "RoadLargeSegment-default-apr" + ext_DDS)))
                                        segmentMaterialAPRMap_name = Path.Combine(
                                            ModLoader.APRMaps_Path,
                                            "RoadLargeSegment-default-apr" + ext_DDS);

                                if (File.Exists(segmentMaterialAPRMap_name))
                                    segment.m_segmentMaterial.SetTexture(
                                        TexType._APRMap,
                                        LoadTextureDDS(segmentMaterialAPRMap_name));
                            }
                        }

                        segment.m_lodRenderDistance = 2500;
                    }
                }
            }

            // Singleton<NetManager>.instance.m_lodRgbAtlas = null;
            // Singleton<NetManager>.instance.m_lodAprAtlas = null;
            // Singleton<NetManager>.instance.m_lodXysAtlas = null;

            // Singleton<NetManager>.instance.InitRenderData();
        }

        public void UpdateRenderer()
        {
        }

        private static void ReplaceNExtNodes(NetInfo netInfo, NetInfo.Node node)
        {
            foreach (KeyValuePair<string, string> road in RU_CoreDicts.NExtRoads)
            {
                if (netInfo.name.Contains(road.Key))
                {
                    foreach (string roadPosition in RoadPos.AllPositions)
                    {
                        foreach (string textype in TexType.AllTex)
                        {
                            if (node.m_nodeMaterial.GetTexture(textype).name.Contains(roadPosition))
                            {
                                string filename = road.Value + "_" + roadPosition + "_" + "Node" + textype + ext_DDS;
                                if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, filename)))
                                    node.m_nodeMaterial.SetTexture(
                                        textype,
                                        LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, filename)));
                                else if (textype == TexType._APRMap
                                         && File.Exists(Path.Combine(ModLoader.APRMaps_Path, filename)))
                                    node.m_nodeMaterial.SetTexture(
                                        textype,
                                        LoadTextureDDS(Path.Combine(ModLoader.APRMaps_Path, filename)));
                            }
                        }

                        node.m_lodRenderDistance = 2500;
                    }
                }
            }
        }

        private static void ReplaceNExtSegments(NetInfo netInfo, NetInfo.Segment segment)
        {
            foreach (KeyValuePair<string, string> road in RU_CoreDicts.NExtRoads)
            {
                if (netInfo.name.Contains(road.Key))
                {
                    foreach (string roadPosition in RoadPos.AllPositions)
                    {
                        foreach (string textype in TexType.AllTex)
                        {
                            if (segment.m_segmentMaterial.GetTexture(textype).name.Contains(roadPosition))
                            {
                                string filename = road.Value + "_" + roadPosition + "_" + "Segment" + textype + ext_DDS;
                                if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, filename)))
                                    segment.m_segmentMaterial.SetTexture(
                                        textype,
                                        LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, filename)));
                                else if (textype == TexType._APRMap
                                         && File.Exists(Path.Combine(ModLoader.APRMaps_Path, filename)))
                                    segment.m_segmentMaterial.SetTexture(
                                        textype,
                                        LoadTextureDDS(Path.Combine(ModLoader.APRMaps_Path, filename)));
                            }

                            segment.m_lodRenderDistance = 2500;
                        }
                    }
                }
            }
        }
    }
}