namespace RoadsUnited_Core
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    using UnityEngine;

    public class RoadThemePack
    {
        public string themeName;
        public string themeDescription;

        public bool supportsParkingLots;

        [XmlIgnore]
        public string packPath;

        public RoadThemePack()
        {
        }

        public void OnPostDeserialize()
        {
        }

        public static RoadThemePack Deserialize(string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(RoadThemePack));
            RoadThemePack result;
            try
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    RoadThemePack roadThemePack = xmlSerializer.Deserialize(streamReader) as RoadThemePack;
                    result = roadThemePack;
                }
            }
            catch (Exception ex)
            {
                Debug.Log(string.Format("[{0}]: Error Parsing {1}: {2}", filePath, ex.Message));
                result = null;
            }

            return result;
        }
    }
}
