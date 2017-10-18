namespace RoadsUnited_Core2
{
    using System.Collections.Generic;

    using ColossalFramework;

    using JetBrains.Annotations;

    using RoadsUnited_Core2.Statics;

    using UnityEngine;

    public class LODResetter
    {
        private static Texture2D m_lodAprAtlas;

        private static Texture2D m_lodRgbAtlas;

        private static Texture2D m_lodXysAtlas;

        private static Texture2D ScaleTexture(Texture2D sourceTex, int targetWidth, int targetHeight)
        {
            float warpFactorX = 1f;
            float warpFactorY = 1f;
            Color[] destPix;

            Texture2D scaleTex = MakeReadable(sourceTex);

            Texture2D destTex = new Texture2D(targetWidth, targetHeight, TextureFormat.ARGB32, false);
            destPix = new Color[destTex.width * destTex.height];
            int y = 0;
            while (y < destTex.height)
            {
                int x = 0;
                while (x < destTex.width)
                {
                    float xFrac = x * 1.0F / (destTex.width - 1);
                    float yFrac = y * 1.0F / (destTex.height - 1);
                    float warpXFrac = Mathf.Pow(xFrac, warpFactorX);
                    float warpYFrac = Mathf.Pow(yFrac, warpFactorY);
                    destPix[y * destTex.width + x] = scaleTex.GetPixelBilinear(warpXFrac, warpYFrac);
                    x++;
                }

                y++;
            }

            destTex.SetPixels(destPix);
            destTex.Apply();
            UnityEngine.Object.Destroy(scaleTex);

            return destTex;

            // try
            // {
            // ScaledTexDict.Add(xx, destTex);
            // }
            // catch (ArgumentNullException argumentNullException)
            // {
            // }
        }

        public static Texture2D MakeReadable([CanBeNull] Texture2D sourceTex)
        {
            if (sourceTex == null)
            {
                return null;
            }

            // Create a temporary RenderTexture of the same size as the texture
            RenderTexture tmp = RenderTexture.GetTemporary(
                sourceTex.width,
                sourceTex.height,
                0);


            // Blit the pixels on texture to the RenderTexture
            Graphics.Blit(sourceTex, tmp);

            // Set the current RenderTexture to the temporary one we created
            RenderTexture.active = tmp;

            // Create a new readable Texture2D to copy the pixels to it
            Texture2D myTexture2D = new Texture2D(sourceTex.width, sourceTex.height, TextureFormat.ARGB32, false);

            // Copy the pixels from the RenderTexture to the new Texture
            myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
            myTexture2D.name = sourceTex.name;
            myTexture2D.Apply();

            // Reset the active RenderTexture
            // RenderTexture.active = previous;

            // Release the temporary RenderTexture
            RenderTexture.ReleaseTemporary(tmp);
            return myTexture2D;

            // "myTexture2D" now has the same pixels from "texture" and it's readable.
        }

        // NetManager
        public static void ResetLOD()
        {
            Singleton<LoadingManager>.instance.m_loadingProfilerMain.BeginLoading("NetManager.InitRenderData");
            FastList<KeyValuePair<NetInfo, NetInfo.Segment>> segments =
                new FastList<KeyValuePair<NetInfo, NetInfo.Segment>>();
            FastList<KeyValuePair<NetInfo, NetInfo.Node>> nodes = new FastList<KeyValuePair<NetInfo, NetInfo.Node>>();
            FastList<Texture2D> rgbTextures = new FastList<Texture2D>();
            FastList<Texture2D> xysTextures = new FastList<Texture2D>();
            FastList<Texture2D> aprTextures = new FastList<Texture2D>();
            int netCount = PrefabCollection<NetInfo>.LoadedCount();
            segments.EnsureCapacity(netCount * 4);
            nodes.EnsureCapacity(netCount * 4);
            rgbTextures.EnsureCapacity(netCount * 4);
            xysTextures.EnsureCapacity(netCount * 4);
            aprTextures.EnsureCapacity(netCount * 4);
            for (int i = 0; i < netCount; i++)
            {
                NetInfo info = PrefabCollection<NetInfo>.GetLoaded((uint)i);
                if (info != null)
                {
                    if (info.m_segments != null)
                    {
                        foreach (NetInfo.Segment t in info.m_segments)
                        {
                            try
                            {
                                NetInfo.Segment segmentInfo = t;
                                if (segmentInfo.m_lodMesh == null || segmentInfo.m_lodMaterial == null)
                                {
                                    info.InitMeshData(segmentInfo, new Rect(0f, 0f, 1f, 1f), null, null, null);
                                }
                                else
                                {
                                    Texture2D rgb = null;
                                    if (segmentInfo.m_lodMaterial.GetTexture(TexType.MainTex) != null)
                                    {
                                        rgb = segmentInfo.m_lodMaterial.GetTexture(TexType.MainTex) as Texture2D;
                                    }

                                    Texture2D xys = null;
                                    if (segmentInfo.m_lodMaterial.GetTexture(TexType.XYSMap) != null)
                                    {
                                        xys = segmentInfo.m_lodMaterial.GetTexture(TexType.XYSMap) as Texture2D;
                                    }

                                    Texture2D apr = null;
                                    if (segmentInfo.m_lodMaterial.GetTexture(TexType.APRMap) != null)
                                    {
                                        apr = segmentInfo.m_lodMaterial.GetTexture(TexType.APRMap) as Texture2D;
                                    }

                                    if (rgb == null && xys == null && apr == null)
                                    {
                                        info.InitMeshData(segmentInfo, new Rect(0f, 0f, 1f, 1f), null, null, null);
                                    }
                                    else
                                    {
                                        if (rgb == null)
                                        {
                                            throw new PrefabException(info, "LOD diffuse null");
                                        }

                                        if (xys == null)
                                        {
                                            throw new PrefabException(info, "LOD xys null");
                                        }

                                        if (apr == null)
                                        {
                                            throw new PrefabException(info, "LOD apr null");
                                        }


                                        if (xys.width != rgb.width || xys.height != rgb.height)
                                        {
                                            throw new PrefabException(
                                                info,
                                                "LOD xys size " + xys.width + "x" + xys.height
                                                + " doesnt match diffuse size " + rgb.width + "x" + rgb.height);

                                            // rgb = ScaleTexture(rgb, xys.width, xys.height);
                                        }

                                        if (apr.width != rgb.width || apr.height != rgb.height)
                                        {
                                            throw new PrefabException(info, "LOD aci size doesnt match diffuse size");

                                            // apr = ScaleTexture(apr, rgb.width, rgb.height);
                                        }

                                        try
                                        {
                                            rgb.GetPixel(0, 0);
                                        }
                                        catch (UnityException)
                                        {
                                            throw new PrefabException(info, "LOD diffuse not readable");
                                        }

                                        try
                                        {
                                            xys.GetPixel(0, 0);
                                        }
                                        catch (UnityException)
                                        {
                                            throw new PrefabException(info, "LOD xys not readable");
                                        }

                                        try
                                        {
                                            apr.GetPixel(0, 0);
                                        }
                                        catch (UnityException)
                                        {
                                            throw new PrefabException(info, "LOD aci not readable");
                                        }

                                        segments.Add(new KeyValuePair<NetInfo, NetInfo.Segment>(info, segmentInfo));
                                        nodes.Add(new KeyValuePair<NetInfo, NetInfo.Node>(null, null));
                                        rgbTextures.Add(rgb);
                                        xysTextures.Add(xys);
                                        aprTextures.Add(apr);
                                    }
                                }
                            }
                            catch (PrefabException ex)
                            {
                                PrefabException e = ex;
                                CODebugBase<LogChannel>.Error(
                                    LogChannel.Core,
                                    string.Concat(
                                        e.m_prefabInfo.gameObject.name,
                                        ": ",
                                        e.Message,
                                        "\n",
                                        e.StackTrace),
                                    e.m_prefabInfo.gameObject);
                                LoadingManager expr_53B = Singleton<LoadingManager>.instance;
                                string brokenAssets = expr_53B.m_brokenAssets;
                                expr_53B.m_brokenAssets =
                                    string.Concat(
                                        brokenAssets,
                                        "\n",
                                        e.m_prefabInfo.gameObject.name,
                                        ": ",
                                        e.Message);
                            }
                        }
                    }

                    if (info.m_nodes != null)
                    {
                        foreach (NetInfo.Node t in info.m_nodes)
                        {
                            try
                            {
                                NetInfo.Node nodeInfo = t;
                                if (nodeInfo.m_lodMesh == null || nodeInfo.m_lodMaterial == null)
                                {
                                    info.InitMeshData(nodeInfo, new Rect(0f, 0f, 1f, 1f), null, null, null);
                                }
                                else
                                {
                                    Texture2D rgb2 = null;
                                    if (nodeInfo.m_lodMaterial.GetTexture(TexType.MainTex))
                                    {
                                        rgb2 = nodeInfo.m_lodMaterial.GetTexture(TexType.MainTex) as Texture2D;
                                    }

                                    Texture2D xys2 = null;
                                    if (nodeInfo.m_lodMaterial.GetTexture(TexType.XYSMap))
                                    {
                                        xys2 = nodeInfo.m_lodMaterial.GetTexture(TexType.XYSMap) as Texture2D;
                                    }

                                    Texture2D apr2 = null;
                                    if (nodeInfo.m_lodMaterial.GetTexture(TexType.APRMap))
                                    {
                                        apr2 = nodeInfo.m_lodMaterial.GetTexture(TexType.APRMap) as Texture2D;
                                    }

                                    if (rgb2 == null && xys2 == null && apr2 == null)
                                    {
                                        info.InitMeshData(nodeInfo, new Rect(0f, 0f, 1f, 1f), null, null, null);
                                    }
                                    else
                                    {
                                        if (rgb2 == null)
                                        {
                                            throw new PrefabException(info, "LOD diffuse null");
                                        }

                                        if (xys2 == null)
                                        {
                                            throw new PrefabException(info, "LOD xys null");
                                        }

                                        if (apr2 == null)
                                        {
                                            throw new PrefabException(info, "LOD apr null");
                                        }

                                        if (xys2.width != rgb2.width || xys2.height != rgb2.height)
                                        {
                                            nodeInfo.m_lodMaterial.SetTexture(TexType.MainTex, ScaleTexture(rgb2, xys2.width, xys2.height));
                                        }

                                        if (apr2.width != rgb2.width || apr2.height != rgb2.height)
                                        {
                                            nodeInfo.m_lodMaterial.SetTexture(TexType.APRMap, ScaleTexture(apr2, xys2.width, xys2.height));
                                        }


                                        if (xys2.width != rgb2.width || xys2.height != rgb2.height)
                                        {
                                            throw new PrefabException(
                                                info,
                                                "LOD xys size " + xys2.width + "x" + xys2.height
                                                + " doesnt match diffuse size " + rgb2.width + "x" + rgb2.height);

                                            // rgb2 = ScaleTexture(rgb2, xys2.width, xys2.height);
                                        }

                                        if (apr2.width != rgb2.width || apr2.height != rgb2.height)
                                        {
                                            throw new PrefabException(info, "LOD aci size doesnt match diffuse size");

                                            // apr2 = ScaleTexture(apr2, rgb2.width, rgb2.height);
                                        }

                                        try
                                        {
                                            rgb2.GetPixel(0, 0);
                                        }
                                        catch (UnityException)
                                        {
                                            throw new PrefabException(info, "LOD diffuse not readable");
                                        }

                                        try
                                        {
                                            xys2.GetPixel(0, 0);
                                        }
                                        catch (UnityException)
                                        {
                                            throw new PrefabException(info, "LOD xys not readable");
                                        }

                                        try
                                        {
                                            apr2.GetPixel(0, 0);
                                        }
                                        catch (UnityException)
                                        {
                                            throw new PrefabException(info, "LOD aci not readable");
                                        }

                                        segments.Add(new KeyValuePair<NetInfo, NetInfo.Segment>(null, null));
                                        nodes.Add(new KeyValuePair<NetInfo, NetInfo.Node>(info, nodeInfo));
                                        rgbTextures.Add(rgb2);
                                        xysTextures.Add(xys2);
                                        aprTextures.Add(apr2);
                                    }
                                }
                            }
                            catch (PrefabException ex2)
                            {
                                PrefabException e2 = ex2;
                                CODebugBase<LogChannel>.Error(
                                    LogChannel.Core,
                                    string.Concat(
                                        e2.m_prefabInfo.gameObject.name,
                                        ": ",
                                        e2.Message,
                                        "\n",
                                        e2.StackTrace),
                                    e2.m_prefabInfo.gameObject);
                                LoadingManager instance = Singleton<LoadingManager>.instance;
                                string brokenAssets = instance.m_brokenAssets;
                                instance.m_brokenAssets =
                                    string.Concat(
                                        brokenAssets,
                                        "\n",
                                        e2.m_prefabInfo.gameObject.name,
                                        ": ",
                                        e2.Message);
                            }
                        }
                    }
                }
            }

            // if (m_lodRgbAtlas == null)
            if (true)
            {
                m_lodRgbAtlas =
                    new Texture2D(1024, 1024, TextureFormat.DXT1, true, false)
                    {
                        filterMode = FilterMode.Trilinear,
                        anisoLevel = 8
                    };
            }

            // if (m_lodXysAtlas == null)
            if (true)
            {
                m_lodXysAtlas =
                    new Texture2D(1024, 1024, TextureFormat.DXT1, true, true)
                    {
                        filterMode = FilterMode.Trilinear,
                        anisoLevel = 8
                    };
            }

            // if (m_lodAprAtlas == null)
            if (true)
            {
                m_lodAprAtlas = new Texture2D(1024, 1024, TextureFormat.DXT1, true, true)
                {
                    filterMode = FilterMode.Trilinear,
                    anisoLevel = 8
                };
            }

            Rect[] rect = m_lodRgbAtlas.PackTextures(rgbTextures.ToArray(), 0, 4096, false);
            m_lodXysAtlas.PackTextures(xysTextures.ToArray(), 0, 4096, false);
            m_lodAprAtlas.PackTextures(aprTextures.ToArray(), 0, 4096, false);

            for (int k = 0; k < segments.m_size; k++)
            {
                try
                {
                    if (segments.m_buffer[k].Value != null)
                    {
                        segments.m_buffer[k].Key.InitMeshData(
                            segments.m_buffer[k].Value,
                            rect[k],
                            m_lodRgbAtlas,
                            m_lodXysAtlas,
                            m_lodAprAtlas);
                    }
                    else
                    {
                        nodes.m_buffer[k].Key.InitMeshData(
                            nodes.m_buffer[k].Value,
                            rect[k],
                            m_lodRgbAtlas,
                            m_lodXysAtlas,
                            m_lodAprAtlas);
                    }
                }
                catch (PrefabException ex3)
                {
                    PrefabException e3 = ex3;
                    CODebugBase<LogChannel>.Error(
                        LogChannel.Core,
                        string.Concat(e3.m_prefabInfo.gameObject.name, ": ", e3.Message, "\n", e3.StackTrace),
                        e3.m_prefabInfo.gameObject);
                    LoadingManager instance = Singleton<LoadingManager>.instance;
                    string brokenAssets = instance.m_brokenAssets;
                    instance.m_brokenAssets =
                        string.Concat(brokenAssets, "\n", e3.m_prefabInfo.gameObject.name, ": ", e3.Message);
                }
            }

            Singleton<LoadingManager>.instance.m_loadingProfilerMain.EndLoading();
        }
    }
}