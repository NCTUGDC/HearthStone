using UnityEngine;

public class PhotonServiceController : MonoBehaviour
{
    void Awake()
    {
        PhotonService.Instance.OnConnectChange += OnConnectChange;
    }
    private void Start()
    {
        PhotonService.Instance.Connect("HearthStone.DevelopmentServer", "127.0.0.1", 30000);
    }
    void Update()
    {
        PhotonService.Instance.Service();
    }
    void OnDestroy()
    {
        PhotonService.Instance.Disconnect();
        PhotonService.Instance.OnConnectChange -= OnConnectChange;
    }

    private void OnConnectChange(bool connected)
    {
        if (connected)
        {
            Debug.Log("Connected");
        }
        else
        {
            Debug.Log("Disconnected");
        }
    }
}
