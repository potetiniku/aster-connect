using AsterConnect.Model.MainApp;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = AsterConnect.Model.MainApp.Scene;

public class RoleSwitch : MonoBehaviour
{
    /// <summary>
    /// ���̃A�v���̃C���X�^���X���ǂ̃T�u�V�X�e���ɊY�����邩
    /// </summary>
    public static Role Role;

    [SerializeField] private Role role = Role.Client;
    [SerializeField] private GameObject androidCanvas;
    [SerializeField] private GameObject windowsCanvas;
    [SerializeField] private GameObject controllerCanvas;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject eventSystem;
    [SerializeField] private GameObject xrOrigin;

    private void Start()
    {
        Role = role;

#if !UNITY_EDITOR && UNITY_SERVER
        Role = Role.Server;
        SceneManager.LoadScene(Scene.Live.ToString());
        return;
#endif

        // �v���b�g�t�H�[���ˑ��R���p�C���̉e��
#pragma warning disable CS0162 // ���B�ł��Ȃ��R�[�h�����o����܂���
        switch (Role)
        {
            case Role.Server:
                SceneManager.LoadScene(Scene.Live.ToString());
                break;

            case Role.Controller:
                controllerCanvas.SetActive(true);
                xrOrigin.SetActive(true);
                break;

            case Role.Client:
                mainCamera.SetActive(true);
                eventSystem.SetActive(true);
#if UNITY_ANDROID
                androidCanvas.SetActive(true);
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
                windowsCanvas.SetActive(true);
#endif
                break;
        }
#pragma warning restore CS0162 // ���B�ł��Ȃ��R�[�h�����o����܂���
    }
}
