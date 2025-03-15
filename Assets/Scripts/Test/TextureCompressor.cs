using UnityEditor;
using UnityEngine;
public class TextureCompressor
{
    [MenuItem("Assets/Compress Texture")]
    static void CompressTexture()
    {
        Texture2D texture = Selection.activeObject as Texture2D;
        if (texture == null) return;

        string path = AssetDatabase.GetAssetPath(texture);
        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

        if (importer != null)
        {
            importer.textureCompression = TextureImporterCompression.CompressedHQ;
            importer.SaveAndReimport();
            Debug.Log("Texture Compressed: " + path);
        }
    }
}
