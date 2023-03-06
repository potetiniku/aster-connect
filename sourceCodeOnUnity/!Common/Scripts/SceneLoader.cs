using System;
using AsterConnect.Model;
using AsterConnect.Model.MainApp;
using Cysharp.Threading.Tasks;
using Unity.Linq;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UI.Image;
using Scene = AsterConnect.Model.MainApp.Scene;

public class SceneLoader : MonoBehaviour
{
	[SerializeField]
	public GameObject mask;

	[SerializeField]
	private GameObject transitionsParent;

	[SerializeField]
	private GameObject loadingTransition;

	[SerializeField]
	private GameObject liveStartTransition;

	[SerializeField]
	private Animator animator;
	private const string conditionName = "InTransition";

	public static SceneLoader Instance;
	public ServerState StateOnLoad { get; private set; }

	public event Action OnLoadTitle;

	private Image maskImage;

	private void Start()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
			Destroy(gameObject);

		mask.SetActive(false);
		maskImage = mask.GetComponent<Image>();

		Application.targetFrameRate = 60;
	}

	// タイトル画面から呼び出すメソッド
	public async UniTask LoadLive(ServerState stateOnLoad)
	{
		StateOnLoad = stateOnLoad;
		await BeginTransition(loadingTransition);
		SceneManager.LoadScene(Scene.Live.ToString());
	}

	// 待機画面から呼び出すメソッド
	public async UniTask LoadStage()
	{
		if (RoleSwitch.Role == Role.Controller)
		{
			// 音を鳴らすなどしてライブの開始を知らせる
			VivoxService.Instance.Client.AudioInputDevices.Muted = false;
			return;
		}

		await BeginTransition(liveStartTransition);
		UISwitcher uISwitcher = GameObject.Find("UISwitcher").GetComponent<UISwitcher>();
		uISwitcher.ShowLiveUI(uISwitcher.ShownCanvas);
		await EndTransition();
	}

	public async UniTask LoadTitle()
	{
		await BeginTransition(loadingTransition);

		OnLoadTitle();

		VivoxVoiceManager.Instance.DisconnectAllChannels();
		VivoxVoiceManager.Instance.Logout();
		VivoxService.Instance.Client.Uninitialize();

		Client.DestroyMine();
		// 待たないとコントローラでDestroyが完了しない
		await UniTask.Delay(1000);

		SceneManager.LoadScene(Scene.Title.ToString());
		Destroy(GameObject.Find("NetworkManager"));

		await EndTransition();
	}

	private async UniTask BeginTransition(GameObject transition)
	{
		if (RoleSwitch.Role == Role.Controller) return;

		maskImage.fillOrigin = (int)OriginHorizontal.Left;
		mask.SetActive(true);
		transition.SetActive(true);
		// ↓がないとアニメーションがカクつく
		await UniTask.DelayFrame(1);

		animator.SetBool(conditionName, true);
		await UniTask.WaitUntil(() =>
			animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
	}

	public async UniTask EndTransition()
	{
		if (RoleSwitch.Role == Role.Controller) return;

		maskImage.fillOrigin = (int)OriginHorizontal.Right;
		animator.SetBool(conditionName, false);
		// normalizedTimeが1だと何故かアニメーションが無くなったように見える
		// 1.5でもだめ
		await UniTask.WaitUntil(() =>
			animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 2);
		await UniTask.Delay(500);

		mask.SetActive(false);
		transitionsParent.Children()
			.ForEach(t => t.SetActive(false));
	}
}
