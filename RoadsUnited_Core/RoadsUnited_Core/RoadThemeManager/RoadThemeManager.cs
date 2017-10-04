namespace RoadsUnited_Core
{
    using System.Collections.Generic;
    using System.IO;

    using ColossalFramework;
    using ColossalFramework.Plugins;

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

                ModLoader.config.texturePackPath = instance.activePack.packPath;
                ModLoader.config.themeName = instance.activePack.themeName;
                ModLoader.config.supportsParkingLots = instance.activePack.supportsParkingLots;

                ModLoader.SaveConfig();

                RoadsUnited_Core.ApplyVanillaDicts();

                RoadsUnited_Core.ReplaceNetTextures();
                RoadsUnited_CoreProps.ReplacePropTextures();

                if (ModLoader.config.selected_pack == 0)
                {
                }
                else
                {
                    RoadsUnited_CoreProps.ReplacePropTextures();
                }
            }
        }

        public List<RoadThemePack> GetAvailablePacks()
        {
            List<RoadThemePack> list = new List<RoadThemePack>();
            foreach (PluginManager.PluginInfo current in Singleton<PluginManager>.instance.GetPluginsInfo())
            {
              //  if (current.isEnabled)
                {
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
