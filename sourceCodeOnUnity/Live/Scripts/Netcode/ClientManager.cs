using System;
using System.Linq;
using AsterConnect.Model;
using Unity.Netcode;

public class ClientManager : NetworkBehaviour
{
	public static bool ShouldLoadStage;
	public static bool ShouldLoadTitle;
	public static bool ShouldAddComment;

	public static string commentUserName;
	public static string commentContent;

	private static bool shouldApplyEachClients;
	private static Action<Client> ApplyEachClientsAction;

	private void Update()
	{
		// GetComponent()はメインスレッド(Updateとか)で呼び出さないと
		// 「GetComponentFastPath can only be called from the main thread.」と
		// 怒られるので、イベントを経由して呼ぶことができない
		// 参考: https://unitygeek.hatenablog.com/entry/2015/08/26/184239
		if (ShouldLoadStage)
		{
			LoadStage();
			ShouldLoadStage = false;
		}

		if (ShouldLoadTitle)
		{
			LoadTitle();
			ShouldLoadTitle = false;
		}

		if (ShouldAddComment)
		{
			AddComment();
			ShouldAddComment = false;
		}

		if (shouldApplyEachClients)
		{
			ApplyEachClients();
			shouldApplyEachClients = false;
		}
	}

	private static void LoadStage()
	{
		ClientsForEach(c => c.LoadStageClientRpc());
	}

	public static void LoadTitle()
	{
		ClientsForEach(c => c.LoadTitleClientRpc());
	}

	public static void AddCommentForEachClients(string userName, string comment)
	{
		commentUserName = userName;
		commentContent = comment;
		ShouldAddComment = true;
	}

	private static void AddComment()
	{
		ClientsForEach(c => c.GetComponent<CommentTransporter>()
				.AddCommentClientRpc(commentUserName, commentContent));
	}

	public static void ApplyEachClients(Action<Client> action)
	{
		shouldApplyEachClients = true;
		ApplyEachClientsAction = action;
	}

	private static void ApplyEachClients()
	{
		ClientsForEach(ApplyEachClientsAction);
	}

	private static void ClientsForEach(Action<Client> action)
	{
		NetworkInitializer.Manager.ConnectedClientsList
			.Select(c => c.PlayerObject.GetComponent<Client>())
			.ForEach(action);
	}
}
