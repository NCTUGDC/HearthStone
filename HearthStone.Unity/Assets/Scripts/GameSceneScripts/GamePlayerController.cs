using HearthStone.Library;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayerController : MonoBehaviour
{
    private bool isOpponent;
    private ManaCrystalBlock manaCrystalBlock;
    private Text nicknameText;
    private HandController hand;

    private void Awake()
    {
        manaCrystalBlock = transform.Find("ManaCrystalBlock").GetComponent<ManaCrystalBlock>();
        nicknameText = transform.Find("NicknameText").GetComponent<Text>();
        hand = transform.Find("Hand").GetComponent<HandController>();
    }

    public void InitialGamePlayer(GamePlayer gamePlayer, bool isOpponent)
    {
        this.isOpponent = isOpponent;
        manaCrystalBlock.ObserveGamePlayer(gamePlayer);
        nicknameText.text = gamePlayer.Player.Nickname;
        hand.RenderHand(gamePlayer, isOpponent);
    }
}
