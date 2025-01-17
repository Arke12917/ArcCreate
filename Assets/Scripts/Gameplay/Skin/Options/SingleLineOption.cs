// <auto-generated> to shut up linter
using ArcCreate.Utilities.ExternalAssets;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArcCreate.Gameplay.Skin
{
    [CreateAssetMenu(fileName = "SingleLine", menuName = "Skin Option/SingleLine")]
    public class SingleLineOption : ScriptableObject
    {
        public string Name;
        public bool Enable;
        [SerializeField] private Sprite singleLineSkin;

        public ExternalSprite SingleLineSkin { get; private set; }

        internal void RegisterExternalSkin()
        {
            SingleLineSkin = new ExternalSprite(singleLineSkin, "SingleLine");
        }

        internal async UniTask LoadExternalSkin()
        {
            await SingleLineSkin.Load();
        }

        internal void UnloadExternalSkin()
        {
            SingleLineSkin.Unload();
        }
    }
}