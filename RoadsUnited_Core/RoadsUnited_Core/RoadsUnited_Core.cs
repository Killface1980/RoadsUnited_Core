using ColossalFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

namespace RoadsUnited_Core
{

    public class RoadsUnited_Core : MonoBehaviour

    {

        public static Configuration config;

        private static Dictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D>();

        public static Dictionary<string, Texture2D> vanillaPrefabProperties = new Dictionary<string, Texture2D>();

        //        public static Dictionary<string, Texture2D> currentPrefabProperties = new Dictionary<string, Texture2D>();


        private static Texture2D defaultmap;

        private static Texture2D aprmap;


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
                if (index > (int)sbyte.MaxValue)
                    list.Add(numArray[index]);
            }
            texture.LoadRawTextureData(list.ToArray());
            texture.name = Path.GetFileName(fullPath);
            texture.anisoLevel = 8;
            texture.Apply();

            textureCache.Add(fullPath, texture); // Neu geladene Textur in den Cache packen

            return texture;
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

                    if (!(
                        //        netInfo.m_class.name.Contains("NExt") ||
                        netInfo.m_class.name.Contains("Heating Pipe") ||
                        netInfo.m_class.name.Contains("Water") ||
                        netInfo.m_class.name.Contains("Train") ||
                        netInfo.m_class.name.Contains("Metro") ||
                        netInfo.m_class.name.Contains("Transport") ||
                        netInfo.m_class.name.Contains("Bus Line") ||
                        netInfo.m_class.name.Contains("Airplane") ||
                        netInfo.m_class.name.Contains("Ship")
                        ))

                    {
                        if (node.m_nodeMaterial != null)
                        {
                            vanillaPrefabProperties.Add(prefab_road_name + "_node_" + (k) + "_nodeMaterial" + "_MainTex", node.m_nodeMaterial.GetTexture("_MainTex") as Texture2D);
                            vanillaPrefabProperties.Add(prefab_road_name + "_node_" + (k) + "_nodeMaterial" + "_APRMap", node.m_nodeMaterial.GetTexture("_APRMap") as Texture2D);
                        }
                    }
                }

                NetInfo.Segment[] segments = netInfo.m_segments;
                for (int l = 0; l < segments.Length; l++)
                {
                    NetInfo.Segment segment = segments[l];

                    if (!(
                        //            netInfo.m_class.name.Contains("NExt") ||
                        netInfo.m_class.name.Contains("Heating Pipe") ||
                        netInfo.m_class.name.Contains("Water") ||
                        netInfo.m_class.name.Contains("Train") ||
                        netInfo.m_class.name.Contains("Metro") ||
                        netInfo.m_class.name.Contains("Transport") ||
                        netInfo.m_class.name.Contains("Bus Line") ||
                        netInfo.m_class.name.Contains("Airplane") ||
                        netInfo.m_class.name.Contains("Ship")

                        ))
                    {

                        if (segment.m_segmentMaterial != null)
                        {
                            vanillaPrefabProperties.Add(prefab_road_name + "_segment_" + (l) + "_segmentMaterial" + "_MainTex", segment.m_segmentMaterial.GetTexture("_MainTex") as Texture2D);
                            vanillaPrefabProperties.Add(prefab_road_name + "_segment_" + (l) + "_segmentMaterial" + "_APRMap", segment.m_segmentMaterial.GetTexture("_APRMap") as Texture2D);
                        }
                    }
                }


            }
        }

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
                        //netInfo.m_class.name.Contains("NExt") ||
                        netInfo.m_class.name.Contains("Heating Pipe") ||
                        netInfo.m_class.name.Contains("Water") ||
                        netInfo.m_class.name.Contains("Train") ||
                        netInfo.m_class.name.Contains("Metro") ||
                        netInfo.m_class.name.Contains("Transport") ||
                        netInfo.m_class.name.Contains("Bus Line") ||
                        netInfo.m_class.name.Contains("Airplane") ||
                        netInfo.m_class.name.Contains("Ship")

                        ))
                    {

                        if (node.m_nodeMaterial != null)
                        {
                            if (vanillaPrefabProperties.TryGetValue(prefab_road_name + "_node_" + (k) + "_nodeMaterial" + "_MainTex", out defaultmap)) node.m_nodeMaterial.SetTexture("_MainTex", defaultmap);
                            if (vanillaPrefabProperties.TryGetValue(prefab_road_name + "_node_" + (k) + "_nodeMaterial" + "_APRMap", out aprmap)) node.m_nodeMaterial.SetTexture("_APRMap", aprmap);
                        }
                    }
                }





                NetInfo.Segment[] segments = netInfo.m_segments;
                for (int l = 0; l < segments.Length; l++)
                {
                    NetInfo.Segment segment = segments[l];

                    if (!(
                        //netInfo.m_class.name.Contains("NExt") ||
                        netInfo.m_class.name.Contains("Heating Pipe") || netInfo.m_class.name.Contains("Water") ||
                        netInfo.m_class.name.Contains("Train") ||
                        netInfo.m_class.name.Contains("Metro") ||
                        netInfo.m_class.name.Contains("Transport") ||
                        netInfo.m_class.name.Contains("Bus Line") ||
                        netInfo.m_class.name.Contains("Airplane") ||
                        netInfo.m_class.name.Contains("Ship")

                        ))
                    {
                        if (segment.m_segmentMaterial != null)
                        {
                            if (vanillaPrefabProperties.TryGetValue(prefab_road_name + "_segment_" + (l) + "_segmentMaterial" + "_MainTex", out defaultmap)) segment.m_segmentMaterial.SetTexture("_MainTex", defaultmap);
                            if (vanillaPrefabProperties.TryGetValue(prefab_road_name + "_segment_" + (l) + "_segmentMaterial" + "_APRMap", out aprmap)) segment.m_segmentMaterial.SetTexture("_APRMap", aprmap);
                        }
                    }
                }


            }
        }

        

        public static void ReplaceNetTextures()
        {
            if (ModLoader.config.texturePackPath != null)
            {
            ModLoader.currentTexturesPath_default = Path.Combine(ModLoader.config.texturePackPath, "BaseTextures");
                ModLoader.currentTexturesPath_apr_maps = Path.Combine(ModLoader.config.texturePackPath, "APRMaps");
            }

            for (uint i = 0; i < PrefabCollection<NetInfo>.LoadedCount(); i++)
            {
                var netInfo = PrefabCollection<NetInfo>.GetLoaded(i);

                if (netInfo == null) continue;

                if (netInfo.m_class.name.Contains("NExt"))
                {
                    NetInfo.Segment[] segments = netInfo.m_segments;
                    for (int l = 0; l < segments.Length; l++)
                    {
                        NetInfo.Segment segment = segments[l];



                        if (segment.m_segmentMaterial.GetTexture("_MainTex") != null)
                        {

                            #region NExt SmallHeavyRoads Default

                            if (netInfo.name.Contains("BasicRoadTL"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "BasicRoadTL_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "BasicRoadTL_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "BasicRoadTL_Elevated_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "BasicRoadTL_Elevated_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "BasicRoadTL_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "BasicRoadTL_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;

                            }

                            if (netInfo.name.Contains("Oneway3L"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "BasicRoadTL_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay3L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    //                   if ((netInfo.name.Contains("Elevated") || (netInfo.name.Contains("Bridge"))))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay3L_Elevated_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay3L_Elevated_MainTex.dds")));

                                //            if ((netInfo.name.Contains("Slope") || (netInfo.name.Contains("Tunnel"))))
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay3L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay3L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Oneway4L"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay4L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay4L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay4L_Elevated_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay4L_Elevated_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay4L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay4L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Small Avenue"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "SmallAvenue4L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "SmallAvenue4L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "SmallAvenue4L_Elevated_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "SmallAvenue4L_Elevated_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "SmallAvenue4L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "SmallAvenue4L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            #endregion

                            #region NExt Avenues Default

                            if (netInfo.name.Contains("Medium Avenue") && !netInfo.name.Contains("TL"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "MediumAvenue4L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "MediumAvenue4L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_apr_maps, "RoadLargeSegment-default-apr.dds")))
                                        segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_apr_maps, "RoadLargeSegment-default-apr.dds")));



                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "MediumAvenue4L_Elevated_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "MediumAvenue4L_Elevated_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "MediumAvenue4L_Slope_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "MediumAvenue4L_Slope_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "MediumAvenue4L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "MediumAvenue4L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Medium Avenue") && netInfo.name.Contains("TL"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "MediumAvenue4LTL_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "MediumAvenue4LTL_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_apr_maps, "RoadLargeSegment-default-apr.dds")))
                                        segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_apr_maps, "RoadLargeSegment-default-apr.dds")));


                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "MediumAvenue4LTL_Elevated_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "MediumAvenue4LTL_Elevated_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "MediumAvenue4LTL_Slope_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "MediumAvenue4LTL_Slope_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "MediumAvenue4LTL_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "MediumAvenue4LTL_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }


                            if (netInfo.name.Contains("Eight-Lane Avenue"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "LargeAvenue8LM_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "LargeAvenue8LM_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "LargeAvenue8LM_Elevated_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "LargeAvenue8LM_Elevated_MainTex.dds")));

                                if (false) // this buggy ATM
                                {
                                    if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                        if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "LargeAvenue8LM_Slope_Segment_MainTex.dds")))
                                            segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "LargeAvenue8LM_Slope_Segment_MainTex.dds")));

                                    if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                        if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "LargeAvenue8LM_Tunnel_Segment_MainTex.dds")))
                                            segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "LargeAvenue8LM_Tunnel_Segment_MainTex.dds")));
                                }

                                segment.m_lodRenderDistance = 2500;

                            }

                            #endregion

                            #region NExt Highways Default

                            if (netInfo.name.Contains("Rural Highway") && netInfo.name.Contains("Small"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway1L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway1L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway1L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway1L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway1L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway1L_Slope_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway1L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway1L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Rural Highway") && !netInfo.name.Contains("Small"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway2L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway2L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway2L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway2L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway2L_Slope_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway2L_Slope_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway2L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway2L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Four-Lane Highway"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway4L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway4L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway4L_Elevated_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway4L_Elevated_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway4L_Slope_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway4L_Slope_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway4L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway4L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Five-Lane Highway"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway5L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway5L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway5L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway5L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway5L_Slope_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway5L_Slope_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway5L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway5L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            if (netInfo.name.Contains("Large Highway"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway6L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway6L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway6L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway6L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Slope"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway6L_Slope_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway6L_Slope_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Highway6L_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Highway6L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;
                            }

                            #endregion

                            #region NExt Busways Default

                            if (netInfo.name.Contains("Small Busway") && !(netInfo.name.Contains("Decoration")))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Busway2L_Ground_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Busway2L_Ground_Segment_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "Busway2L_Elevated_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Busway2L_Elevated_MainTex.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_MainTex").name.Contains("Tunnel"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "BasicRoadTL_Tunnel_Segment_MainTex.dds")))
                                        segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "Busway2L_Tunnel_Segment_MainTex.dds")));

                                segment.m_lodRenderDistance = 2500;

                            }



                            #endregion


                        }



                        if (segment.m_segmentMaterial.GetTexture("_APRMap") != null)
                        {
                            #region SmallHeavyRoads APR Maps

                            if (netInfo.name.Contains("BasicRoadTL"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "BasicRoadTL_Ground_Segment_APRMap.dds")))
                                    segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "BasicRoadTL_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "BasicRoadTL_Elevated_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "BasicRoadTL_Elevated_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Tunnel"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "BasicRoadTL_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "BasicRoadTL_Tunnel_Segment_APRMap.dds")));
                            }

                            if (netInfo.name.Contains("Oneway3L"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay3L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay3L_Ground_Segment_APRMap.dds")));

                                //   if ((netInfo.name.Contains("Elevated") || (netInfo.name.Contains("Bridge"))))
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay3L_Elevated_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay3L_Elevated_APRMap.dds")));

                                //  if ((netInfo.name.Contains("Slope") || (netInfo.name.Contains("Tunnel"))))
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Tunnel"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay3L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay3L_Tunnel_Segment_APRMap.dds")));
                            }

                            if (netInfo.name.Contains("Oneway4L"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay4L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay4L_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay4L_Elevated_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay4L_Elevated_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Tunnel"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay4L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "OneWay4L_Tunnel_Segment_APRMap.dds")));
                            }

                            if (netInfo.name.Contains("Small Avenue"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "SmallAvenue4L_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "SmallAvenue4L_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "SmallAvenue4L_Elevated_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "SmallAvenue4L_Elevated_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Tunnel"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "SmallAvenue4L_Tunnel_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "SmallAvenue4L_Tunnel_Segment_APRMap.dds")));

                                segment.m_lodRenderDistance = 2500;

                            }

                            #endregion

                            #region Avenues APR Maps


                            if (netInfo.name.Contains("Eight-Lane Avenue"))
                            {
                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Ground"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "LargeAvenue8LM_Ground_Segment_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "LargeAvenue8LM_Ground_Segment_APRMap.dds")));

                                if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Elevated"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "LargeAvenue8LM_Elevated_APRMap.dds")))
                                        segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "LargeAvenue8LM_Elevated_APRMap.dds")));

                                if (false) // this buggy ATM
                                {
                                    if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Slope"))
                                        if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "LargeAvenue8LM_Slope_Segment_APRMap.dds")))
                                            segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "LargeAvenue8LM_Slope_Segment_APRMap.dds")));

                                    if (segment.m_segmentMaterial.GetTexture("_APRMap").name.Contains("Tunnel"))
                                        if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "LargeAvenue8LM_Tunnel_Segment_APRMap.dds")))
                                            segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(Path.Combine(ModLoader.currentTexturesPath_default, "LargeAvenue8LM_Tunnel_Segment_APRMap.dds")));
                                }

                                segment.m_lodRenderDistance = 2500;

                            }

                            #endregion

                        }

                    }
                }



                if (!(
                    netInfo.m_class.name.Contains("NExt") ||
                    netInfo.m_class.name.Contains("Water") ||
                    netInfo.m_class.name.Contains("Train") ||
                    netInfo.m_class.name.Contains("Metro") ||
                    netInfo.m_class.name.Contains("Transport") ||
                    netInfo.m_class.name.Contains("Bus Line") ||
                    netInfo.m_class.name.Contains("Airplane") ||
                    netInfo.m_class.name.Contains("Ship")

                    ))
                {
                    NetInfo.Node[] nodes = netInfo.m_nodes;
                    for (int k = 0; k < nodes.Length; k++)
                    {
                        NetInfo.Node node = nodes[k];

                        if (node.m_nodeMaterial.GetTexture("_MainTex") != null)
                        {
                            string nodeMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, node.m_nodeMaterial.GetTexture("_MainTex").name + ".dds");
                            Debug.Log(nodeMaterialTexture_name);
                            if (File.Exists(nodeMaterialTexture_name))
                                node.m_nodeMaterial.SetTexture("_MainTex", LoadTextureDDS(nodeMaterialTexture_name));
                            node.m_lodRenderDistance = 2500;
                            node.m_lodMesh = null;
                        }

                        if (node.m_nodeMaterial.GetTexture("_APRMap") != null)
                        {
                            string nodeMaterialAPRMap_name = Path.Combine(ModLoader.currentTexturesPath_apr_maps, node.m_nodeMaterial.GetTexture("_APRMap").name + ".dds");
                            Debug.Log(nodeMaterialAPRMap_name);
                            if (File.Exists(nodeMaterialAPRMap_name))
                                node.m_nodeMaterial.SetTexture("_APRMap", LoadTextureDDS(nodeMaterialAPRMap_name));
                        }
                    }




                    // Look for segments


                    NetInfo.Segment[] segments = netInfo.m_segments;
                    for (int l = 0; l < segments.Length; l++)
                    {
                        NetInfo.Segment segment = segments[l];

                        if (segment.m_segmentMaterial.GetTexture("_MainTex") != null)
                        {
                            string segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, segment.m_segmentMaterial.GetTexture("_MainTex").name + ".dds");

                            #region exceptions

                            if (netInfo.name.Contains("Oneway"))
                            {
                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeOnewaySegment_d_BusSide.dds")))
                                        segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeOnewaySegment_d_BusSide.dds");

                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeOnewaySegment_d_BusBoth.dds")))
                                        segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeOnewaySegment_d_BusBoth.dds");
                            }

                            if (segment.m_mesh.name.Equals("SmallRoadSegmentBusSide"))
                                if (!(netInfo.name.Contains("Bicycle")))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmall_D_BusSide.dds")))

                                        segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmall_D_BusSide.dds");

                            if (segment.m_mesh.name.Equals("SmallRoadSegmentBusBoth"))
                                if (!(netInfo.name.Contains("Bicycle")))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmall_D_BusBoth.dds")))
                                        segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmall_D_BusBoth.dds");


                            if (segment.m_mesh.name.Equals("SmallRoadSegment2BusSide"))
                                if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "SmallRoadSegmentDeco_BusSide.dds")))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "SmallRoadSegmentDeco_BusSide.dds");

                            if (segment.m_mesh.name.Equals("SmallRoadSegment2BusBoth"))
                                if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "SmallRoadSegmentDeco_BusSide.dds")))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "SmallRoadSegmentDeco_BusBoth.dds");


                            if (segment.m_mesh.name.Equals("RoadMediumSegmentBusSide"))
                            {
                                if (netInfo.name.Contains("Trees"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumDeco_d_BusSide.dds")))
                                        segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumDeco_d_BusSide.dds");
                                    else
                                if (netInfo.name.Contains("Bus"))
                                        if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumBusLane_BusSide.dds")))
                                            segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumBusLane_BusSide.dds");
                                        else
                                if (!(netInfo.name.Contains("Bicycle")))
                                            if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_BusSide.dds")))
                                                segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_BusSide.dds");
                                goto configsettings;
                            }

                            if (segment.m_mesh.name.Equals("RoadMediumSegmentBusBoth"))
                            {
                                if (netInfo.name.Contains("Trees"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumDeco_d_BusBoth.dds")))
                                        segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumDeco_d_BusBoth.dds");
                                    else
                                if (netInfo.name.Contains("Bus"))
                                        if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumBusLane_BusBoth.dds")))
                                            segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumBusLane_BusBoth.dds");
                                        else
                                if (!(netInfo.name.Contains("Bicycle")))
                                            if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_BusBoth.dds")))
                                                segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_BusBoth.dds");
                                goto configsettings;
                            }

                            if (netInfo.name.Contains("Oneway"))
                            {
                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_BusBoth.dds")))
                                        segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeOnewaySegment_d_BusSide.dds");

                                //this texture might not be in use
                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_BusBoth.dds")))
                                        segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeOnewaySegment_d_BusSide.dds");
                            }


                            if (!(netInfo.name.Contains("Bicycle") || netInfo.name.Contains("Oneway")))
                            {
                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusSide"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeSegment_d_BusSide.dds")))
                                        segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeSegment_d_BusSide.dds");

                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBoth"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeSegment_d_BusBoth.dds")))
                                        segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeSegment_d_BusBoth.dds");

                                //
                                if (segment.m_mesh.name.Equals("LargeRoadSegment2BusBoth"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeSegmentDecoBusBoth_d.dds")))
                                        segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeSegmentDecoBusBoth_d.dds");

                                //
                                if (segment.m_mesh.name.Equals("RoadLargeSegmentBusSideBusLane"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeBuslane_D_BusSide.dds")))
                                        segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeBuslane_D_BusSide.dds");

                                if (segment.m_mesh.name.Equals("LargeRoadSegmentBusBothBusLane"))
                                    if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeBuslane_D_BusBoth.dds")))
                                        segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeBuslane_D_BusBoth.dds");
                            }

                            Debug.Log(segmentMaterialTexture_name);

                            #endregion

                            configsettings:
                            { }
                            #region configsettings

                            if (ModLoader.config.basic_road_parking == 1)
                            {
                                if (segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmall_D.dds")))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmall_D_parking1.dds");


                                if (segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, ("RoadSmall_D_BusSide.dds"))))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadSmall_D_BusSide_parking1.dds");

                            }

                            if (
                                (ModLoader.config.medium_road_parking == 1) &&
                                (!(netInfo.name.Contains("Grass") || netInfo.name.Contains("Trees")))
                                )
                            {
                                if (segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D.dds")))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_parking1.dds");

                                if (segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, ("RoadMedium_D_BusSide.dds"))))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_parking1.dds");


                                if (segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, ("RoadMedium_D_BusBoth.dds"))))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_parking1.dds");

                            }

                            if ((ModLoader.config.medium_road_grass_parking == 1) && (netInfo.name.Contains("Grass")))
                            {
                                if ((segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D.dds"))))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_parking1.dds");

                                if (segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, ("RoadMedium_D_BusSide.dds"))))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_parking1.dds");


                                if (segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, ("RoadMedium_D_BusSide.dds"))))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_parking1.dds");
                            }

                            if ((ModLoader.config.medium_road_trees_parking == 1) && (netInfo.name.Contains("Trees")))
                            {
                                if (segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumDeco_d.dds")))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_parking1.dds");

                                if (segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, ("RoadMediumDeco_d_BusSide.dds"))))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_parking1.dds");

                                if (segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, ("RoadMediumDeco_d_BusBoth.dds"))))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadMedium_D_parking1.dds");

                            }

                            if (ModLoader.config.medium_road_bus_parking == 1)
                            {
                                if ((
                                    segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumBusLane.dds")) ||
                                    segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumBusLane_BusSide.dds"))
                                    ))
                                {
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadMediumBusLane_parking1.dds");
                                }
                            }

                            if (ModLoader.config.large_road_parking == 1)
                            {
                                if (segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeSegment_d.dds")))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeSegment_d_parking1.dds");


                                if (segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, ("RoadLargeSegment_d_BusSide.dds"))))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeSegment_d_parking1.dds");


                                if (segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, ("RoadLargeSegment_d_BusBoth.dds"))))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeSegment_d_parking1.dds");

                            }


                            if (ModLoader.config.large_oneway_parking == 1)
                            {
                                if ((
                                    segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeOnewaySegment_d.dds")) ||
                                    segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeOnewaySegment_d_BusSide.dds"))
                                    ))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeOnewaySegment_d_parking1.dds");
                            }

                            if (ModLoader.config.large_road_bus_parking == 1)
                            {
                                if ((
                                    segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeBuslane_D.dds")) ||
                                    segmentMaterialTexture_name.Equals(Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeBuslane_D_BusSide.dds"))
                                    ))
                                    segmentMaterialTexture_name = Path.Combine(ModLoader.currentTexturesPath_default, "RoadLargeBuslane_D_parking1.dds");
                            }

                            #endregion

                            if (File.Exists(segmentMaterialTexture_name))
                                segment.m_segmentMaterial.SetTexture("_MainTex", LoadTextureDDS(segmentMaterialTexture_name));
                        }

                        if (segment.m_segmentMaterial.GetTexture("_APRMap") != null)
                        {
                            string segmentMaterialAPRMap_name = Path.Combine(ModLoader.currentTexturesPath_apr_maps, segment.m_segmentMaterial.GetTexture("_APRMap").name + ".dds");

                            Debug.Log(segmentMaterialAPRMap_name);

                            if ((
                                    segment.m_segmentMaterial.GetTexture("_APRMap").name.Equals("LargeRoadSegmentBusSide-BikeLane-apr") ||
                                    segment.m_segmentMaterial.GetTexture("_APRMap").name.Equals("LargeRoadSegmentBusBoth-BikeLane-apr")
                                    ))
                                if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_apr_maps, "RoadLargeSegment-BikeLane-apr.dds")))
                                    segmentMaterialAPRMap_name = Path.Combine(ModLoader.currentTexturesPath_apr_maps, "RoadLargeSegment-BikeLane-apr.dds");

                            if ((
                                segment.m_segmentMaterial.GetTexture("_APRMap").name.Equals("LargeRoadSegmentBusSide-LargeRoadSegmentBusSide-apr") ||
                                segment.m_segmentMaterial.GetTexture("_APRMap").name.Equals("LargeRoadSegmentBusBoth-LargeRoadSegmentBusBoth-apr")
                                ))
                                if (File.Exists(Path.Combine(ModLoader.currentTexturesPath_apr_maps, "RoadLargeSegment-default-apr.dds")))
                                    segmentMaterialAPRMap_name = Path.Combine(ModLoader.currentTexturesPath_apr_maps, "RoadLargeSegment-default-apr.dds");


                            if (File.Exists(segmentMaterialAPRMap_name))
                                segment.m_segmentMaterial.SetTexture("_APRMap", LoadTextureDDS(segmentMaterialAPRMap_name));
                        }

                        segment.m_lodRenderDistance = 2500;


                    }

                }

            }
        }


    }
}












