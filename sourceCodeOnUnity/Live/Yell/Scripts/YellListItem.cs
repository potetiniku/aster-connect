using TMPro;
using Unity.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class YellListItem
{
	private const string gridAddressablesName = "Assets/!Project/Live/Yell/UI/Prefabs/Grid.prefab";

	public GameObject GameObject { get; private set; }
	public YellInfo Info { get; }

	private YellConfirmationDialog yellDialog;

	public YellListItem(YellInfo info, GameObject gridContainer, YellConfirmationDialog yellDialog)
	{
		Info = info;

		Addressables.InstantiateAsync(
			gridAddressablesName, gridContainer.transform)
			.Completed += Initialize;

		this.yellDialog = yellDialog;
	}

	private void Initialize(AsyncOperationHandle<GameObject> handle)
	{
		GameObject = handle.Result;

		GameObject.Child("Price").Child("Label")
			.GetComponent<TMP_Text>().text =
			Info.Price.GetValue().ToString();

		GameObject.Child("Thumbnail")
			.GetComponent<Image>().sprite = Info.Thumbnail;

		GameObject.GetComponent<Button>()
			.onClick.AddListener(async () => { await yellDialog.Show(Info); });
	}
}
