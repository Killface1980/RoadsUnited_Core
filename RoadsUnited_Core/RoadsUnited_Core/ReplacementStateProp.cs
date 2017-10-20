namespace RoadsUnited_Core
{

    using UnityEngine;

    public partial class RoadsUnitedCore2
    {
        private struct ReplacementStateProp
        {
            #region Public Fields

            public Texture2D aciMap;

            public Texture2D mainTex;

            public PropInfo propInfo;

            #endregion Public Fields
        }

        /*
                public static Texture2D LoadTexture(string fullPath)
                {
                    Texture2D texture2D = new Texture2D(1, 1);
                    if (TextureCache.TryGetValue(fullPath, out texture2D))
                    {
                        return texture2D;
                    }
        
                    texture2D.LoadImage(File.ReadAllBytes(fullPath));
                    texture2D.name = Path.GetFileName(fullPath);
                    texture2D.anisoLevel = 8;
                    texture2D.Compress(true);
                    return texture2D;
                }
                public static Texture2D DDSLoader.LoadDDS(string fullPath)
                {
                    // Testen ob Textur bereits geladen, in dem Fall geladene Textur zurückgeben
                    if (textureCache.TryGetValue(fullPath, out Texture2D texture))
                    {
                        return texture;
                    }
        
                    // Nein? Textur laden
                    byte[] numArray = File.ReadAllBytes(fullPath);
                    int width = BitConverter.ToInt32(numArray, 16);
                    int height = BitConverter.ToInt32(numArray, 12);
                    texture = new Texture2D(width, height, TextureFormat.DXT1, true);
                    List<byte> list = new List<byte>();
                    for (int index = 0; index < numArray.Length; ++index)
                    {
                        if (index > (int)sbyte.MaxValue)
                        {
                            list.Add(numArray[index]);
                        }
                    }
        
                    texture.LoadRawTextureData(list.ToArray());
                    texture.name = Path.GetFileName(fullPath);
                    texture.anisoLevel = 8;
                    texture.Apply();
                    textureCache.Add(fullPath, texture); // Neu geladene Textur in den Cache packen
                    return texture;
                }
                */

        // deactivated for now
    }
}