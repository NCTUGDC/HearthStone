using HearthStone.Library;
using HearthStone.Library.Cards;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AllDeckPanel : MonoBehaviour
{
    public Button deckButtonPrefab;
    public RectTransform content;

    private void Start()
    {
        int cardCount = EndPointManager.EndPoint.Player.Decks.Count();

        content.sizeDelta = new Vector2(200, 60 * cardCount);
        foreach (GameObject child in content)
        {
            Destroy(child);
        }
        foreach (Deck deck in EndPointManager.EndPoint.Player.Decks)
        {
            Button block = Instantiate(deckButtonPrefab);
            block.transform.SetParent(content);
            block.transform.localScale = Vector3.one;
        }
    }

    public void NewDeckPanel()
    {

    }
}
