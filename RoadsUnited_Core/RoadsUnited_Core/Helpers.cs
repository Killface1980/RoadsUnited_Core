namespace RoadsUnited_Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using ColossalFramework;

    using JetBrains.Annotations;

    using UnityEngine;

    public static class Helpers
    {
        #region Private Fields

        private static readonly Dictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D>();

        #endregion Private Fields

        #region Public Methods

        public static bool CheckAndSetSegmentMaterial(this NetInfo.Segment segment, [CanBeNull] string path, string fileName)
        {
            string log = segment + " - " + ModLoader.currentTexturesPath_default + " - " + fileName;

            if (path.IsNullOrWhiteSpace() || fileName.IsNullOrWhiteSpace())
            {
                return false;
            }

            System.Diagnostics.Debug.Assert(path != null, "path != null");
            if (!SetSegmentDirect(segment, fileName, TexKind.MainTex))
            {
                if (!SetSegmentDirect(segment, fileName + Strings.segmentSuffix + Strings.mainTexSuffix, TexKind.MainTex))
                {
                    return false;
                };
            }
            return true;

            // if (segment.m_segmentMaterial.GetTexture(aprmap) != null)
            // {
            //
            //     SetSegmentDirect(segment, fileName, TexKind.APRMap);
            //     string aprPath = fileName + segmentSuffix + aprMapSuffix;
            //
            //     string path2 = segment.m_segmentMaterial.GetTexture(aprmap).name + ext_DDS;
            //     if (File.Exists(Path.Combine(path, path2)))
            //   {
            //       log += "\nAPR map found at: " + path + aprPath;
            //       segment.m_segmentMaterial.SetTexture(
            //           aprmap,
            //           LoadTextureDDS(Path.Combine(path, aprPath)));
            //   }
            //     else if (File.Exists(Path.Combine(path, aprPath)))
            //   {
            //       log += "\nAPR map found at: " + path + aprPath;
            //       segment.m_segmentMaterial.SetTexture(
            //           aprmap,
            //           LoadTextureDDS(Path.Combine(path, aprPath)));
            //   }
            //     else if (File.Exists(Path.Combine(ModLoader.APRMaps_Path, path2)))
            //   {
            //       log += "\nUsing main mod APR map: " + ModLoader.APRMaps_Path + " - " + path2;
            //       segment.m_segmentMaterial.SetTexture(
            //           aprmap,
            //           LoadTextureDDS(Path.Combine(ModLoader.APRMaps_Path, path2)));
            //   }
            //     else if (File.Exists(Path.Combine(ModLoader.APRMaps_Path, aprPath)))
            //   {
            //       log += "\nUsing main mod APR map: " + ModLoader.APRMaps_Path + " - " + aprPath;
            //       segment.m_segmentMaterial.SetTexture(
            //           aprmap,
            //           LoadTextureDDS(Path.Combine(ModLoader.APRMaps_Path, aprPath)));
            //   }
            //     else
            //     {
            //         log += "\nNo texture found at: " + Path.Combine(path, aprPath) + " or "
            //                + Path.Combine(ModLoader.APRMaps_Path, aprPath);
            //     }
            // }

            Debug.Log(log);
        }

        public static Texture2D LoadTextureDDS(this string fullPath)
        {
            Texture2D result;
            if (textureCache.TryGetValue(fullPath, out Texture2D texture2D))
            {
                result = texture2D;
            }
            else
            {
                byte[] array = File.ReadAllBytes(fullPath);
                int width = BitConverter.ToInt32(array, 16);
                int height = BitConverter.ToInt32(array, 12);
                texture2D = new Texture2D(width, height, TextureFormat.DXT5, true);
                List<byte> list = new List<byte>();
                for (int i = 0; i < array.Length; i++)
                {
                    if (i > 127)
                    {
                        list.Add(array[i]);
                    }
                }

                texture2D.LoadRawTextureData(list.ToArray());
                texture2D.name = Path.GetFileName(fullPath);
                texture2D.anisoLevel = 8;
                texture2D.Apply();
                textureCache.Add(fullPath, texture2D);
                result = texture2D;
            }

            return result;
        }

        public static bool SetSegmentDirect(this NetInfo.Segment segment, [NotNull] string fileName, TexKind kind, string path = null)
        {
            if (path.IsNullOrWhiteSpace())
            {
                path = ModLoader.currentTexturesPath_default;
            }

            string texType = Strings.type[(int)kind];
            if (kind == TexKind.MainTex)
            {
                if (File.Exists(Path.Combine(path, fileName)))
                {
                    segment.m_segmentMaterial.SetTexture(texType, LoadTextureDDS(Path.Combine(path, fileName)));
                    return true;
                }

                Debug.Log("No MainTex found for: " + path + "/" + fileName);
                return false;
            }

            if (kind == TexKind.APRMap)
            {
                if (File.Exists(Path.Combine(path, fileName)))
                {
                    segment.m_segmentMaterial.SetTexture(
                        texType,
                        LoadTextureDDS(Path.Combine(path, fileName)));
                    return true;
                }
                string aprPath = fileName + Strings.segmentSuffix + Strings.aprMapSuffix;
                if (File.Exists(Path.Combine(path, aprPath)))
                {
                    segment.m_segmentMaterial.SetTexture(
                        texType,
                        LoadTextureDDS(Path.Combine(path, aprPath)));
                    return true;
                }
                if (File.Exists(Path.Combine(ModLoader.APRMaps_Path, fileName)))
                {
                    segment.m_segmentMaterial.SetTexture(
                        texType,
                        LoadTextureDDS(Path.Combine(ModLoader.APRMaps_Path, fileName)));
                    return true;
                }

                if (File.Exists(Path.Combine(ModLoader.APRMaps_Path, aprPath)))
                {
                    segment.m_segmentMaterial.SetTexture(
                        texType,
                        LoadTextureDDS(Path.Combine(ModLoader.APRMaps_Path, aprPath)));
                    return true;
                }

                Debug.Log("No APR map found for: " + path + "/" + fileName);
            }
            return false;
        }

        #endregion Public Methods
    }
}