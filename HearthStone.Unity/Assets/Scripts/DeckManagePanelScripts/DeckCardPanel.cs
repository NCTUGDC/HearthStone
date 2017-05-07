using HearthStone.Library;
using HearthStone.Protocol;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DeckCardPanel : MonoBehaviour
{
    public GameObject allDeckPanel;
    public Button deckCardButtonPrefab;
    public RectTransform content;
    public Text cardCountText;

    public bool IsUnderBuildDeck { get; private set; }
    public int DeckID { get; private set; }

    public void StartBuildDeck(int deckID)
    {
        gameObject.SetActive(true);
        allDeckPanel.SetActive(false);
        IsUnderBuildDeck = true;
        DeckID = deckID;

        Deck deck;
        if (EndPointManager.EndPoint.Player.FindDeck(DeckID, out deck))
        {
            deck.OnCardChanged += OnDeckCardChanged;
        }

        RenderAllDeckCards();
    }
    public void EndBuildDeck()
    {
        Deck deck;
        if (EndPointManager.EndPoint.Player.FindDeck(DeckID, out deck))
        {
            deck.OnCardChanged -= OnDeckCardChanged;
        }
        gameObject.SetActive(false);
        allDeckPanel.SetActive(true);
        IsUnderBuildDeck = false;
        DeckID = 0;
    }
    public void DeleteDeck()
    {
        EndPointManager.EndPoint.Player.OperationManager.DeleteDeck(DeckID);
        EndBuildDeck();
    }

    public void AddCardToDeck(int cardID)
    {
        EndPointManager.EndPoint.Player.OperationManager.AddCardToDeck(DeckID, cardID);
    }
    public void RemoveCardFromDeck(int cardID)
    {
        EndPointManager.EndPoint.Player.OperationManager.RemoveCardFromDeck(DeckID, cardID);
    }

    private void OnDeckCardChanged(Card card, DataChangeCode changeCode)
    {
        RenderAllDeckCards();
    }
    private void RenderAllDeckCards()
    {
        lock (this)
        {
            foreach (RectTransform child in content)
            {
                Destroy(child.gameObject);
            }

            Deck deck;
            if (EndPointManager.EndPoint.Player.FindDeck(DeckID, out deck))
            {
                content.sizeDelta = new Vector2(230, 60 * deck.TotalCardCount);
                foreach (Card card in deck.Cards)
                {
                    Button block = Instantiate(deckCardButtonPrefab);
                    block.transform.SetParent(content);
                    block.transform.localScale = Vector3.one;
                    block.GetComponentInChildren<Text>().text = card.CardName;
                    int cardID = card.CardID;
                    block.onClick.AddListener(() => RemoveCardFromDeck(cardID));
                }
                cardCountText.text = string.Format("{0}/{1}", deck.TotalCardCount, deck.MaxCardCount);
            }
            else
            {
                cardCountText.text = "";
            }
        }
    }
}
