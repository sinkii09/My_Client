using System;
using System.IO;
using System.IO.Compression;
using UnityEditor;
using UnityEngine;

namespace Nara.Utils
{
    public static partial class ImageConverter
    {
        private const int _bufferSize = 1024;

        /// <summary>
        /// Writes a texture to a file with the specified extension.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="extension"></param>
        /// <param name="texture"></param>
        /// <exception cref="ArgumentNullException">Thrown if the texture or path is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown if the path does not end with the specified extension.</exception>
        public static void WriteTexture(string path, string extension, Texture2D texture)
        {
            if (texture == null)
            {
                throw new ArgumentNullException(nameof(texture));
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (!path.EndsWith(extension))
            {
                throw new ArgumentException($"Path must be a {extension} file.", nameof(path));
            }
            WriteTexture(path, texture);
            AssetDatabase.Refresh();
        }
        /// <summary>
        /// Reads a texture from a file with the specified extension.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static Texture2D ReadTexture(string path, string extension)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (!path.EndsWith(extension))
            {
                throw new ArgumentException($"Path must be a {extension} file.", nameof(path));
            }
            ReadTexture(path, out Texture2D texture);
            return texture;
        }

        /// <summary>
        /// Writes a texture to a file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="texture"></param>
        private static void WriteTexture(string path, Texture2D texture)
        {
            byte[] dataBytes = Texture2Binary(texture);

            /// <summary>
            /// fileStream is used to create a file at the specified path with write access.
            /// </summary>
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, _bufferSize, true))

            /// <summary>
            /// GZipStream is used to compress the data using GZip compression.
            /// <see href="https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.gzipstream?view=dotnet-plat-ext-7.0">GZipStream</see>
            /// </summary>
            using (GZipStream zipStream = new GZipStream(fileStream, CompressionMode.Compress))

            /// <summary>
            /// BinaryWriter is used to write binary data to the file.
            /// <see href="https://learn.microsoft.com/en-us/dotnet/api/system.io.binarywriter?view=dotnet-plat-ext-7.0">BinaryWriter</see>
            /// </summary>
            using (BinaryWriter writer = new BinaryWriter(zipStream))
            {
                writer.Write(texture.width);
                writer.Write(texture.height);

                int offset = 0;

                /// Write the data in chunks to avoid memory issues (especially for large textures).
                while (offset < dataBytes.Length)
                {
                    int chunkSize = Mathf.Min(_bufferSize, dataBytes.Length - offset);
                    writer.Write(dataBytes, offset, chunkSize);
                    offset += chunkSize;
                }
            }
        }

        /// <summary>
        /// Reads a texture from a file.
        /// </summary>
        private static void ReadTexture(string path, out Texture2D texture)
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

        /// <summary>
        /// Converts a Texture2D to a byte array.
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private static byte[] Texture2Binary(Texture2D texture)
        {
            if (texture == null)
            {
                throw new ArgumentNullException(nameof(texture));
            }
            return texture.EncodeToPNG();
        }
    }
}