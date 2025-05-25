using Sirenix.OdinInspector;

namespace Nara.Game.Config
{
    [ShowOdinSerializedPropertiesInInspector]
    public abstract class BaseConfig : SerializedScriptableObject, IConfig
    {
    }
}