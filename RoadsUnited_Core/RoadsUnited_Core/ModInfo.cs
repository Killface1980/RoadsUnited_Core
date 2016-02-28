using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RoadsUnited_Core
{
    public class RoadsUnited_CoreMod : IUserMod
    {
        public const UInt64 workshop_id = 633547552;

        public const String VersionName = "Alpha";


        public string Name
        {
            get
            {
                return "Roads United Core";
            }
        }

        public string Description
        {
            get
            {
                return "Replaces road textures and other road feature with more European ones.";
            }
        }

        #region Small road config


        private void EventSmallRoadBrightness(float c)
        {
            ModLoader.config.basic_road_ground_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventSmallRoadElevatedBrightness(float c)
        {
            ModLoader.config.basic_road_elevated_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventSmallRoadBridgeBrightness(float c)
        {
            ModLoader.config.basic_road_bridge_brightness = c;
            ModLoader.SaveConfig();

        }


        private void EventSmallRoadBicycleBrightness(float c)
        {
            ModLoader.config.basic_road_bicycle_ground_brightness = c;
            ModLoader.SaveConfig();

        }
        private void EventSmallRoadBicycleElevatedBrightness(float c)
        {
            ModLoader.config.basic_road_bicycle_elevated_brightness = c;
            ModLoader.SaveConfig();

        }
        private void EventSmallRoadBicycleBridgeBrightness(float c)
        {
            ModLoader.config.basic_road_bicycle_bridge_brightness = c;
            ModLoader.SaveConfig();

        }

        private void EventSmallRoadTramBrightness(float c)
        {
            ModLoader.config.basic_road_tram_ground_brightness = c;
            ModLoader.SaveConfig();
        }
        private void EventSmallRoadTramElevatedBrightness(float c)
        {
            ModLoader.config.basic_road_tram_elevated_brightness = c;
            ModLoader.SaveConfig();
        }
        private void EventSmallRoadTramBridgeBrightness(float c)
        {
            ModLoader.config.basic_road_tram_bridge_brightness = c;
            ModLoader.SaveConfig();

        }

        private void EventSmallRoadDecorationGrassBrightness(float c)
        {
            ModLoader.config.basic_road_decoration_grass_brightness = c;
            ModLoader.SaveConfig();

        }

        private void EventSmallRoadDecorationTreesBrightness(float c)
        {
            ModLoader.config.basic_road_decoration_trees_brightness = c;
            ModLoader.SaveConfig();

        }
        #endregion

        #region Oneway config

        private void EventOnewayRoadBrightness(float c)
        {
            ModLoader.config.oneway_road_ground_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventOnewayRoadElevatedBrightness(float c)
        {
            ModLoader.config.oneway_road_elevated_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventOnewayRoadBridgeBrightness(float c)
        {
            ModLoader.config.oneway_road_bridge_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventOnewayRoadDecorationGrassBrightness(float c)
        {
            ModLoader.config.oneway_road_decoration_grass_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventOnewayRoadDecorationTreesBrightness(float c)
        {
            ModLoader.config.oneway_road_decoration_trees_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventOnewayRoadTramBrightness(float c)
        {
            ModLoader.config.oneway_road_tram_ground_brightness = c;
            ModLoader.SaveConfig();
        }
        private void EventOnewayRoadTramElevatedBrightness(float c)
        {
            ModLoader.config.oneway_road_tram_elevated_brightness = c;
            ModLoader.SaveConfig();
        }
        private void EventOnewayRoadTramBridgeBrightness(float c)
        {
            ModLoader.config.oneway_road_tram_bridge_brightness = c;
            ModLoader.SaveConfig();
        }


        #endregion

        #region Medium roads config

        private void EventMediumRoadBrightness(float c)
        {
            ModLoader.config.medium_road_ground_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventMediumRoadElevatedBrightness(float c)
        {
            ModLoader.config.medium_road_elevated_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventMediumRoadBridgeBrightness(float c)
        {
            ModLoader.config.medium_road_bridge_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventMediumRoadBicycleBrightness(float c)
        {
            ModLoader.config.medium_road_bicycle_ground_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventMediumRoadBicycleElevatedBrightness(float c)
        {
            ModLoader.config.medium_road_bicycle_elevated_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventMediumRoadBicycleBridgeBrightness(float c)
        {
            ModLoader.config.medium_road_bicycle_bridge_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventMediumRoadBusBrightness(float c)
        {
            ModLoader.config.medium_road_bus_ground_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventMediumRoadBusElevatedBrightness(float c)
        {
            ModLoader.config.medium_road_bus_elevated_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventMediumRoadBusBridgeBrightness(float c)
        {
            ModLoader.config.medium_road_bus_bridge_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventMediumRoadTramBrightness(float c)
        {
            ModLoader.config.medium_road_tram_ground_brightness = c;
            ModLoader.SaveConfig();
        }
        private void EventMediumRoadTramElevatedBrightness(float c)
        {
            ModLoader.config.medium_road_tram_elevated_brightness = c;
            ModLoader.SaveConfig();
        }
        private void EventMediumRoadTramBridgeBrightness(float c)
        {
            ModLoader.config.medium_road_tram_bridge_brightness = c;
            ModLoader.SaveConfig();
        }
        private void EventMediumRoadDecorationGrassBrightness(float c)
        {
            ModLoader.config.medium_road_decoration_grass_brightness = c;
            ModLoader.SaveConfig();

        }

        private void EventMediumRoadDecorationTreesBrightness(float c)
        {
            ModLoader.config.medium_road_decoration_trees_brightness = c;
            ModLoader.SaveConfig();

        }

        #endregion

        #region Large road config

        private void EventLargeRoadBrightness(float c)
        {
            ModLoader.config.large_road_ground_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventLargeRoadElevatedBrightness(float c)
        {
            ModLoader.config.large_road_elevated_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventLargeRoadBridgeBrightness(float c)
        {
            ModLoader.config.large_road_bridge_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventLargeRoadBicycleBrightness(float c)
        {
            ModLoader.config.large_road_bicycle_ground_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventLargeRoadBicycleElevatedBrightness(float c)
        {
            ModLoader.config.large_road_bicycle_elevated_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventLargeRoadBicycleBridgeBrightness(float c)
        {
            ModLoader.config.large_road_bicycle_bridge_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventLargeRoadBusBrightness(float c)
        {
            ModLoader.config.large_road_bus_ground_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventLargeRoadBusElevatedBrightness(float c)
        {
            ModLoader.config.large_road_bus_elevated_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventLargeRoadBusBridgeBrightness(float c)
        {
            ModLoader.config.large_road_bus_bridge_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventLargeRoadDecorationGrassBrightness(float c)
        {
            ModLoader.config.large_road_decoration_grass_brightness = c;
            ModLoader.SaveConfig();

        }

        private void EventLargeRoadDecorationTreesBrightness(float c)
        {
            ModLoader.config.large_road_decoration_trees_brightness = c;
            ModLoader.SaveConfig();

        }

        #endregion

        #region Large oneway

        private void EventLargeOnewayBrightness(float c)
        {
            ModLoader.config.large_oneway_ground_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventLargeOnewayElevatedBrightness(float c)
        {
            ModLoader.config.large_oneway_elevated_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventLargeOnewayBridgeBrightness(float c)
        {
            ModLoader.config.large_oneway_bridge_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventLargeOnewayDecorationGrassBrightness(float c)
        {
            ModLoader.config.large_oneway_decoration_grass_brightness = c;
            ModLoader.SaveConfig();

        }

        private void EventLargeOnewayDecorationTreesBrightness(float c)
        {
            ModLoader.config.large_oneway_decoration_trees_brightness = c;
            ModLoader.SaveConfig();

        }

        #endregion

        #region Highway config

        private void EventHighwayRampGroundBrightness(float c)
        {
            ModLoader.config.highway_ramp_ground_brightness = c;
            ModLoader.SaveConfig();
        }
        private void EventHighwayRampElevatedBrightness(float c)
        {
            ModLoader.config.highway_ramp_elevated_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventHighwayGroundBrightness(float c)
        {
            ModLoader.config.highway_ground_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventHighwayElevatedBrightness(float c)
        {
            ModLoader.config.highway_elevated_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventHighwayBridgeBrightness(float c)
        {
            ModLoader.config.highway_bridge_brightness = c;
            ModLoader.SaveConfig();
        }
        private void EventHighwayBarrierBrightness(float c)
        {
            ModLoader.config.highway_barrier_brightness = c;
            ModLoader.SaveConfig();
        }

        #endregion

        #region Parking marking

        private void EventSmallRoadParking(int c)
        {
            ModLoader.config.basic_road_parking = c;
            ModLoader.SaveConfig();
        }

        private void EventMediumRoadParking(int c)
        {
            ModLoader.config.medium_road_parking = c;
            ModLoader.SaveConfig();
        }

        private void EventMediumRoadGrassParking(int c)
        {
            ModLoader.config.medium_road_grass_parking = c;
            ModLoader.SaveConfig();
        }

        private void EventMediumRoadTreesParking(int c)
        {
            ModLoader.config.medium_road_trees_parking = c;
            ModLoader.SaveConfig();
        }

        private void EventMediumRoadBusParking(int c)
        {
            ModLoader.config.medium_road_bus_parking = c;
            ModLoader.SaveConfig();
        }

        private void EventLargeRoadParking(int c)
        {
            ModLoader.config.large_road_parking = c;
            ModLoader.SaveConfig();
        }

        private void EventLargeRoadBusParking(int c)
        {
            ModLoader.config.large_road_bus_parking = c;
            ModLoader.SaveConfig();
        }

        private void EventLargeOnewayParking(int c)
        {
            ModLoader.config.large_oneway_parking = c;
            ModLoader.SaveConfig();
        }

        #endregion


        #region Config stuff



        private void EventCheckCreateVanillaDictionary(bool c)
        {
            ModLoader.config.create_vanilla_dictionary = c;
            ModLoader.SaveConfig();
        }

        private void EventCheckUseCustomTextures(bool c)
        {
            ModLoader.config.use_custom_textures = c;
            ModLoader.SaveConfig();
        }

        private void EventCheckUseCustomColours(bool c)
        {
            ModLoader.config.use_custom_colours = c;
            ModLoader.SaveConfig();
        }

        private void EventRevertVanillaTextures()
        {
            RoadsUnited_Core.ApplyVanillaDictionary();

        }

        private void EventResetColor()
        {
            ModLoader.config.basic_road_ground_brightness = 0.3f;
            ModLoader.config.basic_road_bicycle_ground_brightness = 0.3f;
            ModLoader.config.basic_road_decoration_grass_brightness = 0.3f;
            ModLoader.config.basic_road_decoration_trees_brightness = 0.3f;

            ModLoader.config.medium_road_ground_brightness = 0.3f;

            ModLoader.config.large_road_ground_brightness = 0.3f;
            ModLoader.SaveConfig();
        }

        private void EventReloadTextures()
        {
            RoadsUnited_Core.ApplyVanillaDictionary();
            RoadsUnited_Core.ReplaceNetTextures();

            ModLoader.SaveConfig();
        }

        private void EventResetConfig()
        {
            ModLoader.config = new Configuration();

            ModLoader.SaveConfig();
        }


        #endregion


        #region RoadThemeDropdownMenu

        public static UICheckBox checkbox = null;

        public static UICheckBox checkbox2 = null;

        public static UIDropDown dropdown = null;

        public static UIPanel panel1 = null;


        public static UITextField infoText = null;

        public static UITextField infoText1 = null;

        public static List<string> packNames;

        public static List<string> packNames1;

        public static List<string> filteredPackNames;

        public static List<RoadThemePack> packs;

        public static int selectedPackID = 0;

        #endregion

        public void OnSettingsUI(UIHelperBase helper)
        {
            Debug.Log("Settings initializing");


            ModLoader.config = Configuration.Deserialize(ModLoader.configPath);
            if (ModLoader.config == null)
            {
                ModLoader.config = new Configuration();
            }
            ModLoader.SaveConfig();


            #region RoadThemSelector

            RoadsUnited_CoreMod.packs = Singleton<RoadThemeManager>.instance.GetAvailablePacks();
            RoadsUnited_CoreMod.packNames = new List<string>();
            RoadsUnited_CoreMod.packNames.Add("None");
            RoadsUnited_CoreMod.packNames.AddRange(from pack in RoadsUnited_CoreMod.packs
                                                   select pack.themeName);


            UIHelperBase uIHelperBase2 = helper.AddGroup("Road Themes");
            RoadsUnited_CoreMod.panel1 = (UIPanel)((UIPanel)((UIHelper)uIHelperBase2).self).parent;
            RoadsUnited_CoreMod.dropdown = (UIDropDown)uIHelperBase2.AddDropdown("Select Road Theme", RoadsUnited_CoreMod.filteredPackNames.ToArray(), RoadsUnited_CoreMod.selectedPackID, delegate (int selectedIndex)
            {
                if (selectedIndex > 0)
                {
                    RoadsUnited_CoreMod.infoText.text = RoadThemesUtil.GetDescription(RoadsUnited_CoreMod.packs.Find((RoadThemePack pack) => pack.themeName == RoadsUnited_CoreMod.filteredPackNames[RoadsUnited_CoreMod.dropdown.selectedIndex]));
                    Singleton<RoadThemeManager>.instance.ActivePack = RoadsUnited_CoreMod.packs.Find((RoadThemePack pack) => pack.themeName == RoadsUnited_CoreMod.filteredPackNames[selectedIndex]);
                }
                else
                {
                    Singleton<RoadThemeManager>.instance.ActivePack = null;
                }
                RoadsUnited_CoreMod.selectedPackID = selectedIndex;
            });
            RoadsUnited_CoreMod.dropdown.width = 600f;


            #endregion

            UIHelperBase uIHelperGeneralSettings = helper.AddGroup("General Settings");
            //            uIHelperGeneralSettings.AddCheckbox("Use mods Vanilla roads texture replacements", ModLoader.config.use_custom_textures, EventCheckUseCustomTextures);
            uIHelperGeneralSettings.AddCheckbox("Activate the brightness sliders below. Slider to the right for a lighter colour.", ModLoader.config.use_custom_colours, EventCheckUseCustomColours);
            uIHelperGeneralSettings.AddCheckbox("Create Vanilla road texture backup on level load.", ModLoader.config.create_vanilla_dictionary, EventCheckCreateVanillaDictionary);
            uIHelperGeneralSettings.AddButton("Revert to Vanilla textures (in-game only)", EventRevertVanillaTextures);
            uIHelperGeneralSettings.AddButton("Reload all mod textures (in-game only)", EventReloadTextures);


            uIHelperGeneralSettings.AddButton("Reset all sliders and configuration next level load. ", EventResetConfig);

            UIHelperBase uIHelperParkingSpaceSettings = helper.AddGroup("Parking space marking");
            uIHelperParkingSpaceSettings.AddDropdown("Small roads", new string[] { "No marking", "Use default" }, ModLoader.config.basic_road_parking, EventSmallRoadParking);
            uIHelperParkingSpaceSettings.AddDropdown("Medium roads", new string[] { "No marking", "Use default" }, ModLoader.config.medium_road_parking, EventMediumRoadParking);
            uIHelperParkingSpaceSettings.AddDropdown("Medium roads grass", new string[] { "No marking", "Use default" }, ModLoader.config.medium_road_grass_parking, EventMediumRoadGrassParking);
            uIHelperParkingSpaceSettings.AddDropdown("Medium roads trees", new string[] { "No marking", "Use default" }, ModLoader.config.medium_road_trees_parking, EventMediumRoadTreesParking);
            uIHelperParkingSpaceSettings.AddDropdown("Medium roads buslane", new string[] { "No marking", "Use default" }, ModLoader.config.medium_road_bus_parking, EventMediumRoadBusParking);
            uIHelperParkingSpaceSettings.AddDropdown("Large roads", new string[] { "No marking", "Use default" }, ModLoader.config.large_road_parking, EventLargeRoadParking);
            uIHelperParkingSpaceSettings.AddDropdown("Large roads buslane", new string[] { "No marking", "Use default" }, ModLoader.config.large_road_bus_parking, EventLargeRoadBusParking);
            uIHelperParkingSpaceSettings.AddDropdown("Large Oneways", new string[] { "No marking", "Use default" }, ModLoader.config.large_oneway_parking, EventLargeOnewayParking);
            uIHelperParkingSpaceSettings.AddSpace(10);


            UIHelperBase uIHelperSmallRoads = helper.AddGroup("Small Roads");
            uIHelperSmallRoads.AddSlider("Ground", 0, 1f, 0.0625f, ModLoader.config.basic_road_ground_brightness, new OnValueChanged(EventSmallRoadBrightness));
            uIHelperSmallRoads.AddSlider("Elevated", 0, 1f, 0.0625f, ModLoader.config.basic_road_elevated_brightness, new OnValueChanged(EventSmallRoadElevatedBrightness));
            uIHelperSmallRoads.AddSlider("Bridge", 0, 1f, 0.0625f, ModLoader.config.basic_road_bridge_brightness, new OnValueChanged(EventSmallRoadBridgeBrightness));
            uIHelperSmallRoads.AddSpace(10);
            uIHelperSmallRoads.AddSlider("Decoration Grass", 0, 1f, 0.0625f, ModLoader.config.basic_road_decoration_grass_brightness, new OnValueChanged(EventSmallRoadDecorationGrassBrightness));
            uIHelperSmallRoads.AddSlider("Decoration Trees", 0, 1f, 0.0625f, ModLoader.config.basic_road_decoration_trees_brightness, new OnValueChanged(EventSmallRoadDecorationTreesBrightness));

            UIHelperBase uIHelperSmallBikeRoads = helper.AddGroup("Small Road with Bikelane");
            uIHelperSmallBikeRoads.AddSlider("Ground", 0, 1f, 0.0625f, ModLoader.config.basic_road_bicycle_ground_brightness, new OnValueChanged(EventSmallRoadBicycleBrightness));
            uIHelperSmallBikeRoads.AddSlider("Elevated", 0, 1f, 0.0625f, ModLoader.config.basic_road_bicycle_elevated_brightness, new OnValueChanged(EventSmallRoadBicycleElevatedBrightness));
            uIHelperSmallBikeRoads.AddSlider("Bridge", 0, 1f, 0.0625f, ModLoader.config.basic_road_bicycle_bridge_brightness, new OnValueChanged(EventSmallRoadBicycleBridgeBrightness));

            UIHelperBase uIHelperSmallRoadsTram = helper.AddGroup("Small Road with Tram");
            uIHelperSmallRoadsTram.AddSlider("Ground", 0, 1f, 0.0625f, ModLoader.config.basic_road_tram_ground_brightness, new OnValueChanged(EventSmallRoadTramBrightness));
            uIHelperSmallRoadsTram.AddSlider("Elevated", 0, 1f, 0.0625f, ModLoader.config.basic_road_tram_elevated_brightness, new OnValueChanged(EventSmallRoadTramElevatedBrightness));
            uIHelperSmallRoadsTram.AddSlider("Bridge", 0, 1f, 0.0625f, ModLoader.config.basic_road_tram_bridge_brightness, new OnValueChanged(EventSmallRoadTramBridgeBrightness));


            UIHelperBase uIHelperOnewayRoads = helper.AddGroup("Small Oneway");
            uIHelperOnewayRoads.AddSlider("Ground", 0, 1f, 0.0625f, ModLoader.config.oneway_road_ground_brightness, new OnValueChanged(EventOnewayRoadBrightness));
            uIHelperOnewayRoads.AddSlider("Elevated", 0, 1f, 0.0625f, ModLoader.config.oneway_road_elevated_brightness, new OnValueChanged(EventOnewayRoadElevatedBrightness));
            uIHelperOnewayRoads.AddSlider("Bridge", 0, 1f, 0.0625f, ModLoader.config.oneway_road_bridge_brightness, new OnValueChanged(EventOnewayRoadBridgeBrightness));
            uIHelperOnewayRoads.AddSpace(10);
            uIHelperOnewayRoads.AddSlider("Decoration Grass", 0, 1f, 0.0625f, ModLoader.config.oneway_road_decoration_grass_brightness, new OnValueChanged(EventOnewayRoadDecorationGrassBrightness));
            uIHelperOnewayRoads.AddSlider("Decoration Trees ", 0, 1f, 0.0625f, ModLoader.config.oneway_road_decoration_trees_brightness, new OnValueChanged(EventOnewayRoadDecorationTreesBrightness));
            uIHelperOnewayRoads.AddSpace(10);
            uIHelperOnewayRoads.AddSlider("Tram Ground", 0, 1f, 0.0625f, ModLoader.config.oneway_road_tram_ground_brightness, new OnValueChanged(EventOnewayRoadTramBrightness));
            uIHelperOnewayRoads.AddSlider("Tram Elevated", 0, 1f, 0.0625f, ModLoader.config.oneway_road_tram_elevated_brightness, new OnValueChanged(EventOnewayRoadTramElevatedBrightness));
            uIHelperOnewayRoads.AddSlider("Tram Bridge", 0, 1f, 0.0625f, ModLoader.config.oneway_road_tram_bridge_brightness, new OnValueChanged(EventOnewayRoadTramBridgeBrightness));

            UIHelperBase uIHelperMediumRoads = helper.AddGroup("Medium Roads");
            uIHelperMediumRoads.AddSlider("Ground", 0, 1f, 0.0625f, ModLoader.config.medium_road_ground_brightness, new OnValueChanged(EventMediumRoadBrightness));
            uIHelperMediumRoads.AddSlider("Elevated", 0, 1f, 0.0625f, ModLoader.config.medium_road_elevated_brightness, new OnValueChanged(EventMediumRoadElevatedBrightness));
            uIHelperMediumRoads.AddSlider("Bridge", 0, 1f, 0.0625f, ModLoader.config.medium_road_bridge_brightness, new OnValueChanged(EventMediumRoadBridgeBrightness));
            uIHelperMediumRoads.AddSpace(10);
            uIHelperMediumRoads.AddSlider("Decoration Grass", 0, 1f, 0.0625f, ModLoader.config.medium_road_decoration_grass_brightness, new OnValueChanged(EventMediumRoadDecorationGrassBrightness));
            uIHelperMediumRoads.AddSlider("Decoration Trees", 0, 1f, 0.0625f, ModLoader.config.medium_road_decoration_trees_brightness, new OnValueChanged(EventMediumRoadDecorationTreesBrightness));

            UIHelperBase uIHelperMediumBusBikeRoads = helper.AddGroup("Medium Roads with Bicycle, Bus Lane or Tram");
            uIHelperMediumBusBikeRoads.AddSlider("Bike Ground", 0, 1f, 0.0625f, ModLoader.config.medium_road_bicycle_ground_brightness, new OnValueChanged(EventMediumRoadBicycleBrightness));
            uIHelperMediumBusBikeRoads.AddSlider("Bike Elevated", 0, 1f, 0.0625f, ModLoader.config.medium_road_bicycle_elevated_brightness, new OnValueChanged(EventMediumRoadBicycleElevatedBrightness));
            uIHelperMediumBusBikeRoads.AddSlider("Bike Bridge", 0, 1f, 0.0625f, ModLoader.config.medium_road_bicycle_bridge_brightness, new OnValueChanged(EventMediumRoadBicycleBridgeBrightness));
            uIHelperMediumBusBikeRoads.AddSpace(10);
            uIHelperMediumBusBikeRoads.AddSlider("Bus Ground", 0, 1f, 0.0625f, ModLoader.config.medium_road_bus_ground_brightness, new OnValueChanged(EventMediumRoadBusBrightness));
            uIHelperMediumBusBikeRoads.AddSlider("Bus Elevated", 0, 1f, 0.0625f, ModLoader.config.medium_road_bus_elevated_brightness, new OnValueChanged(EventMediumRoadBusElevatedBrightness));
            uIHelperMediumBusBikeRoads.AddSlider("Bus Bridge", 0, 1f, 0.0625f, ModLoader.config.medium_road_bus_bridge_brightness, new OnValueChanged(EventMediumRoadBusBridgeBrightness));
            uIHelperMediumBusBikeRoads.AddSpace(10);
            uIHelperMediumBusBikeRoads.AddSlider("Tram Ground", 0, 1f, 0.0625f, ModLoader.config.medium_road_tram_ground_brightness, new OnValueChanged(EventMediumRoadTramBrightness));
            uIHelperMediumBusBikeRoads.AddSlider("Tram Elevated", 0, 1f, 0.0625f, ModLoader.config.medium_road_tram_elevated_brightness, new OnValueChanged(EventMediumRoadTramElevatedBrightness));
            uIHelperMediumBusBikeRoads.AddSlider("Tram Bridge", 0, 1f, 0.0625f, ModLoader.config.medium_road_tram_bridge_brightness, new OnValueChanged(EventMediumRoadTramBridgeBrightness));

            UIHelperBase uIHelperLargeRoads = helper.AddGroup("Large Roads");
            uIHelperLargeRoads.AddSlider("Ground", 0, 1f, 0.0625f, ModLoader.config.large_road_ground_brightness, new OnValueChanged(EventLargeRoadBrightness));
            uIHelperLargeRoads.AddSlider("Elevated", 0, 1f, 0.0625f, ModLoader.config.large_road_elevated_brightness, new OnValueChanged(EventLargeRoadElevatedBrightness));
            uIHelperLargeRoads.AddSlider("Bridge", 0, 1f, 0.0625f, ModLoader.config.large_road_bridge_brightness, new OnValueChanged(EventLargeRoadBridgeBrightness));
            uIHelperLargeRoads.AddSpace(10);
            uIHelperLargeRoads.AddSlider("Decoration Grass", 0, 1f, 0.0625f, ModLoader.config.large_road_decoration_grass_brightness, new OnValueChanged(EventLargeRoadDecorationGrassBrightness));
            uIHelperLargeRoads.AddSlider("Decoration Trees", 0, 1f, 0.0625f, ModLoader.config.large_road_decoration_trees_brightness, new OnValueChanged(EventLargeRoadDecorationTreesBrightness));

            UIHelperBase uIHelperLargeBusBikeRoads = helper.AddGroup("Large Roads with Bicycle or Bus Lane");
            uIHelperLargeBusBikeRoads.AddSlider("Bike Ground", 0, 1f, 0.0625f, ModLoader.config.large_road_bicycle_ground_brightness, new OnValueChanged(EventLargeRoadBicycleBrightness));
            uIHelperLargeBusBikeRoads.AddSlider("Bike Elevated", 0, 1f, 0.0625f, ModLoader.config.large_road_bicycle_elevated_brightness, new OnValueChanged(EventLargeRoadBicycleElevatedBrightness));
            uIHelperLargeBusBikeRoads.AddSlider("Bike Bridge", 0, 1f, 0.0625f, ModLoader.config.large_road_bicycle_bridge_brightness, new OnValueChanged(EventLargeRoadBicycleBridgeBrightness));
            uIHelperLargeBusBikeRoads.AddSpace(10);
            uIHelperLargeBusBikeRoads.AddSlider("Bus Ground", 0, 1f, 0.0625f, ModLoader.config.large_road_bus_ground_brightness, new OnValueChanged(EventLargeRoadBusBrightness));
            uIHelperLargeBusBikeRoads.AddSlider("Bus Elevated", 0, 1f, 0.0625f, ModLoader.config.large_road_bus_elevated_brightness, new OnValueChanged(EventLargeRoadBusElevatedBrightness));
            uIHelperLargeBusBikeRoads.AddSlider("Bus Bridge", 0, 1f, 0.0625f, ModLoader.config.large_road_bus_bridge_brightness, new OnValueChanged(EventLargeRoadBusBridgeBrightness));

            UIHelperBase uIHelperLargeOneway = helper.AddGroup("Large Oneway");
            uIHelperLargeOneway.AddSlider("Ground", 0, 1f, 0.0625f, ModLoader.config.large_oneway_ground_brightness, new OnValueChanged(EventLargeOnewayBrightness));
            uIHelperLargeOneway.AddSlider("Elevated", 0, 1f, 0.0625f, ModLoader.config.large_oneway_elevated_brightness, new OnValueChanged(EventLargeOnewayElevatedBrightness));
            uIHelperLargeOneway.AddSlider("Bridge", 0, 1f, 0.0625f, ModLoader.config.large_oneway_bridge_brightness, new OnValueChanged(EventLargeOnewayBridgeBrightness));
            uIHelperLargeOneway.AddSpace(10);
            uIHelperLargeOneway.AddSlider("Decoration Grass", 0, 1f, 0.0625f, ModLoader.config.large_oneway_decoration_grass_brightness, new OnValueChanged(EventLargeOnewayDecorationGrassBrightness));
            uIHelperLargeOneway.AddSlider("Decoration Trees ", 0, 1f, 0.0625f, ModLoader.config.large_oneway_decoration_trees_brightness, new OnValueChanged(EventLargeOnewayDecorationTreesBrightness));

            UIHelperBase uIHelperHighways = helper.AddGroup("Highways");
            uIHelperHighways.AddSlider("Highway Ramp Ground", 0, 1f, 0.0625f, ModLoader.config.highway_ramp_ground_brightness, new OnValueChanged(EventHighwayRampGroundBrightness));
            uIHelperHighways.AddSlider("Highway Ramp Elevated", 0, 1f, 0.0625f, ModLoader.config.highway_ramp_elevated_brightness, new OnValueChanged(EventHighwayRampElevatedBrightness));
            uIHelperHighways.AddSpace(10);
            uIHelperHighways.AddSlider("Highway Ground", 0, 1f, 0.0625f, ModLoader.config.highway_ground_brightness, new OnValueChanged(EventHighwayGroundBrightness));
            uIHelperHighways.AddSlider("Highway Elevated", 0, 1f, 0.0625f, ModLoader.config.highway_elevated_brightness, new OnValueChanged(EventHighwayElevatedBrightness));
            uIHelperHighways.AddSlider("Highway Bridge", 0, 1f, 0.0625f, ModLoader.config.highway_bridge_brightness, new OnValueChanged(EventHighwayBridgeBrightness));
            uIHelperHighways.AddSpace(10);
            uIHelperHighways.AddSlider("Highway Barrier", 0, 1f, 0.0625f, ModLoader.config.highway_barrier_brightness, new OnValueChanged(EventHighwayBarrierBrightness));



        }


    }

}

