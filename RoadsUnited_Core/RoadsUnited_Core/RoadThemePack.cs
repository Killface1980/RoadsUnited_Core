using ColossalFramework;
using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace RoadsUnited_Core
{
    public class RoadThemePack
    {
        public string themeName;

        public string themeDescription;

        [XmlIgnore]
        public string packPath;

        public RoadThemePack()
        {
        }

        public RoadThemePack(string name, string themeDescription)
        {
            this.themeName = name;
            this.themeDescription = themeDescription;
            this.packPath = packPath;
        }



        public void OnPreSerialize()
        {
        }

        public void OnPostDeserialize()
        {
        }

        public static void Serialize(string filename, RoadThemePack config)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(RoadThemePack));
            using (StreamWriter streamWriter = new StreamWriter(filename))
            {
                config.OnPreSerialize();
                xmlSerializer.Serialize(streamWriter, config);
            }
        }


        public static RoadThemePack Deserialize(string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(RoadThemePack));
            RoadThemePack result;
            try
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    RoadThemePack RoadThemePack = xmlSerializer.Deserialize(streamReader) as RoadThemePack;
                    result = RoadThemePack;
                }
            }
            catch (Exception ex)
            {

                    Debug.Log(string.Format("[{0}]: Error Parsing {1}: {2}", filePath, ex.Message.ToString()));
                
                result = null;
            }
            return result;
        }
    }
}
