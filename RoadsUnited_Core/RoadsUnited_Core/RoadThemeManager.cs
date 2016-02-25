using ColossalFramework;
using ColossalFramework.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RoadsUnited_Core
{
    public class RoadThemesManager : Singleton<RoadThemesManager>
    {
        private bool imported = false;
        private const string ModConfigPath = "RoadsUnitedTheme.xml";

        private const string userConfigPath = "RoadsUnitedTheme.xml";
        private Configuration _configuration;
        internal Configuration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    try
                    {
                        _configuration = Configuration.Deserialize(userConfigPath);

                        if (_configuration == null)
                        {
                            _configuration = new Configuration();
                            ModLoader.SaveConfig();
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }

                return _configuration;
            }
        }

        public void ImportThemes()
        {
            if (!imported)
            {
                ImportThemesFromThemeMods();
                imported = true;
            }
        }

        private void ImportThemesFromThemeMods()
        {
            foreach (var pluginInfo in Singleton<PluginManager>.instance.GetPluginsInfo().Where(pluginInfo => pluginInfo.isEnabled))
            {
                try
                {
                    var config = RoadsUnited_Core.Configuration.Deserialize(Path.Combine(pluginInfo.modPath, ModConfigPath));
                    if (config == null)
                    {
                        continue;
                    }
                    foreach (var theme in config.themes)
                    {
                        AddModTheme(theme, pluginInfo.name);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("Error while parsing BuildingThemes.xml of mod " + pluginInfo.name);
                    Debug.LogException(e);
                }
            }
        }

        private void AddModTheme(Configuration.Theme modTheme, string modName)
        {
            if (modTheme == null)
            {
                return;
            }
            Configuration.Theme theme;
            AddImportedTheme(modTheme.name, null, out theme);
            Debug.LogFormat(
                "Imported theme from mod \"{0}\" as theme \"{1}\".",
                modName, theme.name
                );
        }

        private void AddImportedTheme(string themeName, string stylePackage, out Configuration.Theme theme)
        {
            theme = Configuration.getTheme(themeName);
            if (theme == null)
            {
                theme = new Configuration.Theme
                {
                    name = themeName,
                    stylePackage = stylePackage
                };
                Configuration.themes.Add(theme);
            }
            theme.isBuiltIn = true;

        }


    }
}
