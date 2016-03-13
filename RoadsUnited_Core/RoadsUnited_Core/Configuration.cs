using ColossalFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace RoadsUnited_Core
{
    public class Configuration
    {
        public bool create_vanilla_dictionary = true;

        public bool use_custom_textures = true;
        public bool use_custom_colors = true;
        public bool disable_optional_arrows = true;

        public int selected_pack = 0;

        public int basic_road_parking = 0;
        public int medium_road_parking = 0;
        public int medium_road_grass_parking = 1;
        public int medium_road_trees_parking = 1;
        public int medium_road_bus_parking = 1;
        public int large_road_parking = 0;
        public int large_road_bus_parking = 1;
        public int large_oneway_parking = 1;

        public float ToolbarButtonX;
        public float ToolbarButtonY;


        public float basic_road_ground_brightness = 0.375f;
        public float basic_road_elevated_brightness = 0.375f;
        public float basic_road_bridge_brightness = 0.375f;

        public float basic_road_decoration_grass_brightness = 0.375f;
        public float basic_road_decoration_trees_brightness = 0.375f;
        public float basic_road_bicycle_ground_brightness = 0.375f;
        public float basic_road_bicycle_elevated_brightness = 0.375f;
        public float basic_road_bicycle_bridge_brightness = 0.375f;

        public float basic_road_tram_ground_brightness = 0.375f;
        public float basic_road_tram_elevated_brightness = 0.375f;
        public float basic_road_tram_bridge_brightness = 0.375f;


        public float oneway_road_ground_brightness = 0.375f;
        public float oneway_road_elevated_brightness = 0.375f;
        public float oneway_road_bridge_brightness = 0.375f;
        public float oneway_road_decoration_grass_brightness = 0.375f;
        public float oneway_road_decoration_trees_brightness = 0.375f;

        public float oneway_road_tram_ground_brightness = 0.375f;
        public float oneway_road_tram_elevated_brightness = 0.375f;
        public float oneway_road_tram_bridge_brightness = 0.375f;



        public float medium_road_ground_brightness = 0.375f;
        public float medium_road_elevated_brightness = 0.375f;
        public float medium_road_bridge_brightness = 0.375f;
        public float medium_road_decoration_grass_brightness = 0.375f;
        public float medium_road_decoration_trees_brightness = 0.375f;
        public float medium_road_bicycle_ground_brightness = 0.375f;
        public float medium_road_bicycle_elevated_brightness = 0.375f;
        public float medium_road_bicycle_bridge_brightness = 0.375f;
        public float medium_road_bus_ground_brightness = 0.375f;
        public float medium_road_bus_elevated_brightness = 0.375f;
        public float medium_road_bus_bridge_brightness = 0.375f;
        public float medium_road_tram_ground_brightness = 0.375f;
        public float medium_road_tram_elevated_brightness = 0.375f;
        public float medium_road_tram_bridge_brightness = 0.375f;



        public float large_road_ground_brightness = 0.375f;
        public float large_road_elevated_brightness = 0.375f;
        public float large_road_bridge_brightness = 0.375f;
        public float large_road_decoration_grass_brightness = 0.375f;
        public float large_road_decoration_trees_brightness = 0.375f;
        public float large_road_bicycle_ground_brightness = 0.375f;
        public float large_road_bicycle_elevated_brightness = 0.375f;
        public float large_road_bicycle_bridge_brightness = 0.375f;
        public float large_road_bus_ground_brightness = 0.375f;
        public float large_road_bus_elevated_brightness = 0.375f;
        public float large_road_bus_bridge_brightness = 0.375f;

        public float large_oneway_ground_brightness = 0.375f;
        public float large_oneway_elevated_brightness = 0.375f;
        public float large_oneway_bridge_brightness = 0.375f;
        public float large_oneway_decoration_grass_brightness = 0.375f;
        public float large_oneway_decoration_trees_brightness = 0.375f;

        public float highway_ramp_ground_brightness = 0.5f;
        public float highway_ramp_elevated_brightness = 0.5f;
        public float highway_ground_brightness = 0.5f;
        public float highway_elevated_brightness = 0.5f;
        public float highway_bridge_brightness = 0.5f;
        public float highway_barrier_brightness = 0.5f;

        public bool ShowToolbarButton = true;
        public bool FixateToolbarButton = false;

        public string texturePackPath = "None";

        public void OnPreSerialize()
        {
        }

        public void OnPostDeserialize()
        {
        }

        public static void Serialize(string filename, Configuration config)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuration));
            using (StreamWriter streamWriter = new StreamWriter(filename))
            {
                config.OnPreSerialize();
                xmlSerializer.Serialize(streamWriter, config);
            }
        }

        public static Configuration Deserialize(string filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuration));
            Configuration result;
            try
            {
                using (StreamReader streamReader = new StreamReader(filename))
                {
                    Configuration configuration = (Configuration)xmlSerializer.Deserialize(streamReader);
                    configuration.OnPostDeserialize();
                    result = configuration;
                    return result;
                }
            }
            catch
            {
            }
            result = null;
            return result;
        }
    }

}
