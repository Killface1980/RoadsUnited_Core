namespace RoadsUnited_Core2
{
    using System.Collections.Generic;
    using System.IO;

    using ColossalFramework;
    using ColossalFramework.IO;
    using ColossalFramework.PlatformServices;
    using ColossalFramework.Plugins;
    using ColossalFramework.UI;

    using ICities;

    using JetBrains.Annotations;

    using PrefabHook;

    using RoadsUnited_Core;

    using RoadsUnited_Core2.RoadThemeManager;

    using UnityEngine;

    public class ModLoader : LoadingExtensionBase, IUserMod
    {
        #region Public Methods

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);

            // cancel if Prefab Hook is not installed
            if (!IsHooked())
            {
                return;
            }

            // register event handlers
            // NetInfoHook.OnPreInitialization += OnPreNetInit;
            NetInfoHook.OnPostInitialization += this.OnPostNetInit;
            NetInfoHook.Deploy();
            PropInfoHook.OnPostInitialization += this.OnPostPropInit;
            PropInfoHook.Deploy();

            // deploy (after event handler registration!)
        }

        private void OnPostPropInit(PropInfo propInfo)
        {
            if (Config.selected_pack > 0)
            {
                RoadsUnitedCore2.ChangeArrowProp(propInfo);
                RoadsUnitedCore2.ReplacePropTextures(propInfo, Config.currentTexturesPath_default);
            }
        }

        private static Configuration _config;

        public static Configuration Config
        {
            get
            {
                if (_config == null)
                {
                    _config = Configuration.Deserialize("RoadsUnitedCoreConfig.xml");

                    if (_config == null)
                    {
                        _config = new Configuration();
                    }
                }

                return _config;
            }
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);

            // display warning when level is loaded if Prefab Hook is not installed
            if (!IsHooked())
            {
                UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage(
                    "Missing dependency",
                    this.Name + " requires the 'Prefab Hook' mod to work properly. Please subscribe to the mod and restart the game!",
                    false);
                return;
            }

            // other stuff...
            if (Config.use_custom_colors)
            {
                Debug.Log("RU Core2: Now changing road colours ...");
                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Basic Road");
                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Basic Road Elevated");
                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Basic Road Bridge");

                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Basic Road Bicycle");
                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Basic Road Elevated Bike");
                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Basic Road Bridge Bike");

                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Basic Road Tram");
                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Basic Road Elevated Tram");
                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Basic Road Bridge Tram");
                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Basic Road Slope Tram");
                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Basic Road Tunnel Tram");

                RoadColorChanger.ChangeColor(Config.small_road_decoration, "Basic Road Decoration Grass");
                RoadColorChanger.ChangeColor(Config.small_road_decoration, "Basic Road Decoration Trees");

                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Oneway Road");
                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Oneway Road Elevated");
                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Oneway Road Bridge");
                RoadColorChanger.ChangeColor(Config.small_road_decoration, "Oneway Road Decoration Grass");
                RoadColorChanger.ChangeColor(Config.small_road_decoration, "Oneway Road Decoration Trees");

                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Oneway Road Tram");
                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Oneway Road Elevated Tram");
                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Oneway Road Bridge Tram");
                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Oneway Road Slope Tram");
                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Oneway Road Tunnel Tram");

                RoadColorChanger.ChangeColor(Config.medium_road_brightness, "Medium Road");
                RoadColorChanger.ChangeColor(Config.medium_road_brightness, "Medium Road Elevated");
                RoadColorChanger.ChangeColor(Config.medium_road_brightness, "Medium Road Bridge");

                RoadColorChanger.ChangeColor(Config.medium_road_brightness, "Medium Road Bicycle");
                RoadColorChanger.ChangeColor(Config.medium_road_brightness, "Medium Road Elevated Bike");
                RoadColorChanger.ChangeColor(Config.medium_road_brightness, "Medium Road Bridge Bike");

                RoadColorChanger.ChangeColor(Config.medium_road_brightness, "Medium Road Tram");
                RoadColorChanger.ChangeColor(Config.medium_road_brightness, "Medium Road Elevated Tram");
                RoadColorChanger.ChangeColor(Config.medium_road_brightness, "Medium Road Bridge Tram");
                RoadColorChanger.ChangeColor(Config.medium_road_brightness, "Medium Road Slope Tram");
                RoadColorChanger.ChangeColor(Config.medium_road_brightness, "Medium Road Tunnel Tram");

                RoadColorChanger.ChangeColor(Config.medium_road_decoration_brightness, "Medium Road Decoration Grass");
                RoadColorChanger.ChangeColor(Config.medium_road_decoration_brightness, "Medium Road Decoration Trees");

                RoadColorChanger.ChangeColor(Config.medium_road_brightness, "Medium Road Bus");
                RoadColorChanger.ChangeColor(Config.medium_road_brightness, "Medium Road Elevated Bus");
                RoadColorChanger.ChangeColor(Config.medium_road_brightness, "Medium Road Bridge Bus");
                RoadColorChanger.ChangeColor(Config.medium_road_brightness, "Medium Road Slope Bus");
                RoadColorChanger.ChangeColor(Config.medium_road_brightness, "Medium Road Tunnel Bus");

                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Road");
                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Road Elevated");
                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Road Bridge");

                RoadColorChanger.ChangeColor(Config.large_road_decoration_brightness, "Large Road Decoration Grass");
                RoadColorChanger.ChangeColor(Config.large_road_decoration_brightness, "Large Road Decoration Trees");

                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Road Bicycle");
                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Road Elevated Bike");
                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Road Bridge Bike");

                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Road Bus");
                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Road Elevated Bus");
                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Road Bridge Bus");
                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Road Slope Bus");
                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Road Tunnel Bus");

                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Oneway");
                RoadColorChanger.ChangeColor(
                    Config.large_road_brightness,
                    "Large Oneway Road"); // RCC adds Slope + Tunnel
                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Oneway Elevated");
                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Oneway Bridge");

                RoadColorChanger.ChangeColor(Config.large_road_decoration_brightness, "Large Oneway Decoration Grass");
                RoadColorChanger.ChangeColor(Config.large_road_decoration_brightness, "Large Oneway Decoration Trees");

                RoadColorChanger.ChangeColor(Config.highway_brightness, "HighwayRamp");
                RoadColorChanger.ChangeColor(Config.highway_brightness, "HighwayRampElevated");
                RoadColorChanger.ChangeColor(Config.highway_brightness, "HighwayRamp Slope");
                RoadColorChanger.ChangeColor(Config.highway_brightness, "HighwayRamp Tunnel");

                RoadColorChanger.ChangeColor(Config.highway_brightness, "Highway");
                RoadColorChanger.ChangeColor(Config.highway_brightness, "Highway Slope");
                RoadColorChanger.ChangeColor(Config.highway_brightness, "Highway Tunnel");
                RoadColorChanger.ChangeColor(Config.highway_brightness, "Highway Elevated");
                RoadColorChanger.ChangeColor(Config.highway_brightness, "Highway Bridge");
                RoadColorChanger.ChangeColor(Config.highway_brightness, "Highway Barrier");

                RoadColorChanger.ChangeColorNetExt(Config.small_road_brightness, "NExt2LAlley");
                RoadColorChanger.ChangeColorNetExt(Config.small_road_brightness, "NExt1LOneway");
                RoadColorChanger.ChangeColorNetExt(Config.small_road_brightness, "NExtSmall3LRoad");
                RoadColorChanger.ChangeColorNetExt(Config.small_road_brightness, "NExtSmall4LRoad");
                RoadColorChanger.ChangeColorNetExt(Config.small_road_brightness, "Small Avenue");
                RoadColorChanger.ChangeColorNetExt(Config.small_road_brightness, "Oneway3L");
                RoadColorChanger.ChangeColorNetExt(Config.small_road_brightness, "Oneway4L");
                RoadColorChanger.ChangeColorNetExt(Config.medium_road_brightness, "NExtMediumRoad");
                RoadColorChanger.ChangeColorNetExt(Config.medium_road_brightness, "NExtMediumRoadTunnel");
                RoadColorChanger.ChangeColorNetExt(Config.medium_road_brightness, "NExtMediumRoadTL");
                RoadColorChanger.ChangeColorNetExt(Config.medium_road_brightness, "NExtMediumRoadTLTunnel");
                RoadColorChanger.ChangeColorNetExt(Config.large_road_brightness, "NExtLargeRoad");
                RoadColorChanger.ChangeColorNetExt(Config.large_road_brightness, "NExtLargeRoadTunnel");
                RoadColorChanger.ChangeColorNetExt(Config.large_road_brightness, "NExtLargeRoadTL");
                RoadColorChanger.ChangeColorNetExt(Config.large_road_brightness, "NExtLargeRoadTLTunnel");

                RoadColorChanger.ChangeColorNetExt(Config.large_road_brightness, "NExtXLargeRoad");
                RoadColorChanger.ChangeColorNetExt(Config.large_road_brightness, "NExtXLargeRoadTunnel");

                RoadColorChanger.ChangeColorNetExt(Config.highway_national_brightness, "NExtHighway1L");
                RoadColorChanger.ChangeColorNetExt(Config.highway_national_brightness, "NExtHighwayTunnel1LTunnel");
                RoadColorChanger.ChangeColorNetExt(Config.highway_brightness, "NExtHighway2L");
                RoadColorChanger.ChangeColorNetExt(Config.highway_brightness, "NExtHighwayTunnel2LTunnel");
                RoadColorChanger.ChangeColorNetExt(Config.highway_brightness, "NExtHighway4L");
                RoadColorChanger.ChangeColorNetExt(Config.highway_brightness, "NExtHighwayTunnel4LTunnel");
                RoadColorChanger.ChangeColorNetExt(Config.highway_brightness, "NExtHighway5L");
                RoadColorChanger.ChangeColorNetExt(Config.highway_brightness, "NExtHighwayTunnel5LTunnel");
                RoadColorChanger.ChangeColorNetExt(Config.highway_brightness, "NExtHighway6L");
                RoadColorChanger.ChangeColorNetExt(Config.highway_brightness, "NExtHighwayTunnel6LTunnel");

                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Small Busway");
                RoadColorChanger.ChangeColor(Config.small_road_decoration, "Small Busway Decoration Grass");
                RoadColorChanger.ChangeColor(Config.small_road_decoration, "Small Busway Decoration Trees");

                RoadColorChanger.ChangeColor(Config.small_road_brightness, "Small Busway Oneway");
                RoadColorChanger.ChangeColor(Config.small_road_decoration, "Small Busway Oneway Decoration Grass");
                RoadColorChanger.ChangeColor(Config.small_road_decoration, "Small Busway Oneway Decoration Trees");

                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Road With Bus Lanes");
                RoadColorChanger.ChangeColor(
                    Config.large_road_decoration_brightness,
                    "Large Road Decoration Grass With Bus Lanes");
                RoadColorChanger.ChangeColor(
                    Config.large_road_decoration_brightness,
                    "Large Road Decoration Trees With Bus Lanes");
                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Road Elevated With Bus Lanes");
                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Road Bridge With Bus Lanes");
                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Road Tunnel With Bus Lanes");
                RoadColorChanger.ChangeColor(Config.large_road_brightness, "Large Road Slope With Bus Lanes");
            }

            Debug.Log("RU Core2: OnLevelLoaded finished.");
        }

        // This event handler is called after building initialization
        public void OnPostNetInit(NetInfo info)
        {
            // your code here
            if (Config.selected_pack > 0)
            {
                RoadsUnitedCore2.ReplaceNetTextures(info);
            }
        }

        public override void OnReleased()
        {
            base.OnReleased();

            if (!this.IsHooked())
            {
                return;
            }

            // revert on release
            NetInfoHook.Revert();
            PropInfoHook.Revert();
        }

        #endregion Public Methods


        #region Private Methods

        // // This event handler is called before building initialization
        // public void OnPreNetInit(NetInfo info)
        // {
        // // your code here
        // Debug.Log("Game is now initializing BuildingInfo " + info.name);
        // }

        // checks if the player subscribed to the Prefab Hook mod
        private bool IsHooked()
        {
            foreach (PluginManager.PluginInfo current in PluginManager.instance.GetPluginsInfo())
            {
                if (current.publishedFileID.AsUInt64 == 530771650uL)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion Private Methods

        #region Public Fields

        public const string configPath = "RoadsUnitedCoreConfig.xml";

        public static string APRMaps_Path;

        public static UIDropDown dropdown;

        public static string Export_Path;

        public static List<string> filteredPackNames;

        // public static UIPanel panel2 = null;
        public static UITextField infoText = null;

        public static UITextField infoText1 = null;

        public static List<string> packNames;

        public static List<RoadThemePack> packs;

        // private void EventAtlas()
        // {
        // RoadColorChanger.ReplaceLodRgbAtlas();
        // }
        public static UIPanel panel1;

        public static UIPanel panel2;

        public static int selectedPackID;

        public static string themeName = "None";


        #endregion Public Fields

        #region Private Fields

        private static string modPath;

        #endregion Private Fields

        #region Public Constructors

        // GameObject hookGo;
        // Hook4 hook;
        static ModLoader()
        {
            // Note: this type is marked as 'beforefieldinit'.
            APRMaps_Path = Path.Combine(ModPath, "APRMaps");
            Export_Path = Path.Combine(ModPath, "Export");
        }

        #endregion Public Constructors

        #region Public Properties

        public string Description
        {
            get
            {
                return "Replaces road textures and other road features.";
            }
        }

        public string Name
        {
            get
            {
                return "Roads United Core 2.0";
            }
        }

        #endregion Public Properties

        #region Private Properties

        [NotNull]
        private static string ModPath
        {
            get
            {
                if (modPath.IsNullOrWhiteSpace())
                {
                    modPath = GetModPath();
                }

                return modPath;
            }
        }

        #endregion Private Properties

        #region Public Methods

        public static void SaveConfig()
        {
            Configuration.Serialize(configPath, Config);
        }

        private void ReplaceTextures()
        {
            // string log = "RU Core all files in folder: ";
            // string[] files = Directory.GetFiles(Config.currentTexturesPath_default);
            // foreach (string file in files)
            // {
            //     log += file;
            // }
            // Debug.Log(log);

            for (uint i = 0; i < PrefabCollection<NetInfo>.LoadedCount(); i++)
            {
                NetInfo netInfo = PrefabCollection<NetInfo>.GetLoaded(i);
                if (netInfo == null)
                {
                    continue;
                }

                RoadsUnitedCore2.ReplaceNetTextures(netInfo);
            }

            for (uint i = 0; i < PrefabCollection<PropInfo>.LoadedCount(); i++)
            {
                PropInfo propInfo = PrefabCollection<PropInfo>.GetLoaded(i);
                if (propInfo == null)
                {
                    continue;
                }

                RoadsUnitedCore2.ReplacePropTextures(propInfo, Config.currentTexturesPath_default);
            }
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            packs = Singleton<RoadThemeManager.RoadThemeManager>.instance.GetAvailablePacks();
            packNames = new List<string> { "Vanilla" };
            foreach (RoadThemePack themePack in packs)
            {
                packNames.Add(themePack.themeName);
            }

            filteredPackNames = packNames;

            UIHelperBase uIHelperBase2 = helper.AddGroup("Road Themes");
            panel1 = (UIPanel)((UIPanel)((UIHelper)uIHelperBase2).self).parent;
            dropdown = (UIDropDown)uIHelperBase2.AddDropdown(
                "Select Road Theme",
                filteredPackNames.ToArray(),
                Config.selected_pack,
                delegate (int selectedIndex)
                    {
                        Config.selected_pack = selectedIndex;
                        selectedPackID = selectedIndex;

                        // RoadsUnited_CoreMod.infoText.text = RoadThemesUtil.GetDescription(RoadsUnited_CoreMod.packs.Find((RoadThemePack pack) => pack.themeName == RoadsUnited_CoreMod.filteredPackNames[RoadsUnited_CoreMod.dropdown.selectedIndex]));
                        // Debug.Log("Got description");
                        Singleton<RoadThemeManager.RoadThemeManager>.instance.ActivePack =
                                packs.Find(pack => pack.themeName == filteredPackNames[selectedIndex]);
                        Debug.Log("RU Core 2: Set active pack");
                        this.RevertAll();

                        if (selectedIndex > 0)
                        {
                            this.ReplaceTextures();

                            // RoadsUnited_CoreMod.panel2.isVisible = true;
                        }
                    });
            dropdown.width = 600f;
            if (dropdown.selectedIndex == 0)
            {
                Config.supportsParkingLots = false;

                // RoadsUnited_CoreMod.panel2.isVisible = false;
            }

            UIHelperBase uIHelperGeneralSettings = helper.AddGroup("General Settings");

            // uIHelperGeneralSettings.AddCheckbox("Use mods Vanilla roads texture replacements", ModLoader.config.use_custom_textures, EventCheckUseCustomTextures);
            uIHelperGeneralSettings.AddCheckbox(
                "Disable road arrows pointing to the left, front and right.",
                Config.disable_optional_arrow_lfr,
                this.EventDisableOptionalArrow_LFR);
            uIHelperGeneralSettings.AddCheckbox(
                "Disable road arrows pointing left and right.",
                Config.disable_optional_arrow_lr,
                this.EventDisableOptionalArrow_LR);

            // uIHelperGeneralSettings.AddCheckbox("Create Vanilla road texture backup on level load.", ModLoader.config.create_vanilla_dictionary, EventCheckCreateVanillaDictionary);
            // uIHelperGeneralSettings.AddButton("Mess with RgbAtlas", EventAtlas);
            // uIHelperGeneralSettings.AddButton("Revert to Vanilla textures (in-game only)", EventRevertVanillaTextures);
            // uIHelperGeneralSettings.AddButton("Reload selected mod's textures (in-game only)", EventReloadTextures);
            uIHelperGeneralSettings.AddButton(
                "Reset all sliders and configuration next level load. ",
                this.EventResetConfig);
            {
                // UIHelperBase uIHelperCrackedRoadsSettings = helper.AddGroup("Cracked roads");
                // uIHelperCrackedRoadsSettings.AddCheckbox("Use cracked roads.", ModLoader.config.use_cracked_roads, EventCheckCrack);
                // uIHelperCrackedRoadsSettings.AddSlider("Crack intensity", 0, 1f, 0.125f, ModLoader.config.crackIntensity, new OnValueChanged(EventSlideCrack));
                // uIHelperCrackedRoadsSettings.AddButton("Apply changes. Changes take time and use additional RAM.", EventReloadTextures);
                // if (ModLoader.config.supportsParkingLots)
                UIHelperBase uIHelperParkingSpaceSettings = helper.AddGroup("Parking space marking");
                uIHelperParkingSpaceSettings.AddDropdown(
                    "Small roads",
                    new[] { "No marking", "Parking spots" },
                    Config.basic_road_parking,
                    this.EventSmallRoadParking);
                uIHelperParkingSpaceSettings.AddDropdown(
                    "Medium roads",
                    new[] { "No marking", "Parking spots" },
                    Config.medium_road_parking,
                    this.EventMediumRoadParking);
                uIHelperParkingSpaceSettings.AddDropdown(
                    "Medium roads grass",
                    new[] { "No marking", "Parking spots" },
                    Config.medium_road_grass_parking,
                    this.EventMediumRoadGrassParking);
                uIHelperParkingSpaceSettings.AddDropdown(
                    "Medium roads trees",
                    new[] { "No marking", "Parking spots" },
                    Config.medium_road_trees_parking,
                    this.EventMediumRoadTreesParking);
                uIHelperParkingSpaceSettings.AddDropdown(
                    "Medium roads buslane",
                    new[] { "No marking", "Parking spots" },
                    Config.medium_road_bus_parking,
                    this.EventMediumRoadBusParking);
                uIHelperParkingSpaceSettings.AddDropdown(
                    "Large roads",
                    new[] { "No marking", "Parking spots" },
                    Config.large_road_parking,
                    this.EventLargeRoadParking);
                uIHelperParkingSpaceSettings.AddDropdown(
                    "Large roads buslane",
                    new[] { "No marking", "Parking spots" },
                    Config.large_road_bus_parking,
                    this.EventLargeRoadBusParking);
                uIHelperParkingSpaceSettings.AddDropdown(
                    "Large Oneways",
                    new[] { "No marking", "Parking spots" },
                    Config.large_oneway_parking,
                    this.EventLargeOnewayParking);
                uIHelperParkingSpaceSettings.AddSpace(10);
            }

            UIHelperBase uIHelperRoadColorSettings = helper.AddGroup("Road brightness settings");
            uIHelperRoadColorSettings.AddCheckbox(
                "Use the road brightness sliders below. Changes only visible after next level loading.",
                Config.use_custom_colors,
                this.EventCheckUseCustomColors);

            UIHelperBase uIHelperSmallRoads = helper.AddGroup("Small Roads");
            uIHelperSmallRoads.AddSlider(
                "Standard",
                0.2f,
                0.8f,
                0.05f,
                Config.small_road_brightness,
                this.EventSmallRoadBrightness);
            uIHelperSmallRoads.AddSlider(
                "Decoration (grass and trees)",
                0.2f,
                0.8f,
                0.05f,
                Config.small_road_decoration,
                this.EventSmallRoadDecorationBrightness);

            UIHelperBase uIHelperMediumRoads = helper.AddGroup("Medium Roads");
            uIHelperMediumRoads.AddSlider(
                "Standard",
                0.2f,
                0.8f,
                0.05f,
                Config.medium_road_brightness,
                this.EventMediumRoadBrightness);
            uIHelperMediumRoads.AddSlider(
                "Decoration (grass and trees)",
                0.2f,
                0.8f,
                0.05f,
                Config.medium_road_decoration_brightness,
                this.EventMediumRoadDecorationBrightness);

            UIHelperBase uIHelperLargeRoads = helper.AddGroup("Large Roads");
            uIHelperLargeRoads.AddSlider(
                "Standard",
                0.2f,
                0.8f,
                0.05f,
                Config.large_road_brightness,
                this.EventLargeRoadBrightness);
            uIHelperLargeRoads.AddSlider(
                "Decoration (grass and trees)",
                0.2f,
                0.8f,
                0.05f,
                Config.large_road_decoration_brightness,
                this.EventLargeRoadDecorationBrightness);

            UIHelperBase uIHelperHighways = helper.AddGroup("Highways");
            uIHelperHighways.AddSlider(
                "Standard",
                0.2f,
                0.8f,
                0.05f,
                Config.highway_brightness,
                this.EventHighwayBrightness);
            uIHelperHighways.AddSlider(
                "NExt National Road",
                0.2f,
                0.8f,
                0.05f,
                Config.highway_national_brightness,
                this.EventHighwayNationalBrightness);
        }

        private void RevertAll()
        {
            RoadsUnitedCore2.RevertNodes();
            RoadsUnitedCore2.RevertProps();
            RoadsUnitedCore2.RevertSegments();
        }

        #endregion Public Methods

        #region Private Methods

        private static string GetModPath()
        {
            string text = ".";
            string path = DataLocation.addonsPath + "/Mods/RoadsUnitedCore";
            string result;
            if (Directory.Exists(path))
            {
                Debug.Log("RU Core2: Local path exists, looking for assets here: " + path);
                result = path;
            }
            else
            {
                PublishedFileId[] subscribedItems = PlatformService.workshop.GetSubscribedItems();
                for (int i = 0; i < subscribedItems.Length; i++)
                {
                    PublishedFileId id = subscribedItems[i];
                    if (id.AsUInt64 == 633547552)
                    {
                        text = PlatformService.workshop.GetSubscribedItemPath(id);
                        Debug.Log("RU Core2 using workshop path at: " + text);
                        break;
                    }
                }

                result = text;
            }



            return result;
        }

        private void EventCheckUseCustomColors(bool c)
        {
            Config.use_custom_colors = c;
            SaveConfig();
        }

        private void EventDisableOptionalArrow_LFR(bool c)
        {
            Config.disable_optional_arrow_lfr = c;
            SaveConfig();
            PropInfo propInfo = PrefabCollection<PropInfo>.FindLoaded("Road Arrow LFR");
            RoadsUnitedCore2.ChangeArrowProp(propInfo);
        }

        private void EventDisableOptionalArrow_LR(bool c)
        {
            Config.disable_optional_arrow_lr = c;
            SaveConfig();
            PropInfo propInfo = PrefabCollection<PropInfo>.FindLoaded("Road Arrow LR");
            RoadsUnitedCore2.ChangeArrowProp(propInfo);
        }

        private void EventHighwayBrightness(float c)
        {
            Config.highway_brightness = c;
            SaveConfig();
        }

        private void EventHighwayNationalBrightness(float c)
        {
            Config.highway_national_brightness = c;
            SaveConfig();
        }

        private void EventLargeOnewayParking(int c)
        {
            Config.large_oneway_parking = c;
            SaveConfig();

            // ModLoader.RoadsUnitedCore2.ReplaceNetTextures();
        }

        private void EventLargeRoadBrightness(float c)
        {
            Config.large_road_brightness = c;
            SaveConfig();
        }

        private void EventLargeRoadBusParking(int c)
        {
            Config.large_road_bus_parking = c;
            SaveConfig();

            // RoadsUnitedCore2.ReplaceNetTextures();
        }

        private void EventLargeRoadDecorationBrightness(float c)
        {
            Config.large_road_decoration_brightness = c;
            SaveConfig();
        }

        private void EventLargeRoadParking(int c)
        {
            Config.large_road_parking = c;
            SaveConfig();

            // RoadsUnitedCore2.ReplaceNetTextures();
        }

        private void EventMediumRoadBrightness(float c)
        {
            Config.medium_road_brightness = c;
            SaveConfig();
        }

        private void EventMediumRoadBusParking(int c)
        {
            Config.medium_road_bus_parking = c;
            SaveConfig();

            // RoadsUnitedCore2.ReplaceNetTextures();
        }

        private void EventMediumRoadDecorationBrightness(float c)
        {
            Config.medium_road_decoration_brightness = c;
            SaveConfig();
        }

        private void EventMediumRoadGrassParking(int c)
        {
            Config.medium_road_grass_parking = c;
            SaveConfig();

            // RoadsUnitedCore2.ReplaceNetTextures();
        }

        private void EventMediumRoadParking(int c)
        {
            Config.medium_road_parking = c;
            SaveConfig();

            // RoadsUnitedCore2.ReplaceNetTextures();
        }

        private void EventMediumRoadTreesParking(int c)
        {
            Config.medium_road_trees_parking = c;
            SaveConfig();

            // RoadsUnitedCore2.ReplaceNetTextures();
        }

        private void EventResetConfig()
        {
            _config = new Configuration();

            SaveConfig();
        }

        private void EventSmallRoadBrightness(float c)
        {
            Config.small_road_brightness = c;
            SaveConfig();
        }

        private void EventSmallRoadDecorationBrightness(float c)
        {
            Config.small_road_decoration = c;
            SaveConfig();
        }

        private void EventSmallRoadParking(int c)
        {
            Config.basic_road_parking = c;
            SaveConfig();

            // RoadsUnitedCore2.ReplaceNetTextures();
        }

        #endregion Private Methods
    }
}
