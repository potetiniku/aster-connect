using System;
using PoseAI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarConfig : MonoBehaviour
{
	private static readonly string[] assetPaths =
	{
		"Assets/!Project/Live/Prefabs/Avatars/SampleAvatar.prefab",
		"Assets/!Project/Live/Prefabs/Avatars/SampleAvatar2 Variant.prefab"
	};

	public static string AssetPath { get; private set; } = assetPaths[0];
	public static PoseAI_Modes Mode { get; private set; } = PoseAI_Modes.Room;
	public static bool UseUpperBodyOnly { get; private set; }

	[SerializeField] private TMP_Dropdown avatarSelector;
	[SerializeField] private TMP_Dropdown modeSelector;
	[SerializeField] private Toggle useUpperBodyOnlyToggle;

	public void SetAssetPath()
	{
		AssetPath = assetPaths[avatarSelector.value];
	}

	public void SetMode()
	{
		Mode = FromInt<PoseAI_Modes>(modeSelector.value);
	}

	public void SetUseUpperBodyOnly()
	{
		UseUpperBodyOnly = useUpperBodyOnlyToggle.isOn;
	}

	private static T FromInt<T>(int value) where T : Enum
	{
		return (T)Enum.ToObject(typeof(T), value);
	}
}
