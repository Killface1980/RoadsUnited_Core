namespace RoadsUnited_Core
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Xml.Serialization;

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "StyleCop.SA1310")]
    [SuppressMessage("ReSharper", "StyleCop.SA1401")]
    [SuppressMessage("ReSharper", "StyleCop.SA1307")]
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
        public float ToolbarButtonX;
        public float ToolbarButtonY;
        public bool use_custom_colors = true;
        public bool use_custom_textures = true;

        #endregion Public Fields

        #region Public Methods

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

        public static void Serialize(string filename, Configuration config)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuration));
            using (StreamWriter streamWriter = new StreamWriter(filename))
            {
                config.OnPreSerialize();
                xmlSerializer.Serialize(streamWriter, config);
            }
        }

        public void OnPostDeserialize()
        {
        }

        public void OnPreSerialize()
        {
        }

        #endregion Public Methods
    }
}
