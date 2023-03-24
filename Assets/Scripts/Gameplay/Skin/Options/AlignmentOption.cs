// <auto-generated> to shut up linter
using ArcCreate.Utility.ExternalAssets;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArcCreate.Gameplay.Skin
{
    [CreateAssetMenu(fileName = "Alignment", menuName = "Skin Option/Alignment")]
    public class AlignmentOption : ScriptableObject
    {
        public string Name;
        public NoteSkinOption DefaultNoteOption;
        public ParticleSkinOption DefaultParticleOption;
        public TrackSkinOption DefaultTrackOption;
        public SingleLineOption DefaultSingleLineOption;
        public AccentOption DefaultAccentOption;
        [SerializeField] public Sprite jacketShadowSkin;
        [SerializeField] public Sprite defaultBackground;

        public ExternalSprite JacketShadowSkin { get; private set; }
        public ExternalSprite DefaultBackground { get; private set; }

        internal void RegisterExternalSkin()
        {
            JacketShadowSkin = new ExternalSprite(jacketShadowSkin, "JacketShadow");
            DefaultBackground = new ExternalSprite(defaultBackground, "DefaultBackgrounds");
        }

        internal async UniTask LoadExternalSkin()
        {
            await JacketShadowSkin.Load();
            await DefaultBackground.Load();
        }

        internal void UnloadExternalSkin()
        {
            JacketShadowSkin.Unload();
            DefaultBackground.Unload();
        }
    }
}