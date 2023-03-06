using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField]
    private OkCancelDialog okCancelDialog;

    public async void ExitLive()
    {
        if (!await okCancelDialog.Show("�m�F", "���C�u����ޏo���܂����H")) return;
        await SceneLoader.Instance.LoadTitle();
    }
}
