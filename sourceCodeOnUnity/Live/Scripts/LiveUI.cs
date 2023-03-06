using System.Collections.Generic;
using AsterConnect.Model.MainApp;
using UnityEngine;

public class LiveUI : MonoBehaviour
{
	public static LiveUI Current => fromRole[GetCurrent()];
	private static readonly Dictionary<UIType, LiveUI> fromRole = new();

	[SerializeField] private UIType target;

	[SerializeField] private GameObject yellButton;
	public GameObject YellButton => yellButton;

	[SerializeField] private GameObject yellMenu;
	public GameObject YellMenu => yellMenu;

	[SerializeField] private GameObject liveDialogs;
	public GameObject LiveDialogs => liveDialogs;

	[SerializeField] private RpsUI rpsUI;
	public RpsUI RpsUI => rpsUI;

	private void Awake()
	{
		fromRole.Add(target, this);
	}

	private static UIType GetCurrent()
	{
		if (RoleSwitch.Role == Role.Server) return UIType.Landscape;
		if (RoleSwitch.Role == Role.Controller) return UIType.Controller;
#if UNITY_ANDROID
		return UIType.Portrait;
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
		return UIType.Landscape;
#endif
	}
}
