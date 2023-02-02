// <auto-generated> to shut up linter
using System.IO;
using ArcCreate.Gameplay.Data;
using ArcCreate.Utility;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArcCreate.Gameplay.Skin
{
    public abstract class GamemodeNoteSkinOption : ScriptableObject, INoteSkinProvider
    {
        public string Name;
        public Material ArcTapSfxSkin;
        public Color ConnectionLineColor;
        public Mesh ArcTapMesh;
        public Mesh ArcTapSfxMesh;

        public ExternalTexture ArcTapSfxTexture { get; private set; }

        public abstract (Mesh mesh, Material material) GetArcTapSkin(ArcTap note);
        public abstract (Sprite normal, Sprite highlight) GetHoldSkin(Hold note);
        public abstract Sprite GetTapSkin(Tap note);
        public abstract Sprite GetArcCapSprite(Arc arc);

        internal virtual void RegisterExternalSkin()
        {
            ArcTapSfxTexture = new ExternalTexture(ArcTapSfxSkin.mainTexture, "SfxTap");
        }

        internal virtual async UniTask LoadExternalSkin()
        {
            await ArcTapSfxTexture.Load();
            ArcTapSfxSkin.mainTexture = ArcTapSfxTexture.Value;
        }

        internal virtual void UnloadExternalSkin()
        {
            ArcTapSfxTexture.Unload();
            ArcTapSfxSkin.mainTexture = ArcTapSfxTexture.Value;
        }
    }
}