using ColossalFramework;
using ColossalFramework.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RoadsUnited_Core
{
    public class RoadThemeManager : Singleton<RoadThemeManager>
    {

        public bool isLoaded;




        public RoadThemePack activePack;

        public RoadThemePack activeDefaultPack;

        public RoadThemePack currentPack;





        public RoadThemePack ActivePack
        {
            get
            {
                return this.activePack;
            }
            set
            {
                this.activePack = value;
                if (this.activePack != null)
                {
                    ModLoader.currentTexturesPath_default = Path.Combine(Singleton<RoadThemeManager>.instance.activePack.packPath, "BaseTextures");
                    ModLoader.currentTexturesPath_noParking = Path.Combine(Singleton<RoadThemeManager>.instance.activePack.packPath, "/BaseTextures/noParking");
                    ModLoader.currentTexturesPath_apr_maps = Path.Combine(Singleton<RoadThemeManager>.instance.activePack.packPath, "/BaseTextures/apr_maps");
                    ModLoader.currentTexturesPath_lod_rgb = Path.Combine(Singleton<RoadThemeManager>.instance.activePack.packPath, "/BaseTextures/lod_rgb");
                    ModLoader.currentTexturesPath_NetExt = Path.Combine(Singleton<RoadThemeManager>.instance.activePack.packPath, "NetExtTextures");
                    Debug.Log(Singleton<RoadThemeManager>.instance.activePack.packPath);

                    RoadsUnited_Core.ApplyVanillaDictionary();
                    RoadsUnited_Core.ReplaceNetTextures();
                    return;
                }

            }
        }


        public List<RoadThemePack> GetAvailablePacks()
        {
            List<RoadThemePack> list = new List<RoadThemePack>();

            foreach (PluginManager.PluginInfo current in Singleton<PluginManager>.instance.GetPluginsInfo())
            {
                if (current.isEnabled)
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
