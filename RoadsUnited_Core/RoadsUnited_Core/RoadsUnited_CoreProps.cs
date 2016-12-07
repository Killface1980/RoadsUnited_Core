using System;
using System.IO;
using UnityEngine;

namespace RoadsUnited_Core
{
    public class RoadsUnited_CoreProps : MonoBehaviour
    {
        public static void ReplacePropTextures()
        {
            string tex = ModLoader.Tex;
            string path = ModLoader.currentTexturesPath_default + "/PropTextures/";
            PropCollection[] array = UnityEngine.Object.FindObjectsOfType<PropCollection>();
            for (int i = 0; i < array.Length; i++)
            {
                PropCollection propCollection = array[i];
                try
                {
                    PropInfo[] prefabs = propCollection.m_prefabs;
                    for (int j = 0; j < prefabs.Length; j++)
                    {
                        PropInfo propInfo = prefabs[j];
                        string defaultname = null;
                        if (propInfo.m_lodMaterialCombined.GetTexture("_MainTex").name != null)
                        {
                            defaultname = propInfo.m_lodMaterialCombined.GetTexture("_MainTex").name;
                        }
                        if (defaultname != null)
                        {
                            string propLodTexture;
                            string propLodACIMapTexture;
                            if (defaultname == "BusLaneText")
                            {
                                propLodTexture = Path.Combine(path, "BusLane.dds");
                                propLodACIMapTexture = Path.Combine(path, "BusLane-aci.dds");
                            }
                            else
                            {
                                propLodTexture = Path.Combine(path, defaultname + ".dds");
                                propLodACIMapTexture = Path.Combine(path, defaultname + "-aci.dds");
                            }
                            if (File.Exists(propLodTexture))
                            {
                                //only the m_lodMaterialCombined texture is visible
                                propInfo.m_lodMaterialCombined.SetTexture("_MainTex", RoadsUnited_Core.LoadTextureDDS(propLodTexture));
                            }
                            if (File.Exists(propLodACIMapTexture))
                            {
                                propInfo.m_lodMaterialCombined.SetTexture("_ACIMap", RoadsUnited_Core.LoadTextureDDS(propLodACIMapTexture));
                            }
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
            while ((ulong)num < (ulong)((long)PrefabCollection<PropInfo>.LoadedCount()))
            {
                PropInfo prefab = PrefabCollection<PropInfo>.GetLoaded(num);
                if (!(prefab == null))
                {
                    PropInfo propInfo = prefab;
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
                    }
                }
                num += 1u;
            }
        }
    }
}
