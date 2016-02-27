using ColossalFramework;
using ColossalFramework.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;
using RoadsUnited_Core;


namespace RoadsUnited_Core
{
    public class RoadThemeManager : Singleton<RoadThemeManager>
    {

        public bool levelIsLoaded;

        public bool isLoaded;

        public static readonly string configDirectory = RoadThemeManager.getModPath();


        public RoadThemePack activePack;

        public RoadThemePack activeDefaultPack;

        public RoadThemePack currentPack;

        public static string getModPath()
        {
            string result = null;
            foreach (PluginManager.PluginInfo current in Singleton<PluginManager>.instance.GetPluginsInfo())
            {
                if (current.isEnabled)
                {
                    string path = Path.Combine(current.modPath, "RoadsUnited.dll");
                    if (File.Exists(path))
                    {
                        result = current.modPath;
                    }
                }
            }
            return result;
        }

        public RoadsUnited_Core config;

        public List<RoadThemePack> GetAvailablePacks()
        {
            List<RoadThemePack> list = new List<RoadThemePack>();
            string[] array = new string[]

            {
                "Vanilla"
            };

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
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string path = array2[i];
                string modPath = RoadThemeManager.getModPath();
                string text2 = Path.Combine(modPath, path);
                string text3 = Path.Combine(text2, "RoadsUnitedTheme.xml");
                if (File.Exists(text3))
                {
                    RoadThemePack RoadThemePack2 = RoadThemePack.Deserialize(text3);
                    if (RoadThemePack2 != null)
                    {
                        RoadThemePack2.packPath = text2;
                        list.Add(RoadThemePack2);
                    }
                }
            }
            return list;
        }
    }
}
