using HearthStone.Library;
using System.Linq;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField]
    private HandCardBlock handCardBlockPrefab;
    [SerializeField]
    private GameObject emptyHandCardBlockPrefab;

    public void RenderHand(GamePlayer gamePlayer, bool isOpponent)
    {
        foreach (GameObject child in transform)
        {
            Destroy(child);
        }
        int handCardCount = gamePlayer.HandCards.Count();
        int index = 0;
        foreach (var card in gamePlayer.HandCards)
        {
            if(isOpponent)
            {
                GameObject handCard = Instantiate(emptyHandCardBlockPrefab, transform);
                handCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100 * handCardCount / 2 + index * 100, 10);
            }
            else
            {
                HandCardBlock handCard = Instantiate(handCardBlockPrefab, transform);
                handCard.RenderCard(card, gamePlayer.GamePlayerID);
                handCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100 * handCardCount / 2 + index * 100, 10);
            }
            index++;
        }
    }
}
