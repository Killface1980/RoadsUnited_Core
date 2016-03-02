using ColossalFramework.IO;
using ColossalFramework.Steamworks;
using ICities;
using System.IO;
using UnityEngine;
using ColossalFramework.UI;
using ColossalFramework;
using ColossalFramework.Plugins;

namespace RoadsUnited_Core
{


    public class ModLoader : LoadingExtensionBase
    {

        public static Configuration config;

        public static readonly string configPath = "RoadsUnitedCoreConfig.xml";

        public static string getModPath()
        {
            string text = ".";
            PublishedFileId[] subscribedItems = Steam.workshop.GetSubscribedItems();
            for (int i = 0; i < subscribedItems.Length; i++)
            {
                PublishedFileId id = subscribedItems[i];
                if (id.AsUInt64 == 598151121)
                {
                    text = Steam.workshop.GetSubscribedItemPath(id);
                    Debug.Log("Roads United Core: Workshop path: " + text);
                    break;
                }
            }
            string text2 = DataLocation.modsPath + "/RoadsUnited";
            Debug.Log("Roads United Core: " + text2);
            string result;
            if (Directory.Exists(text2))
            {
                Debug.Log("Roads United Core: Local path exists, looking for assets here: " + text2);
                result = text2;
            }
            else
            {
                result = text;
            }
            return result;
        }

        public static string modPath = getModPath();

        public static string currentTexturesPath_default = Path.Combine(modPath, "BaseTextures");
        public static string currentTexturesPath_noParking = Path.Combine(currentTexturesPath_default, "noParking");
        public static string currentTexturesPath_apr_maps = Path.Combine(currentTexturesPath_default, "apr_maps");
        public static string currentTexturesPath_lod_rgb = Path.Combine(currentTexturesPath_default, "lod_rgb");
        public static string currentTexturesPath_NetExt = Path.Combine(modPath, "NetExtTextures");


        public RoadsUnited_Core textureManager;


        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);

            config = Configuration.Deserialize(configPath);
            if (config == null)
            {
                config = new Configuration();
            }
            SaveConfig();


        }


        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);

            string modPath = getModPath();

            if (ModLoader.config.create_vanilla_dictionary == true)
            {
                bool isEmpty;
                using (var dictionaryEnum = RoadsUnited_Core.vanillaPrefabProperties.GetEnumerator())
                    isEmpty = !dictionaryEnum.MoveNext();

                if (isEmpty)
                {
                    RoadsUnited_Core.CreateVanillaDictionary();
                }
            }


            if (ModLoader.config.use_custom_textures == true)
                RoadsUnited_Core.ReplaceNetTextures();



            #region.RoadColorChanger

            if (ModLoader.config.use_custom_colors == true)
            {
                RoadColourChanger.ChangeColor(ModLoader.config.basic_road_ground_brightness, "Basic Road", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.basic_road_elevated_brightness, "Basic Road Elevated", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.basic_road_bridge_brightness, "Basic Road Bridge", ModLoader.modPath);

                RoadColourChanger.ChangeColor(ModLoader.config.basic_road_bicycle_ground_brightness, "Basic Road Bicycle", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.basic_road_bicycle_elevated_brightness, "Basic Road Elevated Bike", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.basic_road_bicycle_bridge_brightness, "Basic Road Bridge Bike", ModLoader.modPath);

                RoadColourChanger.ChangeColor(ModLoader.config.basic_road_tram_ground_brightness, "Basic Road Tram", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.basic_road_tram_elevated_brightness, "Basic Road Elevated Tram", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.basic_road_tram_bridge_brightness, "Basic Road Bridge Tram", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.basic_road_tram_ground_brightness, "Basic Road Slope Tram", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.basic_road_tram_ground_brightness, "Basic Road Tunnel Tram", ModLoader.modPath);



                RoadColourChanger.ChangeColor(ModLoader.config.basic_road_decoration_grass_brightness, "Basic Road Decoration Grass", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.basic_road_decoration_trees_brightness, "Basic Road Decoration Trees", ModLoader.modPath);


                RoadColourChanger.ChangeColor(ModLoader.config.oneway_road_ground_brightness, "Oneway Road", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.oneway_road_elevated_brightness, "Oneway Road Elevated", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.oneway_road_bridge_brightness, "Oneway Road Bridge", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.oneway_road_decoration_grass_brightness, "Oneway Road Decoration Grass", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.oneway_road_decoration_trees_brightness, "Oneway Road Decoration Trees", ModLoader.modPath);

                RoadColourChanger.ChangeColor(ModLoader.config.oneway_road_tram_ground_brightness, "Oneway Road Tram", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.oneway_road_tram_elevated_brightness, "Oneway Road Elevated Tram", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.oneway_road_tram_bridge_brightness, "Oneway Road Bridge Tram", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.oneway_road_tram_ground_brightness, "Oneway Road Slope Tram", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.oneway_road_tram_ground_brightness, "Oneway Road Tunnel Tram", ModLoader.modPath);

                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_ground_brightness, "Medium Road", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_elevated_brightness, "Medium Road Elevated", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_bridge_brightness, "Medium Road Bridge", ModLoader.modPath);

                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_bicycle_ground_brightness, "Medium Road Bicycle", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_bicycle_elevated_brightness, "Medium Road Elevated Bike", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_bicycle_bridge_brightness, "Medium Road Bridge Bike", ModLoader.modPath);

                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_tram_ground_brightness, "Medium Road Tram", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_tram_elevated_brightness, "Medium Road Elevated Tram", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_tram_bridge_brightness, "Medium Road Bridge Tram", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_tram_ground_brightness, "Medium Road Slope Tram", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_tram_ground_brightness, "Medium Road Tunnel Tram", ModLoader.modPath);

                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_decoration_grass_brightness, "Medium Road Decoration Grass", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_decoration_trees_brightness, "Medium Road Decoration Trees", ModLoader.modPath);

                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_bus_ground_brightness, "Medium Road Bus", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_bus_elevated_brightness, "Medium Road Elevated Bus", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_bus_bridge_brightness, "Medium Road Bridge Bus", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_bus_ground_brightness, "Medium Road Slope Bus", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.medium_road_bus_ground_brightness, "Medium Road Tunnel Bus", ModLoader.modPath);

                RoadColourChanger.ChangeColor(ModLoader.config.large_road_ground_brightness, "Large Road", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.large_road_elevated_brightness, "Large Road Elevated", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.large_road_bridge_brightness, "Large Road Bridge", ModLoader.modPath);

                RoadColourChanger.ChangeColor(ModLoader.config.large_road_decoration_grass_brightness, "Large Road Decoration Grass", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.large_road_decoration_trees_brightness, "Large Road Decoration Trees", ModLoader.modPath);

                RoadColourChanger.ChangeColor(ModLoader.config.large_road_bicycle_ground_brightness, "Large Road Bicycle", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.large_road_bicycle_elevated_brightness, "Large Road Elevated Bike", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.large_road_bicycle_bridge_brightness, "Large Road Bridge Bike", ModLoader.modPath);

                RoadColourChanger.ChangeColor(ModLoader.config.large_road_bus_ground_brightness, "Large Road Bus", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.large_road_bus_elevated_brightness, "Large Road Elevated Bus", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.large_road_bus_bridge_brightness, "Large Road Bridge Bus", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.large_road_bus_ground_brightness, "Large Road Slope Bus", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.large_road_bus_ground_brightness, "Large Road Tunnel Bus", ModLoader.modPath);

                RoadColourChanger.ChangeColor(ModLoader.config.large_oneway_ground_brightness, "Large Oneway", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.large_oneway_ground_brightness, "Large Oneway Road", ModLoader.modPath); // RCC adds Slope + Tunnel
                RoadColourChanger.ChangeColor(ModLoader.config.large_oneway_elevated_brightness, "Large Oneway Elevated", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.large_oneway_bridge_brightness, "Large Oneway Bridge", ModLoader.modPath);

                RoadColourChanger.ChangeColor(ModLoader.config.large_oneway_decoration_grass_brightness, "Large Oneway Decoration Grass", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.large_oneway_decoration_trees_brightness, "Large Oneway Decoration Trees", ModLoader.modPath);

                RoadColourChanger.ChangeColor(ModLoader.config.highway_ramp_ground_brightness, "HighwayRamp", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.highway_ramp_elevated_brightness, "HighwayRampElevated", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.highway_ramp_elevated_brightness, "HighwayRamp Slope", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.highway_ramp_elevated_brightness, "HighwayRamp Tunnel", ModLoader.modPath);

                RoadColourChanger.ChangeColor(ModLoader.config.highway_ground_brightness, "Highway", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.highway_ground_brightness, "Highway Slope", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.highway_ground_brightness, "Highway Tunnel", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.highway_elevated_brightness, "Highway Elevated", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.highway_bridge_brightness, "Highway Bridge", ModLoader.modPath);
                RoadColourChanger.ChangeColor(ModLoader.config.highway_barrier_brightness, "Highway Barrier", ModLoader.modPath);

                RoadColourChanger.ChangeColorNetExt(ModLoader.config.basic_road_ground_brightness, "NExt2LAlley", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.basic_road_ground_brightness, "NExt1LOneway", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.basic_road_ground_brightness, "NExtSmall3LRoad", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.basic_road_ground_brightness, "NExtSmall4LRoad", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.basic_road_ground_brightness, "Small Avenue", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.oneway_road_ground_brightness, "Oneway3L", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.oneway_road_ground_brightness, "Oneway4L", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.medium_road_ground_brightness, "NExtMediumRoad", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.medium_road_ground_brightness, "NExtMediumRoadTunnel", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.medium_road_ground_brightness, "NExtMediumRoadTL", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.medium_road_ground_brightness, "NExtMediumRoadTLTunnel", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.large_road_ground_brightness, "NExtLargeRoad", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.large_road_ground_brightness, "NExtLargeRoadTunnel", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.large_road_ground_brightness, "NExtLargeRoadTL", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.large_road_ground_brightness, "NExtLargeRoadTLTunnel", ModLoader.modPath);

                RoadColourChanger.ChangeColorNetExt(ModLoader.config.large_road_ground_brightness, "NExtXLargeRoad", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.large_road_ground_brightness, "NExtXLargeRoadTunnel", ModLoader.modPath);

                RoadColourChanger.ChangeColorNetExt(ModLoader.config.highway_ground_brightness, "NExtHighway1L", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.highway_ground_brightness, "NExtHighwayTunnel1LTunnel", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.highway_ground_brightness, "NExtHighway2L", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.highway_ground_brightness, "NExtHighwayTunnel2LTunnel", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.highway_ground_brightness, "NExtHighway4L", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.highway_ground_brightness, "NExtHighwayTunnel4LTunnel", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.highway_ground_brightness, "NExtHighway5L", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.highway_ground_brightness, "NExtHighwayTunnel5LTunnel", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.highway_ground_brightness, "NExtHighway6L", ModLoader.modPath);
                RoadColourChanger.ChangeColorNetExt(ModLoader.config.highway_ground_brightness, "NExtHighwayTunnel6LTunnel", ModLoader.modPath);

                RoadColourChanger.ReplaceLodAprAtlas(currentTexturesPath_apr_maps);
            }
            #endregion

        }




        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();


            RoadsUnited_Core.ApplyVanillaDictionary();
            RoadsUnited_Core.vanillaPrefabProperties.Clear();
        }


        public static void SaveConfig()
        {
            Configuration.Serialize(ModLoader.configPath, ModLoader.config);
        }

        public override void OnReleased()
        {

        }




#if Debug
        public void ButtonClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            RoadsUnited.ReplaceNetTextures(modPath);
        } 
#endif
    }


}


