namespace RoadsUnited_Core2
{
    using UnityEngine;

    public class RoadColorChanger : MonoBehaviour
    {
        public static Configuration config;

        public static void ChangeColor(float brightness, string prefab_road_name)
        {
            Debug.Log("RU Core2 changing colour of: " + prefab_road_name);
            uint num = 0u;
            while ((ulong)num < (ulong)((long)PrefabCollection<NetInfo>.LoadedCount()))
            {
                NetInfo netInfo = PrefabCollection<NetInfo>.GetLoaded(num);
                if (!(netInfo == null))
                {
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

                num += 1u;
            }
        }

        // RoadsUnited.RoadColourChanger
        public static void ChangeColorNetExt(float brightness, string prefabClassName)
        {
            Debug.Log("RU Core2 changing NExt road colors if needed ...");
            uint num = 0u;
            while ((ulong)num < (ulong)((long)PrefabCollection<NetInfo>.LoadedCount()))
            {
                NetInfo netInfo = PrefabCollection<NetInfo>.GetLoaded(num);

                if (!(netInfo == null))
                {
                    if (netInfo.m_class.name.Contains(prefabClassName))
                    {
                        if (netInfo.m_color != null)
                        {
                            netInfo.m_color = new Color(brightness, brightness, brightness);
                        }
                    }
                }

                num += 1u;
            }
        }
    }
}
