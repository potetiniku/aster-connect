using System.Linq;
using UnityEngine;

public class YellList : MonoBehaviour
{
	[SerializeField] private GameObject gridContainer;
	[SerializeField] private YellInfo[] yellInfos;
	[SerializeField] private YellConfirmationDialog yellConfirmationDialog;

	private YellListItem[] items;

	private void Start()
	{
		items = yellInfos.Select(y =>
			new YellListItem(y, gridContainer, yellConfirmationDialog)).ToArray();
	}
}
