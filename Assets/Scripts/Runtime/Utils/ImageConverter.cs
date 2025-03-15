using System;
using System.IO;
using System.IO.Compression;
using UnityEditor;
using UnityEngine;

namespace Nara.Utils
{
    public static class ImageConverter
    {
        private const int _bufferSize = 1024;
        public static void WriteTexture(string path, Texture2D texture)
        {
            if (texture == null)
            {
                throw new ArgumentNullException(nameof(texture));
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (!path.EndsWith(".T2D"))
            {
                throw new ArgumentException("Path must be a T2D file.", nameof(path));
            }
            Write(path, texture);
            AssetDatabase.Refresh();
        }
        public static Texture2D ReadTexture(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (!path.EndsWith(".T2D"))
            {
                throw new ArgumentException("Path must be a T2D file.", nameof(path));
            }
            Read(path, out Texture2D texture);
            return texture;
        }
        private static void Write(string path, Texture2D texture)
        {
            byte[] dataBytes = texture.EncodeToPNG();

            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, _bufferSize, true))
            using (GZipStream zipStream = new GZipStream(fileStream, CompressionMode.Compress))
            using (BinaryWriter writer = new BinaryWriter(zipStream))
            {
                writer.Write(texture.width);
                writer.Write(texture.height);

                int offset = 0;
                while (offset < dataBytes.Length)
                {
                    int chunkSize = Mathf.Min(_bufferSize, dataBytes.Length - offset);
                    writer.Write(dataBytes, offset, chunkSize);
                    offset += chunkSize;
                }
            }
        }
        private static void Read(string path, out Texture2D texture)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, _bufferSize, true))
            using (GZipStream zipStream = new GZipStream(fileStream, CompressionMode.Decompress))
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                int width = reader.ReadInt32();
                int height = reader.ReadInt32();

                byte[] bytes = new byte[fileStream.Length - 8];
                int offset = 0;

                while (offset < bytes.Length)
                {
                    int chunkSize = Mathf.Min(_bufferSize, bytes.Length - offset);
                    reader.Read(bytes, offset, chunkSize);
                    offset += chunkSize;
                }

                texture = new Texture2D(width, height);
                texture.LoadImage(bytes);
            }
        }
    }
}