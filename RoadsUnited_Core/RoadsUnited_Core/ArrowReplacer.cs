using ICities;
using System.IO;
using UnityEngine;
using RoadsUnited_Core;
using System;

namespace RoadsUnited_Core
{
    public class RoadsUnited_CoreArrow : MonoBehaviour
    {


        public static void ReplaceLaneProp()
        {
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
                        string str = propInfo.m_material.name;
                        string text = Path.Combine(ModLoader.currentTexturesPath_default, str + ".dds");
                        string text2 = Path.Combine(ModLoader.currentTexturesPath_default, str + "-aci.dds");
                        if (File.Exists(text))
                        {
                            propInfo.m_material.SetTexture("_MainTex", RoadsUnited_Core.LoadTextureDDS(text));
                        }
                        if (File.Exists(text2))
                        {
                            propInfo.m_material.SetTexture("_ACIMap", RoadsUnited_Core.LoadTextureDDS(text2));
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
