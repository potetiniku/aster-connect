using AsterConnect.Model.MainApp;
using UnityEngine;

public class RpsUI : MonoBehaviour
{
	[SerializeField] private RpsTitle title;
	[SerializeField] private RpsChoicesUI choicesUI;
	[SerializeField] private RpsResultUI resultUI;
	[SerializeField] private InsertionText insertionText;

	[SerializeField] private RankingBoard rankingBoard;
	public RankingBoard RankingBoard => rankingBoard;

	[SerializeField] private Color startInsertion;
	[SerializeField] private Color endInsertion;

	public void ShowTitle()
	{
		if (RoleSwitch.Role == Role.Controller) return;
		gameObject.SetActive(true);
		_ = title.Show();
	}

	public void StartRound()
	{
		if (RoleSwitch.Role == Role.Controller) return;
		choicesUI.Show();
		_ = insertionText.Play("スタート!", startInsertion);
	}

	public void ShowResult(RpsResult result)
	{
		if (RoleSwitch.Role == Role.Controller) return;
		choicesUI.Hide();
		_ = resultUI.Show(result);
	}

	public void InsertEndText()
	{
		_ = insertionText.Play("終了!", endInsertion);
	}
}
