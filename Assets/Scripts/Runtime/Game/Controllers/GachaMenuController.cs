using Nara.Core;
using Nara.Core.Architecture;
using Nara.Game.Command;
using Nara.Game.Data;
using Nara.Game.System;
using NUnit.Framework;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Nara.Game.Controller
{
    public class GachaMenuController : MonoBehaviour, IController
    {
        [SerializeField] private CharacterGachaDataSO _characterGachaData;
        private GachaSystem _gachaSystem;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GameApp.Instance.RegisterController(this);

            _gachaSystem = GameApp.Inteface.GetSystem<GachaSystem>();
            _gachaSystem.LoadCharacterPool(_characterGachaData.Pool,_characterGachaData.RarityProbabilities);
        }

        [Button]
        public void Gacha()
        {
            var result = GameApp.Inteface.SendCommand(new RollGachaCommand());
            Debug.Log($"Gacha Result: {result.ResultCharId}");
        }
    }
}