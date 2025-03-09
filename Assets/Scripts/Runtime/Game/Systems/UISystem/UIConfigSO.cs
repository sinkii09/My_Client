using Nara.System.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "UIConfigSO", menuName = "Nara/Configs/UIConfigSO")]
public class UIConfigSO : ScriptableObject
{
    [SerializeReference]
    private GameObject[] _handlers;

    public GameObject[] UIHandlers => _handlers;
    public static UIConfigSO Load()
    {
        return Resources.Load<UIConfigSO>("SO/Configs/UIConfigSO");
    }
}
