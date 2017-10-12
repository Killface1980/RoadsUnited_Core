namespace RoadsUnited_Core2
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using UnityEngine;

    public static class Helpers
    {
        #region Private Fields

        private static readonly Dictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D>();

        #endregion Private Fields

        #region Public Methods


        public static Texture2D LoadTextureDDS(string fullPath)
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

        #endregion Public Methods
    }
}