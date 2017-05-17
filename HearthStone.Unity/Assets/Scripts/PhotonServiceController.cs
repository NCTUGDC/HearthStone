using UnityEngine;

public class PhotonServiceController : MonoBehaviour
{
    void Awake()
    {
        PhotonService.Instance.OnConnectChange += OnConnectChange;
    }
    private void Start()
    {
        PhotonService.Instance.Connect("HearthStone.DevelopmentServer", "140.113.123.134", 30000);
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
