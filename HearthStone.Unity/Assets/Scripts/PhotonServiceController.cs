using UnityEngine;

public class PhotonServiceController : MonoBehaviour
{
    void Awake()
    {
        PhotonService.Instance.OnConnectChange += OnConnectChange;
    }
    void Update()
    {
        PhotonService.Instance.Service();
    }
    void OnGUI()
    {
        if (PhotonService.Instance.ServerConnected)
        {
            GUI.Label(new Rect(20, 10, 100, 20), "connected");
        }
        else
        {
            GUI.Label(new Rect(20, 10, 100, 20), "connect failed");
            if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2, 400, 100), "連接至伺服器"))
            {
                PhotonService.Instance.Connect("HearthStone.DevelopmentServer", "127.0.0.1", 30000);
            }
        }
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
