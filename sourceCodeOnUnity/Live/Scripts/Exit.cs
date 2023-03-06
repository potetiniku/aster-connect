using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField]
    private OkCancelDialog okCancelDialog;

    public async void ExitLive()
    {
        if (!await okCancelDialog.Show("確認", "ライブから退出しますか？")) return;
        await SceneLoader.Instance.LoadTitle();
    }
}
