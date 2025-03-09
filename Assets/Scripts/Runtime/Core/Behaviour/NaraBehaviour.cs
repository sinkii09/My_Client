using Nara.Game;
using UnityEngine;

public class NaraBehaviour : MonoBehaviour
{
    void Start()
    {
        if (GameApp.Instance.Initialized)
        {
            LateStart();
            return;
        }
        GameApp.Instance.OnAppInit += LateStart;
    }
    void OnDestroy()
    {
        GameApp.Instance.OnAppInit -= LateStart;
    }
    protected virtual void LateStart()
    {

    }
}
