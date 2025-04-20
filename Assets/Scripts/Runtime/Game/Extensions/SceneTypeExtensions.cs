using Nara.Game.Enum;

namespace Nara.Game.Extensions
{
    public static class SceneTypeExtensions
    {
        public static string GetSceneName(SceneType sceneType)
        {
            switch (sceneType)
            {
                case SceneType.Logo:
                    return "Logo";
                case SceneType.MainMenu:
                    return "MainMenu";
                case SceneType.MapSelection:
                    return "MapSelection";
                case SceneType.Gameplay:
                    return "Gameplay";
                default:
                    return "MainMenu";
            }
        }
    }
}