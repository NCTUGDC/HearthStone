using HearthStone.Library;
using UnityEngine;
using UnityEngine.UI;

public class ManaCrystalBlock : MonoBehaviour
{
    private GamePlayer gamePlayer;
    private Text manaCrystalText;

    private void Awake()
    {
        manaCrystalText = GetComponentInChildren<Text>();
    }
    private void OnDestroy()
    {
        gamePlayer.OnManaCrystalChanged -= UpdateManaCrystalText;
        gamePlayer.OnRemainedManaCrystalChanged -= UpdateManaCrystalText;
    }

    public void ObserveGamePlayer(GamePlayer gamePlayer)
    {
        this.gamePlayer = gamePlayer;
        gamePlayer.OnManaCrystalChanged += UpdateManaCrystalText;
        gamePlayer.OnRemainedManaCrystalChanged += UpdateManaCrystalText;
        UpdateManaCrystalText(gamePlayer);
    }
    private void UpdateManaCrystalText(GamePlayer gamePlayer)
    {
        manaCrystalText.text = string.Format("{0}/{1}", gamePlayer.RemainedManaCrystal, gamePlayer.ManaCrystal);
    }
}
