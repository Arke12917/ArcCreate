// <auto-generated> to shut up linter
using ArcCreate.Utility.ExternalAssets;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArcCreate.Gameplay.Skin
{
    [CreateAssetMenu(fileName = "Accent", menuName = "Skin Option/Accent")]
    public class AccentOption : ScriptableObject
    {
        public string Name;
        [SerializeField] private Sprite criticalLineSkin;
        [SerializeField] private Sprite criticalLineExtraSkin;
        public Color ComboColor;

        public ExternalSprite CriticalLineSkin { get; private set; }

        public ExternalSprite CriticalLineExtraSkin { get; private set; }

        internal void RegisterExternalSkin()
        {
            CriticalLineSkin = new ExternalSprite(criticalLineSkin, "CriticalLine");
            CriticalLineExtraSkin = new ExternalSprite(criticalLineExtraSkin, "CriticalLine");
        }

        internal async UniTask LoadExternalSkin()
        {
            await CriticalLineSkin.Load();
            await CriticalLineExtraSkin.Load();
        }

        internal void UnloadExternalSkin()
        {
            CriticalLineSkin.Unload();
            CriticalLineExtraSkin.Unload();
        }
    }
}