using Nara.Game;
using System;
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
        GameApp.Instance.OnAppInit += OnAppInit;
    }

    private void OnAppInit()
    {
        LateStart();
        GameApp.Instance.OnAppInit -= LateStart;
    }
    protected virtual void LateStart()
    {

    }
}
