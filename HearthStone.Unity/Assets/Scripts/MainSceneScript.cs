using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneScript : MonoBehaviour
{
    public void ToBattleModeScene()
    {
        SceneManager.LoadScene("BattleMode");
    }
}
