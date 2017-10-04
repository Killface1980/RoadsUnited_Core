namespace RoadsUnited_Core
{
    using System.Collections.Generic;
    using System.Linq;

    using ColossalFramework;
    using ColossalFramework.UI;

    using ICities;

    using UnityEngine;

    public class RoadsUnited_CoreMod : IUserMod
    {
        // public const UInt64 workshop_id = 633547552uL;
        public string Name
        {
            get
            {
                return "Roads United Core 2.0";
            }
        }

        public string Description
        {
            get
            {
                return "Replaces road textures and other road features.";
            }
        }

        #region Small road config

        private void EventSmallRoadBrightness(float c)
        {
            ModLoader.config.small_road_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventSmallRoadDecorationBrightness(float c)
        {
            ModLoader.config.small_road_decoration = c;
            ModLoader.SaveConfig();
        }

        #endregion

        #region Medium roads config

        private void EventMediumRoadBrightness(float c)
        {
            ModLoader.config.medium_road_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventMediumRoadDecorationBrightness(float c)
        {
            ModLoader.config.medium_road_decoration_brightness = c;
            ModLoader.SaveConfig();
        }

        #endregion

        #region Large road config

        private void EventLargeRoadBrightness(float c)
        {
            ModLoader.config.large_road_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventLargeRoadDecorationBrightness(float c)
        {
            ModLoader.config.large_road_decoration_brightness = c;
            ModLoader.SaveConfig();
        }

        #endregion

        #region Highway config

        private void EventHighwayBrightness(float c)
        {
            ModLoader.config.highway_brightness = c;
            ModLoader.SaveConfig();
        }

        private void EventHighwayNationalBrightness(float c)
        {
            ModLoader.config.highway_national_brightness = c;
            ModLoader.SaveConfig();
        }

        #endregion

        #region Parking marking

        private void EventSmallRoadParking(int c)
        {
            ModLoader.config.basic_road_parking = c;
            ModLoader.SaveConfig();
            RoadsUnited_Core.ApplyVanillaRoadDictionary();
            RoadsUnited_Core.ReplaceNetTextures();
        }

        private void EventMediumRoadParking(int c)
        {
            ModLoader.config.medium_road_parking = c;
            ModLoader.SaveConfig();
            RoadsUnited_Core.ApplyVanillaRoadDictionary();
            RoadsUnited_Core.ReplaceNetTextures();
        }

        private void EventMediumRoadGrassParking(int c)
        {
            ModLoader.config.medium_road_grass_parking = c;
            ModLoader.SaveConfig();
            RoadsUnited_Core.ApplyVanillaRoadDictionary();
            RoadsUnited_Core.ReplaceNetTextures();
        }

        private void EventMediumRoadTreesParking(int c)
        {
            ModLoader.config.medium_road_trees_parking = c;
            ModLoader.SaveConfig();
            RoadsUnited_Core.ApplyVanillaRoadDictionary();
            RoadsUnited_Core.ReplaceNetTextures();
        }

        private void EventMediumRoadBusParking(int c)
        {
            ModLoader.config.medium_road_bus_parking = c;
            ModLoader.SaveConfig();
            RoadsUnited_Core.ApplyVanillaRoadDictionary();
            RoadsUnited_Core.ReplaceNetTextures();
        }

        private void EventLargeRoadParking(int c)
        {
            ModLoader.config.large_road_parking = c;
            ModLoader.SaveConfig();
            RoadsUnited_Core.ApplyVanillaRoadDictionary();
            RoadsUnited_Core.ReplaceNetTextures();
        }

        private void EventLargeRoadBusParking(int c)
        {
            ModLoader.config.large_road_bus_parking = c;
            ModLoader.SaveConfig();
            RoadsUnited_Core.ApplyVanillaRoadDictionary();
            RoadsUnited_Core.ReplaceNetTextures();
        }

        private void EventLargeOnewayParking(int c)
        {
            ModLoader.config.large_oneway_parking = c;
            ModLoader.SaveConfig();
            RoadsUnited_Core.ApplyVanillaRoadDictionary();
            RoadsUnited_Core.ReplaceNetTextures();
        }

        #endregion

        #region Config stuff

        private void EventDisableOptionalArrow_LFR(bool c)
        {
            ModLoader.config.disable_optional_arrow_lfr = c;
            ModLoader.SaveConfig();
            RoadsUnited_CoreProps.ChangeArrowProp();
        }

        private void EventDisableOptionalArrow_LR(bool c)
        {
            ModLoader.config.disable_optional_arrow_lr = c;
            ModLoader.SaveConfig();
            RoadsUnited_CoreProps.ChangeArrowProp();
        }

        private void EventCheckUseCustomColors(bool c)
        {
            ModLoader.config.use_custom_colors = c;
            ModLoader.SaveConfig();
        }


        private void EventResetConfig()
        {
            ModLoader.config = new Configuration();

            ModLoader.SaveConfig();
        }

        // private void EventAtlas()
        // {
        // RoadColorChanger.ReplaceLodRgbAtlas();
        // }
        #endregion

        #region RoadThemeDropdownMenu


        public static UIDropDown dropdown;

        public static UIPanel panel1;
        public static UIPanel panel2;

        // public static UIPanel panel2 = null;
        public static UITextField infoText = null;

        public static UITextField infoText1 = null;

        public static List<string> packNames;

        public static List<string> filteredPackNames;

        public static List<RoadThemePack> packs;

        public static int selectedPackID;

        #endregion

        public void OnSettingsUI(UIHelperBase helper)
        {
            Debug.Log("Settings initializing");
            ModLoader.config = Configuration.Deserialize(ModLoader.configFile);
            if (ModLoader.config == null)
            {
                ModLoader.config = new Configuration();
            }

            ModLoader.SaveConfig();
            Debug.Log("Config loaded.");

            packs = Singleton<RoadThemeManager>.instance.GetAvailablePacks();
            packNames = new List<string> { "Vanilla" };
            packNames.AddRange(from pack in packs select pack.themeName);
            filteredPackNames = new List<string>();
            filteredPackNames = packNames;

            UIHelperBase uIHelperBase2 = helper.AddGroup("Road Themes");
            panel1 = (UIPanel)((UIPanel)((UIHelper)uIHelperBase2).self).parent;
            dropdown = (UIDropDown)uIHelperBase2.AddDropdown(
                "Select Road Theme",
                filteredPackNames.ToArray(),
                ModLoader.config.selected_pack,
                delegate (int selectedIndex)
                    {
                        if (selectedIndex == 0)
                        {
                            ModLoader.config.use_custom_textures = false;
                            RoadsUnited_Core.ApplyVanillaDicts();
                            ModLoader.config.selected_pack = selectedIndex;
                            ModLoader.SaveConfig();
                        }

                        if (selectedIndex > 0)
                        {
                            ModLoader.config.use_custom_textures = true;
                            ModLoader.config.selected_pack = selectedIndex;
                            ModLoader.SaveConfig();

                            // RoadsUnited_CoreMod.infoText.text = RoadThemesUtil.GetDescription(RoadsUnited_CoreMod.packs.Find((RoadThemePack pack) => pack.themeName == RoadsUnited_CoreMod.filteredPackNames[RoadsUnited_CoreMod.dropdown.selectedIndex]));
                            // Debug.Log("Got description");
                            Singleton<RoadThemeManager>.instance.ActivePack = packs.Find(
                                pack => pack.themeName == filteredPackNames[selectedIndex]);
                            Debug.Log("Set active pack");
                            ModLoader.SaveConfig();

                            // RoadsUnited_CoreMod.panel2.isVisible = true;
                        }
                        else
                        {
                            Singleton<RoadThemeManager>.instance.ActivePack = null;
                            ModLoader.config.supportsParkingLots = false;
                            // RoadsUnited_CoreMod.panel2.isVisible = false;
                        }

                        selectedPackID = selectedIndex;
                    });
            dropdown.width = 600f;
            if (dropdown.selectedIndex == 0)
            {
                ModLoader.config.supportsParkingLots = false;
                // RoadsUnited_CoreMod.panel2.isVisible = false;
            }
            
            UIHelperBase uIHelperGeneralSettings = helper.AddGroup("General Settings");

            // uIHelperGeneralSettings.AddCheckbox("Use mods Vanilla roads texture replacements", ModLoader.config.use_custom_textures, EventCheckUseCustomTextures);
            uIHelperGeneralSettings.AddCheckbox(
                "Disable road arrows pointing to the left, front and right.",
                ModLoader.config.disable_optional_arrow_lfr,
                EventDisableOptionalArrow_LFR);
            uIHelperGeneralSettings.AddCheckbox(
                "Disable road arrows pointing left and right.",
                ModLoader.config.disable_optional_arrow_lr,
                EventDisableOptionalArrow_LR);

            // uIHelperGeneralSettings.AddCheckbox("Create Vanilla road texture backup on level load.", ModLoader.config.create_vanilla_dictionary, EventCheckCreateVanillaDictionary);
            // uIHelperGeneralSettings.AddButton("Mess with RgbAtlas", EventAtlas);
            // uIHelperGeneralSettings.AddButton("Revert to Vanilla textures (in-game only)", EventRevertVanillaTextures);
            // uIHelperGeneralSettings.AddButton("Reload selected mod's textures (in-game only)", EventReloadTextures);
            uIHelperGeneralSettings.AddButton(
                "Reset all sliders and configuration next level load. ",
                EventResetConfig);

            // UIHelperBase uIHelperCrackedRoadsSettings = helper.AddGroup("Cracked roads");
            // uIHelperCrackedRoadsSettings.AddCheckbox("Use cracked roads.", ModLoader.config.use_cracked_roads, EventCheckCrack);
            // uIHelperCrackedRoadsSettings.AddSlider("Crack intensity", 0, 1f, 0.125f, ModLoader.config.crackIntensity, new OnValueChanged(EventSlideCrack));
            // uIHelperCrackedRoadsSettings.AddButton("Apply changes. Changes take time and use additional RAM.", EventReloadTextures);

      //      if (ModLoader.config.supportsParkingLots)
            {
                UIHelperBase uIHelperParkingSpaceSettings = helper.AddGroup("Parking space marking");
                uIHelperParkingSpaceSettings.AddDropdown(
                    "Small roads",
                    new[] { "No marking", "Parking spots" },
                    ModLoader.config.basic_road_parking,
                    EventSmallRoadParking);
                uIHelperParkingSpaceSettings.AddDropdown(
                    "Medium roads",
                    new[] { "No marking", "Parking spots" },
                    ModLoader.config.medium_road_parking,
                    EventMediumRoadParking);
                uIHelperParkingSpaceSettings.AddDropdown(
                    "Medium roads grass",
                    new[] { "No marking", "Parking spots" },
                    ModLoader.config.medium_road_grass_parking,
                    EventMediumRoadGrassParking);
                uIHelperParkingSpaceSettings.AddDropdown(
                    "Medium roads trees",
                    new[] { "No marking", "Parking spots" },
                    ModLoader.config.medium_road_trees_parking,
                    EventMediumRoadTreesParking);
                uIHelperParkingSpaceSettings.AddDropdown(
                    "Medium roads buslane",
                    new[] { "No marking", "Parking spots" },
                    ModLoader.config.medium_road_bus_parking,
                    EventMediumRoadBusParking);
                uIHelperParkingSpaceSettings.AddDropdown(
                    "Large roads",
                    new[] { "No marking", "Parking spots" },
                    ModLoader.config.large_road_parking,
                    EventLargeRoadParking);
                uIHelperParkingSpaceSettings.AddDropdown(
                    "Large roads buslane",
                    new[] { "No marking", "Parking spots" },
                    ModLoader.config.large_road_bus_parking,
                    EventLargeRoadBusParking);
                uIHelperParkingSpaceSettings.AddDropdown(
                    "Large Oneways",
                    new[] { "No marking", "Parking spots" },
                    ModLoader.config.large_oneway_parking,
                    EventLargeOnewayParking);
                uIHelperParkingSpaceSettings.AddSpace(10);
            }

            UIHelperBase uIHelperRoadColorSettings = helper.AddGroup("Road brightness settings");
            uIHelperRoadColorSettings.AddCheckbox(
                "Use the road brightness sliders below. Changes only visible after next level loading.",
                ModLoader.config.use_custom_colors,
                EventCheckUseCustomColors);

            UIHelperBase uIHelperSmallRoads = helper.AddGroup("Small Roads");
            uIHelperSmallRoads.AddSlider(
                "Standard",
                0.2f,
                0.8f,
                0.05f,
                ModLoader.config.small_road_brightness,
                this.EventSmallRoadBrightness);
            uIHelperSmallRoads.AddSlider(
                "Decoration (grass and trees)",
                0.2f,
                0.8f,
                0.05f,
                ModLoader.config.small_road_decoration,
                this.EventSmallRoadDecorationBrightness);

            UIHelperBase uIHelperMediumRoads = helper.AddGroup("Medium Roads");
            uIHelperMediumRoads.AddSlider(
                "Standard",
                0.2f,
                0.8f,
                0.05f,
                ModLoader.config.medium_road_brightness,
                this.EventMediumRoadBrightness);
            uIHelperMediumRoads.AddSlider(
                "Decoration (grass and trees)",
                0.2f,
                0.8f,
                0.05f,
                ModLoader.config.medium_road_decoration_brightness,
                this.EventMediumRoadDecorationBrightness);
            
            UIHelperBase uIHelperLargeRoads = helper.AddGroup("Large Roads");
            uIHelperLargeRoads.AddSlider(
                "Standard",
                0.2f,
                0.8f,
                0.05f,
                ModLoader.config.large_road_brightness,
                this.EventLargeRoadBrightness);
            uIHelperLargeRoads.AddSlider(
                "Decoration (grass and trees)",
                0.2f,
                0.8f,
                0.05f,
                ModLoader.config.large_road_decoration_brightness,
                this.EventLargeRoadDecorationBrightness);

            UIHelperBase uIHelperHighways = helper.AddGroup("Highways");
            uIHelperHighways.AddSlider(
                "Standard",
                0.2f,
                0.8f,
                0.05f,
                ModLoader.config.highway_brightness,
                this.EventHighwayBrightness);
            uIHelperHighways.AddSlider(
                "NExt National Road",
                0.2f,
                0.8f,
                0.05f,
                ModLoader.config.highway_national_brightness,
                this.EventHighwayNationalBrightness);
        }
    }
}

