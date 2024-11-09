using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUIManager : MonoBehaviour
{
    #region EXTERNAL DATA
    [SerializeField] private Button _hostBtn;
    [SerializeField] private Button _clientBtn;
    #endregion

    private void OnEnable()
    {
        _hostBtn.onClick.AddListener(HostButtonClick);
        _clientBtn.onClick.AddListener(ClientButtonClick);
    }

    private void OnDisable()
    {
        _hostBtn.onClick.RemoveListener(HostButtonClick);
        _clientBtn.onClick.RemoveListener(ClientButtonClick);
    }

    private void HostButtonClick()
    {
        NetworkManager.Singleton.StartHost();
    }

    private void ClientButtonClick()
    {
        NetworkManager.Singleton.StartClient();
    }

}
