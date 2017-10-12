namespace RoadsUnited_Core2.RoadThemeManager
{
    using System.Collections.Generic;
    using System.IO;

    using ColossalFramework;
    using ColossalFramework.Plugins;

    using RoadsUnited_Core;

    public class RoadThemeManager : Singleton<RoadThemeManager>
    {
        public bool isLoaded;
        private RoadThemePack activePack;

        public RoadThemePack ActivePack
        {
            get => this.activePack;

            set
            {
                this.activePack = value;
                if (this.activePack == null)
                {
                    return;
                }

                ModLoader.Config.texturePackPath = instance.activePack.packPath;
                ModLoader.Config.themeName = instance.activePack.themeName;
                ModLoader.Config.supportsParkingLots = instance.activePack.supportsParkingLots;
                ModLoader.Config.currentTexturesPath_default = Path.Combine(instance.activePack.packPath, "BaseTextures");

                ModLoader.SaveConfig();
            }
        }

        public List<RoadThemePack> GetAvailablePacks()
        {
            List<RoadThemePack> list = new List<RoadThemePack>();
            foreach (PluginManager.PluginInfo current in Singleton<PluginManager>.instance.GetPluginsInfo())
            {
                {
                    // if (current.isEnabled)
                    string text = Path.Combine(current.modPath, "RoadsUnitedTheme.xml");
                    if (File.Exists(text))
                    {
                        RoadThemePack RoadThemePack = RoadThemePack.Deserialize(text);
                        if (RoadThemePack != null)
                        {
                            if (RoadThemePack.themeName == null)
                            {
                                RoadThemePack.themeName = current.name;
                            }

                            RoadThemePack.packPath = current.modPath;
                            list.Add(RoadThemePack);
                        }
                    }
                }
            }

            return list;
        }
    }
}
