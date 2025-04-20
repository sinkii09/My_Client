using Nara.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

public class Test_ReadNWriteTexture : MonoBehaviour
{
    public Texture2D texture;
    public string path;
    public string extension = ".png";
    SpriteRenderer _renderer;
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    [Button]
    public void WriteTexture()
    {
        ImageConverter.WriteTexture(path, extension, texture);
    }
    [Button]
    public void ReadTexture()
    {
        Texture2D readTexture = ImageConverter.ReadTexture(path,extension);
        _renderer.material.mainTexture = readTexture;
        _renderer.sprite = Sprite.Create(readTexture, new Rect(0, 0, readTexture.width, readTexture.height), Vector2.one * 0.5f);
    }
}
