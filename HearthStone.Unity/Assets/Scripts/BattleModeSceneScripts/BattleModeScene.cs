using HearthStone.Library;
using HearthStone.Protocol;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleModeScene : MonoBehaviour
{
    [SerializeField]
    private ToggleGroup group;
    [SerializeField]
    private Text waitingPlayerCountText;
    [SerializeField]
    private Button findOpponentButton;
    [SerializeField]
    private Button backButton;

    private void Start()
    {
        WaitingPlayerCounter.OnWaitingPlayerCountUpdated += UpdateWaitingPlayerCount;
        UpdateWaitingPlayerCount(WaitingPlayerCounter.WaitingPlayerCount);
        EndPointManager.EndPoint.Player.ResponseManager.OnFindOpponentFailed += SetFindOpponentButtonInteractable;
        GameManager.Instance.OnGameChanged += ToGameScene;
    }
    private void OnDestroy()
    {
        WaitingPlayerCounter.OnWaitingPlayerCountUpdated -= UpdateWaitingPlayerCount;
        EndPointManager.EndPoint.Player.ResponseManager.OnFindOpponentFailed -= SetFindOpponentButtonInteractable;
        GameManager.Instance.OnGameChanged -= ToGameScene;
    }

    public void ToMainScene()
    {
        SceneManager.LoadScene("Main");
    }
    public void FindOpponent()
    {
        if (group.AnyTogglesOn())
        {
            Toggle deck = group.ActiveToggles().First();
            EndPointManager.EndPoint.Player.OperationManager.FindOpponent(int.Parse(deck.name));
            findOpponentButton.interactable = false;
            backButton.interactable = false;
        }
    }
    private void UpdateWaitingPlayerCount(int waitingPlayerCount)
    {
        waitingPlayerCountText.text = string.Format("等待人數 : {0}", waitingPlayerCount);
    }
    private void SetFindOpponentButtonInteractable()
    {
        findOpponentButton.interactable = true; ;
        backButton.interactable = true;
    }
    private void ToGameScene(Game game, DataChangeCode changeCode)
    {
        if(changeCode == DataChangeCode.Add)
        {
            GameInsatnce.Game = game;
            SceneManager.LoadScene("Game");
        }
    }
}
