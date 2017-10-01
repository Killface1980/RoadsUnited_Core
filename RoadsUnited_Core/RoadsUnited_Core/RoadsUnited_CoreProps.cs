namespace RoadsUnited_Core
{
    using System;
    using System.IO;

    using ColossalFramework;

    using UnityEngine;

    public class RoadsUnited_CoreProps : MonoBehaviour
    {
        public static void ReplacePropTextures()
        {
            string path = ModLoader.currentTexturesPath_default;
            string path2 = ModLoader.currentTexturesPath_default+ "/PropTextures";
            PropCollection[] array = FindObjectsOfType<PropCollection>();
            foreach (PropCollection propCollection in array)
            {
                try
                {
                    PropInfo[] prefabs = propCollection.m_prefabs;
                    foreach (PropInfo propInfo in prefabs)
                    {
                        if (propInfo.m_lodMaterialCombined.GetTexture("_MainTex").name.IsNullOrWhiteSpace())
                        {
                            continue;
                        }

                        string  defaultname = propInfo.m_lodMaterialCombined.GetTexture("_MainTex").name;

                        if (defaultname.IsNullOrWhiteSpace())
                        {
                        }

                        string propLodTexture = Path.Combine(path, defaultname + ".dds");
                        string propLodACIMapTexture = Path.Combine(path, defaultname + "-aci.dds");
                        string propLodTexture2 = Path.Combine(path2, defaultname + ".dds");
                        string propLodACIMapTexture2 = Path.Combine(path2, defaultname + "-aci.dds");

                        if (defaultname == "BusLaneText")
                        {
                            propLodTexture = Path.Combine(path, "BusLane.dds");
                            propLodACIMapTexture = Path.Combine(path, "BusLane-aci.dds");
                            propLodTexture2 = Path.Combine(path2, "BusLane.dds");
                            propLodACIMapTexture2 = Path.Combine(path2, "BusLane-aci.dds");
                        }


                        if (File.Exists(propLodTexture))
                        {
                            // only the m_lodMaterialCombined texture is visible
                            propInfo.m_lodMaterialCombined.SetTexture("_MainTex", propLodTexture.LoadTextureDDS());
                        }
                        else if (File.Exists(propLodTexture2))
                        {
                            // only the m_lodMaterialCombined texture is visible
                            propInfo.m_lodMaterialCombined.SetTexture("_MainTex", propLodTexture2.LoadTextureDDS());
                        }

                        if (File.Exists(propLodACIMapTexture))
                        {
                            propInfo.m_lodMaterialCombined.SetTexture("_ACIMap", propLodACIMapTexture.LoadTextureDDS());
                        }
                        else if (File.Exists(propLodACIMapTexture2))
                        {
                            propInfo.m_lodMaterialCombined.SetTexture("_ACIMap", propLodACIMapTexture2.LoadTextureDDS());
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public static void ChangeArrowProp()
        {
            uint num = 0u;
            while (num < (ulong)PrefabCollection<PropInfo>.LoadedCount())
            {
                PropInfo propInfo = PrefabCollection<PropInfo>.GetLoaded(num);
                if (propInfo == null)
                {
                    continue;
                }
                bool flag  = false;
                bool flag2 = false;

                if (propInfo.name.Equals("Road Arrow LFR"))
                {
                    if (ModLoader.config.disable_optional_arrow_lfr)
                    {
                        propInfo.m_maxRenderDistance = 0f;
                        propInfo.m_maxScale = 0f;
                        propInfo.m_minScale = 0f;
                    }
                    else
                    {
                        propInfo.m_maxRenderDistance = 1000f;
                        propInfo.m_maxScale = 1f;
                        propInfo.m_minScale = 1f;
                    }
                    flag = true;
                }

                if (propInfo.name.Equals("Road Arrow LR"))
                {
                    if (ModLoader.config.disable_optional_arrow_lr)
                    {
                        propInfo.m_maxRenderDistance = 0f;
                        propInfo.m_maxScale = 0f;
                        propInfo.m_minScale = 0f;
                    }
                    else
                    {
                        propInfo.m_maxRenderDistance = 1000f;
                        propInfo.m_maxScale = 1f;
                        propInfo.m_minScale = 1f;
                    }
                    flag2 = true;
                }
                if (flag && flag2)
                {
                    return;
                }
                num += 1u;
            }
        }
    }
}
