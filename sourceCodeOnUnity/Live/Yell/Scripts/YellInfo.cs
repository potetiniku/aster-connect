using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Scriptable Object/Yell Info")]
public class YellInfo : ScriptableObject
{
	[SerializeField] private Sprite thumbnail;
	public Sprite Thumbnail => thumbnail;

	[SerializeField] private new string name;
	public string Name => name;

	[SerializeField] private Price price;
	public Price Price => price;

	[SerializeField] private Size size;
	public Size Size => size;

	[SerializeField] private YellType type;
	public YellType Type => type;

	[SerializeField] private AssetReferenceT<GameObject> originalPrefab;
	public AssetReferenceT<GameObject> OriginalPrefab => originalPrefab;

	[SerializeField] private string assetPath;
	public string AssetPath => assetPath;

	public void Instantiate()
	{
		YellSpawner.Instance.SpawnServerRpc(AssetPath, size, Client.TempUserName);
	}
}