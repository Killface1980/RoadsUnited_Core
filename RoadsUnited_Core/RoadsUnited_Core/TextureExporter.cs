using System.Collections.Generic;
using System.Linq;

namespace RoadsUnited_Core
{
    using System.IO;

    using UnityEngine;

    public static class TextureExporter
    {

        #region Private Fields

        private const string Aprmaps = "/APRMaps";
        private const string AprmapsLod = "/APRMaps/lod";
        private const string Maintex = "/MainTex";
        private const string MaintexLod = "/MainTex/lod";

        private const string NodeString = "/Nodes";

        private const string SegmentString = "/Segments";

        private static readonly List<string> Paths;

        private static bool pathsChecked;

        #endregion Private Fields

        #region Public Constructors

        static TextureExporter()
        {
            Paths = new List<string>
                                     {
                                         ModLoader.Export_Path + SegmentString + MaintexLod,
                                         ModLoader.Export_Path + SegmentString + AprmapsLod,
                                         ModLoader.Export_Path + NodeString + MaintexLod,
                                         ModLoader.Export_Path + NodeString + AprmapsLod,
                                     };
        }

        #endregion Public Constructors

        #region Public Methods

        public static void ExportPrefabTextures(object part)
        {
            // deactivated, all textures created (way too dark, what's wrong?!?)
            if (true)
            {
                return;
            }
            Texture2D texMain = null;
            Texture2D texAPR = null;
            Texture2D texMainLod = null;
            Texture2D texAPRLod = null;

            NetInfo.Segment segment = part as NetInfo.Segment;
            NetInfo.Node node = part as NetInfo.Node;
            bool isSegment = false;

            if (segment != null)
            {
                texMain = LODResetter.MakeReadable(segment.m_segmentMaterial.GetTexture(TexType._MainTex) as Texture2D);
                texAPR = LODResetter.MakeReadable(segment.m_segmentMaterial.GetTexture(TexType._APRMap) as Texture2D);
                texMainLod = LODResetter.MakeReadable(segment.m_lodMaterial.GetTexture(TexType._MainTex) as Texture2D);
                texAPRLod = LODResetter.MakeReadable(segment.m_lodMaterial.GetTexture(TexType._APRMap) as Texture2D);
                isSegment = true;
            }
            else if (node != null)
            {
                texMain = LODResetter.MakeReadable(node.m_nodeMaterial.GetTexture(TexType._MainTex) as Texture2D);
                texAPR = LODResetter.MakeReadable(node.m_nodeMaterial.GetTexture(TexType._APRMap) as Texture2D);
                texMainLod = LODResetter.MakeReadable(node.m_lodMaterial.GetTexture(TexType._MainTex) as Texture2D);
                texAPRLod = LODResetter.MakeReadable(node.m_lodMaterial.GetTexture(TexType._APRMap) as Texture2D);
            }
            else
            {
                return;
            }
            if (!pathsChecked)
            {
                CheckDirectories();
                pathsChecked = true;
            }

            if (texMain != null)
            {
                string path = Path.Combine(ModLoader.Export_Path + (isSegment ? SegmentString : NodeString) + Maintex, texMain.name + ".png");
                if (!File.Exists(path))
                {
                    byte[] bytes = texMain.EncodeToPNG();
                    File.WriteAllBytes(path, bytes);
                }
                string pathLod = Path.Combine(ModLoader.Export_Path + (isSegment ? SegmentString : NodeString) + MaintexLod, texMainLod.name + ".png");
                if (!File.Exists(pathLod))
                {
                    byte[] bytes = texMainLod.EncodeToPNG();
                    File.WriteAllBytes(pathLod, bytes);
                }
            }

            if (texAPR != null)
            {
                string path = Path.Combine(ModLoader.Export_Path + (isSegment ? SegmentString : NodeString) + Aprmaps, texAPR.name + ".png");
                if (!File.Exists(path))
                {
                    byte[] bytes = texAPR.EncodeToPNG();
                    File.WriteAllBytes(path, bytes);
                }
                string pathLod = Path.Combine(ModLoader.Export_Path + (isSegment ? SegmentString : NodeString) + AprmapsLod, texAPRLod.name + ".png");
                if (!File.Exists(pathLod))
                {
                    byte[] bytes = texAPRLod.EncodeToPNG();
                    File.WriteAllBytes(pathLod, bytes);
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static void CheckDirectories()
        {

            foreach (string path in Paths.Where(path => !Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);
                Debug.Log("RU Core Exporter created directory: " + ModLoader.Export_Path + MaintexLod);
            }
        }

        #endregion Private Methods

    }
}