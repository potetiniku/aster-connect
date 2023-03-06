using System;
using System.Linq;
using AsterConnect.Model;
using AsterConnect.Model.MainApp;
using Cysharp.Threading.Tasks;
using Unity.Linq;
using Unity.Services.Vivox;
using UnityEngine;
using VivoxUnity;

public class UISwitcher : MonoBehaviour
{
	[SerializeField] private GameObject androidCanvas;
	[SerializeField] private GameObject windowsCanvas;
	[SerializeField] private GameObject controllerCanvas;
	[SerializeField] private GameObject mainCamera;
	[SerializeField] private GameObject eventSystem;
	[SerializeField] private GameObject xrOrigin;
	[SerializeField] private OkDialog controllerOkDialog;
	[SerializeField] private AudioSource bgmPlayer;

	public GameObject ShownCanvas { get; private set; }

	// HACK: RoleSwitch�Əd�Ȃ镔��������̂ŋ��ʉ��ł�����
	private async void Start()
	{
		if (RoleSwitch.Role == Role.Server) return;
		if (RoleSwitch.Role == Role.Controller)
		{
			ShowControllerUI();
			await ConnectToVivox();
			VivoxService.Instance.Client.AudioInputDevices.Muted =
				SceneLoader.Instance.StateOnLoad == ServerState.Prepared;
			bgmPlayer.Play();
			return;
		}

		mainCamera.SetActive(true);
		eventSystem.SetActive(true);

#if UNITY_ANDROID
		ShownCanvas = androidCanvas;
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
		ShownCanvas = windowsCanvas;
#endif

		await ConnectToVivox();
		VivoxService.Instance.Client.AudioInputDevices.Muted = true;
		VivoxVoiceManager.Instance.SetOthersMute(true);

		if (SceneLoader.Instance.StateOnLoad == ServerState.Prepared)
		{
			ShowPreparingUI(ShownCanvas);
		}
		else if (SceneLoader.Instance.StateOnLoad == ServerState.Streaming)
		{
			ShowLiveUI(ShownCanvas);
		}

		await SceneLoader.Instance.EndTransition();

		async UniTask ConnectToVivox()
		{
			VivoxService.Instance.Client.Initialize();
			VivoxVoiceManager manager = VivoxVoiceManager.Instance;
			await manager.LoginAsync();
			manager.JoinChannel("live", ChannelType.NonPositional,
				VivoxVoiceManager.ChatCapability.AudioOnly);

			// �`�����l���ɓ����Ă�������0���]������Ă��܂�����
			await UniTask.WaitUntil(() => VivoxVoiceManager.Instance
				.ActiveChannels.Single().Participants.Count > 0);
		}
	}

	public async UniTask ShowPortNumber(int portNumber)
	{
		await controllerOkDialog.Show("���",
			string.Join(Environment.NewLine, new[]
			{
				"Pose Cam���J����",
				"Network settings��",
				$"Port�Ɂu{portNumber}�v��",
				"�ݒ肵�Ă�������"
			}));
	}

	public void ShowPreparingUI(GameObject targetCanvas)
	{
		ShowComments(targetCanvas);
		targetCanvas.Child("Preparing").SetActive(true);
		targetCanvas.SetActive(true);
	}

	public void ShowLiveUI(GameObject targetCanvas)
	{
		ShowComments(targetCanvas);
		targetCanvas.Child("YellButton").SetActive(true);
		targetCanvas.SetActive(true);
		VivoxVoiceManager.Instance.SetOthersMute(false);
		bgmPlayer.Play();
	}

	private void ShowComments(GameObject targetCanvas)
	{
		targetCanvas.Children()
			.ForEach(c => c.SetActive(false));

		targetCanvas.Child("CommentView").SetActive(true);
		targetCanvas.Child("CommentField").SetActive(true);
		targetCanvas.Child("ExitButton").SetActive(true);
	}

	private void ShowControllerUI()
	{
		xrOrigin.SetActive(true);
		controllerCanvas.SetActive(true);
	}
}
