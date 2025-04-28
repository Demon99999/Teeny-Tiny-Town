using Object = UnityEngine.Object;

namespace Assets.Scripts.Infrastructure.AssetPro
{
    public interface IAssetProvider
    {
        TAsset Load<TAsset>(string path)
            where TAsset : Object;
        TAsset[] LoadAll<TAsset>(string path)
            where TAsset : Object;
    }
}