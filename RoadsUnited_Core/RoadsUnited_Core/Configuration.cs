namespace RoadsUnited_Core2
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Xml.Serialization;

    using UnityEngine;

    public class Configuration
    {
        #region Public Fields

        public int basic_road_parking = 0;
        public bool create_vanilla_dictionary = true;

        public bool disable_optional_arrow_lfr = false;
        public bool disable_optional_arrow_lr = false;
        public bool FixateToolbarButton = false;
        public float highway_brightness = 0.4f;
        public float highway_national_brightness = 0.4f;
        public int large_oneway_parking = 0;
        public float large_road_brightness = 0.4f;
        public int large_road_bus_parking = 0;
        public float large_road_decoration_brightness = 0.4f;
        public int large_road_parking = 0;
        public float medium_road_brightness = 0.4f;
        public int medium_road_bus_parking = 0;
        public float medium_road_decoration_brightness = 0.4f;
        public int medium_road_grass_parking = 0;
        public int medium_road_parking = 0;
        public int medium_road_trees_parking = 0;
        public int selected_pack = 0;
        public bool ShowToolbarButton = true;
        public float small_road_brightness = 0.4f;
        public float small_road_decoration = 0.4f;
        public string texturePackPath = string.Empty;
        public string currentTexturesPath_default = string.Empty;
        public string themeName = string.Empty;
        public float ToolbarButtonX;
        public float ToolbarButtonY;
        public bool use_custom_colors = true;

        public bool supportsParkingLots = false;

        #endregion Public Fields

        #region Public Methods

        public static Configuration Deserialize(string filename)
        {
            if (!File.Exists(filename))
            {
                return null;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuration));
            try
            {
                using (StreamReader streamReader = new StreamReader(filename))
                {
                    return (Configuration)xmlSerializer.Deserialize(streamReader);
                }
            }
            catch (Exception e)
            {
                Debug.Log("Couldn't load configuration (XML malformed?)");
                throw e;
            }
        }

        public static void Serialize(string filename, Configuration config)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuration));
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(filename))
                {
                    xmlSerializer.Serialize(streamWriter, config);
                }
            }
            catch (Exception e)
            {
                Debug.Log("Couldn't create configuration file at \"" + Directory.GetCurrentDirectory() + "\"");
                throw e;
            }
        }


        #endregion Public Methods
    }
}
