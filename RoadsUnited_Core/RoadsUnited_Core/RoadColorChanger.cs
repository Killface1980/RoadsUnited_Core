using UnityEngine;

namespace RoadsUnited_Core
{
    public class RoadColorChanger : MonoBehaviour
    {
        public static Configuration config;

        public static void ChangeColor(float brightness, string prefab_road_name, string TextureDir)
        {
            for (uint i = 0; i < PrefabCollection<NetInfo>.LoadedCount(); i++)
            {
                NetInfo netInfo = PrefabCollection<NetInfo>.GetLoaded(i);
                if (netInfo == null)
                {
                    continue;
                }

                if (netInfo.name.Equals(prefab_road_name))
                {
                    if (netInfo.m_color != null)
                    {
                        netInfo.m_color = new Color(brightness, brightness, brightness);
                    }
                }

                if (netInfo.name.Equals(prefab_road_name + " Slope"))
                {
                    if (netInfo.m_color != null)
                    {
                        netInfo.m_color = new Color(brightness, brightness, brightness);
                    }
                }

                if (netInfo.name.Equals(prefab_road_name + " Tunnel"))
                {
                    if (netInfo.m_color != null)
                    {
                        netInfo.m_color = new Color(brightness, brightness, brightness);
                    }
                }

                if (netInfo.name.Equals(prefab_road_name + " Elevated"))
                {
                    if (netInfo.m_color != null)
                    {
                        netInfo.m_color = new Color(brightness, brightness, brightness);
                    }
                }

                if (netInfo.name.Equals(prefab_road_name + " Bridge"))
                {
                    if (netInfo.m_color != null)
                    {
                        netInfo.m_color = new Color(brightness, brightness, brightness);
                    }
                }
            }
        }

        // RoadsUnited.RoadColourChanger
        public static void ChangeColorNetExt(float brightness, string prefabClassName, string textureDir)
        {
            for (uint i = 0; i < PrefabCollection<NetInfo>.LoadedCount(); i++)
            {
                NetInfo netInfo = PrefabCollection<NetInfo>.GetLoaded(i);

                if (netInfo == null)
                {
                    continue;
                }

                if (netInfo.m_class.name.Contains(prefabClassName))
                {
                    if (netInfo.m_color != null)
                    {
                        netInfo.m_color = new Color(brightness, brightness, brightness);
                    }
                }
            }
        }
    }
}
