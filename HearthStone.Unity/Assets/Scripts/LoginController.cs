using HearthStone.Protocol;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{
    [SerializeField]
    private InputField accountInputField;
    [SerializeField]
    private InputField passwordInputField;

    private void Start()
    {
        EndPointManager.EndPoint.ResponseManager.OnLoginResponse += OnLoginResponse;
    }
    private void OnDestroy()
    {
        EndPointManager.EndPoint.ResponseManager.OnLoginResponse -= OnLoginResponse;
    }

    public void Login()
    {
        string account = accountInputField.text;
        string password = passwordInputField.text;

        EndPointManager.EndPoint.OperationManager.Login(account, password);
    }

    private void OnLoginResponse(ReturnCode returnCode, string operationMessage)
    {
        switch(returnCode)
        {
            case ReturnCode.Correct:
                SceneManager.LoadScene("MainScene");
                break;
            default:
                Debug.LogFormat("{0}, {1}", returnCode, operationMessage);
                break;
        }
    }
}
