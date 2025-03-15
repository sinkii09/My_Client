using Nara.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

public class Test_ReadNWriteTexture : MonoBehaviour
{
    public Texture2D texture;
    public string path;

    SpriteRenderer _renderer;
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    [Button]
    public void WriteTexture()
    {
        ImageConverter.WriteTexture(path, texture);
    }
    [Button]
    public void ReadTexture()
    {
        Texture2D readTexture = ImageConverter.ReadTexture(path);
        _renderer.material.mainTexture = readTexture;
        _renderer.sprite = Sprite.Create(readTexture, new Rect(0, 0, readTexture.width, readTexture.height), Vector2.one * 0.5f);
    }
}
