using Nara.Game.Enum;

namespace Nara.Game.Extensions
{
    public static class SceneTypeExtensions
    {
        public static string GetSceneName(SceneEnum sceneType)
        {
            switch (sceneType)
            {
                case SceneEnum.Logo:
                    return "Logo";
                case SceneEnum.MainMenu:
                    return "MainMenu";
                case SceneEnum.MapSelection:
                    return "MapSelection";
                case SceneEnum.Gameplay:
                    return "Gameplay";
                default:
                    return "MainMenu";
            }
        }
    }
}