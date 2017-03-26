using HearthStone.Protocol;
using UnityEngine;
using UnityEngine.UI;

public class RegisterController : MonoBehaviour
{
    [SerializeField]
    private InputField accountInputField;
    [SerializeField]
    private InputField passwordInputField;
    [SerializeField]
    private InputField nicknameInputField;

    private void Start()
    {
        EndPointManager.EndPoint.ResponseManager.OnRegisterResponse += OnRegisterResponse;
    }
    private void OnDestroy()
    {
        EndPointManager.EndPoint.ResponseManager.OnRegisterResponse -= OnRegisterResponse;
    }

    public void Register()
    {
        string account = accountInputField.text;
        string password = passwordInputField.text;
        string nickname = nicknameInputField.text;

        EndPointManager.EndPoint.OperationManager.Register(account, password, nickname);
    }
    
    private void OnRegisterResponse(ReturnCode returnCode, string operationMessage)
    {
        Debug.LogFormat("{0}, {1}", returnCode, operationMessage);
    }
}
